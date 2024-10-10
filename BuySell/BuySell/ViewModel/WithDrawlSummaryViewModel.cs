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
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BuySell.ViewModel
{
    public class WithDrawlSummaryViewModel : BaseViewModel
    {
        #region Constructor
        public WithDrawlSummaryViewModel(INavigation _nav, double withdrawAmount)
        {
            navigation = _nav;
            WithDrawAmount = "$" + withdrawAmount.ToString();
        }
        #endregion

        #region Properties

        public ObservableCollection<BankAccountModel> BankAccounts { get; private set; }

        private string _withDrawAmount = "$0";
        public string WithDrawAmount
        {
            get { return _withDrawAmount; }
            set { _withDrawAmount = value; OnPropertyChanged("WithDrawAmount"); }
        }

        private string _depositAmount = "$0.00";
        public string DepositAmount
        {
            get { return _depositAmount; }
            set { _depositAmount = value; OnPropertyChanged("DepositAmount"); }
        }

        private string _transferFee = "$0.00";
        public string TransferFee
        {
            get { return _transferFee; }
            set { _transferFee = value; OnPropertyChanged("TransferFee"); }
        }

        private string _CheckAccNo;
        public string CheckAccNo
        {
            get { return _CheckAccNo; }
            set { _CheckAccNo = value; OnPropertyChanged("CheckAccNo"); }
        }

        private string _Price = "$";
        public string Price
        {
            get
            {
                return _Price;
            }
            set
            {
                try
                {
                    if (!string.IsNullOrEmpty(value))
                    {
                        if (value.Length > 1)
                        {
                            var regexpattern = "^\\d+(\\.\\d{0,2})?$";
                            if (Regex.IsMatch(value.Replace("$", ""), regexpattern))
                            {
                                if (Convert.ToDouble(value.Replace("$", "")) <= Convert.ToDouble(WithDrawAmount.Replace("$", "")))
                                {
                                    _Price = value.Contains("$") ? value : "$" + value;
                                }
                            }
                            else
                            {
                                _Price = value.Contains("$") ? value.Substring(0, value.Length - 1) : "$" + value.Substring(0, value.Length - 1);
                            }

                        }
                        else
                        {
                            _Price = "$";
                        }

                        if (value.IndexOf("$") != 0)
                        {
                            _Price = "$";
                        }
                    }
                    else
                    {
                        _Price = "$";
                    }

                    OnPropertyChanged("Price");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

        }


        #endregion

        #region Commands
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
                    }
                }
             ));
            }
        }



        private Command _SubmitCommand;
        public Command SubmitCommand
        {
            get
            {
                return _SubmitCommand ?? (_SubmitCommand = new Command(async () =>
                {
                    try
                    {
                        if (!Constant.IsConnected)
                        {
                            UserDialogs.Instance.Toast("Internet not available");
                            IsTap = false;
                            return;
                        }


                        if (IsTap)
                            return;

                        IsTap = true;

                        if (string.IsNullOrWhiteSpace(Constant.LoginUserData.UserId))
                        {
                            Acr.UserDialogs.UserDialogs.Instance.Alert("Please login first");
                            IsTap = false;
                            return;
                        }

                        if(Constant.globalBankAccount == null)
                        {
                            Acr.UserDialogs.UserDialogs.Instance.Alert("Please select bank account");
                            IsTap = false;
                            return;
                        }
                        else if (Constant.globalBankAccount.BankAccountId == 0)
                        {
                            Acr.UserDialogs.UserDialogs.Instance.Alert("Please select bank account");
                            IsTap = false;
                            return;
                        }

                        if (string.IsNullOrWhiteSpace(Price))
                        {
                            Acr.UserDialogs.UserDialogs.Instance.Alert("Please enter the amount to be withdrawal");
                            IsTap = false;
                            return;

                        }
                        else
                        {
                            if (string.IsNullOrWhiteSpace(Price.Replace("$", "")))
                            {
                                Acr.UserDialogs.UserDialogs.Instance.Alert("Please enter the amount to be withdrawal");
                                IsTap = false;
                                return;
                            }
                        }

                        Acr.UserDialogs.UserDialogs.Instance.ShowLoading();
                        await Task.Delay(500);

                        WithdrawalRequestModel withdrawalRequestModel = new WithdrawalRequestModel()
                        {
                            BankAccountId = Convert.ToInt64(Constant.globalBankAccount.BankAccountId),
                            AmountRequested = Convert.ToDouble(Convert.ToDecimal(Price.Replace("$", ""))),
                            UserId = Convert.ToInt32(Constant.LoginUserData.Id)
                        };


                        string methodUrl = $"/api/Account/MakeWithdrawalRequest";
                        if (!string.IsNullOrWhiteSpace(methodUrl))
                        {
                            RestResponseModel responseModel = await WebService.PostData(withdrawalRequestModel, methodUrl, true);
                            if (responseModel != null)
                            {
                                if (responseModel.status_code == 200)
                                {
                                    Global.AlertWithAction("Your money is on the way!                                    Your money should be available in 2-3 business days.", () =>
                                    {
                                        App.Current.MainPage = new NavigationPage(new AccountPage());
                                    }, title: "CONGRATS!");

                                    //var alertConfig = new AlertConfig
                                    //    {
                                    //        Title = "CONGRATS!",
                                    //        Message = "Your money is on the way!                                    Your money should be available in 2-3 business days.",
                                    //        OkText = "OK",
                                    //        OnAction = () =>
                                    //        {
                                    //            App.Current.MainPage = new NavigationPage(new AccountPage());
                                    //        }
                                    //    };
                                    //    Acr.UserDialogs.UserDialogs.Instance.Alert(alertConfig);
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
                                Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                                IsTap = false;
                                return;

                            }
                        }
                        else
                        {
                            Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                            Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                            IsTap = false;
                            return;
                        }

                        //if (WithDrawAmount > 0)
                        //{
                        //    var alertConfig = new AlertConfig
                        //    {
                        //        Title = "CONGRATS!",
                        //        Message = "Your money is on the way!                                    Your money should be available in 2-3 business days.",
                        //        OkText = "OK",
                        //        OnAction = () =>
                        //        {
                        //            App.Current.MainPage = new NavigationPage(new MyEarningView());
                        //        }
                        //    };
                        //    Acr.UserDialogs.UserDialogs.Instance.Alert(alertConfig);
                        //}
                        //else
                        //{
                        //    var alertConfig = new AlertConfig
                        //    {
                        //        Message = "In order to make your 1st withdrawl we will need a bit more info!",
                        //        OkText = "OK",
                        //        OnAction = () =>
                        //        {
                        //            App.Current.MainPage = new NavigationPage(new TaxCenterView());
                        //        }
                        //    };
                        //    Acr.UserDialogs.UserDialogs.Instance.Alert(alertConfig);
                        //}
                    }
                    catch (Exception ex)
                    {
                        Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                        Acr.UserDialogs.UserDialogs.Instance.Alert(ex.Message);
                        IsTap = false;
                    }
                }
             ));
            }
        }

        private Command _WithdrawCommand;
        public Command WithdrawCommand
        {
            get
            {
                return _WithdrawCommand ?? (_WithdrawCommand = new Command(async () =>
                {
                    try
                    {
                        if (IsTap)
                            return;

                        IsTap = true;
                        await navigation.PushAsync(new WithdrawView());
                    }
                    catch (Exception ex)
                    {
                        IsTap = false;
                    }
                }
             ));
            }
        }

        public async void GetAllBankAccounts()
        {
            try
            {
                if (!Constant.IsConnected)
                {
                    UserDialogs.Instance.Toast("Internet not available");
                    IsTap = false;
                    return;
                }

                string methodUrl = $"/api/Account/ListBankAccount?id={Constant.LoginUserData.Id}";
                UserDialogs.Instance.ShowLoading("Getting bank accounts.. please wait");
                if (!string.IsNullOrWhiteSpace(methodUrl))
                {
                    RestResponseModel responseModel = await WebService.GetData(methodUrl, true);
                    if (responseModel != null)
                    {
                        if (responseModel.status_code == 200)
                        {
                            BankAccounts = Global.BankAccountsList = JsonConvert.DeserializeObject<ObservableCollection<BankAccountModel>>(responseModel.response_body);
                            if (Global.BankAccountsList != null)
                            {
                                if (Global.BankAccountsList.Count > 0)
                                {
                                    Constant.globalBankAccount = Global.BankAccountsList.Where(account => account.IsDefault == true).FirstOrDefault() == null ? Global.BankAccountsList.FirstOrDefault() : Global.BankAccountsList.Where(account => account.IsDefault == true).FirstOrDefault();

                                    if (Constant.globalBankAccount.AccountNumber != null)
                                    {
                                        CheckAccNo = Constant.globalBankAccount.AccountNumber;
                                    }
                                    else
                                    {
                                        CheckAccNo = Global.BankAccountsList.FirstOrDefault().AccountNumber;
                                    }
                                }
                                else
                                {
                                    Constant.globalBankAccount = new BankAccountModel();
                                    CheckAccNo = string.Empty;
                                }

                                //   Constant.globalBankAccount.BankAccountId = Convert.ToString(1);
                            }
                            else
                            {
                                Constant.globalBankAccount = new BankAccountModel();
                                CheckAccNo = string.Empty;
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
                Acr.UserDialogs.UserDialogs.Instance.Alert(ex.Message);
                UserDialogs.Instance.HideLoading();
            }
        }

        #endregion
    }
}