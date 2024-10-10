using System;
using System.Collections.Generic;
using BuySell.Helper;
using BuySell.Model;
using BuySell.ViewModel;
using MediaManager.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace BuySell.Views
{

    // seller page
    public partial class ViewOffersView : ContentPage
    {
        ViewOfferViewModel vm;
        DashBoardModel refresheddatamodel;
        public ViewOffersView(DashBoardModel dataModel)
        {
            InitializeComponent();
            BindingContext = vm = new ViewOfferViewModel(this.Navigation,dataModel);
            refresheddatamodel = dataModel;
            vm.navigation = this.Navigation;
            lblBrandName.Text = dataModel.Brand;
            lblPrice.Text = dataModel.Price;
            lblSize.Text =  dataModel.Size;
            lblTitle.Text = dataModel.ProductName;
            imgProduct.Source = dataModel.ProductImage.ToImageSource();
            //lblSellerName.Text = "@" + dataModel.UserName;
            lblBuyerName.Text = "@" + Global.Username;
            ToolbarItems.Add(new ToolbarItem
            {
                Name = "Cancel",
                Command = new Command(() =>
                    vm.CancelCommand.Execute(null)
                ),
            }); ;
        }
        void Back_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PopModalAsync();
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
    }
}
