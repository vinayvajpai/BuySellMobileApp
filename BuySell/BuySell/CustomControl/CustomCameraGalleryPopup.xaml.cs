using System;
using System.Collections.Generic;
using BuySell.Helper;
using BuySell.Utility;
using BuySell.ViewModel;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BuySell.CustomControl
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomCameraGalleryPopup : PopupPage
    {
        public event EventHandler eventCamera;
        public event EventHandler eventGallery;
        public CustomCameraGalleryPopup()
        {
            InitializeComponent();
            BindingContext = new CustomCamGalPopupViewModel(this.Navigation);
        }
        //Method created to select images from camera
        private void CameraCommand(object sender, EventArgs e)
        {
            if (eventCamera != null)
            {
                eventCamera.Invoke(sender, e);
            }
        }
        //Method created to select images from gallery
        private void GalleryCommand(object sender, EventArgs e)
        {
            if (eventGallery != null)
            {
                eventGallery.Invoke(sender, e);
            }
        }

       async void PopupCancelCommand(System.Object sender, System.EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }
    }
}
