using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace Neudesic.Schoolistics.WindowsStore.Converters
{
     public class BooleanToImageConverter :IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (!(value is bool))
            {
                return null;
            }
            bool data = (bool)value;
            Image bitmapImage = new Image();
            if (data == true)
            {

                bitmapImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/compare-tick.png", UriKind.Absolute));
                return bitmapImage.Source;
            }
            else
            {
                bitmapImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/compare-no.png",UriKind.Absolute));
                return bitmapImage.Source;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
            //string s = "hsdghvsbdxvnsbqvdxnbsvbdx";
            //s.Substring(0, 4);
            //s.Replace('\\','\');
        }
    }
}
