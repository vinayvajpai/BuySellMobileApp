using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Acr.UserDialogs;
using BuySell.Model;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;
using BuySell.Helper;
using BuySell.Model.RestResponse;
using BuySell.WebServices;
using Newtonsoft.Json;
using System.Threading.Tasks;
using BuySell.Services;
using BuySell.Views;
using Stripe;
using System.Web;
using MediaManager.Forms;
using FFImageLoading.Helpers.Exif;
using Stripe.FinancialConnections;

namespace BuySell.ViewModel
{
    public class OrderDetailViewModel : BaseViewModel
    {
        #region Constructor
        List<PaymentStatusOrderItem> orderItems;
        IStripePaymentService stripePaymentService = DependencyService.Get<IStripePaymentService>();
        public OrderDetailViewModel(INavigation _nav, DashBoardModel prodDataModel)
        {
            StripeConfiguration.ApiKey = Constant.stripSecertAPIKey;
            navigation = _nav;
            ProdataModel = prodDataModel;
            listProdataModel = new ObservableCollection<DashBoardModel>
            {
                prodDataModel
            };
            GetOrderNumber(listProdataModel.ToList());
        }

        public OrderDetailViewModel(INavigation _nav, List<DashBoardModel> prodDataModel, List<OrderItem> _orderItems)
        {
            try
            {
                navigation = _nav;
                orderItems = _orderItems.Select(o => new PaymentStatusOrderItem()
                {
                    OrderId = o.OrderId,
                    OrderItemId = o.OrderItemId,
                    StatusId = o.StatusId,
                    Amount = Convert.ToDecimal(GrandTotalPrice.Replace("$", "")),
                    Tax = TaxPrice,
                    IsPaid = o.IsPaid,
                    Product = new PaymentProduct()
                    {
                        Id = o.Product.Id,
                        UserFirstName = o.Product.UserFirstName,
                        UserId = Convert.ToInt16(o.Product.UserId),
                        UserProfile = o.Product.UserProfile,
                        UserName = o.Product.UserName,
                        UserLastName = o.Product.UserLastName,
                        Source = o.Product.Source,
                        ProductName = o.Product.ProductName,
                        Size = o.Product.Size,
                        ProductRating = o.Product.ProductRating,
                        ProductCondition = o.Product.ProductCondition,
                        ProductColor = o.Product.ProductColor,
                        ProductCategory = o.Product.ProductCategory,
                        ParentCategory = o.Product.ParentCategory,
                        Brand = o.Product.Brand,
                        StoreType = o.Product.StoreType,
                        Quantity = o.Product.Quantity,
                        Availability = o.Product.Availability,
                        TagImage = o.Product.TagImage,
                        Description = o.Product.Description,
                        IsLike = o.Product.IsLike,
                        LikeCount = o.Product.LikeCount
                    }

                }).ToList();
                StripeConfiguration.ApiKey = Constant.stripSecertAPIKey;
                navigation = _nav;
                listProdataModel = new ObservableCollection<DashBoardModel>(prodDataModel);
                if (Constant.globalTax != 0)
                {
                    TaxPrice = Constant.globalTax;
                }
                rndOrderNo = (int)Convert.ToInt64(orderItems.FirstOrDefault().OrderId);
            }
            catch (Exception ex)
            {

            }
        }

        #endregion

        #region Properties

        public int rndOrderNo;

        private DashBoardModel _ProdataModel;
        public DashBoardModel ProdataModel
        {
            get { return _ProdataModel; }
            set
            {
                _ProdataModel = value;
                OnPropertyChanged("ProdataModel");

            }
        }
        
        private bool _IsPlaceOrderEnabled = true;
        public bool IsPlaceOrderEnabled
        {
            get { return _IsPlaceOrderEnabled; }
            set
            {
                _IsPlaceOrderEnabled = value;
                OnPropertyChanged("IsPlaceOrderEnabled");

            }
        }
        
        private double _PlaceOrderBtnOpacity = 1.0;
        public double PlaceOrderBtnOpacity
        {
            get { return _PlaceOrderBtnOpacity; }
            set
            {
                _PlaceOrderBtnOpacity = value;
                OnPropertyChanged("PlaceOrderBtnOpacity");

            }
        }

