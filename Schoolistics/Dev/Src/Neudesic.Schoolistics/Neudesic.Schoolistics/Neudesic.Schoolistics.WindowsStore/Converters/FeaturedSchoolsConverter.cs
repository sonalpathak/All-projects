using Cirrious.CrossCore.WindowsStore.Converters;
//using Neudesic.Schoolistics.Core.Converters;
using Neudesic.Schoolistics.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Neudesic.Schoolistics.WindowsStore.Converters
{


    public class FeaturedSchoolsConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            value = Constants.blobimage + value;
            return value;

        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
        
    }
}
