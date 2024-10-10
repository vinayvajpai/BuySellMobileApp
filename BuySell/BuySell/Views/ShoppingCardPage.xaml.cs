using System;
using System.Collections.Generic;
using BuySell.Helper;
using BuySell.Model;
using BuySell.ViewModel;
using Xamarin.Forms;

namespace BuySell.Views
{
    public partial class ShoppingCardPage : ContentPage
    {
        ShoppingCartViewModel vm;
        int currentFilter = 1;
        public ShoppingCardPage()
        {
            InitializeComponent();
            BindingContext = vm = new ShoppingCartViewModel(this.Navigation);
            vm.navigation = this.Navigation;
            //titleControl.NavStack = this.Navigation;
            sep1.BackgroundColor = Color.FromHex(vm.ThemeColor);
            Device.StartTimer(TimeSpan.FromSeconds(1), () => {
                 vm.GetProductList();
                return false;
            });
           
            vm.GetOrderSummaryList();
            if (vm.CartList.Count > 0)
            {
                vm.IsShowBagList = true;
                vm.IsNoData = false;
            }
            else
            {
                vm.IsShowBagList = false;
                vm.IsNoData = true;
            }
        }

        public ShoppingCardPage(DashBoardModel  productModel)
        {
            InitializeComponent();
            BindingContext = vm = new ShoppingCartViewModel(this.Navigation,productModel);
            vm.navigation = this.Navigation;
            //titleControl.NavStack = this.Navigation;
            sep1.BackgroundColor = Color.FromHex(vm.ThemeColor);
            Device.StartTimer(TimeSpan.FromSeconds(1), () => {
                vm.GetProductList();
                return false;
            });
            vm.GetOrderSummaryList();
            if (vm.CartList.Count > 0)
            {
                vm.IsShowBagList = true;
                vm.IsNoData = false;
            }
            else
            {
                vm.IsShowBagList = false;
                vm.IsNoData = true;
            }
        }


        protected override void OnAppearing()
        {
            vm.ThemeColor = Global.GetThemeColor(Global.setThemeColor);
            vm.SelectedIndexHeaderTab = ((int)Global.setThemeColor);

            MessagingCenter.Send<object>(Constant.UpdateThemeStr, Constant.UpdateThemeStr);
            MessagingCenter.Send<object>(Constant.UpdateHeaderThemeStr, Constant.UpdateHeaderThemeStr);
            vm.IsTap = false;
            base.OnAppearing();
            
        }

        void SelectItem_Tapped(System.Object sender, System.EventArgs e)
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

        async void Back_Tapped(System.Object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
