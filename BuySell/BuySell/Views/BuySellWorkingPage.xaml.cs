using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BuySell.Views
{
    public partial class BuySellWorkingPage : ContentPage
    {
        public bool IsTap = false;

        public BuySellWorkingPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            IsTap = false;
            base.OnAppearing();
        }

        async void Buying_Tapped(System.Object sender, System.EventArgs e)
        {
            try
            {
                if (IsTap)
                    return;
                IsTap = true;
                await Navigation.PushAsync(new BuyWorkingPage());
                //UserDialogs.Instance.Alert("", "Coming soon", "OK");
                IsTap = false;
                //await Navigation.PushAsync(new BuyWorkingPage());
            }
            catch (Exception ex)
            {
                IsTap = false;
                Debug.WriteLine(ex.Message);
            }
        }

      async  void Selling_Tapped(System.Object sender, System.EventArgs e)
        {
            try
            {
                if (IsTap)
                    return;
                IsTap = true;
                await Launcher.OpenAsync(new Uri("https://buysellclothing.com/how-to-buy-%26-sell"));
                //await Navigation.PushAsync(new SellWorkingPage());
            }
            catch (Exception ex)
            {
                IsTap = false;
                Debug.WriteLine(ex.Message);
            }

        }
    }
}
