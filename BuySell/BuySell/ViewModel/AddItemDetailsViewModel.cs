using Acr.UserDialogs;
using BuySell.CustomControl;
using BuySell.Helper;
using BuySell.Model;
using BuySell.Model.RestResponse;
using BuySell.Popup;
using BuySell.Utility;
using BuySell.View;
using BuySell.Views;
using BuySell.WebServices;
using Controls.ImageCropper;
using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using static BuySell.Model.CategoryModel;

namespace BuySell.ViewModel
{
    public class AddItemDetailsViewModel : BaseViewModel
    {
        public int addMoreCount = 0;
        public int currentMultiQuantityIndex = 0;
        public bool IsMultiSizeSelected = false;
        public bool IsUploadTagImage = false;
        public SubRoots subRoots;
        int SelectedCommandperameter = 0;
        public List<ImageSource> imageList = new List<ImageSource>();
        public List<ImageSource> tagImageList = new List<ImageSource>();
        public List<ProductSelectImage> productImgList = new List<ProductSelectImage>();
        public List<TagImage> tagImgList = new List<TagImage>();
        public SubRoots selectedCategory;
        string fileExt = "";
        private readonly Services.IMessageService _messageService;

        #region Constructor
        public AddItemDetailsViewModel(INavigation _nav)
        {
            navigation = _nav;
            this._messageService = DependencyService.Get<Services.IMessageService>();
            MessagingCenter.Subscribe<object, string>("SelectPropertyValue", "SelectPropertyValue", (sender, arg) =>
            {
                if (arg != null)
                {
                    if (!IsSelectMultiplQty)
                    {
                        if(SelectedPropertyIndex == 0)
                        {
                            addItemDetailsModel.Brand = arg;
                        }
                        else
                        {
                            SetSelectedValues(arg, SelectedPropertyIndex);
                        }
                    }
                    else
                    {
                        var objQuantityModel = QuantityModelList[currentMultiQuantityIndex];
                        if (IsMultiSizeSelected)
                        {
                            objQuantityModel.Size = arg.ToString();
                        }
                        else
                        {
                            objQuantityModel.Quantity = arg.ToString();
                        }
                        IsSelectMultiplQty = false;
                        IsMultiSizeSelected = false;
                    }
                }
            });

            MessagingCenter.Subscribe<object>(Constant.UpdateThemeStr, Constant.UpdateThemeStr, (obj) =>
            {
                try
                {
                    if (obj != null)
                    {
                        ThemeColor = Global.GetThemeColor(Global.setThemeColor);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            });

            GetBrandList();
         
        }

        //private void Popupdefault_eventOK(object sender, string e)
        //{
        //    if(e != null)
        //    {
        //        addItemDetailsModel.Size = e;
        //    }
        //}
        #endregion

        #region Properties
        public ImageSource UserImage { get; set; }

        private ObservableCollection<BrandModel> _MasterBrandList = new ObservableCollection<BrandModel>();
        public ObservableCollection<BrandModel> MasterBrandList
        {
            get { return _MasterBrandList; }
            set { _MasterBrandList = value; OnPropertyChanged("MasterBrandList"); }
        }
        private ObservableCollection<string> _BrandList = new ObservableCollection<string>();
        public ObservableCollection<string> BrandList
        {
            get { return _BrandList; }
            set { _BrandList = value; OnPropertyChanged("BrandList"); }
        }
        private ObservableCollection<string> _SelectPickerList = new ObservableCollection<string>();
        public ObservableCollection<string> SelectPickerList
        {
            get
            {
                return _SelectPickerList;
            }
            set
            {
                _SelectPickerList = value;
                OnPropertyChanged("SelectPickerList");
            }
        }

        private ObservableCollection<string> _SelectMultiPickerList = new ObservableCollection<string>();
        public ObservableCollection<string> SelectMultiPickerList
        {
            get
            {
                return _SelectMultiPickerList;
            }
            set
            {
                _SelectMultiPickerList = value;
                OnPropertyChanged("SelectMultiPickerList");
            }
        }

        private ObservableCollection<QuantityModel> _QuantityModelList = new ObservableCollection<QuantityModel>() { new Model.QuantityModel() { QuantityType = "Multiple", Quantity = "12", Size = "XL", QIndex = 0, ThemeColor = Global.GetThemeColor(Global.setThemeColor) } };
        public ObservableCollection<QuantityModel> QuantityModelList
        {
            get
            {
                return _QuantityModelList;
            }
            set
            {
                _QuantityModelList = value;
                OnPropertyChanged("QuantityModelList");
            }
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

        private List<ProductImage> _ProimgList = new List<ProductImage>();
        public List<ProductImage> ProimgList
        {
            get
            {
                return _ProimgList;
            }
            set
            {
                _ProimgList = value;
                OnPropertyChanged("ProimgList");
            }
        }

        private int _SelectedPropertyIndex = 0;
        public int SelectedPropertyIndex
        {
            get { return _SelectedPropertyIndex; }
            set { _SelectedPropertyIndex = value; OnPropertyChanged("SelectedPropertyIndex"); }
        }

        private bool _IsSelectMultiplQty = false;
        public bool IsSelectMultiplQty
        {
            get { return _IsSelectMultiplQty; }
            set { _IsSelectMultiplQty = value; OnPropertyChanged("IsSelectMultiplQty"); }
        }

        private bool _IsShowOneQuantity = true;
        public bool IsShowOneQuantity
        {
            get { return _IsShowOneQuantity; }
            set { _IsShowOneQuantity = value; OnPropertyChanged("IsShowOneQuantity"); }
        }

        private bool _IsShowMultQuantity = false;
        public bool IsShowMultQuantity
        {
            get { return _IsShowMultQuantity; }
            set { _IsShowMultQuantity = value; OnPropertyChanged("IsShowMultQuantity"); }
        }

        private AddItemDetailsModel _addItemDetailsModel = new AddItemDetailsModel();
        public AddItemDetailsModel addItemDetailsModel
        {
            get { return _addItemDetailsModel; }
            set { _addItemDetailsModel = value; OnPropertyChanged("addItemDetailsModel"); }
        }

        private bool _IsFrm1 = true;
        public bool IsFrm1
        {
            get { return _IsFrm1; }
            set { _IsFrm1 = value; OnPropertyChanged("IsFrm1"); }
        }
        private bool _IsFrm2 = false;
        public bool IsFrm2
        {
            get { return _IsFrm2; }
            set { _IsFrm2 = value; OnPropertyChanged("IsFrm2"); }
        }
        private bool _IsFrm3 = false;
        public bool IsFrm3
        {
            get { return _IsFrm3; }
            set { _IsFrm3 = value; OnPropertyChanged("IsFrm3"); }
        }

        private bool _IsFrm4 = false;
        public bool IsFrm4
        {
            get { return _IsFrm4; }
            set { _IsFrm4 = value; OnPropertyChanged("IsFrm4"); }
        }

        private bool _IsFrm5 = false;
        public bool IsFrm5
        {
            get { return _IsFrm5; }
            set { _IsFrm5 = value; OnPropertyChanged("IsFrm5"); }
        }

        private bool _IsFrmImgTag = false;
        public bool IsFrmImgTag
        {
            get { return _IsFrmImgTag; }
            set { _IsFrmImgTag = value; OnPropertyChanged("IsFrmImgTag"); }
        }

        private string _ImageToBeEdited = string.Empty;
        public string ImageToBeEdited
        {
            get { return _ImageToBeEdited; }
            set
            {
                _ImageToBeEdited = value;
                OnPropertyChanged("ImageToBeEdited");
            }
        }
        private string _ImgEdit;
        public string ImgEdit
        {
            get { return _ImgEdit; }
            set { _ImgEdit = value; OnPropertyChanged("ImgEdit"); }
        }

        private bool _RainbowImgShow;
        public bool RainbowImgShow
        {
            get { return _RainbowImgShow; }
            set { _RainbowImgShow = value; OnPropertyChanged("RainbowImgShow"); }
        }

        private bool _CamoImgShow;
        public bool CamoImgShow
        {
            get { return _CamoImgShow; }
            set { _CamoImgShow = value; OnPropertyChanged("CamoImgShow"); }
        }

        private bool _IsShowOneSize = true;
        public bool IsShowOneSize
        {
            get { return _IsShowOneSize; }
            set { _IsShowOneSize = value; OnPropertyChanged("IsShowOneSize"); }
        }

        private bool _IsShowCustomSize = false;
        public bool IsShowCustomSize
        {
            get { return _IsShowCustomSize; }
            set { _IsShowCustomSize = value; OnPropertyChanged("IsShowCustomSize"); }
        }

        private bool _AddMoreIsVisible = true;
        public bool AddMoreIsVisible
        {
            get { return _AddMoreIsVisible; }
            set { _AddMoreIsVisible = value; OnPropertyChanged("AddMoreIsVisible"); }
        }

        private bool _InfoHeadlineIsVisible = false;
        public bool InfoHeadlineIsVisible
        {
            get { return _InfoHeadlineIsVisible; }
            set { _InfoHeadlineIsVisible = value; OnPropertyChanged("InfoHeadlineIsVisible"); }
        }

        private bool _HeadlineTxtIsVisible = false;
        public bool HeadlineTxtIsVisible
        {
            get { return _HeadlineTxtIsVisible; }
            set { _HeadlineTxtIsVisible = value; OnPropertyChanged("HeadlineTxtIsVisible"); }
        }
        private bool _InfoDescriptionIsVisible = false;
        public bool InfoDescriptionIsVisible
        {
            get { return _InfoDescriptionIsVisible; }
            set { _InfoDescriptionIsVisible = value; OnPropertyChanged("InfoDescriptionIsVisible"); }
        }

        private bool _DescriptionTxtIsVisible = false;
        public bool DescriptionTxtIsVisible
        {
            get { return _DescriptionTxtIsVisible; }
            set { _DescriptionTxtIsVisible = value; OnPropertyChanged("DescriptionTxtIsVisible"); }
        }
        private bool _IsBrandListShow = false;
        public bool IsBrandListShow
        {
            get { return _IsBrandListShow; }
            set { _IsBrandListShow = value; OnPropertyChanged("IsBrandListShow"); }
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
                            Global.ResetMessagingCenter();
                            App.Current.MainPage = new NavigationPage(new DashBoardView(true));
                        }
                        catch (Exception ex)
                        {
                            IsTap = false;
                        }
                    });
                }

