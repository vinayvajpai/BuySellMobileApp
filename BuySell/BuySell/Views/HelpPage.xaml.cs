using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BuySell.Views
{
    public partial class HelpPage : ContentPage
    {
        public bool IsTap = false;
        public HelpPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            IsTap = false;
            base.OnAppearing();
        }

        async void FAQ_Tapped(System.Object sender, System.EventArgs e)
        {
            try
            {
                if (IsTap)
                    return;
                IsTap = true;
                await Navigation.PushAsync(new FAQView());
            }
            catch (Exception ex)
            {
                IsTap = false;
                Debug.WriteLine(ex.Message);
            }
        }

       async void PravacyPolicy_Tapped(System.Object sender, System.EventArgs e)
        {
            try
            {
                if (IsTap)
                    return;
                IsTap = true;
                await Navigation.PushAsync(new PrivacyPolicyPage());
            }
            catch (Exception ex)
            {
                IsTap = false;
                Debug.WriteLine(ex.Message);
            }
        }

       async void TermsCondition_Tapped(System.Object sender, System.EventArgs e)
        {
            try
            {
                if (IsTap)
                    return;
                IsTap = true;
                await Navigation.PushAsync(new TermsConditonPage());
            }
            catch (Exception ex)
            {
                IsTap = false;
                Debug.WriteLine(ex.Message);
            }
        }

       async void ContactSupport_Tapped(System.Object sender, System.EventArgs e)
        {
            try
            {
                //if (IsTap)
                    //return;
                //IsTap = true;
                await Launcher.OpenAsync(new Uri("https://buysellclothing.com/contact-us"));
            }
            catch (Exception ex)
            {
                IsTap = false;
                Debug.WriteLine(ex.Message);
            }
        }

        
    }
}
