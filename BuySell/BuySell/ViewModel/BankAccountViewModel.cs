using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
using BuySell.Helper;
using BuySell.Model;
using BuySell.Model.RestResponse;
using BuySell.Views;
using BuySell.WebServices;
using Newtonsoft.Json;
using Stripe;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace BuySell.ViewModel
{
    public class BankAccountViewModel : BaseViewModel
    {
        #region Constructor
        public BankAccountViewModel(INavigation _nav)
        {
            navigation = _nav;
            if (Global.BankAccountsList != null)
            {
                BankAccounts = Global.BankAccountsList;
            }
        }


        private ObservableCollection<BankAccountModel> _BankAccounts = new ObservableCollection<BankAccountModel>();
        public ObservableCollection<BankAccountModel> BankAccounts
        {
            get
            {
               return _BankAccounts;
            }
            set
            {
                _BankAccounts = value;
                OnPropertyChanged(nameof(BankAccounts));
            }
        }


        private BankAccountModel _SelectedBankModel;
        public BankAccountModel SelectedBankModel
        {
            get
            {
                return _SelectedBankModel;
            }
            set
            {
                _SelectedBankModel = value;
                OnPropertyChanged(nameof(SelectedBankModel));
             
                if(SelectedBankModel != null) 
                {
                    Constant.globalBankAccount = SelectedBankModel;
                    navigation.PopAsync();
                    _SelectedBankModel = null;
                }
            }
        }

        

        private Command _AddAccountCommand;
        public Command AddAccountCommand
        {
            get
            {
                return _AddAccountCommand ?? (_AddAccountCommand = new Command(async () =>
                {
                    try
                    {
                        if (IsTap)
                            return;

                        IsTap = true;
                        await navigation.PushAsync(new  EditBankAccountPage());
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
                return _SetAsDefaultCommand ?? (_SetAsDefaultCommand = new Command<BankAccountModel>(async (objAcc) =>
                {
                    try
                    {
                        if (!Constant.IsConnected)
                        {
                            UserDialogs.Instance.Toast("Internet not available");
                            IsTap = false;
                            return;
                        }
                        Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Account setting default, Please wait...");
                        await Task.Delay(50);
                        objAcc.IsDefault = true;
                        string methodUrl = $"/api/Account/UpdateBankAccount?id={Constant.LoginUserData.Id}";
                       
                        if (!string.IsNullOrWhiteSpace(methodUrl))
                        {
                            RestResponseModel responseModel = await WebService.PostData(objAcc, methodUrl, true);
                            if (responseModel != null)
                            {
                                if (responseModel.status_code == 200)
                                {
                                    Global.AlertWithAction("Default account set successfully!!", async () =>
                                    {
                                        IsTap = false;
                                        GetAllBankAccounts();
                                    });
                                    //var alertConfig = new AlertConfig
                                    //{
                                    //    Message = "Default account set successfully",
                                    //    OkText = "OK",
                                    //    OnAction = async () =>
                                    //    {
                                    //        IsTap = false;
                                    //        GetAllBankAccounts();
                                    //    }
                                    //};
                                    //Acr.UserDialogs.UserDialogs.Instance.Alert(alertConfig);
                                    UserDialogs.Instance.HideLoading();
                                    GetAllBankAccounts();
                                    //Constant.globalBankAccount = objAcc;
                                    //Global.BankAccountsList.Where(Accounts=>Accounts.AccountNumber != objAcc.AccountNumber).ForEach(account=>account.IsDefault = false);
                                    //Global.BankAccountsList.Where(Accounts=>Accounts.AccountNumber == objAcc.AccountNumber).ForEach(account=>account.IsDefault = true);
                                    // Acr.UserDialogs.UserDialogs.Instance.Alert("Default card set successfully");

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
                    }

                }));
            }

        }


        private Command _DeleteCommand;
        public Command DeleteCommand
        {
            get
            {
                return _DeleteCommand ?? (_DeleteCommand = new Command<BankAccountModel>(async (objCard) =>
                {
                    try
                    {
                        if (IsTap)
                            return;

                        IsTap = true;
                        var confirm = await Acr.UserDialogs.UserDialogs.Instance.ConfirmAsync(Helper.Constant.deleteConfirmMsgStr, okText: Helper.Constant.OKStr, cancelText: Helper.Constant.CancelStr);
                        if (confirm)
                        {
                            //    BankAccounts.Remove(objCard);
                            //    Global.BankAccountsList.Remove(objCard);
                            //    if (Global.GlobalCardList.Count > 0)
                            //    {
                            //        if (Constant.globalBankAccount != null)
                            //        {
                            //            if (Constant.globalBankAccount == objCard)
                            //            {
                            //                Constant.globalBankAccount = Global.BankAccountsList.LastOrDefault();
                            //            }
                            //        }

                            //    }
                            //    else
                            //        Constant.globalBankAccount = new BankAccountModel();

                            try
                            {
                                if (!Constant.IsConnected)
                                {
                                    UserDialogs.Instance.Toast("Internet not available");
                                    IsTap = false;
                                    return;
                                }


                                Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Deleting Account, Please wait...");
                                await Task.Delay(50);
                                string methodUrl = $"/api/Account/DeleteBankAccount?id={Constant.LoginUserData.Id}";

                                if (!string.IsNullOrWhiteSpace(methodUrl))
                                {
                                    RestResponseModel responseModel = await WebService.PostData(objCard, methodUrl, true);
                                    if (responseModel != null)
                                    {
                                        if (responseModel.status_code == 200)
                                        {
                                            Global.AlertWithAction("Account removed successfully!!", () =>
                                            {
                                                IsTap = false;
                                                GetAllBankAccounts();
                                            });
                                            //var alertConfig = new AlertConfig
                                            //{
                                            //    Message = "Account removed successfully",
                                            //    OkText = "OK",
                                            //    OnAction = async () =>
                                            //    {
                                            //        IsTap = false;
                                            //        GetAllBankAccounts();
                                            //    }
                                            //};
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
                        else
                        {
                            IsTap = false;
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
             ));
            }
        }

        private Command _EditCommand;
        public Command EditCommand
        {
            get
            {
                return _EditCommand ?? (_EditCommand = new Command<BankAccountModel>(async (objBankAcc) =>
                {
                    try
                    {
                        if (IsTap)
                            return;
                        IsTap = true;
                        var objAccPage = new EditBankAccountPage(objBankAcc, true);
                        await navigation.PushAsync(objAccPage);
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
                            if(Global.BankAccountsList != null)
                            {
                                if(Global.BankAccountsList.Count > 0)
                                     Constant.globalBankAccount = Global.BankAccountsList.Where(account=>account.IsDefault == true).FirstOrDefault();
                                else
                                    Constant.globalBankAccount = new BankAccountModel();

                                //   Constant.globalBankAccount.BankAccountId = Convert.ToString(1);
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

            }
        }

        #endregion
    }
}

