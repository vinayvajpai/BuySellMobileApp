using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
using BuySell.Helper;
using BuySell.Model;
using BuySell.Model.RestResponse;
using BuySell.WebServices;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace BuySell.ViewModel
{
    public class SearchGroupViewModel : BaseViewModel
    {
        public int PageNumber = 1;
        public int PageSize = 20;
        public Command SearchCommand { get; set; }
        public ProductPagingListResponse ClothingproductListResponses = null;
        public ProductPagingListResponse SneekarproductListResponses = null;
        public ProductPagingListResponse StreetproductListResponses = null;
        public ProductPagingListResponse VintageproductListResponses = null;
        public SearchResponseModel searchResponseModel;

        #region Constructor
        public SearchGroupViewModel(INavigation _nav,string searchTxt)
        {
            SearchText = searchTxt;
            SearchCommand = new Command(() =>
            {
                 GetSearchProductMethod();
            });
            navigation = _nav;
            Task.Run(async () =>
            {
                await GetSearchProductMethod();
            });
           
        }
        #endregion

        #region Properties
        private ObservableCollection<SearchResultModel> _SearchResultList = new ObservableCollection<SearchResultModel>();
        public ObservableCollection<SearchResultModel> SearchResultList
        {
            get { return _SearchResultList; }
            set { _SearchResultList = value; OnPropertyChanged("SearchResultList"); }
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
        private string _SearchText;
        public string SearchText
        {
            get { return _SearchText; }
            set
            {
                
                _SearchText = value;
                OnPropertyChanged("SearchText");
            }
        }
        #endregion

        #region Methods
        public async Task GetClothingProductMethod()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                Acr.UserDialogs.UserDialogs.Instance.Alert("Please enter search text.");
                return;
            }

            ProductPagingListResponse productListResponses=null;
            try
            {
                if(!Constant.IsConnected)
                {
                    UserDialogs.Instance.Toast("Internet not available");
                    IsTap = false;
                    return;
                }
                Acr.UserDialogs.UserDialogs.Instance.ShowLoading();
                await Task.Delay(50);
                SearchResultList.Clear();
                var req = new ProductRequestModel();
                req.PageNumber = PageNumber;
                req.PageSize = PageSize;
                req.Search = SearchText.Trim();
                string methodUrl;
                methodUrl = "/api/Product/GetAllProducts";
                req.ExtraParam = new ExtraParam();
                req.ExtraParam.StoreId = "1";

                RestResponseModel responseModel = await WebService.PostData(req, methodUrl, false);
                if (responseModel != null)
                {
                    productListResponses = JsonConvert.DeserializeObject<ProductPagingListResponse>(responseModel.response_body);
                    if (productListResponses != null)
                    {
                        if (productListResponses.StatusCode == 200)
                        {
                            if (productListResponses.Data != null)
                            {
                                ClothingproductListResponses = productListResponses;
                                ClothingProductList = new ObservableCollection<DashBoardModel>(productListResponses.Data.Data.ToList());
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
                            IsTap = false;
                            Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                            UserDialogs.Instance.HideLoading();
                        }

                    }
                    
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (productListResponses != null && productListResponses.Data != null)
                {
                    SearchResultList.Add(new SearchResultModel()
                    {
                        StarImage = Constant.StartIconStr,
                        Description = "Clothing (" + productListResponses.Data.TotalRowCount + ")",
                        NextImage = Constant.NextImageStr,
                        TextColor = Color.FromHex("#1567A6"),
                        StoreID = "1"
                    });
                }
                await GetSneakerProductMethod();
            }
            
        }
        private async Task GetSneakerProductMethod()
        {
            ProductPagingListResponse productListResponses = null;
            try
            {
                if(!Constant.IsConnected)
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
                methodUrl = "/api/Product/GetAllProducts";
                req.ExtraParam = new ExtraParam();
                req.ExtraParam.StoreId = "2";

                RestResponseModel responseModel = await WebService.PostData(req, methodUrl, false);
                if (responseModel != null)
                {
                     productListResponses = JsonConvert.DeserializeObject<ProductPagingListResponse>(responseModel.response_body);
                    if (productListResponses != null)
                    {
                        if (productListResponses.StatusCode == 200)
                        {
                            if (productListResponses.Data != null)
                            {
                                SneekarproductListResponses = productListResponses;
                                SneakersProductList = new ObservableCollection<DashBoardModel>(productListResponses.Data.Data.ToList());
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
                                IsTap = false;
                                Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                                UserDialogs.Instance.HideLoading();
                            }

                        }
                        
                    }
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (productListResponses != null && productListResponses.Data != null)
                {
                    SearchResultList.Add(new SearchResultModel()
                    {
                        StarImage = Constant.StartIconStr,
                        Description = "Sneakers (" + productListResponses.Data.TotalRowCount + ")",
                        NextImage = Constant.NextImageStr,
                        TextColor = Color.FromHex("#C52036"),
                        StoreID = "2"
                    });
                }
                await GetStreetwearProductMethod();
            }
           
        }
        private async Task GetStreetwearProductMethod()
        {
            ProductPagingListResponse productListResponses = null;
            try
            {
                if(!Constant.IsConnected)
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
                methodUrl = "/api/Product/GetAllProducts";
                req.ExtraParam = new ExtraParam();
                req.ExtraParam.StoreId = "3";

                RestResponseModel responseModel = await WebService.PostData(req, methodUrl, false);
                if (responseModel != null)
                {
                    productListResponses = JsonConvert.DeserializeObject<ProductPagingListResponse>(responseModel.response_body);
                    if (productListResponses != null)
                    {
                        if (productListResponses.StatusCode == 200)
                        {
                            if (productListResponses.Data != null)
                            {
                                StreetproductListResponses = productListResponses;
                                StreetwearProductList = new ObservableCollection<DashBoardModel>(productListResponses.Data.Data.ToList());
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
                            IsTap = false;
                            Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                            UserDialogs.Instance.HideLoading();
                        }

                    }
                    
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (productListResponses != null && productListResponses.Data != null)
                {
                    SearchResultList.Add(new SearchResultModel()
                    {
                        StarImage = Constant.StartIconStr,
                        Description = "Streetwear (" + productListResponses.Data.TotalRowCount + ")",
                        NextImage = Constant.NextImageStr,
                        TextColor = Color.FromHex("#D04107"),
                        StoreID = "3"
                    });
                }
                await GetVintageProductMethod();
            }
            
        }
        private async Task GetVintageProductMethod()
        {
            ProductPagingListResponse productListResponses = null;
            try
            {
                if(!Constant.IsConnected)
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
                methodUrl = "/api/Product/GetAllProducts";
                req.ExtraParam = new ExtraParam();
                req.ExtraParam.StoreId = "4";

                RestResponseModel responseModel = await WebService.PostData(req, methodUrl, false);
                if (responseModel != null)
                {
                    productListResponses = JsonConvert.DeserializeObject<ProductPagingListResponse>(responseModel.response_body);
                    if (productListResponses != null)
                    {
                        if (productListResponses.StatusCode == 200)
                        {
                            if (productListResponses.Data != null)
                            {
                                VintageproductListResponses = productListResponses;
                                VintageProductList = new ObservableCollection<DashBoardModel>(productListResponses.Data.Data.ToList());
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
                if (productListResponses != null && productListResponses.Data != null)
                {
                    SearchResultList.Add(new SearchResultModel()
                    {
                        StarImage = Constant.StartIconStr,
                        Description = "Vintage (" + productListResponses.Data.TotalRowCount + ")",
                        NextImage = Constant.NextImageStr,
                        TextColor = Color.FromHex("#467904"),
                        StoreID = "4"
                    });
                }
            }
            
        }
        private async Task GetSearchProductMethod()
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
                if(!Constant.IsConnected)
                {
                    UserDialogs.Instance.Toast("Internet not available");
                    Acr.UserDialogs.UserDialogs.Instance.HideLoading();
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
                UserDialogs.Instance.HideLoading();
            }
            finally
            {
                Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                SearchResultList.Clear();
                if (productListResponses != null && productListResponses.Data != null)
                {
                    searchResponseModel = productListResponses;
                    SearchResultList.Add(new SearchResultModel()
                    {
                        StarImage = Constant.StartIconStr,
                        Description = "Clothing ("+ (productListResponses.Data.Count > 0? productListResponses.Data[0].Counter.ToString():"0")+")",
                        NextImage = Constant.NextImageStr,
                        TextColor = Color.FromHex("#1567A6"),
                        StoreID = "1"
                    });
                    ClothingproductListResponses = new ProductPagingListResponse() {
                        Data = productListResponses.Data[0].Data
                    };
                    SearchResultList.Add(new SearchResultModel()
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
                    SearchResultList.Add(new SearchResultModel()
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
                    SearchResultList.Add(new SearchResultModel()
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
                    SearchResultList.Add(new SearchResultModel()
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
                    SearchResultList.Add(new SearchResultModel()
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
                    SearchResultList.Add(new SearchResultModel()
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
                    SearchResultList.Add(new SearchResultModel()
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
            }

        }
        #endregion
    }
}
