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
    public partial class MyEarningView : ContentPage
    {
        MyEarningViewModel vm;
        public MyEarningView(MyEarningsResponseModel earningresponseModel)
        {
            InitializeComponent();
            BindingContext = vm = new MyEarningViewModel(this.Navigation, earningresponseModel);
        }
        protected override void OnAppearing()
        {
            vm.IsTap = false;
            base.OnAppearing();
        }
    }
}