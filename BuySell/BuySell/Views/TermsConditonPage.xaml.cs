using BuySell.Services;
using BuySell.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;

namespace BuySell.Views
{
    public partial class TermsConditonPage : ContentPage
    {
        public TermsConditonPage()
        {
            InitializeComponent();
            //lblTermsConditions.Text = MockProductData.MakeAnOfferFAQs;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            webView.Source = "https://buysellclothing.com/terms-and-conditions";
            //string filePathUrl = string.Empty;
            //if (Device.RuntimePlatform == Device.iOS)
            //{
            //    var urlSource = new UrlWebViewSource();
            //    string baseUrl = DependencyService.Get<IWebViewService>().Get();
            //    filePathUrl = Path.Combine(baseUrl, "TC.pdf");
            //    urlSource.Url = filePathUrl;
            //    webView.Source = urlSource;
            //}
            //else
            //{
            //    webView.Source = "https://drive.google.com/file/d/11s_ZQz6wdVge071YXh4wVf5h8yO0vwCQ/view?usp=share_link";
            //}
        }
    }
}
