using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Acr.UserDialogs;
using BuySell.CustomControl;
using BuySell.Helper;
using BuySell.Model;
using BuySell.ViewModel;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace BuySell.Views
{
    public partial class SearchResultProductsView : ContentPage
    {
        SearchResultProdcutViewModel vm;
        CustomFilterViewModel customFilterView;
        string store;

        #region Constructors
        public SearchResultProductsView(string SearchText)
        {
            InitializeComponent();
            searchtext.Text = SearchText;
            BindingContext = filterView.BindingContext = vm = new SearchResultProdcutViewModel(this.Navigation, SearchText);
            vm.PropertyChanged += Vm_PropertyChanged;
            MessagingCenter.Subscribe<object, ThemesColor>(Constant.UpdateHeaderThemeForDetailStr, Constant.UpdateHeaderThemeForDetailStr, (obj, theme) =>
            {
                vm.ThemeColor = Global.GetThemeColor(theme);
                imgToolLogo.Source = Global.GetProductCatLogo(theme);
            });
        }
        public SearchResultProductsView(string SearchText, ProductPagingListResponse productListRes, string storeCount, string storeID)
        {
            InitializeComponent();
            store = storeID;
            searchtext.Text = SearchText;
            lblStoreCount.Text = storeCount;
            BindingContext = vm = new SearchResultProdcutViewModel(this.Navigation, SearchText, productListRes, storeID);
            vm.PropertyChanged += Vm_PropertyChanged;
            MessagingCenter.Subscribe<object, ThemesColor>(Constant.UpdateHeaderThemeForDetailStr, Constant.UpdateHeaderThemeForDetailStr, (obj, theme) =>
            {
                vm.ThemeColor = Global.GetThemeColor(theme);
                imgToolLogo.Source = Global.GetProductCatLogo(theme);
                filterView.ThemeColor = Color.FromHex(Global.GetThemeColor(theme));
            });
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                ShowHideFilters();
                return false;
            });
            SetupMessagingCenter();
        }
        public SearchResultProductsView(string SearchText, ProductPagingListResponse productListRes, string storeCount, string storeID, SearchResponseModel searchResponseModel)
        {
            InitializeComponent();
            store = storeID;
            searchtext.Text = SearchText;
            lblStoreCount.Text = storeCount;
            BindingContext = vm = new SearchResultProdcutViewModel(this.Navigation, SearchText, productListRes, storeID, searchResponseModel);
            vm.PropertyChanged += Vm_PropertyChanged;
            MessagingCenter.Subscribe<object, ThemesColor>(Constant.UpdateHeaderThemeForDetailStr, Constant.UpdateHeaderThemeForDetailStr, (obj, theme) =>
            {
                vm.ThemeColor = Global.GetThemeColor(theme);
                imgToolLogo.Source = Global.GetProductCatLogo(theme);
                filterView.ThemeColor = Color.FromHex(Global.GetThemeColor(theme));
            });
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
                {
                    ShowHideFilters();
                    return false;
                });
            SetupMessagingCenter();
        }
        #endregion

        protected override void OnAppearing()
        {

            var themeColorUsingIndex = Global.GetThemeColorUsingIndex(Convert.ToInt16(store));
            vm.ThemeColor = Global.GetThemeColor(themeColorUsingIndex);
            vm.SelectedIndexHeaderTab = Convert.ToInt16(themeColorUsingIndex);
            MessagingCenter.Send<object, ThemesColor>(Constant.UpdateThemeForDetailStr, Constant.UpdateThemeForDetailStr, themeColorUsingIndex);
            MessagingCenter.Send<object, ThemesColor>(Constant.UpdateHeaderThemeForDetailStr, Constant.UpdateHeaderThemeForDetailStr, themeColorUsingIndex);
            vm.IsTap = false;
            base.OnAppearing();
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
        private void Vm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SearchProductList")
            {
                ShowHideFilters();
            }
            if (e.PropertyName == "sizeByProductList")
            {
                filterView.SizeList = vm.sizeByProductList.ToList();
            }
            if (e.PropertyName == "brandList")
            {
                filterView.BrandList = vm.brandList.ToList();
            }
        }
        public async void Back_Clicked(System.Object sender, System.EventArgs e)
        {
            try
            {
                if (vm.IsTap)
                    return;
                vm.IsTap = true;
                MessagingCenter.Unsubscribe<object, List<FilterModel>>("FilterList", "FilterList");
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                vm.IsTap = false;
                Debug.WriteLine(ex.Message);
            }
        }
        void btnPrevious_Tapped(System.Object sender, System.EventArgs e)
        {
            try
            {
                vm.PageNumber = vm.productPagingListResponse.Data.PreviousPageNumber;

                Device.InvokeOnMainThreadAsync(async () =>
                {
                    await vm.CallGetSearchProductMethod();
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
                        await vm.CallGetSearchProductMethod();
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
                Task.Run(async () =>
                {
                    await vm.GetProductLikeResponse(objProduct, 0);
                });
            }
            else
            {
                Task.Run(async () => { await vm.GetProductLikeResponse(objProduct, 1); });
            }
        }
        void ShowHideFilters()
        {
            try
            {
                if (vm.SearchProductList.Count > 0)
                {
                    containerGrid.RowDefinitions[2].Height = GridLength.Auto;
                    filterView.IsVisible = true;
                }
                else
                {
                    containerGrid.RowDefinitions[2].Height = 0;
                    filterView.IsVisible = false;
                }
            }
            catch (Exception ex)
            { }
        }
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            Global.TileWidth = (width - 40) / 2;
        }
        private async void searchtext_Focused(object sender, FocusEventArgs e)
        {
            try
            {
                if (vm.IsTap)
                    return;
                vm.IsTap = true;

                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                vm.IsTap = false;
                Debug.WriteLine(ex.Message);
            }
        }
        void ListView_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                //Conditions added set Store type inside global  category to search result products view page
                vm.PageNumber = 1;
                var searchResultModel = ((SearchResultModel)e.SelectedItem);
                if (searchResultModel.Description.ToLower().Contains(Constant.ClothingStr.ToLower()))
                {
                    Global.SearchedResultSelectedStore = Constant.ClothingStr.ToLower();
                    getListData(searchtext.Text, vm.ClothingproductListResponses, searchResultModel.Description, searchResultModel.StoreID);
                }

                else if (searchResultModel.Description.ToLower().Contains(Constant.SneakersStr.ToLower()))
                {
                    Global.SearchedResultSelectedStore = Constant.SneakersStr.ToLower();
                    getListData(searchtext.Text, vm.SneekarproductListResponses, searchResultModel.Description, searchResultModel.StoreID);
                }

                else if (searchResultModel.Description.ToLower().Contains(Constant.StreetwearStr.ToLower()))
                {
                    Global.SearchedResultSelectedStore = Constant.StreetwearStr.ToLower();
                    getListData(searchtext.Text, vm.StreetproductListResponses, searchResultModel.Description, searchResultModel.StoreID);
                }

                else if (searchResultModel.Description.ToLower().Contains(Constant.VintageStr.ToLower()))
                {
                    Global.SearchedResultSelectedStore = Constant.VintageStr.ToLower();
                    getListData(searchtext.Text, vm.VintageproductListResponses, searchResultModel.Description, searchResultModel.StoreID);
                }
                lstProduct.SelectedItem = null;
                vm.count = 0;
            }
        }
        //method created to show selected item data on product list page
        public async void getListData(string SearchText, ProductPagingListResponse productListRes, string storeCount, string storeID)
        {
            store = storeID;
            vm.storeID = storeID;
            searchtext.Text = SearchText;
            lblStoreCount.Text = storeCount;
            //await vm.GetListData(this.Navigation, SearchText, productListRes, storeID);
            MessagingCenter.Subscribe<object, ThemesColor>(Constant.UpdateHeaderThemeForDetailStr, Constant.UpdateHeaderThemeForDetailStr, (obj, theme) =>
            {
                vm.ThemeColor = Global.GetThemeColor(theme);
                imgToolLogo.Source = Global.GetProductCatLogo(theme);
                filterView.ThemeColor = Color.FromHex(Global.GetThemeColor(theme));
            });
            UserDialogs.Instance.HideLoading();
            var themeColorUsingIndex = Global.GetThemeColorUsingIndex(Convert.ToInt16(store));
            vm.ThemeColor = Global.GetThemeColor(themeColorUsingIndex);
            vm.SelectedIndexHeaderTab = Convert.ToInt16(themeColorUsingIndex);
            MessagingCenter.Send<object, ThemesColor>(Constant.UpdateThemeForDetailStr, Constant.UpdateThemeForDetailStr, themeColorUsingIndex);
            MessagingCenter.Send<object, ThemesColor>(Constant.UpdateHeaderThemeForDetailStr, Constant.UpdateHeaderThemeForDetailStr, themeColorUsingIndex);
            vm.IsTap = false;

            //Condition for getting the search item if filter is not selected
            if (vm.productFilterModel == null)
            {
                await vm.GetListData(this.Navigation, SearchText, productListRes, storeID);
                Device.BeginInvokeOnMainThread(() =>
                {
                    ShowHideFilters();
                });
            }
            else
            {
                vm.PageNumber = 1;
                vm.SelectStoreCommand.Execute(null);
                vm.productFilterModel.StoreId = Convert.ToInt16(storeID);
                await vm.GetProductBySelectedFilterRequest();//Filter is selected
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
                            vm.SearchProductList.Clear();
                            vm.PageNumber = 1;
                            vm.ItemTreshold = 1;
                            //if (Device.RuntimePlatform == Device.iOS)
                            {
                                Device.StartTimer(TimeSpan.FromMilliseconds(100), () =>
                                {
                                    vm.CallGetSearchProductMethod();
                                    return false;
                                });
                            }
                        }
                        else
                        {
                            await Task.Run(async () =>
                            {
                                vm.IsFilterChanged = true;
                                vm.productFilterModel = null;
                                vm.SearchProductList.Clear();
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

            }
        }
    }
}
