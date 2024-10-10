using System;
using Android.Widget;
using BuySell.CustomControl;
using BuySell.Droid.CustomRenderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomBorderlessEditor), typeof(CustomBorderlessEditorRenderer))]
namespace BuySell.Droid.CustomRenderer
{
    public class CustomBorderlessEditorRenderer : EditorRenderer
    {
        public static void Init() { }
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                Control.Background = null;
                var layoutParams = new MarginLayoutParams(Control.LayoutParameters);
                layoutParams.SetMargins(0, 0, 0, 0);
                LayoutParameters = layoutParams;
                Control.LayoutParameters = layoutParams;
                Control.SetPadding(0, 0, 0, 0);
                SetPadding(0,0, 0, 0);
                Control.InputType = Android.Text.InputTypes.TextFlagCapSentences;
            }
        }
    }
}
