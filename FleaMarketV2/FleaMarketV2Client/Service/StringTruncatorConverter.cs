using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace FleaMarketV2Client.Service
{
    public class StringTruncatorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str && parameter is string lengthString && int.TryParse(lengthString, out int maxLength))
            {
                if (str.Length > maxLength)
                {
                    return str.Substring(0, maxLength) + "...";
                }
                return str;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

