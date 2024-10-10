using BuySell.Helper;
using BuySell.Model;
using BuySell.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BuySell.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ShippingFromAddressPage : ContentPage
	{
        ShippingFromAddressViewModel vm;
        public ShippingFromAddressPage()
        {
            InitializeComponent();
            BindingContext = titleControl.BindingContext = vm = new ShippingFromAddressViewModel(this.Navigation);
            vm.PropertyChanged += Vm_PropertyChanged;
        }

        private void Vm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (vm.ShoppingAddList.Count > 0 && vm.ShowAddAddressBtn==false)
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

        public ShippingFromAddressPage(Action<AddAddressModel> action)
        {
            InitializeComponent();
            BindingContext = titleControl.BindingContext = vm = new ShippingFromAddressViewModel(this.Navigation, action);
            vm.PropertyChanged += Vm_PropertyChanged;
        }

        protected override void OnAppearing()
        {
            if (vm != null)
            {
                vm.GetAllShippingAddress();
                vm.ShowAddAddressBtn = Global.GlobalShipFromAddressList?.Count == 0;
                vm.IsTap = false;
            }
            base.OnAppearing();
        }

    }
}
