using System;
using System.Collections.Generic;
using System.Diagnostics;
using BuySell.Helper;
using BuySell.View;
using BuySell.ViewModel;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace BuySell.Views.BuyingSellingViews
{	
	public partial class OrderDisputeView : ContentPage
	{	
		OrderDisputeViewModel viewModel;
		public OrderDisputeView (BuyingSellingModel buyingSellingModel)
		{
			InitializeComponent ();
			BindingContext = viewModel =  new OrderDisputeViewModel(this.Navigation, buyingSellingModel);
            viewModel.ShowToBuyer = Global.BuyingPageTitle.ToLower() == "buying" ? true : false;
        }

        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
			this.Navigation.PopAsync();
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            try
            {
                if (viewModel.SelectedOrder != null)
                {
                    var item = JsonConvert.SerializeObject(viewModel.SelectedOrder);
                    viewModel.navigation.PushAsync(new ItemDetailsPage(JsonConvert.DeserializeObject<Model.DashBoardModel>(item), true));
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}

