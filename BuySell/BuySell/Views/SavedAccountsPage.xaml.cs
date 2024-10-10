using System;
using System.Collections.Generic;
using BuySell.ViewModel;
using Xamarin.Forms;

namespace BuySell.Views
{
    public partial class SavedAccountsPage : ContentPage
    {
        SavedAccountsViewModel vm; 
        public SavedAccountsPage()
        {
            InitializeComponent();
            BindingContext = vm =  new SavedAccountsViewModel(this.Navigation);
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
