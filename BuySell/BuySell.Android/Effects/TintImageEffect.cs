using Android.Graphics;
using Android.Widget;
using BuySell.Droid.Effects;
using BuySell.Effects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ResolutionGroupName("BuySell")]
[assembly: ExportEffect(typeof(TintImageEffect), nameof(TintImage))]
namespace BuySell.Droid.Effects
{
    public class TintImageEffect : PlatformEffect
    {
        public TintImageEffect()
        {

        }
        protected override void OnAttached()
        {
            try
            {
                var effect = (TintImage)Element.Effects.FirstOrDefault(e => e is TintImage);

                if (effect == null || !(Control is ImageView image))
                    return;

                var filter = new PorterDuffColorFilter(effect.TintColor.ToAndroid(), PorterDuff.Mode.SrcIn);
                image.SetColorFilter(filter);
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        protected override void OnDetached() { }
    }
}