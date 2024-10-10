using System;
using System.Collections.Generic;
using BuySell.ViewModel;
using Xamarin.Forms;
using BuySell.Model;
using System.Diagnostics;

namespace BuySell.Views
{
    public partial class MakeAnOfferDetailPage : ContentPage
    {
        MakeAnOfferDetailViewModel vm;
        public MakeAnOfferDetailPage(DashBoardModel prodDataModel,string OfferPrice)
        {
            InitializeComponent();
            BindingContext = vm= new MakeAnOfferDetailViewModel(this.Navigation,prodDataModel,OfferPrice);
            vm.navigation = this.Navigation;
        }
        async void Tapped_Back(System.Object sender, System.EventArgs e)
        {
            try
            {
                if (vm.IsTap)
                    return;
                vm.IsTap = true;

              await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                vm.IsTap = false;
                Debug.WriteLine(ex.Message);
            }
        }

        async void Tapped_Cancel(System.Object sender, System.EventArgs e)
        {
            try
            {
                if (vm.IsTap)
                    return;
                vm.IsTap = true;

              await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                vm.IsTap = false;
                Debug.WriteLine(ex.Message);
            }
        }

        protected override void OnAppearing()
        {
            vm.IsTap = false;
            base.OnAppearing();
        }

        async void Questions_Tapped(System.Object sender, System.EventArgs e)
        {
            try
            {
                if (vm.IsTap)
                    return;
                vm.IsTap = true;

                await Navigation.PushAsync(new MakeAnOfferFAQPage());
            }
            catch (Exception ex)
            {
                vm.IsTap = false;
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
