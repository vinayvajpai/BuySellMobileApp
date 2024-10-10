using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
using BuySell.Helper;
using BuySell.Model;
using BuySell.Model.RestResponse;
using BuySell.Persistence;
using BuySell.Utility;
using BuySell.View;
using BuySell.Views;
using BuySell.WebServices;
using Newtonsoft.Json;
using SQLite;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BuySell.ViewModel
{
    public class DashBoardViewModel : BaseViewModel
    {

        MockProductData mockProductData;
        int GenderIndex = Global.GenderIndex = 1;
        public int SelectedStoreIndex = 1;
        int PageNumber = 1;
        int PageSize = 20;
        ProductPagingListResponse productPagingListResponse;
        #region Constructor
        public DashBoardViewModel(INavigation _nav)
        {
            mockProductData = new MockProductData();
            navigation = _nav;
            //_connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            // _connection.CreateTableAsync<RecentViewModel>();

        }
        #endregion

        #region Properties

        private ObservableCollection<DashBoardModel> _ProductList = new ObservableCollection<DashBoardModel>();
        public ObservableCollection<DashBoardModel> ProductList
        {
            get { return _ProductList; }
            set { _ProductList = value; OnPropertyChanged("ProductList"); }
        }

        private ObservableCollection<DashBoardModel> _RecentProductList = new ObservableCollection<DashBoardModel>();
        public ObservableCollection<DashBoardModel> RecentProductList
        {
            get { return _RecentProductList; }
            set { _RecentProductList = value; OnPropertyChanged("RecentProductList"); }
        }

        private ObservableCollection<DashBoardModel> _AllProductList = new ObservableCollection<DashBoardModel>();
        public ObservableCollection<DashBoardModel> AllProductList
        {
            get { return _AllProductList; }
            set { _AllProductList = value; OnPropertyChanged("AllProductList"); }
        }

        private ObservableCollection<DashBoardModel> _AllRecentProductList = new ObservableCollection<DashBoardModel>();
        public ObservableCollection<DashBoardModel> AllRecentProductList
        {
            get { return _AllRecentProductList; }
            set { _AllRecentProductList = value; OnPropertyChanged("AllRecentProductList"); }
        }

        private ObservableCollection<BannerModel> _BannerList;
        public ObservableCollection<BannerModel> BannerList
        {
            get { return _BannerList; }
            set { _BannerList = value; OnPropertyChanged("BannerList"); }
        }

        private ObservableCollection<Category> _CategoryList;
        public ObservableCollection<Category> CategoryList
        {
            get { return _CategoryList; }
            set { _CategoryList = value; OnPropertyChanged("CategoryList"); }
        }

        private ObservableCollection<BannerModel> _filterBannerList;
        public ObservableCollection<BannerModel> filterBannerList
        {
            get { return _filterBannerList; }
            set { _filterBannerList = value; OnPropertyChanged("filterBannerList"); }
        }

        private ObservableCollection<ShopByCategoryModel> _ShopByCategoryList;
        public ObservableCollection<ShopByCategoryModel> ShopByCategoryList
        {
            get { return _ShopByCategoryList; }
            set { _ShopByCategoryList = value; OnPropertyChanged("ShopByCategoryList"); }
        }

        private bool _IsShowRecentProduct;
        public bool IsShowRecentProduct
        {
            get { return _IsShowRecentProduct; }
            set { _IsShowRecentProduct = value; OnPropertyChanged("IsShowRecentProduct"); }
        }
        private string _ThemeCol = Global.GetThemeColor(Global.setThemeColor);
        public string ThemeCol
        {
            get { return _ThemeCol; }
            set { _ThemeCol = value; OnPropertyChanged("ThemeCol"); }

        }
        public string _ProductRating { get; set; }
        public string ProductRating
        {
            get { return _ProductRating; }
            set
            {
                _ProductRating = value;
                OnPropertyChanged("ProductRating");
            }

        }
        private Color _boxSepBackCol1;
        public Color boxSepBackCol1
        {
            get { return _boxSepBackCol1; }
            set
            {
                _boxSepBackCol1 = value;
                OnPropertyChanged("boxSepBackCol1");
            }

        }
        private Color _boxSepBackCol2;
        public Color boxSepBackCol2
        {
            get { return _boxSepBackCol2; }
            set
            {
                _boxSepBackCol2 = value;
                OnPropertyChanged("boxSepBackCol2");
            }

        }
        private Color _boxSepBackCol3;
        public Color boxSepBackCol3
        {
            get { return _boxSepBackCol3; }
            set
            {
                _boxSepBackCol3 = value;
                OnPropertyChanged("boxSepBackCol3");
            }

        }

        private bool _IsRefreshing;
        public bool IsRefreshing
        {
            get { return _IsRefreshing; }
            set { _IsRefreshing = value; OnPropertyChanged("IsRefreshing"); }
        }

        public int SeletedFilter = 1;

        #endregion

        #region Commands

        private Command _TopTrandingCommand;
        public Command TopTrandingCommand
        {
            get
            {
                return _TopTrandingCommand ?? (_TopTrandingCommand = new Command<DashBoardModel>(async (obj) => await ExicuteTopTrandingCommond(obj)));
            }
        }

        private Command _ProductByCategoryCommand;
        public Command ProductByCategoryCommand
        {
            get
            {
                return _ProductByCategoryCommand ?? (_ProductByCategoryCommand = new Command<string>(async (obj) =>
                {
                    try
                    {
                        if (IsTap)
                            return;
                        IsTap = true;

                        string categoryIndex = Global.GenderIndex.ToString();
                        if (categoryIndex == "3")
                            categoryIndex = null;
                        await navigation.PushAsync(new ProductListByCategoryView(obj, categoryIndex, AllProductList.ToList()));

                    }
                    catch (Exception ex)
                    {
                        IsTap = false;
                        Debug.WriteLine(ex.Message);
                    }
                }
                ));
            }
        }

        private Command _RecetViewsCommand;
        public Command RecetViewsCommand
        {
            get
            {
                return _RecetViewsCommand ?? (_RecetViewsCommand = new Command(async (obj) =>
                {
                    try
                    {
                        if (IsTap)
                        return;
                        IsTap = true;
                        await navigation.PushAsync(new ItemDetailsPage((DashBoardModel)obj, false));
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                        IsTap = true;
                    }
                }));
            }
        }

        private Command _SelectedTabCommand;
        public Command SelectedTabCommand
        {
            get
            {
                return _SelectedTabCommand ?? (_SelectedTabCommand = new Command(async (index) =>
                {
                    var param = Convert.ToInt16(index);
                    SetThemeColor(param);
                    SelectedStoreIndex = param;
                    Global.StoreIndex = param;
                    //BannerList = new ObservableCollection<BannerModel>(mockProductData.FilterBannerList(Global.GetStoreName(param), GenderIndex));
                    //OnPropertyChanged("BannerList");

                    //if (AllProductList.Count > 0)
                    //{
                    //    SetFilterSelector();
                    //}
                    //else
                    //{

                    //    await LoadProductData();
                    //}

                    SetFilterSelector();

                }
                ));
            }
        }


        private Command _RefreshCommand;
        public Command RefreshCommand
        {
            get
            {
                return _RefreshCommand ?? (_RefreshCommand = new Command(async (index) =>
                {
                    await GetBannerListMethod();
                    //await GetBannerProductListMethod();
                    //Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                }
                ));
            }
        }

        private Command _LoadMoreCommand;
        public Command LoadMoreCommand
        {
            get
            {
                return _LoadMoreCommand ?? (_LoadMoreCommand = new Command(async (index) =>
                {
                    if (productPagingListResponse != null)
                    {
                        if (productPagingListResponse.Data.HasNextPage)
                        {
                            IsBusy = true;
                            PageNumber = productPagingListResponse.Data.NextPageNumber;
                            //Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Loading");
                            await Task.Delay(10);
                            await GetProductListWithPaginationMethod();
                        }
                        //Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                    }
                }
                ));
            }
        }

        private Command _FilterCommand;
        public Command FilterCommand
        {
            get
            {
                return _FilterCommand ?? (_FilterCommand = new Command(async (obj) =>
                {

                    Acr.UserDialogs.UserDialogs.Instance.ShowLoading();
                    await Task.Delay(100);
                    if (filterBannerList != null)
                        filterBannerList.Clear();
                    int filterIndex = Convert.ToInt32(obj);
                    switch (filterIndex)
                    {
                        case 1:
                            {
                                RecentProductList = new ObservableCollection<DashBoardModel>(Global.GlobalRecentProductList.Where(p => p.Gender.ToLower().Equals("m")).OrderByDescending(p => p.RecentViewItemDate));
                                ProductList = new ObservableCollection<DashBoardModel>(ProductList.Where(p => p.Gender.ToLower().Equals("m")));
                                IsShowRecentProduct = RecentProductList.Count > 0 ? true : false;
                                ShopByCategoryList.Clear();
                                ShopByCategoryList = MockProductData.Instance.GetShopByCatList(SeletedFilter, SelectedIndexHeaderTab);
                                break;
                            }
                        case 2:
                            {
                                RecentProductList = new ObservableCollection<DashBoardModel>(Global.GlobalRecentProductList.Where(p => p.Gender.ToLower().Equals("f")).OrderByDescending(p => p.RecentViewItemDate));
                                ProductList = new ObservableCollection<DashBoardModel>(ProductList.Where(p => p.Gender.ToLower().Equals("f")));
                                IsShowRecentProduct = RecentProductList.Count > 0 ? true : false;
                                ShopByCategoryList.Clear();
                                ShopByCategoryList = MockProductData.Instance.GetShopByCatList(SeletedFilter, SelectedIndexHeaderTab);
                                break;
                            }
                        case 3:
                            {
                                RecentProductList = new ObservableCollection<DashBoardModel>(Global.GlobalRecentProductList.ToList().OrderByDescending(p => p.RecentViewItemDate));
                                IsShowRecentProduct = RecentProductList.Count > 0 ? true : false;
                                ShopByCategoryList.Clear();
                                ShopByCategoryList = MockProductData.Instance.GetShopByCatList(SeletedFilter, SelectedIndexHeaderTab);
                                break;
                            }
                        default:
                            break;
                    }
                    Global.GenderIndex = filterIndex;
                    if (BannerList != null && BannerList.Count > 0)
                    {
                        var tempBanner = BannerList.ToList();
                        if (filterBannerList != null)
                            filterBannerList.Clear();
                        filterBannerList = new ObservableCollection<BannerModel>();
                        filterBannerList = new ObservableCollection<BannerModel>(mockProductData.FilterBannerListByAPI(Global.GetStoreName(SelectedStoreIndex), filterIndex, tempBanner));
                    }

                    if (AllProductList != null && AllProductList.Count > 0)
                    {
                        var tempProductList = AllProductList.ToList();
                        ProductList = new ObservableCollection<DashBoardModel>(mockProductData.FilterProductListByAPI(Global.GetStoreName(SelectedStoreIndex), Global.GenderIndex, tempProductList).OrderByDescending(p => p.Id).ToList());
                        Console.WriteLine(ProductList);
                    }
                    await GetProductListWithPaginationMethod();
                    Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                }));

            }
        }

        #endregion

        #region Methods

        public async Task ExicuteTopTrandingCommond(DashBoardModel obj)
        {
            try
            {
                if (IsTap)
                    return;
                IsTap = true;

                if (Global.GlobalRecentProductList != null)
                {
                    if (Global.GlobalRecentProductList.Count > 0)
                    {
                        var IsResult = Global.GlobalRecentProductList.Where(x => x.Id == obj.Id).ToList();
                        if (IsResult.Count == 0)
                        {
                            obj.RecentViewItemDate = DateTime.Now;
                            Global.GlobalRecentProductList.Add(obj);
                            Global.database.Insert(new RecentProductModel()
                            {
                                UserId = Constant.LoginUserData != null ? Constant.LoginUserData.Id : 0,
                                ProductId = obj.Id
                            });
                        }
                    }
                    else
                    {
                        obj.RecentViewItemDate = DateTime.Now;
                        Global.GlobalRecentProductList.Add(obj);
                        Global.database.Insert(new RecentProductModel()
                        {
                            UserId = Constant.LoginUserData != null ? Constant.LoginUserData.Id : 0,
                            ProductId = obj.Id
                        });
                    }
                    //RecentProductList.Clear();
                    RecentProductList = new ObservableCollection<DashBoardModel>(Global.GlobalRecentProductList.ToList().OrderByDescending(p => p.RecentViewItemDate));
                }
                await navigation.PushAsync(new ItemDetailsPage(obj, false));
            }
            catch (Exception ex)
            {
                IsTap = false;
                Debug.WriteLine(ex.Message);
            }
        }

        public async Task LoadProductData()
        {
            try
            {
                Acr.UserDialogs.UserDialogs.Instance.ShowLoading();
                await Task.Delay(500);
                PageNumber = 1;
                await GetBannerListMethod();
                //await GetBannerProductListMethod();
                ShopByCategoryList = MockProductData.Instance.GetShopByCatList(SeletedFilter, SelectedIndexHeaderTab);
            }
            catch (Exception ex)
            {

            }
        }

        public async Task GetBannerProductListMethod()
        {
            try
            {
                if(!Constant.IsConnected)
                {
                    UserDialogs.Instance.Toast("Internet not available");
                    IsTap = false;
                    return;
                }
                //var req = new ProductResponseModel();
                //req.Data = new ProductDataModel();

                //var banner = new Banner();
                //banner.StoreId = SelectedStoreIndex;
                //banner.CategoryId = Global.GenderIndex;

                //var category = new Category();
                //category.StoreId = SelectedStoreIndex;
                //category.CategoryId = Global.GenderIndex;
                //category.Sizes = 20;

                //req.Data.Banners.Add(banner);
                //req.Data.Categories.Add(category);

                string methodUrl = "/api/Dashboard/GetAppDashboardAllItems?rootCategoryId=" + 1 + "&storeId=" + 1 + "&productPageSize=" + 20;
                RestResponseModel responseModel = await WebService.GetData(methodUrl, true);
                if (responseModel != null)
                {
                    BannerResponseModel bannerList = JsonConvert.DeserializeObject<BannerResponseModel>(responseModel.response_body);
                    if (bannerList.StatusCode == 200)
                    {
                        if (bannerList != null)
                        {
                            BannerList = new ObservableCollection<BannerModel>(bannerList.Data);
                            filterBannerList = new ObservableCollection<BannerModel>(bannerList.Data);
                        }
                        else
                        {
                            Acr.UserDialogs.UserDialogs.Instance.Alert(responseModel.ErrorMessage);
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
                        Acr.UserDialogs.UserDialogs.Instance.Alert(responseModel.ErrorMessage);
                        UserDialogs.Instance.HideLoading();
                    }
                }

                else
                {
                    Acr.UserDialogs.UserDialogs.Instance.Alert("Unable to process your request.");
                    Console.WriteLine(responseModel.ErrorMessage);
                }
            }

            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Alert(ex.Message);
                Console.WriteLine(ex.Message);
            }
            finally
            {
                //Acr.UserDialogs.UserDialogs.Instance.HideLoading();
            }
            //await GetProductListMethod();
            //await GetProductListWithPaginationMethod();

            SetFilterSelector();
        }

        public async Task GetBannerListMethod()
        {
            try
            {
                if(!Constant.IsConnected)
                {
                    UserDialogs.Instance.Toast("Internet not available");
                    IsTap = false;
                    return;
                }
                //Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Loading");
                //await Task.Delay(500);
                string methodUrl;
                methodUrl = "/api/Store/GetBannersV1";
                RestResponseModel responseModel = await WebService.GetData(methodUrl, true);
                if (responseModel != null)
                {
                    BannerResponseModel bannerList = JsonConvert.DeserializeObject<BannerResponseModel>(responseModel.response_body);
                    if (bannerList.StatusCode == 200)
                    {
                        if (bannerList != null)
                        {
                            BannerList = new ObservableCollection<BannerModel>(bannerList.Data);
                            filterBannerList = new ObservableCollection<BannerModel>(bannerList.Data);
                        }
                        else
                        {
                            Acr.UserDialogs.UserDialogs.Instance.Alert(responseModel.ErrorMessage);
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
                        Acr.UserDialogs.UserDialogs.Instance.Alert(responseModel.ErrorMessage);
                        UserDialogs.Instance.HideLoading();
                    }
                }

                else
                {
                    Acr.UserDialogs.UserDialogs.Instance.Alert("Unable to process your request.");
                    Console.WriteLine(responseModel.ErrorMessage);
                }
            }

            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Alert(ex.Message);
                Console.WriteLine(ex.Message);
            }
            finally
            {
                //Acr.UserDialogs.UserDialogs.Instance.HideLoading();
            }
            //await GetProductListMethod();
            //await GetProductListWithPaginationMethod();

            SetFilterSelector();
        }

        public async Task GetProductListMethod()
        {
            try
            {
                if(!Constant.IsConnected)
                {
                    UserDialogs.Instance.Toast("Internet not available");
                    IsTap = false;
                    return;
                }
                //Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Loading");
                string methodUrl;
                methodUrl = "/api/Product/GetProductsV1";
                RestResponseModel responseModel = await WebService.GetData(methodUrl, true);
                if (responseModel != null)
                {
                    var productListResponses = JsonConvert.DeserializeObject<ProductListResponse>(responseModel.response_body);
                    if (productListResponses != null)
                    {
                        if (productListResponses.StatusCode == 200)
                        {
                            if (productListResponses.Data != null)
                            {
                                ProductList = new ObservableCollection<DashBoardModel>(productListResponses.Data.Take(50));
                                AllProductList = new ObservableCollection<DashBoardModel>(productListResponses.Data.Take(50));
                            }
                            else
                            {
                                Acr.UserDialogs.UserDialogs.Instance.Alert(productListResponses.Message);
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
                            Acr.UserDialogs.UserDialogs.Instance.Alert(productListResponses.Message);
                            UserDialogs.Instance.HideLoading();
                        }
                    }
                    else
                    {
                        Acr.UserDialogs.UserDialogs.Instance.Alert(responseModel.ErrorMessage);
                        UserDialogs.Instance.HideLoading();
                    }
                }
                else
                {
                    Acr.UserDialogs.UserDialogs.Instance.Alert(responseModel.ErrorMessage);
                    UserDialogs.Instance.HideLoading();
                }
            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Alert("Unable to process your request.");
                Console.WriteLine(ex.Message);
                IsRefreshing = false;
            }
            finally
            {
                Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                IsRefreshing = false;
            }
            SetFilterSelector();
            //await GetProductListWithPaginationMethod();
        }

        public async Task GetProductListWithPaginationMethod()
        {
            try
            {
                if(!Constant.IsConnected)
                {
                    UserDialogs.Instance.Toast("Internet not available");
                    IsTap = false;
                    return;
                }

                var obj = Global.Storecategory.ToLower();
                var obj1 = GenderIndex;


                Console.WriteLine("Calling paging");
                //Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Loading");
                var req = new ProductRequestModel();
                req.PageNumber = PageNumber;
                req.PageSize = PageSize;

                if (Global.GenderIndex == 3)
                {
                    req.ExtraParam = new ExtraParam()
                    {
                        StoreId = Convert.ToString(SelectedStoreIndex),
                        CategoryId = ""
                    };
                }
                else
                {
                    req.ExtraParam = new ExtraParam()
                    {
                        StoreId = Convert.ToString(SelectedStoreIndex),
                        CategoryId = Convert.ToString(Global.GenderIndex),
                    };
                }
                string methodUrl;
                methodUrl = "/api/Product/GetAllProducts";
                RestResponseModel responseModel = await WebService.PostData(req, methodUrl, false);
                if (responseModel != null)
                {
                    var productListResponses = JsonConvert.DeserializeObject<ProductPagingListResponse>(responseModel.response_body);
                    if (productListResponses != null)
                    {
                        if (productListResponses.StatusCode == 200)
                        {
                            if (productListResponses.Data != null)
                            {
                                productPagingListResponse = productListResponses;
                                ProductList = new ObservableCollection<DashBoardModel>(productListResponses.Data.Data.ToList());
                                AllProductList = new ObservableCollection<DashBoardModel>(productListResponses.Data.Data.ToList());
                                var list = Global.database.GetRecentProductByUserID(Constant.LoginUserData != null ? Constant.LoginUserData.Id : 0);
                                if (list.ToList().Count > 0)
                                {
                                    var res = list.Select(p => p.ProductId).ToList();
                                    Global.GlobalRecentProductList = new ObservableCollection<DashBoardModel>(ProductList.Where(p => res.Contains(p.Id)).ToList());
                                    RecentProductList = new ObservableCollection<DashBoardModel>(ProductList.Where(p => res.Contains(p.Id)).ToList());
                                }

                                IsBusy = false;
                                GetFilterProduct();
                            }
                            else
                            {
                                Acr.UserDialogs.UserDialogs.Instance.Alert(productListResponses.Message);
                                UserDialogs.Instance.HideLoading();
                                IsBusy = false;
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
                            Acr.UserDialogs.UserDialogs.Instance.Alert(productListResponses.Message);
                            UserDialogs.Instance.HideLoading();
                            IsBusy = false;
                        }
                    }
                    else
                    {
                        Acr.UserDialogs.UserDialogs.Instance.Alert(responseModel.ErrorMessage);
                        UserDialogs.Instance.HideLoading();
                        IsBusy = false;
                    }
                }
                else
                {
                    Acr.UserDialogs.UserDialogs.Instance.Alert(responseModel.ErrorMessage);
                    UserDialogs.Instance.HideLoading();
                    IsBusy = false;
                }
            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Alert("Unable to process your request.");
                Console.WriteLine(ex.Message);
                IsRefreshing = false;
                IsBusy = false;
            }
            finally
            {
                Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                IsRefreshing = false;
                IsBusy = false;
            }
            await GetCategoryListMethod();
            //SetFilterSelector();
        }

        private async Task GetCategoryListMethod()
        {
            try
            {
                if(!Constant.IsConnected)
                {
                    UserDialogs.Instance.Toast("Internet not available");
                    IsTap = false;
                    return;
                }
                string methodUrl;
                int ind = Global.GenderIndex;
                if (ind == 3)
                    ind = 0;
                methodUrl = "/api/Dashboard/GetDashboardCategories?rootCategoryId=" + 0 + "&storeId=" + Global.StoreIndex;
                RestResponseModel responseModel = await WebService.GetData(methodUrl, true);
                if (responseModel != null)
                {
                    CategoryListResponse catList = JsonConvert.DeserializeObject<CategoryListResponse>(responseModel.response_body);
                    if (catList.StatusCode == 200)
                    {
                        if (catList != null)
                        {
                            CategoryList = new ObservableCollection<Category>(catList.Data);
                        }
                        else
                        {
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
                        UserDialogs.Instance.HideLoading();
                    }
                }
                else
                { }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {

            }
        }

        public void SetFilterSelector()
        {
            try
            {
                if (SeletedFilter == 1)
                {
                    boxSepBackCol1 = Color.FromHex(ThemeColor);
                    FilterCommand.Execute(1);
                }
                else if (SeletedFilter == 2)
                {
                    boxSepBackCol2 = Color.FromHex(ThemeColor);
                    FilterCommand.Execute(2);
                }
                else if (SeletedFilter == 3)
                {
                    boxSepBackCol3 = Color.FromHex(ThemeColor);
                    FilterCommand.Execute(3);
                }
                //GetProductListWithPaginationMethod();
            }
            catch (Exception ex)
            { }
            IsRefreshing = false;

        }

        public async void GetFilterProduct()
        {
            Acr.UserDialogs.UserDialogs.Instance.ShowLoading();
            await Task.Delay(100);
            switch (SeletedFilter)
            {
                case 1:
                    {
                        RecentProductList = new ObservableCollection<DashBoardModel>(Global.GlobalRecentProductList.Where(p => p.Gender.ToLower().Equals("m")).OrderByDescending(p => p.RecentViewItemDate));
                        ProductList = new ObservableCollection<DashBoardModel>(ProductList.Where(p => p.Gender.ToLower().Equals("m")));

                        IsShowRecentProduct = RecentProductList.Count > 0 ? true : false;

                        ShopByCategoryList.Clear();
                        ShopByCategoryList = MockProductData.Instance.GetShopByCatList(SeletedFilter, SelectedIndexHeaderTab);
                        break;
                    }
                case 2:
                    {
                        RecentProductList = new ObservableCollection<DashBoardModel>(Global.GlobalRecentProductList.Where(p => p.Gender.ToLower().Equals("f")).OrderByDescending(p => p.RecentViewItemDate));
                        ProductList = new ObservableCollection<DashBoardModel>(ProductList.Where(p => p.Gender.ToLower().Equals("f")));

                        IsShowRecentProduct = RecentProductList.Count > 0 ? true : false;

                        ShopByCategoryList.Clear();
                        ShopByCategoryList = MockProductData.Instance.GetShopByCatList(SeletedFilter, SelectedIndexHeaderTab);
                        break;
                    }
                case 3:
                    {
                        RecentProductList = new ObservableCollection<DashBoardModel>(Global.GlobalRecentProductList.ToList().OrderByDescending(p => p.RecentViewItemDate));

                        IsShowRecentProduct = RecentProductList.Count > 0 ? true : false;

                        ShopByCategoryList.Clear();
                        ShopByCategoryList = MockProductData.Instance.GetShopByCatList(SeletedFilter, SelectedIndexHeaderTab);
                        break;
                    }
                default:
                    break;
            }
            Global.GenderIndex = SeletedFilter;
            //BannerList = new ObservableCollection<BannerModel>(mockProductData.FilterBannerList(Global.GetStoreName(SelectedStoreIndex), filterIndex));
            //ProductList = new ObservableCollection<DashBoardModel>(mockProductData.FilterProductList(Global.Storecategory, Global.GenderIndex).Take(5));
            if (BannerList != null && BannerList.Count > 0)
            {
                var tempBanner = BannerList.ToList();
                if (filterBannerList != null)
                    filterBannerList.Clear();

                filterBannerList = new ObservableCollection<BannerModel>(mockProductData.FilterBannerListByAPI(Global.GetStoreName(SelectedStoreIndex), SeletedFilter, tempBanner));
            }

            if (AllProductList != null && AllProductList.Count > 0)
            {
                var tempProductList = AllProductList.ToList();
                ProductList = new ObservableCollection<DashBoardModel>(mockProductData.FilterProductListByAPI(Global.GetStoreName(SelectedStoreIndex), Global.GenderIndex, tempProductList).OrderByDescending(p => p.Id).ToList());
                Console.WriteLine(ProductList);
            }
        }
        #endregion
    }
}
