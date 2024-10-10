using System;
using System.Net;
using System.Text.RegularExpressions;
using Acr.UserDialogs;
using BuySell.Helper;
using BuySell.Model;
using BuySell.Model.LoginResponse;
using BuySell.Model.RestResponse;
using BuySell.WebServices;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.IO;
using BuySell.View;
using System.Diagnostics;
using System.Windows.Input;

namespace BuySell.ViewModel
{
	public class ResetConfirmPasswordViewModel : BaseViewModel
	{
        string fileExt = "";
        string ImagePath = "";
        public ImageSource UserImage { get; private set; }
        public bool IsUploadTagImage = false;
        public ResetConfirmPasswordViewModel(INavigation nav)
		{
            navigation = nav;
            SetProfileData();
        }

        #region Properties
        private string _confirmPassword;
        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set { _confirmPassword = value; OnPropertyChanged("ConfirmPassword"); }
        }

        private ImageSource _ProfilePicture;
        public ImageSource ProfilePicture
        {
            get { return _ProfilePicture; }
            set { _ProfilePicture = value; OnPropertyChanged("ProfilePicture"); }
        }

        private string _ProfilePictureURL;
        public string ProfilePictureURL
        {
            get { return _ProfilePictureURL; }
            set { _ProfilePictureURL = value; OnPropertyChanged("ProfilePictureURL"); }
        }

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; OnPropertyChanged("FirstName"); }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; OnPropertyChanged("LastName"); }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged("Email"); }
        }

        private string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; OnPropertyChanged("UserName"); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged("Password");
                var validatedPassword = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$");
                if (!validatedPassword.IsMatch(value))
                {
                    PasswordMessage = Color.Red;
                }
                else
                {
                    PasswordMessage = Color.Default;
                }
            }
        }
        public Color _PasswordMessage;
        public Color PasswordMessage
        {
            get { return _PasswordMessage; }
            set
            {
                _PasswordMessage = value;
                OnPropertyChanged("PasswordMessage");
            }
        }

        private string _phoneNumber;
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                _phoneNumber = value;
                OnPropertyChanged("PhoneNumber");
            }
        }
        private string _ConvertPhoneNumber;
        public string ConvertPhoneNumber
        {
            get { return _ConvertPhoneNumber; }
            set
            {
                _ConvertPhoneNumber = value;
                OnPropertyChanged("ConvertPhoneNumber");
            }
        }
        #endregion

        #region Commands
        private ICommand _SaveCommand;
        public ICommand SaveCommand
        {
            get
            {
                if (_SaveCommand == null)
                {
                    _SaveCommand = new Command(async () =>
                    {
                        await SaveProfile();
                    });
                }
                return _SaveCommand;
            }
        }

        #endregion

        #region Method

        private async Task SaveProfile()
        {
            try
            {
                if (!Constant.IsConnected)
                {
                    UserDialogs.Instance.Toast("Internet not available");
                    IsTap = false;
                    return;
                }
                else if (string.IsNullOrEmpty(Password))
                {
                    UserDialogs.Instance.Toast("Please enter new password");
                    IsTap = false;
                    return;
                }
                else if (!Password.Equals(ConfirmPassword))
                {
                    UserDialogs.Instance.Toast("Password and Confirm password does not match");
                    IsTap = false;
                    return;
                }

                Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Password is updating, Please wait...");
                await Task.Delay(500);
                EditProfilePicture proPic = null;
                proPic = new EditProfilePicture();
                if (ProfilePicture != null)
                {
                    proPic.Image = ProfilePicture.ConvertImagesourceToBase64();
                    proPic.Extension = fileExt;
                    proPic.ImagePath = ImagePath;
                    proPic.IsDeleted = true;
                }

                var editProf = new EditProfileModel();
                editProf.FirstName = FirstName;
                editProf.LastName = LastName;
                editProf.Email = Email;
                if(PhoneNumber!=null)
                    editProf.PhoneNumber = PhoneNumber.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "");
                editProf.Password = Password;
                editProf.UserId = UserName;
                editProf.Id = Constant.LoginUserData.Id;
                editProf.ProfilePicture = proPic;

                string methodUrl = "/api/Account/UpdateUserProfile";
                RestResponseModel responseModel = await WebService.PutData(editProf, methodUrl, true);
                if (responseModel != null)
                {
                    if (responseModel.status_code == 200)
                    {
                        AddItemResponseModel loginResponse = JsonConvert.DeserializeObject<AddItemResponseModel>(responseModel.response_body);
                        if (loginResponse != null)
                        {
                            if (loginResponse.StatusCode == 200)
                            {
                                
                                _ = GetLoginMethod();
                            }
                            else
                            {
                                Acr.UserDialogs.UserDialogs.Instance.Alert(loginResponse.Message);
                                UserDialogs.Instance.HideLoading();
                            }
                        }
                        else
                        {
                            Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                            UserDialogs.Instance.HideLoading();
                        }
                    }
                    else if (responseModel.status_code == 400)
                    {
                        if (!string.IsNullOrEmpty(responseModel.ErrorMessage))
                        {
                            Acr.UserDialogs.UserDialogs.Instance.Alert(responseModel.ErrorMessage);
                            UserDialogs.Instance.HideLoading();
                        }
                        else
                        {
                            Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
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
                        Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                        UserDialogs.Instance.HideLoading();
                    }
                }
                else
                {
                    Acr.UserDialogs.UserDialogs.Instance.Alert("Unable to update userprofile");
                    UserDialogs.Instance.HideLoading();
                }
            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Alert(ex.Message);
                UserDialogs.Instance.HideLoading();
            }
            finally
            {
                //Acr.UserDialogs.UserDialogs.Instance.HideLoading();
            }
        }

        public async Task GetLoginMethod()
        {
            try
            {
                if (!Constant.IsConnected)
                {
                    UserDialogs.Instance.Toast("Internet not available");
                    IsTap = false;
                    return;
                }
                //Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Loading");
                string methodUrl = "/api/Account/Login?userId=" + UserName + "&password=" + Password;
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
                                Global.AlertWithAction("Password has been updated successfully!!", () =>
                                {
                                    Settings.FirstRun = true;
                                    Global.Username = UserName;
                                    Global.Password = Password;
                                    MessagingCenter.Unsubscribe<object, byte[]>("IsImgAdd", "IsImgAdd");
                                    MessagingCenter.Unsubscribe<object, byte[]>("ImageToBeDelete", "ImageToBeDelete");
                                    Constant.LoginUserData = loginResponse.Data;
                                    Application.Current.Properties.Remove("LoginUserName");
                                    Application.Current.Properties.Remove("LoginUserPass");
                                    Application.Current.Properties.Remove("LoginUserData");
                                    Application.Current.Properties.Remove("LoginUserPhoneNo");
                                    Application.Current.SavePropertiesAsync();
                                    Global.SetValueInProperties("LoginUserName", UserName);
                                    Global.SetValueInProperties("LoginUserPass", Password);
                                    Global.SetValueInProperties("LoginUserPhoneNo", PhoneNumber);
                                    Global.SetValueInProperties("LoginUserData", JsonConvert.SerializeObject(Constant.LoginUserData));
                                    App.Current.MainPage = new NavigationPage(new DashBoardView(true, true));
                                });
                                //var alertConfig = new AlertConfig
                                //{
                                //    Message = "Password has been updated successfully!!",
                                //    OkText = "OK",
                                //    OnAction = () =>
                                //    {


                                //        //Global.Username = UserName;
                                //        //Global.Password = Password;
                                //        //Constant.LoginUserData = loginResponse.Data;
                                //        ////Global.SetValueInProperties("LoginUserName", JsonConvert.SerializeObject(UserName));
                                //        ////Global.SetValueInProperties("LoginUserPass", JsonConvert.SerializeObject(Password));
                                //        //Global.SetValueInProperties("LoginUserName", UserName);
                                //        //Global.SetValueInProperties("LoginUserPass", Password);
                                //        //Global.SetValueInProperties("LoginUserData", JsonConvert.SerializeObject(Constant.LoginUserData));
                                //        Settings.FirstRun = true;


                                //        Global.Username = UserName;
                                //        Global.Password = Password;
                                //        MessagingCenter.Unsubscribe<object, byte[]>("IsImgAdd", "IsImgAdd");
                                //        MessagingCenter.Unsubscribe<object, byte[]>("ImageToBeDelete", "ImageToBeDelete");
                                //        Constant.LoginUserData = loginResponse.Data;
                                //        Application.Current.Properties.Remove("LoginUserName");
                                //        Application.Current.Properties.Remove("LoginUserPass");
                                //        Application.Current.Properties.Remove("LoginUserData");
                                //        Application.Current.Properties.Remove("LoginUserPhoneNo");
                                //        Application.Current.SavePropertiesAsync();
                                //        Global.SetValueInProperties("LoginUserName", UserName);
                                //        Global.SetValueInProperties("LoginUserPass", Password);
                                //        Global.SetValueInProperties("LoginUserPhoneNo", PhoneNumber);
                                //        Global.SetValueInProperties("LoginUserData", JsonConvert.SerializeObject(Constant.LoginUserData));

                                //        App.Current.MainPage = new NavigationPage(new DashBoardView(true, true));
                                //    }
                                //};
                                //Acr.UserDialogs.UserDialogs.Instance.Alert(alertConfig);
                            }
                            else
                            {
                                Acr.UserDialogs.UserDialogs.Instance.Alert(loginResponse.Message);
                                UserDialogs.Instance.HideLoading();
                            }
                        }
                        else
                        {
                            Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                            UserDialogs.Instance.HideLoading();
                        }
                    }
                    else if (responseModel.status_code == 400)
                    {
                        if (loginResponse != null)
                        {
                            Acr.UserDialogs.UserDialogs.Instance.Alert("Unable to update userprofile");
                            UserDialogs.Instance.HideLoading();
                        }
                        else
                        {
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
            finally
            {
                Acr.UserDialogs.UserDialogs.Instance.HideLoading();
            }
        }

        public byte[] DownLoadAndStoreImage(string url)
        {
            //  string OnlineImageUrl =url;
            //_image = await WebService.GetData(OnlineImageUrl, true);
            byte[] imageBytes = null;
            using (var webClient = new WebClient())
            {
                imageBytes = webClient.DownloadData(url);
                if (imageBytes != null)
                {

                }
            }
            return imageBytes;
        }

        async void SetProfileData()
        {
            Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Loading Info..");
            await Task.Delay(50);
            FirstName = (Constant.LoginUserData.FirstName).Replace("'", "");
            LastName = (Constant.LoginUserData.LastName).Replace("'", "");
            UserName = Constant.LoginUserData.UserId;
            Email = Constant.LoginUserData.Email;
            PhoneNumber = Constant.LoginUserData.PhoneNumber;
            if (!string.IsNullOrEmpty(PhoneNumber))
            {
                //lblPhone.Text = string.Format("({0}) {1}-{2}", phoneNumber.Substring(0, 3), phoneNumber.Substring(3, 3), phoneNumber.Substring(6));
               ConvertPhoneNumber = string.Format("({0}) {1}-{2}", PhoneNumber.Substring(0, 3), PhoneNumber.Substring(3, 3), PhoneNumber.Substring(6));
            }
            
            if (Application.Current.Properties.ContainsKey("LoginUserPhoneNo"))
            {
                if (string.IsNullOrEmpty(Constant.LoginUserData.PhoneNumber))
                {
                    //lblPhone.Text = Global.GetValueInProperties("LoginUserPhoneNo") != null ? Global.GetValueInProperties("LoginUserPhoneNo").ToString() : "";
                   ConvertPhoneNumber = Global.GetValueInProperties("LoginUserPhoneNo") != null ? Global.GetValueInProperties("LoginUserPhoneNo").ToString() : "";
                }
            }

            await Task.Run(() =>
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Constant.LoginUserData.ProfilePath)))
                {
                    var data = DownLoadAndStoreImage(Constant.LoginUserData.ProfilePath.ToString());
                    ProfilePicture = ImageSource.FromStream(() =>
                            new MemoryStream(data)
                            );
                    Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                }
            });
           
        }

        #endregion
    }
}