        private ObservableCollection<DashBoardModel> _listProdataModel;
        public ObservableCollection<DashBoardModel> listProdataModel
        {
            get { return _listProdataModel; }
            set
            {
                _listProdataModel = value; OnPropertyChanged("listProdataModel");
                if (listProdataModel != null)
                {
                    var listPrice = listProdataModel.Select(l => Convert.ToDecimal(l.Price.Replace("$", "")));
                    var totalPrice = listProdataModel.ToList().Sum(l => Convert.ToDecimal(l.Price.Replace("$", "")));
                    var totalShpping = listProdataModel.ToList().Sum(l => l.ShippingPrice != null ? Convert.ToDecimal(l.ShippingPrice.Replace("$", "")) : 0);
                    GrandTotalPrice = String.Format("${0:F2}", totalPrice + +Constant.globalTax + totalShpping);
                    ShippingPrice = String.Format("${0:F2}", totalShpping);
                }
            }
        }

        private CardListModel _AddCardModel;
        public CardListModel AddCardModel
        {
            get { return _AddCardModel; }
            set { _AddCardModel = value; OnPropertyChanged("AddCardModel"); }
        }

        private AddAddressModel _AddressModel = new AddAddressModel();
        public AddAddressModel AddressModel
        {
            get { return _AddressModel; }
            set
            {
                _AddressModel = value;
                OnPropertyChanged("AddressModel");
                if (_AddressModel != null)
                {
                    if (!string.IsNullOrWhiteSpace(_AddressModel.ZipCode))
                    {
                        getCalculatedTax();
                    }
                    else
                    {
                        Constant.globalTax = 0;
                    }
                }
            }
        }

        public string TotalPrice
        {
            get
            {
                if (listProdataModel != null)
                {
                    var listPrice = listProdataModel.Select(l => Convert.ToDecimal(l.Price.Replace("$", "")));
                    var totalPrice = listProdataModel.ToList().Sum(l => Convert.ToDecimal(l.Price.Replace("$", "")));
                    return String.Format("${0:F2}", totalPrice);
                    //return String.Format("${0:F2}", Convert.ToDecimal(ProdataModel.Price.Replace("$", "")) + 8 + 7);
                }
                return "$0.00";
            }
        }

        private decimal _TaxPrice;
        public decimal TaxPrice
        {
            get
            {
                return _TaxPrice;
            }
            set
            {
                _TaxPrice = value;
                OnPropertyChanged("TaxPrice");
                if (listProdataModel != null)
                {
                    var listPrice = listProdataModel.Select(l => Convert.ToDecimal(l.Price.Replace("$", "")));
                    var totalPrice = listProdataModel.ToList().Sum(l => Convert.ToDecimal(l.Price.Replace("$", "")));
                    var totalShpping = listProdataModel.ToList().Sum(l => l.ShippingPrice != null ? Convert.ToDecimal(l.ShippingPrice.Replace("$", "")) : 0);
                    GrandTotalPrice = String.Format("${0:F2}", totalPrice + +_TaxPrice + totalShpping);
                    ShippingPrice = String.Format("${0:F2}", totalShpping);
                }
                else
                {
                    GrandTotalPrice = "$0.00";
                }
            }
        }

        private string _GrandTotalPrice = "$0.00";
        public string GrandTotalPrice
        {
            get
            {
                return _GrandTotalPrice;
            }
            set
            {
                _GrandTotalPrice = value;
                OnPropertyChanged("GrandTotalPrice");
            }
        }

        private string _ShippingPrice = "$0.00";
        public string ShippingPrice
        {
            get
            {
                return _ShippingPrice;
            }
            set
            {
                _ShippingPrice = value;
                OnPropertyChanged("ShippingPrice");
            }
        }

        private string _CardIcon;
        public string CardIcon
        {
            get { return _CardIcon; }
            set { _CardIcon = value; OnPropertyChanged("CardIcon"); }
        }


        #endregion

