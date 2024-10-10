using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Acr.UserDialogs;
using BuySell.CustomControl;
using BuySell.Helper;
using BuySell.Model;
using BuySell.Model.RestResponse;
using BuySell.Utility;
using BuySell.ViewModel;
using BuySell.WebServices;
using Newtonsoft.Json;
using Plugin.DeviceInfo;
using Rg.Plugins.Popup.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

namespace BuySell.Views
{
    public partial class AccountPage : ContentPage
    {
        public event EventHandler<int> SelectedCategoryEvent;
        AccountViewModel vm;
        CancellationTokenSource cts = new CancellationTokenSource();
        int currentFilter = 1;
        int currentOfferFilter = 1;

        #region Constructor
        public AccountPage()
        {
            InitializeComponent();
            BindingContext = vm = new AccountViewModel(this.Navigation);
            header.BackCommand = vm.LoginBackCommand;
            header.NavStack = this.Navigation;
            header.SelectedCategoryEvent += Header_SelectedCategoryEvent;
            if (Global.currentPlaceMark != null)
            {
                stkLocation.IsVisible = true;
                lblLocation.Text = string.Format("{0}, {1}", Global.currentPlaceMark.AdminArea, Global.currentPlaceMark.CountryName);
            }
            else
            {
                stkLocation.IsVisible = false;
            }

        }
        #endregion

        #region Methods
        protected override async void OnAppearing()
        {
            //Condition added to check whether user is authorized or not
            if (Constant.LoginUserData != null)
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Constant.LoginUserData.ProfilePath)))
                {
                    imgProfile.Source = ImageSource.FromUri(new Uri(Convert.ToString(Constant.LoginUserData.ProfilePath)));
                    addProPicTxt.Text = null;
                }
                else
                {
                    imgProfile.Source = null;
                    addProPicTxt.Text = "No Image";
                }
                lblFirstname.Text = (Constant.LoginUserData.FirstName + " " + Constant.LoginUserData.LastName).Replace("'", "");
                lblEmail.Text = "@" + Constant.LoginUserData.UserId;
                lblJoinDate.Text = "Joined: " + DateTime.Now.ToString("MMM-yyyy");
            }
            vm.ThemeColor = Global.GetThemeColor(Global.setThemeColor);
            vm.SelectedIndexHeaderTab = ((int)Global.setThemeColor);
            vm.IsTap = false;
            if(vm != null)
            {
                if (Global.currentPlaceMark == null)
                {
                    await vm.GetCurrentLocation(cts); // this will store the current location in global.currentplaceMark

                    if (Global.currentPlaceMark != null)
                    {
                        lblLocation.Text = string.Format("{0}, {1}", Global.currentPlaceMark.AdminArea, Global.currentPlaceMark.CountryName);
                    }
                }
            }

            if (vm.ViewOffersList != null)
            {
                vm.GetViewOffersList();
            }
            vm.GetMyEarning();
            MessagingCenter.Send<object>(Constant.UpdateThemeStr, Constant.UpdateThemeStr);
            MessagingCenter.Send<object>(Constant.UpdateHeaderThemeStr, Constant.UpdateHeaderThemeStr);
            base.OnAppearing();
        }

        private void Header_SelectedCategoryEvent(object sender, int e)
        {
            vm.SetThemeColor(e);
            MessagingCenter.Send<object>(Constant.UpdateThemeStr, Constant.UpdateThemeStr);
            MessagingCenter.Send<object>(Constant.UpdateHeaderThemeStr, Constant.UpdateHeaderThemeStr);
        }
        void SelectSellingFilter_Tapped(System.Object sender, System.EventArgs e)
        {
            UserDialogs.Instance.Alert("", "Coming soon", "OK");
            return;
            var par = Convert.ToInt16(((TappedEventArgs)e).Parameter);
            sep1.BackgroundColor = Color.White;
            sep2.BackgroundColor = Color.White;
            currentFilter = par;
            SetFilterSelector();
        }

        void SetFilterSelector()
        {
            try
            {
                if (currentFilter == 1)
                {
                    sep1.BackgroundColor = Color.White;
                    //BalanceSection.IsVisible = true;
                    //vm.IsAccountShow = true;
                    //vm.IsFilterShow = true;
                    //OffersSection.IsVisible = false;
                    //vm.IsOfferShow = false;
                    //UserDialogs.Instance.Alert("", "Coming soon", "OK");
                    this.Navigation.PushAsync(new MyEarningView(vm.earningresponseModel));
                    return;
                }
                else if (currentFilter == 2)
                {
                    sep2.BackgroundColor = Color.White;
                    BalanceSection.IsVisible = false;
                    vm.IsFilterShow = false;
                    vm.IsAccountShow = false;
                    OffersSection.IsVisible = true;
                    vm.IsOfferShow = true;
                    //UserDialogs.Instance.Alert("", "Coming soon", "OK");
                    return;
                }
            }
            catch (Exception ex)
            {

            }
        }

        async void ShowProfilePicture_Tapped(System.Object sender, System.EventArgs e)
        {

            try
            {
                if (vm.IsTap)
                    return;

                vm.IsTap = true;

                if (vm != null)
                {
                    List<ImageSource> profileImage = new List<ImageSource>();
                    profileImage.Add(imgProfile.Source);
                    if (imgProfile.Source != null)
                    {
                        var popupdefault = new ViewMoreImgPopup(profileImage, vm.ThemeColor);
                        await PopupNavigation.Instance.PushAsync(popupdefault);
                    }
                    await Task.Delay(1000);
                    vm.IsTap = false;
                }
            }
            catch (Exception ex)
            {
                vm.IsTap = false;
                Debug.WriteLine(ex.Message + "From ItemDetailView.xaml.cs");
            }
        }
        private void SelectofferSellingFilter_Tapped(object sender, EventArgs e)
        {
            var par = Convert.ToInt16(((TappedEventArgs)e).Parameter);
            sepOffer1.BackgroundColor = Color.White;
            sepOffer2.BackgroundColor = Color.White;
            currentOfferFilter = par;
            SetOfferFilterSelect();
        }
        void SetOfferFilterSelect()
        {
            try
            {
                if (currentOfferFilter == 1)
                {
                    sepOffer1.BackgroundColor = Color.FromHex(vm.ThemeColor);
                    viewOfferBuyerList.IsVisible = true;
                    viewOfferSellerList.IsVisible = false;
                }
                else if (currentOfferFilter == 2)
                {
                    sepOffer2.BackgroundColor = Color.FromHex(vm.ThemeColor);
                    viewOfferBuyerList.IsVisible = false;
                    viewOfferSellerList.IsVisible = true;
                }
            }
            catch (Exception ex)
            {

            }
        }
        //void ViewOffer_Tapped(System.Object sender, System.EventArgs e)
        //{
        //    this.Navigation.PushModalAsync(new NavigationPage(new ViewOffersView(ViewdataModel)));
        //}
        #endregion
    }
}
