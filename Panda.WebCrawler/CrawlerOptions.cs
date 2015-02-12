using System.IO;
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

        public CrawlerOptions()
        {
            DataFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            MaxThreadCount = 4;
            ThreadDelay = 100;
            UserAgent = "WebCrawlerBot";
            RequestTimeout = 10000;
        }
    }
}
