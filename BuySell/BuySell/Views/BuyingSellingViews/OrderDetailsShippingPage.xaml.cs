using BuySell.Helper;
using BuySell.View;
using BuySell.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BuySell.Views.BuyingSellingViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderDetailsShippingPage : ContentPage
    {
        OrderDetailsShippingViewModel vm;
        string selectedReson = null;
        ViewModel.BuyingSellingModel OrderItem;
        public OrderDetailsShippingPage()
        {
            InitializeComponent();
        }
        public OrderDetailsShippingPage(ViewModel.BuyingSellingModel selectedOrderItem)
        {
            InitializeComponent();
            BindingContext = vm = new OrderDetailsShippingViewModel(this.Navigation, selectedOrderItem);
            OrderItem = selectedOrderItem;
            SetStatusData(OrderItem);
        }


        void SetStatusData(ViewModel.BuyingSellingModel OrderItem)
        {
            try
            {
                if (string.IsNullOrEmpty(OrderItem.OrderProductItem.ShipmentStatus))
                {
                    var diffOfDates = DateTime.Now - OrderItem.CreatedDate;
                    if (diffOfDates.Days >= 5)
                    {
                        vm.IsShowCancelOption = false;
                    }
                    else
                    {
                        vm.IsShowCancelOption = true;
                    }
                    actionStk.IsVisible = false;
                }
                else if (OrderItem.OrderProductItem.ShipmentStatus == "0")
                {
                    var diffOfDates = DateTime.Now - OrderItem.CreatedDate;
                    if (diffOfDates.Days >= 5)
                    {
                        vm.IsShowCancelOption = false;
                    }
                    else
                    {
                        vm.IsShowCancelOption = true;
                    }
                    actionStk.IsVisible = false;
                }
                else if (OrderItem.OrderProductItem.ShipmentStatus == "1")
                {
                    actionStk.IsVisible = false;
                    vm.IsShowCancelOption = false;
                }
                else if (OrderItem.OrderProductItem.ShipmentStatus == "2")
                {
                    actionStk.IsVisible = false;
                    vm.IsShowCancelOption = false;
                }
                else if (OrderItem.OrderProductItem.ShipmentStatus == "3")
                {
                    //var diffOfDates = DateTime.Now - OrderItem.CreatedDate;
                    //if (diffOfDates.Days <= 3)
                    //{
                    //    actionStk.IsVisible = true;
                    //}
                    //else
                    //{
                    //    actionStk.IsVisible = false;
                    //}
                    vm.IsShowCancelOption = false;
                    actionStk.IsVisible = false;
                }
                else if (OrderItem.OrderProductItem.ShipmentStatus == "4")
                {
                    actionStk.IsVisible = false;
                    vm.IsShowCancelOption = false;
                }
                else if (OrderItem.OrderProductItem.ShipmentStatus == "5")
                {
                    actionStk.IsVisible = false;
                    vm.IsShowCancelOption = false;
                }
                else if (OrderItem.OrderProductItem.ShipmentStatus == "6")
                {
                    actionStk.IsVisible = true;
                    vm.IsShowCancelOption = false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }

        private async void cancelResonPicker_Unfocused(object sender, FocusEventArgs e)
        {
            if (selectedReson != null)
            {
                var res = await Acr.UserDialogs.UserDialogs.Instance.ConfirmAsync("Do you want to cancel order?", "Cancel Order", "OK", "Cancel");
                if (res)
                {
                    vm.CancelReason = cancelResonPicker.SelectedItem as string;
                    if (vm.CancelReason != null)
                    {
                        vm.CancelOrderCommand();
                    }
                    cancelResonPicker.SelectedIndex = -1;
                }
                else
                {
                    selectedReson = null;
                    cancelResonPicker.SelectedIndex = -1;
                }
            }
        }

        private void cancelResonPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedReson = cancelResonPicker.SelectedItem as string;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            cancelResonPicker.Focus();
        }

        protected override void OnAppearing()
        {
            try
            {
                if (vm.SellingItem.ShippingAddress != null)
                {
                    vm.FormattedAdd = Global.AddressFormatter(vm.SellingItem.ShippingAddress);
                }
            }
            catch (Exception)
            {
                Debug.WriteLine("issue with setting status in order detail");
            }
           
            base.OnAppearing();

        }

        private void SelectItem_Tapped(object sender, EventArgs e)
        {
            try
            {
                var selectedProduct = ((BuyingSellingOrderItem)((TappedEventArgs)e).Parameter).Product;
                if (selectedProduct != null)
                {
                    var item = JsonConvert.SerializeObject(selectedProduct);
                    vm.navigation.PushAsync(new ItemDetailsPage(JsonConvert.DeserializeObject<Model.DashBoardModel>(item), true));
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}