using System;
using System.Collections.Generic;
using BuySell.Helper;
using BuySell.ViewModel;
using Xamarin.Forms;

namespace BuySell.Views
{
    public partial class OrderConfirmPage : ContentPage
    {
        OrderConfirmViewModel vm;
        public OrderConfirmPage()
        {
            InitializeComponent();
            BindingContext = vm =  new OrderConfirmViewModel(this.Navigation);
            vm.navigation = this.Navigation;
            header.SelectedCategoryEvent += Header_SelectedCategoryEvent;
            //lblEmail.Text = "sent to "+Constant.LoginUserData.Email;
            //lblDate.Text = DateTime.Now.ToString("MMM dd, yyyy HH:mm");
            //lblCCName.Text = Constant.LoginUserData.FirstName+" "+ Constant.LoginUserData.LastName;
        }

        public OrderConfirmPage(int orderId)
        {
            InitializeComponent();
            BindingContext = vm = new OrderConfirmViewModel(this.Navigation);
            vm.navigation = this.Navigation;
            header.SelectedCategoryEvent += Header_SelectedCategoryEvent;
            lblOrderId.Text = string.Format("#{0}", orderId.ToString());
            //lblEmail.Text = "sent to " + Constant.LoginUserData.Email;
            //lblDate.Text = DateTime.Now.ToString("MMM dd, yyyy HH:mm");
            //lblCCName.Text = Constant.LoginUserData.FirstName + " " + Constant.LoginUserData.LastName;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            vm.IsTap = false;
            vm.ThemeColor = Global.GetThemeColor(Global.setThemeColor);
            vm.SelectedIndexHeaderTab = ((int)Global.setThemeColor);
            if (!string.IsNullOrEmpty(Convert.ToString(vm.SelectedIndexHeaderTab)))
            {
                if (vm.SelectedIndexHeaderTab == 1)
                {
                    imgOrderConfrm.Source = "OrderConfrm_Clothing";
                }
                else if (vm.SelectedIndexHeaderTab == 2)
                {
                    imgOrderConfrm.Source = "OrderConfrm_Sneakers";
                }
                else if (vm.SelectedIndexHeaderTab == 3)
                {
                    imgOrderConfrm.Source = "OrderConfrm_Streetwear";
                }
                else
                {
                    imgOrderConfrm.Source = "OrderConfrm_Vintage";
                }
            }

            MessagingCenter.Send<object>(Constant.UpdateThemeStr, Constant.UpdateThemeStr);
            MessagingCenter.Send<object>(Constant.UpdateHeaderThemeStr, Constant.UpdateHeaderThemeStr);
        }

        private void Header_SelectedCategoryEvent(object sender, int e)
        {
            vm.SetThemeColor(e);
            MessagingCenter.Send<object>(Constant.UpdateThemeStr, Constant.UpdateThemeStr);
            MessagingCenter.Send<object>(Constant.UpdateHeaderThemeStr, Constant.UpdateHeaderThemeStr);
        }
    }
}
