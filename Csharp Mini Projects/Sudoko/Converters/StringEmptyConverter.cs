using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Sudoko.Converters
{
    public class StringEmptyConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value.Equals(0))
            {
                return "";
            }
            else
            {
                return value.ToString();
            }
        }

        public object ConvertBack(
              object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value.Equals(""))
            {
                return "0";
            }
            else
            {
                return value;
            }
        }

    }
}
