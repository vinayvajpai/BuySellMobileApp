using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using BuySell.Helper;
using BuySell.Model;
using BuySell.Model.RestResponse;
using BuySell.Utility;
using BuySell.View;
using BuySell.WebServices;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace BuySell.ViewModel
{
    internal class MyFavoritesViewModel : BaseViewModel
    {
        #region Variables
        INavigation navigation;
        int GenderIndex = Global.GenderIndex;
        public DashBoardModel objData;
        public int PageNumber = 1;
        public ProductPagingListResponse productPagingListResponse;
        string genderType = "m";
        bool IsAll = false;
        public ProductFilterModel productFilterModel;
        #endregion

        #region Constructor
        public MyFavoritesViewModel(INavigation _nav) 
        {
            //var id = Constant.LoginUserData.Id;
            navigation = _nav;
            MockProductData.Instance.nav = navigation;
            ItemTreshold = 1;
            ItemTresholdReachedCommand = new Command(async () => await ItemsTresholdReached());
            if (Device.RuntimePlatform == Device.iOS)
            {
                Task.Run(async () =>
                {
                    await GetFavoriteItemsListMethod();
                });
            }
        }
        #endregion

        #region Properties
        private ObservableCollection<string> _sizeByProductList = new ObservableCollection<string>();
        public ObservableCollection<string> sizeByProductList
        {
            get { return _sizeByProductList; }
            set { _sizeByProductList = value; OnPropertyChanged("sizeByProductList"); }
        }
        private ObservableCollection<string> _brandList = new ObservableCollection<string>();
        public ObservableCollection<string> brandList
        {
            get { return _brandList; }
            set { _brandList = value; OnPropertyChanged("brandList"); }
        }
        private bool _IsLoadMore = false;
        public bool IsLoadMore
        {
            get { return _IsLoadMore; }
            set { _IsLoadMore = value; OnPropertyChanged("IsLoadMore"); }
        }

        public int _PageSize = 20;
        public int PageSize
        {
            get
            { return _PageSize; }
            set
            { _PageSize = value; OnPropertyChanged("PageSize"); }
        }

        private int _itemTreshold;
        public int ItemTreshold
        {
            get { return _itemTreshold; }
            set { SetProperty(ref _itemTreshold, value); }
        }

        private ObservableCollection<DashBoardModel> _ProductList = new ObservableCollection<DashBoardModel>();
        public ObservableCollection<DashBoardModel> ProductList
        {
            get { return _ProductList; }
            set { _ProductList = value; OnPropertyChanged("ProductList"); }
        }

        private ObservableCollection<DashBoardModel> _AllProductList = new ObservableCollection<DashBoardModel>();
        public ObservableCollection<DashBoardModel> AllProductList
        {
            get { return _AllProductList; }
            set { _AllProductList = value; OnPropertyChanged("AllProductList"); }
        }

        private bool _IsShowMore = false;
        public bool IsShowMore
        {
            get { return _IsShowMore; }
            set { _IsShowMore = value; OnPropertyChanged("IsShowMore"); }
        }

        private string _ThemeCol = Global.GetThemeColor(Global.setThemeColor);
        public string ThemeCol
        {
            get { return _ThemeCol; }
            set { _ThemeCol = value; OnPropertyChanged("ThemeCol"); }
        }

        private bool _ShowPreviousButton = false;
        public bool ShowPreviousButton
        {
            get { return _ShowPreviousButton; }
            set { _ShowPreviousButton = value; OnPropertyChanged("ShowPreviousButton"); }
        }

        private bool _ShowNextButton = false;
        public bool ShowNextButton
        {
            get { return _ShowNextButton; }
            set { _ShowNextButton = value; OnPropertyChanged("ShowNextButton"); }
        }

        private bool _ShowPreNext = false;
        public bool ShowPreNext
        {
            get { return _ShowPreNext; }
            set { _ShowPreNext = value; OnPropertyChanged("ShowPreNext"); }
        }

        public bool _IsFilterChanged = true;
        public bool IsFilterChanged
        {
            get
            {
                return _IsFilterChanged;
            }
            set
            {
                _IsFilterChanged = value;
                OnPropertyChanged("IsFilterChanged");
            }
        }

        public bool _IsScrolledList = false;
        public bool IsScrolledList
        {
            get
            {
                return _IsScrolledList;
            }
            set
            {
                _IsScrolledList = value;
                OnPropertyChanged("IsScrolledList");
            }
        }

        #endregion

        #region Commands
        public Command ItemTresholdReachedCommand { get; set; }

        private Command _MyFavoriteItemsCommand;
        public Command MyFavoriteItemsCommand
        {
            get
            {
                return _MyFavoriteItemsCommand ?? (_MyFavoriteItemsCommand = new Command(async (obj) => await ExicuteTopTrandingCommond(obj)));
            }
        }
        #endregion

        #region Methods
        private async Task ExicuteTopTrandingCommond(object obj)
        {
            try
            {
                if (IsTap)
                    return;
                IsTap = true;
                Global.SetRecentViewList((DashBoardModel)obj);
                await navigation.PushAsync(new ItemDetailsPage((DashBoardModel)obj, true));
            }
            catch (Exception ex)
            {
                IsTap = false;
                Debug.WriteLine(ex.Message);
            }
        }
        public async Task GetFavoriteItemsListMethod()
        {
            try
            {
                Acr.UserDialogs.UserDialogs.Instance.ShowLoading();
                await Task.Delay(100);
                await GetProductListWithPaginationMethod();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        public async Task GetProductListWithPaginationMethod()
        {
            try
            {
                if (!Constant.IsConnected)
                {
                    UserDialogs.Instance.Toast("Internet not available");
                    IsTap = false;
                    return;
                }
                var obj = Global.Storecategory.ToLower();
                var obj1 = GenderIndex;
                Console.WriteLine("Calling paging");
                IsBusy = true;
                var req = new MyFavoritesModel();
                req.PageNumber = PageNumber;
                req.PageSize = PageSize;
                req.LoggedUserId = Constant.LoginUserData.Id;
                string methodUrl;
                methodUrl = "/api/Product/GetAllUserFavouriteProducts";
                RestResponseModel responseModel = await WebService.PostData(req, methodUrl, true);
                if (responseModel != null)
                {
                    var productListResponses = JsonConvert.DeserializeObject<ProductPagingListResponse>(responseModel.response_body);
                    if (responseModel.status_code == 200)
                    {
                        if (productListResponses != null)
                        {
                            if (productListResponses.StatusCode == 200)
                            {
                                if (productListResponses.Data != null)
                                {
                                    productPagingListResponse = productListResponses;
                                    
                                    IsBusy = false;
                                    foreach (var item in productListResponses.Data.Data)
                                    {
                                        ProductList.Add(item);
                                    }
                                    if (productListResponses.Data.Data.Count() == 0)
                                    {
                                        ItemTreshold = -1;
                                        //return;
                                    }
                                    if (ProductList.Count > 0)
                                    {
                                        if ((PageNumber * PageSize) < productListResponses.Data.TotalRowCount)
                                            TotalCount = "Showing 1 to " + (PageNumber * PageSize) + " of " + productListResponses.Data.TotalRowCount + " Items";
                                        else
                                            TotalCount = "Showing 1 to " + productListResponses.Data.TotalRowCount + " of " + productListResponses.Data.TotalRowCount + " Items";

                                    }
                                    PageNumber = productPagingListResponse.Data.NextPageNumber;
                                    sizeByProductList = new ObservableCollection<string>(ProductList.Select(s => s.Size).Distinct().ToList().ToList().OrderBy(x => x));
                                    brandList = new ObservableCollection<string>(ProductList.Select(s => s.Brand).Distinct().ToList().ToList().OrderBy(x => x));
                                    IsLoadMore = false;
                                }
                                else
                                {
                                    Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                                    IsBusy = false;
                                    IsLoadMore = false;
                                }
                            }
                            else
                            {
                                Acr.UserDialogs.UserDialogs.Instance.Alert(productListResponses.Message);
                                IsBusy = false;
                                IsLoadMore = false;
                            }
                        }
                        else
                        {
                            Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                            IsBusy = false;
                            IsLoadMore = false;
                        }
                    }
                    else if (responseModel.status_code == 400)
                    {
                        if (!string.IsNullOrEmpty(responseModel.ErrorMessage))
                        {
                            Acr.UserDialogs.UserDialogs.Instance.Alert(responseModel.ErrorMessage);
                            UserDialogs.Instance.HideLoading();
                        }
                        else
                        {
                            Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                            UserDialogs.Instance.HideLoading();
                        }
                    }
                    else if (responseModel.status_code == 500)
                    {
                        ResponseBodyModel responseBodyModel = JsonConvert.DeserializeObject<ResponseBodyModel>(responseModel.response_body);
                        IsTap = false;
                        Acr.UserDialogs.UserDialogs.Instance.Alert(responseBodyModel.Message);
                        UserDialogs.Instance.HideLoading();
                    }
                    else
                    {
                        Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                        UserDialogs.Instance.HideLoading();
                    }
                }
                else
                {
                    Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                    IsBusy = false;
                    IsLoadMore = false;
                }
            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Alert(ex.Message);
                Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                IsLoadMore = false;
            }
            finally
            {
                IsFilterChanged = false;
                Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                if (ProductList.Count > 0)
                {
                    IsShowFilter = true;
                    IsNoData = false;
                }
                else
                {
                    IsShowFilter = false;
                    IsNoData = true;
                }
            }

        }
        async Task ItemsTresholdReached()
        {
            if (ProductList.Count == 0 && IsFilterChanged == true && IsScrolledList==false)
            {
                return;
            }

            if (!Constant.IsConnected)
            {
                UserDialogs.Instance.Toast("Internet not available");
                IsTap = false;
                return;
            }
            if (productPagingListResponse != null)
            {
                if (productPagingListResponse.Data.HasNextPage == false)
                {
                    IsLoadMore = false;
                    IsBusy = false;
                    return;
                }
            }
            if (IsLoadMore)
                return;

            await Task.Run(async () => {
                IsLoadMore = true;
                await Task.Delay(50);
                try
                {
                    if (productFilterModel == null)
                    {
                        await GetProductListWithPaginationMethod();
                    }
                    else
                    {
                        productFilterModel.PageNumber = PageNumber;
                        await GetProductByFilterMethod(productFilterModel);
                    }
                    //await GetProductListWithPaginationMethod();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                }
                finally
                {
                    IsBusy = false;
                    Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                }
            });
        }
        private async Task GetProductByFilterMethod(ProductFilterModel productFilterModel)
        {
            int prevPageNumber = 0;
            
            try
            {
                if (!Constant.IsConnected)
                {
                    UserDialogs.Instance.Toast("Internet not available");
                    IsTap = false;
                    return;
                }
                prevPageNumber = PageNumber;
                productFilterModel.PageNumber = PageNumber;
                string methodUrl = "/api/Product/GetAllUserFavouriteFilterProducts";
                RestResponseModel responseModel = await WebService.PostData(productFilterModel, methodUrl, true);
                if (responseModel != null)
                {
                    if (responseModel.status_code == 200)
                    {
                        var productListResponses = JsonConvert.DeserializeObject<ProductPagingListResponse>(responseModel.response_body);
                        if (productListResponses != null)
                        {
                            if (productListResponses.StatusCode == 200)
                            {
                                if (productListResponses.Data != null)
                                {
                                    //ProductList.Clear();
                                    productPagingListResponse = productListResponses;
                                    IsBusy = false;
                                    foreach (var item in productListResponses.Data.Data)
                                    {
                                        ProductList.Add(item);
                                    }
                                    if (productListResponses.Data.Data.Count() == 0)
                                    {
                                        ItemTreshold = -1;
                                    }
                                    if (ProductList.Count > 0)
                                    {
                                        if ((PageNumber * PageSize) < productListResponses.Data.TotalRowCount)
                                            TotalCount = "Showing 1 to " + (PageNumber * PageSize) + " of " + productListResponses.Data.TotalRowCount + " Items";
                                        else
                                            TotalCount = "Showing 1 to " + productListResponses.Data.TotalRowCount + " of " + productListResponses.Data.TotalRowCount + " Items";

                                    }
                                    PageNumber = productPagingListResponse.Data.NextPageNumber;
                                    sizeByProductList = new ObservableCollection<string>(ProductList.Select(s => s.Size).Distinct().ToList().ToList().OrderBy(x => x));
                                    brandList = new ObservableCollection<string>(ProductList.Select(s => s.Brand).Distinct().ToList().ToList().OrderBy(x => x));
                                    IsLoadMore = false;
                                }
                                else
                                {
                                    Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                                    UserDialogs.Instance.HideLoading();
                                    IsLoadMore = false;
                                }
                            }
                            else
                            {
                                Acr.UserDialogs.UserDialogs.Instance.Alert(productListResponses.Message);
                                UserDialogs.Instance.HideLoading();
                                IsLoadMore = false;
                            }
                        }
                        else
                        {
                            Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                            UserDialogs.Instance.HideLoading();
                            IsLoadMore = false;
                            PageNumber = prevPageNumber;
                        }
                    }
                    else if (responseModel.status_code == 400)
                    {
                        if (!string.IsNullOrEmpty(responseModel.ErrorMessage))
                        {
                            Acr.UserDialogs.UserDialogs.Instance.Alert(responseModel.ErrorMessage);
                            UserDialogs.Instance.HideLoading();
                        }
                        else
                        {
                            Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                            UserDialogs.Instance.HideLoading();
                        }
                        PageNumber = prevPageNumber;
                    }
                    else if (responseModel.status_code == 500)
                    {
                        ResponseBodyModel responseBodyModel = JsonConvert.DeserializeObject<ResponseBodyModel>(responseModel.response_body);
                        IsTap = false;
                        Acr.UserDialogs.UserDialogs.Instance.Alert(responseBodyModel.Message);
                        UserDialogs.Instance.HideLoading();
                    }
                    else
                    {
                        Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                        UserDialogs.Instance.HideLoading();
                        PageNumber = prevPageNumber;
                    }
                }
                else
                {
                    Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                    UserDialogs.Instance.HideLoading();
                    IsLoadMore = false;
                    PageNumber = prevPageNumber;
                }
            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Alert(ex.Message);
                UserDialogs.Instance.HideLoading();
                IsLoadMore = false;
                PageNumber = prevPageNumber;

            }
            finally
            {
                IsFilterChanged = false;
                Acr.UserDialogs.UserDialogs.Instance.HideLoading();
            }
            IsNoData = ProductList.Count > 0 ? false : true;
        }
        public async void SetProductFilterRequest(List<FilterModel> listFilter)
        {
            try
            {
                productFilterModel = new ProductFilterModel();
                productFilterModel.StoreId = null;
                productFilterModel.Search = string.Empty;
                productFilterModel.PageSize = PageSize;
                productFilterModel.PageNumber = PageNumber;
                productFilterModel.Price = new PriceFilter();
                productFilterModel.ShippingPrice = new PriceFilter();
                productFilterModel.Sort = new SortingFilter();
                productFilterModel.LoggedUserId = Constant.LoginUserData.Id;
                productFilterModel.UserId = null;
                var selectedPrice = listFilter.Where(f => f.KEY == Constant.PriceStr).FirstOrDefault().Value;
                if (selectedPrice.Contains(Constant.UnderStr))
                {
                    productFilterModel.Price.IsApply = true;
                    productFilterModel.Price.FromPrice = Convert.ToDecimal(selectedPrice.Replace(Constant.UnderStr, "").Replace(" ", "").Replace("$", ""));
                    productFilterModel.Price.ToPrice = Convert.ToDecimal(selectedPrice.Replace(Constant.UnderStr, "").Replace(" ", "").Replace("$", ""));
                    productFilterModel.Price.PriceCompareOperator = PriceCompareFilter.LessThan;
                }
                else if (selectedPrice.Contains(Constant.OverStr))
                {
                    productFilterModel.Price.IsApply = true;
                    productFilterModel.Price.FromPrice = Convert.ToDecimal(selectedPrice.Replace(Constant.OverStr, "").Replace(" ", "").Replace("$", ""));
                    productFilterModel.Price.ToPrice = Convert.ToDecimal(selectedPrice.Replace(Constant.OverStr, "").Replace(" ", "").Replace("$", ""));
                    productFilterModel.Price.PriceCompareOperator = PriceCompareFilter.GreaterThan;
                }
                else
                {
                    var priceList = selectedPrice.Trim().Split('-');
                    if (priceList.Length > 1)
                    {
                        productFilterModel.Price.IsApply = true;
                        productFilterModel.Price.FromPrice = Convert.ToDecimal(priceList[0].Replace(" ", "").Replace("$", ""));
                        productFilterModel.Price.ToPrice = Convert.ToDecimal(priceList[1].Replace(" ", "").Replace("$", ""));
                        productFilterModel.Price.PriceCompareOperator = PriceCompareFilter.Between;
                    }
                }
                var selectedShipPrice = listFilter.Where(f => f.KEY == Constant.ShippingPriceStr).FirstOrDefault().Value;
                if (selectedShipPrice != null)
                {
                    if (selectedShipPrice.ToLower().Equals(Constant.AllStr.ToLower()))
                    {

                    }
                    else
                    {
                        productFilterModel.ShippingPrice.IsApply = true;
                        productFilterModel.ShippingPrice.FromPrice = 0;
                        productFilterModel.ShippingPrice.ToPrice = Convert.ToDecimal(selectedShipPrice.Replace(" ", "").Replace("$", ""));
                        productFilterModel.ShippingPrice.PriceCompareOperator = PriceCompareFilter.Between;
                    }
                }
                var selectedSort = listFilter.Where(f => f.KEY == Constant.SortStr).FirstOrDefault().Value;
                if (selectedSort.Contains(Constant.LowToHighStr))
                {
                    productFilterModel.Sort.IsApply = true;
                    productFilterModel.Sort.Sort = SortCompareFilter.PriceLowToHigh;
                }
                else if (selectedSort.Contains(Constant.HighToLowStr))
                {
                    productFilterModel.Sort.IsApply = true;
                    productFilterModel.Sort.Sort = SortCompareFilter.PriceHighToLow;
                }
                else if (selectedSort.ToLower().Contains(Constant.AllStr.ToLower()))
                {

                }
                else if (selectedSort.Contains(Constant.NewListingStr))
                {
                    productFilterModel.Sort.IsApply = true;
                    productFilterModel.Sort.Sort = SortCompareFilter.NewList;
                }
                var Availability = listFilter.Where(f => f.KEY == Constant.AvailabilityStr).FirstOrDefault().Value;
                if (Availability.Trim().ToLower().Equals(Constant.AllStr.ToLower()))
                {
                    productFilterModel.Availability = new List<string>();
                }
                else
                {
                    productFilterModel.Availability = new List<string>(){
                        Availability
                    };
                }

                var Condition = listFilter.Where(f => f.KEY == Constant.ConditionStr).FirstOrDefault().Value;
                if (Condition.Trim().ToLower().Equals(Constant.AllStr.ToLower()))
                {
                    productFilterModel.Condition = new List<string>();
                }
                else
                {
                    productFilterModel.Condition = new List<string>(){
                        Condition
                    };
                }

                var Colour = listFilter.Where(f => f.KEY == Constant.ColorStr).FirstOrDefault().Value;
                if (Colour.Trim().ToLower().Equals(Constant.AllStr.ToLower()))
                {
                    productFilterModel.Color = new List<string>();
                }
                else
                {
                    productFilterModel.Color = new List<string>(){
                        Colour
                    };
                }

                var Brand = listFilter.Where(f => f.KEY == Constant.BrandStr).FirstOrDefault().Value;
                if (Brand.Trim().ToLower().Equals(Constant.AllStr.ToLower()))
                {
                    productFilterModel.Brand = new List<string>();
                }
                else
                {
                    productFilterModel.Brand = new List<string>(){
                        Brand
                    };
                }

                var Size = listFilter.Where(f => f.KEY == Constant.SizeStr).FirstOrDefault().Value;
                if (Size.Trim().ToLower().Equals(Constant.AllStr.ToLower()))
                {
                    productFilterModel.Size = new List<string>();
                }
                else
                {
                    productFilterModel.Size = new List<string>(){
                       //(Size.ToLower().Contains("one size"))?"OS":Size
                       Size
                    };
                }

                var Categories = listFilter.Where(f => f.KEY == Constant.CategoryStr).FirstOrDefault().Value;
                if (Categories.ToLower().Trim().Equals(Constant.AllStr.ToLower()))
                {
                    productFilterModel.Categories = new List<string>();
                    productFilterModel.RootCategories = new List<int> { 1, 2 };
                }
                else
                {
                    var filterCat = Categories.Split('|');
                    if (filterCat.Length > 0)
                    {
                        if (filterCat[0].ToLower().Contains("all"))
                        {
                            if (filterCat.Length < 3 && filterCat.Length > 1)
                            {
                                productFilterModel.RootCategories = new List<int> { 1, 2 };
                                productFilterModel.Categories = new List<string>() {
                                filterCat[1].Trim()};
                            }
                            else
                            {

                                productFilterModel.RootCategories = new List<int> { 1, 2 };
                                productFilterModel.Categories = new List<string>() {
                                filterCat[1].Trim(),
                                filterCat[2].Trim()};
                            }
                        }
                       else if (filterCat.Length < 3 && filterCat.Length > 1)
                        {
                            productFilterModel.RootCategories = new List<int> { filterCat[0].Trim().ToLower() == "men" ? 1 : 2 };
                            productFilterModel.Categories = new List<string>() {
                                filterCat[1].Trim()
                            };

                        }
                        else if (filterCat.Length > 2)
                        {
                            productFilterModel.RootCategories = new List<int> { filterCat[0].Trim().ToLower() == "men" ? 1 : 2 };
                            productFilterModel.Categories = new List<string>() {
                                filterCat[1].Trim(),
                                filterCat[2].Trim()
                            };
                        }
                        else if (filterCat.Length == 1)
                        {
                            productFilterModel.RootCategories = new List<int> { filterCat[0].Trim().ToLower() == "men" ? 1 : 2 };
                        }
                    }
                }

                //var Gender = listFilter.Where(f => f.KEY == Constant.GenderStr).FirstOrDefault().Value;
                //if (Gender.Contains("Men") || Gender.Contains("Women"))
                //{
                //    productFilterModel.RootCategories = new List<int> { Gender.ToLower() == "men" ? 1 : 2 };
                //}
                //else if (Gender.ToLower().Contains(Constant.AllStr.ToLower()))
                //{
                //    productFilterModel.RootCategories = new List<int> { 1, 2 };
                //}

                PageNumber = 1;
                ProductList.Clear();
                await GetProductByFilterMethod(productFilterModel);
            }
            catch (Exception ex)
            {
            }
        }
        #endregion
    }
}
