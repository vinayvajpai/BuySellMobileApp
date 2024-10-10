using BuySell.Utility;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace BuySell.Views
{
    public partial class ContactSupportPage : ContentPage
    {
        public ContactSupportPage()
        {
            InitializeComponent();
            lblContactSupport.Text = MockProductData.MakeAnOfferFAQs;
        }
    }
}
