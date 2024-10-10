﻿using System;
using Android.Graphics;
using Android.Widget;
using BuySell.CustomControl;
using BuySell.Droid.CustomRenderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomSwitch), typeof(MySwitchRendererd))]
namespace BuySell.Droid.CustomRenderer
{
    [Obsolete]
    public class MySwitchRendererd : SwitchRenderer
    {
        private CustomSwitch view;
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Switch> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null || e.NewElement == null)
                return;
            view = (CustomSwitch)Element;
            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.JellyBean)
            {
                if (this.Control != null)
                {
                    if (this.Control.Checked)
                    {
                        this.Control.TrackDrawable.SetColorFilter(view.SwitchOnColor.ToAndroid(), PorterDuff.Mode.Darken);
                    }
                    else
                    {
                        this.Control.TrackDrawable.SetColorFilter(view.SwitchOffColor.ToAndroid(), PorterDuff.Mode.Lighten);
                    }
                    this.Control.CheckedChange += this.OnCheckedChange;
                    UpdateSwitchThumbImage(view);
                }
                //Control.TrackDrawable.SetColorFilter(view.SwitchBGColor.ToAndroid(), PorterDuff.Mode.Multiply);  
            }
        }

        private void UpdateSwitchThumbImage(CustomSwitch view)
        {
            if (!string.IsNullOrEmpty(view.SwitchThumbImage))
            {
                view.SwitchThumbImage = view.SwitchThumbImage.Replace(".jpg", "").Replace(".png", "");
                int imgid = (int)typeof(Resource.Drawable).GetField(view.SwitchThumbImage).GetValue(null);
                //Control.SetThumbResource(Resource.Drawable.settings);
            }
            else
            {
                Control.ThumbDrawable.SetColorFilter(view.SwitchThumbColor.ToAndroid(), PorterDuff.Mode.Multiply);
                // Control.SetTrackResource(Resource.Drawable.track);  
            }
        }

        private void OnCheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (this.Control.Checked)
            {
                this.Control.TrackDrawable.SetColorFilter(view.SwitchOnColor.ToAndroid(), PorterDuff.Mode.Darken);
            }
            else
            {
                this.Control.TrackDrawable.SetColorFilter(view.SwitchOffColor.ToAndroid(), PorterDuff.Mode.Lighten);
            }
        }
        protected override void Dispose(bool disposing)
        {
            this.Control.CheckedChange -= this.OnCheckedChange;
            base.Dispose(disposing);
        }
    }

}