        #region Method
        public async Task CallUpdatePaymentStatus(string paymentToken, PaymentIntent paymentIntent, Charge charge = null)
        {
            try
            {
                if (!Constant.IsConnected)
                {
                    UserDialogs.Instance.Toast("Internet not available");
                    IsTap = false;
                    return;
                }

                Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Placing Order");
                var orderFilterItems = orderItems.Where(o => listProdataModel.ToList().Select(p => p.Id).Contains(o.Product.Id));
                orderFilterItems.ToList().ForEach(o => o.Tax = TaxPrice);
                UpdatePaymentStatusRequest stripePaymentRequestModel = new UpdatePaymentStatusRequest
                {
                    Order = new PaymentStatusOrder()
                    {
                        UserId = Convert.ToInt32(Constant.LoginUserData.Id),
                        OrderId = rndOrderNo,
                        StatusId = 0,
                        IsPaid = paymentIntent.Status == "succeeded" ? true : false,
                        OrderItems = orderFilterItems.ToList()
                    },
                    PaymentReceiptNumber = paymentIntent.InvoiceId,
                    ShippingAddressId = Constant.globalSelectedAddress.ShippingAddressId,
                    PaymentResponse = paymentIntent.Status,
                    PaymentId = Constant.globalAddedCard.Id,
                    ShippingAmount = Convert.ToDecimal(ShippingPrice.Replace("$", ""))
                };
                string methodUrl = "/api/Account/UpdatePaymentStatus";
                Debug.WriteLine(JsonConvert.SerializeObject(stripePaymentRequestModel));
                RestResponseModel responseModel = await WebService.PostData(stripePaymentRequestModel, methodUrl, true);
                if (responseModel != null)
                {
                    if (responseModel.status_code == 200)
                    {
                        UserDialogs.Instance.HideLoading();
                        await navigation.PushAsync(new Views.OrderConfirmPage(rndOrderNo));
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
                    Debug.WriteLine(Constant.ErrorMessage);
                    UserDialogs.Instance.HideLoading();
                }

            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Alert(ex.Message);
            }
        }

        #endregion

        #region Commands
        private Command _CancleCommand;
        public Command CancleCommand
        {
            get
            {
                return _CancleCommand ?? (_CancleCommand = new Command(async () =>
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

        private Command _AddCardCommand;
        public Command AddCardCommand
        {
            get
            {
                return _AddCardCommand ?? (_AddCardCommand = new Command(async () =>
                {
                    try
                    {
                        if (IsTap)
                            return;
                        IsTap = true;
                        if (Global.GlobalCardList.Count > 0)
                        {
                            await navigation.PushAsync(new CreditCardListView());
                        }
                        else
                        {
                            await navigation.PushAsync(new AddCardPage());
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

        private Command _PlaceOrderCommand;
        public Command PlaceOrderCommand
        {
            get
            {
                return _PlaceOrderCommand ?? (_PlaceOrderCommand = new Command(async () =>
                {
                    try
                    {

                        if (IsTap)
                            return;
                        IsTap = true;
                        if (Helper.Constant.LoginUserData == null)
                        {
                            Global.AlertWithAction("Please login first", () =>
                            {
                                var nav = new NavigationPage(new View.LoginPage());
                                App.Current.MainPage = nav;
                            });

                        }
                        else
                        {
                            if (AddCardModel != null)
                            {
                                if (string.IsNullOrEmpty(AddCardModel.CardToken))
                                {
                                    Acr.UserDialogs.UserDialogs.Instance.Alert("Added card is invalid");
                                    IsTap = false;
                                    return;
                                }
                            }
                            else
                            {
                                Acr.UserDialogs.UserDialogs.Instance.Alert("Please add your card details");
                                IsTap = false;
                                return;
                            }

                            if (AddressModel != null)
                            {
                                if (string.IsNullOrEmpty(AddressModel.AddressLine1))
                                {
                                    Acr.UserDialogs.UserDialogs.Instance.Alert(Helper.Constant.CheckAddressStr);
                                    IsTap = false;
                                    return;
                                }

                            }
                            else
                            {
                                Acr.UserDialogs.UserDialogs.Instance.Alert("Please add your shipping address");
                                IsTap = false;
                                return;
                            }

                            Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Processing your order, please wait..");
                            await Task.Delay(50);
                            var tp = Convert.ToDecimal(GrandTotalPrice.Replace("$", ""));
                            var listOrderItemIds = orderItems.Where(o => listProdataModel.ToList().Select(p => p.Id).Contains(o.Product.Id)).Select(o => o.OrderItemId).ToList();
                            var listOrderItemsProducts = orderItems.Where(o => listProdataModel.ToList().Select(p => p.Id).Contains(o.Product.Id)).Select(o => new { ProductName = o.Product.ProductName, Price = o.Amount, Tax = o.Tax, shippingprice = listProdataModel.ToList().Where(p => p.Id == o.Product.Id).FirstOrDefault().ShippingPrice, Total = (o.Amount + o.Tax + listProdataModel.ToList().Where(p => p.Id == o.Product.Id).FirstOrDefault().ShippingPrice) });
                            var productMetadata = new Dictionary<string, string>();
                            productMetadata.Add("OrderNo", rndOrderNo.ToString());
                            productMetadata.Add("OrderItemNo", string.Join(",", listOrderItemIds));
                            productMetadata.Add("Products", JsonConvert.SerializeObject(listOrderItemsProducts));
                            var payment = new PaymentModel()
                            {
                                Amount = Convert.ToInt64(tp * 100),
                                Description = "Product Item Payment",
                                metaData = productMetadata
                            };
                            //#if DEBUG
                            //payment.Token = "tok_visa";
                            //#endif
                            payment.Token = Constant.globalAddedCard.CardToken;  // use this line when go live for getting original token from selected card.

                            var restultCharges = await stripePaymentService.PayWithCartIntent(payment);
                            if (restultCharges != null)
                            {
                                await CallUpdatePaymentStatus(payment.Token, restultCharges);
                                //Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                            }
                            else
                            {
                                Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                                Acr.UserDialogs.UserDialogs.Instance.Alert("Payment Failed, Please try again after some time");
                            }
                            IsTap = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        IsTap = false;
                        Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                        Acr.UserDialogs.UserDialogs.Instance.Alert(ex.Message);
                    }
                }
            ));
            }
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

        private Command _AddAddressCommand;
        public Command AddAddressCommand
        {
            get
            {
                return _AddAddressCommand ?? (_AddAddressCommand = new Command(async () =>
                {
                    try
                    {
                        IsTap = false;
                        if (Global.GlobalAddressList.Count > 0)
                        {
                            var shippingAdd = new Views.ShoppingAddressPage((addModel) =>
                        {
                            AddressModel = addModel;
                            Helper.Constant.globalSelectedAddress = addModel;
                        });
                            await navigation.PushAsync(shippingAdd);
                        }
                        else
                        {
                            var objAddShipPage = new AddShippingAddressPage(async (addModel) =>
                            {
                                Helper.Global.GlobalAddressList.Add(addModel);
                                AddressModel = addModel;
                                Helper.Constant.globalSelectedAddress = addModel;
                            });
                            IsTap = true;
                            await navigation.PushAsync(objAddShipPage);
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
             ));
            }
        }


        public async void getCalculatedTax()
        {
            try
            {
                if (!Constant.IsConnected)
                {
                    UserDialogs.Instance.Toast("Internet not available");
                    IsTap = false;
                    return;
                }

                if (listProdataModel != null)
                {
                    if (string.IsNullOrWhiteSpace(listProdataModel.FirstOrDefault().UserId))
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }

                if (string.IsNullOrWhiteSpace(rndOrderNo.ToString()))
                {
                    return;
                }
                else if (string.IsNullOrWhiteSpace(TotalPrice))
                {
                    return;
                }
                else if (string.IsNullOrWhiteSpace(Constant.globalSelectedAddress.ZipCode))
                {
                    return;
                }

                Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Calculating tax , Please wait...");
                await Task.Delay(50);
                var State2Letter = Global.GetTwoLetterforState(AddressModel.State);
                ComputeTax computeTax = new ComputeTax()
                {
                    UserId = Constant.LoginUserData.Id,
                    OrderId = rndOrderNo,
                    OrderItemId = listProdataModel.FirstOrDefault().Id,
                    SellerId = Convert.ToInt16(listProdataModel.FirstOrDefault().UserId),
                    Amount = Convert.ToDecimal(Convert.ToDecimal(TotalPrice.Replace("$", "")) + Convert.ToDecimal(ShippingPrice.Replace("$", ""))),
                    TaxAmount = Convert.ToDecimal(Constant.globalTax),
                    ShippingZipCode = AddressModel.ZipCode,
                    ShippingCost = Convert.ToInt32(Convert.ToDecimal(ShippingPrice.Replace("$", ""))),
                    ShippingState2LetterCode = State2Letter.ToUpper()
                };
                // string methodUrl = $"/api/Order/ComputeTax?id={Constant.LoginUserData.Id}&UserId={Convert.ToInt32(listProdataModel.FirstOrDefault().UserId)}&OrderId={Convert.ToInt64(rndOrderNo)}&Amount={Convert.ToDouble(TotalPrice.Replace("$", ""))}&TaxAmount={0}&ShippingZipCode={Convert.ToInt32(Constant.globalSelectedAddress.ZipCode?.Trim())}";
                string methodUrl = $"/api/Order/ComputeTax";
                if (!string.IsNullOrWhiteSpace(methodUrl))
                {
                    RestResponseModel responseModel = await WebService.PostData(computeTax, methodUrl, true);
                    Debug.WriteLine(JsonConvert.SerializeObject(computeTax));
                    Debug.WriteLine(JsonConvert.SerializeObject(responseModel));
                    if (responseModel != null)
                    {
                        if (responseModel.status_code == 200)
                        {
                            CalculateTaxModel taxResponsemodel = JsonConvert.DeserializeObject<CalculateTaxModel>(responseModel.response_body);
                            if (taxResponsemodel != null)
                            {
                                IsPlaceOrderEnabled = true;
                                PlaceOrderBtnOpacity = 1.0;
                                Constant.globalTax = TaxPrice = Convert.ToDecimal(taxResponsemodel.TaxAmount);
                                // ProdataModel.tax = Convert.ToDouble(taxResponsemodel.TaxAmount);
                            }
                            else
                            {
                                Constant.globalTax = TaxPrice = 0;
                            }
                            Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                        }
                        else if (responseModel.status_code == 500)
                        {
                            ResponseBodyModel responseBodyModel = JsonConvert.DeserializeObject<ResponseBodyModel>(responseModel.response_body);
                            if(responseBodyModel != null)
                            {
                                if(responseBodyModel.Message != null)
                                {
                                    if(responseBodyModel.Message.Contains("valid postal code"))
                                    {
                                        IsPlaceOrderEnabled = false;
                                        PlaceOrderBtnOpacity = 0.7;
                                        Constant.globalTax = TaxPrice = 0;
                                    }
                                    else
                                    {
                                        IsPlaceOrderEnabled = true;
                                        PlaceOrderBtnOpacity = 1.0;
                                    }
                                }
                            }
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
                        Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                    }
                    else
                    {
                        IsTap = false;
                        Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                        UserDialogs.Instance.HideLoading();
                    }
                }
            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                Debug.WriteLine(ex.ToString());
            }
        }

        async Task GetOrderNumber(List<DashBoardModel> prodDataModel, bool IsFromCart = false)
        {
            try
            {

                long orderNo = 0;
                if (!IsFromCart)
                    orderNo = await OrderNumberGenerator(listProdataModel.FirstOrDefault().Id);

                rndOrderNo = (int)Convert.ToInt64(orderNo);
                if (Constant.globalTax != 0)
                {
                    TaxPrice = Constant.globalTax;
                }
                await GetCartProductList();
            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                Acr.UserDialogs.UserDialogs.Instance.Alert(ex.Message);

            }
        }
        #endregion

        #region get shipping list 
        public async void GetAllShippingAddress()
        {
            try
            {
                if (!Constant.IsConnected)
                {
                    UserDialogs.Instance.Toast("Internet not available");
                    IsTap = false;
                    return;
                }
                string methodUrl = $"/api/Account/ListShippingAddress?id={Constant.LoginUserData.Id}";
                if (!string.IsNullOrWhiteSpace(methodUrl))
                {
                    RestResponseModel responseModel = await WebService.GetData(methodUrl, true);
                    if (responseModel != null)
                    {
                        if (responseModel.status_code == 200)
                        {
                            Global.GlobalAddressList = JsonConvert.DeserializeObject<ObservableCollection<AddAddressModel>>(responseModel.response_body);
                            if (Global.GlobalAddressList != null)
                            {
                                if (Global.GlobalAddressList.Count > 0)
                                {
                                    if (Constant.globalSelectedAddress != null)
                                    {
                                        if (string.IsNullOrWhiteSpace(Constant.globalSelectedAddress.AddressLine1))
                                        {
                                            AddressModel = Constant.globalSelectedAddress = Global.GlobalAddressList.Where(account => account.IsDefault == true).FirstOrDefault() == null ? Global.GlobalAddressList.FirstOrDefault() : Global.GlobalAddressList.Where(account => account.IsDefault == true).FirstOrDefault();
                                        }
                                        else
                                        {
                                            AddressModel = Constant.globalSelectedAddress;
                                        }
                                    }
                                    else
                                    {
                                        AddressModel = Constant.globalSelectedAddress = Global.GlobalAddressList.Where(account => account.IsDefault == true).FirstOrDefault() == null ? Global.GlobalAddressList.FirstOrDefault() : Global.GlobalAddressList.Where(account => account.IsDefault == true).FirstOrDefault();
                                    }
                                }
                                else
                                {
                                    Constant.globalSelectedAddress = new AddAddressModel();
                                    AddressModel= Constant.globalSelectedAddress;
                                }
                            }
                            else
                            {
                                Constant.globalSelectedAddress = new AddAddressModel();
                                AddressModel = Constant.globalSelectedAddress;
                            }
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
                            Acr.UserDialogs.UserDialogs.Instance.Alert(responseModel.ErrorMessage);
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
                UserDialogs.Instance.HideLoading();
                Debug.WriteLine(ex.Message);
            }
        }
        #endregion

        #region Get All card list
        public async Task GetallcardList()
        {
            try
            {
                if (!Constant.IsConnected)
                {
                    UserDialogs.Instance.Toast("Internet not available");
                    IsTap = false;
                    return;
                }

                //if (Constant.globalAddedCard==null||Constant.globalAddedCard.Id == 0 )
                {
                    if (Constant.LoginUserData.Id == 0)
                    {
                        Acr.UserDialogs.UserDialogs.Instance.Alert("Please enter name");
                        return;
                    }
                    try
                    {
                        UserDialogs.Instance.ShowLoading();
                        await Task.Delay(50);
                        string methodUrl = string.Empty;
                        methodUrl = $"/api/Account/GetCustomerPayments?id={Constant.LoginUserData.Id}";
                        if (!string.IsNullOrWhiteSpace(methodUrl))
                        {
                            RestResponseModel responseModel = await WebService.PostData(null, methodUrl, true);
                            if (responseModel != null)
                            {
                                if (responseModel.status_code == 200)
                                {
                                    var CardList = JsonConvert.DeserializeObject<ObservableCollection<CardListModel>>(responseModel.response_body);
                                    if (CardList != null)
                                    {
                                        if (Constant.globalAddedCard != null)
                                        {
                                            if (string.IsNullOrWhiteSpace(Constant.globalAddedCard.Bin))
                                            {
                                                Constant.globalAddedCard = AddCardModel = CardList.Where(c => c.IsPrimary == true).FirstOrDefault()== null ? CardList.FirstOrDefault(): CardList.Where(c => c.IsPrimary == true).FirstOrDefault();
                                                Global.GlobalCardList = CardList;
                                            }
                                            else
                                            {
                                                AddCardModel = Constant.globalAddedCard;
                                            }
                                        }
                                        else
                                        {
                                            AddCardModel = Constant.globalAddedCard;
                                        }
                                    }
                                    // UserDialogs.Instance.HideLoading();
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
                                    //Acr.UserDialogs.UserDialogs.Instance.Alert(responseModel.ErrorMessage);
                                    //UserDialogs.Instance.HideLoading();
                                }
                            }
                            else
                            {
                                //Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                                //UserDialogs.Instance.HideLoading();
                            }
                        }
                        else
                        {
                            // Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                            //UserDialogs.Instance.HideLoading();
                        }
                    }
                    catch (Exception ex)
                    {
                        UserDialogs.Instance.HideLoading();
                        Debug.WriteLine(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                Debug.WriteLine(ex.Message);
            }
            GetAllShippingAddress();
        }
        #endregion

        #region Get Cart List
        public async Task GetCartProductList(bool IsDelete = false)
        {
            try
            {
                Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Loading cart..Please wait");
                await Task.Delay(500);
                string methodUrl = $"/api/Order/GetCartForUser?id={Constant.LoginUserData.Id}";
                if (!string.IsNullOrWhiteSpace(methodUrl))
                {
                    RestResponseModel responseModel = await WebService.GetData(methodUrl, true);
                    if (responseModel != null)
                    {
                        if (responseModel.status_code == 200)
                        {
                            var UserCartResponseModel = JsonConvert.DeserializeObject<UserCartModel>(responseModel.response_body);
                            if (UserCartResponseModel != null)
                            {
                                if (UserCartResponseModel.OrderItems != null)
                                {
                                    if (UserCartResponseModel.OrderItems.Count > 0)
                                    {
                                        var orderItem = UserCartResponseModel.OrderItems.Where(o => o.Product.Id == ProdataModel.Id).FirstOrDefault();
                                        if (orderItem != null)
                                        {
                                            orderItems = new List<PaymentStatusOrderItem>(){new PaymentStatusOrderItem()
                                        {
                                            OrderId = orderItem.OrderId,
                                            OrderItemId = orderItem.OrderItemId,
                                            StatusId = orderItem.StatusId,
                                            Amount = Convert.ToDecimal(GrandTotalPrice.Replace("$", "")),
                                            Tax = Constant.globalTax,
                                            IsPaid = orderItem.IsPaid,
                                            Product = new PaymentProduct()
                                            {
                                                Id = ProdataModel.Id,
                                                UserFirstName = ProdataModel.UserFirstName,
                                                UserId = Convert.ToInt16(ProdataModel.UserId),
                                                UserProfile = ProdataModel.UserProfile,
                                                UserName = ProdataModel.UserName,
                                                UserLastName = ProdataModel.UserLastName,
                                                Source = ProdataModel.Source,
                                                ProductName = ProdataModel.ProductName,
                                                Size = ProdataModel.Size,
                                                ProductRating = ProdataModel.ProductRating,
                                                ProductCondition = ProdataModel.ProductCondition,
                                                ProductColor = ProdataModel.ProductColor,
                                                ProductCategory = ProdataModel.ProductCategory,
                                                ParentCategory = ProdataModel.ParentCategory,
                                                Brand = ProdataModel.Brand,
                                                StoreType = ProdataModel.StoreType,
                                                Quantity = ProdataModel.Quantity,
                                                Availability = ProdataModel.Availability,
                                                TagImage = ProdataModel.TagImage,
                                                Description = ProdataModel.Description,
                                                IsLike = ProdataModel.IsLike,
                                                LikeCount = ProdataModel.LikeCount
                                            }

                                        }
                                    };
                                        }
                                        else
                                        {
                                            IsTap = false;
                                            Acr.UserDialogs.UserDialogs.Instance.Alert(responseModel.ErrorMessage);
                                            UserDialogs.Instance.HideLoading();
                                        }
                                    }
                                    else
                                    {
                                        IsTap = false;
                                        Acr.UserDialogs.UserDialogs.Instance.Alert(responseModel.ErrorMessage);
                                        UserDialogs.Instance.HideLoading();
                                    }

                                }
                                else
                                {
                                    IsTap = false;
                                    Acr.UserDialogs.UserDialogs.Instance.Alert(responseModel.ErrorMessage);
                                    UserDialogs.Instance.HideLoading();
                                }
                            }
                            else
                            {
                                IsTap = false;
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
                            IsTap = false;
                            Acr.UserDialogs.UserDialogs.Instance.Alert(responseModel.ErrorMessage);
                            UserDialogs.Instance.HideLoading();
                        }
                    }
                    else
                    {
                        IsTap = false;
                        Acr.UserDialogs.UserDialogs.Instance.Alert(responseModel.ErrorMessage);
                        UserDialogs.Instance.HideLoading();
                    }
                }

            }
            catch (Exception ex)
            {
                IsTap = false;
                Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                await Acr.UserDialogs.UserDialogs.Instance.AlertAsync(ex.Message);
            }
        }
        #endregion
    }
}
