using System;
using System.Collections.Generic;
using BuySell.ViewModel;
using Xamarin.Forms;

namespace BuySell.Views
{
    public partial class SellingPage : ContentPage
    {
        SellingViewModel vm;
        int currentFilter = 1;
        public SellingPage()
        {
            InitializeComponent();
            BindingContext = vm = new SellingViewModel(this.Navigation);
        }

        void SelectSellingFilter_Tapped(System.Object sender, System.EventArgs e)
        {
            var par = Convert.ToInt16(((TappedEventArgs)e).Parameter);
            sep1.BackgroundColor = Color.White;
            sep2.BackgroundColor = Color.White;
            currentFilter = par;
            SetFilterSelector();
        }

        void SetFilterSelector()
        {
            try
            {
                if (currentFilter == 1)
                {
                    sep1.BackgroundColor = Color.FromHex(vm.ThemeColor);
                }
                else if (currentFilter == 2)
                {
                    sep2.BackgroundColor = Color.FromHex(vm.ThemeColor);
                }
            }
            catch (Exception ex)
            {

            }
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
