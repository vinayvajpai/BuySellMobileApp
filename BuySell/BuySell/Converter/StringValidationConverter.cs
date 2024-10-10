using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace BuySell.Converter
{
    public class StringValidationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(string.IsNullOrWhiteSpace(System.Convert.ToString(value)))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (string.IsNullOrWhiteSpace(System.Convert.ToString(value)))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
