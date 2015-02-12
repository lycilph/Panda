using System.Collections.Generic;

namespace Panda.WebCrawler.LinkExtractor
{
    public interface ILinkExtractor
    {
        List<string> Get(Page page);
    }
}