using System;
using System.Diagnostics;
using NLog;
using Panda.Utilities.Extensions;
using Panda.WebCrawler.Utils;

namespace Panda.WebCrawler.PageProvider
{
    public class WebPageProvider : DisposableObject, IPageProvider
    {
        private static readonly Logger log = LogManager.GetCurrentClassLogger();

        private readonly CrawlerWebClient web_client;
        private bool disposed;
        private int pages_downloaded;

        public WebPageProvider(string user_agent, int timeout)
        {
            web_client = new CrawlerWebClient(user_agent, timeout);
        }

        public Page Get(string url)
        {
            pages_downloaded++;
            var sw = Stopwatch.StartNew();
            var html = string.Empty;
            try
            {
                html = web_client.DownloadString(url);
            }
            catch (Exception e)
            {
                log.Error("Url {0}, exception {1}", url, e.Message);
            }
            var time = sw.StopAndGetElapsedMilliseconds();
            return new Page(url, html, time);
        }

        public string Status()
        {
            return string.Format("Pages downloaded: {0}", pages_downloaded);
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
                    web_client.Dispose();
                }

                // Free any unmanaged objects here.
            }
            finally
            {
                disposed = true;
                base.Dispose(disposing);
            }
        }
    }
}
