using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Acr.UserDialogs;
using BuySell.Helper;
using BuySell.Model.RestResponse;
using BuySell.Model;
using BuySell.View;
using BuySell.WebServices;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Xamarin.Forms;
using System.Linq;
using System.Collections.ObjectModel;
using System.Threading;
using Xamarin.Essentials;

namespace BuySell.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region Constructor
        public BaseViewModel()
        {
        }
        #endregion

        #region INotifyPropertyChanged
        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            try
            {
                var changed = PropertyChanged;
                if (changed == null)
                    return;

                changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        #endregion

        #region Properties
        private INavigation _navigation = null;
        public INavigation navigation
        {
            get { return _navigation; }
            set { _navigation = value; OnPropertyChanged("navigation"); }
        }
        
        
        private FormattedAddress _FormattedAdd = new FormattedAddress();
        public FormattedAddress FormattedAdd
        {
            get { return _FormattedAdd; }
            set { _FormattedAdd = value; OnPropertyChanged("FormattedAdd"); }
        }

        private bool _IsBusy = false;
        public bool IsBusy
        {
            get { return _IsBusy; }
            set { _IsBusy = value; OnPropertyChanged("IsBusy"); }
        }

        private bool _IsTap = false;
        public bool IsTap
        {
            get { return _IsTap; }
            set { _IsTap = value; OnPropertyChanged("IsTap"); }
        }

        private double _RowHeight = 300;
        public double RowHeight
        {
            get { return _RowHeight; }
            set { _RowHeight = value; OnPropertyChanged("RowHeight"); }
        }

        private bool _IsNoData = false;
        public bool IsNoData
        {
            get { return _IsNoData; }
            set { _IsNoData = value; OnPropertyChanged("IsNoData"); }
        }

        private bool _IsShowFilter = false;
        public bool IsShowFilter
        {
            get { return _IsShowFilter; }
            set { _IsShowFilter = value; OnPropertyChanged("IsShowFilter"); }
        }

        private String _ThemeColor = Global.GetThemeColor(Global.setThemeColor);
        public String ThemeColor
        {
            get { return _ThemeColor; }
            set { _ThemeColor = value; OnPropertyChanged("ThemeColor"); }
        }

        private String _ProductCatName = Global.GetProductCatName(Global.setThemeColor);
        public String ProductCatName
        {
            get { return Global.GetProductCatName(Global.setThemeColor); }
            set { _ProductCatName = value; OnPropertyChanged("ProductCatName"); }

        }

        private String _ProductCatIcon = Global.GetProductCatIcon(Global.setThemeColor);
        public String ProductCatIcon
        {
            get { return Global.GetProductCatIcon(Global.setThemeColor); }
            set { _ProductCatIcon = value; OnPropertyChanged("ProductCatIcon"); }
        }

        private String _ProductCatLogo = Global.GetProductCatLogo(Global.setThemeColor);
        public String ProductCatLogo
        {
            get { return Global.GetProductCatLogo(Global.setThemeColor); }
            set { _ProductCatLogo = value; OnPropertyChanged("ProductCatLogo"); }
        }

        private int _SelectedIndexHeaderTab = ((int)Global.setThemeColor);
        public int SelectedIndexHeaderTab
        {
            get { return _SelectedIndexHeaderTab; }
            set { _SelectedIndexHeaderTab = value; OnPropertyChanged("SelectedIndexHeaderTab"); }
        }

        private int _SelectedIndexHeaderTabForAddListing;
        public int SelectedIndexHeaderTabForAddListing
        {
            get { return _SelectedIndexHeaderTabForAddListing; }
            set { _SelectedIndexHeaderTabForAddListing = value; OnPropertyChanged("SelectedIndexHeaderTabForAddListing"); }
        }

        private string _TotalCount;
        public string TotalCount
        {
            get { return _TotalCount; }
            set { _TotalCount = value; OnPropertyChanged("TotalCount"); }
        }

        #endregion

        #region Commands
        private Command _LoginBackCommand;
        public Command LoginBackCommand
        {
            get
            {
                return _LoginBackCommand ?? (_LoginBackCommand = new Command(async () =>
                {
                    if (navigation != null)
                    {
                        try
                        {
                            //if (IsTap)
                            //    return;
                            //IsTap = true;

                            await navigation.PopAsync(true);
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(ex.Message);
                            IsTap = false;
                        }

                    }
                }
             ));
            }
        }

        private Command _LogingoutCommand;
        public Command LogingoutCommand
        {
            get
            {
                return _LogingoutCommand ?? (_LogingoutCommand = new Command(async () =>
                {
                    if (navigation != null)
                    {
                        App.Current.MainPage = new NavigationPage(new LoginPage());
                        //Global.RemoveHomePageAndInsertLogin(navigation);
                    }
                }
             ));
            }
        }
        #endregion

        #region Method

        #region Get Product Like Response
        //Method created to integrate product like and dislike api
        public async Task GetProductLikeResponse(DashBoardModel product, int status)
        {
            if (Global.Username == null && string.IsNullOrEmpty(Global.Username) && string.IsNullOrEmpty(Global.Password))
            {
                Global.AlertWithAction("Please login first", () =>
                {
                    var nav = new NavigationPage(new LoginPage());
                    App.Current.MainPage = nav;
                });
                //var alertConfig = new AlertConfig
                //{
                //    Message = "Please login first",
                //    OkText = "OK",
                //    OnAction = () =>
                //    {
                //        var nav = new NavigationPage(new LoginPage());
                //        App.Current.MainPage = nav;
                //    }
                //};
                //Acr.UserDialogs.UserDialogs.Instance.Alert(alertConfig);
                return;
            }
            else
            {
                if (!Constant.IsConnected)
                {
                    UserDialogs.Instance.Toast("Internet not available");
                    IsTap = false;
                    return;
                }


                if (status == 1)
                {
                    product.IsLike = true;
                    if (product.LikeCount != null)
                        product.LikeCount++;
                    else
                    {
                        product.LikeCount = 0;
                        product.LikeCount++;
                    }
                }
                else
                {
                    product.IsLike = false;
                    product.LikeCount--;
                    if (product.LikeCount == 0)
                    {
                        product.LikeCount = null;
                    }
                }
                ProductLikeRequestModel productLikeRequestModel = new ProductLikeRequestModel();
                productLikeRequestModel.ProductId = product.Id;
                productLikeRequestModel.Status = status;
                string methodUrl = "/api/Product/ProductLikeDislike";
                RestResponseModel responseModel = await WebService.PostData(productLikeRequestModel, methodUrl, true);
                if (responseModel != null)
                {
                    ProductLikeDislikeResponseModel productLikeDislikeResponse = JsonConvert.DeserializeObject<ProductLikeDislikeResponseModel>(responseModel.response_body);
                    if (productLikeDislikeResponse.StatusCode == 200)
                    {

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
                        Debug.WriteLine(productLikeDislikeResponse.Message);
                        UserDialogs.Instance.HideLoading();
                    }
                }
                else
                {
                    IsTap = false;
                    Debug.WriteLine(Constant.ErrorMessage);
                    UserDialogs.Instance.HideLoading();
                }
            }
        }
        #endregion

        public void SetThemeColor(int index)
        {
            try
            {
                Global.SetThemeColor(index);
                ThemeColor = Global.GetThemeColor(Global.setThemeColor);
                SelectedIndexHeaderTab = index;
            }
            catch (Exception ex)
            { }
        }
        public void SelectCatOnThemeTap(int index)
        {
            try
            {
                SelectedIndexHeaderTab = index;
                var param = Convert.ToInt16(index);
                if (param == 1)
                {
                    Global.Storecategory = Constant.ClothingStr;
                }
                else if (param == 2)
                {
                    Global.Storecategory = Constant.SneakersStr;
                }
                else if (param == 3)
                {
                    Global.Storecategory = Constant.StreetwearStr;
                }

                else if (param == 4)
                {
                    Global.Storecategory = Constant.VintageStr;
                }
            }
            catch (Exception ex)
            { }
        }

        async public Task GetCurrentLocation(CancellationTokenSource cts)
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                var location = await Geolocation.GetLocationAsync(request, cts.Token);

                if (location != null)
                {
                    var placemarks = await Geocoding.GetPlacemarksAsync(location.Latitude, location.Longitude);
                    var placemark = placemarks?.FirstOrDefault();
                    if (placemark != null)
                    {
                        Global.currentPlaceMark = placemark;
                    }
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                Acr.UserDialogs.UserDialogs.Instance.Toast("Please enable location");
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }
        }


        #region Order Number generator

        public async Task<long> OrderNumberGenerator(long productId)
        {
            // calling API for Shopping cart Order Id generation.
            try
            {
                if (!Constant.IsConnected)
                {
                    UserDialogs.Instance.Toast("Internet not available");
                    IsTap = false;
                    return 0;
                }


                long orderId = 0;

                if (Global.globalCartList != null)
                {

                    if (Global.globalCartList.Count == 0)
                    {
                        orderId = 0;
                    }
                    else
                    {
                        orderId = Global.globalCartList.LastOrDefault().OrderId;
                    }


                    RequestCartOrderID requestCartOrderID = new RequestCartOrderID()
                    {
                        OrderId = orderId,
                        UserId = Constant.LoginUserData.Id,
                        ProductId = productId,
                    };


                    UserDialogs.Instance.ShowLoading();
                    await Task.Delay(50);
                    string methodUrl = "/api/Order/AddToCart";
                    RestResponseModel responseModel = await WebService.PostData(requestCartOrderID, methodUrl, true);
                    if (responseModel != null)
                    {
                        if (responseModel.status_code == 200)
                        {
                            ResponseCartOrderID responseCartOrderID = JsonConvert.DeserializeObject<ResponseCartOrderID>(responseModel.response_body);

                            if (responseCartOrderID != null)
                            {
                                orderId = responseCartOrderID.OrderId;
                            }
                            else
                            {
                                orderId = 0;
                            }
                            Acr.UserDialogs.UserDialogs.Instance.HideLoading();
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
                    else
                    {
                        IsTap = false;
                        Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                        UserDialogs.Instance.HideLoading();
                    }
                }
                return orderId;
            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                return 0;
            }

        }

        public async Task<long> OrderNumberGenerator(long productId, long orderId = 0)
        {
            // calling API for Shopping cart Order Id generation.
            try
            {

                if (!Constant.IsConnected)
                {
                    UserDialogs.Instance.Toast("Internet not available");
                    IsTap = false;
                    return 0;
                }

                RequestCartOrderID requestCartOrderID = new RequestCartOrderID()
                {
                    OrderId = orderId,
                    UserId = Constant.LoginUserData.Id,
                    ProductId = productId,
                };
                //UserDialogs.Instance.ShowLoading();
                //await Task.Delay(50);
                string methodUrl = "/api/Order/AddToCart";
                RestResponseModel responseModel = await WebService.PostData(requestCartOrderID, methodUrl, true);
                if (responseModel != null)
                {
                    if (responseModel.status_code == 200)
                    {
                        ResponseCartOrderID responseCartOrderID = JsonConvert.DeserializeObject<ResponseCartOrderID>(responseModel.response_body);

                        if (responseCartOrderID != null)
                        {
                            orderId = responseCartOrderID.OrderId;
                        }
                        else
                        {
                            orderId = 0;
                        }
                        Acr.UserDialogs.UserDialogs.Instance.HideLoading();
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
                else
                {
                    IsTap = false;
                    Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                    UserDialogs.Instance.HideLoading();
                }

                return orderId;
            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                return 0;
            }

        }

        #endregion

        #endregion
    }

    public class BaseViewModelWithoutProperty : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            try
            {
                var changed = PropertyChanged;
                if (changed == null)
                    return;

                changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private String _ThemeColor = Global.GetThemeColor(Global.setThemeColor);
        public String ThemeColor
        {
            get { return _ThemeColor; }
            set { _ThemeColor = value; OnPropertyChanged("ThemeColor"); }
        }
        #endregion
    }
}
