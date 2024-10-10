using System;
using System.Collections.Generic;
using System.Diagnostics;
using BuySell.ViewModel;
using Xamarin.Forms;

namespace BuySell.Views
{
    public partial class PaymentMethodsPage : ContentPage
    {
        PaymentMethodsViewModel vm;
        public PaymentMethodsPage()
        {
            InitializeComponent();
            BindingContext = vm = new PaymentMethodsViewModel(this.Navigation);
        }

        protected override void OnAppearing()
        {
            vm.IsTap = false;
            base.OnAppearing();
        }

        async void EditCard_Tapped(System.Object sender, System.EventArgs e)
        {
            try
            {
                if (vm.IsTap)
                    return;
                vm.IsTap = true;

                await Navigation.PushAsync(new EditCardPage());
            }
            catch (Exception ex)
            {
                vm.IsTap = false;
                Debug.WriteLine(ex.Message);
            }
            
        }
    }
}
