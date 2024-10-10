using System;
using System.Diagnostics;
using System.Linq;
using BuySell.Effects;
using BuySell.iOS.Effects;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("BuySell")]
[assembly: ExportEffect(typeof(TintImageEffect), nameof(TintImage))]
namespace BuySell.iOS.Effects
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
                if (effect == null || !(Control is UIImageView image))
                    return;

                image.Image = image.Image.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
                image.TintColor = effect.TintColor.ToUIColor();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        protected override void OnDetached()
        {}
    }
}
