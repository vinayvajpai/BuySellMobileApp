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
using BuySell.Utility;
using BuySell.View;
using BuySell.Views;
using BuySell.WebServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace BuySell.ViewModel
{
    public class DashboardProductViewModel : BaseViewModel
    {
        int GenderIndex = 1;
        public int SelectedStoreIndex = 1;
        int PageNumber = 1;
        int PageSize = 20;
        bool _IsPopup;
        ProductPagingListResponse productPagingListResponse;
        public DashboardProductViewModel(INavigation _nav)
        {
            navigation = _nav;
            //LoadProductData();
        }
        public DashboardProductViewModel(INavigation _nav, bool IsPopup)
        {
            navigation = _nav;
            _IsPopup = IsPopup;
            //LoadProductData();
        }
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

        private ObservableCollection<BannerModel> _filterBannerList;
        public ObservableCollection<BannerModel> filterBannerList
        {
            get { return _filterBannerList; }
            set {
                _filterBannerList = value; OnPropertyChanged("filterBannerList");
                if (_filterBannerList != null)
                {
                    if (_filterBannerList.Count > 0)
                    {
                        IsShowBannerMsg = false;
                    }
                    else
                    {
                        IsShowBannerMsg = true;
                    }
                }
            }
        }

        private ObservableCollection<ShopByCategoryModel> _ShopByCategoryList = new ObservableCollection<ShopByCategoryModel>();
        public ObservableCollection<ShopByCategoryModel> ShopByCategoryList
        {
            get { return _ShopByCategoryList; }
            set { _ShopByCategoryList = value; OnPropertyChanged("ShopByCategoryList"); }
        }
        private bool _EarlyAccessPopupIsVisible = false;
        public bool EarlyAccessPopupIsVisible
        {
            get { return _EarlyAccessPopupIsVisible; }
            set
            {
                _EarlyAccessPopupIsVisible = value;
                OnPropertyChanged("EarlyAccessPopupIsVisible");
            }

        }
        private bool _IsShowRecentProduct;
        public bool IsShowRecentProduct
        {
            get { return _IsShowRecentProduct; }
            set { _IsShowRecentProduct = value; OnPropertyChanged("IsShowRecentProduct"); }
        }

        private bool _IsLoadingLocal = true;
        public bool IsLoadingLocal
        {
            get { return _IsLoadingLocal; }
            set { _IsLoadingLocal = value; OnPropertyChanged("IsLoadingLocal"); }
        }

        private bool _IsShowBannerMsg = true;
        public bool IsShowBannerMsg
        {
            get { return _IsShowBannerMsg; }
            set { _IsShowBannerMsg = value; OnPropertyChanged("IsShowBannerMsg"); }
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
        private Command _ListNowCommand;
        public Command ListNowCommand
        {
            get
            {
                return _ListNowCommand ?? (_ListNowCommand = new Command(async () =>
                {
                    try
                    {
                        if (Global.Username == null && string.IsNullOrEmpty(Global.Username) && string.IsNullOrEmpty(Global.Password))
                        {
                            Global.AlertWithAction("Please login first", () =>
                            {
                                var nav1 = new NavigationPage(new LoginPage());
                                App.Current.MainPage = nav1;
                            });
                            //var alertConfig = new AlertConfig
                            //{
                            //    Message = "Please login first",
                            //    OkText = "OK",
                            //    OnAction = () =>
                            //    {
                            //        var nav1 = new NavigationPage(new LoginPage());
                            //        App.Current.MainPage = nav1;
                            //    }
                            //};
                            //Acr.UserDialogs.UserDialogs.Instance.Alert(alertConfig);
                            return;
                        }

                        EarlyAccessPopupIsVisible = false;
                        var nav = new NavigationPage(new AddItemDetailsPage());
                        App.Current.MainPage = nav;
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
        private Command _NotNowCommand;
        public Command NotNowCommand
        {
            get
            {
                return _NotNowCommand ?? (_NotNowCommand = new Command(async () =>
                {
                    try
                    {
                        if(_IsPopup == true)
                        {
                            EarlyAccessPopupIsVisible = false;
                        }
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
                      IsTap = false;
                      Debug.WriteLine(ex.Message);
                  }

              }
          ));
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
                        string categoryIndex = Global.GenderIndex.ToString();

                        if (categoryIndex == "3")
                            categoryIndex = null;

                        if (IsTap)
                            return;
                        IsTap = true;
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

        private Command _FilterCommand;
        public Command FilterCommand
        {
            get
            {
                return _FilterCommand ?? (_FilterCommand = new Command(async (obj) =>
                {

                    int filterIndex = Convert.ToInt32(obj);
                    switch (filterIndex)
                    {
                        case 1:
                            {
                                Global.GenderParam = "Men";
                                RecentProductList = new ObservableCollection<DashBoardModel>(Global.GlobalRecentProductList.Where(p => p.Gender.ToLower().Equals("m")).OrderByDescending(p => p.RecentViewItemDate));
                                ProductList = new ObservableCollection<DashBoardModel>(ProductList.Where(p => p.Gender.ToLower().Equals("m")));
                                IsShowRecentProduct = RecentProductList.Count > 0 ? true : false;
                                ShopByCategoryList.Clear();
                                ShopByCategoryList = MockProductData.Instance.GetShopByCatList(SeletedFilter, SelectedIndexHeaderTab);
                                break;
                            }
                        case 2:
                            {
                                Global.GenderParam = "Women";
                                RecentProductList = new ObservableCollection<DashBoardModel>(Global.GlobalRecentProductList.Where(p => p.Gender.ToLower().Equals("f")).OrderByDescending(p => p.RecentViewItemDate));
                                ProductList = new ObservableCollection<DashBoardModel>(ProductList.Where(p => p.Gender.ToLower().Equals("f")));
                                IsShowRecentProduct = RecentProductList.Count > 0 ? true : false;
                                ShopByCategoryList.Clear();
                                ShopByCategoryList = MockProductData.Instance.GetShopByCatList(SeletedFilter, SelectedIndexHeaderTab);
                                break;
                            }
                        case 3:
                            {
                                Global.GenderParam = "All";
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
                    LoadProductData();
                }));

            }
        }

        private Command _RecentViewAllCommand;
        public Command RecentViewAllCommand
        {
            get
            {
                return _RecentViewAllCommand ?? (_RecentViewAllCommand = new Command(async (obj) =>
                {
                    try
                    {
                        if (IsTap)
                            return;
                        IsTap = true;
                        await navigation.PushAsync(new RecentProductListView(RecentProductList.ToList()));
                    }
                    catch (Exception)
                    {
                        IsTap = false;
                    }

                }));
            }
        }

        private Command _TopTrandingCommand;
        public Command TopTrandingCommand
        {
            get
            {
                return _TopTrandingCommand ?? (_TopTrandingCommand = new Command<DashBoardModel>(async (obj) => await ExicuteTopTrandingCommond(obj)));
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
                    //LoadProductData();
                }
                ));
            }
        }

        private Command _RefreshCommand;
        public Command RefreshCommand
        {
            get
            {
                return _RefreshCommand ?? (_RefreshCommand = new Command((index) =>
                {
                    LoadProductData();
                }
                ));
            }
        }

        #endregion

        #region Methods

        async Task GetBannerListMethod()
        {
            try
            {
                string methodUrl;
                methodUrl = "/api/Store/GetBannersV1";
                RestResponseModel responseModel = await WebService.GetData(methodUrl, true);
                if (responseModel != null)
                {
                    if (responseModel.status_code == 200)
                    {
                        BannerResponseModel bannerList = JsonConvert.DeserializeObject<BannerResponseModel>(responseModel.response_body);
                        if (bannerList.StatusCode == 200)
                        {
                            if (bannerList != null)
                            {
                                Global.database.DeleteAllBanner();
                                Global.database.InsertBanner(new BannerProductModel()
                                {
                                    BannerDetail = JsonConvert.SerializeObject(bannerList.Data)
                                });
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
                                Acr.UserDialogs.UserDialogs.Instance.Toast(responseModel.ErrorMessage);
                                UserDialogs.Instance.HideLoading();
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(responseModel.ErrorMessage))
                            {
                                Acr.UserDialogs.UserDialogs.Instance.Toast(responseModel.ErrorMessage);
                                UserDialogs.Instance.HideLoading();
                            }
                            else
                            {
                                Acr.UserDialogs.UserDialogs.Instance.Toast(Constant.ErrorMessage);
                                UserDialogs.Instance.HideLoading();
                            }
                        }
                    }
                    else if (responseModel.status_code == 400)
                    {
                        if (!string.IsNullOrEmpty(responseModel.ErrorMessage))
                        {
                            Acr.UserDialogs.UserDialogs.Instance.Toast(responseModel.ErrorMessage);
                            UserDialogs.Instance.HideLoading();
                        }
                        else
                        {
                            Acr.UserDialogs.UserDialogs.Instance.Toast(Constant.ErrorMessage);
                            UserDialogs.Instance.HideLoading();
                        }
                    }
                    else
                    {
                        if (responseModel.response_body.ToLower().Contains(Constant.TokenExpireMsgStr.ToLower()))
                        {
                            Global.ShowActionAlert(Constant.SessionTimeOutMsgStr, okAction: () =>
                            {
                                Global.LogoutWithoutConfirm();
                            });
                            UserDialogs.Instance.HideLoading();
                        }
                        else
                        {
                            Acr.UserDialogs.UserDialogs.Instance.Toast(Constant.ErrorMessage);
                            UserDialogs.Instance.HideLoading();
                        }
                    }
                }
                else
                {
                    Acr.UserDialogs.UserDialogs.Instance.Toast(Constant.ErrorMessage);
                    UserDialogs.Instance.HideLoading();
                }
            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast(ex.Message);
                UserDialogs.Instance.HideLoading();
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }
        }
        async Task GetProductListWithPaginationMethod()
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
                RestResponseModel responseModel = await WebService.PostData(req, methodUrl, true);
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
                                    productPagingListResponse = productListResponses;
                                    //ProductList = new ObservableCollection<DashBoardModel>(productListResponses.Data.Data.ToList());
                                    //AllProductList = new ObservableCollection<DashBoardModel>(productListResponses.Data.Data.ToList());
                                    Global.database.DeleteProductViaStoreCat(Global.GenderIndex, SelectedStoreIndex);
                                    Global.database.InsertProduct(new DashboardProductModel()
                                    {
                                        CategoryID = Global.GenderIndex,
                                        StoreID = SelectedStoreIndex,
                                        ProductDetail = responseModel.response_body
                                    });
                                    GetProductLocalData();
                                    IsBusy = false;
                                }
                                else
                                {
                                    Acr.UserDialogs.UserDialogs.Instance.Toast(productListResponses.Message);
                                    UserDialogs.Instance.HideLoading();
                                    IsBusy = false;
                                }
                            }
                            else
                            {
                                Acr.UserDialogs.UserDialogs.Instance.Toast(productListResponses.Message);
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
                            Acr.UserDialogs.UserDialogs.Instance.Toast(Constant.ErrorMessage);
                            UserDialogs.Instance.HideLoading();
                            IsBusy = false;
                        }
                    }
                    else if (responseModel.status_code == 400)
                    {
                        if (!string.IsNullOrEmpty(responseModel.ErrorMessage))
                        {
                            Acr.UserDialogs.UserDialogs.Instance.Toast(responseModel.ErrorMessage);
                            UserDialogs.Instance.HideLoading();
                        }
                        else
                        {
                            Acr.UserDialogs.UserDialogs.Instance.Toast(Constant.ErrorMessage);
                            UserDialogs.Instance.HideLoading();
                        }
                    }
                    else
                    {
                        Acr.UserDialogs.UserDialogs.Instance.Toast(Constant.ErrorMessage);
                        UserDialogs.Instance.HideLoading();
                    }
                }
                else
                {
                    Acr.UserDialogs.UserDialogs.Instance.Toast(Constant.ErrorMessage);
                    UserDialogs.Instance.HideLoading();
                    IsBusy = false;
                }
            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast(ex.Message);
                UserDialogs.Instance.HideLoading();
                IsRefreshing = false;
                IsBusy = false;
            }
            finally
            {
                Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                IsRefreshing = false;
                IsBusy = false;
            }
        }
        public async void LoadProductData()
        {
            try
            {


                await Task.Run(() =>
                {
                    SetGenderFilterSelector();
                    ShopByCategoryList = new ObservableCollection<ShopByCategoryModel>();
                    ShopByCategoryList = MockProductData.Instance.GetShopByCatList(SeletedFilter, SelectedIndexHeaderTab);
                });
                var BannerLocalData = Global.database.GetAllBanner();
                var productLocalData = Global.database.GetAllProductByStoreCat(Global.GenderIndex, SelectedStoreIndex);
                if (BannerLocalData == null)
                {
                    //Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Loading");
                    //await Task.Delay(50);
                    IsBusy = true;
                    await GetBannerListMethod();
                    await GetProductListWithPaginationMethod();
                }
                else
                {
                    var bannerList = JsonConvert.DeserializeObject<List<BannerModel>>(BannerLocalData.BannerDetail);
                    if (bannerList != null)
                    {
                        filterBannerList = new ObservableCollection<BannerModel>(MockProductData.Instance.FilterBannerListByAPI(Global.GetStoreName(SelectedStoreIndex), SeletedFilter, bannerList)); ;
                        //BannerList = new ObservableCollection<BannerModel>(bannerList);
                    }
                    if (productLocalData != null)
                    {
                        var productListRes = JsonConvert.DeserializeObject<ProductPagingListResponse>(productLocalData.ProductDetail);
                        if (productListRes != null)
                        {
                            productPagingListResponse = productListRes;
                            AllProductList = new ObservableCollection<DashBoardModel>(productListRes.Data.Data);
                            ProductList = new ObservableCollection<DashBoardModel>(productListRes.Data.Data);
                        }
                    }
                    if (bannerList.Count == 0 || AllProductList.Count == 0)
                    {
                        //Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Loading");
                        //await Task.Delay(50);
                        IsBusy = true;
                        await GetBannerListMethod();
                        await GetProductListWithPaginationMethod();
                    }
                    else
                    {
                        await Task.Run(async () =>
                        {
                            await GetBannerListMethod();
                            await GetProductListWithPaginationMethod();
                        });
                    }
                }

            }
            catch (Exception ex)
            {
                IsBusy = false;
            }
            finally
            {
                IsBusy = false;
            }
        }
        public void GetProductLocalData()
        {
            try
            {
                var BannerLocalData = Global.database.GetAllBanner();
                var productLocalData = Global.database.GetAllProductByStoreCat(Global.GenderIndex, SelectedStoreIndex);
                var bannerList = JsonConvert.DeserializeObject<List<BannerModel>>(BannerLocalData.BannerDetail);
                if (bannerList != null)
                {
                    filterBannerList = new ObservableCollection<BannerModel>(MockProductData.Instance.FilterBannerListByAPI(Global.GetStoreName(SelectedStoreIndex), SeletedFilter, bannerList)); ;
                    //BannerList = new ObservableCollection<BannerModel>(bannerList);
                }
                if (productLocalData != null)
                {
                    var productListRes = JsonConvert.DeserializeObject<ProductPagingListResponse>(productLocalData.ProductDetail);
                    if (productListRes != null)
                    {
                        productPagingListResponse = productListRes;
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            AllProductList.Clear();
                            ProductList.Clear();
                            AllProductList = new ObservableCollection<DashBoardModel>(productListRes.Data.Data);
                            ProductList = new ObservableCollection<DashBoardModel>(productListRes.Data.Data);
                            if (ProductList.Count == 0)
                            {
                                IsNoData = true;
                            }
                            else
                            {
                                IsNoData = false;
                            } 
                           GetRecentViewList();
                        });

                    }
                }

                ShopByCategoryList = new ObservableCollection<ShopByCategoryModel>();
                ShopByCategoryList = MockProductData.Instance.GetShopByCatList(SeletedFilter, SelectedIndexHeaderTab);
            }
            catch (Exception ex)
            {
                IsBusy = false;
            }
            finally
            {
                IsBusy = false;
            }
        }
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
                                ProductId = obj.Id,
                                RecentViewItemDate = obj.RecentViewItemDate
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
                            ProductId = obj.Id,
                            RecentViewItemDate = obj.RecentViewItemDate
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
        public void SetGenderFilterSelector()
        {
            try
            {
                if (SeletedFilter == 1)
                {
                    boxSepBackCol1 = Color.FromHex(ThemeColor);
                }
                else if (SeletedFilter == 2)
                {
                    boxSepBackCol2 = Color.FromHex(ThemeColor);
                }
                else if (SeletedFilter == 3)
                {
                    boxSepBackCol3 = Color.FromHex(ThemeColor);
                }
            }
            catch (Exception ex)
            { }
            IsRefreshing = false;

        }
        public void GetRecentViewList()
        {
            try
            {

                var list = Global.database.GetRecentProductByUserID(Constant.LoginUserData != null ? Constant.LoginUserData.Id : 0);
                if (list.ToList().Count > 0)
                {
                    var res = list.OrderByDescending(p => p.RecentViewItemDate).Select(p => p.ProductId).ToList().Distinct();
                    Global.GlobalRecentProductList = new ObservableCollection<DashBoardModel>();
                    RecentProductList = new ObservableCollection<DashBoardModel>();
                    foreach (var objpro in res)
                    {
                        var pro = ProductList.Where(p => p.Id == objpro).FirstOrDefault();
                        if (pro != null)
                        {
                            Global.GlobalRecentProductList.Add(pro);
                            RecentProductList.Add(pro);
                        }
                    }
                }
                IsShowRecentProduct = RecentProductList.Count > 0 ? true : false;

            }
            catch (Exception ex)
            {

            }
        }

        #endregion

    }
}

