using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using BuySell.Services;
using BuySell.Utility;
using BuySell.ViewModel;
using Xamarin.Forms;

namespace BuySell.Views
{
    public partial class FAQView : ContentPage
    {
        BaseViewModel vm;
        public FAQView()
        {
            InitializeComponent();
            BindingContext = vm =  new BaseViewModel();
            vm.navigation = this.Navigation;
            //lblFAQs.Text = MockProductData.MakeAnOfferFAQs;
        }

        protected override void OnAppearing()
        {
            vm.IsTap = false;
            base.OnAppearing();
            string filePathUrl = string.Empty;
            webView.Source = "https://buysellclothing.com/faq";
            //if (Device.RuntimePlatform == Device.iOS)
            //{
            //    var urlSource = new UrlWebViewSource();
            //    string baseUrl = DependencyService.Get<IWebViewService>().Get();
            //    filePathUrl = Path.Combine(baseUrl, "FAQ.pdf");
            //    urlSource.Url = filePathUrl;
            //    webView.Source = urlSource;
            //}
            //else
            //{
            //    webView.Source = "https://buysellclothing.com/faq";
            //}

        }

    }
}
