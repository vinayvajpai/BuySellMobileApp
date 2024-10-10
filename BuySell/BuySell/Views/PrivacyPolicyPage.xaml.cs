using BuySell.Services;
using BuySell.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;

namespace BuySell.Views
{
    public partial class PrivacyPolicyPage : ContentPage
    {
        public PrivacyPolicyPage()
        {
            InitializeComponent();
            //lblPrivacyPolicy.Text = MockProductData.MakeAnOfferFAQs;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            webView.Source = "https://buysellclothing.com/privacy-policy";
            //string filePathUrl = string.Empty;
            //if (Device.RuntimePlatform == Device.iOS)
            //{
            //    var urlSource = new UrlWebViewSource();
            //    string baseUrl = DependencyService.Get<IWebViewService>().Get();
            //    filePathUrl = Path.Combine(baseUrl, "PP.pdf");
            //    urlSource.Url = filePathUrl;
            //    webView.Source = urlSource;
            //}
            //else
            //{
            //    webView.Source = "https://drive.google.com/file/d/1cMdNYopN0b-njebA_wrXKEwPuAU4qw_4/view?usp=share_link";
            //}
        }
    }
}
