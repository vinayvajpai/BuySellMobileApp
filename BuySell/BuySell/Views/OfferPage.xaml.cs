using System;
using System.Collections.Generic;
using BuySell.Model;
using BuySell.ViewModel;
using Xamarin.Forms;

namespace BuySell.Views
{
    public partial class OfferPage : ContentPage
    {
        OfferViewModel vm;
        int currentFilter = 1;
        DashBoardModel dashboardModel;
        public OfferPage()
        {
            InitializeComponent();
            BindingContext = vm =  new OfferViewModel(this.Navigation);
        }
        void SelectSellingFilter_Tapped(System.Object sender, System.EventArgs e)
        {
            var par = Convert.ToInt16(((TappedEventArgs)e).Parameter);
            sep1.BackgroundColor = Color.White;
            sep2.BackgroundColor = Color.White;
            currentFilter = par;
            SetFilterSelector();
        }

        protected override void OnAppearing()
        {
            vm.IsTap = false;
            base.OnAppearing();
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
        void ViewOffersView_Tapped(System.Object sender, System.EventArgs e)
        {
            DashBoardModel dashBoard = ((ImageButton)sender).BindingContext as DashBoardModel;
            //Navigation.PushAsync(new ViewOffersView(dashBoard));
        }

        void Item_Tapped(object sender, SelectedItemChangedEventArgs e) {
            DashBoardModel dashBoard = ((ListView)sender).BindingContext as DashBoardModel;
            //Navigation.PushAsync(new MakeAnOfferDetailPage(dashBoard,"12"));
        }
    }
}
