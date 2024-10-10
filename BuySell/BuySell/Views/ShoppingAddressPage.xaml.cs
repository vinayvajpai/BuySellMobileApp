using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BuySell.Helper;
using BuySell.Model;
using BuySell.Model.RestResponse;
using BuySell.ViewModel;
using BuySell.WebServices;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace BuySell.Views
{
    public partial class ShoppingAddressPage : ContentPage
    {

        ShoppingAddressViewModel vm;
        public ShoppingAddressPage()
        {
            InitializeComponent();
            BindingContext = titleControl.BindingContext = vm = new ShoppingAddressViewModel(this.Navigation);
            vm.PropertyChanged += Vm_PropertyChanged;
        }

        private void Vm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (vm.ShoppingAddList.Count > 0)
            {
                //lblNoData.IsVisible = false;
                listAddress.IsVisible = true;
            }
            else
            {
                //lblNoData.IsVisible = true;
                listAddress.IsVisible = false;
            }
        }

        public ShoppingAddressPage(Action<AddAddressModel> action)
        {
            InitializeComponent();
            BindingContext = titleControl.BindingContext = vm = new ShoppingAddressViewModel(this.Navigation, action);
            vm.PropertyChanged += Vm_PropertyChanged;
        }

        protected override void OnAppearing()
        {
            if (vm != null)
            {
                vm.GetAllShippingAddress();
                vm.IsTap = false;
            }
            base.OnAppearing();
        }

    }
}
