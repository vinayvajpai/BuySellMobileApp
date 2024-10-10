using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
using BuySell.Helper;
using BuySell.Model;
using BuySell.Model.LoginResponse;
using BuySell.Model.RestResponse;
using BuySell.View;
using BuySell.Views;
using BuySell.WebServices;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace BuySell.ViewModel
{
    public class SearchResultProdcutViewModel : BaseViewModel
    {
        public int PageNumber = 1;
        public string storeID = "1";
        public int count = 0;
        public ProductFilterModel productFilterModel;

        public Command searchCommand { get; set; }
        public ProductPagingListResponse productPagingListResponse;
        public ProductPagingListResponse ClothingproductListResponses = null;
        public ProductPagingListResponse SneekarproductListResponses = null;
        public ProductPagingListResponse StreetproductListResponses = null;
        public ProductPagingListResponse VintageproductListResponses = null;

        #region Constructor
        public SearchResultProdcutViewModel(INavigation _nav, string searchTxt)
        {
            navigation = _nav;
            SearchText = searchTxt;
            ItemTreshold = 1;
            ItemTresholdReachedCommand = new Command(async () => await ItemsTresholdReached());
            if (Device.RuntimePlatform == Device.iOS)
            {
                CallGetSearchProductMethod();
            }
            Global.SearchResultSelStore = storeID;
        }
        public SearchResultProdcutViewModel(INavigation _nav, string searchTxt, ProductPagingListResponse productListRes, string storeID)
        {
            navigation = _nav;
            SearchText = searchTxt;
            this.storeID = storeID;
            searchCommand = new Command(() =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await GetSearchProductListMethod();
                });
            });

            Device.StartTimer(TimeSpan.FromMilliseconds(10), () =>
            {
                productPagingListResponse = productListRes;
                if (productPagingListResponse != null && productPagingListResponse.Data != null)
                {
                    //SearchProductList = new ObservableCollection<DashBoardModel>(productPagingListResponse.Data.Data.ToList());
                    foreach (var item in productPagingListResponse.Data.Data)
                    {
                        SearchProductList.Add(item);
                    }

                    if (productPagingListResponse.Data.Data.Count() == 0)
                    {
                        ItemTreshold = -1;
                    }
                    if (SearchProductList.Count > 0)
                    {
                        if ((PageNumber * PageSize) < productPagingListResponse.Data.TotalRowCount)
                            TotalCount = "Showing 1 to " + (PageNumber * PageSize) + " of " + productPagingListResponse.Data.TotalRowCount + " Items";
                        else
                            TotalCount = "Showing 1 to " + productPagingListResponse.Data.TotalRowCount + " of " + productPagingListResponse.Data.TotalRowCount + " Items";
                    }
                    PageNumber = productPagingListResponse.Data.NextPageNumber;
                    IsLoadMore = false;
                    Device.StartTimer(TimeSpan.FromSeconds(1), () => {
                        sizeByProductList = new ObservableCollection<string>(SearchProductList.Select(s => s.Size).Distinct().ToList().ToList().OrderBy(x => x));
                        brandList = new ObservableCollection<string>(SearchProductList.Select(s => s.Brand).Distinct().ToList().ToList().OrderBy(x => x));
                        return false;
                    });
                }
                IsNoData = SearchProductList.Count > 0 ? false : true;
                ItemTreshold = 1;
                ItemTresholdReachedCommand = new Command(async () => await ItemsTresholdReached());
                IsFilterChanged = false;
                return false;
            });
            Global.SearchResultSelStore = storeID;
        }
        public SearchResultProdcutViewModel(INavigation _nav, string searchTxt, ProductPagingListResponse productListRes, string storeID, SearchResponseModel searchResponseModel)
        {
            navigation = _nav;
            SearchText = searchTxt;
            this.storeID = storeID;
            searchCommand = new Command(() =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await GetSearchProductListMethod();
                });
            });

            productPagingListResponse = productListRes;
            if (productPagingListResponse != null && productPagingListResponse.Data != null)
            {
                //SearchProductList = new ObservableCollection<DashBoardModel>(productPagingListResponse.Data.Data.ToList());
                foreach (var item in productPagingListResponse.Data.Data)
                {
                    SearchProductList.Add(item);
                }

                if (productPagingListResponse.Data.Data.Count() == 0)
                {
                    ItemTreshold = -1;
                }
                if (SearchProductList.Count > 0)
                {
                    if ((PageNumber * PageSize) < productPagingListResponse.Data.TotalRowCount)
                        TotalCount = "Showing 1 to " + (PageNumber * PageSize) + " of " + productPagingListResponse.Data.TotalRowCount + " Items";
                    else
                        TotalCount = "Showing 1 to " + productPagingListResponse.Data.TotalRowCount + " of " + productPagingListResponse.Data.TotalRowCount + " Items";
                }
                PageNumber = productPagingListResponse.Data.NextPageNumber;
                IsLoadMore = false;
                Device.StartTimer(TimeSpan.FromSeconds(1),()=>{
                    sizeByProductList = new ObservableCollection<string>(SearchProductList.Select(s => s.Size).Distinct().ToList().ToList().OrderBy(x => x));
                    brandList = new ObservableCollection<string>(SearchProductList.Select(s => s.Brand).Distinct().ToList().ToList().OrderBy(x => x));
                    return false;
                });
            }
            IsNoData = SearchProductList.Count > 0 ? false : true;
            ItemTreshold = 1;
            ItemTresholdReachedCommand = new Command(async () => await ItemsTresholdReached());
            searchResultList.Clear();
            if (searchResponseModel != null && searchResponseModel.Data != null)
            {
                searchResultList.Add(new SearchResultModel()
                {
                    StarImage = Constant.StartIconStr,
                    Description = "Clothing (" + (searchResponseModel.Data.Count > 0 ? searchResponseModel.Data[0].Counter.ToString() : "0") + ")",
                    NextImage = Constant.NextImageStr,
                    TextColor = Color.FromHex("#1567A6"),
                    StoreID = "1"
                });
                ClothingproductListResponses = new ProductPagingListResponse()
                {
                    Data = searchResponseModel.Data[0].Data
                };
                searchResultList.Add(new SearchResultModel()
                {
                    StarImage = Constant.StartIconStr,
                    Description = "Sneakers (" + (searchResponseModel.Data.Count > 1 ? searchResponseModel.Data[1].Counter.ToString() : "0") + ")",
                    NextImage = Constant.NextImageStr,
                    TextColor = Color.FromHex("#C52036"),
                    StoreID = "2"
                });
                SneekarproductListResponses
                   = new ProductPagingListResponse()
                   {
                       Data = searchResponseModel.Data[1].Data
                   };
                searchResultList.Add(new SearchResultModel()
                {
                    StarImage = Constant.StartIconStr,
                    Description = "Streetwear (" + (searchResponseModel.Data.Count > 2 ? searchResponseModel.Data[2].Counter.ToString() : "0") + ")",
                    NextImage = Constant.NextImageStr,
                    TextColor = Color.FromHex("#D04107"),
                    StoreID = "3"
                });
                StreetproductListResponses
                  = new ProductPagingListResponse()
                  {
                      Data = searchResponseModel.Data[2].Data
                  };
                searchResultList.Add(new SearchResultModel()
                {
                    StarImage = Constant.StartIconStr,
                    Description = "Vintage (" + (searchResponseModel.Data.Count > 3 ? searchResponseModel.Data[3].Counter.ToString() : "0") + ")",
                    NextImage = Constant.NextImageStr,
                    TextColor = Color.FromHex("#467904"),
                    StoreID = "4"
                });
                VintageproductListResponses
                  = new ProductPagingListResponse()
                  {
                      Data = searchResponseModel.Data[3].Data
                  };
            }
            else
            {
                searchResultList.Add(new SearchResultModel()
                {
                    StarImage = Constant.StartIconStr,
                    Description = Constant.ClothingZeroStr,
                    NextImage = Constant.NextImageStr,
                    TextColor = Color.FromHex("#1567A6"),
                    StoreID = "1"
                });
                ClothingproductListResponses = new ProductPagingListResponse()
                {
                    Data = new ProductPagingListData() { }
                };
                searchResultList.Add(new SearchResultModel()
                {
                    StarImage = Constant.StartIconStr,
                    Description = Constant.SneakersZeroStr,
                    NextImage = Constant.NextImageStr,
                    TextColor = Color.FromHex("#C52036"),
                    StoreID = "2"
                });
                SneekarproductListResponses
                   = new ProductPagingListResponse()
                   {
                       Data = new ProductPagingListData() { }
                   };
                searchResultList.Add(new SearchResultModel()
                {
                    StarImage = Constant.StartIconStr,
                    Description = Constant.StreetwearZeroStr,
                    NextImage = Constant.NextImageStr,
                    TextColor = Color.FromHex("#D04107"),
                    StoreID = "3"
                });
                StreetproductListResponses
                  = new ProductPagingListResponse()
                  {
                      Data = new ProductPagingListData() { }
                  };
                searchResultList.Add(new SearchResultModel()
                {
                    StarImage = Constant.StartIconStr,
                    Description = Constant.VintageStr,
                    NextImage = Constant.NextImageStr,
                    TextColor = Color.FromHex("#467904"),
                    StoreID = "4"
                });
                VintageproductListResponses
                  = new ProductPagingListResponse()
                  {
                      Data = new ProductPagingListData() { }
                  };

            }
            IsFilterChanged = false;
            Global.SearchResultSelStore = storeID;
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

        private ObservableCollection<DashBoardModel> _SearchProductList = new ObservableCollection<DashBoardModel>();
        public ObservableCollection<DashBoardModel> SearchProductList
        {
            get { return _SearchProductList; }
            set { _SearchProductList = value; OnPropertyChanged("SearchProductList"); }
        }

        private string _SearchText;
        public string SearchText
        {
            get { return _SearchText; }
            set { _SearchText = value; OnPropertyChanged("SearchText"); }
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

        private ObservableCollection<SearchResultModel> _SearchResultList = new ObservableCollection<SearchResultModel>();
        public ObservableCollection<SearchResultModel> SearchResultList
        {
            get { return _SearchResultList; }
            set { _SearchResultList = value; OnPropertyChanged("SearchResultList"); }
        }

        private ObservableCollection<SearchResultModel> _searchResultList = new ObservableCollection<SearchResultModel>();
        public ObservableCollection<SearchResultModel> searchResultList
        {
            get { return _searchResultList; }
            set { _searchResultList = value; OnPropertyChanged("searchResultList"); }
        }

        private ObservableCollection<DashBoardModel> _ClothingProductList = new ObservableCollection<DashBoardModel>();
        public ObservableCollection<DashBoardModel> ClothingProductList
        {
            get { return _ClothingProductList; }
            set { _ClothingProductList = value; OnPropertyChanged("ClothingProductList"); }
        }

        private ObservableCollection<DashBoardModel> _SneakersProductList = new ObservableCollection<DashBoardModel>();
        public ObservableCollection<DashBoardModel> SneakersProductList
        {
            get { return _SneakersProductList; }
            set { _SneakersProductList = value; OnPropertyChanged("SneakersProductList"); }
        }

        private ObservableCollection<DashBoardModel> _StreetwearProductList = new ObservableCollection<DashBoardModel>();
        public ObservableCollection<DashBoardModel> StreetwearProductList
        {
            get { return _StreetwearProductList; }
            set { _StreetwearProductList = value; OnPropertyChanged("StreetwearProductList"); }
        }

        private ObservableCollection<DashBoardModel> _VintageProductList = new ObservableCollection<DashBoardModel>();
        public ObservableCollection<DashBoardModel> VintageProductList
        {
            get { return _VintageProductList; }
            set { _VintageProductList = value; OnPropertyChanged("VintageProductList"); }
        }

        private bool _CategoryPopupIsVisible = false;
        public bool CategoryPopupIsVisible
        {
            get { return _CategoryPopupIsVisible; }
            set
            {
                _CategoryPopupIsVisible = value;
                OnPropertyChanged("CategoryPopupIsVisible");
            }

        }

        private bool _ResultListIsVisible = true;
        public bool ResultListIsVisible
        {
            get { return _ResultListIsVisible; }
            set
            {
                _ResultListIsVisible = value;
                OnPropertyChanged("ResultListIsVisible");
            }

        }

        private bool _FilterViewIsVisible = true;
        public bool FilterViewIsVisible
        {
            get { return _FilterViewIsVisible; }
            set
            {
                _FilterViewIsVisible = value;
                OnPropertyChanged("FilterViewIsVisible");
            }

        }

        private bool _BoxViewIsVisible = true;
        public bool BoxViewIsVisible
        {
            get { return _BoxViewIsVisible; }
            set
            {
                _BoxViewIsVisible = value;
                OnPropertyChanged("BoxViewIsVisible");
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
        #endregion

        #region Methods
        public async Task CallGetSearchProductMethod()
        {
            try
            {
                if (string.IsNullOrEmpty(SearchText))
                {
                    Acr.UserDialogs.UserDialogs.Instance.Alert("Please enter search text.");
                    return;
                }

                Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Loading Items, Please wait");
                await Task.Delay(100);
                await Device.InvokeOnMainThreadAsync(async () =>
                {
                    await GetSearchProductMethod();

                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        private async Task GetSearchProductMethod()
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

                var req = new ProductRequestModel();
                req.PageNumber = PageNumber;
                req.PageSize = PageSize;
                req.Search = SearchText.Trim();
                string methodUrl;
                methodUrl = "/api/Product/GetAllProducts";
                req.ExtraParam = new ExtraParam();
                req.ExtraParam.StoreId = storeID;

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
                                        SearchProductList.Add(item);
                                    }

                                    if (productListResponses.Data.Data.Count() == 0)
                                    {
                                        ItemTreshold = -1;
                                        //return;
                                    }
                                    if (SearchProductList.Count > 0)
                                    {
                                        if ((PageNumber * PageSize) < productListResponses.Data.TotalRowCount)
                                            TotalCount = "Showing 1 to " + (PageNumber * PageSize) + " of " + productListResponses.Data.TotalRowCount + " Items";
                                        else
                                            TotalCount = "Showing 1 to " + productListResponses.Data.TotalRowCount + " of " + productListResponses.Data.TotalRowCount + " Items";

                                    }
                                    PageNumber = productPagingListResponse.Data.NextPageNumber;
                                    IsLoadMore = false;
                                    Device.StartTimer(TimeSpan.FromSeconds(1), () => {
                                        sizeByProductList = new ObservableCollection<string>(SearchProductList.Select(s => s.Size).Distinct().ToList().ToList().OrderBy(x => x));
                                        brandList = new ObservableCollection<string>(SearchProductList.Select(s => s.Brand).Distinct().ToList().ToList().OrderBy(x => x));
                                        return false;
                                    });
                                }
                                else
                                {
                                    Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                                    UserDialogs.Instance.HideLoading();
                                    IsLoadMore = false;
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
            IsNoData = SearchProductList.Count > 0 ? false : true;

        }
        async Task ItemsTresholdReached()
        {
            if (SearchProductList.Count == 0 && IsFilterChanged == true)
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
                        await GetSearchProductMethod();
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
        public async Task GetSearchProductListMethod()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                Acr.UserDialogs.UserDialogs.Instance.Alert("Please enter search text.");
                return;
            }
            Acr.UserDialogs.UserDialogs.Instance.ShowLoading();
            await Task.Delay(50);
            SearchResponseModel productListResponses = null;
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
                req.Search = SearchText.Trim();
                string methodUrl;
                methodUrl = "/api/Product/SearchAllProducts";
                RestResponseModel responseModel = await WebService.PostData(req, methodUrl, false);
                if (responseModel != null)
                {
                    productListResponses = JsonConvert.DeserializeObject<SearchResponseModel>(responseModel.response_body);
                    if (productListResponses != null)
                    {
                        if (productListResponses.StatusCode == 200)
                        {
                            if (productListResponses.Data != null)
                            {
                                //VintageproductListResponses = productListResponses;
                                //VintageProductList = new ObservableCollection<DashBoardModel>(productListResponses.Data.Data.ToList());
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
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                searchResultList.Clear();
                if (productListResponses != null && productListResponses.Data != null)
                {
                    searchResultList.Add(new SearchResultModel()
                    {
                        StarImage = Constant.StartIconStr,
                        Description = "Clothing (" + (productListResponses.Data.Count > 0 ? productListResponses.Data[0].Counter.ToString() : "0") + ")",
                        NextImage = Constant.NextImageStr,
                        TextColor = Color.FromHex("#1567A6"),
                        StoreID = "1"
                    });
                    ClothingproductListResponses = new ProductPagingListResponse()
                    {
                        Data = productListResponses.Data[0].Data
                    };
                    searchResultList.Add(new SearchResultModel()
                    {
                        StarImage = Constant.StartIconStr,
                        Description = "Sneakers (" + (productListResponses.Data.Count > 1 ? productListResponses.Data[1].Counter.ToString() : "0") + ")",
                        NextImage = Constant.NextImageStr,
                        TextColor = Color.FromHex("#C52036"),
                        StoreID = "2"
                    });
                    SneekarproductListResponses
                        = new ProductPagingListResponse()
                        {
                            Data = productListResponses.Data[1].Data
                        };
                    searchResultList.Add(new SearchResultModel()
                    {
                        StarImage = Constant.StartIconStr,
                        Description = "Streetwear (" + (productListResponses.Data.Count > 2 ? productListResponses.Data[2].Counter.ToString() : "0") + ")",
                        NextImage = Constant.NextImageStr,
                        TextColor = Color.FromHex("#D04107"),
                        StoreID = "3"
                    });
                    StreetproductListResponses
                       = new ProductPagingListResponse()
                       {
                           Data = productListResponses.Data[2].Data
                       };
                    searchResultList.Add(new SearchResultModel()
                    {
                        StarImage = Constant.StartIconStr,
                        Description = "Vintage (" + (productListResponses.Data.Count > 3 ? productListResponses.Data[3].Counter.ToString() : "0") + ")",
                        NextImage = Constant.NextImageStr,
                        TextColor = Color.FromHex("#467904"),
                        StoreID = "4"
                    });
                    VintageproductListResponses
                       = new ProductPagingListResponse()
                       {
                           Data = productListResponses.Data[3].Data
                       };
                }
                else
                {
                    searchResultList.Add(new SearchResultModel()
                    {
                        StarImage = Constant.StartIconStr,
                        Description = Constant.ClothingZeroStr,
                        NextImage = Constant.NextImageStr,
                        TextColor = Color.FromHex("#1567A6"),
                        StoreID = "1"
                    });
                    ClothingproductListResponses = new ProductPagingListResponse()
                    {
                        Data = new ProductPagingListData() { }
                    };
                    searchResultList.Add(new SearchResultModel()
                    {
                        StarImage = Constant.StartIconStr,
                        Description = Constant.SneakersZeroStr,
                        NextImage = Constant.NextImageStr,
                        TextColor = Color.FromHex("#C52036"),
                        StoreID = "2"
                    });
                    SneekarproductListResponses
                        = new ProductPagingListResponse()
                        {
                            Data = new ProductPagingListData() { }
                        };
                    searchResultList.Add(new SearchResultModel()
                    {
                        StarImage = Constant.StartIconStr,
                        Description = Constant.StreetwearZeroStr,
                        NextImage = Constant.NextImageStr,
                        TextColor = Color.FromHex("#D04107"),
                        StoreID = "3"
                    });
                    StreetproductListResponses
                       = new ProductPagingListResponse()
                       {
                           Data = new ProductPagingListData() { }
                       };
                    searchResultList.Add(new SearchResultModel()
                    {
                        StarImage = Constant.StartIconStr,
                        Description = Constant.VintageZeroStr,
                        NextImage = Constant.NextImageStr,
                        TextColor = Color.FromHex("#467904"),
                        StoreID = "4"
                    });
                    VintageproductListResponses
                       = new ProductPagingListResponse()
                       {
                           Data = new ProductPagingListData() { }
                       };
                }
                IsFilterChanged = false;
            }

        }
        public async Task GetListData(INavigation _nav, string searchTxt, ProductPagingListResponse productListRes, string storeID)
        {
            SearchProductList.Clear();
            ResultListIsVisible = true;
            FilterViewIsVisible = true;
            BoxViewIsVisible = true;
            CategoryPopupIsVisible = false;
            Opacity = 1;

            SearchText = searchTxt.Trim();
            this.storeID = storeID;

            productPagingListResponse = productListRes;
            if (productPagingListResponse != null && productPagingListResponse.Data != null)
            {
                //SearchProductList = new ObservableCollection<DashBoardModel>(productPagingListResponse.Data.Data.ToList());
                foreach (var item in productPagingListResponse.Data.Data)
                {
                    SearchProductList.Add(item);
                }

                if (productPagingListResponse.Data.Data.Count() == 0)
                {
                    ItemTreshold = -1;
                }
                if (SearchProductList.Count > 0)
                {
                    if ((PageNumber * PageSize) < productPagingListResponse.Data.TotalRowCount)
                        TotalCount = "Showing 1 to " + (PageNumber * PageSize) + " of " + productPagingListResponse.Data.TotalRowCount + " Items";
                    else
                        TotalCount = "Showing 1 to " + productPagingListResponse.Data.TotalRowCount + " of " + productPagingListResponse.Data.TotalRowCount + " Items";
                }
                PageNumber = productPagingListResponse.Data.NextPageNumber;
                IsLoadMore = false;
                Device.StartTimer(TimeSpan.FromSeconds(1), () => {
                    sizeByProductList = new ObservableCollection<string>(SearchProductList.Select(s => s.Size).Distinct().ToList().ToList().OrderBy(x => x));
                    brandList = new ObservableCollection<string>(SearchProductList.Select(s => s.Brand).Distinct().ToList().ToList().OrderBy(x => x));
                    return false;
                });
            }
            IsNoData = SearchProductList.Count > 0 ? false : true;
            ItemTreshold = 1;
            ItemTresholdReachedCommand = new Command(async () => await ItemsTresholdReached());
            Global.SearchResultSelStore = storeID;
        }

        #region Filter Methods
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
                UserDialogs.Instance.ShowLoading();
                Task.Delay(50);
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

                                    productPagingListResponse = productListResponses;
                                    IsBusy = false;
                                    foreach (var item in productListResponses.Data.Data)
                                    {
                                        SearchProductList.Add(item);
                                    }

                                    if (productListResponses.Data.Data.Count() == 0)
                                    {
                                        ItemTreshold = -1;
                                        //return;
                                    }
                                    if (SearchProductList.Count > 0)
                                    {
                                        if ((PageNumber * PageSize) < productListResponses.Data.TotalRowCount)
                                            TotalCount = "Showing 1 to " + (PageNumber * PageSize) + " of " + productListResponses.Data.TotalRowCount + " Items";
                                        else
                                            TotalCount = "Showing 1 to " + productListResponses.Data.TotalRowCount + " of " + productListResponses.Data.TotalRowCount + " Items";

                                    }
                                    PageNumber = productPagingListResponse.Data.NextPageNumber;
                                    IsLoadMore = false;
                                    Device.StartTimer(TimeSpan.FromSeconds(1), () => {
                                        sizeByProductList = new ObservableCollection<string>(SearchProductList.Select(s => s.Size).Distinct().ToList().ToList().OrderBy(x => x));
                                        brandList = new ObservableCollection<string>(SearchProductList.Select(s => s.Brand).Distinct().ToList().ToList().OrderBy(x => x));
                                        return false;
                                    });
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
            IsNoData = SearchProductList.Count > 0 ? false : true;
        }
        public async void SetProductFilterRequest(List<FilterModel> listFilter)
        {
            try
            {
                productFilterModel = new ProductFilterModel();
                productFilterModel.StoreId = Convert.ToInt16(storeID);
                productFilterModel.Search = SearchText.Trim();
                productFilterModel.PageSize = PageSize;
                productFilterModel.PageNumber = PageNumber;
                productFilterModel.Price = new PriceFilter();
                productFilterModel.ShippingPrice = new PriceFilter();
                productFilterModel.Sort = new SortingFilter();
                productFilterModel.UserId = null;
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
                    if (selectedShipPrice.ToLower().Trim().Equals(Constant.AllStr.ToLower()))
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
                else if (selectedSort.Trim().ToLower().Equals(Constant.AllStr.ToLower()))
                {

                }
                else
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
                if (Categories.Trim().ToLower().Equals(Constant.AllStr.ToLower()))
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
                                //filterCat[1].Trim().ToLower().Equals("coats")?"JACKETS & COATS":filterCat[1].Trim(),
                            };

                        }
                        else if (filterCat.Length > 2)
                        {
                            productFilterModel.RootCategories = new List<int> { filterCat[0].Trim().ToLower() == "men" ? 1 : 2 };
                            productFilterModel.Categories = new List<string>() {
                                filterCat[1].Trim(),
                                //filterCat[1].Trim().ToLower().Equals("coats")?"JACKETS & COATS":filterCat[1].Trim(),
                                filterCat[2].Trim()
                            };
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
                SearchProductList.Clear();
                await GetProductByFilterMethod(productFilterModel);
            }
            catch (Exception ex)
            {
            }
        }
        public async Task GetProductBySelectedFilterRequest()
        {
            try
            {
                PageNumber = 1;
                SearchProductList.Clear();
                await GetProductByFilterMethod(productFilterModel);
            }
            catch (Exception ex)
            {

            }
        }

        #endregion

        #endregion

        #region Commands
        private Command _SelectStoreCommand;
        public Command SelectStoreCommand
        {
            get
            {
                //return _SelectStoreCommand ?? (_SelectStoreCommand = new Command(async (obj) => await navigation.PopAsync()));
                return _SelectStoreCommand ?? (_SelectStoreCommand = new Command(() =>
                {
                    if (count == 0)
                    {
                        //ResultListIsVisible = false;
                        //FilterViewIsVisible = false;
                        //BoxViewIsVisible = false;
                        //IsLoadMore = false;
                        CategoryPopupIsVisible = true;
                        Opacity = 0.5;
                        count = 1;
                    }
                    else
                    {

                        //FilterViewIsVisible = true;
                        //BoxViewIsVisible = true;
                        //IsLoadMore = true;
                        CategoryPopupIsVisible = false;
                        Opacity = 1;
                        count = 0;
                    }

                }));

            }
        }

        private Command _SelectItemsCommand;
        public Command SelectItemsCommand
        {
            get
            {
                return _SelectItemsCommand ?? (_SelectItemsCommand = new Command(async (obj) => await SelectItemsCommandCommond(obj)));

            }
        }
        private async Task SelectItemsCommandCommond(object obj)
        {
            try
            {
                if (IsTap)
                    return;
                IsTap = true;
                Global.SetRecentViewList((DashBoardModel)obj);
                await navigation.PushAsync(new ItemDetailsPage((DashBoardModel)obj, false));
            }
            catch (Exception ex)
            {
                IsTap = false;
                Debug.WriteLine(ex.Message);
            }

        }
        public Command ItemTresholdReachedCommand { get; set; }

        private Command _SearchCommand;
        public Command SearchCommand
        {
            get
            {
                return _SearchCommand ?? (_SearchCommand = new Command(async () =>
                {
                    SearchProductList.Clear();
                    PageNumber = 1;
                    await CallGetSearchProductMethod();
                }
                ));
            }
        }

        private Command _PopupCancelCommand;
        public Command PopupCancelCommand
        {
            get
            {
                return _PopupCancelCommand ?? (_PopupCancelCommand = new Command(async () =>
                {
                    CategoryPopupIsVisible = false;
                    count = 0;
                }));
            }
        }
        #endregion
    }
}
