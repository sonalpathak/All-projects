using Cirrious.CrossCore.Converters;
using Neudesic.Schoolistics.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neudesic.Schoolistics.Core.Converters
{
   public class FeaturedSchoolsImageConverter :MvxValueConverter<string,string>
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            value = Constants.BlobBaseUrl + value;
            return value;

        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}
