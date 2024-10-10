using System;
using BuySell.Droid.Effects;
using Xamarin.Forms;
using BuySell.Effects;
using Android.Views;
using Com.Tomergoldst.Tooltips;
using static Com.Tomergoldst.Tooltips.ToolTipsManager;
using System.ComponentModel;
using Xamarin.Forms.Platform.Android;
using Rg.Plugins.Popup.Services;
using Android.OS;
using Android.Renderscripts;

[assembly: ExportEffect(typeof(DroidTooltipEffect), nameof(TooltipEffect))]
namespace BuySell.Droid.Effects
{
    public class DroidTooltipEffect : PlatformEffect
    {
        ToolTip toolTipView;
        ToolTipsManager _toolTipsManager;
        ITipListener listener;

        public DroidTooltipEffect()
        {
            try
            {
                listener = new TipListener();
                _toolTipsManager = new ToolTipsManager(listener);

                Device.StartTimer(TimeSpan.FromSeconds(1), () =>
                {
                    if (Element != null)
                    {
                        var isshow = TooltipEffect.GetIsShowTooltip(Element);
                        if (isshow)
                            OnTap(null, null);
                    }
                    return false;
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

        }

        void OnTap(object sender, EventArgs e)
        {
            try
            {
                var control = Control ?? Container;

                var text = TooltipEffect.GetText(Element);

                if (!string.IsNullOrEmpty(text))
                {
                    ToolTip.Builder builder;
                    var parentContent = control.RootView;

                    var position = TooltipEffect.GetPosition(Element);
                    switch (position)
                    {
                        case TooltipPosition.Top:
                            builder = new ToolTip.Builder(control.Context, control, parentContent as ViewGroup, text.PadRight(80, ' '), ToolTip.PositionAbove);
                            break;
                        case TooltipPosition.Left:
                            builder = new ToolTip.Builder(control.Context, control, parentContent as ViewGroup, text.PadRight(80, ' '), ToolTip.PositionLeftTo);
                            break;
                        case TooltipPosition.Right:
                            builder = new ToolTip.Builder(control.Context, control, parentContent as ViewGroup, text.PadRight(80, ' '), ToolTip.PositionRightTo);
                            break;
                        default:
                            builder = new ToolTip.Builder(control.Context, control, parentContent as ViewGroup, text.PadRight(90, ' '), ToolTip.PositionBelow);
                            break;
                    }

                    builder.SetAlign(ToolTip.AlignRight);
                    builder.SetBackgroundColor(TooltipEffect.GetBackgroundColor(Element).ToAndroid());
                    builder.SetTextColor(TooltipEffect.GetTextColor(Element).ToAndroid());

                    toolTipView = builder.Build();

                    _toolTipsManager?.Show(toolTipView);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

        }


        protected override void OnAttached()
        {
            var control = Control ?? Container;
            //control.Click += OnTap;
        }


        protected override void OnDetached()
        {
            var control = Control ?? Container;
            control.Click -= OnTap;
            _toolTipsManager.FindAndDismiss(control);
        }

        class TipListener : Java.Lang.Object, ITipListener
        {
            public void OnTipDismissed(Android.Views.View p0, int p1, bool p2)
            {
                try
                {

                    if (PopupNavigation.Instance.PopupStack.Count > 0)
                        PopupNavigation.Instance.PopAsync();
                }
                catch (Exception ex)
                {

                }

            }
        }
    }
}

