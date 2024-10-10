using BuySell.CustomControl;
using BuySell.Helper;
using BuySell.Model;
using BuySell.Popup;
using BuySell.ViewModel;
using BuySell.Views;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BuySell.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailsPage : ContentPage
    {
        public ItemDetailsViewModel vm;
        bool IsLoggedIn = false;
        public event EventHandler<int> SelectedCategoryEvent;
        DashBoardModel _dataModel;
        int count = 0;

        #region Constructor
        public ItemDetailsPage(DashBoardModel dataModel, bool isLoggedIn)
        {
            InitializeComponent();
            BindingContext = vm = new ItemDetailsViewModel(this.Navigation, dataModel, isLoggedIn);
            try
            {
                header.BackCommand = vm.LoginBackCommand;
                header.NavStack = this.Navigation;
                header.SelectedCategoryEvent += Header_SelectedCategoryEvent;
                _dataModel = dataModel;
                if (!string.IsNullOrEmpty(dataModel.UserProfile))
                {
                    sellerImg.Source = ImageSource.FromUri(new Uri(dataModel.UserProfile));
                }
                lblSellerName.Text = "@" + dataModel.UserName;
                IsLoggedIn = isLoggedIn;

                var themeIndex = Global.GetSelectedThemeColorIndex(_dataModel.StoreType);
                var themeColorUsingIndex = Global.GetThemeColorUsingIndex(themeIndex);
                vm.ThemeColor = Global.GetThemeColor(themeColorUsingIndex);
                vm.SelectedIndexHeaderTab = themeIndex;

                MessagingCenter.Send<object, ThemesColor>(Constant.UpdateThemeForDetailStr, Constant.UpdateThemeForDetailStr, themeColorUsingIndex);
                MessagingCenter.Send<object, ThemesColor>(Constant.UpdateHeaderThemeForDetailStr, Constant.UpdateHeaderThemeForDetailStr, themeColorUsingIndex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        #endregion

        #region On Appearing Method
        protected override void OnAppearing()
        {
            try
            {

                var themeIndex = Global.GetSelectedThemeColorIndex(_dataModel.StoreType);
                var themeColorUsingIndex = Global.GetThemeColorUsingIndex(themeIndex);
                vm.ThemeColor = Global.GetThemeColor(themeColorUsingIndex);
                vm.SelectedIndexHeaderTab = themeIndex;

                MessagingCenter.Send<object, ThemesColor>(Constant.UpdateThemeForDetailStr, Constant.UpdateThemeForDetailStr, themeColorUsingIndex);
                MessagingCenter.Send<object, ThemesColor>(Constant.UpdateHeaderThemeForDetailStr, Constant.UpdateHeaderThemeForDetailStr, themeColorUsingIndex);
                if (_dataModel != null && Constant.LoginUserData != null)
                {
                    if (_dataModel.UserId == Constant.LoginUserData.Id.ToString())
                    {
                        vm.IsEditfrmShow = true;
                    }
                    else
                    {
                        vm.IsEditfrmShow = false;
                    }
                }
                if (Global.OfferProductList.Count > 0)
                {
                    var res = Global.OfferProductList.Where(x => x.Id == vm.ProdataModel.Id).ToList();
                    if (res != null && Constant.LoginUserData != null)
                    {
                        if (res.Count == 0)
                        {
                            vm.IsMakeOfferShow = true;
                            vm.IsViewofferfrmShow = false;
                        }
                        else if (res.Count > 0)
                        {
                            vm.IsViewofferfrmShow = true;
                            vm.IsMakeOfferShow = false;
                        }
                        else
                        {
                            vm.IsMakeOfferShow = false;
                            vm.IsViewofferfrmShow = false;
                        }
                    }
                }
                Device.BeginInvokeOnMainThread(() =>
                {
                    vm.GetList();
                });
                var hexColName = Global.GetHexColByName(_dataModel.ProductColor);
                if (vm.ProdataModel.ProductColor.ToLower().Equals("multi-color"))
                {
                    frmColorPicker.IsVisible = false;
                    vm.CamoImgShow = false;
                    vm.RainbowImgShow = true;
                }
                else if (vm.ProdataModel.ProductColor.ToLower().Equals("camo"))
                {
                    frmColorPicker.IsVisible = false;
                    vm.RainbowImgShow = false;
                    vm.CamoImgShow = true;
                }
                else
                {
                    frmColorPicker.IsVisible = true;
                    //vm.ProdataModel.ProductColor = hexColName;
                    vm.ProdHexColor = Color.FromHex(hexColName);
                    vm.RainbowImgShow = false;
                    vm.CamoImgShow = false;
                }

                if (vm.ProdataModel != null)
                {
                    if (vm.ProdataModel.ParentCategory != null)
                    {
                        if (vm.ProdataModel.ParentCategory.Equals("M") || vm.ProdataModel.ParentCategory.Equals("F"))
                        {
                            vm.IsParentCatShow = false;
                        }
                        else
                        {
                            vm.IsParentCatShow = true;
                        }
                    }
                    else
                    {
                        vm.IsParentCatShow = false;
                    }
                }
                else
                {
                    vm.IsParentCatShow = false;
                }
                vm.IsTap = false;
                if (_dataModel.Size != null && _dataModel.Size.Contains("OS"))
                {
                    lblSpnSize.Text = "Size: " + "One Size";
                }
                else
                {
                    lblSpnSize.Text = "Size: " + _dataModel.Size;
                }
            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Alert(ex.Message);
            }
            base.OnAppearing();
            if (Settings.FirstRun)
            {
                PopupNavigation.Instance.PushAsync(new CustomToolTipPopup());
                Settings.FirstRun = false;
            }

        }
        #endregion
        private void Header_SelectedCategoryEvent(object sender, int e)
        {
            vm.SetThemeColor(e);
            MessagingCenter.Send<object>(Constant.UpdateThemeStr, Constant.UpdateThemeStr);
            MessagingCenter.Send<object>(Constant.UpdateHeaderThemeStr, Constant.UpdateHeaderThemeStr);
        }
        async void Questions_Tapped(System.Object sender, System.EventArgs e)
        {
            try
            {
                if (vm.IsTap)
                    return;
                vm.IsTap = true;
                //this.Navigation.PushAsync(new FAQView());
                await Launcher.OpenAsync(new Uri("https://buysellclothing.com/contact-us"));
                vm.IsTap = false;
            }
            catch (Exception ex)
            {
                vm.IsTap = false;
                Debug.WriteLine(ex.Message);
            }

        }
        //Event created to show description of products condition on click of information icon
        void Information_Tapped(System.Object sender, System.EventArgs e)
        {
            try
            {
                count++;
                if (count == 1)
                {
                    if (vm.ProdataModel.ProductCondition.ToLower().Contains("nwt"))
                    {
                        vm.ProdataModel.ProductCondition = "NWT";
                    }
                    else if (vm.ProdataModel.ProductCondition.ToLower().Contains("nwot"))
                    {
                        vm.ProdataModel.ProductCondition = "NWOT";
                    }
                    else
                    {
                        vm.ProdataModel.ProductCondition = "Used";
                    }
                    var Description = Global.ItemConditionDescription(vm.ProdataModel.ProductCondition);
                    description.IsVisible = true;
                    description.Text = Description;
                }
                else
                {
                    description.IsVisible = false;
                    count = 0;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        void ViewOffer_Tapped(System.Object sender, System.EventArgs e)
        {
            if (_dataModel != null && Constant.LoginUserData != null)
            {
                this.Navigation.PushModalAsync(new NavigationPage(new ViewOffersPage(vm.ProdataModel)));
            }
            else
            {
                this.Navigation.PushModalAsync(new NavigationPage(new ViewOffersView(vm.ProdataModel)));
            }
        }
        async void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            try
            {
                var frame = (CustomFrame)sender;
                var parent = frame.Parent as StackLayout;
                var Allchild = parent.Children.AsEnumerable();
                var SelectedPopUpText = Allchild.ElementAt(1) as Label;
                if (!string.IsNullOrEmpty(SelectedPopUpText.Text))
                {
                    var popuppage = new CustomTabPopup(SelectedPopUpText.Text);
                    popuppage.SelectedEvent += Popuppage_SelectedEvent;
                    await PopupNavigation.Instance.PushAsync(popuppage);
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void Popuppage_SelectedEvent(object sender, int e)
        {
            if (SelectedCategoryEvent != null)
            {
                SelectedCategoryEvent.Invoke(sender, e);
            }
        }
        void ImageHeart_Clicked(System.Object sender, System.EventArgs e)
        {
            if (vm.ProdataModel.IsLike)
            {
                Task.Run(async () =>
                {
                    await vm.GetProductLikeResponse(vm.ProdataModel, 0);
                });
            }
            else
            {
                Task.Run(async () => { await vm.GetProductLikeResponse(vm.ProdataModel, 1); });
            }
        }
        void ViewCloset_Tapped(System.Object sender, System.EventArgs e)
        {
            try
            {
                if (vm.IsTap)
                    return;
                vm.IsTap = true;

                this.Navigation.PushAsync(new SellerClosetView(lblSellerName.Text + "'s" + " Store", vm.ProdataModel.UserId, true));
            }
            catch (Exception ex)
            {
                vm.IsTap = false;
                Debug.WriteLine(ex.Message);
            }

        }
        private async void ImageViewTapped(object sender, EventArgs e)
        {
            try
            {
                if (vm.IsTap)
                    return;

                vm.IsTap = true;

                if (vm != null)
                {
                    List<ImageSource> SingleImage = new List<ImageSource>();

                    SingleImage.Add(vm.CurrentImage.GroupImage);

                    var popupdefault = new ViewMoreImgPopup(SingleImage, vm.ThemeColor);
                    await PopupNavigation.Instance.PushAsync(popupdefault);
                    await Task.Delay(1000);
                    vm.IsTap = false;
                }
            }
            catch (Exception ex)
            {
                vm.IsTap = false;
                Debug.WriteLine(ex.Message + "From ItemDetailView.xaml.cs");
            }
        }
        protected override bool OnBackButtonPressed()
        {
            if (PopupNavigation.PopupStack.Count > 0)
            {
                PopupNavigation.PopAsync();
            }
            return false;
        }
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            caroselView.HeightRequest = (width - 40);
            //Global.TileWidth = (width - 40) / 2;
        }
    }
}