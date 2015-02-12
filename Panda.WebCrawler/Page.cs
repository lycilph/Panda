using System;

namespace Panda.WebCrawler
{
    public class Page : IEquatable<Page>
    {
        public Uri Uri { get; set; }
        public string Html { get; set; }
        public long DownloadTime { get; set; } // Download time in ms
        public DateTime Timestamp { get; set; }

        public Page() { }
        public Page(string url) : this(url, string.Empty, 0) { }
        public Page(string url, string html, long download_time)
        {
            Uri = new Uri(url);
            Html = html;
            DownloadTime = download_time;

            Timestamp = DateTime.Now;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Page) obj);
        }
        public bool Equals(Page other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Uri, other.Uri) && string.Equals(Html, other.Html);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Uri.GetHashCode() * 397) ^ Html.GetHashCode();
            }
        }

        public static bool operator ==(Page left, Page right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Page left, Page right)
        {
            return !Equals(left, right);
        }
    }
}
