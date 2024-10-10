using Acr.UserDialogs;
using BuySell.Helper;
using BuySell.Model.LoginResponse;
using BuySell.Model.RestResponse;
using BuySell.View;
using BuySell.WebServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BuySell.ViewModel
{
    public class ResetPasswordViewModel : BaseViewModel
    {
        private readonly Services.IMessageService _messageService;
        #region Constructor
        public ResetPasswordViewModel(INavigation nav)
        {
            this._messageService = DependencyService.Get<Services.IMessageService>();
            navigation = nav;
        }
        #endregion

        #region Properties
        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged("Email"); }
        }
        #endregion

        #region Command
        private Command _ContinueCommand;
        public Command ContinueCommand
        {
            get
            {
                return _ContinueCommand ?? (_ContinueCommand = new Command(async () =>
                  await GetResetPasswordMethod()));
            }
        }

        private Command _BackCommand;
        public Command BackCommand
        {
            get
            {
                return _BackCommand ?? (_BackCommand = new Command(async () =>
                
                await navigation.PopAsync()));
            }
        }
        #endregion

        #region Methods
        private async Task GetResetPasswordMethod()
        {
            try
            {
                if (!Constant.IsConnected)
                {
                    IsTap = false;
                    return;
                }
                if (IsTap)
                    return;
                IsTap = true;

                if (string.IsNullOrEmpty(Email) || string.IsNullOrWhiteSpace(Email))
                {
                    IsTap = false;
                    await _messageService.ShowAsync("Please enter email for reset your password");
                    return;
                }
              
                Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Loading");
                await Task.Delay(500);
                string methodUrl = "/api/Account/ResetPassword?Email="+ Email;
                RestResponseModel responseModel = await WebService.GetData(methodUrl, true);
                if (responseModel != null)
                {
                    if (responseModel.status_code == 0)
                    {
                        IsTap = false;
                        Acr.UserDialogs.UserDialogs.Instance.Alert("Unable to process your request. Server Timeout");
                        UserDialogs.Instance.HideLoading();
                        return;
                    }
                    ResetPasswordResponseModel ResetPassResponse = JsonConvert.DeserializeObject<ResetPasswordResponseModel>(responseModel.response_body);
                    if (ResetPassResponse.StatusCode == 200)
                    {
                        if (ResetPassResponse != null)
                        {
                            Global.AlertWithAction("Password has been reset successfully please check your mailbox.", () =>
                            {
                                App.Current.MainPage = new NavigationPage(new LoginPage());
                            });
                            //var alertConfig = new AlertConfig
                            //{
                            //    Message = "Password has been reset successfully please check your mailbox.",
                            //    OkText = "OK",
                            //    OnAction = () =>
                            //    {
                            //        App.Current.MainPage = new NavigationPage(new LoginPage());
                            //    }
                            //};
                            //Acr.UserDialogs.UserDialogs.Instance.Alert(alertConfig);

                        }
                        else
                        {
                            IsTap = false;
                            Acr.UserDialogs.UserDialogs.Instance.Alert(ResetPassResponse.Message);
                            UserDialogs.Instance.HideLoading();
                        }
                    }
                    else if (ResetPassResponse.StatusCode == 400)
                    {
                        if (ResetPassResponse == null)
                        {
                            IsTap = false;
                            Acr.UserDialogs.UserDialogs.Instance.Alert("Password not reset.");
                            UserDialogs.Instance.HideLoading();
                        }
                        else
                        {
                            IsTap = false;
                            Acr.UserDialogs.UserDialogs.Instance.Alert(ResetPassResponse.Message);
                            UserDialogs.Instance.HideLoading();
                        }
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
            finally
            {
                Acr.UserDialogs.UserDialogs.Instance.HideLoading();
            }
        }
        #endregion
    }
}