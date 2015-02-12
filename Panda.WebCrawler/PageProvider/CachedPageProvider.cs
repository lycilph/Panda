using System;
using Panda.WebCrawler.Utils;

namespace Panda.WebCrawler.PageProvider
{
    public class CachedPageProvider : DisposableObject, IPageProvider
    {
        private const int cache_lifetime = 7; // Lifetime in days

        private readonly bool dispose_page_provider;
        private readonly IPageProvider page_provider;
        private readonly Cache cache;
        private bool disposed;
        private int hits;
        private int misses;
        
        public CachedPageProvider(Cache cache, string user_agent, int timeout) : this(cache, new WebPageProvider(user_agent, timeout))
        {
            dispose_page_provider = true;
        }

        public CachedPageProvider(Cache cache, IPageProvider page_provider)
        {
            this.page_provider = page_provider;
            this.cache = cache;
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
                    if (dispose_page_provider)
                        page_provider.Dispose();
                }
                // Free any unmanaged objects here. 
            }
            finally
            {
                disposed = true;
                base.Dispose(disposing);
            }
        }

        public Page Get(string url)
        {
            // Check if the url is in the cache
            Page page;
            if (cache.TryGetValue(url, out page) && page.Timestamp.AddDays(cache_lifetime) > DateTime.Now)
            {
                hits++;
                return page;
            }

            // Otherwise pass on to the internal page provider and add to cache
            misses++;
            page = page_provider.Get(url);
            cache.TryAdd(url, page);
            return page;
        }

        public string Status()
        {
            return string.Format("Cache: hits {0}, misses {1}, {2}", hits, misses, page_provider.Status());
        }
    }
}
