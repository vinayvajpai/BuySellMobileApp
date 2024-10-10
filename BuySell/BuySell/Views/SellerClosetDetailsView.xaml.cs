using Acr.UserDialogs;
using BuySell.CustomControl;
using BuySell.Helper;
using BuySell.Model;
using BuySell.Utility;
using BuySell.View;
using BuySell.ViewModel;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static BuySell.Model.CategoryModel;

namespace BuySell.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SellerClosetDetailsView : ContentPage
    {
        SellerClosetDetailsViewModel m_model;
        int currentFilter = 1;
        public CategoryViewModel vm;
        bool IsLoadingData = false;
        Model.DashBoardModel productDashBoardModel = null;

        #region Constructor
        public SellerClosetDetailsView(Model.DashBoardModel obj)
        {
            InitializeComponent();
            IsLoadingData = true;
            productDashBoardModel = obj;
            Global.ResetMessagingCenter(this);
            BindingContext = m_model = new SellerClosetDetailsViewModel(obj, this.Navigation, this);
            subCatHead.SelectedTapIndex = m_model.SelectedIndexHeaderTab;
            m_model.PropertyChanged += Vm_PropertyChanged;
            EditSection.IsVisible = true;
            subCatHead.SelectedTapIndex = m_model.SelectedIndexHeaderTab;
            OffersSection.IsVisible = false;

            if (obj != null)
            {
                lblTitletxt.Text = obj.ProductName + " " + "Details";
            }

            MessagingCenter.Subscribe<object, string>("IsImgAdd", "IsImgAdd", (sender, arg) =>
            {
                if (arg != null)
                {
                    SelectedAddImgMethod(File.ReadAllBytes(arg), arg);
                }
            });

            MessagingCenter.Subscribe<object, byte[]>("ImageToBeDelete", "ImageToBeDelete", (sender, arg) =>
            {
                if (arg != null)
                {
                    SelectedDeletedImgMethod(arg);
                }
            });

            MessagingCenter.Subscribe<object, SubRoots>("SelectedGenderCat", "SelectedGenderCat", (sender, arg) =>
            {
                if (arg != null)
                {
                    var str = arg.Root + " | " + arg.NodeTitle + " | " + arg.SubRoot;
                    m_model.selectedCategory = arg;
                    m_model.subRoots = arg;
                    m_model.addItemDetailsModel.Category = str;

                    if (m_model.addItemDetailsModel != null)
                        m_model.addItemDetailsModel.Size = Constant.SizeText;

                    m_model.IsShowCustomSize = false;
                    m_model.IsShowOneQuantity = true;
                    m_model.IsSelected3level = true;
                    //sizePickerText.Text = string.Empty;
                }
            });

            MessagingCenter.Subscribe<object, SelectSubRootCategory>("SelGenderCatSneakers", "SelGenderCatSneakers", (sender, arg) =>
            {
                if (arg != null)
                {
                    var str = arg.Key.Root + " | " + arg.Key.NodeTitle;
                    SubRoots sub = new SubRoots()
                    {
                        Gender = arg.Key.Gender,
                        Node = arg.Key.Node,
                        NodeTitle = arg.Key.NodeTitle,
                        IsShowMore = arg.Key.IsShowMore,
                        Root = arg.Key.Root
                    };
                    m_model.selectedCategory = sub;
                    m_model.subRoots = sub;
                    m_model.addItemDetailsModel.Category = str;

                    if (m_model.addItemDetailsModel != null)
                        m_model.addItemDetailsModel.Size = Constant.SizeText;

                    //sizePickerText.Text = string.Empty;
                    m_model.IsShowCustomSize = false;
                    m_model.IsShowOneQuantity = true;
                    m_model.IsSelected3level = false;
                }
            });

            MessagingCenter.Subscribe<object, List<ImageSource>>("IsReorder", "IsReorder", (sender, arg) =>
            {
                var list = ((List<ImageSource>)arg);
                if (list != null)
                {
                    if (list.Count > 0)
                    {
                        imgAdd1.Source = list[0];
                    }
                    if (list.Count > 1)
                    {
                        imgAdd2.Source = list[1];
                    }
                    if (list.Count == 0)
                    {
                        m_model.imageList.Clear();
                        m_model.ImgList.Clear();
                        m_model.productImgList.Clear();
                    }
                    SetSelectedImageFrames();
                }
            });

            //To set the store as per dashboard
            Global.SearchedResultSelectedStore = Global.Storecategory;

            m_model.PropertyChanged += Vm_PropertyChanged;

        }
        #endregion

        #region Selected Delete Image Method
        private void SelectedDeletedImgMethod(byte[] DelImage)
        {
            Device.InvokeOnMainThreadAsync(() =>
            {
                if (DelImage != null)
                {
                    if (m_model.ImgEdit == "EditingImage1")
                    {
                        m_model.imageList.RemoveAt(0);
                        m_model.ImgList.RemoveAt(0);
                        m_model.productImgList.RemoveAt(0);
                        imgAdd1.Source = null;
                        AddPic1Txt.IsVisible = true;
                        Editimg1.IsVisible = false;
                    }
                    else if (m_model.ImgEdit == "EditingImage2")
                    {
                        var imgSrc = m_model.imageList[1];
                        m_model.imageList.RemoveAt(1);
                        m_model.ImgList.RemoveAt(1);
                        m_model.productImgList.RemoveAt(1);
                        imgAdd2.Source = null;
                        AddPic2Txt.IsVisible = true;
                        Editimg2.IsVisible = false;
                    }
                    else if (m_model.ImgEdit == "EditingImage3")
                    {
                        if (m_model.tagImageList.Count > 0)
                        {
                            m_model.tagImageList.RemoveAt(0);
                            m_model.tagImgList.RemoveAt(0);
                            if (m_model.tagImageList.Count > 0)
                            {
                                imgTag.Source = m_model.tagImageList[0];
                                Editimg3.IsVisible = true;
                                m_model.IsFrmImgTag = true;
                            }
                            else
                            {
                                imgTag.Source = "Gallery";
                                Editimg3.IsVisible = false;
                                m_model.IsFrmImgTag = false;
                            }
                        }
                        else
                        {
                            imgTag.Source = "Gallery";
                            Editimg3.IsVisible = false;
                            m_model.IsFrmImgTag = false;
                        }
                    }
                    m_model.ImageToBeEdited = "null";
                    var arg = ImageSource.FromStream(() => new MemoryStream(DelImage));
                    SetImageFrame(arg);

                    if (PopupNavigation.PopupStack.Count > 0)
                    {
                        PopupNavigation.PopAsync();
                    }
                }
                //vm.IsTap = true;
            });
        }
        #endregion

        #region Selected add image Method
        private void SelectedAddImgMethod(Byte[] byt, string url = null)
        {
            try
            {
                UserDialogs.Instance.ShowLoading();
                Device.InvokeOnMainThreadAsync(() =>
                {
                    var arg = ImageSource.FromStream(() => new MemoryStream(byt));
                    if (!m_model.IsUploadTagImage)
                    {
                        if (m_model.ImageToBeEdited == "EditingImage1")
                        {
                            m_model.imageList.RemoveAt(0);
                            m_model.ImgList.RemoveAt(0);
                            m_model.ProimgList.RemoveAt(0);
                            m_model.imageList.Insert(0, arg);
                            m_model.ImgList.Insert(0, arg);
                            if (m_model.productDashBoardModel.otherImages.Count > 0)
                            {
                                m_model.ProimgList.Insert(0, new ProductImage()
                                {
                                    Extension = Global.GetFileExtentionUsingURL(url),
                                    ImagePath = url,
                                    IsDeleted = true,

                                });
                                m_model.productImgList[0].IsDeleted = true;
                            }
                            else
                            {
                                m_model.ProimgList.Insert(0, new ProductImage()
                                {
                                    Extension = Global.GetFileExtentionUsingURL(url),
                                    ImagePath = url,
                                    IsDeleted = false
                                });
                                m_model.productImgList[0].IsDeleted = false;
                            }
                            m_model.productImgList[0].image = arg;
                            imgAdd1.Source = arg;
                            m_model.ImageToBeEdited = "null";
                            //m_model.ImgList = m_model.imageList;
                        }

                        else if (m_model.ImageToBeEdited == "EditingImage2")
                        {
                            m_model.imageList.RemoveAt(1);
                            m_model.ImgList.RemoveAt(1);
                            m_model.ProimgList.RemoveAt(1);
                            m_model.imageList.Insert(1, arg);
                            m_model.ImgList.Insert(1, arg);
                            if (m_model.productDashBoardModel.otherImages.Count > 1)
                            {
                                m_model.ProimgList.Insert(1, new ProductImage()
                                {
                                    Extension = Global.GetFileExtentionUsingURL(url),
                                    ImagePath = url,
                                    IsDeleted = true
                                }); ;
                                m_model.productImgList[1].IsDeleted = true;
                            }
                            else
                            {
                                m_model.ProimgList.Insert(1, new ProductImage()
                                {
                                    Extension = Global.GetFileExtentionUsingURL(url),
                                    ImagePath = url,
                                    IsDeleted = false
                                }); ;
                                m_model.productImgList[1].IsDeleted = false;
                            }
                            imgAdd2.Source = arg;
                            m_model.productImgList[1].image = arg;
                            m_model.ImageToBeEdited = "null";
                            //m_model.ImgList = m_model.imageList;
                        }
                        else if (m_model.ImageToBeEdited == "EditingImage3")
                        {
                            if (arg != null)
                            {
                                imgTag.Source = arg;
                                Editimg3.IsVisible = true;
                                //m_model.tagImageList.RemoveAt(0);
                                //m_model.tagImgList.RemoveAt(0);
                                //m_model.tagImageList.Insert(0, arg);
                                //m_model.tagImgList.Insert(0, new TagImage()
                                //{
                                //    Image = arg.ConvertImagesourceToBase64(),
                                //    Extension = Global.GetFileExtentionUsingURL(url),
                                //    ImagePath = url,
                                //    IsDeleted = true,
                                //    imageBytes=byt
                                //}) ;
                            }
                            m_model.ImageToBeEdited = "null";

                        }
                        else
                        {
                            m_model.ProimgList.Add(new ProductImage()
                            {
                                Extension = Global.GetFileExtentionUsingURL(url),
                                ImagePath = url
                            });
                            m_model.ImgList.Add(arg);
                            m_model.imageList.Add(arg);
                            if (m_model.imageList.Count == 1)
                            {
                                Device.InvokeOnMainThreadAsync(() =>
                                {
                                    imgAdd1.Source = arg;
                                    imgAdd1.IsVisible = true;
                                    m_model.IsFrm2 = true;
                                    AddPic1Txt.IsVisible = false;
                                });
                            }
                            else if (m_model.imageList.Count == 2)
                            {
                                Device.InvokeOnMainThreadAsync(() =>
                                {
                                    imgAdd2.Source = arg;
                                    imgAdd2.IsVisible = true;
                                    m_model.IsFrm5 = true;
                                    AddPic2Txt.IsVisible = false;
                                });
                            }
                            else if (m_model.imageList.Count == 3)
                            {
                                Device.InvokeOnMainThreadAsync(() =>
                                {
                                    m_model.IsFrm3 = false;
                                    m_model.IsFrm4 = true;
                                    m_model.IsFrm5 = true;
                                });
                            }
                            SetSelectedImageFrames();
                            //m_model.ImgList = m_model.imageList;
                        }
                    }
                    else
                    {
                        if (m_model.tagImageList.Count == 1)
                        {
                            Device.InvokeOnMainThreadAsync(() =>
                            {
                                //m_model.tagImgList.Add(new TagImage()
                                //{
                                //    Image = arg.ConvertImagesourceToBase64(),
                                //    Extension = Global.GetFileExtentionUsingURL(url),
                                //    ImagePath = url,
                                //    imageBytes = byt

                                //});
                                //m_model.tagImageList.Add(arg);
                                imgTag.Source = arg;
                                m_model.IsFrmImgTag = true;
                                Editimg3.IsVisible = true;
                                m_model.IsUploadTagImage = false;
                            });
                        }
                        else
                        {
                            //m_model.tagImgList.Add(new TagImage()
                            //{
                            //    Image = arg.ConvertImagesourceToBase64(),
                            //    Extension = Global.GetFileExtentionUsingURL(url),
                            //    ImagePath = url,
                            //    imageBytes = byt
                            //});
                            //m_model.tagImageList.Add(arg);
                            m_model.IsUploadTagImage = false;
                        }
                    }
                });
                UserDialogs.Instance.HideLoading();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                UserDialogs.Instance.HideLoading();
            }
        }
        #endregion

        private void Vm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedIndexHeaderTab")
            {
                subCatHead.SelectedTapIndex = m_model.SelectedIndexHeaderTab;
                //MessagingCenter.Send<object>(Constant.UpdateThemeStr, Constant.UpdateThemeStr);
                //MessagingCenter.Send<object>(Constant.UpdateHeaderThemeStr, Constant.UpdateHeaderThemeStr);
                if (!IsLoadingData)
                {
                    if (m_model != null)
                    {
                        if (m_model.addItemDetailsModel != null)
                        {
                            m_model.addItemDetailsModel.Category = Constant.CategoryText;
                            m_model.addItemDetailsModel.Size = Constant.SizeText;
                        }
                    }
                }
            }
        }
        void SetImageFrame(ImageSource arg)
        {
            SetSelectedImageFrames();
        }
        protected override void OnAppearing()
        {
            try
            {

                SetProductColor();
                SetSelectedImageFrames();
                m_model.IsTap = false;

                var themeIndex = Global.GetSelectedThemeColorIndex(productDashBoardModel.StoreType);
                var themeColorUsingIndex = Global.GetThemeColorUsingIndex(themeIndex);
                m_model.ThemeColor = Global.GetThemeColor(themeColorUsingIndex);
                //m_model.SelectedIndexHeaderTab = themeIndex;
                MessagingCenter.Send<object, ThemesColor>(Constant.UpdateThemeForDetailStr, Constant.UpdateThemeForDetailStr, themeColorUsingIndex);
                MessagingCenter.Send<object, ThemesColor>(Constant.UpdateHeaderThemeForDetailStr, Constant.UpdateHeaderThemeForDetailStr, themeColorUsingIndex);
                base.OnAppearing();
            }
            catch (Exception ex)
            {
                //Acr.UserDialogs.UserDialogs.Instance.Alert(ex.Message);
            }
        }
        void SetProductColor()
        {
            if (m_model.addItemDetailsModel.ProdColor.Equals("Multi-Color"))
            {
                frmColorPicker.IsVisible = false;
                m_model.CamoImgShow = false;
                m_model.RainbowImgShow = true;
            }
            else if (m_model.addItemDetailsModel.ProdColor.Equals("Camo"))
            {
                frmColorPicker.IsVisible = false;
                m_model.RainbowImgShow = false;
                m_model.CamoImgShow = true;
            }
            else
            {
                frmColorPicker.IsVisible = true;
                m_model.RainbowImgShow = false;
                m_model.CamoImgShow = false;
            }
        }

        #region Set selected image frames Method
        private void SetSelectedImageFrames()
        {
            if (m_model.imageList.Count == 0)
            {
                ImageContGrid.ColumnDefinitions[0].Width = new GridLength(10, GridUnitType.Star);
                ImageContGrid.ColumnDefinitions[1].Width = new GridLength(0);
                ImageContGrid.ColumnDefinitions[2].Width = new GridLength(0);
                Editimg1.IsVisible = false;
                Editimg2.IsVisible = false;
                ImageContGrid.ColumnSpacing = 0;
                AddPic1Txt.IsVisible = true;
                AddPic2Txt.IsVisible = false;
                m_model.IsFrm1 = true;
                m_model.IsFrm2 = false;
                m_model.IsFrm3 = false;
                m_model.IsFrm4 = false;
                m_model.IsFrm5 = false;
                imgAdd1.Source = null;
                imgAdd2.Source = null;
            }
            else if (m_model.imageList.Count == 1)
            {
                ImageContGrid.ColumnDefinitions[0].Width = new GridLength(5, GridUnitType.Star);
                ImageContGrid.ColumnDefinitions[1].Width = new GridLength(5, GridUnitType.Star);
                ImageContGrid.ColumnDefinitions[2].Width = new GridLength(0);
                Editimg1.IsVisible = true;
                Editimg2.IsVisible = false;
                ImageContGrid.ColumnSpacing = 10;
                frm1.GestureRecognizers.Clear();
                imgAdd1.Source = m_model.imageList[0];
                AddPic1Txt.IsVisible = false;
                AddPic2Txt.IsVisible = true;
                m_model.IsFrm2 = true;
                m_model.IsFrm3 = false;
                m_model.IsFrm4 = false;
                m_model.IsFrm5 = false;
                imgAdd2.Source = null;

            }
            else if (m_model.imageList.Count == 2)
            {
                ImageContGrid.ColumnDefinitions[0].Width = new GridLength(3.3, GridUnitType.Star);
                ImageContGrid.ColumnDefinitions[1].Width = new GridLength(3.3, GridUnitType.Star);
                ImageContGrid.ColumnDefinitions[2].Width = new GridLength(3.3, GridUnitType.Star);
                Editimg1.IsVisible = true;
                Editimg2.IsVisible = true;
                ImageContGrid.ColumnSpacing = 10;
                frm2.GestureRecognizers.Clear();
                imgAdd1.Source = m_model.imageList[0];
                imgAdd2.Source = m_model.imageList[1];
                AddPic1Txt.IsVisible = false;
                AddPic2Txt.IsVisible = false;
                AddPic3Txt.IsVisible = true;
                m_model.IsFrm2 = true;
                m_model.IsFrm5 = true;
                m_model.IsFrm4 = false;
            }
            else if (m_model.imageList.Count > 2)
            {
                imgAdd2.IsVisible = true;
                imgAdd1.IsVisible = true;
                frm1.IsVisible = true;
                frm2.IsVisible = true;
                ImageContGrid.ColumnDefinitions[0].Width = new GridLength(3.3, GridUnitType.Star);
                ImageContGrid.ColumnDefinitions[1].Width = new GridLength(3.3, GridUnitType.Star);
                ImageContGrid.ColumnDefinitions[2].Width = new GridLength(3.3, GridUnitType.Star);
                Editimg1.IsVisible = true;
                Editimg2.IsVisible = true;
                ImageContGrid.ColumnSpacing = 10;
                frm2.GestureRecognizers.Clear();
                imgAdd1.Source = m_model.imageList[0];
                imgAdd2.Source = m_model.imageList[1];
                AddPic1Txt.IsVisible = false;
                AddPic2Txt.IsVisible = false;
                AddPic3Txt.IsVisible = false;
                m_model.IsFrm3 = false;
                m_model.IsFrm4 = true;
                m_model.IsFrm5 = true;
            }
        }
        #endregion

        public void SetTagImage()
        {
            if (m_model.tagImageList.Count == 0)
            {
                Device.InvokeOnMainThreadAsync(() =>
                {
                    m_model.IsFrmImgTag = true;
                    Editimg3.IsVisible = false;
                    //m_model.IsUploadTagImage = false;
                });
            }
            else
            {
                Device.InvokeOnMainThreadAsync(() =>
                {
                    imgTag.Source = m_model.tagImageList[0];
                    m_model.IsFrmImgTag = true;
                    Editimg3.IsVisible = true;
                    //m_model.IsUploadTagImage = false;
                });
            }

            //Set Product Color
            SetProductColor();

            //Set Selected Store
            if (m_model.addItemDetailsModel.StoreType.ToLower().Equals(Constant.ClothingStr.ToLower()))
            {
                subCatHead.SelectedTapIndex = m_model.SelectedIndexHeaderTab = 1;
            }
            else if (m_model.addItemDetailsModel.StoreType.ToLower().Equals(Constant.SneakersStr.ToLower()))
            {
                subCatHead.SelectedTapIndex = m_model.SelectedIndexHeaderTab = 2;
            }
            else if (m_model.addItemDetailsModel.StoreType.ToLower().Equals(Constant.StreetwearStr.ToLower()))
            {
                subCatHead.SelectedTapIndex = m_model.SelectedIndexHeaderTab = 3;
            }
            else if (m_model.addItemDetailsModel.StoreType.ToLower().Equals(Constant.VintageStr.ToLower()))
            {
                subCatHead.SelectedTapIndex = m_model.SelectedIndexHeaderTab = 4;
            }
            IsLoadingData = false;
        }
        void SelectSellingFilter_Tapped(System.Object sender, System.EventArgs e)
        {
            var par = Convert.ToInt16(((TappedEventArgs)e).Parameter);
            sep1.BackgroundColor = Color.White;
            sep2.BackgroundColor = Color.White;
            currentFilter = par;
            SetFilterSelector();
        }
        void SetFilterSelector()
        {
            try
            {
                if (currentFilter == 1)
                {
                    sep1.BackgroundColor = Color.FromHex(m_model.ThemeColor);
                    EditSection.IsVisible = true;
                    OffersSection.IsVisible = false;
                }
                else if (currentFilter == 2)
                {
                    sep2.BackgroundColor = Color.FromHex(m_model.ThemeColor);
                    EditSection.IsVisible = false;
                    OffersSection.IsVisible = true;
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void HeadlinePickerText_TextChanged(object sender, TextChangedEventArgs e)
        {
            Entry entry = sender as Entry;
            String val = entry.Text;
            if (val.Length > 50)
            {
                m_model.InfoHeadlineIsVisible = true;
                m_model.HeadlineTxtIsVisible = true;
                lblHLCharCount.TextColor = Color.Red;
                lblHLTLCharCount.TextColor = Color.Red;
            }
            else
            {
                m_model.InfoHeadlineIsVisible = false;
                m_model.HeadlineTxtIsVisible = false;
                lblHLCharCount.TextColor = Color.FromHex(m_model.ThemeColor);
                lblHLTLCharCount.TextColor = Color.FromHex(m_model.ThemeColor);
            }
            lblHLCharCount.Text = Convert.ToString(val.Length);
        }
        private void DescriptionPickerText_TextChanged(object sender, TextChangedEventArgs e)
        {
            Editor entry = sender as Editor;
            String val = entry.Text;
            if (val.Length > 100)
            {
                m_model.InfoDescriptionIsVisible = true;
                m_model.DescriptionTxtIsVisible = true;
                lblDescriptionCharCount.TextColor = Color.Red;
                lblDescriptionTLCharCount.TextColor = Color.Red;
            }
            else
            {
                m_model.InfoDescriptionIsVisible = false;
                m_model.DescriptionTxtIsVisible = false;
                lblDescriptionCharCount.TextColor = Color.FromHex(m_model.ThemeColor);
                lblDescriptionTLCharCount.TextColor = Color.FromHex(m_model.ThemeColor);
            }
            lblDescriptionCharCount.Text = Convert.ToString(val.Length);
        }
        private void BrandPickerText_TextChanged(object sender, TextChangedEventArgs e)
        {
           if(!m_model.IsFirstLoad == true)
            {
                if (!string.IsNullOrEmpty(e.NewTextValue))
                {
                    m_model.GetSearchBrandList(e.NewTextValue);
                }
                else
                {
                    m_model.IsBrandListShow = false;
                }
            }
           m_model.IsFirstLoad = false;
        }
        private void brandListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                if (m_model.MasterBrandList == null)
                {
                    return;
                }
                else
                {
                    m_model.addItemDetailsModel.Brand = ((e.SelectedItem as string));
                    m_model.IsBrandListShow = false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        private async void BrandPickerText_Focused(object sender, FocusEventArgs e)
        {
            try
            {
                if (m_model.IsTap)
                    return;
                m_model.IsTap = true;
                m_model.SelectedPropertyIndex = 0;
                await Navigation.PushAsync(new BrandPage());
            }
            catch (Exception)
            {
                m_model.IsTap = false;
            }
        }
    }
}