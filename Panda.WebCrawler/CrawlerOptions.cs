using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Panda.WebCrawler
{
    public class CrawlerOptions
    {
        public string DataFolder { get; set; }
        public int MaxThreadCount { get; set; }
        public int ThreadDelay { get; set; }
        public string UserAgent { get; set; }
        public int RequestTimeout { get; set; }

        public List<IProgress<string>> ThreadProgress { get; set; }
        public IProgress<string> OverallProgress { get; set; }

        public CrawlerOptions()
        {
            DataFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            MaxThreadCount = 4;
            ThreadDelay = 100;
            UserAgent = "WebCrawlerBot";
            RequestTimeout = 10000;
        }

        public IProgress<string> GetThreadProgress(int index)
        {
            return ThreadProgress.Any() ? ThreadProgress[index] : new Progress<string>();
        }

        public IProgress<string> GetOverallProgress()
        {
            return OverallProgress ?? new Progress<string>();
        }
    }
}
