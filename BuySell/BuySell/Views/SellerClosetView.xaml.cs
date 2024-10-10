using BuySell.Helper;
using BuySell.Model;
using BuySell.ViewModel;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BuySell.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SellerClosetView : ContentPage
    {
        SellerClosetViewModel vm;
        public SellerClosetView(string headerTitle=null,string userID=null,bool IsLogingUser=false)
        {
            InitializeComponent();
            if (Constant.LoginUserData != null)
            {
                if (Convert.ToString(Constant.LoginUserData.Id) == userID)
                {
                    header.Text = "MY STORE";
                }
                else
                {
                    if (!string.IsNullOrEmpty(headerTitle))
                    {
                        header.Text = headerTitle;
                    }
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(headerTitle))
                {
                    header.Text = headerTitle;
                }
            }
            BindingContext= filterView.BindingContext = vm = new SellerClosetViewModel(this.Navigation, IsLogingUser, userID);
            //To set the store as per dashboard
            Global.SearchedResultSelectedStore = Global.Storecategory;
            vm.PropertyChanged += Vm_PropertyChanged;
            SetupMessagingCenter();
        }
        private void Vm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "sizeByProductList")
            {
                filterView.SizeList = vm.sizeByProductList.ToList();
            }
            if (e.PropertyName == "brandList")
            {
                filterView.BrandList = vm.brandList.ToList();
            }
        }
        protected override void OnAppearing()
        {
            vm.IsTap = false;
            base.OnAppearing();
            vm.ThemeColor = Global.GetThemeColor(Global.setThemeColor);
            vm.SelectedIndexHeaderTab = ((int)Global.setThemeColor);
            if (Global.IsUploadedProduct)
            {
                //if (Device.RuntimePlatform == Device.iOS)
                //{
                //    //Device.InvokeOnMainThreadAsync(()=> {
                //    vm.TopTrendingItemsList.Clear();
                //    vm.PageNumber = 1;

                //    //});
                //}
                vm.ItemTreshold = 1;
                vm.TopTrendingItemsList.Clear();
                vm.PageNumber = 1;
                vm.GetTopTrendingItemsListMethod();
               
            }
            MessagingCenter.Send<object>(Constant.UpdateThemeStr, Constant.UpdateThemeStr);
            MessagingCenter.Send<object>(Constant.UpdateHeaderThemeStr, Constant.UpdateHeaderThemeStr);
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
        void btnPrevious_Tapped(System.Object sender, System.EventArgs e)
        {
            try
            {
                vm.PageNumber = vm.productPagingListResponse.Data.PreviousPageNumber;
                Device.InvokeOnMainThreadAsync(async () =>
                {
                    await vm.CallGetProductListByUserIdMethod();
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        void btnNext_Tapped(System.Object sender, System.EventArgs e)
        {
            try
            {
                if (vm != null)
                {
                    vm.PageNumber = vm.productPagingListResponse.Data.NextPageNumber;

                    Device.InvokeOnMainThreadAsync(async () =>
                    {
                        await vm.CallGetProductListByUserIdMethod();
                    });
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            Global.TileWidth = (width - 40) / 2;
        }
        private void searchTxt_Focused(object sender, FocusEventArgs e)
        {
            searchTxt.Focus();
        }
        /// <summary>
        /// To received the filter list
        /// </summary>
        private void SetupMessagingCenter()
        {
            try
            {
                MessagingCenter.Subscribe<object, List<FilterModel>>("FilterList", "FilterList", async (sender, arg) =>
                {
                    if (arg != null)
                    {
                        var allCount = arg.Where(f => f.Value.ToLower() == "all").ToList().Count;
                        if (allCount == 9)
                        {
                            vm.IsFilterChanged = true;
                            vm.productFilterModel = null;
                            vm.TopTrendingItemsList.Clear();
                            vm.PageNumber = 1;
                            vm.ItemTreshold = 1;
                            //if (Device.RuntimePlatform == Device.iOS)
                            {
                                Device.StartTimer(TimeSpan.FromMilliseconds(100), () =>
                                {
                                    vm.GetTopTrendingItemsListMethod();
                                    return false;
                                });
                            }
                        }
                        else
                        {
                            vm.IsFilterChanged = true;
                            vm.productFilterModel = null;
                            vm.TopTrendingItemsList.Clear();
                            vm.PageNumber = 1;
                            await Task.Run(() =>
                            {
                                vm.SetProductFilterRequest(arg);
                            });
                        }
                    }
                });
            }
            catch (Exception ex)
            {

            }
        }
    }
}