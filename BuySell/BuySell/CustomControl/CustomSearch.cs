using System;

using Xamarin.Forms;

namespace BuySell.CustomControl
{
    public class CustomSearch : SearchBar
    {
        public static new readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CustomSearch), typeof(CornerRadius), typeof(CustomSearch));
        public CustomSearch()
        {
        }
        public new CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }
    }
}

