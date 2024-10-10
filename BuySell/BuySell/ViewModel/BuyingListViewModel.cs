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
using BuySell.Views.BuyingSellingViews;
using BuySell.WebServices;
using Newtonsoft.Json;
using Stripe;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace BuySell.ViewModel
{
    public class BuyingListViewModel : BaseViewModel
    {
        public int PageNumber = 1;
        public ProductPagingListResponse SellPagingListResponse;
        public ProductPagingListResponse BuyPagingListResponse;
        public string Title;
        public BuyingListViewModel(string title, INavigation _nav)
        {
            navigation = _nav;
            Title = title;
            if (title.ToLower() == "buying")
            {

                IsSellListVisible = false;
                BuyItemThreshold = 1;
                BuyItemTresholdReachedCmd = new Command(async () => await BuyItemsTresholdReached());
                ShowToBuyer = true;
                Task.Run(() =>
                {
                    GetBuyingOrderList();
                });
            }
            else
            {
                //GetSellingOrderList();
                IsSellListVisible = false;
                SellItemThreshold = 1;
                SellItemTresholdReachedCmd = new Command(async () => await SellItemsTresholdReached());
                ShowToBuyer = false;
                Task.Run(() =>
                {
                    GetSellingOrderList();
                });

            }
        }

        public async Task SellItemsTresholdReached()
        {
            if (SellByProductList.Count == 0)
            {
                return;
            }

            if (!Constant.IsConnected)
            {
                UserDialogs.Instance.Toast("Internet not available");
                IsTap = false;
                return;
            }

            if (SellPagingListResponse != null)
            {
                if (SellPagingListResponse.Data.HasNextPage == false)
                {
                    IsSellLoadMore = false;
                    IsBusy = false;
                    return;
                }
            }

            if (IsSellLoadMore)
                return;

                await Task.Run(async () =>
                {
                    IsSellLoadMore = true;
                    await Task.Delay(50);
                    try
                    {
                        GetSellingOrderList();
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

        public async Task BuyItemsTresholdReached()
        {
            //if (BuyByProductList.Count == 0)
            //{
            //    return;
            //}

            //if (!Constant.IsConnected)
            //{
            //    UserDialogs.Instance.Toast("Internet not available");
            //    IsTap = false;
            //    return;
            //}

            //if (BuyPagingListResponse != null)
            //{
            //    if (BuyPagingListResponse.Data.HasNextPage == false)
            //    {
            //        IsBuyLoadMore = false;
            //        IsBusy = false;
            //        // return;
            //    }
            //}

            //if (IsBuyLoadMore)
            //    return;

            //    await Task.Run(async () =>
            //    {
            //        IsBuyLoadMore = true;
            //        await Task.Delay(50);
            //        try
            //        {
            //            GetBuyingOrderList();
            //        }
            //        catch (Exception ex)
            //        {
            //            Debug.WriteLine(ex);
            //            Acr.UserDialogs.UserDialogs.Instance.HideLoading();
            //        }
            //        finally
            //        {
            //            IsBusy = false;
            //            Acr.UserDialogs.UserDialogs.Instance.HideLoading();
            //        }
            //    });
        }

        #region Properties
        private ObservableCollection<BuyingSellingModel> _SellByProductList = new ObservableCollection<BuyingSellingModel>();
        public ObservableCollection<BuyingSellingModel> SellByProductList
        {
            get { return _SellByProductList; }
            set { _SellByProductList = value; OnPropertyChanged("SellByProductList"); }
        }

        private bool _ShowToBuyer;
        public bool ShowToBuyer
        {
            get
            {
                return _ShowToBuyer;
            }
            set
            {
                _ShowToBuyer = value;
                OnPropertyChanged("ShowToBuyer");
            }
        }
        
        private bool _ShowToSeller;
        public bool ShowToSeller
        {
            get
            {
                return _ShowToSeller;
            }
            set
            {
                _ShowToSeller = value;
                OnPropertyChanged("ShowToSeller");
            }
        }


        private ObservableCollection<BuyingSellingModel> _tempSellByProductList = new ObservableCollection<BuyingSellingModel>();
        public ObservableCollection<BuyingSellingModel> TempSellByProductList
        {
            get { return _tempSellByProductList; }
            set {
                _tempSellByProductList = value;
                OnPropertyChanged("TempSellByProductList");
                if (TempSellByProductList.Count == 0)
                    IsNoData = true;
                else
                    IsNoData = false;
            }
        }

        private BuyingSellingModel _SelectedOrderItem;
        public BuyingSellingModel SelectedOrderItem
        {
            get { return _SelectedOrderItem; }
            set {
                _SelectedOrderItem = value;
                OnPropertyChanged("SelectedOrderItem"); 
            if(_SelectedOrderItem != null)
                {
                    if(_SelectedOrderItem.OrderItems.Count > 0)
                    {
                        var prod = _SelectedOrderItem.OrderProductItem.Product;
                        _SelectedOrderItem.OrderProductItem.TotalPrice =Convert.ToString(Convert.ToDecimal(prod.Price.Replace("$", "")) +Convert.ToDecimal(prod.ShippingPrice.Replace("$", "")) +Convert.ToDecimal(_SelectedOrderItem.OrderProductItem.Tax.ToString().Replace("$", "")));
                        _SelectedOrderItem.OrderProductItem.TotalPrice = "$" + _SelectedOrderItem.OrderProductItem.TotalPrice;
                        if (Title.ToLower() == "buying")
                        {
                            NavigateToOrderSummary();
                        }
                        else
                        {
                            NavigateToShippingLabel();
                        }                   
                    }
                }
            }
        }
        private async void NavigateToShippingLabel()
        {
            if(SelectedOrderItem != null)
            {
                await navigation.PushAsync(new OrderDetailsShippingPage(SelectedOrderItem));
                Debug.WriteLine(JsonConvert.SerializeObject(SelectedOrderItem).ToString());
                SelectedOrderItem = null;
            }
        }
        private async void NavigateToOrderSummary()
        {
            if(SelectedOrderItem != null)
            {
                await navigation.PushAsync(new OrderSummaryView(SelectedOrderItem));
                SelectedOrderItem = null;
            }
        }

        private bool _IsSellLoadMore;
        public bool IsSellLoadMore
        {
            get => _IsSellLoadMore;
            set
            {
                _IsSellLoadMore = value;
                OnPropertyChanged("IsSellLoadMore");
            }
        }
        
        private bool _IsBuyLoadMore;
        public bool IsBuyLoadMore
        {
            get => _IsBuyLoadMore;
            set
            {
                _IsBuyLoadMore = value;
                OnPropertyChanged("IsBuyLoadMore");
            }
        }

        private string _SearchedString;
        public string SearchedString
        {
            get { return _SearchedString; }
            set
            {
                _SearchedString = value;
                OnPropertyChanged("SearchedString");
                //try
                //{

                //if (!string.IsNullOrEmpty(_SearchedString))
                //{
                //    var searchList = SellByProductList.Where(p => p.OrderProductItem!=null?(p.OrderProductItem.Product.ProductName.ToLower().Contains(_SearchedString.ToLower()) || p.OrderProductItem.Product.ProductCondition.ToLower().Contains(_SearchedString.ToLower()) || p.OrderProductItem.Product.Size.ToLower().Contains(_SearchedString.ToLower()) || p.OrderProductItem.Product.ProductCondition.ToLower().Contains(_SearchedString.ToLower()) || p.OrderProductItem.Product.Brand.ToLower().Contains(_SearchedString.ToLower()) || p.OrderProductItem.Product.Availability.ToLower().Contains(_SearchedString.ToLower())):false);
                //    TempSellByProductList = new ObservableCollection<BuyingSellingModel>(searchList);
                //}
                //else
                //{
                //    TempSellByProductList = new ObservableCollection<BuyingSellingModel>(SellByProductList.ToList());
                //}

                //}
                //catch (Exception ex)
                //{

                //}
            }
        }

        public int _BuyPageSize = 20;
        public int BuyPageSize
        {
            get
            {
                return _BuyPageSize;
            }
            set
            {
                _BuyPageSize = value;
                OnPropertyChanged("BuyPageSize");
            }
        }

        public int _SellPageSize = 20;
        public int SellPageSize
        {
            get
            {
                return _SellPageSize;
            }
            set
            {
                _SellPageSize = value;
                OnPropertyChanged("SellPageSize");
            }
        }


        public int _BuyItemThreshold = 20;
        public int BuyItemThreshold
        {
            get
            {
                return _BuyItemThreshold;
            }
            set
            {
                _BuyItemThreshold = value;
                OnPropertyChanged("BuyItemThreshold");
            }
        }

        public int _SellItemThreshold = 20;
        public int SellItemThreshold
        {
            get
            {
                return _SellItemThreshold;
            }
            set
            {
                _SellItemThreshold = value;
                OnPropertyChanged("SellItemThreshold");
            }
        }

        public Command BuyItemTresholdReachedCmd { get; set; }
        public Command SellItemTresholdReachedCmd { get; set; }

        public bool _IsSellListVisible = false;
        public bool IsSellListVisible
        {
            get
            {
                return _IsSellListVisible;
            }
            set
            {
                _IsSellListVisible = value;
                OnPropertyChanged("IsSellListVisible");
            }
        }

        #endregion

        #region Method
        public void SetData(int tabIndex)
        {
            if (tabIndex == 1)
            {
                if (Title.ToLower() == "buying")
                {
                    Task.Run(() =>
                    {
                         GetBuyingOrderList();
                    });
                }
                else
                {
                    Task.Run(async() =>
                    {
                        await GetSellingOrderList();
                    });
                }

                //var temp = SellByProductList.Where(s => s.OrderProductItem.ShipmentStatus != "3" || s.OrderProductItem.ShipmentStatus != "4").ToList();
                //TempSellByProductList = new ObservableCollection<BuyingSellingModel>(temp);
                //OnPropertyChanged("TempSellByProductList");
            }
            else
            {

                if (Title.ToLower() == "buying")
                {
                    Task.Run(async () =>
                    {
                       await GetCompleteBuyingOrderList();
                    });
                }
                else
                {
                    Task.Run(async() =>
                    {
                       await GetCompleteSellingOrderList();
                    });
                }
            }

        }

        public async void GetBuyingOrderList()
        {
            // string methodUrl = "/api/Order/GetAllOrdersForBuyer";
            try
            {
                if (!Constant.IsConnected)
                {
                    UserDialogs.Instance.Toast("Internet not available");
                    IsTap = false;
                    return;
                }


                Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Loading, please wait...");
                await Task.Delay(50);//{Constant.LoginUserData.Id}
                string methodUrl = $"/api/Order/GetAllOrdersForBuyer?id={Constant.LoginUserData.Id}&UserId={0}&Search={SearchedString}&PageNumber={1}&PageSize={20}&additionalProp1={null}&additionalProp2={null}&additionalProp3={null}";
                RestResponseModel responseModel = await WebService.GetData(methodUrl, true);
                if (responseModel != null)
                {
                    if (responseModel.status_code == 200)
                    {
                        var buyingSellingProducts = JsonConvert.DeserializeObject<List<BuyingSellingModel>>(responseModel.response_body);
                        SellByProductList = new ObservableCollection<BuyingSellingModel>(buyingSellingProducts.ToList());
                        var temp = SellByProductList.ToList();
                        TempSellByProductList = new ObservableCollection<BuyingSellingModel>(temp);
                        UserDialogs.Instance.HideLoading();
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
                        UserDialogs.Instance.Alert(responseModel.ErrorMessage);
                        UserDialogs.Instance.HideLoading();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                UserDialogs.Instance.Alert(ex.Message);
                UserDialogs.Instance.HideLoading();
            }
            finally
            {
               
            }
        }

        public async Task GetSellingOrderList()
        {
            try
            {
                if (!Constant.IsConnected)
                {
                    UserDialogs.Instance.Toast("Internet not available");
                    IsTap = false;
                    return;
                }


                Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Loading, please wait...");
                await Task.Delay(50);//1053
                string methodUrl = $"/api/Order/GetAllOrdersForSeller?id={Constant.LoginUserData.Id}&UserId={0}&Search={SearchedString}&PageNumber={1}&PageSize={20}&additionalProp1={null}&additionalProp2={null}&additionalProp3={null}";
                RestResponseModel responseModel = await WebService.GetData(methodUrl, true);
                if (responseModel != null)
                {
                    if (responseModel.status_code == 200)
                    {
                        var buyingSellingProducts = JsonConvert.DeserializeObject<List<BuyingSellingModel>>(responseModel.response_body);
                        SellByProductList = new ObservableCollection<BuyingSellingModel>(buyingSellingProducts.ToList());
                        var temp = SellByProductList.ToList();
                        TempSellByProductList = new ObservableCollection<BuyingSellingModel>(temp);
                        UserDialogs.Instance.HideLoading();
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
                        UserDialogs.Instance.Alert(responseModel.ErrorMessage);
                        UserDialogs.Instance.HideLoading();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                UserDialogs.Instance.Alert(ex.Message);
                UserDialogs.Instance.HideLoading();
            }
        }

        public async Task GetCompleteSellingOrderList()
        {
            try
            {
                if (!Constant.IsConnected)
                {
                    UserDialogs.Instance.Toast("Internet not available");
                    IsTap = false;
                    return;
                }

                Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Loading, please wait...");
                await Task.Delay(50);//1053
                string methodUrl = $"/api/Order/GetAllCompletedOrdersForSeller?id={Constant.LoginUserData.Id}&UserId={0}&Search={SearchedString}&PageNumber={1}&PageSize={20}&additionalProp1={null}&additionalProp2={null}&additionalProp3={null}";
                RestResponseModel responseModel = await WebService.GetData(methodUrl, true);
                if (responseModel != null)
                {
                    if (responseModel.status_code == 200)
                    {
                        var buyingSellingProducts = JsonConvert.DeserializeObject<List<BuyingSellingModel>>(responseModel.response_body);
                        SellByProductList = new ObservableCollection<BuyingSellingModel>(buyingSellingProducts.ToList());
                        var temp = SellByProductList.ToList();
                        TempSellByProductList = new ObservableCollection<BuyingSellingModel>(temp);
                        UserDialogs.Instance.HideLoading();
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
                        UserDialogs.Instance.Alert(responseModel.ErrorMessage);
                        UserDialogs.Instance.HideLoading();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                UserDialogs.Instance.Alert(ex.Message);
                UserDialogs.Instance.HideLoading();
            }
        }

        public async Task GetCompleteBuyingOrderList()
        {
            try
            {
                if (!Constant.IsConnected)
                {
                    UserDialogs.Instance.Toast("Internet not available");
                    IsTap = false;
                    return;
                }

                Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Loading, please wait...");
                await Task.Delay(50);//1053
                string methodUrl = $"/api/Order/GetAllCompletedOrdersForBuyer?id={Constant.LoginUserData.Id}&UserId={0}&Search={SearchedString}&PageNumber={1}&PageSize={20}&additionalProp1={null}&additionalProp2={null}&additionalProp3={null}";
                RestResponseModel responseModel = await WebService.GetData(methodUrl, true);
                if (responseModel != null)
                {
                    if (responseModel.status_code == 200)
                    {
                        var buyingSellingProducts = JsonConvert.DeserializeObject<List<BuyingSellingModel>>(responseModel.response_body);
                        SellByProductList = new ObservableCollection<BuyingSellingModel>(buyingSellingProducts.ToList());
                        var temp = SellByProductList.ToList();
                        TempSellByProductList = new ObservableCollection<BuyingSellingModel>(temp);
                        UserDialogs.Instance.HideLoading();
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
                        UserDialogs.Instance.Alert(responseModel.ErrorMessage);
                        UserDialogs.Instance.HideLoading();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                UserDialogs.Instance.Alert(ex.Message);
                UserDialogs.Instance.HideLoading();
            }
        }

        #endregion
    }

    public class BuyingProductModel : BaseViewModelWithoutProperty
    {
        public DashBoardModel product { get; set; }

        private string _status;
        public string status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
                OnPropertyChanged("status");
            }
        }

        private string _statuscolor;
        public string statuscolor
        {
            get
            {
                return _statuscolor;
            }
            set
            {
                _statuscolor = value;
                OnPropertyChanged("statuscolor");
            }
        }
    }

    public class BuyingSellingOrderItem :BaseViewModelWithoutProperty
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public decimal Tax { get; set; }
        public bool IsPaid { get; set; }
        public string ShipmentStatus { get; set; }
        public bool IsDispute { get; set; }
        public string BuyerDisputeReason { get; set; }
        public string SellerDisputeReason { get; set; }
        public BuyingSellingProduct Product { get; set; }
        public string PayerToken { get; set; }
        public string PaymentAuth { get; set; }
        public int? UserCardOnFileId { get; set; }
        public ShipmentStatus Shipment
        {
            get
            {
                if (string.IsNullOrEmpty(ShipmentStatus))
                {
                    return Global.GetShipmentStatus(0);
                }
                else
                {
                    return Global.GetShipmentStatus(Convert.ToInt32(ShipmentStatus));
                }
            }
        }

        public string TotalPrice { get; set; }
    }

    public class BuyingSellingProduct
    {
        public int Id { get; set; }
        public string UserFirstName { get; set; }
        public int UserId { get; set; }
        public string UserProfile { get; set; }
        public string UserName { get; set; }
        public string UserLastName { get; set; }
        public string Source { get; set; }
        public string ProductName { get; set; }
        public string Size { get; set; }
        public string ProductRating { get; set; }
        public string ProductCondition { get; set; }
        public string ProductColor { get; set; }
        public string ProductCategory { get; set; }
        public string ParentCategory { get; set; }
        public string Brand { get; set; }
        public string StoreType { get; set; }
        public string Quantity { get; set; }
        public string Availability { get; set; }
        public string TagImage { get; set; }
        public string Description { get; set; }
        public bool IsLike { get; set; }
        public bool IsBuyingView
        {
            get
            {
                return Global.BuyingPageTitle.ToLower() == "buying" ? true : false;
            }
        }
        public bool IsSellingView
        {
            get
            {
                return !IsBuyingView;
            }
        }
        public int? LikeCount { get; set; }
        public string ProductImage { get; set; }
        public string Gender { get; set; }
        public List<string> otherImages { get; set; }
        public string Price { get; set; }

        public string ProdDetails
        {
            get
            {
                return Global.ProdDetailsFormatter(Price, Size, Brand);
            }

        }

        public string ShippingPrice { get; set; }

        public string EarningPrice {
            get
            {

                    var twentyPercent = Convert.ToDouble(Price?.Replace("$", "")) * 20 / 100;
                //earning = price - (twentyPercent + 14);
              return Convert.ToString(Convert.ToDouble(Price?.Replace("$","")) - twentyPercent);
            }
        }

        #region Theme Color property
        private string _ThemeCol = Global.GetThemeColor(Global.setThemeColor);
        public string ThemeCol
        {
            get
            {
                if (StoreType.ToLower() == Constant.ClothingStr.ToLower())
                {
                    return Global.GetThemeColor(ThemesColor.BlueColor);
                }
                else if (StoreType.ToLower() == Constant.SneakersStr.ToLower())
                {
                    return Global.GetThemeColor(ThemesColor.RedColor);
                }
                else if (StoreType.ToLower() == Constant.StreetwearStr.ToLower())
                {
                    return Global.GetThemeColor(ThemesColor.OrangeColor);
                }
                else if (StoreType.ToLower() == Constant.VintageStr.ToLower())
                {
                    return Global.GetThemeColor(ThemesColor.GreenColor);
                }
                return _ThemeCol;
            }
            set
            {
                _ThemeCol = value;
            }

        }
        #endregion
    }

    public class ShippingAddress
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public int ShippingAddressId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public bool IsDefault { get; set; }
        public string Country { get; set; }
        public bool IsBilling { get; set; }
        public bool IsBillingShippingSame { get; set; }
    }

    public class ByingSellingPaymentDetail
    {
        public int Id { get; set; }
        public string CardType { get; set; }
        public string CCLast4 { get; set; }
        public string Expiry { get; set; }
        public string FormattedExpiry
        {
            get
            {
                if (!string.IsNullOrEmpty(Expiry))
                    return Expiry.Insert(2, "/");
                else
                {
                    return null;
                }
            }
        }
    }

    public class BuyingSellingModel
    {
        public int OrderId { get; set; }
        public List<BuyingSellingOrderItem> OrderItems { get; set; }
        public ShippingAddress ShippingAddress { get; set; }
        public ByingSellingPaymentDetail PaymentDetail { get; set; }
        public DateTime CreatedDate { get; set; }
        public string  BuyerUserName { get; set; }
        public BuyingSellingOrderItem OrderProductItem {
            get
            {
                if(OrderItems!=null && OrderItems.Count>0)
                {
                    return OrderItems[0];
                }
                return new BuyingSellingOrderItem();
            }
        } 
    }



}

