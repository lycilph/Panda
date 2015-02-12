using System;

namespace Panda.WebCrawler.PageProvider
{
    public interface IPageProvider : IDisposable
    {
        Page Get(string url);
        string Status();
    }
}