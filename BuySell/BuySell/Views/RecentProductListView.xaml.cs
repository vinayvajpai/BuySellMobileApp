using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuySell.Helper;
using BuySell.Model;
using BuySell.ViewModel;
using Xamarin.Forms;

namespace BuySell.Views
{
    public partial class RecentProductListView : ContentPage
    {
        RecentAllProductViewModel vm;
        public RecentProductListView(List<DashBoardModel> listRecentProducts)
        {
            InitializeComponent();
            BindingContext= filterView.BindingContext = vm = new RecentAllProductViewModel(this.Navigation,listRecentProducts);
            SetupMessagingCenter();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            vm.IsTap = false;
            MessagingCenter.Send<object>(Constant.UpdateThemeStr, Constant.UpdateThemeStr);
            MessagingCenter.Send<object>(Constant.UpdateHeaderThemeStr, Constant.UpdateHeaderThemeStr);

        }

        //void imgLikeUnlike_Clicked(System.Object sender, System.EventArgs e)
        //{
        //    var objProduct = (DashBoardModel)((TappedEventArgs)e).Parameter;
        //    if (objProduct.ProductRating.Equals("FillHeart"))
        //    {
        //        objProduct.ProductRating = "UnfillHeart";
        //    }
        //    else
        //    {
        //        objProduct.ProductRating = "FillHeart";
        //    }
        //}

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

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            Global.TileWidth = (width - 40) / 2;
        }

        private void SetupMessagingCenter()
        {
            try
            {
                MessagingCenter.Subscribe<object, List<FilterModel>>("FilterList", "FilterList", async (sender, arg) =>
                {
                    if (arg != null)
                    {
                        var allCount = arg.Where(f => f.Value.ToLower() == "all").ToList().Count;
                        var checkCat = arg.Where(c => c.Value.Contains("|")).ToList().Count;
                        if (allCount == 9 && checkCat == 0)
                        {
                            vm.IsFilterChanged = true;
                            vm.productFilterModel = null;
                            vm.ProductList.Clear();
                            vm.PageNumber = 1;
                            vm.ItemTreshold = 1;
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                vm.ConstructorMethod();
                            });
                        }
                        else
                        {
                            vm.IsFilterChanged = true;
                            vm.productFilterModel = null;
                            vm.ProductList.Clear();
                            vm.PageNumber = 1;
                            Acr.UserDialogs.UserDialogs.Instance.ShowLoading();
                            await Task.Delay(100);
                            await Task.Run(() =>
                            {
                                vm.FilterData(arg);
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
