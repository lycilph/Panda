using System.Collections.Concurrent;
using System.IO;
using System.Threading;
using NLog;
using Panda.Utilities.Extensions;
using Panda.WebCrawler.Utils;

namespace Panda.WebCrawler
{
    public class Cache : DisposableObject
    {
        private static readonly Logger log = LogManager.GetCurrentClassLogger();

        private readonly string filename;
        private ConcurrentDictionary<string, Page> data = new ConcurrentDictionary<string, Page>();
        private bool disposed;
        private bool dirty;

        public Cache(string filename)
        {
            this.filename = filename;
            Load();
        }

        private void Load()
        {
            if (!File.Exists(filename))
                return;

            log.Debug("Loading cache [{0}]", filename);
            dirty = false;
            data = JsonExtensions.ReadFromFileAndUnzip<ConcurrentDictionary<string, Page>>(filename);
        }

        private void Save()
        {
            if (!dirty)
                return;

            log.Debug("Saving cache [{0}]", filename);
            JsonExtensions.ZipAndWriteToFile(filename, data);
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
                    Save();
                    data.Clear();
                    data = null;
                }

                // Free any unmanaged objects here. 
            }
            finally
            {
                disposed = true;
                base.Dispose(disposing);
            }
        }

        public bool TryGetValue(string url, out Page page)
        {
            if (data.TryGetValue(url, out page))
            {
                Thread.Sleep((int)page.DownloadTime);
                return true;
            }

            return false;
        }

        public bool TryAdd(string url, Page page)
        {
            dirty = true;
            return data.TryAdd(url, page);
        }
    }
}
