using System.Collections.Generic;
using System.Linq;
using Panda.WebCrawler.Extensions;

namespace Panda.WebCrawler.LinkExtractor
{
    public class AllInternalLinksExtractor : ILinkExtractor
    {
        private readonly List<string> page_extensions = new List<string> { ".html", ".htm", ".php", "" };
        private readonly string host;

        public AllInternalLinksExtractor(string host)
        {
            this.host = host;
        }

        public List<string> Get(Page page)
        {
            var doc = HtmlExtensions.Load(page.Html);
            return doc.GetAnchorLinks() // Anchor links
                      .Concat(doc.GetFrameLinks()) // Frame links
                      .Select(link => link.Normalize(page.Uri)) // Get the full link
                      .Where(uri => uri.Host == host) // Is this internal to the main page
                      .Select(uri => uri.ToString()) // Get the full link as a string
                      .Where(link => page_extensions.Contains(link.GetExtension())) // Does the link end with one of the selected extensions
                      .Where(link => !link.Contains("#")) // Remove "bookmark" links
                      .Distinct() // Only get unique links
                      .ToList();
        }
    }
}
