using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using BuySell.Helper;
using BuySell.Model;
using BuySell.View;
using FFImageLoading.Work;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using System.Collections.Generic;
using BuySell.Model.RestResponse;
using BuySell.WebServices;
using Newtonsoft.Json;
using System.Diagnostics;
using BuySell.Views;
using Acr.UserDialogs;

namespace BuySell.ViewModel
{
    public class ShoppingCartViewModel : BaseViewModel
    {
        int OrderId = 0;
        DashBoardModel productModel;
        UserCartModel UserCartResponseModel;
        #region Constructor
        public ShoppingCartViewModel(INavigation _nav)
        {
            navigation = _nav;
            CartList = new ObservableCollection<CartModel>(Global.globalCartList);
        }

        public ShoppingCartViewModel(INavigation _nav, DashBoardModel _productModel)
        {
            navigation = _nav;
            productModel = _productModel;
            CartList = new ObservableCollection<CartModel>(Global.globalCartList);
        }

        #endregion

        #region Properties
        public int totalItems
        {
            get
            {
                var totalCount = CartList.ToList().Select(i => i.listShoppingCart.Where(j => j.product.IsNotSaleOrSold == false).ToList().Count).Sum();
                return totalCount;
            }
        }

        public int SellerNameWithCount
        {
            get
            {
                var sellerNameWithCount = CartList.ToList().Select(i => i.listShoppingCart.Count).Sum();
                return sellerNameWithCount;
            }
        }

        public string totalItemsCount
        {
            get
            {
                if (totalItems > 1)
                    return string.Format("Total({0} items)", totalItems.ToString());
                else if (totalItems <= 1)
                    return string.Format("Total({0} item)", totalItems.ToString());
                else
                    return string.Format("Total({0} item)", 0.ToString());
            }
        }

        public int totaSellers
        {
            get
            {

                var totalCount = CartList.Distinct().ToList().Select(i => i.SellerName).Count();
                return totalCount;
            }

        }

        public string SubTotalPrice
        {
            get
            {

                var totalPrice = CartList.ToList().Select(i => i.listShoppingCart.Where(k => k.product.IsNotSaleOrSold == false).Sum(j => Convert.ToDecimal(j.Price.Replace("$", "")))).Sum();
                return string.Format("${0:F2}", totalPrice);

            }

        }

        public string TotalPrice
        {
            get
            {

                var totalPrice = CartList.ToList().Select(i => i.listShoppingCart.Where(k => k.product.IsNotSaleOrSold == false).Sum(j => Convert.ToDecimal(j.Price.Replace("$", "")))).Sum() + 14;
                return string.Format("${0:F2}", totalPrice);
            }

        }

        public string TotalSellerTotalItems
        {
            get
            {
                if (totalItems > 1 && CartList.Count <= 1)
                    return string.Format("{0} items from {1} seller", totalItems.ToString(), CartList.Count);
                else if (totalItems > 1 && CartList.Count > 1)
                    return string.Format("{0} items from {1} sellers", totalItems.ToString(), CartList.Count);
                else if (totalItems <= 1 && CartList.Count > 1)
                    return string.Format("{0} item from {1} sellers", totalItems.ToString(), CartList.Count);
                else if (totalItems == 1 && CartList.Count == 1)
                    return string.Format("{0} item from {1} seller", totalItems.ToString(), CartList.Count);
                else if (totalItems == 0 && CartList.Count == 0)
                    return string.Format("{0} items from {1} sellers", totalItems.ToString(), CartList.Count);
                else
                    return string.Format("{0} item from {1} seller", 0.ToString(), CartList.Count.ToString());
            }
        }

        public string EstimatedTotal
        {
            get
            {
                return string.Format("Estimated Total ({0} item ) {1}", totalItems.ToString(), CartList.Count);
            }
        }

        private ObservableCollection<CartModel> _CartList = new ObservableCollection<CartModel>();
        public ObservableCollection<CartModel> CartList
        {
            get { return _CartList; }
            set
            {
                _CartList = value;
                OnPropertyChanged("CartList");
                if (CartList.Count > 0)
                {
                    IsNoData = false;
                }
                else
                {
                    IsNoData = true;
                }

            }
        }

