using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BuySell.Helper;
using BuySell.Model;
using BuySell.ViewModel;
using BuySell.Views;
using Microsoft.AppCenter.Crashes;
using Xamarin.Essentials;
using Xamarin.Forms;
using static BuySell.Model.CategoryModel;

namespace BuySell.View
{
    public partial class DashBoardView : ContentPage
    {
        private DashboardProductViewModel vm;
        private bool IsLoaded = false;
        CancellationTokenSource cts = new CancellationTokenSource();

        #region Constructor
        public DashBoardView(bool IsPopup,bool IsShowAccessPopup=false)
        {
            InitializeComponent();
            BindingContext = vm = new DashboardProductViewModel(this.Navigation, IsPopup);
            header.BackCommand = vm.LogingoutCommand;
            header.NavStack = this.Navigation;
            Global.Storecategory = Constant.ClothingStr.ToLower();
            subCatHead.SelectedTapIndex = vm.SelectedIndexHeaderTab;
            vm.PropertyChanged += Vm_PropertyChanged;
            header.SelectedCategoryEvent += Header_SelectedCategoryEvent;
            sep1.BackgroundColor = Color.FromHex(vm.ThemeColor);
            vm.EarlyAccessPopupIsVisible = IsShowAccessPopup;
            MessagingCenter.Subscribe<object>(Constant.UpdateThemeStr, Constant.UpdateThemeStr, (obj) =>
            {
                try
                {
                    if (obj != null)
                    {
                        vm.ThemeColor = Global.GetThemeColor(Global.setThemeColor);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            });
            MessagingCenter.Subscribe<object>(Constant.UpdateHeaderThemeStr, Constant.UpdateHeaderThemeStr, (obj) =>
            {
                try
                {
                    if (obj != null)
                    {
                        vm.ThemeColor = Global.GetThemeColor(Global.setThemeColor);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            });
            vm.IsBusy = true;
            //To set the store as per dashboard
            Global.SearchedResultSelectedStore = Global.Storecategory;
        }
        #endregion
        async protected override void OnAppearing()
        {
            try
            {
               await vm.GetCurrentLocation(cts);
                vm.IsTap = false;
                //  Global.Storecategory = Constant.ClothingStr.ToLower();
                Device.BeginInvokeOnMainThread(async () =>
                {
                    vm.ThemeColor = Global.GetThemeColor(Global.setThemeColor);
                    vm.SelectedIndexHeaderTab = ((int)Global.setThemeColor);
                    vm.SelectedStoreIndex = vm.SelectedIndexHeaderTab;
                    subCatHead.SelectedTapIndex = vm.SelectedIndexHeaderTab;
                    MessagingCenter.Send<object>(Constant.UpdateThemeStr, Constant.UpdateThemeStr);
                    MessagingCenter.Send<object>(Constant.UpdateHeaderThemeStr, Constant.UpdateHeaderThemeStr);
                    Global.ResetMessagingCenter();
                    vm.GetRecentViewList();
                });
                Global.Subcategory = null;
                base.OnAppearing();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        protected override void OnDisappearing()
        {
            if (cts != null && !cts.IsCancellationRequested)
                cts.Cancel();
            base.OnDisappearing();
        }

        private void Header_SelectedCategoryEvent(object sender, int e)
        {
            vm.SelectedTabCommand.Execute(e);
        }
        //Method created to update theme and data as per store selected by user
        private async void Vm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedIndexHeaderTab")
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    subCatHead.SelectedTapIndex = vm.SelectedIndexHeaderTab;
                    if (vm.RecentProductList.Count > 0)
                    {
                        vm.IsShowRecentProduct = true;
                    }
                    else
                    {
                        vm.IsShowRecentProduct = false;
                    }
                    
                    if (Global.IsUploadedProduct || IsLoaded==false)
                    {
                        SelectGenderFilter_Tapped(sender, null);
                    }
                    else
                    {
                        vm.LoadProductData();
                    }
                    Global.IsUploadedProduct = false;
                    MessagingCenter.Send<object>(Constant.UpdateThemeStr, Constant.UpdateThemeStr);
                    MessagingCenter.Send<object>(Constant.UpdateHeaderThemeStr, Constant.UpdateHeaderThemeStr);
                    IsLoaded = true;
                });
            }
            else if(e.PropertyName == nameof(DashBoardViewModel.ProductList))
            {
                if (vm.ProductList.Count > 0)
                {
                    lblProAvailMSG.IsVisible = false;
                    btnViewAll.IsVisible = true;
                }
                else
                {
                    if (TopTrandingList.ItemsSource == null)
                    {
                        lblProAvailMSG.IsVisible = true;
                        btnViewAll.IsVisible = true;
                    }
                }
            }
        }
        //Method created to update theme and data as per filter selected by user
        void Category_Tapped(System.Object sender, System.EventArgs e)
        {
            var param = ((TappedEventArgs)e).Parameter;
            Global.Subcategory = ((TappedEventArgs)e).Parameter.ToString();
            Global.GenderParam = "Men";
            if (Global.GenderIndex == 1)
            {
                Global.GenderParam = "Men";
                vm.ProductByCategoryCommand.Execute(param.ToString());
            }
            else if (Global.GenderIndex == 2)
            {
                Global.GenderParam = "Women";
                vm.ProductByCategoryCommand.Execute(param.ToString());
            }
            else
            {
                Global.GenderParam = "All";
                vm.ProductByCategoryCommand.Execute(param.ToString());
            }
        }
        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            Navigation.PopToRootAsync();
        }
        void SelectGenderFilter_Tapped(System.Object sender, System.EventArgs e)
        {
            int par = 1;
            if (e != null)
            {
                par = Convert.ToInt16(((TappedEventArgs)e).Parameter);
            }
            else
                par = Global.GenderIndex;

            sep1.BackgroundColor = Color.White;
            sep2.BackgroundColor = Color.White;
            sep3.BackgroundColor = Color.White;
            vm.SeletedFilter = par;
            vm.SetFilterSelector();
        }
        async void ViewAll_Tapped(System.Object sender, System.EventArgs e)
        {
            //Global.Storecategory, Global.GenderIndex
            try
            {
                if (vm.IsTap)
                    return;
                vm.IsTap = true;

                //Conditions added to  show products at top trending page as per filter selected by user at dashboard page
                var genderType = "m";
                if (Global.GenderIndex == 1)
                {
                    genderType = "m";
                    //await Navigation.PushAsync(new TopTrendingViewAllPage(vm.AllProductList.ToList().Where(p => p.StoreType.ToLower() == Global.Storecategory.ToLower() && p.Gender.ToLower() == genderType).ToList()));
                    await Navigation.PushAsync(new TopTrendingViewAllPage(genderType, false));
                }
                else if (Global.GenderIndex == 2)
                {
                    genderType = "f";
                    await Navigation.PushAsync(new TopTrendingViewAllPage(genderType, false));
                    //await Navigation.PushAsync(new TopTrendingViewAllPage(vm.AllProductList.ToList().Where(p => p.StoreType.ToLower() == Global.Storecategory.ToLower() && p.Gender.ToLower() == genderType).ToList()));
                }
                else
                {
                    await Navigation.PushAsync(new TopTrendingViewAllPage(genderType, true));
                    //await Navigation.PushAsync(new TopTrendingViewAllPage(vm.AllProductList.ToList().Where(p => p.StoreType.ToLower() == Global.Storecategory.ToLower()).ToList()));
                }
            }
            catch (Exception ex)
            {
                vm.IsTap = true;
                Debug.WriteLine(ex.Message);
            } 
        }
        void imgLikeUnlike_Clicked(System.Object sender, System.EventArgs e)
        {
            var objProduct = (DashBoardModel)((TappedEventArgs)e).Parameter;
            if (objProduct.IsLike)
            {
                Task.Run(async () => {
                    await vm.GetProductLikeResponse(objProduct, 0);
                });
            }
            else
            {
                Task.Run(async () => { await vm.GetProductLikeResponse(objProduct, 1); });
            }
        }
        void TrendingViewScrollView_Scrolled(System.Object sender, Xamarin.Forms.ScrolledEventArgs e)
        {
            ScrollView scrollView = sender as ScrollView;
            double scrollingSpace = scrollView.ContentSize.Width - scrollView.Width;

            if (scrollingSpace <= e.ScrollX)
            {
                //var allPro = vm.AllProductList.Skip(50).Take(50);
                //vm.AllPageProductList = new ObservableCollection<DashBoardModel>(vm.AllPageProductList.Concat(new ObservableCollection<DashBoardModel>(allPro)));
                //vm.ProductList = new ObservableCollection<DashBoardModel>(vm.ProductList.Concat(new ObservableCollection<DashBoardModel>(allPro)));
                //vm.FilterCommand.Execute(null);
                Console.WriteLine("end");
            }
        }
        void TopTrandingList_Scrolled(System.Object sender, Xamarin.Forms.ItemsViewScrolledEventArgs e)
        {
            if (!vm.IsBusy)
            {
                if (e.HorizontalDelta > 0)
                {
                    if (e.LastVisibleItemIndex == vm.ProductList.Count - 1)
                    {

                        //vm.LoadMoreCommand.Execute(null);
                    }
                }
            }
            
        }
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            Global.TileWidth = (width - 40) / 2;
            if (vm != null)
            {
                vm.RowHeight = Global.TileWidth + 70;
            }
        }

        //async Task GetCurrentLocation()
        //{
        //    try
        //    {
        //        var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
        //        cts = new CancellationTokenSource();
        //        var location = await Geolocation.GetLocationAsync(request, cts.Token);

        //        if (location != null)
        //        {
        //            var placemarks = await Geocoding.GetPlacemarksAsync(location.Latitude, location.Longitude);
        //            var placemark = placemarks?.FirstOrDefault();
        //            if (placemark != null)
        //            {
        //                Global.currentPlaceMark = placemark;
        //            }
        //            Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
        //        }
        //    }
        //    catch (FeatureNotSupportedException fnsEx)
        //    {
        //        // Handle not supported on device exception
        //    }
        //    catch (FeatureNotEnabledException fneEx)
        //    {
        //        Acr.UserDialogs.UserDialogs.Instance.Toast("Please enable location");
        //        // Handle not enabled on device exception
        //    }
        //    catch (PermissionException pEx)
        //    {
        //        // Handle permission exception
        //    }
        //    catch (Exception ex)
        //    {
        //        // Unable to get location
        //    }
        //}
    }
}
