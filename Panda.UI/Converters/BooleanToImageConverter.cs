using System.Windows.Media.Imaging;

namespace Panda.UI.Converters
{
    public class BooleanToImageConverter : BooleanConverter<BitmapImage>
    {
        public BooleanToImageConverter() : base(null, null) { }
    }
}