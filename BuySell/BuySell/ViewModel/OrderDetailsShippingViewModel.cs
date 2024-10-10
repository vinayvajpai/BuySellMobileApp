using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using BuySell.Helper;
using System.Threading.Tasks;
using BuySell.Model;
using Xamarin.Forms;
using BuySell.Model.RestResponse;
using BuySell.WebServices;
using Newtonsoft.Json;
using System.Drawing.Printing;
using Xamarin.Essentials;
using Acr.UserDialogs;
using Xamarin.Forms.Internals;
using System.Collections.ObjectModel;
using BuySell.Views.BuyingSellingViews;

namespace BuySell.ViewModel
{
    internal class OrderDetailsShippingViewModel : BaseViewModel
    {
        #region Constructor
        public OrderDetailsShippingViewModel(INavigation _nav, DashBoardModel dataModel, string offerPrice)
        {
            navigation = _nav;
            ProdataModel = dataModel;
            //if (offerPrice != null)
            //{
            //    offerPrice = offerPrice.Replace("$", "");
            //    OfferPrice = Convert.ToDouble(offerPrice);
            //}
            //else
            //{
            //    OfferPrice = Convert.ToDouble(ProdataModel.OfferPrice);
            //}
        }

        public OrderDetailsShippingViewModel(INavigation _nav, ViewModel.BuyingSellingModel selectedOrderItem)
        {
            navigation = _nav;
            if (selectedOrderItem != null)
            {
                SellingItem = selectedOrderItem;
                IsPrintLabelAvail = Convert.ToInt16(selectedOrderItem.OrderItems.FirstOrDefault().ShipmentStatus) == 0 ? true : false;
                if (Convert.ToInt16(selectedOrderItem.OrderItems.FirstOrDefault().ShipmentStatus) < 2)
                {
                    IsReprintShippingLabel = !IsPrintLabelAvail;
                }
                else
                {
                    IsReprintShippingLabel = false;
                }
                SelectedOrder = selectedOrderItem.OrderItems.FirstOrDefault().Product;
                Status = SellingItem.OrderItems.FirstOrDefault().Shipment.StatusType;
                StatusColor = SellingItem.OrderItems.FirstOrDefault().Shipment.StatusColor;
            }
        }
        #endregion

        #region Properties

