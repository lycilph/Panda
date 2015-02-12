using System;
using System.IO;

namespace Panda.WebCrawler.Extensions
{
    public static class UrlExtensions
    {
        public static string GetExtension(this string url)
        {
            var extension = Path.GetExtension(url);
            return (extension != null ? extension.ToLowerInvariant() : string.Empty);
        }

        public static string GetHost(this string url)
        {
            return new Uri(url).Host;
        }

        public static string GetFilename(this string url)
        {
            return Path.GetFileName(url);
        }

        public static Uri Normalize(this string url, Uri page_uri)
        {
            return new Uri(page_uri, url);
        }
    }
}
