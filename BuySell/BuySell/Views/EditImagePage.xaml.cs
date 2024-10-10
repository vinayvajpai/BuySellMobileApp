using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using BuySell.Helper;
using BuySell.ViewModel;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BuySell.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditImagePage : ContentPage
    {
        INavigation navigation;
        EditImageViewModel m_viewmodel;
        Stream _imgStream;
        private readonly SKBitmap _originalBitmap;
        private bool _pageLoaded;
        private SKPath _pathToClip;
        private byte[] _bytearray;
        public EditImagePage(Stream imgStream)
        {
            InitializeComponent();
            _imgStream = imgStream;
            BindingContext = m_viewmodel = new EditImageViewModel(this.Navigation);
            MessagingCenter.Subscribe<object, object>(this, "Refresh", (sender, args) =>
            {
                RefreshImage();
            });
            navigation = this.Navigation;
            _originalBitmap = SKBitmap.Decode(Global.BuySellimageByte);
            CropImageCanvas.PaintSurface += OnCanvasViewPaintSurface;
            
        }

        async private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            SKImageInfo info = e.Info;
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;
            canvas.Clear();
            if (_pathToClip != null)
            {
                // Crop canvas before drawing image
                canvas.ClipPath(_pathToClip);
            }

            canvas.DrawBitmap(_originalBitmap, info.Rect);
            if (_pathToClip != null)
            {
                // Get cropped image byte array
                var snap = surface.Snapshot();
                var data = snap.Encode();
                _bytearray = data.ToArray();
                CroppedImageView.Source = ImageSource.FromStream(() => new MemoryStream(_bytearray));
                IsVisible = true;
                ManualCropView.IsVisible = false;
                MessagingCenter.Send<object, ImageSource>("IsImgAdd", "IsImgAdd", CroppedImageView.Source);
                await navigation.PopAsync(true);
            }
        }

        private void RefreshImage()
        {
            //myimage.ReloadImage();
            //myimage.LoadingPlaceholder = null;
        }

        protected override void OnAppearing()
        {
            try
            {
                ManualCropView.Initialize();
                if (Global.UserImage != null)
                {
                    //myimage.Source = Global.UserImage;
                }
                if (m_viewmodel != null)
                {
                    m_viewmodel.navigation = this.Navigation;
                    m_viewmodel.IsTap = false;
                }
                base.OnAppearing();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        void OnPinchUpdated(object sender, PinchGestureUpdatedEventArgs e)
        {
            m_viewmodel.OnPinchUpdated(e);
        }

        void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            m_viewmodel.OnPanUpdated(e);
        }

        private async void CropSquare_Tapped(object sender, EventArgs e)
        {
            try
            {
                //myimage.HeightRequest = 250;
                //myimage.WidthRequest = 250;
                //var vardata = await myimage.GetImageAsPngAsync(250, 250);

                //myimage.Source = ImageSource.FromStream(() => new MemoryStream(vardata));

                //MessagingCenter.Send<object, ImageSource>(this, "IsImgAdd", myimage.Source);
                //await navigation.PopAsync(true);

                _pathToClip = new SKPath();
                SKPoint[] arr = new SKPoint[]
                {
                new SKPoint((float) (ManualCropView.TopLeftCorner.X * DeviceDisplay.MainDisplayInfo.Density), (float) (ManualCropView.TopLeftCorner.Y * DeviceDisplay.MainDisplayInfo.Density)),
                new SKPoint((float) (ManualCropView.TopRightCorner.X * DeviceDisplay.MainDisplayInfo.Density), (float) (ManualCropView.TopRightCorner.Y * DeviceDisplay.MainDisplayInfo.Density)),
                new SKPoint((float) (ManualCropView.BottomRightCorner.X * DeviceDisplay.MainDisplayInfo.Density), (float) (ManualCropView.BottomRightCorner.Y * DeviceDisplay.MainDisplayInfo.Density)),
                new SKPoint((float) (ManualCropView.BottomLeftCorner.X * DeviceDisplay.MainDisplayInfo.Density), (float) (ManualCropView.BottomLeftCorner.Y * DeviceDisplay.MainDisplayInfo.Density))
                };
                _pathToClip.AddPoly(arr);

                CropImageCanvas.InvalidateSurface(); // redraw
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
