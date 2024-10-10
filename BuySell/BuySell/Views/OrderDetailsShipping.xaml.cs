using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuySell.Model;
using BuySell.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BuySell.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderDetailsShipping : ContentPage
    {
        OrderDetailsShippingViewModel vm;

        public OrderDetailsShipping(ViewModel.BuyingSellingModel selectedOrderItem)
        {
            InitializeComponent();
            BindingContext = vm = new OrderDetailsShippingViewModel(this.Navigation, selectedOrderItem);
        }

        public OrderDetailsShipping(DashBoardModel prodDataModel, string OfferPrice)
        {
            InitializeComponent();
            BindingContext = vm = new OrderDetailsShippingViewModel(this.Navigation, prodDataModel,OfferPrice);
        }
    }
}