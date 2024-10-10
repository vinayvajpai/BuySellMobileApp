using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BuySell.Helper;
using BuySell.Model;
using BuySell.ViewModel;
using Xamarin.Forms;

namespace BuySell.Views
{
    public partial class ProductListByCategoryView : ContentPage
    {
        ProductListByCategoryViewModel vm;

        #region Constructors
        public ProductListByCategoryView(string category, List<DashBoardModel> dashBoardModels)
        {
            InitializeComponent();
            BindingContext = filterView.BindingContext = vm = new ProductListByCategoryViewModel(this.Navigation, category.ToUpper(), dashBoardModels);
            titleControle.TitleText = category.ToUpper();
            vm.PropertyChanged += Vm_PropertyChanged;
            SetupMessagingCenter();
        }
        public ProductListByCategoryView(string category, string categoryID, List<DashBoardModel> dashBoardModels)
        {
            InitializeComponent();
            BindingContext = filterView.BindingContext = vm = new ProductListByCategoryViewModel(this.Navigation, category.ToUpper(), categoryID, dashBoardModels);
            titleControle.TitleText = category.ToUpper();
            vm.PropertyChanged += Vm_PropertyChanged;
            SetupMessagingCenter();
        }
        #endregion

        #region Methods
        protected override void OnAppearing()
        {
            vm.IsTap = false;
            base.OnAppearing();
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            vm.IsTap = false;
            //Global.Subcategory = null;
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
        void btnPrevious_Tapped(System.Object sender, System.EventArgs e)
        {
            try
            {
                vm.PageNumber = vm.productPagingListResponse.Data.PreviousPageNumber;

                Device.InvokeOnMainThreadAsync(async () =>
                {
                    await vm.CallGetProductListByCategoryMethod();
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
                if (vm != null)
                {
                    vm.PageNumber = vm.productPagingListResponse.Data.NextPageNumber;

                    Device.InvokeOnMainThreadAsync(async () =>
                    {
                        await vm.CallGetProductListByCategoryMethod();
                    });
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
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
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            Global.TileWidth = (width - 40) / 2;
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
                        int checkCatSplit = 0;
                        var allCount = arg.Where(f => f.Value.ToLower() == "all").ToList().Count;
                        var checkCat = arg.Where(c => c.KEY.ToLower() == Constant.CategoryStr.ToLower()).FirstOrDefault();
                        if (checkCat != null)
                        {
                            var checkCatCount = checkCat.Value.Contains("|");
                           if(checkCatCount == true)
                            {
                                checkCatSplit = checkCat.Value.Split('|').Length;
                            }
                        }
                        if (allCount == 8 && checkCatSplit <= 2)
                        {
                            vm.IsFilterChanged = true;
                            vm.productFilterModel = null;
                            vm.ProductList.Clear();
                            vm.PageNumber = 1;
                            vm.ItemTreshold = 1;
                            Device.StartTimer(TimeSpan.FromMilliseconds(100), () =>
                            {
                                vm.CallGetProductListByCategoryMethod();
                                return false;
                            });
                        }
                        else
                        {
                            vm.IsFilterChanged = true;
                            vm.productFilterModel = null;
                            vm.ProductList.Clear();
                            vm.PageNumber = 1;
                            Acr.UserDialogs.UserDialogs.Instance.ShowLoading();
                            await Task.Delay(100);
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                vm.SetProductFilterRequest(arg);
                            });
                        }
                    }
                });
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
    }
}