        private ObservableCollection<SummaryCartModel> _OrderSummaryList = new ObservableCollection<SummaryCartModel>();
        public ObservableCollection<SummaryCartModel> OrderSummaryList
        {
            get { return _OrderSummaryList; }
            set { _OrderSummaryList = value; OnPropertyChanged("OrderSummaryList"); }
        }

        private bool _IsShowBagList;
        public bool IsShowBagList
        {
            get { return _IsShowBagList; }
            set { _IsShowBagList = value; OnPropertyChanged("IsShowBagList"); }
        }
        //private bool _IsNoData;
        //public bool IsNoData
        //{
        //    get { return _IsNoData; }
        //    set { _IsNoData = value; OnPropertyChanged("IsNoData"); }
        //}
        #endregion

        #region Commands
        private Command _checkOutCommand;
        public Command CheckOutCommand
        {
            get
            {
                return _checkOutCommand ?? (_checkOutCommand = new Command(async (obj) =>
                {
                    try
                    {
                        if (Global.globalCartList.Count != 0)
                        {
                            var listUnLoadItems = ((CartModel)obj).listShoppingCart.Where(i => i.product.IsNotSaleOrSold == false).ToList(); ;
                            if (listUnLoadItems != null && listUnLoadItems.Count == 0)
                            {
                                Acr.UserDialogs.UserDialogs.Instance.Alert("Cart items already sold, please choose another items.");
                                return;
                            }
                            List <DashBoardModel> listProduct = new List<DashBoardModel>();
                            var objCardModel = (CartModel)obj;
                            //var objShopModel = objCardModel.listShoppingCart.FirstOrDefault();
                            foreach (var objShopModel in objCardModel.listShoppingCart)
                            {
                                if (!objShopModel.product.IsNotSaleOrSold)
                                {
                                    var objDataModel = new DashBoardModel();
                                    objDataModel.ProductName = objCardModel.product.ProductName;
                                    objDataModel.Price = objShopModel.Price;
                                    objDataModel.ProductCatIcon = objShopModel.ProductCatIcon;
                                    objDataModel.ProductCatName = objShopModel.ProductCatName;
                                    objDataModel.ProductImage = objShopModel.ProductImage;
                                    objDataModel.Size = objShopModel.Size;
                                    objDataModel.Brand = objShopModel.Brand;
                                    objDataModel.StoreType = objShopModel.product.StoreType;
                                    objDataModel.ThemeCol = objShopModel.product.ThemeCol;
                                    objDataModel.Id = objShopModel.product.Id;
                                    objDataModel.UserId = objShopModel.product.UserId;
                                    objDataModel.ShippingPrice = objShopModel.product.ShippingPrice;
                                    objDataModel.otherImages = objShopModel.product.otherImages;
                                    objDataModel.Availability = objShopModel.product.Availability;
                                    listProduct.Add(objDataModel);
                                }
                            }
                            await navigation.PushAsync(new OrderDetailPage(listProduct, UserCartResponseModel.OrderItems));
                        }
                        else
                        {
                            Acr.UserDialogs.UserDialogs.Instance.Alert("Please add product.");
                        }

                    }
                    catch (Exception ex)
                    {
                        Acr.UserDialogs.UserDialogs.Instance.Alert(ex.Message);
                    }
                }
          )); ;
            }
        }

