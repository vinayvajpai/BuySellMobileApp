using System;
using System.Collections.Generic;
using System.Diagnostics;
using BuySell.ViewModel;
using Xamarin.Forms;

namespace BuySell.Views
{
    public partial class SellingCategoryPage : ContentPage
    {
        SellingCategoryViewModel vm; 
        public SellingCategoryPage()
        {
            InitializeComponent();
            BindingContext = vm = new SellingCategoryViewModel(this.Navigation);
        }

        protected override void OnAppearing()
        {
            vm.IsTap = false;
            base.OnAppearing();
        }

        //async void SelecteCategory_Tapped(System.Object sender, System.EventArgs e)
        // {
        //     await Navigation.PushAsync(new OurStoresPage());
        // }

        async void HelpNeed_Tapped(System.Object sender, System.EventArgs e)
        {
            try
            {
                if (vm.IsTap)
                    return;
                vm.IsTap = true;

                await Navigation.PushAsync(new OurStoresPage());
            }
            catch (Exception ex)
            {
                vm.IsTap = false;
                Debug.WriteLine(ex.Message);
            }
          
        }
    }
}
