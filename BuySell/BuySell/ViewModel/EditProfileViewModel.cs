using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using BuySell.CustomControl;
using BuySell.Helper;
using BuySell.Model;
using BuySell.Model.LoginResponse;
using BuySell.Model.RestResponse;
using BuySell.Model.SignUpResponse;
using BuySell.Popup;
using BuySell.Views;
using BuySell.WebServices;
using Controls.ImageCropper;
using FFImageLoading.Work;
using MediaManager.Forms;
using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Rg.Plugins.Popup.Services;
using SkiaSharp;
using Xamarin.Essentials;
using Xamarin.Forms;
using ImageSource = Xamarin.Forms.ImageSource;

namespace BuySell.ViewModel
{
    public class EditProfileViewModel : BaseViewModel
    {
        string fileExt = "";
        string ImagePath = "";
        public ImageSource UserImage { get; private set; }
        public bool IsUploadTagImage = false;
        #region Constructor
        private readonly Services.IMessageService _messageService;
        public EditProfileViewModel(INavigation _nav)
        {
            navigation = _nav;
            this._messageService = DependencyService.Get<Services.IMessageService>();
        }
        #endregion

        #region Properties

        private ImageSource _ProfilePicture;
        public ImageSource ProfilePicture
        {
            get { return _ProfilePicture; }
            set { _ProfilePicture = value; OnPropertyChanged("ProfilePicture"); }
        }

        private ImageSource _OrginalProfilePicture;
        public ImageSource OrginalProfilePicture
        {
            get { return _OrginalProfilePicture; }
            set { _OrginalProfilePicture = value; OnPropertyChanged("OrginalProfilePicture"); }
        }

