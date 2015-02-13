using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NLog;
using Panda.Utilities.Extensions;
using Panda.WebCrawler.Extensions;
using Panda.WebCrawler.LinkExtractor;
using Panda.WebCrawler.PageProcessor;
using Panda.WebCrawler.PageProvider;
using Panda.WebCrawler.Utils;

namespace Panda.WebCrawler
{
    public class Crawler : DisposableObject
    {
        private static readonly Logger log = LogManager.GetCurrentClassLogger();

        private readonly ConcurrentQueue<string> queue = new ConcurrentQueue<string>();
        private readonly ILinkExtractor link_extractor;
        private readonly IPageProcessor page_processor;
        private readonly CrawlerOptions options;
        private bool disposed;
        private Cache cache;

        public Crawler(string url) : this(url, new CrawlerOptions(), new AllInternalLinksExtractor(url.GetHost()), new NullPageProcessor()) { }
        public Crawler(string url, CrawlerOptions options) : this(url, options, new AllInternalLinksExtractor(url.GetHost()), new NullPageProcessor()) { }
        public Crawler(string url, CrawlerOptions options, ILinkExtractor link_extractor, IPageProcessor page_processor)
        {
            this.options = options;
            this.link_extractor = link_extractor;
            this.page_processor = page_processor;
            EnsureMinThreadCount();
            CreateCache(url);
            queue.Enqueue(url);
        }
        
        private void CreateCache(string url)
        {
            var filename = url.TrimEnd(new []{'/'})
                              .GetFilename()
                              .MakeFilenameSafe();
            var path = Path.Combine(options.DataFolder, filename + ".cache");
            cache = new Cache(path);
        }

        private void EnsureMinThreadCount()
        {
            int min_worker, min_ioc;
            ThreadPool.GetMinThreads(out min_worker, out min_ioc);
            if (min_worker < options.MaxThreadCount)
                ThreadPool.SetMinThreads(options.MaxThreadCount, min_ioc);
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposed)
                return;

            try
            {
                if (disposing)
                {
                    // Free any other managed objects here.
                    cache.Dispose(); // This also saves the cache
                }
                // Free any unmanaged objects here. 
            }
            finally
            {
                disposed = true;
                base.Dispose(disposing);
            }
        }

        public Task Crawl()
        {
            var visited = new ConcurrentBag<string>();
            var cts = new CancellationTokenSource();
            var tasks = new List<Task>();
            var execution_count = 0;

            log.Debug("Crawling started");
            var sw = Stopwatch.StartNew();

            var overall_progress = options.GetOverallProgress();
            for (var i = 0; i < options.MaxThreadCount; i++)
            {
                var consumer_id = "Consumer " + i;
                var progress = options.GetThreadProgress(i);
                var consumer_task = Task.Factory.StartNew(() =>
                {
                    log.Debug("Starting consumer " + consumer_id);

                    using (var page_provide = new CachedPageProvider(cache, options.UserAgent, options.RequestTimeout))
                    {
                        while (true)
                        {
                            if (cts.Token.IsCancellationRequested)
                                break;

                            if (!queue.Any())
                            {
                                Thread.Sleep(options.ThreadDelay);
                                continue;
                            }

                            Interlocked.Increment(ref execution_count);

                            string url;
                            if (queue.TryDequeue(out url) && !visited.Contains(url))
                            {
                                visited.Add(url);
                                var page = page_provide.Get(url);

                                var links = link_extractor.Get(page);
                                links.Except(queue)
                                     .Except(visited)
                                     .Apply(queue.Enqueue);

                                page_processor.Process(page);

                                progress.Report(string.Format("Processed {0} in {1} ms", url, page.DownloadTime));
                                overall_progress.Report(string.Format("Queue {0}, Visited {1}", queue.Count, visited.Count));
                                log.Trace("{0} processed {1} in {2} ms [queue {3}, visited {4}]", consumer_id, url, page.DownloadTime, queue.Count, visited.Count);
                            }

                            Interlocked.Decrement(ref execution_count);
                        }

                        log.Debug("Stopping consumer {0} [{1}]", consumer_id, page_provide.Status());
                    }
                }, cts.Token);
                tasks.Add(consumer_task);
            }

            var download_completion_task = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    if (!queue.Any() && execution_count == 0)
                        cts.Cancel();
                    else
                        Thread.Sleep(options.ThreadDelay);

                    if (cts.Token.IsCancellationRequested)
                        break;
                }

                var elapsed = sw.StopAndGetElapsedMilliseconds();
                log.Debug("Crawl done [elapsed time {0} ms]", elapsed);
            }, cts.Token);
            tasks.Add(download_completion_task);

            return Task.WhenAll(tasks.ToArray());
        }
    }
}
