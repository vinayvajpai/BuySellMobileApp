using BuySell.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static BuySell.Model.CategoryModel;

namespace BuySell.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BrandPage : ContentPage
    {
        BrandViewModel vm;
        public BrandPage()
        {
            InitializeComponent();
            BindingContext = BindingContext = vm = new BrandViewModel(this.Navigation);
        }
        public BrandPage(List<string> brand)
        {
            InitializeComponent();
            BindingContext = BindingContext = vm = new BrandViewModel(this.Navigation, brand);
        }
        protected override void OnAppearing()
        {
            vm.IsTap = false;
            searchTxt.Focus();
            base.OnAppearing();
        }

        List<string> BrandList = new List<string>
        {
           "Adidas", "Coogi", "Nike", "Reebok", "Supreme", "Wrogen", "Wrangler"
        };
        private void searchTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(searchTxt.Text))
            {
                var brandSearched = BrandList.Where(b => b.ToLower().Contains(searchTxt.Text.ToLower()));
                brandListView.ItemsSource = brandSearched;
            }
            else
            {
                brandListView.ItemsSource = null;
            }
        }
        private void brandListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                if (vm.BrandList == null)
                {
                    return;
                }
                else
                {
                    searchTxt.Text = ((e.SelectedItem as BrandModel)).BrandName;
                    var brandName = searchTxt.Text;
                    MessagingCenter.Send<object, string>("SelectPropertyValue", "SelectPropertyValue", brandName.ToString());
                    MessagingCenter.Send<object, bool>("IsTapChangedFilter", "IsTapChangedFilter", false);
                    Navigation.PopAsync();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}