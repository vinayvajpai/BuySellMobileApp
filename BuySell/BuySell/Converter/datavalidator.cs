using AutoMapper;
using System;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Forms;

namespace BuySell.Converter
{
    public class datavalidator : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((int)value > 0) // length > 0 ?
                return true;            // some data has been entered

            else
                return false;            // input is empty
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return 0;
        }
    }
}
