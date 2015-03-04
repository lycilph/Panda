using System.Windows.Media.Imaging;

namespace Panda.ApplicationCore.Converters
{
    public class BooleanToImageConverter : BooleanConverter<BitmapImage>
    {
        public BooleanToImageConverter() : base(null, null) { }
    }
}