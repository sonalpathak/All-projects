using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace Neudesic.Schoolistics.WindowsStore.Converters
{
    public class FindSchoolsImageToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
           // return value;
            int number = System.Convert.ToInt32(value);
            BitmapImage bitmapImage = new BitmapImage();
            switch (number)
            {
                case 1:
                    //bitmapImage= new BitmapImage(new Uri("ms-appx:///Assets/1-star-rating.png", UriKind.Absolute));
                    //return bitmapImage;
                 return "../Assets/1-star-rating.png";
                case 2:
                    return "../Assets/2-star-rating.png";
                case 3:
                    return "../Assets/3-star-rating.png";
                case 4:
                    return "../Assets/4-star-rating.png";
                case 5:
                    return "../Assets/5-star-rating.png";

            }
            return "Any Rating";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            string image = value as string;
           // BitmapImage path = image.Source as BitmapImage;
           // string imagePath = path.UriSource.ToString();
            if (image == "../Assets/1-star-rating.png")
                return 1;
            else if (image == "../Assets/2-star-rating.png")
                return 2;
            else if (image == "../Assets/3-star-rating.png")
                return 3;
            else if (image == "../Assets/4-star-rating.png")
                return 4;
            else if (image == "../Assets/5-star-rating.png")
                return 5;
            else
                return 0;
        }
    }
}
