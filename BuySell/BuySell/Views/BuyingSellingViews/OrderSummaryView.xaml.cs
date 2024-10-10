using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Acr.UserDialogs;
using BuySell.Helper;
using BuySell.View;
using BuySell.ViewModel;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace BuySell.Views.BuyingSellingViews
{	
	public partial class OrderSummaryView : ContentPage
	{
        OrderSummaryViewModel vm;
        string selectedReson = null;
        public OrderSummaryView (BuyingSellingModel buyingProduct)
		{
			InitializeComponent ();
			BindingContext = vm = new OrderSummaryViewModel(this.Navigation ,buyingProduct);
            SetStatusData(buyingProduct);
        }

        void SetStatusData(BuyingSellingModel buyingProduct)
		{
            try
            {

                orderstatusFrm.BackgroundColor = Color.FromHex(buyingProduct.OrderProductItem.Shipment.StatusColor);
                orderStatus.Text = buyingProduct.OrderProductItem.Shipment.StatusType;
                if (string.IsNullOrEmpty(buyingProduct.OrderProductItem.ShipmentStatus))
                {
                    var diffOfDates = DateTime.Now - buyingProduct.CreatedDate;
                    if (diffOfDates.Days>=5)
                    {
                        cancelStk.IsVisible = false;
                    }
                    else
                    {
                        cancelStk.IsVisible = true;
                    }
                    actionStk.IsVisible = false;
                }
                else if (buyingProduct.OrderProductItem.ShipmentStatus == "0")
                {
                    var diffOfDates = DateTime.Now - buyingProduct.CreatedDate;
                    if (diffOfDates.Days >= 5)
                    {
                        cancelStk.IsVisible = false;
                    }
                    else
                    {
                        cancelStk.IsVisible = true;
                    }
                    actionStk.IsVisible = false;
                }
                else if (buyingProduct.OrderProductItem.ShipmentStatus=="1")
                {
                    actionStk.IsVisible = false;
                    cancelStk.IsVisible = false;
                }
                else if (buyingProduct.OrderProductItem.ShipmentStatus == "2")
                {
                    actionStk.IsVisible = false;
                    cancelStk.IsVisible = false;
                }
                else if (buyingProduct.OrderProductItem.ShipmentStatus == "3")
                {
                    var diffOfDates = DateTime.Now - buyingProduct.CreatedDate;
                    if (diffOfDates.Days <= 3)
                    {
                        actionStk.IsVisible = true;
                    }
                    else
                    {
                        actionStk.IsVisible = false;
                    }
                    cancelStk.IsVisible = false;
                }
                else if (buyingProduct.OrderProductItem.ShipmentStatus == "4")
                {
                    actionStk.IsVisible = false;
                    cancelStk.IsVisible = false;
                }
                else if (buyingProduct.OrderProductItem.ShipmentStatus == "5")
                {
                    actionStk.IsVisible = false;
                    cancelStk.IsVisible = false;
                }
                else if (buyingProduct.OrderProductItem.ShipmentStatus == "6")
                {
                    actionStk.IsVisible = false;
                    cancelStk.IsVisible = false;
                }



                //if (buyingProduct.OrderProductItem.Shipment.StatusType.Contains("Pending"))
                //{
                //    orderstatusFrm.BackgroundColor = Color.FromHex(buyingProduct.OrderProductItem.Shipment.StatusColor);
                //    orderStatus.Text = "Pending Shippment";
                //    actionStk.IsVisible = false;
                //    cancelStk.IsVisible = true;
                //}
                //else if (buyingProduct.OrderProductItem.Shipment.StatusType.Contains("Shipped"))
                //{
                //    orderstatusFrm.BackgroundColor = Color.FromHex(buyingProduct.OrderProductItem.Shipment.StatusColor);
                //    orderStatus.Text = "Shipped";
                //    actionStk.IsVisible = false;
                //    cancelStk.IsVisible = false;
                //}
                //else if (buyingProduct.OrderProductItem.Shipment.StatusType.Contains("Delivered"))
                //{
                //    orderstatusFrm.BackgroundColor = Color.FromHex(buyingProduct.OrderProductItem.Shipment.StatusColor);
                //    orderStatus.Text = "Delivered";
                //    actionStk.IsVisible = true;
                //    cancelStk.IsVisible = true;
                //}
                //else if (buyingProduct.OrderProductItem.Shipment.StatusType.Contains("Dispute"))
                //{
                //    orderstatusFrm.BackgroundColor = Color.FromHex(buyingProduct.OrderProductItem.Shipment.StatusColor);
                //    orderStatus.Text = "In Dispute";
                //    actionStk.IsVisible = true;
                //    cancelStk.IsVisible = false;
                //}
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
			
        }

        protected override void OnAppearing()
        {
            if (vm.BuyingProduct.ShippingAddress != null)
            {
                vm.FormattedAdd = Global.AddressFormatter(vm.BuyingProduct.ShippingAddress);
            }
            base.OnAppearing();
        }

        void Cancel_Tapped(System.Object sender, System.EventArgs e)
        {
            UserDialogs.Instance.Alert("", "Coming soon", "OK");
            return;
            cancelResonPicker.Focus();
        }

        private async void cancelResonPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedReson = cancelResonPicker.SelectedItem as string;
        }

        async void cancelResonPicker_Unfocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            if (selectedReson != null)
            {
                var res = await Acr.UserDialogs.UserDialogs.Instance.ConfirmAsync("Do you want to cancel order?", "Cancel Order", "OK", "Cancel");
                if (res)
                {
                    vm.CancelReason = cancelResonPicker.SelectedItem as string;
                    if (vm.CancelReason != null)
                    {
                        await vm.CancelOrderMethod();
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

        private void SelectItem_Tapped(object sender, EventArgs e)
        {
            try
            {
                var selectedProduct = ((BuyingSellingOrderItem)((TappedEventArgs)e).Parameter).Product;
                if(selectedProduct != null)
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

