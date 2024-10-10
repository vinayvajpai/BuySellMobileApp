using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
using AutoMapper.Internal;
using BuySell.Helper;
using BuySell.Model;
using BuySell.Model.RestResponse;
using BuySell.ViewModel;
using BuySell.Views.BuyingSellingViews;
using BuySell.WebServices;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace BuySell.ViewModel
{
    public class OrderSummaryViewModel : BaseViewModel
    {
        public OrderSummaryViewModel(INavigation nav, BuyingSellingModel buyingProduct)
        {
            navigation = nav;
            BuyingProduct = buyingProduct;
            SelectedOrder = buyingProduct.OrderItems.FirstOrDefault().Product;
            StatusColor = BuyingProduct.OrderItems.FirstOrDefault().Shipment.StatusColor;
            Status = BuyingProduct.OrderItems.FirstOrDefault().Shipment.StatusType;
        }

        private Command _BackCommand;
        public Command BackCommand
        {
            get
            {
                return _BackCommand ?? (_BackCommand = new Command(async () =>
                {
                    try
                    {
                        IsTap = false;
                        await navigation.PopAsync(true);
                    }
                    catch (Exception ex)
                    {

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
                        await navigation.PushAsync(new OrderDisputeView(buyingproduct));
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

        private BuyingSellingModel buyingproduct;
        public BuyingSellingModel BuyingProduct
        {
            get
            {
                return buyingproduct;
            }
            set
            {
                buyingproduct = value;
                OnPropertyChanged("BuyingProduct");
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
                if (buyingproduct != null)
                {
                    if (buyingproduct.OrderItems != null)
                    {
                        var totalPrice = buyingproduct.OrderItems.ToList().Sum(p => Convert.ToDecimal(p.Product.Price.Replace("$", "")));
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
                if (buyingproduct != null)
                {
                    if (buyingproduct.OrderItems != null)
                    {
                        var totalSPrice = buyingproduct.OrderItems.ToList().Sum(p => Convert.ToDecimal(p.Product.ShippingPrice.Replace("$", "")));
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
                if (buyingproduct != null)
                {
                    if (buyingproduct.OrderItems != null && buyingproduct.OrderItems.Count > 0)
                    {
                        return buyingproduct.OrderItems[0].Tax;
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



        public async Task CancelOrderMethod()
        {
            try
            {

                if (!Constant.IsConnected)
                {
                    UserDialogs.Instance.Toast("Internet not available");
                    IsTap = false;
                    return;
                }

                UserDialogs.Instance.Alert("", "Coming soon", "OK");
                return;

                if (!string.IsNullOrWhiteSpace(CancelReason))
                {

                    Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Cancelling, Please wait...");
                    await Task.Delay(50);
                    string methodUrl = $"/api/Order/CancelOrder";

                    if (!string.IsNullOrWhiteSpace(methodUrl))
                    {
                        RestResponseModel responseModel = await WebService.PostData(BuyingProduct, methodUrl, true);
                        Debug.WriteLine(JsonConvert.SerializeObject(BuyingProduct));
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
                    RestResponseModel responseModel = await WebService.PostData(BuyingProduct, methodUrl, true);
                    Debug.WriteLine(JsonConvert.SerializeObject(BuyingProduct));
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
    }
}

public class Order
{
    public int OrderId { get; set; }
    public int UserId { get; set; }
    public int StatusId { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool IsPaid { get; set; }
    public List<BuyingSellingOrderItem> OrderItems { get; set; }
}

public class OrderToCancel
{
    public Order Order { get; set; }
    public int ShippingAmount { get; set; }
    public int PaymentId { get; set; }
    public int ShippingAddressId { get; set; }
    public string PaymentReceiptNumber { get; set; }
    public string PaymentResponse { get; set; }
}

public class OrderCancelModel
{
    public OrderToCancel OrderToCancel { get; set; }
    public string CancellationReason { get; set; }
    public int UserId { get; set; }
}