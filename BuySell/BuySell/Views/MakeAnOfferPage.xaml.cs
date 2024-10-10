using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BuySell.Helper;
using BuySell.Model;
using BuySell.ViewModel;
using Xamarin.Forms;

namespace BuySell.Views
{
    public partial class MakeAnOfferPage : ContentPage
    {
        MakeAnOfferViewModel vm;
        public  MakeAnOfferPage(DashBoardModel dataModel)
        {
            InitializeComponent();
            BindingContext = vm =  new MakeAnOfferViewModel(this.Navigation, dataModel);
            vm.navigation = this.Navigation;
        }

        protected override void OnAppearing()
        {
            vm.IsTap = false;
            base.OnAppearing();
        }

        async void Tapped_Back(System.Object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

      async  void Tapped_Cancel(System.Object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
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

        private void OfferPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            vm.OfferPrice = "$" + vm.OfferPrice.Replace("$", "");
        }

        private void ListPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            vm.ListPrice = "$" + vm.ListPrice.Replace("$", "");
        }

        //void CustomEntry_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        //{
        //    try
        //    {
        //        if (!string.IsNullOrWhiteSpace(e.NewTextValue))
        //        {
        //            bool isValid = Global.IsValidatedecimalNumber(e.NewTextValue.Replace("$", ""));
        //            ((Entry)sender).Text = isValid ? e.NewTextValue : e.NewTextValue.Remove(e.NewTextValue.Length - 1);
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
    }
}
