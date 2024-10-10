using System;
using System.Collections.Generic;
using BuySell.ViewModel;
using Xamarin.Forms;

namespace BuySell.Views
{
    public partial class OurStoresPage : ContentPage
    {
        OurStoresViewModel vm;
        public OurStoresPage()
        {
            InitializeComponent();
            BindingContext = vm = new OurStoresViewModel(this.Navigation);
        }

        protected override void OnAppearing()
        {
            vm.IsTap = false;
            base.OnAppearing();
        }

        void Back_Tapped(System.Object sender, System.EventArgs e)
        {
        }
    }
}
