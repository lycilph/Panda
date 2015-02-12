using System;
using System.Net;

namespace Panda.WebCrawler.Utils
{
    public class CrawlerWebClient : WebClient
    {
        private readonly string user_agent;
        private readonly int timeout; // value in milliseconds

        public CrawlerWebClient(string user_agent, int timeout)
        {
            this.user_agent = user_agent;
            this.timeout = timeout;
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            var request = base.GetWebRequest(address);

            var http_request = request as HttpWebRequest;
            if (http_request != null)
            {
                http_request.UserAgent = user_agent;
                http_request.Timeout = timeout;
            }

            return request;
        }
    }
}
