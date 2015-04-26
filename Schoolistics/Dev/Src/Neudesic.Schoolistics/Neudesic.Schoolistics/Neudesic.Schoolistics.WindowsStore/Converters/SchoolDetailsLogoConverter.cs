using Neudesic.Schoolistics.Core.Utils;
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
    class SchoolDetailsLogoConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {


            value = Constants.blobimage + value;
            return value;
            //var uri = value as Uri;
            //if (uri == null)
            //    return null;

            //var result = uri.ToString();
            //return result;
            var val = value as string;
            //Image i = new Image();
            //i.Source = new BitmapImage(new Uri("ms-ap", UriKind.Absolute));
            //return i.Source;



            //string s=Constants.BlobBaseUrl;
            //string newstring = s + val;
            //Image i = new Image();
            //i.Source = new BitmapImage(new Uri(newstring, UriKind.Absolute));
            //return i;
            

        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}
