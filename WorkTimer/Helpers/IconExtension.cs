using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media.Imaging;

namespace WorkTimer.Helpers
{
    /// <summary>
    /// Simple extension for icon, to let you choose icon with specific size.
    /// Usage sample:
    /// Image Stretch="None" Source="{common:Icon /Controls;component/icons/custom.ico, 16}"
    /// Or:
    /// Image Source="{common:Icon Source={Binding IconResource}, Size=16}"
    /// </summary> 
    public class IconExtension : MarkupExtension
    {
        private string _source;

        public string Source
        {
            get
            {
                return _source;
            }
            set
            {
                // Have to make full pack URI from short form, so System.Uri recognizes it.
                _source = "pack://application:,,," + value;
            }
        }

        public int Size { get; set; }

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

        public IconExtension(string source, int size)
        {
            Source = source;
            Size = size;
        }

        public IconExtension() { }
    }
}
