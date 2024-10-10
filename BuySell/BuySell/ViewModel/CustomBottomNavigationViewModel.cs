using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
using BuySell.Helper;
using BuySell.View;
using BuySell.Views;
using Xamarin.Forms;

namespace BuySell.ViewModel
{
    public class CustomBottomNavigationViewModel : BaseViewModel
    {
        private int PreviouslySelectedIndex = -1;
        private int PreviouslyIndex = -1;
        private readonly Services.IMessageService _messageService;
        #region Constructor
        public CustomBottomNavigationViewModel()
        {
            this._messageService = DependencyService.Get<Services.IMessageService>();
            HomeColor = WallColor = GiftColor = LocationColor = MenuColor = Color.White;
        }
        #endregion

        #region Properties
        private Color _HomeColor;
        public Color HomeColor
        {
            get { return _HomeColor; }
            set { _HomeColor = value; OnPropertyChanged("HomeColor"); }
        }

        private bool _BuyInActive = true;
        public bool BuyInActive
        {
            get { return _BuyInActive; }
            set { _BuyInActive = value; OnPropertyChanged("BuyInActive"); }
        }

        private bool _BuyActive = false;
        public bool BuyActive
        {
            get { return _BuyActive; }
            set { _BuyActive = value; OnPropertyChanged("BuyActive"); }
        }

        private Color _WallColor;
        public Color WallColor
        {
            get { return _WallColor; }
            set { _WallColor = value; OnPropertyChanged("WallColor"); }
        }

        private bool _AlertsInActive = true;
        public bool AlertsInActive
        {
            get { return _AlertsInActive; }
            set { _AlertsInActive = value; OnPropertyChanged("AlertsInActive"); }
        }

        private bool _AlertsActive = false;
        public bool AlertsActive
        {
            get { return _AlertsActive; }
            set { _AlertsActive = value; OnPropertyChanged("AlertsActive"); }
        }

        private Color _GiftColor;
        public Color GiftColor
        {
            get { return _GiftColor; }
            set { _GiftColor = value; OnPropertyChanged("GiftColor"); }
        }

        private bool _SellInActive = true;
        public bool SellInActive
        {
            get { return _SellInActive; }
            set { _SellInActive = value; OnPropertyChanged("SellInActive"); }
        }

        private bool _SellActive = false;
        public bool SellActive
        {
            get { return _SellActive; }
            set { _SellActive = value; OnPropertyChanged("SellActive"); }
        }

        private Color _LocationColor;
        public Color LocationColor
        {
            get { return _LocationColor; }
            set { _LocationColor = value; OnPropertyChanged("LocationColor"); }
        }

        private bool _BagsInActive = true;
        public bool BagsInActive
        {
            get { return _BagsInActive; }
            set { _BagsInActive = value; OnPropertyChanged("BagsInActive"); }
        }

        private bool _BagsActive = false;
        public bool BagsActive
        {
            get { return _BagsActive; }
            set { _BagsActive = value; OnPropertyChanged("BagsActive"); }
        }

        private Color _MenuColor;
        public Color MenuColor
        {
            get { return _MenuColor; }
            set { _MenuColor = value; OnPropertyChanged("MenuColor"); }
        }

        private bool _AccountInActive = true;
        public bool AccountInActive
        {
            get { return _AccountInActive; }
            set { _AccountInActive = value; OnPropertyChanged("AccountInActive"); }
        }

        private bool _AccountActive = false;
        public bool AccountActive
        {
            get { return _AccountActive; }
            set { _AccountActive = value; OnPropertyChanged("AccountActive"); }
        }
        #endregion

        #region Methods

