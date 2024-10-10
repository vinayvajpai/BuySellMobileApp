using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Acr.UserDialogs;
using BuySell.Helper;
using BuySell.Model.LoginResponse;
using BuySell.Model.RestResponse;
using BuySell.View;
using BuySell.Views;
using BuySell.WebServices;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BuySell.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly Services.IMessageService _messageService;

        #region Constructor
        public LoginViewModel(INavigation nav)
        {
            this._messageService = DependencyService.Get<Services.IMessageService>();
            navigation = nav;
        }
        #endregion

        #region Properties
        private string _userName = "";
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; OnPropertyChanged("UserName"); }
        }

        private string _password = "";
        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged("Password"); }
        }
        #endregion

        #region Commands
        private Command _SignUpCommand;
        public Command SignUpCommand
        {
            get
            {
                return _SignUpCommand ?? (_SignUpCommand = new Command(async () =>
                {
                    try
                    {
                        if (IsTap)
                            return;
                        IsTap = true;
                        await navigation.PushAsync(new SignUpPage());
                    }
                    catch (Exception ex)
                    {
                        IsTap = false;
                        Debug.WriteLine(ex.Message);
                    }
                    
                }));

            }
        }

        private Command _LoginCommand;
        public Command LoginCommand
        {
            get
            {
                return _LoginCommand ?? (_LoginCommand = new Command(async () =>
                  await GetLoginMethod()));
            }
        }

        private Command _TermsCommand;
        public Command TermsCommand
        {
            get
            {
                return _TermsCommand ?? (_TermsCommand = new Command(async () =>
                 await Launcher.OpenAsync(new Uri("https://buysellclothing.com/terms-and-conditions"))));
            }
        }

        private Command _PrivacyPolicyCommand;
        public Command PrivacyPolicyCommand
        {
            get
            {
                return _PrivacyPolicyCommand ?? (_PrivacyPolicyCommand = new Command(async () =>
                 await Launcher.OpenAsync(new Uri("https://buysellclothing.com/privacy-policy"))));
            }
        }
        #endregion

        #region Methods
        public async Task GetLoginMethod()
        {
            try
            {
                if(!Constant.IsConnected)
                {
                    UserDialogs.Instance.Toast("Internet not available");
                    IsTap = false;
                    return;
                }
                if (IsTap)
                    return;
                IsTap = true;

                if (string.IsNullOrEmpty(UserName) || string.IsNullOrWhiteSpace(UserName))
                {
                    await _messageService.ShowAsync("Please enter username");
                    IsTap = false;
                    return;
                }
                if (string.IsNullOrEmpty(Password) || string.IsNullOrWhiteSpace(Password))
                {
                    await _messageService.ShowAsync("Please enter password");
                    IsTap = false;
                    return;
                }
                
                Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Logging, Please wait...");
                string methodUrl = "/api/Account/Login?userId=" + UserName + "&password=" +HttpUtility.UrlEncode(Password);
                RestResponseModel responseModel = await WebService.GetData(methodUrl, true);
                if (responseModel != null)
                {
                    LoginResponseModel loginResponse = JsonConvert.DeserializeObject<LoginResponseModel>(responseModel.response_body);
                    if (responseModel.status_code == 200)
                    {
                        if (loginResponse != null)
                        {  
                            if (loginResponse.StatusCode == 200)
                            {
                                if (loginResponse.Data.RequirePasswordChange) {
                                    Constant.LoginUserData = loginResponse.Data;
                                    await navigation.PushAsync(new ResetConfirmPasswordPage());
                                }
                                else
                                {

                                    Global.Username = UserName;
                                    Global.Password = Password;
                                    Constant.LoginUserData = loginResponse.Data;
                                    //Global.SetValueInProperties("LoginUserName", JsonConvert.SerializeObject(UserName));
                                    //Global.SetValueInProperties("LoginUserPass", JsonConvert.SerializeObject(Password));
                                    Global.SetValueInProperties("LoginUserName", UserName);
                                    Global.SetValueInProperties("LoginUserPass", Password);
                                    Global.SetValueInProperties("LoginUserData", JsonConvert.SerializeObject(Constant.LoginUserData));
                                    Settings.FirstRun = true;
                                    App.Current.MainPage = new NavigationPage(new DashBoardView(true, true));
                                }
                            }
                            else
                            {
                                IsTap = false;
                                Acr.UserDialogs.UserDialogs.Instance.Alert(loginResponse.Message);
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
                    else if (responseModel.status_code == 400)
                    {
                        if (loginResponse != null)
                        {
                            IsTap = false;
                            Acr.UserDialogs.UserDialogs.Instance.Alert("Invalid Credentials");
                            UserDialogs.Instance.HideLoading();
                        }
                        else
                        {
                            IsTap = false;
                            Acr.UserDialogs.UserDialogs.Instance.Alert(loginResponse.Message);
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
                        Acr.UserDialogs.UserDialogs.Instance.Alert(loginResponse.Message);
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