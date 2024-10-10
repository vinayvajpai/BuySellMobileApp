using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using BuySell.CustomControl;
using BuySell.Helper;
using BuySell.Model;
using BuySell.Model.RestResponse;
using BuySell.ViewModel;
using BuySell.Views;
using BuySell.WebServices;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using Stripe;
using Stripe.Checkout;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BuySell.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderDetailPage : ContentPage
    {
        OrderDetailViewModel vm;
        public event EventHandler<int> SelectedCategoryEvent;
        public OrderDetailPage(DashBoardModel dataModel)
        {
            InitializeComponent();
            BindingContext = titleView.BindingContext = vm = new OrderDetailViewModel(this.Navigation, dataModel);
            header.SelectedCategoryEvent += Header_SelectedCategoryEvent;
            header.NavStack = this.Navigation;
            //vm.GetallcardList();

        }

        public OrderDetailPage(List<DashBoardModel> dataModelList, List<OrderItem> orderItems)
        {
            InitializeComponent();
            BindingContext = titleView.BindingContext = vm = new OrderDetailViewModel(this.Navigation, dataModelList,orderItems);
            header.SelectedCategoryEvent += Header_SelectedCategoryEvent;
            header.NavStack = this.Navigation;
            //vm.GetallcardList();
        }

        private void Header_SelectedCategoryEvent(object sender, int e)
        {
            vm.SetThemeColor(e);
            MessagingCenter.Send<object>(Constant.UpdateThemeStr, Constant.UpdateThemeStr);
            MessagingCenter.Send<object>(Constant.UpdateHeaderThemeStr, Constant.UpdateHeaderThemeStr);
        }

        async protected override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                vm.ThemeColor = Global.GetThemeColor(Global.setThemeColor);
                vm.SelectedIndexHeaderTab = ((int)Global.setThemeColor);
                vm.IsTap = false;
                MessagingCenter.Send<object>(Constant.UpdateThemeStr, Constant.UpdateThemeStr);
                MessagingCenter.Send<object>(Constant.UpdateHeaderThemeStr, Constant.UpdateHeaderThemeStr);
                vm.GetallcardList();
                //vm.AddressModel = Constant.globalSelectedAddress==null?new AddAddressModel() : Constant.globalSelectedAddress;
                //vm.AddCardModel = Constant.globalAddedCard;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            if (containView.IsVisible)
            {
                containView.IsVisible = false;
                lblStatus.Text = "Show More";
                imgArrow.Source = "Down_Img";

            }
            else
            {
                containView.IsVisible = true;
                lblStatus.Text = "Show Less";
                imgArrow.Source = "UpArrow";
            }
        }

        void OrderConfirmed_Clicked(System.Object sender, System.EventArgs e)
        {
            try
            {
                if (vm.IsTap)
                    return;
                vm.IsTap = true;
                if (Constant.LoginUserData == null)
                {
                    Global.AlertWithAction("Please login first", () =>
                    {
                        var nav = new NavigationPage(new LoginPage());
                        App.Current.MainPage = nav;
                    });
                    //var alertConfig = new AlertConfig
                    //{
                    //    Message = "Please login first",
                    //    OkText = "OK",
                    //    OnAction = () =>
                    //    {
                    //        var nav = new NavigationPage(new LoginPage());
                    //        App.Current.MainPage = nav;
                    //    }
                    //};
                    //Acr.UserDialogs.UserDialogs.Instance.Alert(alertConfig);
                }
                else
                {
                    this.Navigation.PushAsync(new OrderConfirmPage());
                }
            }
            catch (Exception ex)
            {
                vm.IsTap = false;
                Debug.WriteLine(ex.Message);
            }

        }

        async void ToolItem_Tapped(System.Object sender, System.EventArgs e)
        {
            try
            {
                var frame = (CustomFrame)sender;
                var parent = frame.Parent as StackLayout;
                var Allchild = parent.Children.AsEnumerable();
                var SelectedPopUpText = Allchild.ElementAt(1) as Label;
                if (!string.IsNullOrEmpty(SelectedPopUpText.Text))
                {
                    var popuppage = new CustomTabPopup(SelectedPopUpText.Text);
                    popuppage.SelectedEvent += Popuppage_SelectedEvent;
                    await PopupNavigation.Instance.PushAsync(popuppage);
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void Popuppage_SelectedEvent(object sender, int e)
        {
            if (SelectedCategoryEvent != null)
            {
                SelectedCategoryEvent.Invoke(sender, e);
            }
        }
        async void Search_Tapped(System.Object sender, System.EventArgs e)
        {
            try
            {
                if (vm.IsTap)
                    return;
                vm.IsTap = true;

                await Navigation.PushAsync(new SearchPage());
            }
            catch (Exception ex)
            {
                vm.IsTap = false;
                Debug.WriteLine(ex.Message);
            }

        }
    }
}