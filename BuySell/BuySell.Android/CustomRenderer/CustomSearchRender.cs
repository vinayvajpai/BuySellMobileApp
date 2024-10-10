using System;
using Android.Content;
using BuySell.CustomControl;
using BuySell.Droid.CustomRenderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomSearch), typeof(CustomSearchRender))]
namespace BuySell.Droid.CustomRenderer
{
   public class CustomSearchRender : SearchBarRenderer
        {
            public CustomSearchRender(Context context) : base(context)
            {

            }

            protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> args)
            {
                base.OnElementChanged(args);
                var plateId = Resources.GetIdentifier("android:id/search_plate", null, null);
                var plate = Control.FindViewById(plateId);
                plate.SetBackgroundColor(Android.Graphics.Color.White);
            }
    }
}

