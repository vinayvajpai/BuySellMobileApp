using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Input;
using Acr.UserDialogs;
using BuySell.CustomControl;
using BuySell.Helper;
using BuySell.Model.LoginResponse;
using BuySell.Model.RestResponse;
using BuySell.Model.SignUpResponse;
using BuySell.Popup;
using BuySell.View;
using BuySell.WebServices;
using Controls.ImageCropper;
using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace BuySell.ViewModel
{
    public class SignUpViewModel : BaseViewModel
    {
        private readonly Services.IMessageService _messageService;
        public List<ImageSource> imageList = new List<ImageSource>();
        public List<ImageSource> tagImageList = new List<ImageSource>();
        public bool IsUploadTagImage = false;
        public bool IsImageSelect = false;
        string fileExt = "";
        string ImagePath = "";
        #region Constructor
        public SignUpViewModel(INavigation nav)
        {
            this._messageService = DependencyService.Get<Services.IMessageService>();
            navigation = nav;
        }
        #endregion

        #region Properties
        public ImageSource UserImage { get; private set; }

        private ImageSource _ProfilePicture;
        public ImageSource ProfilePicture
        {
            get { return _ProfilePicture; }
            set { _ProfilePicture = value; OnPropertyChanged("ProfilePicture"); }
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

        private string _phoneNumber;
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; OnPropertyChanged("PhoneNumber"); }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged("Email");
            }
        }

        private string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; OnPropertyChanged("UserName"); }
        }

        private string _password = string.Empty;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged("Password");
                //Add password validation using regex
                var validatedPassword = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$");
                if (!validatedPassword.IsMatch(value))
                {
                    PasswordMessage = Color.Red;
                    IsPassword = true;
                }
                else
                {
                    PasswordMessage = Color.Default;
                    IsPassword = false;
                }

                if (string.IsNullOrEmpty(value))
                {
                    PasswordMessage = Color.Default;
                    IsPassword = false;
                }
                else if (value == ConfirmPassword)
                {
                    PasswordMessage = Color.Default;
                    ConfirmPasswordMessage = Color.Default;
                    IsConfirmPassword = false;
                }
                else if (!string.IsNullOrEmpty(ConfirmPassword))
                {
                    //PasswordMessage = Color.Red;
                    IsConfirmPassword = false;
                    ConfirmPasswordMessage = Color.Red;
                }
                else
                {
                    PasswordMessage = Color.Default;
                    ConfirmPasswordMessage = Color.Default;
                }
            }
        }

        private string _confirmPassword = string.Empty;
        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set
            {
                _confirmPassword = value;
                OnPropertyChanged("ConfirmPassword");
                if (string.IsNullOrEmpty(value))
                {
                    ConfirmPasswordMessage = Color.Default;
                    IsConfirmPassword = false;
                }
                else if (value != Password || Password.Length < 8)
                {
                    ConfirmPasswordMessage = Color.Red;
                    IsConfirmPassword = true;
                }
                else if (Password.Length == ConfirmPassword.Length)
                {
                    ConfirmPasswordMessage = Color.Default;
                    IsConfirmPassword = false;
                }
                else
                {
                    ConfirmPasswordMessage = Color.Default;
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

        public Color _ConfirmPasswordMessage;
        public Color ConfirmPasswordMessage
        {
            get { return _ConfirmPasswordMessage; }
            set
            {
                _ConfirmPasswordMessage = value;
                OnPropertyChanged("ConfirmPasswordMessage");
            }
        }

        private bool _IsChecked;
        public bool IsChecked
        {
            get { return _IsChecked; }
            set { _IsChecked = value; OnPropertyChanged("IsChecked"); }
        }

        private string _ImageToBeEdited = string.Empty;
        public string ImageToBeEdited
        {
            get { return _ImageToBeEdited; }
            set { _ImageToBeEdited = value; OnPropertyChanged("ImageToBeEdited"); }
        }

        private string _ImgEdit;
        public string ImgEdit
        {
            get { return _ImgEdit; }
            set { _ImgEdit = value; OnPropertyChanged("ImgEdit"); }
        }

        private List<ImageSource> _imgList = new List<ImageSource>();
        public List<ImageSource> ImgList
        {
            get
            {
                return _imgList;
            }
            set
            {
                _imgList = value;
                OnPropertyChanged("ImgList");
            }
        }

        private bool _AddProPic = true;
        public bool AddProPic
        {
            get { return _AddProPic; }
            set { _AddProPic = value; OnPropertyChanged("AddProPic"); }
        }
        private bool _EditProPic = false;
        public bool EditProPic
        {
            get { return _EditProPic; }
            set { _EditProPic = value; OnPropertyChanged("EditProPic"); }
        }

        private bool _IsPassword = false;
        public bool IsPassword
        {
            get { return _IsPassword; }
            set { _IsPassword = value; OnPropertyChanged("IsPassword"); }
        }
        private bool _IsConfirmPassword = false;
        public bool IsConfirmPassword
        {
            get { return _IsConfirmPassword; }
            set { _IsConfirmPassword = value; OnPropertyChanged("IsConfirmPassword"); }
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
                    //Condition added to check wheather password is valid or not before joining
                    var validatedPassword = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$");
                    if (!validatedPassword.IsMatch(Password))
                    {
                        await _messageService.ShowAsync("Please enter valid  Password");
                    }
                    else {

                        if (Password.Length != ConfirmPassword.Length)
                        {
                            IsTap = false;
                            IsConfirmPassword = true;
                            await _messageService.ShowAsync("Please enter valid Confirm Password");
                            return;
                        }
                        else
                        {
                            IsTap = false;
                            IsConfirmPassword = false;
                            ConfirmPasswordMessage = Color.Default;
                        }
                        var confirmed = await Application.Current.MainPage.DisplayAlert("Allow \"BuySell\" to track your activity across other companies apps and websites?", "Your data will be used to provide a more personalized experience on BUYSELL and in our ads.", "Allow", "Ask App Not to Track");
                        if (confirmed)
                        {
                            await GetSignupMethod();
                        }
                        else
                        {
                            await GetSignupMethod();
                        }
                    } 
                }
               ));
            }
        }

        private Command _EditImg1Command;
        public Command EditImg1Command
        {
            get
            {
                if (_EditImg1Command == null)
                {
                    try
                    {
                        _EditImg1Command = new Command(EditImg1Method);
                    }
                    catch (Exception ex)
                    {

                    }
                }

                return _EditImg1Command;
            }
        }

        private ICommand _UploadImageClicked;
        public ICommand UploadImageClicked
        {
            get
            {
                if (_UploadImageClicked == null)
                {
                    _UploadImageClicked = new Command(() =>
                    {
                        try
                        {
                            IsUploadTagImage = true;
                            UploadImageClickedMethod();
                        }
                        catch (Exception ex)
                        {

                        }
                    });
                }

                return _UploadImageClicked;
            }

        }

        #endregion

        #region Methods
        private void EditImg1Method()
        {
            Editimageclicked("EditingImage1");
        }

        public async void Editimageclicked(string SelectedImagename)
        {
            //try
            //{
            //    ImgEdit = SelectedImagename;
            //    var pupupdefault = new ImageDeleteEditPopup();
            //    await PopupNavigation.Instance.PushAsync(pupupdefault);
            //    pupupdefault.eventDelete += Pupupdefault_eventDelete;
            //    pupupdefault.eventEdit += Pupupdefault_eventEdit;
            //}
            //catch (Exception ex)
            //{

            //}
            try
            {
                if (ProfilePicture != null)
                {
                    ImgEdit = SelectedImagename;
                    var pupupdefault = new ImageDeleteEditPopup();
                    await PopupNavigation.Instance.PushAsync(pupupdefault);
                    pupupdefault.eventDelete += Pupupdefault_eventDelete;
                    pupupdefault.eventEdit += Pupupdefault_eventEdit;
                }
                else
                {
                    UploadImageClickedMethod();
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void Pupupdefault_eventEdit(object sender, EventArgs e)
        {
            //try
            //{
            //    ImageToBeEdited = ImgEdit;
            //    UploadImageClickedMethod();
            //}
            //catch (Exception ex)
            //{

            //}
            try
            {
                ImageToBeEdited = ImgEdit;
                IsUploadTagImage = true;
                UploadImageClickedMethod();
            }
            catch (Exception ex)
            {

            }
        }

        private void Pupupdefault_eventDelete(object sender, EventArgs e)
        {
            //try
            //{
            //    ImageToBeEdited = ImgEdit;
            //    MessagingCenter.Send<object, byte[]>("ImageToBeDelete", "ImageToBeDelete", Global.ImageToBeDelete);
            //}
            //catch (Exception ex)
            //{

            //}
            try
            {
                if (IsTap)
                    return;
                IsTap = true;

                ProfilePicture = null;
                IsImageSelect = false;
                EditProPic = false;
                AddProPic = true;
                if (PopupNavigation.PopupStack.Count > 0)
                {
                    PopupNavigation.PopAsync();
                }
                IsTap = false;

            }
            catch (Exception ex)
            {
                IsTap = false;
                Debug.WriteLine(ex.Message);
            }
        }

        public async void UploadImageClickedMethod()
        {
            try
            {
                if (PopupNavigation.PopupStack.Count > 0)
                {
                    PopupNavigation.PopAsync();
                }
                var popupdefault = new CustomCameraGalleryPopup();
                await PopupNavigation.Instance.PushAsync(popupdefault);
                popupdefault.eventCamera += Popupdefault_eventCamera;
                popupdefault.eventGallery += Popupdefault_eventGallery;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                GC.Collect();
            }
        }

        private void Popupdefault_eventGallery(object sender, EventArgs e)
        {
            try
            {
                UploadImage_Gallery();
            }
            catch (Exception ex)
            {

            }
        }

        private void Popupdefault_eventCamera(object sender, EventArgs e)
        {
            try
            {
                UploadImage_Camera();
            }
            catch (Exception ex)
            {

            }
        }

        public async void UploadImage_Camera()
        {
            try
            {
                //UserDialogs.Instance.ShowLoading();
                if (!CrossMedia.Current.IsCameraAvailable)
                {
                    UserDialogs.Instance.Alert("No Camera", " No Camera Available", "Ok");
                    return;
                }
                var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    PhotoSize = PhotoSize.Small,
                    DefaultCamera = CameraDevice.Rear,
                    AllowCropping = true
                });

                if (file == null)
                    return;
                var stream = file.GetStream();
                if (file != null && Device.RuntimePlatform == Device.iOS)
                {
                    stream = file.GetStreamWithImageRotatedForExternalStorage();
                    stream.Seek(0, SeekOrigin.Begin);
                }
                byte[] imageData;
                MemoryStream ms = new MemoryStream();
                stream.CopyTo(ms);
                imageData = ms.ToArray();

                Global.BuySellimageByte = imageData;
                var imgStream = new MemoryStream(imageData);
                imgStream.Seek(0, SeekOrigin.Begin);
                UserImage = ImageSource.FromStream(() => imgStream);
                Global.UserImage = UserImage;

                var path = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/Profile.png";
                File.WriteAllBytes(path, imageData);

                await PopupNavigation.Instance.PopAsync();
                await ImageCropper.Current.Crop(new CropSettings()
                {
                    CropShape = CropSettings.CropShapeType.Rectangle
                }, path).ContinueWith(t =>
                {
                    if (t.IsFaulted)
                    {
                        var ex = t.Exception;
                        //alert user
                    }
                    else if (t.IsCanceled)
                    {
                        //do nothing
                    }
                    else if (t.IsCompleted)
                    {
                        var result = t.Result;
                        ImagePath = result;
                        FileInfo fi = new FileInfo(result);
                        fileExt = fi.Extension;
                        var imgSource = ImageSource.FromStream(() => new MemoryStream(File.ReadAllBytes(t.Result)));
                        Global.ImageToBeDelete = File.ReadAllBytes(t.Result);
                        ProfilePicture = imgSource;
                        IsImageSelect = true;
                        if (ProfilePicture != null)
                        {
                            AddProPic = false;
                            EditProPic = true;
                        }
                        else
                        {
                            AddProPic = true;
                            EditProPic = false;
                        }
                        //MessagingCenter.Send<object, byte[]>(this, "IsImgAdd", File.ReadAllBytes(t.Result));
                    }
                });
                UserDialogs.Instance.HideLoading();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                UserDialogs.Instance.HideLoading();
            }
            finally
            {
                GC.Collect();
                UserDialogs.Instance.HideLoading();
            }
        }

        public async void UploadImage_Gallery()
        {
            try
            {
                //UserDialogs.Instance.ShowLoading();
                if (!CrossMedia.Current.IsPickPhotoSupported)
                {

                    UserDialogs.Instance.Alert("No Image", "No Image Available in gallery", "Ok");
                    return;
                }
                var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = PhotoSize.Small,
                    RotateImage = false

                });

                if (file == null)
                    return;

                var stream = file.GetStream();
                //if (file != null && Device.RuntimePlatform == Device.iOS)
                //{
                //    stream = file.GetStreamWithImageRotatedForExternalStorage();
                //    stream.Seek(0, SeekOrigin.Begin);
                //}
                byte[] imageData;
                MemoryStream ms = new MemoryStream();
                stream.CopyTo(ms);
                imageData = ms.ToArray();
                Global.BuySellimageByte = imageData;
                var imgStream = new MemoryStream(imageData);
                UserImage = ImageSource.FromStream(() => imgStream);
                Global.UserImage = UserImage;
                await PopupNavigation.Instance.PopAsync();
                await ImageCropper.Current.Crop(new CropSettings()
                {
                    CropShape = CropSettings.CropShapeType.Rectangle,
                    PageTitle = "EDIT IMAGE"
                }, file.Path).ContinueWith(t =>
                {
                    if (t.IsFaulted)
                    {
                        var ex = t.Exception;
                        //alert user
                    }
                    else if (t.IsCanceled)
                    {
                        //do nothing
                    }
                    else if (t.IsCompleted)
                    {
                        var result = t.Result;
                        ImagePath = result;
                        FileInfo fi = new FileInfo(result);
                        fileExt = fi.Extension;
                        var imgSource = ImageSource.FromStream(() => new MemoryStream(File.ReadAllBytes(t.Result)));
                        Global.ImageToBeDelete = File.ReadAllBytes(t.Result);
                        ProfilePicture = imgSource;
                        IsImageSelect = true;
                        if (ProfilePicture != null)
                        {
                            AddProPic = false;
                            EditProPic = true;
                        }
                        else
                        {
                            AddProPic = true;
                            EditProPic = false;
                        }
                        //MessagingCenter.Send<object, byte[]>(this, "IsImgAdd", File.ReadAllBytes(t.Result));
                        //do smth with result
                    }
                });
                UserDialogs.Instance.HideLoading();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                UserDialogs.Instance.HideLoading();
            }
            finally
            {
                GC.Collect();
                UserDialogs.Instance.HideLoading();
            }
        }

        public async Task GetSignupMethod()
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

                //if (ProfilePicture == null)
                //{
                //    await _messageService.ShowAsync("Please upload profile picture");
                //    return;
                //}
                if (string.IsNullOrEmpty(FirstName) || string.IsNullOrWhiteSpace(FirstName))
                {
                    IsTap = false;
                    await _messageService.ShowAsync("Please enter first name");
                    return;
                }
                else if (string.IsNullOrEmpty(LastName) || string.IsNullOrWhiteSpace(LastName))
                {
                    IsTap = false;
                    await _messageService.ShowAsync("Please enter last name");
                    return;
                }
                else if (string.IsNullOrEmpty(Email))
                {
                    IsTap = false;
                    await _messageService.ShowAsync("Please enter email address");
                    return;
                }
                else if (!Global.IsValidateEmail(Email))
                {
                    IsTap = false;
                    await _messageService.ShowAsync("User must enter valid email address to proceed");
                    return;
                }
                //else if (!Global.IsValidatePhoneNumber(PhoneNumber))
                //{
                //    await _messageService.ShowAsync("Must enter valid phone number to proceed");
                //    return;
                //}
                else if (string.IsNullOrEmpty(UserName) || string.IsNullOrWhiteSpace(UserName))
                {
                    IsTap = false;
                    await _messageService.ShowAsync("Please enter user name");
                    return;
                }
                else if (string.IsNullOrEmpty(Password) || string.IsNullOrWhiteSpace(Password))
                {
                    IsTap = false;
                    await _messageService.ShowAsync("Please enter password");
                    return;
                }
                else if (string.IsNullOrEmpty(ConfirmPassword) || string.IsNullOrWhiteSpace(ConfirmPassword))
                {
                    IsTap = false;
                    await _messageService.ShowAsync("Please enter confirm password");
                    return;
                }
                else if (ConfirmPassword != Password)
                {
                    IsTap = false;
                    await _messageService.ShowAsync("Passwords do not match");
                    return;
                }
                else if (UserName.ToUpper() == "BUYSELL")
                {
                    IsTap = false;
                    await _messageService.ShowAsync("BUYSELL is not validated!");
                    return;
                }
                else
                {
                    if (!IsChecked == true)
                    {
                        IsTap = false;
                        await _messageService.ShowAsync("You must accept our Privacy Policy & Terms to Join");
                        return;
                    }
                }

                ProfilePicture proPic = null;
                if (ProfilePicture != null)
                {
                    proPic = new ProfilePicture();
                    proPic.Image = ProfilePicture.ConvertImagesourceToBase64();
                    proPic.Extension = fileExt;
                }

                var signUpRequestModel = new SignUpRequestModel();
                signUpRequestModel.FirstName = FirstName;
                signUpRequestModel.LastName = LastName;
                signUpRequestModel.Email = Email;
                //signUpRequestModel.PhoneNumber = PhoneNumber;
                signUpRequestModel.Password = Password;
                signUpRequestModel.UserId = UserName;
                signUpRequestModel.ProfilePicture = proPic;

                Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Joining, Please wait...");
                await Task.Delay(500);
                string methodUrl = "/api/Account/Register";
                RestResponseModel responseModel = await WebService.PostData(signUpRequestModel, methodUrl, true);
                if (responseModel != null)
                {
                    if (responseModel.status_code == 0)
                    {
                        IsTap = false;
                        Acr.UserDialogs.UserDialogs.Instance.Alert("Unable to process your request. Server Timeout");
                        UserDialogs.Instance.HideLoading();
                        return;
                    }
                    SignUpResponseModel signUpResponse = JsonConvert.DeserializeObject<SignUpResponseModel>(responseModel.response_body);
                    if (signUpResponse.StatusCode == 200)
                    {
                        if (signUpResponse != null)
                        {
                            Global.AlertWithAction("User registered successfully!!", async () =>
                            {
                                MessagingCenter.Unsubscribe<object, byte[]>("IsImgAdd", "IsImgAdd");
                                MessagingCenter.Unsubscribe<object, byte[]>("ImageToBeDelete", "ImageToBeDelete");
                                await GetLogin();
                            });

                            //var alertConfig = new AlertConfig
                            //{
                            //    Message = "User registered successfully!!",
                            //    OkText = "OK",
                            //    OnAction = async () =>
                            //    {
                            //        MessagingCenter.Unsubscribe<object, byte[]>("IsImgAdd", "IsImgAdd");
                            //        MessagingCenter.Unsubscribe<object, byte[]>("ImageToBeDelete", "ImageToBeDelete");
                            //        await GetLogin();
                            //    }
                            //};

                            //Acr.UserDialogs.UserDialogs.Instance.Alert(alertConfig);

                        }
                        else
                        {
                            IsTap = false;
                            Acr.UserDialogs.UserDialogs.Instance.Alert(signUpResponse.Message);
                            UserDialogs.Instance.HideLoading();
                        }
                    }
                    else if (signUpResponse.StatusCode == 400)
                    {
                        if (signUpResponse == null)
                        {
                            IsTap = false;
                            Acr.UserDialogs.UserDialogs.Instance.Alert("User not registered");
                            UserDialogs.Instance.HideLoading();
                        }
                        else
                        {
                            IsTap = false;
                            Acr.UserDialogs.UserDialogs.Instance.Alert(signUpResponse.Message);
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
                        if (signUpResponse == null)
                        {
                            IsTap = false;
                            Acr.UserDialogs.UserDialogs.Instance.Alert("User not registered");
                            UserDialogs.Instance.HideLoading();
                        }
                        else
                        {
                            IsTap = false;
                            Acr.UserDialogs.UserDialogs.Instance.Alert(signUpResponse.Message);
                            UserDialogs.Instance.HideLoading();
                        }
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
                IsTap = false;
            }
        }

        public async Task GetLogin()
        {

            try
            {
                if (!Constant.IsConnected)
                {
                    UserDialogs.Instance.Toast("Internet not available");
                    IsTap = false;
                    return;
                }

                Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Logging, Please wait...");
                string loginMethodUrl = "/api/Account/Login?userId=" + UserName + "&password=" + HttpUtility.UrlEncode(Password);
                RestResponseModel responseModelForLogin = await WebService.GetData(loginMethodUrl, true);
                if (responseModelForLogin != null)
                {
                    LoginResponseModel loginResponse = JsonConvert.DeserializeObject<LoginResponseModel>(responseModelForLogin.response_body);
                    if (responseModelForLogin.status_code == 200)
                    {
                        if (loginResponse != null)
                        {
                            if (loginResponse.StatusCode == 200)
                            {
                                Global.Username = UserName;
                                Global.Password = Password;
                                Constant.LoginUserData = loginResponse.Data;
                                //Global.SetValueInProperties("LoginUserName", JsonConvert.SerializeObject(UserName));
                                //Global.SetValueInProperties("LoginUserPass", JsonConvert.SerializeObject(Password));
                                Global.SetValueInProperties("LoginUserName", UserName);
                                Global.SetValueInProperties("LoginUserPass", Password);
                                Global.SetValueInProperties("LoginUserData", JsonConvert.SerializeObject(Constant.LoginUserData));
                                App.Current.MainPage = new NavigationPage(new DashBoardView(true,true));
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
                    else if (responseModelForLogin.status_code == 400)
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
                    else if (responseModelForLogin.status_code == 500)
                    {
                        ResponseBodyModel responseBodyModel = JsonConvert.DeserializeObject<ResponseBodyModel>(responseModelForLogin.response_body);
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


