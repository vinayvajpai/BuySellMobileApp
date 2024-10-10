using System;
using Xamarin.Forms;

namespace BuySell.Effects
{
    public class TintImage : RoutingEffect
    {
        public Color TintColor { get; set; }

        public TintImage(Color color) : base($"BuySell.{nameof(TintImage)}")
        {
            TintColor = color;
        }
    }
}
