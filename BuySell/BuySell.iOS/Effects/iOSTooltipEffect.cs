using System;
using System.ComponentModel;
using BuySell.iOS.Effects;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using BuySell.Effects;
using Rg.Plugins.Popup.Services;

[assembly: ExportEffect(typeof(iOSTooltipEffect), nameof(BuySell.Effects.TooltipEffect))]
namespace BuySell.iOS.Effects
{
    public class iOSTooltipEffect : PlatformEffect
    {
        EasyTipView.EasyTipView tooltip;
        UITapGestureRecognizer tapGestureRecognizer;

        public iOSTooltipEffect()
        {
            tooltip = new EasyTipView.EasyTipView();
            tooltip.DidDismiss += OnDismiss;
            Device.StartTimer(TimeSpan.FromSeconds(1), () => {
                var isshow = BuySell.Effects.TooltipEffect.GetIsShowTooltip(Element);
                if (isshow)
                    OnTap(null, null);
                return false;
            });
            
        }

        void OnTap(object sender, EventArgs e)
        {
            var control = Control ?? Container;

            var text = BuySell.Effects.TooltipEffect.GetText(Element);

            if (!string.IsNullOrEmpty(text))
            {
                
                tooltip.BubbleColor = BuySell.Effects.TooltipEffect.GetBackgroundColor(Element).ToUIColor();
                tooltip.ForegroundColor = BuySell.Effects.TooltipEffect.GetTextColor(Element).ToUIColor();
                tooltip.Text = new Foundation.NSString(text);
                UpdatePosition();

                var window = UIApplication.SharedApplication.KeyWindow;
                var vc = window.RootViewController;
                while (vc.PresentedViewController != null)
                {
                    vc = vc.PresentedViewController;
                }
                //control.Frame = new CoreGraphics.CGRect(Convert.ToDouble(control.Frame.X-20), Convert.ToDouble(control.Frame.Y), control.Frame.Width, control.Frame.Height);
                tooltip?.Show(control, vc.View, true);
            }

        }

        void OnDismiss(object sender, EventArgs e)
        {
            // do something on dismiss
            try
            {
                if (PopupNavigation.Instance.PopupStack.Count > 0)
                    PopupNavigation.Instance.PopAsync();
            }
            catch (Exception ex)
            {

            }
        }


        protected override void OnAttached()
        {
            var control = Control ?? Container;

            if (control is UIButton)
            {
                var btn = Control as UIButton;
               // btn.TouchUpInside += OnTap;
            }
            else
            {
                tapGestureRecognizer = new UITapGestureRecognizer((UITapGestureRecognizer obj) =>
                {
                   // OnTap(obj, EventArgs.Empty);
                });
                control.UserInteractionEnabled = true;
                control.AddGestureRecognizer(tapGestureRecognizer);
            }
        }

        protected override void OnDetached()
        {

            var control = Control ?? Container;

            if (control is UIButton)
            {
                var btn = Control as UIButton;
                btn.TouchUpInside -= OnTap;
            }
            else
            {
                if (tapGestureRecognizer != null)

                    control.RemoveGestureRecognizer(tapGestureRecognizer);

            }
            tooltip?.Dismiss();
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(args);

            if (args.PropertyName == TooltipEffect.BackgroundColorProperty.PropertyName)
            {
                tooltip.BubbleColor = TooltipEffect.GetBackgroundColor(Element).ToUIColor();
            }
            else if (args.PropertyName == TooltipEffect.TextColorProperty.PropertyName)
            {
                tooltip.ForegroundColor = TooltipEffect.GetTextColor(Element).ToUIColor();
            }
            else if (args.PropertyName == TooltipEffect.TextProperty.PropertyName)
            {
                tooltip.Text = new Foundation.NSString(TooltipEffect.GetText(Element));
            }
            else if (args.PropertyName == TooltipEffect.PositionProperty.PropertyName)
            {
                UpdatePosition();
            }
        }

        void UpdatePosition()
        {
            var position = TooltipEffect.GetPosition(Element);
            switch (position)
            {
                case TooltipPosition.Top:
                    tooltip.ArrowPosition = EasyTipView.ArrowPosition.Bottom;
                    break;
                case TooltipPosition.Left:
                    tooltip.ArrowPosition = EasyTipView.ArrowPosition.Right;
                    break;
                case TooltipPosition.Right:
                    tooltip.ArrowPosition = EasyTipView.ArrowPosition.Left;
                    break;
                default:
                    tooltip.ArrowPosition = EasyTipView.ArrowPosition.Top;
                    break;
            }
        }
    }
}

