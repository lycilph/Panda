using System.Windows;

namespace Panda.UI.Extensions
{
    public static class RectExtensions
    {
        public static Rect Interpolate(Rect r1, Rect r2, double value)
        {
            var x = ((r2.Left - r1.Left) * value) + r1.Left;
            var y = ((r2.Top - r1.Top) * value) + r1.Top;
            var width = (r2.Width - r1.Width) * value + r1.Width;
            var height = (r2.Height - r1.Height) * value + r1.Height;
            return new Rect(x, y, width, height);
        }
    }
}
