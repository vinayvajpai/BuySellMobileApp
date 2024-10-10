using System;
using System.Collections.Generic;
using System.Diagnostics;
using Acr.UserDialogs;
using BuySell.Helper;
using BuySell.ViewModel;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BuySell.Popup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomSizePopup : PopupPage
    {
        public event EventHandler<string> eventOK;
        public event EventHandler eventCancel;
        CustomSizePopupViewModel vm;
        public CustomSizePopup()
        {
            InitializeComponent();
            BindingContext =vm= new CustomSizePopupViewModel(this.Navigation);
            //MessagingCenter.Subscribe<object>("Updatetheme", "Updatetheme", (obj) =>
            //{
            //    vm.ThemeColor = Global.GetThemeColor(Global.setThemeColor);
            //});
            //MessagingCenter.Subscribe<object>("UpdateHeadertheme", "UpdateHeadertheme", (obj) =>
            //{
            //    vm.ThemeColor = Global.GetThemeColor(Global.setThemeColor);
            //});
            MessagingCenter.Subscribe<object>(Constant.UpdateThemeStr, Constant.UpdateThemeStr, (obj) =>
            {
                vm.ThemeColor = Global.GetThemeColor(Global.setThemeColor);
            });
            MessagingCenter.Subscribe<object>(Constant.UpdateHeaderThemeStr, Constant.UpdateHeaderThemeStr, (obj) =>
            {
                vm.ThemeColor = Global.GetThemeColor(Global.setThemeColor);
            });

            //Condition added to change background color of custom size popup as per theme selected by user even at search result page
            switch (Global.SearchedResultSelectedStore.ToLower())
            {
                case "clothing":
                    customSizePopupFrame.BackgroundColor = Color.FromHex("#1567A6");
                    customText.BorderColor = Color.FromHex("#1567A6");
                    btnCancel.TextColor = Color.FromHex("#1567A6");
                    btnOk.TextColor = Color.FromHex("#1567A6");
                    break;
                case "streetwear":
                    customSizePopupFrame.BackgroundColor = Color.FromHex("#D04107");
                    customText.BorderColor = Color.FromHex("#D04107");
                    btnCancel.TextColor = Color.FromHex("#D04107");
                    btnOk.TextColor = Color.FromHex("#D04107");
                    break;
                case "sneakers":
                    customSizePopupFrame.BackgroundColor = Color.FromHex("#C52036");
                    customText.BorderColor = Color.FromHex("#C52036");
                    btnCancel.TextColor = Color.FromHex("#C52036");
                    btnOk.TextColor = Color.FromHex("#C52036");
                    break;
                case "vintage":
                    customSizePopupFrame.BackgroundColor = Color.FromHex("#467904");
                    customText.BorderColor = Color.FromHex("#467904");
                    btnCancel.TextColor = Color.FromHex("#467904");
                    btnOk.TextColor = Color.FromHex("#467904");
                    break;
                default:
                    customSizePopupFrame.BackgroundColor = Color.FromHex("#1567A6");
                    customText.BorderColor = Color.FromHex("#1567A6");
                    btnCancel.TextColor = Color.FromHex("#1567A6");
                    btnOk.TextColor = Color.FromHex("#1567A6");
                    break;
            }
        }

        public CustomSizePopup(string detailText)
        {
            InitializeComponent();
            customText.Text = detailText;
        }

        private void OkButtonPressed(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(customText.Text))
            {
                UserDialogs.Instance.Alert(Constant.SizeMessageStr);
                return;
            }
            if (eventOK != null)
            {
                PopupNavigation.Instance.PopAsync();
                Device.StartTimer(TimeSpan.FromMilliseconds(100),() => {
                    eventOK.Invoke(sender, customText.Text);
                    return false;
                });
            }
        }
        private void CancelButtonPressed(object sender, EventArgs e)
        {
            if (eventCancel != null)
            {
                eventCancel.Invoke(sender, e);
            }
            else
            {
                PopupNavigation.Instance.PopAsync();
            }
        }
    }
}
