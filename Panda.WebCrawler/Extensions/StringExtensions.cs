using System;
using System.Globalization;
using System.IO;

namespace Panda.WebCrawler.Extensions
{
    public static class StringExtensions
    {
        public static string MakeFilenameSafe(this string str)
        {
            Array.ForEach(Path.GetInvalidFileNameChars(), c => str = str.Replace(c.ToString(CultureInfo.InvariantCulture), String.Empty));
            return str.Replace(".", string.Empty);
        }
    }
}