        private string _ProfilePictureURL;
        public string ProfilePictureURL
        {
            get { return _ProfilePictureURL; }
            set { _ProfilePictureURL = value; OnPropertyChanged("ProfilePictureURL"); }
        }
        
        
        private string _ShipFromAdd;
        public string ShipFromAdd
        {
            get { return _ShipFromAdd; }
            set { _ShipFromAdd = value; OnPropertyChanged("ShipFromAdd"); }
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
                    IsPassword = true;
                }
                else
                {
                    PasswordMessage = Color.Default;
                    IsPassword = false;
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

        private bool _AddProPictxt = false;
        public bool AddProPictxt
        {
            get { return _AddProPictxt; }
            set { _AddProPictxt = value; OnPropertyChanged("AddProPictxt"); }
        }
        private bool _IsPassword = false;
        public bool IsPassword
        {
            get { return _IsPassword; }
            set { _IsPassword = value; OnPropertyChanged("IsPassword"); }
        }

        private bool _IsImageSelect = false;
        public bool IsImageSelect
        {
            get { return _IsImageSelect; }
            set { _IsImageSelect = value; OnPropertyChanged("IsImageSelect"); }
        }
        #endregion

        #region Coomands
        private ICommand _BackCommand;
        public ICommand BackCommand
        {
            get
            {
                if (_BackCommand == null)
                {
                    _BackCommand = new Command(async () =>
                    {
                        try
                        {
                            if (IsTap)
                                return;
                            IsTap = true;
                            MessagingCenter.Unsubscribe<object, byte[]>("IsImgAdd", "IsImgAdd");
                            MessagingCenter.Unsubscribe<object, byte[]>("ImageToBeDelete", "ImageToBeDelete");
                            await Task.Delay(100);
                            IsTap = false;
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

        private Command _EditProfCommand;
        public Command EditProfCommand
        {
            get
            {
                if (_EditProfCommand == null)
                {
                    try
                    {
                        _EditProfCommand = new Command(async () =>
                        {
                            var validatedPassword = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$");
                            if (!validatedPassword.IsMatch(Password))
                            {
                                await _messageService.ShowAsync("Please enter valid Password");
                                IsPassword = true;
                                return;
                            }
                            //else if (Global.Password == Password)
                            //{
                            //    await _messageService.ShowAsync("Please enter new Password");
                            //    PasswordMessage = Color.Red;
                            //    return;
                            //}
                            else
                            {
                                IsPassword = false;
                                PasswordMessage = Color.Default;
                            }
                            await SaveProfile();
                        });
                    }
                    catch (Exception ex)
                    {

                    }
                }

                return _EditProfCommand;
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
            try
            {
                ImageToBeEdited = ImgEdit;
                IsUploadTagImage = true;
                EditImage();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private async void EditImage()
        {
            try
            {
                byte[] imageData;

                StreamImageSource streamImageSource = (StreamImageSource)OrginalProfilePicture;
                System.Threading.CancellationToken cancellationToken = System.Threading.CancellationToken.None;
                Task<Stream> task = streamImageSource.Stream(cancellationToken);
                Stream stream = task.Result;

                //Convert Image Stream to Byte Array  
                MemoryStream ms = new MemoryStream();
                stream.CopyTo(ms);
                imageData = ms.ToArray();
                var path = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/Profile.png";
                File.WriteAllBytes(path, imageData);
                await ImageCropper.Current.Crop(new CropSettings()
                {
                    CropShape = CropSettings.CropShapeType.Rectangle,
                    PageTitle = "EDIT IMAGE",
                }, path).ContinueWith(t =>
                {
                    if (t.IsFaulted)
                    {
                        var ex = t.Exception;
                        IsUploadTagImage = false;
                        //alert user
                    }
                    else if (t.IsCanceled)
                    {
                        //do nothing
                        IsUploadTagImage = false;
                    }
                    else if (t.IsCompleted)
                    {
                        //var result = t.Result;
                        //var imgSource = ImageSource.FromStream(() => new MemoryStream(File.ReadAllBytes(t.Result))); ;
                        //Global.ImageToBeDelete = File.ReadAllBytes(t.Result);
                        if (IsUploadTagImage)
                        {
                            var arg = ImageSource.FromStream(() => new MemoryStream(File.ReadAllBytes(t.Result))); ;
                            ProfilePicture = arg;
                            if (PopupNavigation.PopupStack.Count > 0)
                            {
                                PopupNavigation.PopAsync();
                            }
                        }
                        IsUploadTagImage = false;
                        IsImageSelect = true;
                    }
                });
            }
            catch (Exception ex)
            {
                IsTap = false;
                Debug.WriteLine(ex.Message);
            }
        }

        private void Pupupdefault_eventDelete(object sender, EventArgs e)
        {
            try
            {
                if (IsTap)
                    return;
                IsTap = true;

                ProfilePicture = null;
                IsImageSelect = true;
                AddProPictxt = true;

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
                        OrginalProfilePicture = imgSource;
                        IsImageSelect = true;
                        AddProPictxt = false;

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

                });

                if (file == null)
                    return;

                var stream = file.GetStream();
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
                        OrginalProfilePicture = imgSource;
                        IsImageSelect = true;
                        AddProPictxt = false;
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
                Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Updating Profile, Please wait...");
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

                if(!string.IsNullOrWhiteSpace(PhoneNumber))
                editProf.PhoneNumber = PhoneNumber.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "");
                else
                    editProf.PhoneNumber = Constant.LoginUserData.PhoneNumber;
                
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
                                IsImageSelect = false;

                                Global.AlertWithAction("Profile details updated successfully!!", () =>
                                {
                                    Global.Username = UserName;
                                    Global.Password = Password;
                                    MessagingCenter.Unsubscribe<object, byte[]>("IsImgAdd", "IsImgAdd");
                                    MessagingCenter.Unsubscribe<object, byte[]>("ImageToBeDelete", "ImageToBeDelete");
                                    
                                    Constant.LoginUserData.Id =  loginResponse.Data.Id;
                                    Constant.LoginUserData.FirstName = loginResponse.Data.FirstName;
                                    Constant.LoginUserData.LastName = loginResponse.Data.LastName;
                                    Constant.LoginUserData.UserId = loginResponse.Data.UserId;
                                    Constant.LoginUserData.PhoneNumber = loginResponse.Data.PhoneNumber;
                                    Constant.LoginUserData.ProfilePath = loginResponse.Data.ProfilePath;
                                    Constant.LoginUserData.RequirePasswordChange = loginResponse.Data.RequirePasswordChange;
                                    Constant.LoginUserData.Email = loginResponse.Data.Email;

                                    Application.Current.Properties.Remove("LoginUserName");
                                    Application.Current.Properties.Remove("LoginUserPass");
                                    Application.Current.Properties.Remove("LoginUserData");
                                    Application.Current.Properties.Remove("LoginUserPhoneNo");
                                    Application.Current.SavePropertiesAsync();
                                    Global.SetValueInProperties("LoginUserName", UserName);
                                    Global.SetValueInProperties("LoginUserPass", Password);
                                    Global.SetValueInProperties("LoginUserPhoneNo", PhoneNumber);
                                    Global.SetValueInProperties("LoginUserData", JsonConvert.SerializeObject(Constant.LoginUserData));
                                    //App.Current.MainPage = new NavigationPage(new AccountPage());
                                });
                                Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                                //_ = GetLoginMethod();
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
                    Acr.UserDialogs.UserDialogs.Instance.Alert("Unable to update user profile");
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
                                Global.AlertWithAction("Profile details updated successfully!!", () =>
                                {
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
                                    //App.Current.MainPage = new NavigationPage(new AccountPage());
                                });
                                //var alertConfig = new AlertConfig
                                //{
                                //    Message = "Profile details updated successfully!!",
                                //    OkText = "OK",
                                //    OnAction = () =>
                                //    {
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
                                //        //App.Current.MainPage = new NavigationPage(new AccountPage());
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
                            Acr.UserDialogs.UserDialogs.Instance.Alert("Unable to update user profile");
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
                }
                else
                {
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

        #endregion

        #region getall Shipping from Addresses
        public async void GetAllShippingFromAddress()
        {
            try
            {
                if (!Constant.IsConnected)
                {
                    UserDialogs.Instance.Toast("Internet not available");
                    IsTap = false;
                    return;
                }

                Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Getting address.. Please wait..");
                await Task.Delay(50);
                string methodUrl = $"/api/Account/GetFromShippingAddress?id={Constant.LoginUserData.Id}";
                if (!string.IsNullOrWhiteSpace(methodUrl))
                {
                    RestResponseModel responseModel = await WebService.GetData(methodUrl, true);
                    if (responseModel != null)
                    {
                        if (responseModel.status_code == 200)
                        {
                            var ShoppingAdd = JsonConvert.DeserializeObject<AddAddressModel>(responseModel.response_body);
                            if (ShoppingAdd != null)
                            {
                                Constant.globalSelectedFromAddress = ShoppingAdd;
                                if (Constant.globalSelectedFromAddress != null)
                                {
                                    if (!string.IsNullOrWhiteSpace(Constant.globalSelectedFromAddress.AddressLine1))
                                    {
                                        ShipFromAdd = Constant.globalSelectedFromAddress.AddressLine1;
                                    }
                                    if (!string.IsNullOrWhiteSpace(Constant.globalSelectedFromAddress.AddressLine2))
                                    {
                                        ShipFromAdd = ShipFromAdd + ", " + Constant.globalSelectedFromAddress.AddressLine2;
                                    }
                                    if (!string.IsNullOrWhiteSpace(Constant.globalSelectedFromAddress.Country))
                                    {
                                        ShipFromAdd = ShipFromAdd + ", " + Constant.globalSelectedFromAddress.Country;
                                    }
                                    if (!string.IsNullOrWhiteSpace(Constant.globalSelectedFromAddress.ZipCode))
                                    {
                                        ShipFromAdd = ShipFromAdd + ", " + Constant.globalSelectedFromAddress.ZipCode;
                                    }
                                }
                                else
                                {
                                    ShipFromAdd = string.Empty;
                                }
                            }
                            else
                            {
                                Constant.globalSelectedFromAddress = new AddAddressModel();
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
                        else if (responseModel.status_code == 404)
                        {
                            Constant.globalSelectedFromAddress = new AddAddressModel();
                            Global.GlobalShipFromAddressList = new ObservableCollection<AddAddressModel>();
                            ShipFromAdd = string.Empty;
                            IsTap = false;
                            Acr.UserDialogs.UserDialogs.Instance.Toast("there is no address found ,please add");
                            UserDialogs.Instance.HideLoading();
                        }
                        else
                        {
                            Acr.UserDialogs.UserDialogs.Instance.Alert(responseModel.ErrorMessage);
                            UserDialogs.Instance.HideLoading();
                        }
                    }
                    else
                    {
                        Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                        UserDialogs.Instance.HideLoading();
                    }
                }
                else
                {
                    Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                    UserDialogs.Instance.HideLoading();
                }
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                Debug.WriteLine(ex.Message);
            }
        }
        #endregion

    }
}
