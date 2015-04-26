using Neudesic.Schoolistics.Core.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Neudesic.Schoolistics.WindowsStore.Converters
{
    public class ConverterToDisplayTwoPopularSchools :IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {

            if (value != null)
            {
                List<SchoolsComparison> data = (List<SchoolsComparison>)value;
                
                    if (data.Count > 3)
                    {
                        var list = data.Take(2);
                        return list;
                   
                }
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
