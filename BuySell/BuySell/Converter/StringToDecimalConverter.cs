using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace BuySell.Converter
{
    public class StringToDecimalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!string.IsNullOrWhiteSpace(System.Convert.ToString(value)))
            {
                return System.Convert.ToDecimal(System.Convert.ToString(value).Replace("$", ""));
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (!string.IsNullOrWhiteSpace(System.Convert.ToString(value)))
            {
                return System.Convert.ToDecimal(System.Convert.ToString(value).Replace("$", ""));
            }
            else
            {
                return null;
            }
        }
    }
}
