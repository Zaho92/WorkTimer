using System;
using System.Linq;
using System.Windows.Markup;
using System.Windows.Media.Imaging;

namespace WorkTimer.Helpers
{
    public class IconExtension : MarkupExtension
    {
        private string _source;

        public string Source
        {
            get { return _source; }
            set { _source = "pack://application:,,," + value; }
        }

        public int Size { get; set; }

        public IconExtension(string source, int size)
        {
            Source = source;
            Size = size;
        }

        public IconExtension()
        {
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var decoder = BitmapDecoder.Create(new Uri(Source),
                BitmapCreateOptions.DelayCreation,
                BitmapCacheOption.OnDemand);

            var result = decoder.Frames.SingleOrDefault(f => f.Width == Size);
            if (result == default(BitmapFrame))
            {
                result = decoder.Frames.OrderBy(f => f.Width).First();
            }

            return result;
        }
    }
}