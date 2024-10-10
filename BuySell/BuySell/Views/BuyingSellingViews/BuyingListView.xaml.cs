using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuySell.Helper;
using BuySell.Model;
using BuySell.ViewModel;
using Xamarin.Forms;

namespace BuySell.Views.BuyingSellingViews
{	
	public partial class BuyingListView : ContentPage
	{
		BuyingListViewModel vm;
        int currentFilter = 1;
        public BuyingListView(string title)
		{
			InitializeComponent ();
            titleView.TitleText = title;
           Global.BuyingPageTitle = title;
            BindingContext = vm = new BuyingListViewModel(title, this.Navigation);
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
                    vm.SetData(1);
                    return;
                }
                else if (currentFilter == 2)
                {
                    sep2.BackgroundColor = Color.FromHex(vm.ThemeColor);
                    vm.SetData(2);
                    return;
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected override void OnAppearing()
        {
            //if (vm.Title.ToLower() == "buying")
            //{

            //    vm.IsSellListVisible = false;
            //    vm.BuyItemThreshold = 1;
            //    vm.BuyItemTresholdReachedCmd = new Command(async () => await vm.BuyItemsTresholdReached());
            //    Task.Run(() =>
            //    {
            //        vm.GetBuyingOrderList();
            //    });

            //}
            //else
            //{
            //    //GetSellingOrderList();
            //    vm.IsSellListVisible = false;
            //    vm.SellItemThreshold = 1;
            //    vm.SellItemTresholdReachedCmd = new Command(async () => await vm.SellItemsTresholdReached());

            //    Task.Run(() =>
            //    {
            //        vm.GetSellingOrderList();
            //    });
            //}
            if (Global.IsReload)
            {
                SetFilterSelector();
            }
            Global.IsReload = false;
            SerachEntry.Placeholder = Global.BuyingPageTitle.ToLower() == "buying" ? "Search my purchases" : "Search my sales" ;
            base.OnAppearing();

        }

        void ListView_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                this.Navigation.PushAsync(new OrderSummaryView((ViewModel.BuyingSellingModel)e.SelectedItem));
                listBuying.SelectedItem = null;
            }
        }

        async void ViewStore_Tapped(System.Object sender, System.EventArgs e)
        {
            await this.Navigation.PushAsync(new SellerClosetView("My Store", Constant.LoginUserData.Id.ToString(), true));
        }

        private async void listBuying_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var item = sender as ViewModel.BuyingSellingModel;
            if (vm.SelectedOrderItem != null)
            await vm.navigation.PushAsync(new OrderDetailsShippingPage(item));
        }
    }
}

