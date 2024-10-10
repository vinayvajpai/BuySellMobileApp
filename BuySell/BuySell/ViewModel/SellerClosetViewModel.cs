using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using BuySell.Helper;
using BuySell.Model;
using BuySell.Model.RestResponse;
using BuySell.Utility;
using BuySell.View;
using BuySell.Views;
using BuySell.WebServices;
using Newtonsoft.Json;
using Xamarin.Forms;
namespace BuySell.ViewModel
{
    public class SellerClosetViewModel : BaseViewModel
    {
        INavigation navigation;
        string userID = string.Empty;
        bool IsLoggedIn = false;
        public int PageNumber = 1;
        public int count = 0;
        public ProductPagingListResponse productPagingListResponse;
        public ProductFilterModel productFilterModel;
        #region Constructor
        public SellerClosetViewModel(INavigation _nav, bool _IsLoggedIn, string _userID = null)
        {
            navigation = _nav;
            IsLoggedIn = _IsLoggedIn;
            userID = _userID;
            ItemTreshold = 1;
            ItemTresholdReachedCommand = new Command(async () => await ItemsTresholdReached());
            //if (Device.RuntimePlatform == Device.iOS)
            {
                GetTopTrendingItemsListMethod();
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
        private double _Opacity = 1;
        public double Opacity
        {
            get { return _Opacity; }
            set
            {
                _Opacity = value;
                OnPropertyChanged("Opacity");
            }

        }

        private bool _UserBlockPopupIsVisible = false;
        public bool UserBlockPopupIsVisible
        {
            get { return _UserBlockPopupIsVisible; }
            set
            {
                _UserBlockPopupIsVisible = value;
                OnPropertyChanged("UserBlockPopupIsVisible");
            }

        }

        private bool _SearchPopupIsVisible = false;
        public bool SearchPopupIsVisible
        {
            get { return _SearchPopupIsVisible; }
            set
            {
                _SearchPopupIsVisible = value;
                OnPropertyChanged("SearchPopupIsVisible");
            }

        }

        public int _PageSize = 20;
        public int PageSize
        {
            get
            {
                return _PageSize;
            }
            set
            {
                _PageSize = value;
                OnPropertyChanged("PageSize");
            }
        }

        private bool _IsLoadMore;
        public bool IsLoadMore
        {
            get => _IsLoadMore;
            set
            {
                _IsLoadMore = value;
                OnPropertyChanged("IsLoadMore");
            }
        }

        private int _itemTreshold;
        public int ItemTreshold
        {
            get { return _itemTreshold; }
            set { SetProperty(ref _itemTreshold, value); }
        }

        MockProductData mockProductData;
        private ObservableCollection<DashBoardModel> _TopTrendingItemsList = new ObservableCollection<DashBoardModel>();
        public ObservableCollection<DashBoardModel> TopTrendingItemsList
        {
            get { return _TopTrendingItemsList; }
            set { _TopTrendingItemsList = value; OnPropertyChanged("TopTrendingItemsList"); }
        }

        private bool _IsBlockUserTxt;
        public bool IsBlockUserTxt
        {
            get => _IsBlockUserTxt;
            set
            {
                _IsBlockUserTxt = value;
                OnPropertyChanged("IsBlockUserTxt");
            }
        }

        private bool _IsOwnBlockTxt;
        public bool IsOwnBlockTxt
        {
            get => _IsOwnBlockTxt;
            set
            {
                _IsOwnBlockTxt = value;
                OnPropertyChanged("IsOwnBlockTxt");
            }
        }

        private string _SearchText { get; set; }
        public string SearchText
        {
            get { return _SearchText; }
            set
            {
                if (_SearchText != value)
                {
                    _SearchText = value;
                }
                OnPropertyChanged("SearchText");
            }
        }

        private int _translationY = -200;
        public int TranslationY
        {
            get { return _translationY; }
            set
            {
                if (_translationY != value)
                {
                    _translationY = value;
                }
                OnPropertyChanged("TranslationY");
            }
        }

        #endregion

        #region Methods
        public void GetTopTrendingItemsListMethod()
        {
            try
            {
                _ = CallGetProductListByUserIdMethod();
            }
            catch (Exception ex)
            {

            }
        }
        public async Task GetProductListMethod()
        {
            try
            {
                if (!Constant.IsConnected)
                {
                    UserDialogs.Instance.Toast("Internet not available");
                    IsTap = false;
                    return;
                }
                Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Loading");
                string methodUrl = "/api/Product/GetProductsByUserIdV1?id=" + userID;
                RestResponseModel responseModel = await WebService.GetData(methodUrl, true);
                if (responseModel != null)
                {
                    if (responseModel.status_code == 200)
                    {
                        var productListResponses = JsonConvert.DeserializeObject<ProductListResponse>(responseModel.response_body);

                        if (productListResponses.StatusCode == 200)
                        {
                            if (productListResponses.Data != null)
                            {
                                if (productListResponses.Data.Count > 0)
                                {
                                    var productList = productListResponses.Data;
                                    TopTrendingItemsList = new ObservableCollection<DashBoardModel>(productList.Where(x => x.UserId == userID).OrderByDescending(p => p.Id).ToList());
                                }
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
                            Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                            UserDialogs.Instance.HideLoading();
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
                    else
                    {
                        Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                        UserDialogs.Instance.HideLoading();
                    }
                }
                else
                {
                    Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                    UserDialogs.Instance.HideLoading();
                }
            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Alert(ex.Message);
                Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                Global.IsUploadedProduct = false;
            }
            finally
            {
                Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                Global.IsUploadedProduct = false;
            }
        }
        public async Task CallGetProductListByUserIdMethod()
        {
            try
            {
                Acr.UserDialogs.UserDialogs.Instance.ShowLoading();
                await Task.Delay(100);
                await Device.InvokeOnMainThreadAsync(async () =>
                {
                    await GetProductListByUserIdMethod();
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                IsLoadMore = false;
                Acr.UserDialogs.UserDialogs.Instance.HideLoading();
            }
        }
        private async Task GetProductListByUserIdMethod()
        {
            try
            {
                if (!Constant.IsConnected)
                {
                    UserDialogs.Instance.Toast("Internet not available");
                    IsTap = false;
                    return;
                }
                var req = new ProductRequestModel();
                req.PageNumber = PageNumber;
                req.PageSize = PageSize;
                string methodUrl;
                methodUrl = "/api/Product/GetAllProducts";
                req.ExtraParam = new ExtraParam()
                {
                    UserId = userID//Convert.ToString(Global.GenderIndex),
                };
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
                                    IsBusy = false;
                                    foreach (var item in productListResponses.Data.Data)
                                    {
                                        TopTrendingItemsList.Add(item);
                                    }
                                    if (productListResponses.Data.Data.Count() == 0)
                                    {
                                        ItemTreshold = -1;
                                        //return;
                                    }
                                    if (TopTrendingItemsList.Count > 0)
                                    {
                                        if ((PageNumber * PageSize) < productListResponses.Data.TotalRowCount)
                                            TotalCount = "Showing 1 to " + (PageNumber * PageSize) + " of " + productListResponses.Data.TotalRowCount + " Items";
                                        else
                                            TotalCount = "Showing 1 to " + productListResponses.Data.TotalRowCount + " of " + productListResponses.Data.TotalRowCount + " Items";

                                    }
                                    PageNumber = productPagingListResponse.Data.NextPageNumber;
                                    sizeByProductList = new ObservableCollection<string>(TopTrendingItemsList.Select(s => s.Size).Distinct().ToList().ToList().OrderBy(x => x));
                                    brandList = new ObservableCollection<string>(TopTrendingItemsList.Select(s => s.Brand).Distinct().ToList().ToList().OrderBy(x => x));
                                    IsLoadMore = false;

                                }
                                else
                                {
                                    IsLoadMore = false;
                                    Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                                    UserDialogs.Instance.HideLoading();
                                }
                            }
                            else
                            {
                                IsLoadMore = false;
                                Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                                UserDialogs.Instance.HideLoading();
                            }
                        }
                        else
                        {
                            IsLoadMore = false;
                            Acr.UserDialogs.UserDialogs.Instance.Alert(responseModel.ErrorMessage);
                            UserDialogs.Instance.HideLoading();
                        }
                    }
                    else if (responseModel.status_code == 400)
                    {
                        if (responseModel != null)
                        {
                            Acr.UserDialogs.UserDialogs.Instance.Alert(responseModel.ErrorMessage);
                            UserDialogs.Instance.HideLoading();
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
                        Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                        UserDialogs.Instance.HideLoading();
                    }
                }
                else
                {
                    IsLoadMore = false;
                    Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                    UserDialogs.Instance.HideLoading();
                }
            }
            catch (Exception ex)
            {
                IsLoadMore = false;
                Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                Global.IsUploadedProduct = false;
            }
            finally
            {
                 Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                Global.IsUploadedProduct = false;
                IsFilterChanged = false;
            }
            IsNoData = TopTrendingItemsList.Count > 0 ? false : true;
            IsShowFilter = !IsNoData;
        }
        async Task ItemsTresholdReached()
        {
            if(TopTrendingItemsList.Count==0 && IsFilterChanged == true)
            {
                return;
            }


            if (Global.IsUploadedProduct)
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

            await Task.Run(async () =>
            {
                IsLoadMore = true;
                await Task.Delay(50);

                try
                {
                    
                    if (productFilterModel == null)
                    {
                        await GetProductListByUserIdMethod();
                    }
                    else
                    {
                        productFilterModel.PageNumber = PageNumber;
                        await GetProductByFilterMethod(productFilterModel);
                    }
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
        public void SearchClick()
        {
            try
            {
                if (!string.IsNullOrEmpty(SearchText))
                {
                    GetSearchItemList();
                }
            }
            catch (Exception ex)
            {

            }
        }
        private async void GetSearchItemList()
        {
            try
            {
                if (!Constant.IsConnected)
                {
                    UserDialogs.Instance.Toast("Internet not available");
                    UserDialogs.Instance.HideLoading();
                    IsTap = false;
                    return;
                }
                PageNumber = 1;
                var req = new ProductRequestModel();
                req.PageNumber = PageNumber;
                req.PageSize = PageSize;
                req.Search = SearchText.Trim();
                string methodUrl = "/api/Product/GetAllProducts";
                req.ExtraParam = new ExtraParam();
                req.ExtraParam.UserId = userID;
                UserDialogs.Instance.ShowLoading();
                await Task.Delay(100);
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
                                    TopTrendingItemsList.Clear();
                                    productPagingListResponse = productListResponses;
                                    IsBusy = false;
                                    foreach (var item in productListResponses.Data.Data)
                                    {
                                        TopTrendingItemsList.Add(item);
                                    }

                                    if (productListResponses.Data.Data.Count() == 0)
                                    {
                                        ItemTreshold = -1;
                                        //return;
                                    }
                                    if (TopTrendingItemsList.Count > 0)
                                    {
                                        if ((PageNumber * PageSize) < productListResponses.Data.TotalRowCount)
                                            TotalCount = "Showing 1 to " + (PageNumber * PageSize) + " of " + productListResponses.Data.TotalRowCount + " Items";
                                        else
                                            TotalCount = "Showing 1 to " + productListResponses.Data.TotalRowCount + " of " + productListResponses.Data.TotalRowCount + " Items";

                                    }
                                    PageNumber = productPagingListResponse.Data.NextPageNumber;
                                    sizeByProductList = new ObservableCollection<string>(TopTrendingItemsList.Select(s => s.Size).Distinct().ToList().ToList().OrderBy(x => x));
                                    brandList = new ObservableCollection<string>(TopTrendingItemsList.Select(s => s.Brand).Distinct().ToList().ToList().OrderBy(x => x));
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
                        IsLoadMore = false;
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
                        IsLoadMore = false;
                    }
                }
                else
                {
                    Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                    UserDialogs.Instance.HideLoading();
                    IsLoadMore = false;
                }
            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Alert(ex.Message);
                UserDialogs.Instance.HideLoading();
                IsLoadMore = false;
            }
            finally
            {
                Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                IsLoadMore = false;
                IsFilterChanged = false;
            }
            IsNoData = TopTrendingItemsList.Count > 0 ? false : true;
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
                Acr.UserDialogs.UserDialogs.Instance.ShowLoading();
                await Task.Delay(100);

                prevPageNumber = PageNumber;
                productFilterModel.PageNumber = PageNumber;
                string methodUrl = "/api/Product/GetAllFilterProduct";
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
                                        TopTrendingItemsList.Add(item);
                                    }

                                    if (productListResponses.Data.Data.Count() == 0)
                                    {
                                        ItemTreshold = -1;
                                    }
                                    if (TopTrendingItemsList.Count > 0)
                                    {
                                        if ((PageNumber * PageSize) < productListResponses.Data.TotalRowCount)
                                            TotalCount = "Showing 1 to " + (PageNumber * PageSize) + " of " + productListResponses.Data.TotalRowCount + " Items";
                                        else
                                            TotalCount = "Showing 1 to " + productListResponses.Data.TotalRowCount + " of " + productListResponses.Data.TotalRowCount + " Items";

                                    }
                                    PageNumber = productPagingListResponse.Data.NextPageNumber;
                                    sizeByProductList = new ObservableCollection<string>(TopTrendingItemsList.Select(s => s.Size).Distinct().ToList().ToList().OrderBy(x => x));
                                    brandList = new ObservableCollection<string>(TopTrendingItemsList.Select(s => s.Brand).Distinct().ToList().ToList().OrderBy(x => x));
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
            IsNoData = TopTrendingItemsList.Count > 0 ? false : true;
        }
        public async void SetProductFilterRequest(List<FilterModel> listFilter)
        {
            try
            {
                productFilterModel = new ProductFilterModel();
                productFilterModel.StoreId = null;
                if (!string.IsNullOrEmpty(SearchText))
                {
                    productFilterModel.Search = SearchText.Trim();
                }
                productFilterModel.PageSize = PageSize;
                productFilterModel.PageNumber = PageNumber;
                productFilterModel.Price = new PriceFilter();
                productFilterModel.ShippingPrice = new PriceFilter();
                productFilterModel.Sort = new SortingFilter();
                productFilterModel.UserId = Convert.ToInt32(userID);
                productFilterModel.LoggedUserId = null;
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
                TopTrendingItemsList.Clear();
                await GetProductByFilterMethod(productFilterModel);
            }
            catch (Exception ex)
            {
            }
        }
        #endregion

        #region Commands
        private Command _UserBlockDownCommand;
        public Command UserBlockDownCommand
        {
            get
            {
                return _UserBlockDownCommand ?? (_UserBlockDownCommand = new Command(() => {
                    if (IsLoggedIn && Constant.LoginUserData != null)
                    {
                        if (userID == Constant.LoginUserData.Id.ToString())
                        {
                            Global.AlertWithAction("You can't block yourself!!", null);
                            //var alertConfig = new AlertConfig
                            //{
                            //    Message = "You can't block yourself",
                            //    OkText = "OK"
                            //};
                            //Acr.UserDialogs.UserDialogs.Instance.Alert(alertConfig);
                        }
                        else
                        {
                            UserBlockPopupIsVisible = true;
                            IsBlockUserTxt = true;
                        }
                    }
                }));
            }
        }

        private Command _PopupCancelCommand;
        public Command PopupCancelCommand
        {
            get
            {
                return _PopupCancelCommand ?? (_PopupCancelCommand = new Command(async () =>
                {
                    UserBlockPopupIsVisible = false;
                    count = 0;
                }));
            }
        }

        private ICommand _BackCommand;
        public ICommand BackCommand
        {
            get
            {
                if (_BackCommand == null)
                {
                    _BackCommand = new Command(async () =>
                    {
                        try
                        {
                            if (IsTap)
                                return;
                            IsTap = true;
                            await navigation.PopAsync();
                        }
                        catch (Exception ex)
                        {
                            IsTap = false;
                        }
                    });
                }
                return _BackCommand;
            }
        }

        private ICommand _SearchPopUpCommand;
        public ICommand SearchPopUpCommand
        {
            get
            {
                if (_SearchPopUpCommand == null)
                {
                    _SearchPopUpCommand = new Command(async () =>
                    {
                        try
                        {
                            if (TranslationY == -200)
                            {
                                TranslationY = 0;
                            }
                            else
                            {
                                TranslationY = -200;
                            }
                            if (!String.IsNullOrEmpty(SearchText))
                            {
                                PageNumber = 1;
                                TopTrendingItemsList.Clear();
                                GetTopTrendingItemsListMethod();
                                SearchText = String.Empty;
                            }
                        }
                        catch (Exception ex)
                        {
                            IsTap = false;
                        }
                    });
                }
                return _SearchPopUpCommand;
            }
        }
        public Command ItemTresholdReachedCommand { get; set; }

        private Command _TopTrandingItemsCommand;
        public Command TopTrandingItemsCommand
        {
            get
            {
                return _TopTrandingItemsCommand ?? (_TopTrandingItemsCommand = new Command(async (obj) =>
                {
                    //if (IsLoggedIn)
                    //    await navigation.PushAsync(new SellerClosetDetailsView((DashBoardModel)obj));
                    //else
                    try
                    {
                        if (IsTap)
                            return;
                        IsTap = true;

                        Global.SetRecentViewList((DashBoardModel)obj);
                        await navigation.PushAsync(new ItemDetailsPage((DashBoardModel)obj, IsLoggedIn));
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }

                }));

            }
        }

        private ICommand _SearchCommand;
        public ICommand SearchCommand
        {
            get
            {
                if (_SearchCommand == null)
                {
                    _SearchCommand = new Command(async () =>
                    {
                        try
                        {
                            SearchClick();
                        }
                        catch (Exception ex)
                        {
                            IsTap = false;
                        }
                    });
                }
                return _SearchCommand;
            }
        }

        //private ICommand _SearchClrTxtCommand;
        //public ICommand SearchClrTxtCommand
        //{
        //    get
        //    {
        //        if (_SearchClrTxtCommand == null)
        //        {
        //            _SearchClrTxtCommand = new Command(async () =>
        //            {
        //                try
        //                {
        //                    if (!String.IsNullOrEmpty(SearchText))
        //                    {
        //                        PageNumber = 1;
        //                        TopTrendingItemsList.Clear();
        //                        GetTopTrendingItemsListMethod();
        //                        SearchText = String.Empty;
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    IsTap = false;
        //                }
        //            });
        //        }
        //        return _SearchClrTxtCommand;
        //    }
        //}

        #endregion
    }
}
