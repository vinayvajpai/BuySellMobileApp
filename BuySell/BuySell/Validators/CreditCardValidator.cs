using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace BuySell.Validators
{
    public class CreditCardValidator : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
                    Regex regex = new Regex(@"^\d{4}\s?\d{4}\s?\d{4}\s?\d{1,4}$");
                    bool isValid = regex.IsMatch(System.Convert.ToString(value));
                    if (isValid)
                    {
                    return true;
                    }
                    else
                    {
                    return false;
                    }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
                Regex regex = new Regex(@"^\d{4}\s?\d{4}\s?\d{4}\s?\d{1,4}$");
                bool isValid = regex.IsMatch(System.Convert.ToString(value));
                if (isValid)
                {
                    return true;
                }
                else
                {
                    return false;
                }
        }
    }
}
