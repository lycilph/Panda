using System;
using System.Collections.Generic;
using System.Linq;

namespace Panda.WebCrawler
{
    public class CrawlerProgress
    {
        public IProgress<string> Progress { get; set; }
        public List<IProgress<string>> TaskProgress { get; set; }

        public IProgress<string> this[int i]
        {
            get
            {
                if (!TaskProgress.Any() || i >= TaskProgress.Count)
                    throw new ArgumentException();
                return TaskProgress[i];
            }
        }

        public CrawlerProgress()
        {
            Progress = new Progress<string>();
            TaskProgress = new List<IProgress<string>>();
        }

        public void Report(string str)
        {
            Progress.Report(str);
        }
    }
}
