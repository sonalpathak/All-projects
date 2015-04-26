using Neudesic.Schoolistics.Core.Entities;
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
    public class PopularComparisonsImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (!(value is List<School>))
            {
                return null;
            }

            List<School> data = (List<School>)value;
            Image bitmapImage = new Image();
            if (data != null)
            {
                for (int i = 0; i < data.Count; i++ )
                {
                    if (data[1].SchoolId != null)
                    {
                        bitmapImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/vs.png", UriKind.Absolute));
                        return bitmapImage.Source;
                    }
                    if (data[2].SchoolId != null)
                    {
                        bitmapImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/vs.png", UriKind.Absolute));
                        return bitmapImage.Source;
                    }
                    else
                    {
                        bitmapImage.Source = new BitmapImage(new Uri("", UriKind.Absolute));
                        return bitmapImage.Source;
                    }
                }
                bitmapImage.Source = new BitmapImage(new Uri("", UriKind.Absolute));
                return bitmapImage.Source;
                   
            }
            else
            {
                bitmapImage.Source = new BitmapImage(new Uri("", UriKind.Absolute));
                return bitmapImage.Source;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
