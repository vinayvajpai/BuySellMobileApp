using System;
using System.Collections.Generic;
using System.Diagnostics;
using BuySell.ViewModel;
using Xamarin.Forms;

namespace BuySell.Views
{
    public partial class SettingPage : ContentPage
    {
        SettingViewModel vm;

        #region Constructor
        public SettingPage()
        {
            InitializeComponent();
            BindingContext = vm = new SettingViewModel(this.Navigation);
        }
        #endregion

        protected override void OnAppearing()
        {
            vm.IsTap = false;
            base.OnAppearing();
        }
        async void Notifications_Tapped(System.Object sender, System.EventArgs e)
        {
            try
            {
                if (vm.IsTap)
                    return;
                vm.IsTap = true;

                await Navigation.PushAsync(new NotificationsPage());
            }
            catch (Exception ex)
            {
                vm.IsTap = false;
                Debug.WriteLine(ex.Message);
            }

        }

        async void PaymentMethods_Tapped(System.Object sender, System.EventArgs e)
        {
            try
            {
                if (vm.IsTap)
                    return;
                vm.IsTap = true;

                await Navigation.PushAsync(new CreditCardListView());
            }
            catch (Exception ex)
            {
                vm.IsTap = false;
                Debug.WriteLine(ex.Message);
            }
        }

        async void BankAccount_Tapped(System.Object sender, System.EventArgs e)
        {
            try
            {
                if (vm.IsTap)
                    return;
                vm.IsTap = true;

                await Navigation.PushAsync(new BankAccountPage());
            }
            catch (Exception ex)
            {
                vm.IsTap = false;
                Debug.WriteLine(ex.Message);
            }
        }

        async void ShoppingAddress_Tapped(object sender, EventArgs e)
        {
            try
            {
                if (vm.IsTap)
                    return;
                vm.IsTap = true;

                await Navigation.PushAsync(new ShoppingAddressPage());
            }
            catch (Exception ex)
            {
                vm.IsTap = false;
                Debug.WriteLine(ex.Message);
            }
        }

        //private  async void ShoppingFromAddress_Tapped(object sender, EventArgs e)
        //{

        //    try
        //    {
        //        if (vm.IsTap)
        //            return;
        //        vm.IsTap = true;

        //        await Navigation.PushAsync(new ShippingFromAddressPage());
        //    }
        //    catch (Exception ex)
        //    {
        //        vm.IsTap = false;
        //        Debug.WriteLine(ex.Message);
        //    }
        //}
    }
}
