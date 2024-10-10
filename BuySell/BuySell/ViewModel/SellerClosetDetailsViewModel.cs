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
using FFImageLoading.Work;
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
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using static BuySell.Model.CategoryModel;
using ImageSource = Xamarin.Forms.ImageSource;

namespace BuySell.ViewModel
{
    public class SellerClosetDetailsViewModel : BaseViewModel
    {
        public int addMoreCount = 0;
        public int currentMultiQuantityIndex = 0;
        public bool IsMultiSizeSelected = false;
        public bool IsUploadTagImage = false;
        //int SelectedStoreIndex = 1;
        int GenderIndex = Global.GenderIndex = 1;
        public SubRoots subRoots;
        IDisposable disposable;
        int SelectedCommandperameter = 0;
        public List<ImageSource> imageList = new List<ImageSource>();
        public List<ImageSource> tagImageList = new List<ImageSource>();
        public List<ProductSelectImage> productImgList = new List<ProductSelectImage>();
        public List<TagImage> tagImgList = new List<TagImage>();
        public SubRoots selectedCategory;
        string fileExt = "";
        private readonly Services.IMessageService _messageService;
        private BuySell.Views.SellerClosetDetailsView _sellerClosetDetailsView;
        public Model.DashBoardModel productDashBoardModel;
        public bool IsSelected3level = false;
        public bool IsFirstLoad = false;
        #region Constructor
        public SellerClosetDetailsViewModel(Model.DashBoardModel obj, INavigation _nav, BuySell.Views.SellerClosetDetailsView sellerClosetDetailsView)
        {
            navigation = _nav;
            productDashBoardModel = obj;
            GetViewOffersList();
            if (obj != null)
            {
                //Device.InvokeOnMainThreadAsync(() =>
                //{
                    GetEditMethod(obj);
                //});
            }
            _sellerClosetDetailsView = sellerClosetDetailsView;
            this._messageService = DependencyService.Get<Services.IMessageService>();
            MessagingCenter.Subscribe<object, string>("SelectPropertyValue", "SelectPropertyValue", (sender, arg) =>
            {
                if (arg != null)
                {
                    if (!IsSelectMultiplQty)
                    {
                        if (SelectedPropertyIndex == 0)
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
            IsFirstLoad = true;
            GetBrandList();
        }
        #endregion

        #region Properties
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

        private DashBoardModel _ProdataModel;
        public DashBoardModel ProdataModel
        {
            get { return _ProdataModel; }
            set { _ProdataModel = value; OnPropertyChanged("ProdataModel"); }
        }

        private ObservableCollection<ViewOffersModel> _ViewOffersList = new ObservableCollection<ViewOffersModel>();
        public ObservableCollection<ViewOffersModel> ViewOffersList
        {
            get { return _ViewOffersList; }
            set { _ViewOffersList = value; OnPropertyChanged("ViewOffersList"); }
        }
        public ImageSource UserImage { get; set; }

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

        private string _OfferCount = Global.OfferProductList != null ? "Offer(" + Convert.ToString(Global.OfferProductList.Count) + ")" : "Offer(0)";
        public string OfferCount
        {
            get { return _OfferCount; }
            set { _OfferCount = value; OnPropertyChanged("OfferCount"); }
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

        private Command _TopTrandingCommand;
        public Command TopTrandingCommand
        {
            get
            {
                return _TopTrandingCommand ?? (_TopTrandingCommand = new Command<ViewOffersModel>(async (obj) => await ExicuteTopTrandingCommond(obj)));
            }
        }

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
                            MessagingCenter.Unsubscribe<object, SubRoots>("SelectedGenderCat", "SelectedGenderCat");
                            MessagingCenter.Unsubscribe<object, string>("SelectPropertyValue", "SelectPropertyValue");
                            await navigation.PopAsync();
                            //App.Current.MainPage = new NavigationPage(new SellerClosetView());
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

        private Command _DeleteProductCommand;
        public Command DeleteProductCommand
        {
            get
            {
                return _DeleteProductCommand ?? (_DeleteProductCommand = new Command<DashBoardModel>(async (obj) => await ExecuteDeleteProductCommand(obj)));
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
                        _UploadImageClicked = new Command(async () =>
                        {

                            if (productImgList.Count >= 12)
                            {
                                await Task.Delay(100);
                                Acr.UserDialogs.UserDialogs.Instance.Alert("", "You can't add more than 12 images", "OK");
                                return;
                            }
                            UploadImageClickedMethod();
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
                    _UploadTagImageClicked = new Command(async () =>
                    {
                        try
                        {
                            //if (IsTap)
                            //    return;
                            //IsTap = true;
                            if (tagImageList.Count > 0)
                            {
                                return;
                            }
                            else
                            {
                                IsUploadTagImage = true;
                                ImageToBeEdited = "null";
                                UploadImageClickedMethod();
                                await Task.Delay(100);
                            }
                            //IsTap = false;
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
                        if (addItemDetailsModel != null)
                            addItemDetailsModel.StoreType = Global.Storecategory;
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

        private Command _SaveCommand;
        public Command SaveCommand
        {
            get
            {
                if (_SaveCommand == null)
                {
                    try
                    {
                        _SaveCommand = new Command(UpdateProduct);
                    }
                    catch (Exception)
                    {

                    }
                }

                return _SaveCommand;
            }
        }

        private Command _DeleteProdCommand;
        public Command DeleteProdCommand
        {
            get
            {
                if (_DeleteProdCommand == null)
                {
                    try
                    {
                        _DeleteProdCommand = new Command(DeleteProductMethod);
                    }
                    catch (Exception)
                    {

                    }
                }

                return _DeleteProdCommand;
            }
        }

        private Command _CopyCommand;
        public Command CopyCommand
        {
            get
            {
                if (_CopyCommand == null)
                {
                    try
                    {
                        _CopyCommand = new Command(CopyProductMethod);
                    }
                    catch (Exception)
                    {

                    }
                }

                return _CopyCommand;
            }
        }

        #endregion

        #region Methods
        private void DeleteProductMethod(object obj)
        {
            var confirmConfig = new ConfirmConfig();
            confirmConfig.UseYesNo();
            confirmConfig.SetTitle("Delete Listing");
            confirmConfig.Message = "Are you sure you want to delete listing?";
            confirmConfig.OnAction = ConfirmExit;
            disposable = UserDialogs.Instance.Confirm(confirmConfig);
        }
        private void ConfirmExit(bool IsConfirmed)
        {
            if (IsConfirmed)
            {
                DeleteProductCommand.Execute(productDashBoardModel);
            }
        }
        private async Task ExecuteDeleteProductCommand(DashBoardModel obj)
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
                Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Loading");

                string methodUrl = "/api/Product/DeleteProduct?id=" + obj.Id;
                RestResponseModel responseModel = await WebService.DeleteData(methodUrl, true);
                if (responseModel != null)
                {
                    if (responseModel.status_code == 200)
                    {
                        //var alertConfig = new AlertConfig
                        //{
                        //    Message = "Delete Listing Successfully!!",
                        //    OkText = "OK",
                        //    OnAction = () =>
                        //    {
                        //        var nav = new NavigationPage(new AccountPage());
                        //        App.Current.MainPage = nav;
                        //    }
                        //};
                        //Acr.UserDialogs.UserDialogs.Instance.Alert(alertConfig);
                        Global.AlertWithAction("Deleted item successfully!!", () =>
                        {
                            Global.ResetMessagingCenterForEdit();
                            var itemDetailPage = navigation.NavigationStack.FirstOrDefault(p => p is ItemDetailsPage);
                            if (itemDetailPage != null)
                            {
                                navigation.RemovePage(itemDetailPage);
                            }
                            Global.IsUploadedProduct = true;
                            navigation.PopAsync();
                        });
                        //var alertConfig = new AlertConfig
                        //{
                        //    Message = "Deleted item successfully!!",
                        //    OkText = "OK",
                        //    OnAction = () =>
                        //    {
                        //        Global.ResetMessagingCenterForEdit();
                        //        var itemDetailPage = navigation.NavigationStack.FirstOrDefault(p => p is ItemDetailsPage);
                        //        if (itemDetailPage != null)
                        //        {
                        //            navigation.RemovePage(itemDetailPage);
                        //        }
                        //        Global.IsUploadedProduct = true;
                        //        navigation.PopAsync();
                        //    }
                        //};
                        //Acr.UserDialogs.UserDialogs.Instance.Alert(alertConfig);
                    }
                    else if (responseModel.status_code == 400)
                    {
                        if (responseModel != null)
                        {
                            IsTap = false;
                            Acr.UserDialogs.UserDialogs.Instance.Alert(responseModel.ErrorMessage);
                            UserDialogs.Instance.HideLoading();
                        }
                        else
                        {
                            IsTap = false;
                            Acr.UserDialogs.UserDialogs.Instance.Alert(responseModel.ErrorMessage);
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
                        Acr.UserDialogs.UserDialogs.Instance.Alert(responseModel.ErrorMessage);
                        UserDialogs.Instance.HideLoading();
                    }
                }
                else
                {
                    IsTap = false;
                    Acr.UserDialogs.UserDialogs.Instance.Alert("Unable to process your request.");
                    UserDialogs.Instance.HideLoading();
                }
            }
            catch (Exception ex)
            {
                IsTap = false;
                Acr.UserDialogs.UserDialogs.Instance.Alert("Unable to process your request.");
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Acr.UserDialogs.UserDialogs.Instance.HideLoading();
            }
        }
        public void GetViewOffersList()
        {
            try
            {
                if (Global.OfferProductList != null)
                {
                    var templist = new ObservableCollection<ViewOffersModel>();
                    foreach (var item in Global.OfferProductList)
                    {
                        var IsResult = templist.Where(x => x.Description == item.ProductName).ToList();
                        if (IsResult.Count == 0)
                        {
                            templist.Add(new ViewOffersModel
                            {
                                Image = item.ProductImage,
                                Description = item.ProductName,
                                DollerValue = item.Price,
                                OfferValue = item.OfferPrice,
                                Brand = item.Brand,
                                Size = item.Size,
                                NextImage = "NextArrow",
                                Seller = Global.Username != null ? Global.Username : "NA",
                            });
                        }
                    }
                    _ViewOffersList = templist;
                    ViewOffersList = _ViewOffersList;
                    if (ViewOffersList.Count > 0)
                    {
                        OfferCount = "Offer(" + Convert.ToString(ViewOffersList.Count) + ")";
                    }
                    else
                    {
                        OfferCount = "Offer(0)";
                    }

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        private async Task ExicuteTopTrandingCommond(ViewOffersModel obj)
        {
            try
            {
                if (IsTap)
                    return;
                IsTap = true;

                var prodcu = Global.GlobalProductList.Where(p => p.Id == obj.ProductID).FirstOrDefault();
                if (prodcu != null)
                {
                    Global.SetRecentViewList(prodcu);
                    await navigation.PushAsync(new ItemDetailsPage(prodcu, false));
                }
                else
                {
                    IsTap = false;
                }
            }
            catch (Exception ex)
            {
                IsTap = false;
                Debug.WriteLine(ex.Message);
            }
        }
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
            var popupdefault = new ViewMoreImgPopup(productImgList);
            popupdefault.eventClose += Popupdefault_eventClose;
            await PopupNavigation.Instance.PushAsync(popupdefault);
        }
        private void Popupdefault_eventClose(object sender, List<ProductSelectImage> e)
        {
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

        //Method created to shoe list of items as per field selected by the user
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
                    await navigation.PushAsync(new CategoryPage());
                    return;
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
        private async void SelectMoreQuantityMethod(object obj)
        {
            IsMultiSizeSelected = false;
            IsSelectMultiplQty = true;
            var parameter = Convert.ToInt32(Convert.ToString(obj));
            await GetPicketList(4, "Quantity");
            currentMultiQuantityIndex = parameter;
            return;
        }
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
                ImgEdit = SelectedImagename;
                var pupupdefault = new ImageDeleteEditPopup();
                await PopupNavigation.Instance.PushAsync(pupupdefault);
                pupupdefault.eventDelete += Pupupdefault_eventDelete;
                pupupdefault.eventEdit += Pupupdefault_eventEdit;
            }
            catch (Exception ex)
            { }
        }
        private async void Pupupdefault_eventEdit(object sender, EventArgs e)
        {
            try
            {
                await PopupNavigation.Instance.PopAsync();
                ImageToBeEdited = ImgEdit;
                //UploadImageClickedMethod();
                EditImage();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        private void Pupupdefault_eventDelete(object sender, EventArgs e)
        {
            try
            {
                ImageToBeEdited = ImgEdit;
                if (ImageToBeEdited.Equals("EditingImage3"))
                {
                    MessagingCenter.Send<object, byte[]>("ImageToBeDelete", "ImageToBeDelete", tagImgList[0].imageBytes);
                }
                else if (ImageToBeEdited.Equals("EditingImage1"))
                {
                    // MessagingCenter.Send<object, byte[]>("ImageToBeDelete", "ImageToBeDelete",Global.ImageToBeDelete);
                    MessagingCenter.Send<object, byte[]>("ImageToBeDelete", "ImageToBeDelete", productImgList[0].imageBytes);
                }
                else if (ImageToBeEdited.Equals("EditingImage2"))
                {
                    // MessagingCenter.Send<object, byte[]>("ImageToBeDelete", "ImageToBeDelete",Global.ImageToBeDelete);
                    MessagingCenter.Send<object, byte[]>("ImageToBeDelete", "ImageToBeDelete", productImgList[1].imageBytes);
                }
            }
            catch (Exception ex)
            {

            }
        }
        private async void EditImage()
        {
            string imagePath = "";
            byte[] imgBytes = null;
            if (ImageToBeEdited == "EditingImage1")
            {
                imagePath = productImgList[0].path;
                imgBytes = productImgList[0].imageBytes;
            }
            else if (ImageToBeEdited == "EditingImage2")
            {
                imagePath = productImgList[1].path;
                imgBytes = productImgList[1].imageBytes;
            }
            else if (ImageToBeEdited == "EditingImage3")
            {
                IsUploadTagImage = true;
                imagePath = tagImgList[0].OrignalImagePath;
                imgBytes = tagImgList[0].OrgimageBytes;
            }
            var path = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/temp.png";
            File.WriteAllBytes(path, imgBytes);

            await ImageCropper.Current.Crop(new CropSettings()
            {
                CropShape = CropSettings.CropShapeType.Rectangle,
                PageTitle = "EDIT IMAGE",
                //AspectRatioX = 16,
                //AspectRatioY = 16,
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
                    var result = t.Result;
                    var imgSource = ImageSource.FromStream(() => new MemoryStream(File.ReadAllBytes(t.Result)));
                    Global.ImageToBeDelete = File.ReadAllBytes(t.Result);
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
                                imageBytes = File.ReadAllBytes(t.Result),
                                OrgimageBytes = imgBytes
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
                                imageBytes = File.ReadAllBytes(t.Result),
                                OrgimageBytes = File.ReadAllBytes(t.Result)
                            });
                            tagImageList.Add(arg);
                        }
                    }
                    MessagingCenter.Send<object, string>("IsImgAdd", "IsImgAdd", t.Result);
                    IsUploadTagImage = false;
                }
            });
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

                }, file.Path).ContinueWith(t =>
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
                                    ImagePath = t.Result,
                                    IsDeleted = true,
                                    imageBytes = File.ReadAllBytes(t.Result),
                                    OrgimageBytes = File.ReadAllBytes(t.Result)
                                });
                            }
                            else
                            {
                                tagImgList.Add(new TagImage()
                                {
                                    OrignalImagePath = file.Path,
                                    Image = arg.ConvertImagesourceToBase64(),
                                    Extension = Global.GetFileExtentionUsingURL(t.Result),
                                    ImagePath = t.Result,
                                    imageBytes = File.ReadAllBytes(t.Result),
                                    OrgimageBytes = File.ReadAllBytes(t.Result)
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

                if (file == null)
                    return;

                await PopupNavigation.Instance.PopAllAsync();
                if (file.Count > 1)
                {
                    foreach (var f in file)
                    {
                        if (productImgList.Count < 12)
                        {
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
                else if (file.Count > 0)
                {
                    var f = file[0];
                    var stream = f.GetStream();
                    //if (f != null && Device.RuntimePlatform == Device.iOS)
                    //{
                    //    stream = f.GetStreamWithImageRotatedForExternalStorage();
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
                    if (value == "$8(Under 1 lb)")
                    {
                        addItemDetailsModel.ShipPrice = "$8";
                    }
                    else if (value == "$11(Under 3 lb)")
                    {
                        addItemDetailsModel.ShipPrice = "$11";
                    }
                    else if (value == "$14(Under 5 lb)")
                    {
                        addItemDetailsModel.ShipPrice = "$14";
                    }
                    var price = Convert.ToDecimal(addItemDetailsModel.Price.Replace("$", ""));
                    decimal twentyPercent = 0;
                    decimal earning = 0;
                    var shippingCost = string.Empty;
                    string shippingText = "(Shipping)";
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
                    DashBoardModel dashboardModel = new DashBoardModel();
                    dashboardModel.Earning = addItemDetailsModel.Earning;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
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
                foreach (var objImg in productImgList)
                {
                    listImgSource.Add(new ProductImage
                    {
                        Image = objImg.image.ConvertImagesourceToBase64(),
                        Extension = ProimgList[index].Extension,
                        IsDeleted = objImg.IsDeleted
                    });
                    index++;
                }

                var proCovImg = new ProductCoverImage();
                proCovImg.Image = dashBoardModel.otherImages[0].ConvertImagesourceToBase64();
                proCovImg.Extension = ProimgList[0].Extension;
                proCovImg.IsDeleted = true;

                var addItemRM = new AddItemRequestModel();
                addItemRM.Id = dashBoardModel.Id;
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
                addItemRM.Earning = addItemDetailsModel.Earning;
                addItemRM.RootCategoryName = dashBoardModel.CategorySubRoot.Gender == null ? string.Empty : (dashBoardModel.CategorySubRoot.Gender.ToLower() == "m" ? "M" : (dashBoardModel.CategorySubRoot.Gender.ToLower() == "men") ? "M" : "F");
                if (dashBoardModel.ParentCategory.ToLower() == Constant.SneakersStr.ToLower())
                {
                    addItemRM.ParentCategoryName = dashBoardModel.ParentCategory;
                }
                else
                {
                    addItemRM.ParentCategoryName = dashBoardModel.CategorySubRoot.NodeTitle == null ? String.Empty : (dashBoardModel.CategorySubRoot.NodeTitle.ToLower() == "m" || dashBoardModel.CategorySubRoot.NodeTitle.ToLower() == "f") ? dashBoardModel.CategorySubRoot.SubRoot : dashBoardModel.CategorySubRoot.NodeTitle;
                }
                addItemRM.UserId = Constant.LoginUserData.Id;

                if (IsSelected3level)
                    addItemRM.CategoryName = dashBoardModel.CategorySubRoot.SubRoot;// == null ? String.Empty : dashBoardModel.ParentCategory==null? dashBoardModel.CategorySubRoot.SubRoot : ((dashBoardModel.ParentCategory.ToLower()=="m"|| dashBoardModel.ParentCategory.ToLower() == "f")?string.Empty:dashBoardModel.CategorySubRoot.SubRoot);
                else
                    addItemRM.CategoryName = "";

                addItemRM.ShippingPrice = Convert.ToDouble(dashBoardModel.ShippingPrice.Replace("$", ""));
                addItemRM.TagImage = tagImgList.Count > 0 ? tagImgList[0] : null;
                addItemRM.Source = Device.RuntimePlatform == Device.iOS ? "iOS" : "Android";
                string methodUrl = "/api/Product/UpdateProduct";
                RestResponseModel responseModel = await WebService.PutData(addItemRM, methodUrl, true, 300);

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
                                Global.AlertWithAction("Item details updated successfully !!", () =>
                                {
                                    Global.IsUploadedProduct = true;
                                    Global.SetThemeColor(storeID);
                                    Global.GenderIndex = dashBoardModel.CategorySubRoot.Root == null ? 1 : (dashBoardModel.CategorySubRoot.Root.ToLower() == "men" ? 1 : 2);
                                    Global.GenderParam = dashBoardModel.CategorySubRoot.Root == null ? "men" : (dashBoardModel.CategorySubRoot.Root.ToLower() == "men" ? "men" : "women");
                                    Global.ResetMessagingCenterForEdit();
                                    var itemDetailPage = navigation.NavigationStack.FirstOrDefault(p => p is ItemDetailsPage);
                                    if (itemDetailPage != null)
                                    {
                                        navigation.RemovePage(itemDetailPage);
                                    }
                                    Global.IsUploadedProduct = true;
                                    navigation.PopAsync();
                                    //App.Current.MainPage = new NavigationPage(new DashBoardView());
                                });
                                //var alertConfig = new AlertConfig
                                //{
                                //    Message = "Item details updated successfully !!",
                                //    OkText = "OK",
                                //    OnAction = () =>
                                //    {
                                //        Global.IsUploadedProduct = true;
                                //        Global.SetThemeColor(storeID);
                                //        Global.GenderIndex = dashBoardModel.CategorySubRoot.Root == null ? 1 : (dashBoardModel.CategorySubRoot.Root.ToLower() == "men" ? 1 : 2);
                                //        Global.GenderParam = dashBoardModel.CategorySubRoot.Root == null ? "men" : (dashBoardModel.CategorySubRoot.Root.ToLower() == "men" ? "men" : "women");
                                //        Global.ResetMessagingCenterForEdit();
                                //        var itemDetailPage = navigation.NavigationStack.FirstOrDefault(p => p is ItemDetailsPage);
                                //        if (itemDetailPage != null)
                                //        {
                                //            navigation.RemovePage(itemDetailPage);
                                //        }
                                //        Global.IsUploadedProduct = true;
                                //        navigation.PopAsync();
                                //        //App.Current.MainPage = new NavigationPage(new DashBoardView());
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
                    Acr.UserDialogs.UserDialogs.Instance.Alert("Unable to update item details");
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
        private async void UpdateProduct()
        {
            try
            {

                if (imageList.Count == 0)
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
                dashBoardModel.Id = productDashBoardModel.Id;
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
                dashBoardModel.ParentCategory = addItemDetailsModel.ParentCategory;
                dashBoardModel.ProductColor = addItemDetailsModel.ProdColor;
                dashBoardModel.ProductCondition = addItemDetailsModel.Condition;
                dashBoardModel.quantityModels = QuantityModelList.ToList();
                dashBoardModel.StoreCategory = addItemDetailsModel.StoreType;
                dashBoardModel.StoreType = addItemDetailsModel.StoreType;
                dashBoardModel.Price = addItemDetailsModel.Price;
                dashBoardModel.ShippingPrice = addItemDetailsModel.ShipPrice;
                dashBoardModel.Brand = addItemDetailsModel.Brand != null ? addItemDetailsModel.Brand : "NA";
                dashBoardModel.Availability = addItemDetailsModel.Availability;
                dashBoardModel.CategorySubRoot = subRoots;
                //if (Global.postProductList.Count > 0)
                //{
                //    var maxList = Global.postProductList.Max(x => x.Id);
                //    dashBoardModel.Id = maxList + 1;
                //}
                //else
                //{
                //    dashBoardModel.Id = 1;
                //}
                Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Updating item details, Please wait....");
                await Task.Delay(500);
                await SaveProduct(dashBoardModel);
            }
            catch (Exception ex)
            {

            }
        }
        private async Task GetEditMethod(DashBoardModel obj)
        {
            try
            {
                Acr.UserDialogs.UserDialogs.Instance.ShowLoading();
                try
                {

                    WebClient client = new WebClient();
                    foreach (var imgurl in obj.otherImages)
                    {
                        var imgURL = new Uri(Convert.ToString(imgurl)).OriginalString.Replace("Uri: ", "");
                        var downloadImgSource = await client.DownloadDataTaskAsync(new Uri(imgURL));
                        var downloadImgsource = ImageSource.FromStream(() =>
                                new MemoryStream(downloadImgSource)
                                );
                        imageList.Add(downloadImgsource);
                        ImgList.Add(downloadImgsource);

                        productImgList.Add(new ProductSelectImage()
                        {
                            path = imgURL,
                            image = downloadImgsource,
                            imageBytes = downloadImgSource,
                            OrgimageBytes = downloadImgSource
                        });
                        ProimgList.Add(new ProductImage()
                        {
                            ImagePath = imgURL,
                            Extension = Global.GetFileExtentionUsingURL(imgURL),
                            IsDeleted = false
                        });

                    }

                    var tagImgSource = await client.DownloadDataTaskAsync(new Uri(obj.TagImage));
                    tagImageList.Add(ImageSource.FromStream(() =>
                               new MemoryStream(tagImgSource)
                                ));
                    tagImgList.Add(new TagImage()
                    {
                        Image = ImageSource.FromStream(() =>
                               new MemoryStream(tagImgSource)
                                ).ConvertImagesourceToBase64(),
                        Extension = Global.GetFileExtentionUsingURL(new Uri(Convert.ToString(obj.TagImage)).OriginalString.Replace("Uri: ", "")),
                        ImagePath = new Uri(Convert.ToString(obj.TagImage)).OriginalString.Replace("Uri: ", ""),
                        imageBytes = tagImgSource,
                        OrgimageBytes = tagImgSource,
                        OrignalImagePath = new Uri(Convert.ToString(obj.TagImage)).OriginalString.Replace("Uri: ", "")
                    });


                }
                catch (Exception ex)
                {
                    Acr.UserDialogs.UserDialogs.Instance.Toast("Images not found, unable to load");
                }

                addItemDetailsModel.HeadLine = obj.ProductName;
                addItemDetailsModel.Description = obj.Description;
                if ((obj.EditCategory.Gender.ToLower() == "women" || obj.EditCategory.Gender.ToLower() == "men") && obj.StoreType.ToLower() == Constant.SneakersStr.ToLower())
                {
                    if (obj.ParentCategory.ToLower() == Constant.SneakersStr.ToLower())
                    {
                        addItemDetailsModel.Category = (obj.EditCategory.Gender + " | " + obj.ParentCategory + " | " + obj.EditCategory.SubRoot);
                        IsSelected3level = true;
                    }
                    else
                    {
                        IsSelected3level = false;
                        addItemDetailsModel.Category = obj.StoreType.ToLower().Equals(Constant.SneakersStr.ToLower()) ? obj.EditCategory.Gender + " | " + obj.EditCategory.SubRoot : obj.EditCategory.Root + " | " + obj.EditCategory.SubRoot;
                    }
                }
                else if (obj.EditCategory.NodeTitle != null && obj.EditCategory.NodeTitle.ToLower().Equals("m") || obj.EditCategory.NodeTitle.ToLower().Equals("f"))
                {
                    IsSelected3level = false;
                    addItemDetailsModel.Category = obj.StoreType.ToLower().Equals(Constant.SneakersStr.ToLower()) ? obj.EditCategory.Gender + " | " + obj.EditCategory.SubRoot : obj.EditCategory.Root + " | " + obj.EditCategory.SubRoot;
                }
                else
                {
                    IsSelected3level = true;
                    addItemDetailsModel.Category = !obj.StoreType.ToLower().Equals(Constant.SneakersStr.ToLower()) ? (obj.EditCategory.Gender + " | " + obj.EditCategory.NodeTitle + " | " + obj.EditCategory.SubRoot) : (obj.EditCategory.Root + " | " + obj.EditCategory.NodeTitle);
                }
                addItemDetailsModel.StoreType = obj.StoreType;
                addItemDetailsModel.Quantity = obj.Quantity;
                addItemDetailsModel.Size = obj.Size;
                addItemDetailsModel.Brand = obj.Brand;
                addItemDetailsModel.ProdColor = obj.ProductColor;
                addItemDetailsModel.Condition = obj.ProductCondition;
                addItemDetailsModel.Availability = obj.Availability;
                addItemDetailsModel.Price = obj.Price;
                addItemDetailsModel.ShipPrice = obj.ShippingPrice;
                addItemDetailsModel.quantityModels = obj.quantityModels;
                addItemDetailsModel.CategorySubRoot = obj.EditCategory;
                subRoots = obj.EditCategory;

                //If selected store is sneakers, then to set the parentcategory
                if (!IsSelected3level)
                    addItemDetailsModel.ParentCategory = obj.ProductCategory;
                else
                    addItemDetailsModel.ParentCategory = obj.ParentCategory;

                selectedCategory = obj.EditCategory;
                selectedCategory.Gender = selectedCategory.Gender.ToLower().Equals("men") ? "M" : "F";
                MessagingCenter.Send<object, List<ImageSource>>("IsReorder", "IsReorder", ImgList);
                _sellerClosetDetailsView.SetTagImage();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                Acr.UserDialogs.UserDialogs.Instance.HideLoading();
            }
        }
        private async void CopyProductMethod()
        {
            try
            {

                if (imageList.Count == 0)
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
                dashBoardModel.Id = productDashBoardModel.Id;
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
                // dashBoardModel.ProductRating = "UnfillHeart";
                dashBoardModel.ParentCategory = addItemDetailsModel.ParentCategory;
                dashBoardModel.ProductColor = addItemDetailsModel.ProdColor;
                dashBoardModel.ProductCondition = addItemDetailsModel.Condition;
                dashBoardModel.quantityModels = QuantityModelList.ToList();
                dashBoardModel.StoreCategory = Global.Storecategory;
                dashBoardModel.StoreType = Global.Storecategory;
                dashBoardModel.Price = addItemDetailsModel.Price;
                dashBoardModel.Brand = addItemDetailsModel.Brand != null ? addItemDetailsModel.Brand : "NA";
                dashBoardModel.Availability = addItemDetailsModel.Availability;
                dashBoardModel.CategorySubRoot = subRoots;
                dashBoardModel.ShippingPrice = addItemDetailsModel.ShipPrice;
                Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Copying item details, Please wait...");
                await Task.Delay(500);
                await CopyProduct(dashBoardModel);
            }
            catch (Exception ex)
            {

            }
        }
        public async Task CopyProduct(DashBoardModel dashBoardModel)
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
                foreach (var objImg in productImgList)
                {
                    listImgSource.Add(new ProductImage
                    {
                        Image = objImg.image.ConvertImagesourceToBase64(),
                        Extension = ProimgList[index].Extension,
                        IsDeleted = objImg.IsDeleted
                    });
                    index++;
                }

                var proCovImg = new ProductCoverImage();
                proCovImg.Image = dashBoardModel.otherImages[0].ConvertImagesourceToBase64();
                proCovImg.Extension = ProimgList[0].Extension;
                proCovImg.IsDeleted = true;

                var addItemRM = new AddItemRequestModel();
                addItemRM.Brand = dashBoardModel.Brand != null ? dashBoardModel.Brand : "NA";
                addItemRM.CategoryName = dashBoardModel.ProductCategory;
                addItemRM.Description = dashBoardModel.Description;
                //addItemRM.CategoryName = dashBoardModel.Gender;
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
                addItemRM.RootCategoryName = dashBoardModel.CategorySubRoot.Gender == null ? string.Empty : (dashBoardModel.CategorySubRoot.Gender.ToLower() == "m" ? "M" : (dashBoardModel.CategorySubRoot.Gender.ToLower() == "men") ? "M" : "F");
                if (dashBoardModel.ParentCategory.ToLower() == Constant.SneakersStr.ToLower())
                {
                    addItemRM.ParentCategoryName = addItemDetailsModel.ParentCategory;
                }
                else
                {
                    addItemRM.ParentCategoryName = dashBoardModel.CategorySubRoot.NodeTitle == null ? String.Empty : (dashBoardModel.CategorySubRoot.NodeTitle.ToLower() == "m" || dashBoardModel.CategorySubRoot.NodeTitle.ToLower() == "f") ? dashBoardModel.CategorySubRoot.SubRoot : dashBoardModel.CategorySubRoot.NodeTitle;
                }
                addItemRM.UserId = Constant.LoginUserData.Id;

                if (IsSelected3level)
                    addItemRM.CategoryName = dashBoardModel.CategorySubRoot.SubRoot;// == null ? String.Empty : dashBoardModel.ParentCategory==null? dashBoardModel.CategorySubRoot.SubRoot : ((dashBoardModel.ParentCategory.ToLower()=="m"|| dashBoardModel.ParentCategory.ToLower() == "f")?string.Empty:dashBoardModel.CategorySubRoot.SubRoot);
                else
                    addItemRM.CategoryName = "";
                //addItemRM.CategoryName = dashBoardModel.CategorySubRoot.SubRoot == null ? String.Empty : dashBoardModel.CategorySubRoot.SubRoot;

                addItemRM.ShippingPrice = Convert.ToDouble(dashBoardModel.ShippingPrice.Replace("$", ""));
                addItemRM.TagImage = tagImgList.Count > 0 ? tagImgList[0] : null;
                addItemRM.Source = Device.RuntimePlatform == Device.iOS ? "iOS" : "Android";
                string methodUrl = "/api/Product/SaveProduct";
                RestResponseModel responseModel = await WebService.PostData(addItemRM, methodUrl, true);
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
                                Global.AlertWithAction("Copied item details successfully!!", () =>
                                {
                                    Global.IsUploadedProduct = true;
                                    Global.SetThemeColor(storeID);
                                    Global.GenderIndex = dashBoardModel.CategorySubRoot.Root == null ? 1 : (dashBoardModel.CategorySubRoot.Root.ToLower() == "men" ? 1 : 2);
                                    Global.GenderParam = dashBoardModel.CategorySubRoot.Root == null ? "men" : (dashBoardModel.CategorySubRoot.Root.ToLower() == "men" ? "men" : "women");
                                    Global.ResetMessagingCenterForEdit();
                                    var itemDetailPage = navigation.NavigationStack.FirstOrDefault(p => p is ItemDetailsPage);
                                    if (itemDetailPage != null)
                                    {
                                        navigation.RemovePage(itemDetailPage);
                                    }
                                    Global.IsUploadedProduct = true;
                                    navigation.PopAsync();
                                });
                                //var alertConfig = new AlertConfig
                                //{
                                //    Message = "Copied item details successfully!!",
                                //    OkText = "OK",
                                //    OnAction = () =>
                                //    {
                                //        //Global.IsUploadedProduct = true;
                                //        //Global.SetThemeColor(storeID);
                                //        //Global.GenderIndex = dashBoardModel.CategorySubRoot.Root == null ? 1 : (dashBoardModel.CategorySubRoot.Root.ToLower() == "men" ? 1 : 2);
                                //        //Global.GenderParam = dashBoardModel.CategorySubRoot.Root == null ? "men" : (dashBoardModel.CategorySubRoot.Root.ToLower() == "men" ? "men" : "women");
                                //        //Global.ResetMessagingCenterForEdit();
                                //        //var itemDetailPage = navigation.NavigationStack.FirstOrDefault(p => p is ItemDetailsPage);
                                //        //if (itemDetailPage != null)
                                //        //{
                                //        //    navigation.RemovePage(itemDetailPage);
                                //        //}
                                //        //Global.IsUploadedProduct = true;
                                //        //navigation.PopAsync();
                                //        Global.IsUploadedProduct = true;
                                //        Global.SetThemeColor(storeID);
                                //        Global.GenderIndex = dashBoardModel.CategorySubRoot.Root == null ? 1 : (dashBoardModel.CategorySubRoot.Root.ToLower() == "men" ? 1 : 2);
                                //        Global.GenderParam = dashBoardModel.CategorySubRoot.Root == null ? "men" : (dashBoardModel.CategorySubRoot.Root.ToLower() == "men" ? "men" : "women");
                                //        Global.ResetMessagingCenterForEdit();
                                //        var itemDetailPage = navigation.NavigationStack.FirstOrDefault(p => p is ItemDetailsPage);
                                //        if (itemDetailPage != null)
                                //        {
                                //            navigation.RemovePage(itemDetailPage);
                                //        }
                                //        Global.IsUploadedProduct = true;
                                //        navigation.PopAsync();
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
                    Acr.UserDialogs.UserDialogs.Instance.Alert("Unable to copy product details");
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
                IsTap = false;
                Acr.UserDialogs.UserDialogs.Instance.HideLoading();
            }
        }
        public void ResetMessagingCenter()
        {
            MessagingCenter.Unsubscribe<object, string>("SelectPropertyValue", "SelectPropertyValue");
            MessagingCenter.Unsubscribe<object, byte[]>("IsImgAdd", "IsImgAdd");
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
                if (BrandList.Count > 0)
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