            #region Set tabs
        private void SetTabs(int tabIndex)
        {
            try
            {
                PreviouslyIndex = PreviouslySelectedIndex;
                //if (PreviouslySelectedIndex == tabIndex)
                //{
                //    return;
                //}
                if (PreviouslySelectedIndex == tabIndex)
                {
                    if (tabIndex == 2 || tabIndex == 4)
                    {
                        return;
                    }
                }

                Device.BeginInvokeOnMainThread(() =>
                {
                    if (tabIndex == 3 || tabIndex == 5)
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
                    }

                    ResetData();
                    MessagingCenter.Unsubscribe<object, string>("UpdateCard", "UpdateCard");
                    PreviouslySelectedIndex = tabIndex;
                    HomeColor = WallColor = GiftColor = LocationColor = MenuColor = Color.White;
                    switch (tabIndex)
                    {
                        case 1:
                            {
                                Global.ResetMessagingCenter();
                                BuyInActive = true;
                                BuyActive = true;

                                AlertsInActive = true;
                                AlertsActive = false;

                                SellInActive = true;
                                SellActive = false;

                                BagsInActive = true;
                                BagsActive = false;

                                AccountInActive = true;
                                AccountActive = false;

                                HomeColor = Color.White;
                                //var nav = new NavigationPage(new DashBoardView());
                                //App.Current.MainPage = nav;
                                if (App.Current.MainPage.Navigation.NavigationStack.Count > 1)
                                {
                                    var _navigation = Application.Current.MainPage.Navigation;
                                    var _lastPage = _navigation.NavigationStack[_navigation.NavigationStack.Count - 2];
                                    _navigation.RemovePage(_lastPage);
                                    var nav = new NavigationPage(new DashBoardView(true));
                                    App.Current.MainPage = nav;
                                }
                                else
                                {
                                    var nav = new NavigationPage(new DashBoardView(true));
                                    App.Current.MainPage = nav;
                                }
                                break;
                            }
                        case 2:
                            {
                                //BuyInActive = true;
                                //BuyActive = false;

                                //AlertsInActive = true;
                                //AlertsActive = true;

                                //SellInActive = true;
                                //SellActive = false;

                                //BagsInActive = true;
                                //BagsActive = false;

                                //AccountInActive = true;
                                //AccountActive = false;

                                //WallColor = Color.White;
                                //Application.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new AlertsPage()));
                                // UserDialogs.Instance.Alert("", "Coming soon", "OK");
                                //Device.StartTimer(TimeSpan.FromMilliseconds(2000), () =>
                                //{
                                //    PreviouslySelectedIndex = PreviouslyIndex;
                                //    return false;
                                //});

                                UserDialogs.Instance.Alert("", "Coming soon", "OK");
                                return;
                                
                                Application.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new AlertsPage()));
                                Device.StartTimer(TimeSpan.FromMilliseconds(2000), () =>
                                {
                                    PreviouslySelectedIndex = PreviouslyIndex;
                                    return false;
                                });
                                break;
                            }
                        case 3:
                            {
                                Global.ResetMessagingCenter();
                                BuyInActive = true;
                                BuyActive = false;

                                AlertsInActive = true;
                                AlertsActive = false;

                                SellActive = true;
                                SellInActive = true;


                                BagsInActive = true;
                                BagsActive = false;

                                AccountInActive = true;
                                AccountActive = false;

                                GiftColor = Color.White;
                                if (App.Current.MainPage.Navigation.NavigationStack.Count > 1)
                                {
                                    App.Current.MainPage = new NavigationPage(new AddItemDetailsPage());
                                }
                                else
                                {
                                    App.Current.MainPage = new NavigationPage(new AddItemDetailsPage());
                                }
                                break;
                            }
                        case 4:
                            {

                                //BuyInActive = true;
                                //BuyActive = false;

                                //AlertsInActive = true;
                                //AlertsActive = false;

                                //SellInActive = true;
                                //SellActive = false;

                                //BagsInActive = true;
                                //BagsActive = true;

                                //AccountInActive = true;
                                //AccountActive = false;
                                //LocationColor = Color.White;

                                Application.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new ShoppingCardPage()));
                                //UserDialogs.Instance.Alert("", "Coming soon", "OK");
                                Device.StartTimer(TimeSpan.FromMilliseconds(2000), () => {
                                    PreviouslySelectedIndex = PreviouslyIndex;
                                    return false;
                                });
                                //((INavigation)App.Current.MainPage).PushModalAsync(new NavigationPage(new ShoppingCardPage()));

                                break;
                            }
                        case 5:
                            {
                                Global.ResetMessagingCenter();
                                BuyInActive = true;
                                BuyActive = false;

                                AlertsInActive = true;
                                AlertsActive = false;

                                SellInActive = true;
                                SellActive = false;

                                BagsInActive = true;
                                BagsActive = false;

                                AccountInActive = true;
                                AccountActive = true;

                                MenuColor = Color.White;
                                //var nav = new NavigationPage(new AccountPage());
                                //App.Current.MainPage = nav;
                                if (App.Current.MainPage.Navigation.NavigationStack.Count > 1)
                                {
                                    var _navigation = Application.Current.MainPage.Navigation;
                                    var _lastPage = _navigation.NavigationStack[_navigation.NavigationStack.Count - 2];
                                    _navigation.RemovePage(_lastPage);
                                    var nav = new NavigationPage(new AccountPage());
                                    App.Current.MainPage = nav;
                                }
                                else
                                {
                                    var nav = new NavigationPage(new AccountPage());
                                    App.Current.MainPage = nav;
                                }
                                break;
                            }
                    }
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        #endregion

