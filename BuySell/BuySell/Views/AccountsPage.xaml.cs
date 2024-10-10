using System;
using System.Collections.Generic;
using BuySell.ViewModel;
using Xamarin.Forms;

namespace BuySell.Views
{
    public partial class AccountsPage : ContentPage
    {
        #region Constructor

        AccountsViewModel vm; 
        public AccountsPage()
        {
            InitializeComponent();
            BindingContext = vm =  new AccountsViewModel(this.Navigation);
        }
        #endregion
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
