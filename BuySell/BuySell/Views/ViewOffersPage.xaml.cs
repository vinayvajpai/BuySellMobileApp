using System;
using System.Collections.Generic;
using BuySell.Helper;
using BuySell.Model;
using BuySell.ViewModel;
using MediaManager.Forms;
using Xamarin.Forms;

namespace BuySell.Views
{

    // buyer page
    public partial class ViewOffersPage : ContentPage
    {
        ViewOffersViewModel vm;
        DashBoardModel refresheddatamodel;
        public ViewOffersPage(DashBoardModel dataModel)
        {
            InitializeComponent();
            BindingContext = vm = new ViewOffersViewModel(this.Navigation, dataModel);
            vm.navigation = this.Navigation;
            refresheddatamodel = dataModel;
            lblBrandName.Text = dataModel.Brand;
            lblPrice.Text = dataModel.Price;
            lblSize.Text = dataModel.Size;
            lblTitle.Text = dataModel.ProductName;
            imgProduct.Source = dataModel.ProductImage.ToImageSource();
            lblSellerName.Text = "@" + dataModel.UserName;
            ToolbarItems.Add(new ToolbarItem
            {
                Name = "Cancel",
                Command = new Command(() =>
                    vm.DeclineOfferCmd.Execute(null)
                ),
            }); ;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            vm.IsTap = false;
            vm.ThemeColor = Global.GetThemeColor(Global.setThemeColor);
            vm.SelectedIndexHeaderTab = ((int)Global.setThemeColor);
            vm.objdashboardModel = refresheddatamodel;
            vm.GetChatList();
            MessagingCenter.Send<object>(Constant.UpdateThemeStr, Constant.UpdateThemeStr);
            MessagingCenter.Send<object>(Constant.UpdateHeaderThemeStr, Constant.UpdateHeaderThemeStr);
        }

        //void SelectSellingFilter_Tapped(System.Object sender, System.EventArgs e)
        //{
        //    var par = Convert.ToInt16(((TappedEventArgs)e).Parameter);
        //    sep1.BackgroundColor = Color.White;
        //    sep2.BackgroundColor = Color.White;
        //    currentFilter = par;
        //    SetFilterSelector();
        //}

        //void SetFilterSelector()
        //{
        //    try
        //    {
        //        if (currentFilter == 1)
        //        {
        //            sep1.BackgroundColor = Color.FromHex(vm.ThemeColor);
        //            vm.IsAccept = true;
        //            vm.IsCounter = true;
        //            vm.IsDecline = true;
        //        }
        //        else if (currentFilter == 2)
        //        {
        //            sep2.BackgroundColor = Color.FromHex(vm.ThemeColor);
        //            vm.IsAccept = true;
        //            vm.IsCounter = true;
        //            vm.IsDecline = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        private void Back_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}
