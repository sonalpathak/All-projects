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
    public class SchoolDetailsImageToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            Image image = value as Image;
            BitmapImage path = image.Source as BitmapImage;
            string imagePath = path.UriSource.ToString();
            if (imagePath == "ms-appx:/Assets/1-star-rating.png")
                return 1;
            else if (imagePath == "ms-appx:/Assets/2-star-rating.png")
                return 2;
            else if (imagePath == "ms-appx:/Assets/3-star-rating.png")
                return 3;
            else if (imagePath == "ms-appx:/Assets/4-star-rating.png")
                return 4;
            else if (imagePath == "ms-appx:/Assets/5-star-rating.png")
                return 5;
            else
                return 6;

        }
    }
}
