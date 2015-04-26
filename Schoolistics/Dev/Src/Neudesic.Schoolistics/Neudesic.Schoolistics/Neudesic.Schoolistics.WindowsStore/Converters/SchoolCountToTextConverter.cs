using Neudesic.Schoolistics.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Neudesic.Schoolistics.WindowsStore.Converters
{
    public class SchoolCountToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {

            if (value != null)
            {
                List<SchoolsComparison> data = (List<SchoolsComparison>)value;
                var schoolCount = data.Count;
                if (schoolCount > 3)
                {
                    return (schoolCount - 2).ToString()+ " "  + "MORE SCHOOLS";
                }
            }
            return "";

        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
