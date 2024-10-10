using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace BuySell.Converter
{
    public class PhoneNumberValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!string.IsNullOrEmpty(System.Convert.ToString(value)))
            {
                var phoneNumber = System.Convert.ToString(value);
                var maskedphoneNumber = phoneNumber.Aggregate(string.Empty, (value1, next) =>
                {
                    if(value1.Length == 0 || value1.Length == 4 || value1.Length == 5 || value1.Length == 9)
                    {
                       return value1 + next;
                    }   
                    else if (value1.Length > 0 && value1.Length < phoneNumber.Length - 4)
                    {
                        next = '*';
                    }
                    return value1 + next;
                });
                return maskedphoneNumber;
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Regex.Replace(value.ToString(), @"\D", "");
        }
    }
}