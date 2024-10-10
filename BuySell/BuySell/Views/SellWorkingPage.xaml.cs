using BuySell.Services;
using BuySell.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BuySell.Views
{
    public partial class SellWorkingPage : ContentPage
    {
        public SellWorkingPage()
        {
            InitializeComponent();
            //lblSellWorking.Text = MockProductData.MakeAnOfferFAQs;
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            string filePathUrl = string.Empty;
            var urlSource = new HtmlWebViewSource();
            string baseUrl = DependencyService.Get<IWebViewService>().ReadAssetFile("Content/Selling.html");
            urlSource.Html = baseUrl;
            webView.Source = urlSource;

        }
    }
}
