using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using BuySell.Helper;
using BuySell.Model;
using BuySell.View;
using BuySell.Views;
using Xamarin.Forms;

namespace BuySell.ViewModel
{
// seller view model
    public class ViewOfferViewModel : BaseViewModel
    {
        #region Constructor
        public ViewOfferViewModel(INavigation _nav, DashBoardModel obj)
        {
            navigation = _nav;
            objdashboardModel = obj;
            GetChatList();
        }
        #endregion

        #region Properties

        private DashBoardModel _objdashboardModel;
        public DashBoardModel objdashboardModel
        {
            get { return _objdashboardModel; }
            set { _objdashboardModel = value; OnPropertyChanged("objdashboardModel"); }
        }

        private ObservableCollection<OfferChatModel> _listChat;
        public ObservableCollection<OfferChatModel> listChat
        {
            get { return _listChat; }
            set { _listChat = value; OnPropertyChanged("listChat"); }
        }

        private bool _IsOrderSold = false;
        public bool IsOrderSold
        {
            get { return _IsOrderSold; }
            set { _IsOrderSold = value; OnPropertyChanged("IsOrderSold"); }
        }


        private string _PaymentMsg;
        public string PaymentMsg
        {
            get { return _PaymentMsg; }
            set { _PaymentMsg = value; OnPropertyChanged("PaymentMsg"); }
        }
        #endregion

        #region Commands

        private Command _OrderDetailsCmd;
        public Command OrderDetailsCmd
        {
            get
            {
                return _OrderDetailsCmd ?? (_OrderDetailsCmd = new Command(async () =>
                {
                    OrderDetailsCommand();
                }));
            }
        }

        private Command _CancelCommand;
        public Command CancelCommand
        {
            get
            {
                return _CancelCommand ?? (_CancelCommand = new Command(async () =>
                {
                    CancelOffer();
                }));
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
                }));
            }
        }

        private Command _AcceptOfferCmd;
        public Command AcceptOfferCmd
        {
            get
            {
                return _AcceptOfferCmd ?? (_AcceptOfferCmd = new Command(async () =>
                {
                    AcceptOfferCommand();
                }));
            }
        }

       

        private Command _CounterOfferCmd;
        public Command CounterOfferCmd
        {
            get
            {
                return _CounterOfferCmd ?? (_CounterOfferCmd = new Command(async () =>
                {
                    CounterofferCommand();
                }));
            }
        }

        
        private Command _DeclineOfferCmd;
        public Command DeclineOfferCmd
        {
            get
            {
                return _DeclineOfferCmd ?? (_DeclineOfferCmd = new Command(async () =>
                {
                    CancelOffer();
                }));
            }
        }


        #endregion

        #region Methods
        public void GetChatList()
        {
            try
            {
                listChat = new ObservableCollection<OfferChatModel>() {
                    new OfferChatModel()
                    {
                        SellerProfile  = Convert.ToString(objdashboardModel.UserProfile),
                        Name = "@"+objdashboardModel.UserFirstName,
                        Sender = true,
                        PaymentMsg = "Listing price " + objdashboardModel.Price
                    },
                    new OfferChatModel()
                    {
                        BuyerProfile  = (string)Constant.LoginUserData.ProfilePath,
                        Name = "@"+Global.Username,
                        Sender = false,
                        msgTime = DateTime.Now,
                        PaymentMsg = "Offered  " + "$" + objdashboardModel.OfferPrice
                    },
                };
            }
            catch (Exception ex)
            {

            }
        }
        private async void AcceptOfferCommand()
        {
            try
            {
                if (IsTap)
                    return;
                IsTap = true;

                var res = await Acr.UserDialogs.UserDialogs.Instance.ConfirmAsync("Are you sure you want to accept this offer?", "Confirmation", "Yes", "Cancel");
                if (res)
                {
                    IsOrderSold= true;
                    //await navigation.PushAsync(new OrderDetailPage(objdashboardModel));
                    IsTap = false;
                }
                else
                {
                    IsTap = false;
                    //await navigation.PopModalAsync();
                }
            }
            catch (Exception ex)
            {
                IsTap = false;
                Debug.WriteLine(ex);
            }
        }
        private async void CounterofferCommand()
        {
            try
            {
                await navigation.PushModalAsync(new NavigationPage(new MakeAnOfferPage(objdashboardModel)));
            }
            catch (Exception ex)
            {
                IsTap = false;
            }
        }
        public async void CancelOffer()
        {
            try
            {
                if (IsTap)
                    return;
                IsTap = true;

                var res = await Acr.UserDialogs.UserDialogs.Instance.ConfirmAsync("Are you sure you want to cancel this offer?", "Confirmation", "Yes", "Cancel");
                if (res)
                {
                    await Acr.UserDialogs.UserDialogs.Instance.AlertAsync("Your offer has been cancelled");
                    await navigation.PopModalAsync();
                }
                else
                {
                    IsTap = false;

                }
            }
            catch (Exception ex)
            {
                IsTap = false;
            }
        }

        private async void OrderDetailsCommand()
        {
            try
            {
                if (IsTap)
                    return;
                IsTap = true;
                await navigation.PushAsync(new OrderDetailsShipping(objdashboardModel, objdashboardModel.OfferPrice));
            }
            catch (Exception ex)
            {
                IsTap = false;
            }
           
        }

        #endregion
    }
}