        private Command _DeleteCommand;
        public Command DeleteCommand
        {
            get
            {
                return _DeleteCommand ?? (_DeleteCommand = new Command(async (obj) =>
                {
                    try
                    {
                        if (!Constant.IsConnected)
                        {
                            UserDialogs.Instance.Toast("Internet not available");
                            IsTap = false;
                            return;
                        }


                        var orderItem = UserCartResponseModel.OrderItems.Where(o => o.Product.Id== ((ShoppingCartModel)obj).product.Id).ToList().FirstOrDefault();

                        try
                        {
                            if (IsTap)
                                return;

                            IsTap = true;
                            var confirm = await Acr.UserDialogs.UserDialogs.Instance.ConfirmAsync(Helper.Constant.deleteConfirmMsgStr, okText: Helper.Constant.OKStr, cancelText: Helper.Constant.CancelStr);
                            if (confirm)
                            {

                               var cartdeleteReq =new DeleteCartRequest()
                                {
                                    OrderId = orderItem.OrderId,
                                    OrderItemId = orderItem.OrderItemId,
                                    StatusId = orderItem.StatusId,
                                    Amount = orderItem.Amount,
                                    Tax = orderItem.Tax,
                                    IsPaid = orderItem.IsPaid,
                                    Product = new DeleteCartProduct()
                                    {
                                        Id = orderItem.Product.Id,
                                        UserFirstName = orderItem.Product.UserFirstName,
                                        UserId = Convert.ToInt16(orderItem.Product.UserId),
                                        UserProfile = orderItem.Product.UserProfile,
                                        UserName = orderItem.Product.UserName,
                                        UserLastName = orderItem.Product.UserLastName,
                                        Source = orderItem.Product.Source,
                                        ProductName = orderItem.Product.ProductName,
                                        Size = orderItem.Product.Size,
                                        ProductRating = orderItem.Product.ProductRating,
                                        ProductCondition = orderItem.Product.ProductCondition,
                                        ProductColor = orderItem.Product.ProductColor,
                                        ProductCategory = orderItem.Product.ProductCategory,
                                        ParentCategory = orderItem.Product.ParentCategory,
                                        Brand = orderItem.Product.Brand,
                                        StoreType = orderItem.Product.StoreType,
                                        Quantity = orderItem.Product.Quantity,
                                        Availability = orderItem.Product.Availability,
                                        TagImage = orderItem.Product.TagImage,
                                        Description = orderItem.Product.Description,
                                        IsLike = orderItem.Product.IsLike,
                                        LikeCount = orderItem.Product.LikeCount
                                    }

                                };

                                //uncomment below line when delete shipping address API applied.
                                //Global.GetAllShippingAddress();
                                try
                                {
                                    
                                    Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Deleting cart item, Please wait...");
                                    await Task.Delay(50);
                                    string methodUrl = $"/api/Order/DeleteCartItem";

                                    if (!string.IsNullOrWhiteSpace(methodUrl))
                                    {
                                        RestResponseModel responseModel = await WebService.PostData(cartdeleteReq, methodUrl, true);
                                        if (responseModel != null)
                                        {
                                            if (responseModel.status_code == 200)
                                            {
                                                productModel = null;
                                                Global.AlertWithAction("Cart item removed successfully!!", async () =>
                                                {
                                                    IsTap = false;
                                                    await GetCartProductList(true);
                                                });
                                                //var alertConfig = new AlertConfig
                                                //{
                                                //    Message = "Cart item removed successfully",
                                                //    OkText = "OK",
                                                //    OnAction = async () =>
                                                //    {
                                                //        IsTap = false;
                                                //        await GetCartProductList(true);
                                                //    }
                                                //};
                                                //Acr.UserDialogs.UserDialogs.Instance.Alert(alertConfig);
                                                UserDialogs.Instance.HideLoading();
                                                foreach (var objcart in Global.globalCartList)
                                                {
                                                    objcart.listShoppingCart.Remove((ShoppingCartModel)obj);
                                                    //objcart.SellerName = objcart.product.UserName + "(" + objcart.listShoppingCart.Count + ")";
                                                    objcart.SellerNameWithCount = objcart.product.UserName + "(" + objcart.listShoppingCart.Count + ")";
                                                    if (objcart.listShoppingCart.Count == 0)
                                                    {
                                                        Global.globalCartList.Remove(objcart);
                                                        CartList.Remove(objcart);
                                                        break;
                                                    }
                                                }

                                                CartList = new ObservableCollection<CartModel>(Global.globalCartList);
                                                OnPropertyChanged("totalItems");
                                                OnPropertyChanged("EstimatedTotal");
                                                OnPropertyChanged("TotalSellerTotalItems");
                                                OnPropertyChanged("SubTotalPrice");
                                                OnPropertyChanged("TotalPrice");
                                                OnPropertyChanged("SellerNameWithCount");
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
                                catch (Exception ex)
                                {
                                    IsTap = false;
                                    Acr.UserDialogs.UserDialogs.Instance.Alert(ex.Message);
                                    UserDialogs.Instance.HideLoading();
                                }

                            }
                            IsTap = false;
                        }
                        catch (Exception ex)
                        {
                            IsTap = false;
                            Acr.UserDialogs.UserDialogs.Instance.Alert(ex.Message);
                            UserDialogs.Instance.HideLoading();
                        }

                        
                    }
                    catch (Exception ex)
                    {

                    }

                    //CartList = new ObservableCollection<CartModel>(Global.globalCartList);
                    //OnPropertyChanged("totalItems");
                    //OnPropertyChanged("EstimatedTotal");
                    //OnPropertyChanged("TotalSellerTotalItems");
                    //OnPropertyChanged("SubTotalPrice");
                    //OnPropertyChanged("TotalPrice");
                    //OnPropertyChanged("SellerNameWithCount");
                }
                 ));
            }
        }

        private Command _BackCommand;
        public Command BackCommand
        {
            get
            {
                return _BackCommand ?? (_BackCommand = new Command(async (obj) =>
                {
                    await navigation.PopModalAsync();
                }));
            }
        }

        private Command _TopTrandingItemsCommand;
        public Command TopTrandingItemsCommand
        {
            get
            {
                return _TopTrandingItemsCommand ?? (_TopTrandingItemsCommand = new Command(async (obj) => await ExicuteTopTrandingCommond(obj)));
            }
        }

        private async Task ExicuteTopTrandingCommond(object obj)
        {
            if (IsTap)
                return;
            IsTap = true;

            var cartObj = (ShoppingCartModel)obj;
            //var prodObj = CartList.Where(x => x.listShoppingCart.Where(c => c.product.Id == Convert.ToInt16(cartObj.ProductID)) != null);
            await navigation.PushAsync(new ItemDetailsPage(cartObj.product, false));
        }
        #endregion

        #region Methods
        async public void GetProductList()
        {
            //CartList = new ObservableCollection<CartModel>(Global.globalCartList);
            await GetCartProductList();
        }

        public void GetOrderSummaryList()
        {
            OrderSummaryList.Clear();
            OrderSummaryList = new ObservableCollection<SummaryCartModel>()
                {
                new SummaryCartModel()
                {
                    listOrderSummary = new System.Collections.Generic.List<OrderSummaryListModel>()
                    {
                   new OrderSummaryListModel()
                    {
                         Title="Deliver to",
                         Description="Cara Whitehead 123 Washington st, Princeton, NJ 45678",
                         Image = "NextArrow",
                    },
                    }
                },
                 new SummaryCartModel()
                {
                    listOrderSummary = new System.Collections.Generic.List<OrderSummaryListModel>()
                    {
                   new OrderSummaryListModel()
                    {
                         Title="Card and Billing",
                         Description="****1234 05/23 123 Main St, Chicago, IL 12345",
                         Image = "NextArrow",
                    },

                    }
                 },
                };
        }


        public async Task GetCartProductList(bool IsDelete=false)
        {
            try
            {
                if (!Constant.IsConnected)
                {
                    UserDialogs.Instance.Toast("Internet not available");
                    IsTap = false;
                    return;
                }

                Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Loading, Please wait");
                await Task.Delay(500);
                string methodUrl = $"/api/Order/GetCartForUser?id={Constant.LoginUserData.Id}";
                if (!string.IsNullOrWhiteSpace(methodUrl))
                {
                    RestResponseModel responseModel = await WebService.GetData(methodUrl, true);
                    if (responseModel != null)
                    {
                        if (responseModel.status_code == 200)
                        {
                            UserCartResponseModel = JsonConvert.DeserializeObject<UserCartModel>(responseModel.response_body);
                            if (UserCartResponseModel != null)
                            {
                                if (UserCartResponseModel.OrderItems != null)
                                {
                                    Global.globalCartList.Clear();
                                    if (UserCartResponseModel.OrderItems.Count > 0)
                                    {
                                        var productlist = UserCartResponseModel.OrderItems.Select(o => o.Product).ToList();
                                        var grpProductList = productlist.GroupBy(p => p.UserId).ToList();
                                        foreach (var objorder in grpProductList)
                                        {
                                            foreach (var objPro in objorder)
                                            {
                                                CartModel objcart = new CartModel();
                                                if (Global.globalCartList.Count > 0)
                                                {
                                                    var objCart = Global.globalCartList.Where(c => c.SellerName == objPro.UserId).FirstOrDefault();
                                                    if (objCart != null)
                                                    {
                                                        var IsResult = objCart.listShoppingCart.Where(x => x.ProductID == objPro.Id.ToString()).ToList();
                                                        if (IsResult.Count == 0)
                                                        {
                                                            objCart.listShoppingCart.Add(new ShoppingCartModel()
                                                            {
                                                                ProductID = objPro.Id.ToString(),
                                                                Price = objPro.Price,
                                                                Size = objPro.Size,
                                                                ProductName = objPro.ProductName,
                                                                ProductImage = objPro.ProductImage,
                                                                Brand = objPro.Brand,
                                                                product = objPro
                                                            });
                                                            objCart.SellerName = objPro.UserId;
                                                            objCart.SellerNameWithCount = objPro.UserName + "(" + objCart.listShoppingCart.Count + ")";
                                                            objCart.product = objPro;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        // var objcart = new CartModel();
                                                        objcart.PromoCode = "";
                                                        objcart.product = objPro;
                                                        objcart.listShoppingCart = new List<ShoppingCartModel>()
                                            {
                                                new ShoppingCartModel() {
                                                     ProductID = objPro.Id.ToString(),
                                                Price = objPro.Price,
                                                Size=objPro.Size,
                                                ProductName = objPro.ProductName,
                                                ProductImage = objPro.ProductImage,
                                                Brand = objPro.Brand,
                                                product = objPro
                                            }

                                            };
                                                        objcart.SellerName = objPro.UserId;
                                                        objcart.SellerNameWithCount = objPro.UserName + "(" + objcart.listShoppingCart.Count + ")";
                                                        objcart.product = objPro;
                                                        Global.globalCartList.Add(objcart);
                                                    }
                                                }
                                                else
                                                {
                                                    // var objcart = new CartModel();
                                                    objcart.PromoCode = "";
                                                    objcart.product = objPro;
                                                    objcart.listShoppingCart = new List<ShoppingCartModel>()
                                                {
                                                    new ShoppingCartModel()
                                                    {
                                                     ProductID = objPro.Id.ToString(),
                                                    Price = objPro.Price,
                                                    Size=objPro.Size,
                                                    ProductName = objPro.ProductName,
                                                    ProductImage = objPro.ProductImage,
                                                    Brand = objPro.Brand,
                                                    product = objPro
                                                    }};

                                                    objcart.SellerName = objPro.UserId;
                                                    objcart.SellerNameWithCount = objPro.UserName + "(" + objcart.listShoppingCart.Count + ")";
                                                    objcart.product = objPro;
                                                    Global.globalCartList.Add(objcart);
                                                }
                                            }
                                        }
                                        AddCartItem(IsDelete);
                                    }
                                    else
                                    {
                                        Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                                        AddCartItem(IsDelete);
                                    }
                                }
                                else
                                {
                                    Global.globalCartList.Clear();
                                    Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                                    AddCartItem(IsDelete);
                                }
                            }
                            else
                            {
                                Global.globalCartList.Clear();
                                Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                            }
                            Debug.WriteLine(UserCartResponseModel);
                        }
                        else if (responseModel.status_code == 500)
                        {
                            Global.globalCartList.Clear();
                            ResponseBodyModel responseBodyModel = JsonConvert.DeserializeObject<ResponseBodyModel>(responseModel.response_body);
                            IsTap = false;
                            Acr.UserDialogs.UserDialogs.Instance.Alert(responseBodyModel.Message);
                            UserDialogs.Instance.HideLoading();
                        }
                        else
                        {
                            Global.globalCartList.Clear();
                            Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                        }
                    }
                    else
                    {
                        Global.globalCartList.Clear();
                        Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                    }
                }
                else
                {
                    Global.globalCartList.Clear();
                    Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                }
            }
            catch (Exception ex)
            {
                Global.globalCartList.Clear();
                Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                await Acr.UserDialogs.UserDialogs.Instance.AlertAsync(ex.Message);
            }
        }

        async void AddCartItem(bool IsDelete = false)
        {
            if (productModel != null)
            {
                if (!IsDelete)
                {
                    if (Global.globalCartList.Where(c => c.product.Id == productModel.Id).FirstOrDefault() == null)
                    {
                        var ordernumber = await OrderNumberGenerator(productModel.Id, UserCartResponseModel.OrderId);
                        await GetCartProductList();
                        //var objcart = new CartModel();
                        //objcart.PromoCode = "";
                        //objcart.product = productModel;
                        //objcart.listShoppingCart = new List<ShoppingCartModel>(){
                        //                                    new ShoppingCartModel() {
                        //                                    ProductID = productModel.Id.ToString(),
                        //                                    Price = productModel.Price,
                        //                                    Size=productModel.Size,
                        //                                    ProductName = productModel.ProductName,
                        //                                    ProductImage = productModel.ProductImage,
                        //                                    Brand = productModel.Brand,
                        //                                    product = productModel
                        //                                    }};
                        //objcart.SellerName = productModel.UserId;
                        //objcart.SellerNameWithCount = productModel.UserName + "(" + objcart.listShoppingCart.Count + ")";
                        //objcart.product = productModel;
                        ////Global.globalCartList.Add(objcart);
                        //CartModel objcart = new CartModel();
                        //if (Global.globalCartList.Count > 0)
                        //{
                        //    var objCart = Global.globalCartList.Where(c => c.SellerName == productModel.UserId).FirstOrDefault();
                        //    if (objCart != null)
                        //    {
                        //        var IsResult = objCart.listShoppingCart.Where(x => x.ProductID == productModel.Id.ToString()).ToList();
                        //        if (IsResult.Count == 0)
                        //        {
                        //            objCart.listShoppingCart.Add(new ShoppingCartModel()
                        //            {
                        //                ProductID = productModel.Id.ToString(),
                        //                Price = productModel.Price,
                        //                Size = productModel.Size,
                        //                ProductName = productModel.ProductName,
                        //                ProductImage = productModel.ProductImage,
                        //                Brand = productModel.Brand,
                        //                product = productModel
                        //            });
                        //            objCart.SellerName = productModel.UserId;
                        //            objCart.SellerNameWithCount = productModel.UserName + "(" + objCart.listShoppingCart.Count + ")";
                        //            objCart.product = productModel;
                        //        }
                        //    }
                        //    else
                        //    {
                        //        // var objcart = new CartModel();
                        //        objcart.PromoCode = "";
                        //        objcart.product = productModel;
                        //        objcart.listShoppingCart = new List<ShoppingCartModel>()
                        //                        {
                        //                            new ShoppingCartModel() {
                        //                                 ProductID = productModel.Id.ToString(),
                        //                            Price = productModel.Price,
                        //                            Size=productModel.Size,
                        //                            ProductName = productModel.ProductName,
                        //                            ProductImage = productModel.ProductImage,
                        //                            Brand = productModel.Brand,
                        //                            product = productModel
                        //                        }

                        //                        };
                        //        objcart.SellerName = productModel.UserId;
                        //        objcart.SellerNameWithCount = productModel.UserName + "(" + objcart.listShoppingCart.Count + ")";
                        //        objcart.product = productModel;
                        //        Global.globalCartList.Add(objcart);
                        //    }
                        //}
                        //else
                        //{
                        //    // var objcart = new CartModel();
                        //    objcart.PromoCode = "";
                        //    objcart.product = productModel;
                        //    objcart.listShoppingCart = new List<ShoppingCartModel>()
                        //                            {
                        //                                new ShoppingCartModel()
                        //                                {
                        //                                 ProductID = productModel.Id.ToString(),
                        //                                Price = productModel.Price,
                        //                                Size=productModel.Size,
                        //                                ProductName = productModel.ProductName,
                        //                                ProductImage = productModel.ProductImage,
                        //                                Brand = productModel.Brand,
                        //                                product = productModel
                        //                                }};

                        //    objcart.SellerName = productModel.UserId;
                        //    objcart.SellerNameWithCount = productModel.UserName + "(" + objcart.listShoppingCart.Count + ")";
                        //    objcart.product = productModel;
                        //    Global.globalCartList.Add(objcart);
                        //}

                    }
                    else
                    {
                        CartList = new ObservableCollection<CartModel>();
                    }
                }
            }

            CartList = new ObservableCollection<CartModel>(Global.globalCartList);
            if (CartList.Count > 0)
            {
                IsShowBagList = true;
                IsNoData = false;
            }
            else
            {
                IsShowBagList = false;
                IsNoData = true;
            }
            Acr.UserDialogs.UserDialogs.Instance.HideLoading();

            OnPropertyChanged("totalItems");
            OnPropertyChanged("EstimatedTotal");
            OnPropertyChanged("TotalSellerTotalItems");
            OnPropertyChanged("SubTotalPrice");
            OnPropertyChanged("TotalPrice");
            OnPropertyChanged("SellerNameWithCount");
        }

        #endregion
    }
}