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
    public class IntToImageConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if(!(value is double))
            {
                return null;
            }
            double val = (double)value;
            Image i = new Image();

            //switch ((int)val)
            //{
            //    case 1:
            //        i.Source = new BitmapImage(new Uri("ms-appx:///Assets/1-star-rating.png", UriKind.Absolute));
            //        return i.Source;
            //    case 2:
            //        i.Source = new BitmapImage(new Uri("ms-appx:///Assets/2-star-rating.png", UriKind.Absolute));
            //        return i.Source;
            //    case 3:
            //    i.Source = new BitmapImage(new Uri("ms-appx:///Assets/3-star-rating.png", UriKind.Absolute));
            //    return i.Source;
            //    case 4:
            //        i.Source = new BitmapImage(new Uri("ms-appx:///Assets/4-star-rating.png", UriKind.Absolute));
            //        return i.Source;
            //    case 5:
            //        i.Source = new BitmapImage(new Uri("ms-appx:///Assets/5-star-rating.png", UriKind.Absolute));
            //        return i.Source;
            //}
            if (val == 5)
            {
                i.Source = new BitmapImage(new Uri("ms-appx:///Assets/5-star-rating.png", UriKind.Absolute));
                return i.Source;

            }
            if (val > 4 && val < 5)
            {
                i.Source = new BitmapImage(new Uri("ms-appx:///Assets/4-5.png", UriKind.Absolute));
                return i.Source;
            }
            if (val == 4)
            {
                i.Source = new BitmapImage(new Uri("ms-appx:///Assets/4-star-rating.png", UriKind.Absolute));
                return i.Source;

            }
            if (val > 3 && val < 4)
            {
                i.Source = new BitmapImage(new Uri("ms-appx:///Assets/3-5.png", UriKind.Absolute));
                return i.Source;
            }

            if (val == 3)
            {
                i.Source = new BitmapImage(new Uri("ms-appx:///Assets/3-star-rating.png", UriKind.Absolute));
                return i.Source;

            }
            if (val > 2 && val < 3)
            {
                i.Source = new BitmapImage(new Uri("ms-appx:///Assets/2-5.png", UriKind.Absolute));
                return i.Source;
            }
            if (val == 2)
            {
                i.Source = new BitmapImage(new Uri("ms-appx:///Assets/2-star-rating.png", UriKind.Absolute));
                return i.Source;

            }
            if (val > 1 && val < 2)
            {
                i.Source = new BitmapImage(new Uri("ms-appx:///Assets/1-5.png", UriKind.Absolute));
                return i.Source;
            }
            if (val == 1)
            {
                i.Source = new BitmapImage(new Uri("ms-appx:///Assets/1-star-rating.png", UriKind.Absolute));
                return i.Source;

            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }

        
    }
}