            #region Set tab highlight
        public void SetTabsHighlight(int tabIndex)
        {
            try
            {
                if (PreviouslySelectedIndex == tabIndex)
                {
                    return;
                }

                Device.BeginInvokeOnMainThread(() =>
                {

                    PreviouslySelectedIndex = tabIndex;
                    HomeColor = WallColor = GiftColor = LocationColor = MenuColor = Color.White;
                    switch (tabIndex)
                    {
                        case 1:
                            {
                                BuyInActive = true;
                                BuyActive = true;

                                AlertsInActive = true;
                                AlertsActive = false;

                                SellInActive = true;
                                SellActive = false;

                                BagsInActive = true;
                                BagsActive = false;

                                AccountInActive = true;
                                AccountActive = false;

                                HomeColor = Color.White;
                                break;
                            }
                        case 2:
                            {
                                BuyInActive = true;
                                BuyActive = false;

                                AlertsInActive = true;
                                AlertsActive = true;

                                SellInActive = true;
                                SellActive = false;

                                BagsInActive = true;
                                BagsActive = false;

                                AccountInActive = true;
                                AccountActive = false;

                                WallColor = Color.White;
                                break;
                            }
                        case 3:
                            {

                                BuyInActive = true;
                                BuyActive = false;

                                AlertsInActive = true;
                                AlertsActive = false;

                                SellInActive = true;
                                SellActive = true;

                                BagsInActive = true;
                                BagsActive = false;

                                AccountInActive = true;
                                AccountActive = false;

                                GiftColor = Color.White;
                                break;
                            }
                        case 4:
                            {

                                BuyInActive = true;
                                BuyActive = false;

                                AlertsInActive = true;
                                AlertsActive = false;

                                SellInActive = true;
                                SellActive = false;

                                BagsInActive = true;
                                BagsActive = true;

                                AccountInActive = true;
                                AccountActive = false;

                                LocationColor = Color.White;
                                break;
                            }
                        case 5:
                            {
                                BuyInActive = true;
                                BuyActive = false;

                                AlertsInActive = true;
                                AlertsActive = false;

                                SellInActive = true;
                                SellActive = false;

                                BagsInActive = true;
                                BagsActive = false;

                                AccountInActive = true;
                                AccountActive = true;

                                MenuColor = Color.White;
                                break;
                            }
                    }
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        #endregion

            #region ResetData
        void ResetData()
        {
            //App.IsDataLoad = false;
            //App.fromWash = false;
            //App.fromOrderReview = false;
            //App.IsBottomTab = true;

            //App.UpdatePhoneNumber = false;
            //App.FromOrderHistory = false;
            //App.NavigateFromOrderHistory = false;
            //App.IsPopUpOpen = false;

            //App.fromWashCard = false;
            //App.IsPrepaidSelected = false;
            //App.IsMembership = false;
            //App.MonthlyActive = false;
            //App.IsShowPayNow = false;
        }
        #endregion

            private void ExecuteTabSelected(string index)
            {
                try
                {
                    this.SetTabs(Convert.ToInt32(index));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        #endregion

        #region Commands
        private Command _TabSelectedCommand;
        public Command TabSelectedCommand
        {
            get { return _TabSelectedCommand ?? (_TabSelectedCommand = new Command<string>(async (x) => ExecuteTabSelected(x))); }
        }
        #endregion
    }

}
