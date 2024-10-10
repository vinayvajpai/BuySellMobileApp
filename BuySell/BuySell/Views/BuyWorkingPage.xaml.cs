using BuySell.Utility;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace BuySell.Views
{
    public partial class BuyWorkingPage : ContentPage
    {
        public BuyWorkingPage()
        {
            InitializeComponent();
            lblBuyWorking.Text = MockProductData.MakeAnOfferFAQs;
        }
    }
}
