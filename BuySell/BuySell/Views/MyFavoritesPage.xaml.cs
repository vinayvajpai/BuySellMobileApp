using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuySell.Helper;
using BuySell.Model;
using BuySell.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BuySell.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyFavoritesPage : ContentPage
    {
        MyFavoritesViewModel vm;
        public MyFavoritesPage()
        {
            InitializeComponent();
            BindingContext = filterView.BindingContext = vm = new MyFavoritesViewModel(this.Navigation);
            vm.PropertyChanged += Vm_PropertyChanged;
            SetupMessagingCenter();
        }
        private void Vm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "sizeByProductList")
            {
                filterView.SizeList = vm.sizeByProductList.ToList();
            }
            if (e.PropertyName == "brandList")
            {
                filterView.BrandList = vm.brandList.ToList();
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (vm != null)
            {
                vm.IsTap = false;
            }
        }
        void imgLikeUnlike_Clicked(System.Object sender, System.EventArgs e)
        {
            var objProduct = (DashBoardModel)((TappedEventArgs)e).Parameter;
            if (objProduct.IsLike)
            {
                Task.Run(async () => {
                    await vm.GetProductLikeResponse(objProduct, 0);
                });
            }
            else
            {
                Task.Run(async () => { await vm.GetProductLikeResponse(objProduct, 1); });
            }
        }
        private void TopTrandingItemsList_Scrolled(object sender, ItemsViewScrolledEventArgs e)
        {
            if (vm != null)
            {
                vm.IsScrolledList = true;
                Device.StartTimer(TimeSpan.FromMilliseconds(500), () =>
                {
                    vm.IsScrolledList = false;
                    return false;
                });
            }

        }
        private void SetupMessagingCenter()
        {
            try
            {
                MessagingCenter.Subscribe<object, List<FilterModel>>("FilterList", "FilterList", async (sender, arg) =>
                {
                    if (arg != null)
                    {
                        var allCount = arg.Where(f => f.Value.ToLower() == "all").ToList().Count;
                        if (allCount == 9)
                        {
                            vm.IsFilterChanged = true;
                            vm.productFilterModel = null;
                            vm.ProductList.Clear();
                            vm.PageNumber = 1;
                            vm.ItemTreshold = 1;
                            Acr.UserDialogs.UserDialogs.Instance.ShowLoading();
                            Device.StartTimer(TimeSpan.FromMilliseconds(100), () =>
                            {
                                vm.GetProductListWithPaginationMethod();
                                return false;
                            });
                        }
                        else
                        {
                            Acr.UserDialogs.UserDialogs.Instance.ShowLoading();
                            await Task.Delay(50);
                            await Task.Run(async () =>
                            {
                                vm.IsFilterChanged = true;
                                vm.productFilterModel = null;
                                vm.ProductList.Clear();
                                vm.PageNumber = 1;
                                await Task.Run(() =>
                                {
                                    vm.SetProductFilterRequest(arg);
                                });
                            });
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}