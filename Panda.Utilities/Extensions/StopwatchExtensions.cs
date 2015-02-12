using System.Diagnostics;

namespace Panda.Utilities.Extensions
{
    public static class StopwatchExtensions
    {
        public static long StopAndGetElapsedMilliseconds(this Stopwatch sw)
        {
            sw.Stop();
            return sw.ElapsedMilliseconds;
        }
    }
}
