using System;
using System.Collections.Generic;
using BuySell.ViewModel;
using Xamarin.Forms;

namespace BuySell.Views
{
    public partial class MyBalancePage : ContentPage
    {
        MyBalanceViewModel ViewModel;
        public MyBalancePage()
        {
            InitializeComponent();
            BindingContext = ViewModel =  new MyBalanceViewModel(this.Navigation);
        }

        protected override void OnAppearing()
        {
            ViewModel.IsTap = false;
            base.OnAppearing();
        }

        void Back_Tapped(System.Object sender, System.EventArgs e)
        {
        }
    }
}
