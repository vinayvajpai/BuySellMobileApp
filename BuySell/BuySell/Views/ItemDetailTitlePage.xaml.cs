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
    public partial class ItemDetailTitlePage : ContentPage
    {
        ItemDetailTitleViewModel ViewModel;
        public ItemDetailTitlePage()
        {
            InitializeComponent();
            BindingContext = ViewModel =  new ItemDetailTitleViewModel(this.Navigation);
            ViewModel.navigation = this.Navigation;
        }

        async void Back_Tapped(object sender, EventArgs e)
        {
            try
            {
                if (ViewModel.IsTap)
                    return;
                ViewModel.IsTap = true;
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                ViewModel.IsTap = false;
                Debug.WriteLine(ex.Message);
            }
           
        }

        protected override void OnAppearing()
        {
            ViewModel.IsTap = false;
            base.OnAppearing();
        }
    }
}