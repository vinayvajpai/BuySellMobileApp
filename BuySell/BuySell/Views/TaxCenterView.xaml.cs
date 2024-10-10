using BuySell.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BuySell.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaxCenterView : ContentPage
    {
        TaxCenterViewModel vm;
        public TaxCenterView()
        {
            InitializeComponent();
            BindingContext = vm = new TaxCenterViewModel(this.Navigation);

        }
        void Questions_Tapped(System.Object sender, System.EventArgs e)
        {
            try
            {
                if (vm.IsTap)
                    return;
                vm.IsTap = true;
                this.Navigation.PushAsync(new FAQView());
            }
            catch (Exception ex)
            {
                vm.IsTap = false;
                Debug.WriteLine(ex.Message);
            }

        }
    }
}