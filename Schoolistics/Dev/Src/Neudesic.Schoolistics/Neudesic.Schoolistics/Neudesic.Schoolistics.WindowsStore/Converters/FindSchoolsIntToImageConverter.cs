using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Neudesic.Schoolistics.WindowsStore.Converters
{
    public class FindSchoolsIntToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int number = System.Convert.ToInt32(value);
            switch (number)
            {
                case 1:
                    return "../Assets/1-star.png";
                case 2:
                    return "../Assets/2-star.png";
                case 3:
                    return "../Assets/3-star.png";
                case 4:
                    return "../Assets/4-star.png";
                case 5:
                    return "../Assets/5-star.png";

            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value is Visibility && (Visibility)value == Visibility.Visible;
        }
    }
}
