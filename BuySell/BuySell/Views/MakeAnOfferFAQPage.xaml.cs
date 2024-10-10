using System;
using System.Collections.Generic;
using System.Diagnostics;
using BuySell.Helper;
using BuySell.Utility;
using Xamarin.Forms;

namespace BuySell.Views
{
    public partial class MakeAnOfferFAQPage : ContentPage
    {
        public bool IsTap = false;
        public MakeAnOfferFAQPage()
        {
            InitializeComponent();
            lblFAQs.Text = MockProductData.MakeAnOfferFAQs;
        }

        protected override void OnAppearing()
        {
            IsTap = false;
            base.OnAppearing();
        }

        async  void BackArrow_Tapped(System.Object sender, System.EventArgs e)
        {
            try
            {
                if (IsTap)
                    return;
                IsTap = true;

                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                IsTap= false;
                Debug.WriteLine(ex.Message);  
            }
            
        }
    }
}
