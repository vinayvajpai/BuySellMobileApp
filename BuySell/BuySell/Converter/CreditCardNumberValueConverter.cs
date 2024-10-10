using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace BuySell.Converter
{
    public class CreditCardNumberValueConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!string.IsNullOrEmpty(System.Convert.ToString(value)))
            {
                var cardPan = System.Convert.ToString(value);
                var maskedPan = cardPan.Aggregate(string.Empty, (value1, next) =>
                {
                    if (value1.Length >= 0 && value1.Length < cardPan.Length - 4)
                    {
                        next = '*';
                    }
                    return value1 + next;
                });
                return maskedPan;
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Regex.Replace(value.ToString(), @"\D", "");
        }
    }

    public class ImageSourceConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(ImageSource);
        }
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if(reader.Value!=null && !string.IsNullOrEmpty(reader.Value.ToString()))
                return ImageSource.FromUri(new Uri(reader.Value.ToString()));

            return ImageSource.FromFile("Placeholder.png");
            //TODO: convert reader.Value to ImageSource and return
           // return null;
        }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
           
            //TODO: write image Source to writer
        }
    }
}

