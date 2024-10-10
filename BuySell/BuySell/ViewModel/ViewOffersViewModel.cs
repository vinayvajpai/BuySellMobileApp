using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using BuySell.Helper;
using BuySell.Model;
using BuySell.View;
using BuySell.Views;
using Xamarin.Forms;

namespace BuySell.ViewModel
{
    // buyer view model
    public class ViewOffersViewModel : BaseViewModel
    {
        #region Constructor
        public ViewOffersViewModel(INavigation _nav, DashBoardModel obj)
        {
            navigation = _nav;
            objdashboardModel = obj;
            //GetViewOffersList();
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

        private ObservableCollection<ViewOffersModel> _ViewOffersList = new ObservableCollection<ViewOffersModel>();
        public ObservableCollection<ViewOffersModel> ViewOffersList
        {
            get { return _ViewOffersList; }
            set { _ViewOffersList = value; OnPropertyChanged("ViewOffersList"); }
        }
        private ObservableCollection<OfferChatModel> _listChat;
        public ObservableCollection<OfferChatModel> listChat
        {
            get { return _listChat; }
            set { _listChat = value; OnPropertyChanged("listChat"); }
        }
        private bool _Isaccept = true;
        public bool IsAccept
        {
            get { return _Isaccept; }
            set { _Isaccept = value; OnPropertyChanged("IsAccept"); }
        }

        private bool _IsOrderSold = false;
        public bool IsOrderSold
        {
            get { return _IsOrderSold; }
            set { _IsOrderSold = value; OnPropertyChanged("IsOrderSold"); }
        }

        private bool _Iscounter = true;
        public bool IsCounter
        {
            get { return _Iscounter; }
            set { _Iscounter = value; OnPropertyChanged("IsCounter"); }
        }
        private bool _Isdecline = true;
        public bool IsDecline
        {
            get { return _Isdecline; }
            set { _Isdecline = value; OnPropertyChanged("IsDecline"); }
        }
        #endregion

        #region Command

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
        //public void GetViewOffersList()
        //{
        //    try
        //    {
        //        ViewOffersList.Clear();
        //        ViewOffersList = new ObservableCollection<ViewOffersModel>()
        //        {
        //            new ViewOffersModel()
        //            {
        //                 Image = "GreenDress",
        //                 Description = "Blue Cotton Trouser",
        //                 DollerValue = "150",
        //                 OfferValue = "12",
        //                 Size = "XXL",
        //                 Seller ="@talldh 22",
        //                 NextImage = "NextArrow"
        //            }
        //    };
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

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
                        PaymentMsg = "Your offer  " + "$" + objdashboardModel.OfferPrice
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
                    objdashboardModel.Price = objdashboardModel.OfferPrice;
                    await navigation.PushAsync(new OrderDetailPage(objdashboardModel));
                    // await navigation.PushAsync(new OrderConfirmPage());
                }
                else
                {
                    IsTap = false;
                    //await navigation.PopModalAsync();
                }
            }
            catch (Exception ex)
            {
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
                    IsTap = false;
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

