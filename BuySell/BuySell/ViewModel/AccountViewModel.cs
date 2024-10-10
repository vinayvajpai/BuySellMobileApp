using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
using BuySell.Helper;
using BuySell.Model;
using BuySell.Model.RestResponse;
using BuySell.View;
using BuySell.Views;
using BuySell.Views.BuyingSellingViews;
using BuySell.WebServices;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BuySell.ViewModel
{
    public class AccountViewModel : BaseViewModel
    {
        public MyEarningsResponseModel earningresponseModel;
        #region Constructor
        public AccountViewModel(INavigation _nav)
        {
            GetViewOffersList();
            navigation = _nav;
        }
        #endregion

        #region Properties
        public string AppVersion => AppInfo.VersionString + "." + AppInfo.BuildString;
        private ObservableCollection<ViewOffersModel> _ViewOffersList = new ObservableCollection<ViewOffersModel>();
        public ObservableCollection<ViewOffersModel> ViewOffersList
        {
            get { return _ViewOffersList; }
            set { _ViewOffersList = value; OnPropertyChanged("ViewOffersList"); }
        }
        private string _OfferCount; //= Global.OfferProductList != null ? "Offer(" + Convert.ToString(Global.OfferProductList.Count) + ")" : "Offer(0)";
        public string OfferCount
        {
            get { return _OfferCount; }
            set { _OfferCount = value; OnPropertyChanged("OfferCount"); }
        }
        private bool _AddProPictxt = true;
        public bool AddProPictxt
        {
            get { return _AddProPictxt; }
            set { _AddProPictxt = value; OnPropertyChanged("AddProPictxt"); }
        }
        private bool _IsFilterShow = true;
        public bool IsFilterShow
        {
            get { return _IsFilterShow; }
            set { _IsFilterShow = value; OnPropertyChanged("IsFilterShow"); }
        }
        private bool _IsOfferShow = false;
        public bool IsOfferShow
        {
            get { return _IsOfferShow; }
            set { _IsOfferShow = value; OnPropertyChanged("IsOfferShow"); }
        }
        private bool _IsAccountShow = true;
        public bool IsAccountShow
        {
            get { return _IsAccountShow; }
            set { _IsAccountShow = value; OnPropertyChanged("IsAccountShow"); }
        }

        private string _myearning="Earnings $0.00"; //= Global.OfferProductList != null ? "Offer(" + Convert.ToString(Global.OfferProductList.Count) + ")" : "Offer(0)";
        public string Myearning
        {
            get { return _myearning; }
            set { _myearning = value; OnPropertyChanged("Myearning"); }
        }

        #endregion

        #region Comands
        private Command _BackCommand;
        public Command BackCommand
        {
            get
            {
                return _BackCommand ?? (_BackCommand = new Command(async () =>
                {
                    try
                    {
                        if (navigation != null)
                        {
                            if (IsTap)
                                return;
                            IsTap = true;
                            var nav = new NavigationPage(new AccountPage());
                            App.Current.MainPage = nav;
                        }
                    }
                    catch (Exception ex)
                    {
                        IsTap = false;
                    }
                }
             ));
            }
        }
        private Command _LogoutCommand;
        public Command LogoutCommand
        {
            get
            {
                return _LogoutCommand ?? (_LogoutCommand = new Command(async () =>
                {
                    try
                    {
                        if (navigation != null)
                        {
                            Global.LogoutConfirm();
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
             ));
            }
        }

        private Command _ProfileCommand;
        public Command ProfileCommand
        {
            get
            {
                return _ProfileCommand ?? (_ProfileCommand = new Command(async () =>
                {
                    try
                    {
                        if (navigation != null)
                        {
                            if (IsTap)
                                return;
                            IsTap = true;
                            await navigation.PushAsync(new EditProfilePage());
                        }
                    }
                    catch (Exception ex)
                    {
                        IsTap = false;
                    }
                }
             ));
            }
        }

        private Command _HelpCommand;
        public Command HelpCommand
        {
            get
            {
                return _HelpCommand ?? (_HelpCommand = new Command(async () =>
                {
                    try
                    {
                        if (navigation != null)
                        {
                            if (IsTap)
                                return;
                            IsTap = true;
                            await navigation.PushAsync(new HelpPage());
                        }
                    }
                    catch (Exception ex)
                    {
                        IsTap= false;
                    }
                }
             ));
            }
        }

        private Command _HowItWorkCommand;
        public Command HowItWorkCommand
        {
            get
            {
                return _HowItWorkCommand ?? (_HowItWorkCommand = new Command(async () =>
                {
                    try
                    {
                        if (navigation != null)
                        {
                            if (IsTap)
                                return;
                            IsTap = true;
                            await navigation.PushAsync(new BuySellWorkingPage());
                        }
                    }
                    catch (Exception ex)
                    {
                        IsTap = false;
                    }
                }
             ));
            }
        }

        private Command _SettingsCommand;
        public Command SettingsCommand
        {
            get
            {
                return _SettingsCommand ?? (_SettingsCommand = new Command(async () =>
                {
                    try
                    {
                        if (navigation != null)
                        {
                            if (IsTap)
                                return;
                            IsTap = true;
                            await navigation.PushAsync(new SettingPage());
                            //UserDialogs.Instance.Alert("", "Coming soon", "OK");
                            IsTap = false;
                            return;
                            //await navigation.PushAsync(new SettingPage());
                        }
                    }
                    catch (Exception ex)
                    {
                        IsTap = false;
                    }
                }
             ));
            }
        }

        private Command _SellingCommand;
        public Command SellingCommand
        {
            get
            {
                return _SellingCommand ?? (_SellingCommand = new Command(async () =>
                {
                    try
                    {
                        if (navigation != null)
                        {
                            if (IsTap)
                                return;
                            IsTap = true;
                            await navigation.PushAsync(new BuyingListView("SELLING"));
                            //await navigation.PushAsync(new SellerClosetView("My Store", Constant.LoginUserData.Id.ToString(),true));
                        }
                    }
                    catch (Exception ex)
                    {
                        IsTap = false;
                    }
                }
             ));
            }
        }

        private Command _BuyingCommand;
        public Command BuyingCommand
        {
            get
            {
                return _BuyingCommand ?? (_BuyingCommand = new Command(async () =>
                {
                    try
                    {
                        if (navigation != null)
                        {
                            if (IsTap)
                                return;
                            IsTap = true;
                            await navigation.PushAsync(new BuyingListView("BUYING"));
                            //UserDialogs.Instance.Alert("", "Coming soon", "OK");
                            IsTap = false;
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        IsTap = false;
                    }
                }
             ));
            }
        }

        private Command _OffersSectionCommand;
        public Command OffersSectionCommand
        {
            get
            {
                return _OffersSectionCommand ?? (_OffersSectionCommand = new Command<ViewOffersModel>(async (obj) => await ExicuteOffersSectionCommand(obj)));
            }
        }
       
        private Command _ViewMyStoreCommand;
        public Command ViewMyStoreCommand
        {
            get
            {
                return _ViewMyStoreCommand ?? (_ViewMyStoreCommand = new Command(async () =>
                {
                    try
                    {
                        if (navigation != null)
                        {
                            if (IsTap)
                                return;
                            IsTap = true;
                            await navigation.PushAsync(new SellerClosetView("My Store",Constant.LoginUserData.Id.ToString(),true));
                        }
                    }
                    catch (Exception ex)
                    {
                        IsTap = false;
                    }
                }
             ));
            }
        }
        
        private Command _FavouritesCommand;
        public Command FavouritesCommand
        {
            get
            {
                return _FavouritesCommand ?? (_FavouritesCommand = new Command(async () =>
                {
                    try
                    {
                        if (navigation != null)
                        {
                            if (IsTap)
                                return;
                            IsTap = true;
                            await navigation.PushAsync( new MyFavoritesPage());
                        }
                    }
                    catch (Exception ex)
                    {
                        IsTap = false;
                    }
                }
             ));
            }
        }
        public string OfferValue { get; private set; }

        private Command _OfferArrowCommand;
        public Command OfferArrowCommand
        {
            get
            {
                return _OfferArrowCommand ?? (_OfferArrowCommand = new Command<ViewOffersModel>(async (obj) => await ExicuteOffersArrowCommand(obj)));
            }
        }

        private Command _OfferBuyerArrowCmd;
        public Command OfferBuyerArrowCmd
        {
            get
            {
                return _OfferBuyerArrowCmd ?? (_OfferBuyerArrowCmd = new Command<ViewOffersModel>(async (obj) => await BuyerOffersArrowCommand(obj)));
            }
        }

        #endregion

        #region GetViewOffersList Methods
        public void GetViewOffersList()
        {
            try
            {
                if (Global.OfferProductList != null)
                {
                    var templist = new ObservableCollection<ViewOffersModel>();
                    foreach (var item in Global.OfferProductList)
                    {
                        var IsResult = templist.Where(x => x.Description == item.ProductName).ToList();
                        if (IsResult.Count == 0)
                        {
                            templist.Add(new ViewOffersModel
                            {
                                ProductID = item.Id,
                                Image = item.ProductImage,
                                Description = item.ProductName,
                                DollerValue = item.Price,
                                OfferValue = "$" + item.OfferPrice,
                                Brand = item.Brand,
                                Size = item.Size,
                                NextImage = "NextArrow",
                                Seller = "@" + Global.Username != null ? Global.Username : "NA",
                                Buyer = item.UserName
                            });
                        }
                    }
                    _ViewOffersList = templist;
                    ViewOffersList = _ViewOffersList;
                    if (ViewOffersList.Count > 0)
                    {
                        OfferCount = Convert.ToString(ViewOffersList.Count);
                    }
                    else
                    {
                        OfferCount = "0";
                    }

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        public async Task ExicuteOffersSectionCommand(ViewOffersModel obj)
        {
            try
            {
                UserDialogs.Instance.Alert("", "Coming soon", "OK");
                return;


                if (IsTap)
                    return;
                IsTap = true;

                var proOfferObj = Global.OfferProductList.Where(p => p.Id == obj.ProductID).FirstOrDefault();
                //await navigation.PushAsync(new MakeAnOfferDetailPage(proOfferObj, OfferValue));
                await navigation.PushAsync(new ItemDetailsPage(proOfferObj,false));
                await Task.Delay(100);
                IsTap = false;
            }
            catch (Exception ex)
            {
                IsTap = false;
                Debug.WriteLine(ex.Message);
            }
        }
        private async Task ExicuteOffersArrowCommand(ViewOffersModel obj)
        {
            try
            {
                UserDialogs.Instance.Alert("", "Coming soon", "OK");
                return;

                if (IsTap)
                    return;
                IsTap = true;

                var proOfferObj = Global.OfferProductList.Where(p => p.Id == obj.ProductID).FirstOrDefault();
                await navigation.PushModalAsync(new NavigationPage(new ViewOffersView(proOfferObj)));
            }
            catch (Exception ex)
            {
                IsTap = false;
                Debug.WriteLine(ex.Message);
            }
        }


        private async Task BuyerOffersArrowCommand(ViewOffersModel obj)
        {
            try
            {
                if (IsTap)
                    return;
                IsTap = true;

                var proOfferObj = Global.OfferProductList.Where(p => p.Id == obj.ProductID).FirstOrDefault();
                await navigation.PushModalAsync(new NavigationPage(new ViewOffersPage(proOfferObj)));
            }
            catch (Exception ex)
            {
                IsTap = false;
                Debug.WriteLine(ex.Message);
            }
        }


        #region Get Account earnings
        public async void GetMyEarning()
        {
            try
            {
                if (!Constant.IsConnected)
                {
                    UserDialogs.Instance.Toast("Internet not available");
                    IsTap = false;
                    return;
                }

                if (Constant.LoginUserData.Id == 0)
                {
                    Acr.UserDialogs.UserDialogs.Instance.Alert("Please login first");
                    IsTap = false;
                    return;
                }

                Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Getting earnings details ,please wait..");
                string methodUrl = $"/api/Account/GetPaymentDashboard?id={Constant.LoginUserData.Id}";
                if (!string.IsNullOrWhiteSpace(methodUrl))
                {
                    RestResponseModel responseModel = await WebService.GetData(methodUrl, true);
                    if (responseModel != null)
                    {
                        if (responseModel.status_code == 200)
                        {
                            MyEarningsResponseModel myEarningsResponseModel = new MyEarningsResponseModel();
                            myEarningsResponseModel = JsonConvert.DeserializeObject<MyEarningsResponseModel>(responseModel.response_body);
                            if (myEarningsResponseModel != null)
                            {
                                earningresponseModel = myEarningsResponseModel;
                                if (myEarningsResponseModel.Balance != null)
                                {
                                    decimal earning = 0;
                                    if (!string.IsNullOrEmpty(myEarningsResponseModel.Balance))
                                        earning = Convert.ToDecimal(myEarningsResponseModel.Balance);

                                    Myearning = "Earnings $" + String.Format("{0:0.00}", earning);
                                }
                            }
                            Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                            IsTap = false;
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
                            Acr.UserDialogs.UserDialogs.Instance.Alert(responseModel.ErrorMessage);
                            IsTap = false;
                            return;
                        }
                    }
                    else
                    {
                        Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                        Acr.UserDialogs.UserDialogs.Instance.Alert(responseModel.ErrorMessage);
                        IsTap = false;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                IsTap = false;
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                IsTap = false;
            }
            
        }
        #endregion

        #endregion
    }
}