                return _BackCommand;
            }

        }

        private ICommand _UploadImageClicked;
        public ICommand UploadImageClicked
        {
            get
            {
                if (_UploadImageClicked == null)
                {
                    try
                    {
                        _UploadImageClicked = new Command(async()=> {

                            if (string.IsNullOrEmpty(Global.Storecategory))
                            {
                                await _messageService.ShowAsync("Please select store first");
                                return;
                            }
                            else 
                            {
                                if (productImgList.Count >= 12)
                                {
                                    await Task.Delay(100);
                                    Acr.UserDialogs.UserDialogs.Instance.Alert("", "You can't add more than 12 images", "OK");
                                    return;
                                }
                                UploadImageClickedMethod();
                            }
                            
                        });
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }
                }
                return _UploadImageClicked;
            }

        }

        private ICommand _UploadTagImageClicked;
        public ICommand UploadTagImageClicked
        {
            get
            {
                if (_UploadTagImageClicked == null)
                {
                    _UploadTagImageClicked = new Command(async() =>
                    {
                        try
                        {
                            if (string.IsNullOrEmpty(Global.Storecategory))
                            {
                                await _messageService.ShowAsync("Please select store first");
                                return;
                            }
                            else if(tagImageList.Count > 0)
                            {
                                return;
                            }
                            else 
                            {
                                IsUploadTagImage = true;
                                ImageToBeEdited = "null";
                                UploadImageClickedMethod();
                            }
                            
                        }
                        catch (Exception ex)
                        {
                            IsTap = false;
                        }
                    });
                }

                return _UploadTagImageClicked;
            }

        }

        private ICommand _CategoryClicked;
        public ICommand CategoryClicked
        {
            get
            {
                if (_CategoryClicked == null)
                {
                    try
                    {
                        _CategoryClicked = new Command(CategoryClickedmethod);
                    }
                    catch (Exception ex)
                    {

                    }
                }

                return _CategoryClicked;
            }

        }

        private Command _SelectedTabCommand;
        public Command SelectedTabCommand
        {
            get
            {
                return _SelectedTabCommand ?? (_SelectedTabCommand = new Command(async (index) =>
                {
                    try
                    {
                        var param = Convert.ToInt16(index);
                        SelectCatOnThemeTap(param);
                    }
                    catch (Exception ex)
                    {

                    }
                }
          ));
            }
        }

        private ICommand _AddMoreCommand;
        public ICommand AddMoreCommand
        {
            get
            {
                if (_AddMoreCommand == null)
                {
                    _AddMoreCommand = new Command(() =>
                    {
                        try
                        {
                            addMoreCount++;
                            if (addMoreCount < 2)
                            {
                                QuantityModelList.Add(new Model.QuantityModel() { QuantityType = "Multiple", Quantity = "12", Size = "XL", QIndex = QuantityModelList.Count, ThemeColor = Global.GetThemeColor(Global.setThemeColor) });
                            }
                            else if (addMoreCount == 2)
                            {
                                QuantityModelList.Add(new Model.QuantityModel() { QuantityType = "Multiple", Quantity = "12", Size = "XL", QIndex = QuantityModelList.Count, ThemeColor = Global.GetThemeColor(Global.setThemeColor) });
                                AddMoreIsVisible = false;
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    });
                }

                return _AddMoreCommand;
            }
        }

        private Command<QuantityModel> _DeleteCommand;
        public Command<QuantityModel> DeleteCommand
        {
            get
            {
                if (_DeleteCommand == null)
                {
                    _DeleteCommand = new Command<QuantityModel>((QIndex) =>
                    {
                        try
                        {
                            addMoreCount--;
                            AddMoreIsVisible = true;
                            QuantityModelList.Remove(QIndex);
                            int i = 0;
                            while (i < QuantityModelList.Count)
                            {
                                var obj = QuantityModelList[i];
                                obj.QIndex = i;
                                i++;
                            }
                            var tempList = QuantityModelList.ToList();
                            QuantityModelList.Clear();
                            QuantityModelList = new ObservableCollection<QuantityModel>(tempList);
                            OnPropertyChanged("QuantityModelList");
                        }
                        catch (Exception ex)
                        {

                        }
                    });
                }

                return _DeleteCommand;
            }
        }

        private Command _InfoCommand;
        public Command InfoCommand
        {
            get
            {
                return _InfoCommand ?? (_InfoCommand = new Command<string>
                    (async =>
                         Acr.UserDialogs.UserDialogs.Instance.Alert("Snap a picture of the tag inside your shirt, pants, shoes, etc. Some people like to see the fabric content, so feel free to add a picture!")
                        //Acr.UserDialogs.UserDialogs.Instance.Alert("Take a photo of the tag inside your shirt, trousers, shoes and so on. Some customers prefer to see the fabric content, so feel free to include a photograph!")
                    )
                 );
            }
        }

        private Command _NeedHelpCommand;
        public Command NeedHelpCommand
        {
            get
            {
                return _NeedHelpCommand ?? (_NeedHelpCommand = new Command<string>
                    (
                    async async =>
                    {
                        try
                        {
                            if (IsTap)
                                return;
                            IsTap = true;
                            await navigation.PushAsync(new OurStoresPage());
                        }
                        catch (Exception)
                        {
                            IsTap = false;
                        }
                    }));
            }
        }

        private Command _ViewMoreImgCommand;
        public Command ViewMoreImgCommand
        {
            get
            {
                if (_ViewMoreImgCommand == null)
                {
                    try
                    {
                        _ViewMoreImgCommand = new Command(ViewMoreImgMethod);
                    }
                    catch (Exception)
                    {

                    }
                }

                return _ViewMoreImgCommand;
            }
        }

        private Command _BrandDropDownCommand;
        public Command BrandDropDownCommand
        {
            get
            {
                return _BrandDropDownCommand ?? (_BrandDropDownCommand = new Command
                    (
                    async async =>
                    {
                        try
                        {
                            if (IsTap)
                                return;
                            IsTap = true;
                            SelectedPropertyIndex = 0;
                            await navigation.PushAsync(new BrandPage());
                        }
                        catch (Exception)
                        {
                            IsTap = false;
                        }
                    }));
            }
        }

        private Command _SelectPropertiesValuesCommand;
        public Command SelectPropertiesValuesCommand
        {
            get
            {
                if (_SelectPropertiesValuesCommand == null)
                {
                    try
                    {
                        _SelectPropertiesValuesCommand = new Command(SelectPropertiesValuesMethod);
                    }
                    catch (Exception)
                    {

                    }
                }

                return _SelectPropertiesValuesCommand;
            }
        }

        private Command _SelectMoreQuantityCommand;
        public Command SelectMoreQuantityCommand
        {
            get
            {
                if (_SelectMoreQuantityCommand == null)
                {
                    try
                    {
                        _SelectMoreQuantityCommand = new Command(SelectMoreQuantityMethod);
                    }
                    catch (Exception)
                    {

                    }
                }

                return _SelectMoreQuantityCommand;
            }
        }

        private Command _SelectMoreSizeCommand;
        public Command SelectMoreSizeCommand
        {
            get
            {
                if (_SelectMoreSizeCommand == null)
                {
                    try
                    {
                        _SelectMoreSizeCommand = new Command(SelectMoreSizeMethod);
                    }
                    catch (Exception)
                    {

                    }
                }

                return _SelectMoreSizeCommand;
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
                    catch (Exception)
                    {

                    }
                }

                return _EditImg1Command;
            }
        }

        private Command _EditImg2Command;
        public Command EditImg2Command
        {
            get
            {
                if (_EditImg2Command == null)
                {
                    try
                    {
                        _EditImg2Command = new Command(EditImg2Method);
                    }
                    catch (Exception)
                    {

                    }
                }

                return _EditImg2Command;
            }
        }

        private Command _EditTagCommand;
        public Command EditTagCommand
        {
            get
            {
                if (_EditTagCommand == null)
                {
                    try
                    {
                        _EditTagCommand = new Command(EditTagImage);
                    }
                    catch (Exception)
                    {

                    }
                }

                return _EditTagCommand;
            }
        }

        private Command _PostCommand;
        public Command PostCommand
        {
            get
            {
                if (_PostCommand == null)
                {
                    try
                    {
                        _PostCommand = new Command(PostMethod);
                    }
                    catch (Exception)
                    {

                    }
                }

                return _PostCommand;
            }
        }

        private Command _DraftCommand;
        public Command DraftCommand
        {
            get
            {
                return _DraftCommand ?? (_DraftCommand = new Command(async (index) =>
                {
                    try
                    {
                        //UserDialogs.Instance.Alert("", "Coming soon", "OK");
                    }
                    catch (Exception ex)
                    {

                    }
                }
          ));
            }
        }
        #endregion

        #region Methods

        #region Selected properties value method

        //Method created to show items as per field choosed by user
        private async void SelectPropertiesValuesMethod(object obj)
        {
            var parameter = Convert.ToInt32(Convert.ToString(obj));
            if (parameter == 1)
            {
                SelectedCommandperameter = parameter;
                await GetPicketList(parameter, TitleName: "Headline");
                return;
            }
            else if (parameter == 2)
            {
                SelectedCommandperameter = parameter;
                await GetPicketList(parameter, TitleName: "Description");
                return;
            }
            else if (parameter == 3)
            {
                try
                {
                    if (IsTap)
                        return;
                    IsTap = true;
                    if (!string.IsNullOrEmpty(Global.Storecategory))
                    {
                        await navigation.PushAsync(new CategoryPage());
                    }
                    else
                    {
                        UserDialogs.Instance.Alert("Please select Store first.", "", "Ok");
                        IsTap = false;
                        return;
                    }
                }
                catch (Exception ex)
                {
                    IsTap = false;
                    Debug.WriteLine(ex.Message);
                }

            }
            else if (parameter == 4)
            {
                SelectedCommandperameter = parameter;
                await GetPicketList(parameter, TitleName: "Category");
                return;
            }
            else if (parameter == 5)
            {
                SelectedCommandperameter = parameter;
                await GetPicketList(parameter, TitleName: "Headline");
                return;
            }
            else if (parameter == 6)
            {
                SelectedCommandperameter = parameter;
                await GetPicketList(parameter, TitleName: "Quantity");
                return;
            }
            else if (parameter == 7)
            {
                if (addItemDetailsModel != null)
                {
                    if (addItemDetailsModel.Category.ToLower().Equals("select category") && !string.IsNullOrEmpty(addItemDetailsModel.Category))
                    {
                        UserDialogs.Instance.Alert("Please select category.", "", "Ok");
                        return;
                    }
                }
                SelectedCommandperameter = parameter;
                await GetPicketList(parameter, TitleName: "Size");
                return;
            }
            else if (parameter == 8)
            {
                SelectedCommandperameter = parameter;
                await GetPicketList(parameter, TitleName: "Brand");
                return;
            }
            else if (parameter == 9)
            {
                SelectedCommandperameter = parameter;
                await GetPicketList(parameter, TitleName: "Color");
                return;
            }
            else if (parameter == 10)
            {
                SelectedCommandperameter = parameter;
                await GetPicketList(parameter, TitleName: "Condition");
                return;
            }
            else if (parameter == 11)
            {
                SelectedCommandperameter = parameter;
                await GetPicketList(parameter, TitleName: "Availability");
                return;
            }
            else if (parameter == 12)
            {
                SelectedCommandperameter = parameter;
                await GetPicketList(parameter, TitleName: "Listing Price");
                return;
            }
            else if (parameter == 13)
            {
                SelectedCommandperameter = parameter;
                SelectedPropertyIndex = parameter;
                await navigation.PushAsync(new ShippingPrizePage("Shipping Price"));
                return;
            }
            else if (parameter == 14)
            {
                SelectedCommandperameter = parameter;
                await GetPicketList(parameter, TitleName: "Earning");
                return;
            }
        }
        #endregion region

        #region Edit image
        private async void EditImage()
        {
            string imagePath = "";
            if (ImageToBeEdited == "EditingImage1")
            {
                imagePath = productImgList[0].path;
            }
            else if (ImageToBeEdited == "EditingImage2")
            {
                imagePath = productImgList[1].path;
            }
            else if (ImageToBeEdited == "EditingImage3")
            {
                imagePath = tagImgList[0].OrignalImagePath;
            }

            await ImageCropper.Current.Crop(new CropSettings()
            {
                CropShape = CropSettings.CropShapeType.Rectangle,
                PageTitle = "EDIT IMAGE",
                //AspectRatioX = 16,
                //AspectRatioY = 16,
            }, imagePath).ContinueWith(t =>
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
                    var result = t.Result;
                    var imgSource = ImageSource.FromStream(() => new MemoryStream(File.ReadAllBytes(t.Result)));
                    Global.ImageToBeDelete = File.ReadAllBytes(t.Result);
                    //MessagingCenter.Send<object, byte[]>(this, "IsImgAdd", File.ReadAllBytes(t.Result));
                    //MessagingCenter.Send<object, string>("IsImgAdd", "IsImgAdd", t.Result);
                    if (IsUploadTagImage)
                    {
                        var arg = ImageSource.FromStream(() => new MemoryStream(File.ReadAllBytes(t.Result)));
                        if (tagImageList.Count > 0 && ImageToBeEdited == "EditingImage3")
                        {
                            tagImageList.RemoveAt(0);
                            tagImgList.RemoveAt(0);
                            tagImageList.Insert(0, arg);
                            tagImgList.Insert(0, new TagImage()
                            {
                                OrignalImagePath = imagePath,
                                Image = arg.ConvertImagesourceToBase64(),
                                Extension = Global.GetFileExtentionUsingURL(t.Result),
                                ImagePath = t.Result,
                                imageBytes = File.ReadAllBytes(t.Result)
                            });
                        }
                        else
                        {
                            tagImgList.Add(new TagImage()
                            {
                                OrignalImagePath = imagePath,
                                Image = arg.ConvertImagesourceToBase64(),
                                Extension = Global.GetFileExtentionUsingURL(t.Result),
                                ImagePath = t.Result,
                                imageBytes = File.ReadAllBytes(t.Result)
                            });
                            tagImageList.Add(arg);
                        }
                    }
                    MessagingCenter.Send<object, string>("IsImgAdd", "IsImgAdd", t.Result);
                    IsUploadTagImage = false;

                }
            });
        }
        #endregion

        #region Upload image from camera
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
                    PhotoSize = PhotoSize.Medium,
                    DefaultCamera = CameraDevice.Rear,
                    AllowCropping = false
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
                UserImage = ImageSource.FromStream(() => imgStream);
                Global.UserImage = UserImage;
                await PopupNavigation.Instance.PopAsync();
                await ImageCropper.Current.Crop(new CropSettings()
                {
                    CropShape = CropSettings.CropShapeType.Rectangle,
                    //AspectRatioX = 16,
                    //AspectRatioY = 16,

                }, file.Path).ContinueWith(async t =>
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
                        var result = t.Result;
                        var imgSource = ImageSource.FromStream(() => new MemoryStream(File.ReadAllBytes(t.Result)));
                        Global.ImageToBeDelete = File.ReadAllBytes(t.Result);
                        //MessagingCenter.Send<object, string>("IsImgAdd", "IsImgAdd", t.Result);
                        if (!IsUploadTagImage)
                        {
                            productImgList.Add(new ProductSelectImage()
                            {
                                image = imgSource,
                                path = file.Path,
                                imageBytes = Global.ImageToBeDelete,
                                OrgimageBytes = Global.ImageToBeDelete
                            });

                        }
                        else if (IsUploadTagImage)
                        {
                            var arg = ImageSource.FromStream(() => new MemoryStream(File.ReadAllBytes(t.Result)));
                            if (tagImageList.Count > 0 && ImageToBeEdited == "EditingImage3")
                            {
                                tagImageList.RemoveAt(0);
                                tagImgList.RemoveAt(0);
                                tagImageList.Insert(0, arg);
                                tagImgList.Insert(0, new TagImage()
                                {
                                    OrignalImagePath = file.Path,
                                    Image = arg.ConvertImagesourceToBase64(),
                                    Extension = Global.GetFileExtentionUsingURL(t.Result),
                                    ImagePath = t.Result
                                });
                            }
                            else
                            {
                                tagImgList.Add(new TagImage()
                                {
                                    OrignalImagePath = file.Path,
                                    Image = arg.ConvertImagesourceToBase64(),
                                    Extension = Global.GetFileExtentionUsingURL(t.Result),
                                    ImagePath = t.Result
                                });
                                tagImageList.Add(arg);
                            }

                        }
                        MessagingCenter.Send<object, string>("IsImgAdd", "IsImgAdd", t.Result);
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
                //GC.Collect();
                UserDialogs.Instance.HideLoading();
            }
        }
        #endregion

        #region Upload image from gallery
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

                #region To select tag image
                if (IsUploadTagImage)
                {
                    var fileTag = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                    {
                        PhotoSize = PhotoSize.Medium,
                        RotateImage = false
                    });
                    var stream = fileTag.GetStream();
                    //if (fileTag != null && Device.RuntimePlatform == Device.iOS)
                    //{
                    //    stream = fileTag.GetStreamWithImageRotatedForExternalStorage();
                    //    stream.Seek(0, SeekOrigin.Current);
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
                        PageTitle = "EDIT IMAGE",
                    }, fileTag.Path).ContinueWith(async t =>
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
                            var result = t.Result;
                            var imgSource = ImageSource.FromStream(() => new MemoryStream(File.ReadAllBytes(t.Result)));
                            Global.ImageToBeDelete = File.ReadAllBytes(t.Result);
                            if (!IsUploadTagImage)
                            {
                                productImgList.Add(new ProductSelectImage()
                                {
                                    image = imgSource,
                                    path = fileTag.Path,
                                    imageBytes = Global.ImageToBeDelete,
                                    OrgimageBytes = Global.ImageToBeDelete
                                });

                            }
                            else if (IsUploadTagImage)
                            {
                                var arg = ImageSource.FromStream(() => new MemoryStream(File.ReadAllBytes(t.Result)));
                                if (tagImageList.Count > 0 && ImageToBeEdited == "EditingImage3")
                                {
                                    tagImageList.RemoveAt(0);
                                    tagImgList.RemoveAt(0);
                                    tagImageList.Insert(0, arg);
                                    tagImgList.Insert(0, new TagImage()
                                    {
                                        OrignalImagePath = fileTag.Path,
                                        Image = arg.ConvertImagesourceToBase64(),
                                        Extension = Global.GetFileExtentionUsingURL(t.Result),
                                        ImagePath = t.Result
                                    });
                                }
                                else
                                {
                                    tagImgList.Add(new TagImage()
                                    {
                                        OrignalImagePath = fileTag.Path,
                                        Image = arg.ConvertImagesourceToBase64(),
                                        Extension = Global.GetFileExtentionUsingURL(t.Result),
                                        ImagePath = t.Result
                                    });
                                    tagImageList.Add(arg);
                                }

                            }
                            MessagingCenter.Send<object, string>("IsImgAdd", "IsImgAdd", t.Result);
                        }
                    });

                    return;
                }
                #endregion

                #region To select the multiple images
                var file = await Plugin.Media.CrossMedia.Current.PickPhotosAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = PhotoSize.Medium,
                    RotateImage = false
                });
                await PopupNavigation.Instance.PopAsync();
                //await PopupNavigation.Instance.PopAllAsync();
                if (file.Count > 1)
                {
                    foreach (var f in file)
                    {
                        if (productImgList.Count < 12)
                        {
                            //var stream = f.GetStream();
                            //if (f != null && Device.RuntimePlatform == Device.iOS)
                            //{
                            //    stream = f.GetStreamWithImageRotatedForExternalStorage();
                            //    stream.Seek(0, SeekOrigin.Current);
                            //}
                            //ImageSource source = ImageSource.FromStream(() =>
                            //{
                            //    return f.GetStreamWithImageRotatedForExternalStorage();
                            //});
                            productImgList.Add(new ProductSelectImage()
                            {
                                image = ImageSource.FromStream(() => f.GetStream()),
                                path = f.Path,
                                imageBytes = File.ReadAllBytes(f.Path),
                                OrgimageBytes = File.ReadAllBytes(f.Path),
                            });
                            MessagingCenter.Send<object, string>("IsImgAdd", "IsImgAdd", f.Path);
                        }
                        else
                        {
                            Acr.UserDialogs.UserDialogs.Instance.Alert("", "You can't add more than 12 images", "OK");
                            return;
                        }
                    }
                }
                else if(file.Count>0)
                {
                    var f = file[0];
                    var stream = f.GetStream();
                    //if (f != null && Device.RuntimePlatform == Device.iOS)
                    //{
                    //    stream = f.GetStreamWithImageRotatedForExternalStorage();
                    //    stream.Seek(0, SeekOrigin.Current);
                    //}
                    byte[] imageData;
                    MemoryStream ms = new MemoryStream();
                    stream.CopyTo(ms);
                    imageData = ms.ToArray();
                    Global.BuySellimageByte = imageData;
                    var imgStream = new MemoryStream(imageData);
                    UserImage = ImageSource.FromStream(() => imgStream);
                    Global.UserImage = UserImage;
                    //await PopupNavigation.Instance.PopAsync();
                    await ImageCropper.Current.Crop(new CropSettings()
                    {
                        CropShape = CropSettings.CropShapeType.Rectangle,
                        PageTitle = "EDIT IMAGE",
                        //AspectRatioX = 16,
                        //AspectRatioY = 16,
                    }, f.Path).ContinueWith(async t =>
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
                            var result = t.Result;
                            var imgSource = ImageSource.FromStream(() => new MemoryStream(File.ReadAllBytes(t.Result)));
                            Global.ImageToBeDelete = File.ReadAllBytes(t.Result);
                            if (!IsUploadTagImage)
                            {
                                productImgList.Add(new ProductSelectImage()
                                {
                                    image = imgSource,
                                    path = f.Path,
                                    imageBytes = Global.ImageToBeDelete,
                                    OrgimageBytes = Global.ImageToBeDelete
                                });

                            }
                            else if (IsUploadTagImage)
                            {
                                var arg = ImageSource.FromStream(() => new MemoryStream(File.ReadAllBytes(t.Result)));
                                if (tagImageList.Count > 0 && ImageToBeEdited == "EditingImage3")
                                {
                                    tagImageList.RemoveAt(0);
                                    tagImgList.RemoveAt(0);
                                    tagImageList.Insert(0, arg);
                                    tagImgList.Insert(0, new TagImage()
                                    {
                                        OrignalImagePath = f.Path,
                                        Image = arg.ConvertImagesourceToBase64(),
                                        Extension = Global.GetFileExtentionUsingURL(t.Result),
                                        ImagePath = t.Result
                                    });
                                }
                                else
                                {
                                    tagImgList.Add(new TagImage()
                                    {
                                        OrignalImagePath = f.Path,
                                        Image = arg.ConvertImagesourceToBase64(),
                                        Extension = Global.GetFileExtentionUsingURL(t.Result),
                                        ImagePath = t.Result
                                    });
                                    tagImageList.Add(arg);
                                }

                            }
                            MessagingCenter.Send<object, string>("IsImgAdd", "IsImgAdd", t.Result);
                        }
                    });
                }

                #endregion
                UserDialogs.Instance.HideLoading();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                UserDialogs.Instance.HideLoading();
            }
            finally
            {
                //GC.Collect();
                UserDialogs.Instance.HideLoading();
            }
        }
        #endregion

        #region Set selected values
        public void SetSelectedValues(string value, int index)
        {
            try
            {
                if (index == 1)
                {
                    addItemDetailsModel.HeadLine = value;
                }
                else if (index == 2)
                {
                    addItemDetailsModel.Description = value;
                }
                else if (index == 3)
                {
                    addItemDetailsModel.Category = value;
                }
                else if (index == 6)
                {
                    addItemDetailsModel.Quantity = value;
                    if (value.ToString().ToLower().Equals("one"))
                    {
                        IsShowMultQuantity = false;
                        IsShowOneQuantity = true;
                    }
                    else
                    {
                        IsShowMultQuantity = true;
                        IsShowOneQuantity = false;
                    }
                }
                else if (index == 7)
                {
                    if (value.ToLower().Equals("custom"))
                    {
                        IsShowOneQuantity = false;
                        IsShowCustomSize = true;
                        addItemDetailsModel.Size = string.Empty;
                    }
                    else
                    {
                        addItemDetailsModel.Size = value;
                        IsShowOneQuantity = true;
                        IsShowCustomSize = false;
                    }
                }
                else if (index == 8)
                {
                    addItemDetailsModel.Brand = value;
                }
                else if (index == 9)
                {
                    addItemDetailsModel.ProdColor = value;
                }
                else if (index == 10)
                {
                    addItemDetailsModel.Condition = value;
                }
                else if (index == 11)
                {
                    addItemDetailsModel.Availability = value;
                }
                else if (index == 12)
                {
                    addItemDetailsModel.Price = value;
                }
                else if (index == 13)
                {
                    // To remove text from shipping price
                    //addItemDetailsModel.ShipPrice = value.Remove(2);
                    if(value == "$8(Under 1 lb)")
                    {
                        addItemDetailsModel.ShipPrice = "$8";
                    }
                    else if(value == "$11(Under 3 lb)")
                    {
                        addItemDetailsModel.ShipPrice = "$11";
                    }
                    else if(value == "$14(Under 5 lb)")
                    {
                        addItemDetailsModel.ShipPrice = "$14";
                    }
                    //addItemDetailsModel.ShipPrice = value;
                    var price = Convert.ToDecimal(addItemDetailsModel.Price.Replace("$", ""));
                    decimal twentyPercent = 0;
                    decimal earning = 0;
                    var shippingCost = string.Empty;
                    string shippingText = "(Shipping)";

                    //Condition added to calculate user's earnings at add listing
                    if (!string.IsNullOrEmpty(addItemDetailsModel.Price))
                    {
                        if (addItemDetailsModel.ShipPrice == "$8")
                        {
                            twentyPercent = price * 20 / 100;
                            //earning = price - (twentyPercent + 8);
                            earning = price - (twentyPercent);
                            shippingCost = "$8";
                        }
                        else if (addItemDetailsModel.ShipPrice == "$11")
                        {
                            twentyPercent = price * 20 / 100;
                            //earning = price - (twentyPercent + 11);
                            earning = price - (twentyPercent);
                            shippingCost = "$11";
                        }
                        else
                        {
                            twentyPercent = price * 20 / 100;
                            //earning = price - (twentyPercent + 14);
                            earning = price - (twentyPercent);
                            shippingCost = "$14";
                        }
                    }
                    //addItemDetailsModel.Earning = "$" + price + " - " + "20%" + " (" + "$" + Convert.ToString(twentyPercent) + ") " + "- " + shippingCost + shippingText + " = " + "$" + Convert.ToString(earning);
                    addItemDetailsModel.Earning = "$" + Convert.ToString(earning);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }
        #endregion

        #region Save product
        public async Task SaveProduct(DashBoardModel dashBoardModel)
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

                var productImage = dashBoardModel.otherImages[0].ConvertImagesourceToBase64();
                int storeID = 1;
                if (dashBoardModel.StoreType.ToLower().Equals(Constant.ClothingStr.ToLower()))
                {
                    storeID = 1;
                }
                else if (dashBoardModel.StoreType.ToLower().Equals(Constant.SneakersStr.ToLower()))
                {
                    storeID = 2;
                }
                else if (dashBoardModel.StoreType.ToLower().Equals(Constant.StreetwearStr.ToLower()))
                {
                    storeID = 3;
                }
                else if (dashBoardModel.StoreType.ToLower().Equals(Constant.VintageStr.ToLower()))
                {
                    storeID = 4;
                }

                List<ProductImage> listImgSource = new List<ProductImage>();
                int index = 0;
                foreach (var objImg in dashBoardModel.otherImages)
                {
                    listImgSource.Add(new ProductImage
                    {
                        Image = objImg.ConvertImagesourceToBase64(),
                        Extension = ProimgList[index].Extension
                    });
                    index++;
                }

                var proCovImg = new ProductCoverImage();
                proCovImg.Image = dashBoardModel.otherImages[0].ConvertImagesourceToBase64();
                proCovImg.Extension = ProimgList[0].Extension;

                var addItemRM = new AddItemRequestModel();
                addItemRM.Brand = dashBoardModel.Brand != null ? dashBoardModel.Brand : "NA";
                addItemRM.CategoryName = dashBoardModel.ProductCategory;
                addItemRM.Description = dashBoardModel.Description;
                addItemRM.CategoryName = dashBoardModel.Gender;
                addItemRM.Price = Convert.ToDouble(dashBoardModel.Price.Replace("$", ""));
                addItemRM.ProductColor = dashBoardModel.ProductColor;
                addItemRM.ProductCondition = dashBoardModel.ProductCondition;
                addItemRM.ProductImages = listImgSource;
                addItemRM.ProductName = dashBoardModel.ProductName;
                addItemRM.ProductRating = dashBoardModel.ProductRating;
                addItemRM.Size = dashBoardModel.Size;
                addItemRM.Availability = dashBoardModel.Availability;
                addItemRM.Quantity = addItemDetailsModel.Quantity;
                addItemRM.StoreId = storeID;
                addItemRM.RootCategoryName = dashBoardModel.CategorySubRoot.Root == null ? string.Empty : (dashBoardModel.CategorySubRoot.Root.ToLower() == "men" ? "M" : "F");
                addItemRM.ParentCategoryName = dashBoardModel.CategorySubRoot.NodeTitle == null ? String.Empty : dashBoardModel.CategorySubRoot.NodeTitle;
                addItemRM.UserId = Constant.LoginUserData.Id;
                addItemRM.CategoryName = dashBoardModel.CategorySubRoot.SubRoot == null ? String.Empty : dashBoardModel.CategorySubRoot.SubRoot;
                addItemRM.ShippingPrice = Convert.ToDouble(dashBoardModel.ShippingPrice.Replace("$", ""));
                addItemRM.TagImage = tagImgList.Count > 0 ? tagImgList[0] : null;
                addItemRM.Source = Device.RuntimePlatform == Device.iOS ? "iOS" : "Android";
                string methodUrl = "/api/Product/SaveProduct";
                RestResponseModel responseModel = await WebService.PostData(addItemRM, methodUrl, true, 300);
                if (responseModel != null)
                {
                    if (responseModel.status_code == 200)
                    {
                        AddItemResponseModel loginResponse = JsonConvert.DeserializeObject<AddItemResponseModel>(responseModel.response_body);
                        if (loginResponse != null)
                        {
                            if (loginResponse.StatusCode == 200)
                            {
                                Global.postProductList.Add(dashBoardModel);
                                Global.AlertWithAction("Item details added successfully!!", () =>
                                {
                                    Global.IsUploadedProduct = true;
                                    Global.SetThemeColor(storeID);
                                    Global.GenderIndex = dashBoardModel.CategorySubRoot.Root == null ? 1 : (dashBoardModel.CategorySubRoot.Root.ToLower() == "men" ? 1 : 2);
                                    Global.GenderParam = dashBoardModel.CategorySubRoot.Root == null ? "men" : (dashBoardModel.CategorySubRoot.Root.ToLower() == "men" ? "men" : "women");
                                    Global.ResetMessagingCenter();
                                    App.Current.MainPage = new NavigationPage(new DashBoardView(false));
                                });
                                //var alertConfig = new AlertConfig
                                //{
                                //    Message = "Item details added successfully!!",
                                //    OkText = "OK",
                                //    OnAction = () =>
                                //    {
                                //        Global.IsUploadedProduct = true;
                                //        Global.SetThemeColor(storeID);
                                //        Global.GenderIndex = dashBoardModel.CategorySubRoot.Root == null ? 1 : (dashBoardModel.CategorySubRoot.Root.ToLower() == "men" ? 1 : 2);
                                //        Global.GenderParam = dashBoardModel.CategorySubRoot.Root == null ? "men" : (dashBoardModel.CategorySubRoot.Root.ToLower() == "men" ? "men" : "women");
                                //        Global.ResetMessagingCenter();
                                //        App.Current.MainPage = new NavigationPage(new DashBoardView(false));
                                //    }
                                //};
                                //Acr.UserDialogs.UserDialogs.Instance.Alert(alertConfig);
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
                        if (!string.IsNullOrEmpty(responseModel.ErrorMessage))
                        {
                            IsTap = false;
                            Acr.UserDialogs.UserDialogs.Instance.Alert(responseModel.ErrorMessage);
                            UserDialogs.Instance.HideLoading();
                        }
                        else
                        {
                            IsTap = false;
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
                        IsTap = false;
                        Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                        UserDialogs.Instance.HideLoading();
                    }
                }
                else
                {
                    IsTap = false;
                    Acr.UserDialogs.UserDialogs.Instance.Alert("Unable to add item details");
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

        #region Post method
        private async void PostMethod()
        {
            try
            {
                if (string.IsNullOrEmpty(Global.Storecategory))
                {
                    await _messageService.ShowAsync("Please select Store first");
                    return;
                }
                else if (imageList.Count == 0)
                {
                    IsTap = false;
                    await _messageService.ShowAsync("Please upload image first");
                    return;
                }
                else if (tagImgList.Count == 0)
                {
                    IsTap = false;
                    await _messageService.ShowAsync("Please select tag image");
                    return;
                }
                else if (string.IsNullOrEmpty(addItemDetailsModel.HeadLine))
                {
                    IsTap = false;
                    await _messageService.ShowAsync("Please fill headline first");
                    return;
                }
                else if (string.IsNullOrEmpty(addItemDetailsModel.Description))
                {
                    IsTap = false;
                    await _messageService.ShowAsync("Please fill description first");
                    return;
                }
                else if (addItemDetailsModel.Category == "Select Category")
                {
                    IsTap = false;
                    await _messageService.ShowAsync("Please select category first");
                    return;
                }
                else if (addItemDetailsModel.Size.Contains("Select") || string.IsNullOrEmpty(addItemDetailsModel.Size))
                {
                    IsTap = false;
                    await _messageService.ShowAsync("Please select size first");
                    return;
                }
                else if (string.IsNullOrEmpty(addItemDetailsModel.Price.Replace("$", "")))
                {
                    IsTap = false;
                    await _messageService.ShowAsync("Please fill price first");
                    return;
                }
                else if (Convert.ToDecimal(addItemDetailsModel.Price.Replace("$", "")) < 10)
                {
                    IsTap = false;
                    await _messageService.ShowAsync("Minimum Listing Price is $10");
                    return;
                }

                DashBoardModel dashBoardModel = new DashBoardModel();
                dashBoardModel.ProductName = addItemDetailsModel.HeadLine;
                dashBoardModel.Description = addItemDetailsModel.Description;
                if (selectedCategory != null)
                {
                    if (selectedCategory.SubRoot != null)
                    {
                        dashBoardModel.ProductCategory = selectedCategory.SubRoot;
                    }
                    else  
                    {
                        dashBoardModel.ProductCategory = selectedCategory.NodeTitle;
                    }
                }
                else
                {
                    dashBoardModel.ProductCategory = selectedCategory.NodeTitle;
                }

                dashBoardModel.Gender = selectedCategory.Gender;
                dashBoardModel.Earning = addItemDetailsModel.Earning;
                dashBoardModel.Size = addItemDetailsModel.Size;
                dashBoardModel.SingleQty = addItemDetailsModel.Quantity;
                dashBoardModel.Quantity = addItemDetailsModel.Quantity;
                dashBoardModel.otherImages = imageList;
                dashBoardModel.ProductCoverImage = imageList[0].ConvertImagesourceToBase64();
                //dashBoardModel.ProductRating = "UnfillHeart";
                dashBoardModel.ProductColor = addItemDetailsModel.ProdColor;
                dashBoardModel.ProductCondition = addItemDetailsModel.Condition;
                dashBoardModel.quantityModels = QuantityModelList.ToList();
                dashBoardModel.StoreCategory = Global.Storecategory;
                dashBoardModel.StoreType = Global.Storecategory;
                dashBoardModel.Price = addItemDetailsModel.Price;
                dashBoardModel.ShippingPrice = addItemDetailsModel.ShipPrice;
                dashBoardModel.Brand = addItemDetailsModel.Brand != null ? addItemDetailsModel.Brand : "NA";
                dashBoardModel.Availability = addItemDetailsModel.Availability;
                dashBoardModel.CategorySubRoot = subRoots;
                if (Global.postProductList.Count > 0)
                {
                    var maxList = Global.postProductList.Max(x => x.Id);
                    dashBoardModel.Id = maxList + 1;
                }
                else
                {
                    dashBoardModel.Id = 1;
                }
                Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Adding item details, Please wait...");
                await Task.Delay(500);
                await SaveProduct(dashBoardModel);
            }
            catch (Exception ex)
            {
                IsTap = false;
                Debug.WriteLine(ex.Message);
            }
        }
        #endregion

        private void EditImg1Method(object obj)
        {
            Editimageclicked("EditingImage1");
        }

        private void EditImg2Method(object obj)
        {
            Editimageclicked("EditingImage2");
        }

        private void EditTagImage(object obj)
        {

            Editimageclicked("EditingImage3");
        }

        private async void ViewMoreImgMethod()
        {
            //List<ProductSelectImage> reorderImgListModels = new List<ProductSelectImage>();
            //foreach(var img in ImgList)
            //{
            //    var reorderImgModel = new ProductSelectImage();
            //    reorderImgModel.image = img;
            //    reorderImgListModels.Add(reorderImgModel);
            //}
            try
            {
                if (IsTap)
                    return;
                IsTap = true;

                var popupdefault = new ViewMoreImgPopup(productImgList);
                popupdefault.eventClose += Popupdefault_eventClose;
                await PopupNavigation.Instance.PushAsync(popupdefault);
            }
            catch (Exception ex)
            {
                IsTap = false;
                Debug.WriteLine(ex.Message);
            }
        }

        private void Popupdefault_eventClose(object sender, List<ProductSelectImage> e)
        {
            IsTap = false;
            ImgList.Clear();
            imageList.Clear();
            //imageList.Concat(e);
            imageList = e.Select(p => p.image).ToList();//ImgList.Concat(e).ToList();
            ImgList = e.Select(p => p.image).ToList();//ImgList.Concat(e).ToList();
            productImgList = e;
            MessagingCenter.Send<object, List<ImageSource>>("IsReorder", "IsReorder", ImgList);
        }

        private async void BrandDropDownMethod()
        {
            try
            {

                SelectedCommandperameter = 8;
                await GetPicketList(8, TitleName: "Brand");
            }
            catch (Exception ex)
            {

            }
        }

        public async Task GetPicketList(int index, string TitleName)
        {
            try
            {
                if (IsTap)
                    return;
                IsTap = true;

                SelectedPropertyIndex = index;
                SelectPickerList = MockProductData.Instance.GetAddItemOtherFieldData(index, subRoots);

                var productPropertyView = new ProductPropertyListView(TitleName);
                productPropertyView.BindingContext = this;
                await navigation.PushAsync(productPropertyView);
            }
            catch (Exception ex)
            {
                IsTap = false;
                Debug.WriteLine(ex.Message);
            }
        }

        //Method created to select more quantity 
        private async void SelectMoreQuantityMethod(object obj)
        {
            IsMultiSizeSelected = false;
            IsSelectMultiplQty = true;
            var parameter = Convert.ToInt32(Convert.ToString(obj));
            await GetPicketList(4, "Quantity");
            currentMultiQuantityIndex = parameter;
            return;
        }
        //Method created to select more size 
        private async void SelectMoreSizeMethod(object obj)
        {
            IsMultiSizeSelected = true;
            IsSelectMultiplQty = true;
            var parameter = Convert.ToInt32(Convert.ToString(obj));
            await GetPicketList(7, "Size");
            currentMultiQuantityIndex = parameter;
            return;
        }

        private void CategoryClickedmethod()
        {
            try
            {
                if (IsTap)
                    return;
                IsTap = true;
                navigation.PushAsync(new CategoryPage());
            }
            catch (Exception ex)
            {
                IsTap = false;
                Debug.WriteLine(ex.Message);
            }
        }

        public async void Editimageclicked(string SelectedImagename)
        {
            try
            {
                if (IsTap)
                    return;
                IsTap = true;
                ImgEdit = SelectedImagename;
                var pupupdefault = new ImageDeleteEditPopup();
                await PopupNavigation.Instance.PushAsync(pupupdefault);
                pupupdefault.eventDelete += Pupupdefault_eventDelete;
                pupupdefault.eventEdit += Pupupdefault_eventEdit;
                Device.StartTimer(TimeSpan.FromMilliseconds(500), () =>
                {
                    IsTap = false;
                    return false;
                });
            }
            catch (Exception ex)
            {
                IsTap = false;
            }
        }

        private async void Pupupdefault_eventEdit(object sender, EventArgs e)
        {
            try
            {
                if (IsTap)
                    return;
                IsTap = true;

                await PopupNavigation.Instance.PopAsync();
                ImageToBeEdited = ImgEdit;
                //UploadImageClickedMethod();
                EditImage();
                Device.StartTimer(TimeSpan.FromMilliseconds(500), () =>
                {
                    IsTap = false;
                    return false;
                });
            }
            catch (Exception ex)
            {
                IsTap = true;
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
                ImageToBeEdited = ImgEdit;
                MessagingCenter.Send<object, byte[]>(this, "ImageToBeDelete", Global.ImageToBeDelete);
                Device.StartTimer(TimeSpan.FromMilliseconds(500), () =>
                {
                    IsTap = false;
                    return false;
                });
            }
            catch (Exception ex)
            {
                IsTap = true;
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
                Device.BeginInvokeOnMainThread(() =>
                {
                    UploadImage_Gallery();
                });
            }
            catch (Exception ex)
            {

            }
        }

        private void Popupdefault_eventCamera(object sender, EventArgs e)
        {
            try
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    UploadImage_Camera();
                });
            }
            catch (Exception ex)
            {

            }
        }

        public void ResetMessagingCenter()
        {
            MessagingCenter.Unsubscribe<object, string>("SelectPropertyValue", "SelectPropertyValue");
            MessagingCenter.Unsubscribe<object, byte[]>("IsImgAdd", "IsImgAdd");
            MessagingCenter.Unsubscribe<object, byte[]>("ImageToBeDelete", "ImageToBeDelete");
            MessagingCenter.Unsubscribe<object, List<ImageSource>>("IsReorder", "IsReorder");
            MessagingCenter.Unsubscribe<object, SubRoots>("SelectedGenderCat", "SelectedGenderCat");
            MessagingCenter.Unsubscribe<object, SelectSubRootCategory>("SelGenderCatSneakers", "SelGenderCatSneakers");
            MessagingCenter.Unsubscribe<object, SubRoots>("SelectedGenderCatFilter", "SelectedGenderCatFilter");
            MessagingCenter.Unsubscribe<object, SelectSubRootCategory>("SelGenderCatSneakersFilter", "SelGenderCatSneakersFilter");
        }

        public async void GetBrandList()
        {
            try
            {
                var brandJson = await Global.GetBrandJson();
                var productListResponses = JsonConvert.DeserializeObject<List<BrandModel>>(brandJson);
                MasterBrandList = new ObservableCollection<BrandModel>(productListResponses);
            }
            catch (Exception ex)
            {

            }
        }

        public async void GetSearchBrandList(string search)
        {
            try
            {
                BrandList = new ObservableCollection<string>(MasterBrandList.Where(b => b.BrandName.ToLower().Contains(search.ToLower())).Select(s => s.BrandName).ToList());
                if(BrandList.Count > 0)
                {
                    IsBrandListShow = true;
                }
                else
                {
                    IsBrandListShow = false;
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
    }
}