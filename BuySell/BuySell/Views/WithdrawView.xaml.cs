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
    public partial class WithdrawView : ContentPage
    {
        WithDrawViewModel vm;
        public WithdrawView()
        {
            InitializeComponent();
            BindingContext = vm = new WithDrawViewModel(this.Navigation);
        }

        protected override void OnAppearing()
        {
            vm.IsTap = false;
            base.OnAppearing();
        }
    }
}