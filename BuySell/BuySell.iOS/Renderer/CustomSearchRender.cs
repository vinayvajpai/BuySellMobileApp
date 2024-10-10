using System;
using BuySell.CustomControl;
using BuySell.iOS.Renderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomSearch), typeof(CustomSearchRender))]
namespace BuySell.iOS.Renderer
{
    public class CustomSearchRender : SearchBarRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);

            var searchbar = (UISearchBar)Control;

            if (e.NewElement != null)
            {
                Foundation.NSString _searchField = new Foundation.NSString("searchField");
                var textFieldInsideSearchBar = (UITextField)searchbar.ValueForKey(_searchField);
                textFieldInsideSearchBar.BackgroundColor = UIColor.White;
                textFieldInsideSearchBar.TextColor = UIColor.Black;
                searchbar.Layer.CornerRadius = 0;
                searchbar.Layer.BorderWidth = 0;
                searchbar.Layer.BorderColor = UIColor.LightGray.CGColor;
                searchbar.ShowsCancelButton = true;
            }
        }
    }
}

