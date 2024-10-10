using Acr.UserDialogs;
using BuySell.Helper;
using BuySell.Model;
using BuySell.Model.RestResponse;
using BuySell.Views;
using BuySell.WebServices;
using Newtonsoft.Json;
using Stripe;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace BuySell.ViewModel
{
    public class CreditCardListViewModel : BaseViewModel
    {
        Action<CardListModel> callbackAction = null;
        #region Constructor
        public CreditCardListViewModel(INavigation _nav)
        {
            navigation = _nav;
        }
        public CreditCardListViewModel(INavigation _nav, Action<CardListModel> action)
        {
            navigation = _nav;
            callbackAction = action;
        }
        #endregion

        #region Properties
        private ObservableCollection<CardListModel> _CreditCardAddList = new ObservableCollection<CardListModel>();
        public ObservableCollection<CardListModel> CreditCardAddList
        {
            get
            {
                return _CreditCardAddList;
            }
            set { _CreditCardAddList = value; OnPropertyChanged("CreditCardAddList"); }
        }

        //private string _Name = Convert.ToString(Constant.LoginUserData.FirstName) + " " + Convert.ToString(Constant.LoginUserData.LastName);
        //public string Name
        //{
        //    get { return _Name; }
        //    set { _Name = value; OnPropertyChanged("Name"); }
        //}

        private CardListModel _SelectedCreditCardModel;
        public CardListModel SelectedCreditCardModel
        {
            get
            {
                return _SelectedCreditCardModel;
            }
            set
            {
                _SelectedCreditCardModel = value;
                OnPropertyChanged("SelectedCreditCardModel");
                if (_SelectedCreditCardModel != null)
                {
                    Constant.globalAddedCard = value;
                    navigation.PopAsync();
                    _SelectedCreditCardModel = null;
                }
            }
        }
        #endregion

        //#region Methods
        //public void GetCreditCardList()
        //{
        //    try
        //    {
        //        CreditCardAddList.Clear();
        //        CreditCardAddList = new ObservableCollection<CardModel>();

        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //}
        //#endregion

        #region Command
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
                        await navigation.PopAsync(true);
                    }
                    catch (Exception ex)
                    {
                        IsTap = false;
                    }
                }
             ));
            }
        }

        private Command _SetAsDefaultCommand;
        public Command SetAsDefaultCommand
        {
            get
            {
                return _SetAsDefaultCommand ?? (_SetAsDefaultCommand = new Command<CardListModel>(async (objCard) =>
                {
                    try
                    {
                        await UpdateToDefaultCard(objCard);
                        
                       // Acr.UserDialogs.UserDialogs.Instance.Alert("Default card set successfully");
                        //   CreditCardAddList = new ObservableCollection<CardModel>(Helper.Global.GlobalCardList.ToList());
                    }
                    catch (Exception ex)
                    {
                        IsTap = false;
                    }

                }));
            }

        }

        public async Task UpdateToDefaultCard(CardListModel cardListModel)
        {
            try
            {
                try
                {
                    if (!Constant.IsConnected)
                    {
                        UserDialogs.Instance.Toast("Internet not available");
                        IsTap = false;
                        return;
                    }

                    CCardModel RequestforAddcard = new CCardModel()
                    {
                        PaymentId = cardListModel.Id,
                        UserId = cardListModel.UserId,
                        Token = cardListModel.CardToken,
                        CCType = "card",
                        CCLast4 = cardListModel.Span,
                        CCFirst6 = cardListModel.Span,
                        AuthReference = null,
                        sCCExpMonthYear = cardListModel.Expiry,
                        IsDefault = true
                    };

                    Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Setting Default Card, please wait..");
                    await Task.Delay(50);
                    string methodUrl = string.Empty;
                    methodUrl = $"/api/Account/UpdateCustomerPayment";
                    if (!string.IsNullOrWhiteSpace(methodUrl))
                    {
                        
                        RestResponseModel responseModel = await WebService.PostData(RequestforAddcard, methodUrl, true);
                        if (responseModel != null)
                        {
                            if (responseModel.status_code == 200)
                            {
                                Global.AlertWithAction("Default Card Set Successfully!!", async () =>
                                {
                                    await GetallcardList();
                                });
                                //var alertConfig = new AlertConfig
                                //{
                                //    Message = "Default Card Set Successfully",
                                //    OkText = "OK",
                                //    OnAction = async () =>
                                //    {
                                //        await GetallcardList();
                                //    }
                                //};
                                //Acr.UserDialogs.UserDialogs.Instance.Alert(alertConfig);
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
                    UserDialogs.Instance.HideLoading();
                    Debug.WriteLine(ex.Message);
                }
            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                IsTap = false;
                Debug.WriteLine(ex.Message);
            }
        }
        private Command _DeleteCommand;
        public Command DeleteCommand
        {
            get
            {
                return _DeleteCommand ?? (_DeleteCommand = new Command<CardListModel>(async (objCard) =>
                {
                    try
                    {
                        if (IsTap)
                            return;

                        IsTap = true;
                        var confirm = await Acr.UserDialogs.UserDialogs.Instance.ConfirmAsync(Helper.Constant.deleteConfirmMsgStr, okText: Helper.Constant.OKStr, cancelText: Helper.Constant.CancelStr);
                        if (confirm)
                        {
                            DeleteCardMethod(objCard);
                        }
                        IsTap = false;
                    }
                    catch (Exception ex)
                    {
                        IsTap = false;
                    }
                }
             ));
            }
        }

        private Command _EditCommand;
        public Command EditCommand
        {
            get
            {
                return _EditCommand ?? (_EditCommand = new Command<CardListModel>(async (objCard) =>
                {
                    try
                    {
                        if (IsTap)
                            return;
                        IsTap = true;
                        await navigation.PushAsync(new AddCardPage(objCard));
                    }
                    catch (Exception ex)
                    {
                        IsTap = false;
                    }
                }
             ));
            }
        }

        private Command _AddNewCreditCardCommand;
        public Command AddNewCreditCardCommand
        {
            get
            {
                return _AddNewCreditCardCommand ?? (_AddNewCreditCardCommand = new Command(async () =>
                {
                    try
                    {
                        if (IsTap)
                            return;

                        var objAddCardPage = new AddCardPage((cardModel) =>
                        {
                            OnPropertyChanged("CreditCardAddList");
                        });

                        IsTap = true;
                        await navigation.PushAsync(objAddCardPage);
                    }
                    catch (Exception ex)
                    {
                        IsTap = false;
                    }
                }
             ));
            }
        }

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

                if (Constant.LoginUserData.Id == 0)
                {
                    Acr.UserDialogs.UserDialogs.Instance.Alert("Please enter name");
                    IsTap = false;
                    return;
                }
                try
                {
                    UserDialogs.Instance.ShowLoading("Loading... please wait");
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
                                var AddressList = JsonConvert.DeserializeObject<System.Collections.ObjectModel.ObservableCollection<CardListModel>>(responseModel.response_body);

                                if (AddressList != null)
                                {
                                    if (AddressList.Count > 0)
                                    {
                                        AddressList.ForEach(x => x.CardIcon = Global.GetCardICon(x.Bin + "111111" + x.Span));
                                        Global.GlobalCardList = CreditCardAddList = AddressList;
                                        Constant.globalAddedCard = AddressList.Where(c => c.IsPrimary == true).FirstOrDefault();
                                    }
                                    else
                                    {
                                        Global.GlobalCardList.Clear();
                                        CreditCardAddList.Clear();
                                        Constant.globalAddedCard = new CardListModel();
                                    }
                                }
                                else
                                {
                                    Global.GlobalCardList.Clear();
                                    CreditCardAddList.Clear();
                                    Constant.globalAddedCard = new CardListModel();
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
                    UserDialogs.Instance.HideLoading();
                    Debug.WriteLine(ex.Message);
                }
            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                IsTap = false;
                Debug.WriteLine(ex.Message);
            }
        }

        private async void DeleteCardMethod(CardListModel objCard)
        {
            try
            {
                if (!Constant.IsConnected)
                {
                    UserDialogs.Instance.Toast("Internet not available");
                    IsTap = false;
                    return;
                }

                Cardmodel RequestCardmodel = new Cardmodel()
                {
                    PaymentId = objCard.Id,
                    UserId = Constant.LoginUserData.Id,
                    Token = objCard.CardToken,
                    CCType = "card",
                    CCLast4 = objCard.Span,
                    CCFirst6 = objCard.Bin,
                    AuthReference = null,
                    sCCExpMonthYear = objCard.Expiry
                };

                Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Deleting card, please wait..");
                await Task.Delay(50);
                string methodUrl = $"/api/Account/DeleteCustomerPayment";
                if (!string.IsNullOrWhiteSpace(methodUrl))
                {
                    RestResponseModel responseModel = await WebService.PostData(RequestCardmodel, methodUrl, true);
                    if (responseModel != null)
                    {
                        if (responseModel.status_code == 200)
                        {
                            Global.AlertWithAction("Card deleted successfully!!", async () =>
                            {
                                await GetallcardList();
                            });

                            //var alertConfig = new AlertConfig
                            //{
                            //    Message = "Card deleted successfully",
                            //    OkText = "OK",
                            //    OnAction = async () =>
                            //    {
                            //        await GetallcardList();
                            //    }
                            //};
                            if(Constant.globalAddedCard == objCard)
                            {
                                Constant.globalAddedCard = new CardListModel();
                            }
                            //Acr.UserDialogs.UserDialogs.Instance.Alert(alertConfig);
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
            }
            catch (Exception ex)
            {
                IsTap = false;
                Acr.UserDialogs.UserDialogs.Instance.Alert(ex.Message);
                UserDialogs.Instance.HideLoading();
            }
        }

        #endregion
    }
}