        private string _status;
        public string Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
                OnPropertyChanged("Status");
            }
        }

        private string _statuscolor;
        public string StatusColor
        {
            get
            {
                return _statuscolor;
            }
            set
            {
                _statuscolor = value;
                OnPropertyChanged("StatusColor");
            }
        }


        private double _OfferPrice;
        public double OfferPrice
        {
            get { return _OfferPrice; }
            set { _OfferPrice = value; OnPropertyChanged("OfferPrice"); }
        }


        private string _Title;
        public string Title
        {
            get { return _Title; }
            set { _Title = value; OnPropertyChanged("Title"); }
        }

        private string cancelreason;
        public string CancelReason
        {
            get
            {
                return cancelreason;
            }
            set
            {
                cancelreason = value;
                OnPropertyChanged("CancelReason");
            }
        }


        private bool _IsPrintLabelAvail;
        public bool IsPrintLabelAvail
        {
            get
            {
                return _IsPrintLabelAvail;
            }
            set
            {
                _IsPrintLabelAvail = value;
                OnPropertyChanged("IsPrintLabelAvail");
            }
        }


        private bool _IsShowCancelOption;
        public bool IsShowCancelOption
        {
            get
            {
                return _IsShowCancelOption;
            }
            set
            {
                _IsShowCancelOption = value;
                OnPropertyChanged("IsShowCancelOption");
            }
        }

        private bool _IsReprintShippingLabel;
        public bool IsReprintShippingLabel
        {
            get
            {
                return _IsReprintShippingLabel;
            }
            set
            {
                _IsReprintShippingLabel = value;
                OnPropertyChanged("IsReprintShippingLabel");
            }
        }


        private ObservableCollection<string> cancelresons = new ObservableCollection<string>(Global.listCancelResons);
        public ObservableCollection<string> CancelResons
        {
            get
            {
                return cancelresons;
            }
            set
            {
                cancelresons = value;
                OnPropertyChanged("CancelResons");
            }
        }


        public DashBoardModel _ProdataModel = new DashBoardModel();
        public DashBoardModel ProdataModel
        {
            get { return _ProdataModel; }
            set { _ProdataModel = value; OnPropertyChanged("ProdataModel"); }
        }


        public ViewModel.BuyingSellingModel _SellingItem = new ViewModel.BuyingSellingModel();
        public ViewModel.BuyingSellingModel SellingItem
        {
            get { return _SellingItem; }
            set
            {
                _SellingItem = value;
                OnPropertyChanged("SellingItem");
                OnPropertyChanged("TotalItemPrice");
                OnPropertyChanged("TotalShippingPrice");
                OnPropertyChanged("TaxAmount");
                OnPropertyChanged("TotalPrice");
            }
        }


        public decimal TotalItemPrice
        {
            get
            {
                if (SellingItem != null)
                {
                    if (SellingItem.OrderItems != null)
                    {
                        var totalPrice = SellingItem.OrderItems.ToList().Sum(p => Convert.ToDecimal(p.Product.Price.Replace("$", "")));
                        return totalPrice;
                    }
                }
                return Convert.ToDecimal("0.00");
            }
        }

        public decimal TotalShippingPrice
        {
            get
            {
                if (SellingItem != null)
                {
                    if (SellingItem.OrderItems != null)
                    {
                        var totalSPrice = SellingItem.OrderItems.ToList().Sum(p => Convert.ToDecimal(p.Product.ShippingPrice.Replace("$", "")));
                        return totalSPrice;
                    }
                }
                return Convert.ToDecimal("0.00");
            }
        }

        public decimal TaxAmount
        {
            get
            {
                if (SellingItem != null)
                {
                    if (SellingItem.OrderItems != null && SellingItem.OrderItems.Count > 0)
                    {
                        return SellingItem.OrderItems[0].Tax;
                    }
                }
                return Convert.ToDecimal("0.00");
            }
        }

        public decimal TotalPrice
        {
            get
            {
                return TotalItemPrice + TotalShippingPrice + TaxAmount;
            }
        }


        #endregion

        #region Commands
        private Command _Tapped_Back;
        public Command Tapped_Back
        {
            get
            {
                return _Tapped_Back ?? (_Tapped_Back = new Command(async () =>
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
                        Debug.WriteLine(ex.Message);
                    }
                }

            ));
            }

        }


        private Command _PrintLabelCmd;
        public Command PrintLabelCmd
        {
            get
            {
                return _PrintLabelCmd ?? (_PrintLabelCmd = new Command(async () =>
                {
                    try
                    {
                        if (IsTap)
                            return;
                        IsTap = true;

                        PrintLabelMethod();
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

        private Command _DisputeCommand;
        public Command DisputeCommand
        {
            get
            {
                return _DisputeCommand ?? (_DisputeCommand = new Command(async () =>
                {
                    try
                    {
                        IsTap = false;
                        UserDialogs.Instance.Alert("", "Coming soon", "OK");
                        return;
                        await navigation.PushAsync(new OrderDisputeView(SellingItem));
                    }
                    catch (Exception ex)
                    {

                    }
                }
             ));
            }
        }

        private Command _AcceptOfferCmd;
        public Command AcceptOfferCmd
        {
            get
            {
                return _AcceptOfferCmd ?? (_AcceptOfferCmd = new Command(async () =>
                {
                    await AcceptOfferMethod();
                }
             ));
            }
        }

        private ViewModel.BuyingSellingProduct _SelectedOrder;
        public ViewModel.BuyingSellingProduct SelectedOrder
        {
            get
            {
                return _SelectedOrder;
            }
            set
            {
                _SelectedOrder = value;
                OnPropertyChanged("SelectedOrder");
            }
        }

        #endregion

        #region Methods
        private async void PrintLabelMethod()
        {
            try
            {
                if (!Constant.IsConnected)
                {
                    UserDialogs.Instance.Toast("Internet not available");
                    IsTap = false;
                    return;
                }

                if (SelectedOrder != null)
                {

                    Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Preparing the label, Please wait...");
                    await Task.Delay(50);
                    string methodUrl = $"/api/Order/CreateShippingLabel";
                    if (!string.IsNullOrWhiteSpace(methodUrl))
                    {
                        SellingItem.OrderProductItem.TotalPrice.Replace("$", "");
                        var spModel = await ShippingLabelModelConverter(SellingItem);
                        RestResponseModel responseModel = await WebService.PostData(spModel, methodUrl, true);
                        Debug.WriteLine(JsonConvert.SerializeObject(spModel));
                        Debug.WriteLine(JsonConvert.SerializeObject(responseModel));
                        if (responseModel != null)
                        {

                            if (responseModel.status_code == 200)
                            {
                                Global.IsReload = true;
                                ShippingLabelResponse shippingLabelResponse = JsonConvert.DeserializeObject<ShippingLabelResponse>(responseModel.response_body);
                                if (shippingLabelResponse != null)
                                {
                                    await Browser.OpenAsync(shippingLabelResponse.LabelDownLoadUrl, BrowserLaunchMode.SystemPreferred);
                                    IsTap = false;
                                    Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                                    Status = "Delivered";
                                    StatusColor = "#EB9748";
                                    IsPrintLabelAvail = false;
                                    IsShowCancelOption = false;
                                }
                                else
                                {
                                    Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                                    Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                                    IsTap = false;
                                }
                            }
                            else if (responseModel.status_code == 500)
                            {
                                ResponseBodyModel responseBodyModel = JsonConvert.DeserializeObject<ResponseBodyModel>(responseModel.response_body);
                                IsTap = false;
                                if (responseBodyModel.Message != null)
                                {
                                    if (responseBodyModel.Message.ToLower().Contains("object"))
                                    {
                                        UserDialogs.Instance.HideLoading();
                                        // Acr.UserDialogs.UserDialogs.Instance.Alert("Please add the address in your profile by going in account > login & security > address");
                                        Acr.UserDialogs.UserDialogs.Instance.Alert("In order to create a Shipping Label we will need your address! Please add it by going to \n\nAccount > Login & Security > Address");
                                    }
                                    else
                                    {
                                        UserDialogs.Instance.HideLoading();
                                        Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                                    }
                                }
                                else
                                {
                                    Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                                    Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                                    IsTap = false;
                                }
                            }
                            else
                            {
                                Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                                Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                                IsTap = false;
                            }
                        }
                        else
                        {
                            Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                            Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                            IsTap = false;
                        }
                    }
                    else
                    {
                        Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                        Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                        IsTap = false;
                    }
                }
                else
                {
                    Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                    Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                    IsTap = false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public async Task AcceptOfferMethod()
        {

            try
            {
                if (!Constant.IsConnected)
                {
                    UserDialogs.Instance.Toast("Internet not available");
                    IsTap = false;
                    return;
                }

                Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Changing status, Please wait...");
                await Task.Delay(50);
                string methodUrl = $"/api/Order/AcceptOrder";

                if (!string.IsNullOrWhiteSpace(methodUrl))
                {
                    RestResponseModel responseModel = await WebService.PostData(SellingItem, methodUrl, true);
                    Debug.WriteLine(JsonConvert.SerializeObject(SellingItem));
                    Debug.WriteLine(JsonConvert.SerializeObject(responseModel));
                    if (responseModel != null)
                    {

                        if (responseModel.status_code == 200)
                        {
                            string shippingLabelResponse = JsonConvert.DeserializeObject<string>(responseModel.response_body);
                            if (shippingLabelResponse != null)
                            {
                                Status = "Accepted";
                                StatusColor = "#EB9748";
                                Global.AlertWithAction("Order Accepted successfully!!", async () =>
                                {
                                    Global.IsReload = true;
                                    await navigation.PopAsync();
                                });

                                IsTap = false;
                                Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                            }
                            else
                            {
                                Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                                Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                                IsTap = false;
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
                            Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                            Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                            IsTap = false;
                        }
                    }
                    else
                    {
                        Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                        Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                        IsTap = false;
                    }
                }
                else
                {
                    Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                    Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                    IsTap = false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public async void CancelOrderCommand()
        {
            try
            {
                if (!Constant.IsConnected)
                {
                    UserDialogs.Instance.Toast("Internet not available");
                    IsTap = false;
                    return;
                }

                if (!string.IsNullOrWhiteSpace(CancelReason))
                {
                    Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Cancelling, Please wait...");
                    await Task.Delay(50);
                    string methodUrl = $"/api/Order/CancelOrder";

                    if (!string.IsNullOrWhiteSpace(methodUrl))
                    {
                        RestResponseModel responseModel = await WebService.PostData(SellingItem, methodUrl, true);
                        Debug.WriteLine(JsonConvert.SerializeObject(SellingItem));
                        Debug.WriteLine(JsonConvert.SerializeObject(responseModel));
                        if (responseModel != null)
                        {

                            if (responseModel.status_code == 200)
                            {
                                string shippingLabelResponse = JsonConvert.DeserializeObject<string>(responseModel.response_body);
                                if (shippingLabelResponse != null)
                                {
                                    Status = "Cancelled";
                                    StatusColor = "#FF0000";
                                    Global.AlertWithAction("Order cancelled successfully!!", async () =>
                                    {
                                        Global.IsReload = true;
                                        await navigation.PopAsync();
                                    });
                                    //var alertConfig = new AlertConfig
                                    //{
                                    //    Message =  "Order cancelled successfully",
                                    //    OkText = "OK",
                                    //    OnAction = async () =>
                                    //    {
                                    //        await navigation.PopAsync();

                                    //    }
                                    //};
                                    //Acr.UserDialogs.UserDialogs.Instance.Alert(alertConfig);
                                    IsTap = false;
                                    Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                                }
                                else
                                {
                                    Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                                    Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                                    IsTap = false;
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
                                Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                                Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                                IsTap = false;
                            }
                        }
                        else
                        {
                            Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                            Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                            IsTap = false;
                        }
                    }
                    else
                    {
                        Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                        Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                        IsTap = false;
                    }
                }
                else
                {
                    Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                    Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                    IsTap = false;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        #endregion

        #region shipping labelmodel assigner


        public async Task<ShippinglabelRequestModel> ShippingLabelModelConverter(ViewModel.BuyingSellingModel BSModel)
        {
            try
            {
                var shippinglabelmodel = new ShippinglabelRequestModel()
                {
                    UserId = Constant.LoginUserData.Id,
                    OrderItems = new List<ShipOrderItem>()
                };


                foreach (var item in BSModel.OrderItems)
                {
                    shippinglabelmodel.OrderItems.Add(new ShipOrderItem()
                    {
                        Amount = item.Amount,
                        IsPaid = item.IsPaid,
                        OrderId = BSModel.OrderId,
                        OrderItemId = item.Id,
                        Product = item.Product,
                        StatusId = Convert.ToInt16(item.ShipmentStatus),
                        Tax = item.Tax,
                    });
                }


                return shippinglabelmodel;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }


        }
        #endregion
    }
}

public class ShippingLabelResponse
{
    public string TrackingNumber { get; set; }
    public string LabelId { get; set; }
    public DateTime ShipDate { get; set; }
    public string LabelDownLoadUrl { get; set; }
}