using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BuySell.Helper;
using BuySell.Model;
using BuySell.ViewModel;
using Xamarin.Forms;

namespace BuySell.Views
{
    public partial class TopTrendingViewAllPage : ContentPage
    {
        TopTrendingViewAllViewModel vm;
        public TopTrendingViewAllPage(List<DashBoardModel> dashBoardModels)
        {
            InitializeComponent();
            BindingContext = filterView.BindingContext = vm = new TopTrendingViewAllViewModel(this.Navigation);
            vm.ProductList = new System.Collections.ObjectModel.ObservableCollection<DashBoardModel>(dashBoardModels);
            //To set the store as per dashboard
            Global.SearchedResultSelectedStore = Global.Storecategory;
            vm.PropertyChanged += Vm_PropertyChanged;
            SetupMessagingCenter();
        }
        public TopTrendingViewAllPage(string genderType, bool IsAll)
        {
            InitializeComponent();
            BindingContext = filterView.BindingContext = vm = new TopTrendingViewAllViewModel(this.Navigation);
            //To set the store as per dashboard
            Global.SearchedResultSelectedStore = Global.Storecategory;
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
            vm.IsTap = false;
            MessagingCenter.Send<object>(Constant.UpdateThemeStr, Constant.UpdateThemeStr);
            MessagingCenter.Send<object>(Constant.UpdateHeaderThemeStr, Constant.UpdateHeaderThemeStr);
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
        void TopTrandingItemsList_Scrolled(System.Object sender, Xamarin.Forms.ItemsViewScrolledEventArgs e)
        {
            if (!vm.IsBusy)
            {
                if (e.VerticalDelta > 0)
                {
                    if (e.LastVisibleItemIndex == vm.ProductList.Count - 1)
                    {
                        vm.LoadMoreCommand.Execute(null);
                    }
                }
            }
        }
        void btnPrevious_Tapped(System.Object sender, System.EventArgs e)
        {
            try
            {
                vm.PageNumber = vm.productPagingListResponse.Data.PreviousPageNumber;

                Device.InvokeOnMainThreadAsync(async () =>
                {
                    await vm.GetTopTrendingItemsListMethod();
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        void btnNext_Tapped(System.Object sender, System.EventArgs e)
        {
            try
            {
                if(vm!=null)
                {
                    vm.PageNumber = vm.productPagingListResponse.Data.NextPageNumber;

                    Device.InvokeOnMainThreadAsync(async () =>
                    {
                        await vm.GetTopTrendingItemsListMethod();
                    });
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            Global.TileWidth = (width-40) / 2;
        }
        /// <summary>
        /// To received the filter list
        /// </summary>
        private void SetupMessagingCenter()
        {
            try
            {
                MessagingCenter.Subscribe<object, List<FilterModel>>("FilterList", "FilterList", async (sender, arg) =>
                {
                    if (arg != null)
                    {
                        vm.IsFilterChanged = true;
                        vm.productFilterModel = null;
                        vm.ProductList.Clear();
                        vm.PageNumber = 1;
                        Acr.UserDialogs.UserDialogs.Instance.ShowLoading();
                        await Task.Delay(100);
                        await Task.Run(() =>
                        {
                            vm.SetProductFilterRequest(arg);
                        });

                        //var allCount = arg.Where(f => f.Value.ToLower() == "all").ToList().Count;
                        //var checkCat = arg.Where(c => c.Value.Contains("|")).ToList().Count;
                        //if (allCount == 9 && checkCat == 0)
                        //{
                        //    vm.IsFilterChanged = true;
                        //    vm.productFilterModel = null;
                        //    vm.ProductList.Clear();
                        //    vm.PageNumber = 1;
                        //    vm.ItemTreshold = 1;
                        //    Device.BeginInvokeOnMainThread( () =>
                        //    {
                        //        vm.GetTopTrendingItemsListMethod();
                        //    });
                        //}
                        //else
                        //{
                        //    vm.IsFilterChanged = true;
                        //    vm.productFilterModel = null;
                        //    vm.ProductList.Clear();
                        //    vm.PageNumber = 1;
                        //    Acr.UserDialogs.UserDialogs.Instance.ShowLoading();
                        //    await Task.Delay(100);
                        //    await Task.Run(() =>
                        //    {
                        //        vm.SetProductFilterRequest(arg);
                        //    });
                        //}
                    }
                });
            }
            catch (Exception ex)
            {

            }
        }
    }
}
