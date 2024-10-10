using Acr.UserDialogs;
using BuySell.Helper;
using BuySell.Model;
using BuySell.Model.RestResponse;
using BuySell.WebServices;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace BuySell.ViewModel
{
    public class EditBankAccountViewModel : BaseViewModel
    {
        #region Constructor
        public EditBankAccountViewModel(INavigation _nav)
        {
            navigation = _nav;
        }
        public EditBankAccountViewModel(INavigation _nav, BankAccountModel bank)
        {
            navigation = _nav;
            if (bank != null)
            {
                BankAccountDetail = bank;
            }
        }


        #endregion

        #region properties

        private bool _IsDefaultAcc = false;
        public bool IsDefaultAcc
        {
            get { return _IsDefaultAcc; }
            set
            {
                _IsDefaultAcc = value;
                OnPropertyChanged(nameof(IsDefaultAcc));
            }
        }

        private bool _IsEdit = false;
        public bool IsEdit
        {
            get { return _IsEdit; }
            set
            {
                _IsEdit = value;
                OnPropertyChanged(nameof(IsEdit));
            }
        }

        private string _ConfmCheckingAccNo;
        public string ConfmCheckingAccNo
        {
            get { return _ConfmCheckingAccNo; }
            set
            {
                _ConfmCheckingAccNo = value;
                OnPropertyChanged(nameof(ConfmCheckingAccNo));
            }
        }


        private BankAccountModel _BankAccountDetail = new BankAccountModel();
        public BankAccountModel BankAccountDetail
        {
            get
            {
                return _BankAccountDetail;
            }
            set
            {
                _BankAccountDetail = value;
                OnPropertyChanged(nameof(BankAccountDetail));
            }
        }

        #endregion

        #region Commands
        private ICommand _BackCommand;
        public ICommand BackCommand
        {
            get
            {
                if (_BackCommand == null)
                {
                    _BackCommand = new Command(() =>
                    {
                        try
                        {
                            if (IsTap)
                                return;
                            IsTap = true;
                            navigation.PopAsync();

                        }
                        catch (Exception ex)
                        {
                            IsTap = false;
                            Debug.WriteLine(ex.Message);
                        }
                    });
                }

                return _BackCommand;
            }
        }


        private ICommand _SaveAccountDetailCmd;
        public ICommand SaveAccountDetailCmd
        {
            get
            {
                if (_SaveAccountDetailCmd == null)
                {
                    _SaveAccountDetailCmd = new Command(async () =>
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

                            if (BankAccountDetail != null)
                            {
                                if (string.IsNullOrWhiteSpace(BankAccountDetail.AccountHolderName))
                                {
                                    Acr.UserDialogs.UserDialogs.Instance.Alert("Please enter account holder name");
                                    IsTap = false;
                                    return;
                                }
                                if (!IsEdit)
                                {

                                    if (string.IsNullOrWhiteSpace(BankAccountDetail.RoutingNumber))
                                    {
                                        Acr.UserDialogs.UserDialogs.Instance.Alert("Please enter bank routing number");
                                        IsTap = false;
                                        return;
                                    }

                                    if (string.IsNullOrWhiteSpace(BankAccountDetail.AccountNumber))
                                    {
                                        Acr.UserDialogs.UserDialogs.Instance.Alert("Please enter checking account number");
                                        IsTap = false;
                                        return;
                                    }

                                    if (string.IsNullOrWhiteSpace(ConfmCheckingAccNo))
                                    {
                                        Acr.UserDialogs.UserDialogs.Instance.Alert("Please enter confirm checking account number");
                                        IsTap = false;
                                        return;
                                    }
                                }
                                Acr.UserDialogs.UserDialogs.Instance.ShowLoading(!IsEdit ? "Adding account, Please wait..." : "Updating account, Please wait...");
                                await Task.Delay(50);

                                string methodUrl = string.Empty;

                                if (IsEdit)
                                {
                                    methodUrl = $"/api/Account/UpdateBankAccount?id={Constant.LoginUserData.Id}";
                                }
                                else
                                {
                                    methodUrl = $"/api/Account/AddBankAccount?id={Constant.LoginUserData.Id}";
                                }

                                if (!string.IsNullOrWhiteSpace(methodUrl))
                                {
                                    BankAccountDetail.IsDefault = BankAccountDetail.IsDefault;
                                    RestResponseModel responseModel = await WebService.PostData(BankAccountDetail, methodUrl, true);
                                    if (responseModel != null)
                                    {
                                        if (responseModel.status_code == 200)
                                        {
                                            if (IsEdit)
                                            {
                                                Global.AlertWithAction("Account updated successfully!!", async () =>
                                                {
                                                    await navigation.PopAsync();
                                                });

                                                //var alertConfig = new AlertConfig
                                                //{
                                                //    Message = "Account updated successfully",
                                                //    OkText = "OK",
                                                //    OnAction = async () =>
                                                //    {
                                                //        await navigation.PopAsync();
                                                //    }
                                                //};
                                                //Acr.UserDialogs.UserDialogs.Instance.Alert(alertConfig);
                                            }
                                            else
                                            {
                                                Global.AlertWithAction("Account added successfully!!", async () =>
                                                {
                                                    await navigation.PopAsync();
                                                });
                                                //var alertConfig = new AlertConfig
                                                //{
                                                //    Message = "Account added successfully",
                                                //    OkText = "OK",
                                                //    OnAction = async () =>
                                                //    {
                                                //        await navigation.PopAsync();
                                                //    }
                                                //};
                                                //Acr.UserDialogs.UserDialogs.Instance.Alert(alertConfig);

                                            }
                                            // await navigation.PopAsync();
                                            Constant.globalBankAccount = BankAccountDetail;
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
                        }
                        catch (Exception ex)
                        {
                            IsTap = false;
                            Debug.WriteLine(ex.Message);
                        }
                    });
                }

                return _SaveAccountDetailCmd;
            }
        }
        #endregion
    }
}

