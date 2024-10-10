using Acr.UserDialogs;
using BuySell.Helper;
using BuySell.Model;
using BuySell.Model.RestResponse;
using BuySell.View;
using BuySell.Views;
using BuySell.WebServices;
using FFImageLoading.Helpers.Exif;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BuySell.ViewModel
{
    public class ItemDetailsViewModel : BaseViewModel
    {
        #region Constructor
        bool IsLoggedIn = false;
        public ItemDetailsViewModel(INavigation _nav, DashBoardModel dataModel, bool _IsLoggedIn)
        {
            navigation = _nav;
            ProdataModel = dataModel;
            IsLoggedIn = _IsLoggedIn;

            //Condition added to set the Product Condition only short form
            if (ProdataModel != null && !string.IsNullOrEmpty(ProdataModel.ProductCondition))
            {
                switch (ProdataModel.ProductCondition.ToLower())
                {
                    case "nwt(new with price tag attached)":
                        ProdataModel.ProductCondition = "NWT";
                        break;
                    case "nwot(new with price tag removed)":
                        ProdataModel.ProductCondition = "NWOT";
                        break;
                    case "used(preowned)":
                        ProdataModel.ProductCondition = "Used";
                        break;
                }
            }
        }
        #endregion

        #region Properties
        private DashBoardModel _ProdataModel;
        public DashBoardModel ProdataModel
        {
            get { return _ProdataModel; }
            set { _ProdataModel = value; OnPropertyChanged("ProdataModel"); }
        }

        private ObservableCollection<GroupItemModel> _list = new ObservableCollection<GroupItemModel>();
        public ObservableCollection<GroupItemModel> list
        {
            get { return _list; }
            set { _list = value; OnPropertyChanged("list"); }
        }

        private GroupItemModel _CurrentImage;
        public GroupItemModel CurrentImage
        {
            get { return _CurrentImage; }
            set { _CurrentImage = value; OnPropertyChanged("CurrentImage"); }
        }


        private bool _IsMakeOfferShow = true;
        public bool IsMakeOfferShow
        {
            get { return _IsMakeOfferShow; }
            set { _IsMakeOfferShow = value; OnPropertyChanged("IsMakeOfferShow"); }
        }

        private bool _IsViewofferfrmShow = false;
        public bool IsViewofferfrmShow
        {
            get { return _IsViewofferfrmShow; }
            set { _IsViewofferfrmShow = value; OnPropertyChanged("IsViewofferfrmShow"); }
        }
        private bool _IsEditfrmShow;
        public bool IsEditfrmShow
        {
            get { return _IsEditfrmShow; }
            set { _IsEditfrmShow = value; OnPropertyChanged("IsEditfrmShow"); }
        }
        private bool _RainbowImgShow;
        public bool RainbowImgShow
        {
            get { return _RainbowImgShow; }
            set { _RainbowImgShow = value; OnPropertyChanged("RainbowImgShow"); }
        }

        private bool _CamoImgShow;
        public bool CamoImgShow
        {
            get { return _CamoImgShow; }
            set { _CamoImgShow = value; OnPropertyChanged("CamoImgShow"); }
        }
        private Color _ProdHexColor;
        public Color ProdHexColor
        {
            get { return _ProdHexColor; }
            set { _ProdHexColor = value; OnPropertyChanged("ProdHexColor"); }
        }

        private bool _IsParentCatShow;
        public bool IsParentCatShow
        {
            get { return _IsParentCatShow; }
            set { _IsParentCatShow = value; OnPropertyChanged("IsParentCatShow"); }
        }

        public bool IsSoldOrNotForSale
        {
            get
            {
                if (Constant.LoginUserData != null)
                {
                    if (Constant.LoginUserData.Id == Convert.ToUInt32(ProdataModel.UserId))
                    {
                        return false;
                    }
                }

                //return false;
                return !ProdataModel.IsNotSaleOrSold;
            }
        }


        public double IsMakeOfferShowOpacity
        {
            get
            {
                return IsSoldOrNotForSale ? 1.0 : 0.5;
            }
        }

        #endregion

        #region Methods
        public void GetList()
        {
            try
            {
                if (ProdataModel.otherImages == null)
                {
                    list = new ObservableCollection<GroupItemModel>()
               {
                new GroupItemModel()
                {
                    GroupImage = ProdataModel.ProductImage,
                    ThemeColor=ThemeColor
                }
                };
                }
                else
                {
                    if (list != null)
                    {
                        list.Clear();
                    }
                    foreach (var otherimg in ProdataModel.otherImages)
                    {
                        list.Add(new GroupItemModel()
                        {
                            GroupImage = otherimg,
                            ThemeColor = ThemeColor
                        });
                    }
                    list.Add(new GroupItemModel()
                    {
                        GroupImage = ProdataModel.TagImage,
                        ThemeColor = ThemeColor
                    });
                }
            }
            catch (Exception ex)
            {

            }
        }
        public async Task GetBuyNowMethod()
        {
            try
            {
                if (IsTap)
                    return;
                IsTap = true;

                await navigation.PushAsync(new OrderDetailPage(ProdataModel));

                //if (Global.globalCartList.Count > 0)
                //{
                //    var objCart = Global.globalCartList.Where(c => c.SellerName == ProdataModel.UserId).FirstOrDefault();
                //    if (objCart != null)
                //    {
                //        var IsResult = objCart.listShoppingCart.Where(x => x.ProductID == ProdataModel.Id.ToString()).ToList();
                //        if (IsResult.Count == 0)
                //        {
                //            objCart.listShoppingCart.Add(new ShoppingCartModel()
                //            {
                //                ProductID = ProdataModel.Id.ToString(),
                //                Price = ProdataModel.Price,
                //                Size = ProdataModel.Size,
                //                ProductName = ProdataModel.ProductName,
                //                ProductImage = ProdataModel.ProductImage,
                //                Brand = ProdataModel.Brand,
                //                product = ProdataModel
                //            });
                //            objCart.SellerName = ProdataModel.UserId;
                //            objCart.SellerNameWithCount = ProdataModel.UserName + "(" + objCart.listShoppingCart.Count + ")";
                //            objCart.product = ProdataModel;
                //        }
                //    }
                //    else
                //    {
                //        var objcart = new CartModel();
                //        objcart.PromoCode = "";
                //        objcart.product = ProdataModel;
                //        objcart.listShoppingCart = new List<ShoppingCartModel>(){
                //                                        new ShoppingCartModel() {
                //                                        ProductID = ProdataModel.Id.ToString(),
                //                                        Price = ProdataModel.Price,
                //                                        Size=ProdataModel.Size,
                //                                        ProductName = ProdataModel.ProductName,
                //                                        ProductImage = ProdataModel.ProductImage,
                //                                        Brand = ProdataModel.Brand,
                //                                        product = ProdataModel
                //                                        }};
                //        objcart.SellerName = ProdataModel.UserId;
                //        objcart.SellerNameWithCount = ProdataModel.UserName + "(" + objcart.listShoppingCart.Count + ")";
                //        objcart.product = ProdataModel;
                //        Global.globalCartList.Add(objcart);
                //    }

                //    await navigation.PushAsync(new ShoppingCardPage());
                //}
                //else
                //{
                //    await navigation.PushAsync(new OrderDetailPage(ProdataModel));
                //}
            }
            catch (Exception ex)
            {
                IsTap = false;
                Debug.WriteLine(ex.Message);
            }
        }
        #endregion

        #region Commands
        private Command _BuyNowCommand;
        public Command BuyNowCommand
        {
            get { return _BuyNowCommand ?? (_BuyNowCommand = new Command(async () => await GetBuyNowMethod())); }
        }

        private Command _BackCommand;
        public Command BackCommand
        {
            get
            {
                return _BackCommand ?? (_BackCommand = new Command(async () =>
                {
                    if (navigation != null)
                    {
                        try
                        {
                            if (IsTap)
                                return;
                            IsTap = true;
                            //MessagingCenter.Unsubscribe<object>(Constant.UpdateThemeStr, Constant.UpdateThemeStr);
                            //MessagingCenter.Unsubscribe<object>(Constant.UpdateHeaderThemeStr, Constant.UpdateHeaderThemeStr);
                            //MessagingCenter.Unsubscribe<object, ThemesColor>("UpdatethemeForDetail", "UpdatethemeForDetail");
                            //MessagingCenter.Unsubscribe<object, ThemesColor>("UpdateHeaderthemeForDetail", "UpdateHeaderthemeForDetail");
                            await navigation.PopAsync(true);

                        }
                        catch (Exception ex)
                        {
                            IsTap = false;
                            Debug.WriteLine(ex.Message);
                        }

                    }
                }
             ));
            }
        }

        //private Command _ViewImageCmd;
        //public Command ViewImageCmd
        //{
        //    get { return _ViewImageCmd ?? (_ViewImageCmd = new Command(async () => await ViewImagemethod())); }
        //}

        private Command _MakeAnOfferCommand;
        public Command MakeAnOfferCommand
        {
            get
            {
                return _MakeAnOfferCommand ?? (_MakeAnOfferCommand = new Command(async () =>
                {
                    try
                    {
                        UserDialogs.Instance.Alert("", "Coming soon", "OK");
                        return;


                        if (IsTap)
                            return;
                        IsTap = true;
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
                            await navigation.PushModalAsync(new NavigationPage(new MakeAnOfferPage(ProdataModel)));
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

        private Command _ShareCommand;
        public Command ShareCommand
        {
            get
            {
                return _ShareCommand ?? (_ShareCommand = new Command(async () =>
                {
                    try
                    {
                        //.Instance.Alert("", "Coming soon", "OK");
                        //return;
                        await Share.RequestAsync(new ShareTextRequest
                        {
                            Text = !string.IsNullOrEmpty(ProdataModel.Description) ? ProdataModel.Description : "",
                            Title = ProdataModel.ProductCatName,
                            Uri = ProdataModel.otherImages.Count > 0 ? ProdataModel.otherImages[0].ToString() : ""

                        }); ;
                    }
                    catch (Exception ex)
                    {

                    }

                }));
            }
        }

        private Command _AddtoCartCommand;
        public Command AddtoCartCommand
        {
            get
            {
                return _AddtoCartCommand ?? (_AddtoCartCommand = new Command(async () =>
                {
                    //CartModel objcart = new CartModel();
                    //if (Global.globalCartList.Count > 0)
                    //{
                    //    var objCart = Global.globalCartList.Where(c => c.SellerName == ProdataModel.UserId).FirstOrDefault();
                    //    if (objCart != null)
                    //    {
                    //        var IsResult = objCart.listShoppingCart.Where(x => x.ProductID == ProdataModel.Id.ToString()).ToList();
                    //        if (IsResult.Count == 0)
                    //        {
                    //            objCart.listShoppingCart.Add(new ShoppingCartModel()
                    //            {
                    //                ProductID = ProdataModel.Id.ToString(),
                    //                Price = ProdataModel.Price,
                    //                Size = ProdataModel.Size,
                    //                ProductName = ProdataModel.ProductName,
                    //                ProductImage = ProdataModel.ProductImage,
                    //                Brand = ProdataModel.Brand,
                    //                product = ProdataModel
                    //            });
                    //            objCart.SellerName = ProdataModel.UserId;
                    //            objCart.SellerNameWithCount = ProdataModel.UserName + "(" + objCart.listShoppingCart.Count + ")";
                    //            objCart.product = ProdataModel;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        // var objcart = new CartModel();
                    //        objcart.PromoCode = "";
                    //        objcart.product = ProdataModel;
                    //        objcart.listShoppingCart = new List<ShoppingCartModel>()
                    //{
                    //    new ShoppingCartModel() {
                    //         ProductID = ProdataModel.Id.ToString(),
                    //    Price = ProdataModel.Price,
                    //    Size=ProdataModel.Size,
                    //    ProductName = ProdataModel.ProductName,
                    //    ProductImage = ProdataModel.ProductImage,
                    //    Brand = ProdataModel.Brand,
                    //    product = ProdataModel
                    //}

                    //};
                    //        objcart.SellerName = ProdataModel.UserId;
                    //        objcart.SellerNameWithCount = ProdataModel.UserName + "(" + objcart.listShoppingCart.Count + ")";
                    //        objcart.product = ProdataModel;
                    //        Global.globalCartList.Add(objcart);
                    //    }
                    //}
                    //else
                    //{
                    //   // var objcart = new CartModel();
                    //    objcart.PromoCode = "";
                    //    objcart.product = ProdataModel;
                    //    objcart.listShoppingCart = new List<ShoppingCartModel>()
                    //    {
                    //        new ShoppingCartModel()
                    //        {
                    //         ProductID = ProdataModel.Id.ToString(),
                    //        Price = ProdataModel.Price,
                    //        Size=ProdataModel.Size,
                    //        ProductName = ProdataModel.ProductName,
                    //        ProductImage = ProdataModel.ProductImage,
                    //        Brand = ProdataModel.Brand,
                    //        product = ProdataModel
                    //        }};

                    //    objcart.SellerName = ProdataModel.UserId;
                    //    objcart.SellerNameWithCount = ProdataModel.UserName + "(" + objcart.listShoppingCart.Count + ")";
                    //    objcart.product = ProdataModel;
                    //    Global.globalCartList.Add(objcart);
                    //}
                    try
                    {
                        if (IsTap)
                            return;
                        IsTap = true;
                        // calling API for Shopping cart Order Id generation.
                        await navigation.PushModalAsync(new NavigationPage(new ShoppingCardPage(ProdataModel)));

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

        //private async Task ViewImagemethod()
        //{
        //    try
        //    {
        //        List<ImageSource> SingleImage = new List<ImageSource>();

        //        SingleImage.Add(CurrentImage.GroupImage);

        //        var popupdefault = new ViewMoreImgPopup(SingleImage);
        //        await PopupNavigation.Instance.PushAsync(popupdefault);
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex.Message + "From ItemDetailViewModel");
        //    }
        //}

        private Command _EditCommand;
        public Command EditCommand
        {
            get
            {
                return _EditCommand ?? (_EditCommand = new Command(async () =>
                {
                    try
                    {
                        if (IsTap)
                            return;
                        IsTap = true;
                        await navigation.PushAsync(new SellerClosetDetailsView(ProdataModel));
                    }
                    catch (Exception ex)
                    {
                        IsTap = false;
                        Debug.WriteLine(ex.Message);
                    }

                }));
            }
        }

        private Command _MessageCommand;
        public Command MessageCommand
        {
            get
            {
                return _MessageCommand ?? (_MessageCommand = new Command(async () =>
                {
                    try
                    {
                        //UserDialogs.Instance.Alert("", "Coming soon", "OK");
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
        #endregion
    }
}