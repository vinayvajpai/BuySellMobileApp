using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using BuySell.Helper;
using BuySell.Model;
using BuySell.Utility;
using BuySell.Views;
using Newtonsoft.Json;
using Xamarin.Forms;
using static BuySell.Model.CategoryModel;

namespace BuySell.ViewModel
{
    public class CategoryViewModel : BaseViewModel
    {
        #region Constructor
        public CategoryViewModel()
        {
           
            try
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    if (Global.Storecategory.ToLower() == Constant.ClothingStr.ToLower() || Global.Storecategory.ToLower() == Constant.StreetwearStr.ToLower() || Global.Storecategory.ToLower() == Constant.VintageStr.ToLower())
                    {
                        roots = MockProductData.Instance.GetCategoryList();
                    }
                    else
                    {
                        roots = MockProductData.Instance.GetSneakesCatList();
                    }
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        public CategoryViewModel(string selectedcat, string selectedsubcat,string storeID=null)
        {
            try
            {
                UserDialogs.Instance.ShowLoading();
                Device.StartTimer(TimeSpan.FromMilliseconds(250),()=>{
                    //if (Global.Storecategory.ToLower() == Constant.ClothingStr.ToLower() || Global.Storecategory.ToLower() == Constant.StreetwearStr.ToLower()|| Global.Storecategory.ToLower() == Constant.VintageStr.ToLower() || selectedsubcat != "2")
                    if(storeID!="2")
                    {
                        if ((selectedcat != null && selectedcat.ToLower() == "all") || selectedcat == null)
                        {
                            roots = MockProductData.Instance.GetCommonCategoryListForAllStores(selectedcat, selectedsubcat);
                        }
                        else 
                        {
                            roots = MockProductData.Instance.GetCategoryList(selectedcat, selectedsubcat);
                        }
                    }
                    else //if (Global.Storecategory.ToLower() == Constant.SneakersStr.ToLower() || selectedsubcat == "2")
                    {
                        if ((selectedcat != null && selectedcat.ToLower() == "all") || selectedcat == null)
                        {
                            roots = MockProductData.Instance.GetCommonSneakersCateListForAllStores(selectedcat, selectedsubcat);
                        }
                        else
                        {
                            roots = MockProductData.Instance.GetSneakesCatList(selectedcat, selectedsubcat);
                        }
                    }
                    //else
                    //{
                    //    roots = MockProductData.Instance.GetSneakesCatList(selectedcat, selectedsubcat);
                    //}
                    //Condition added to check for last navigation, if it is my favorite or my store then call common category method
                    //if (navigation.NavigationStack.LastOrDefault().GetType() == typeof(MyFavoritesPage) || navigation.NavigationStack.LastOrDefault().GetType() == typeof(SellerClosetView))
                    //if(navigation.NavigationStack.LastOrDefault().GetType() == typeof(MyFavoritesPage))
                    //{
                    //    //Condition added to show category list inside search result products view according to the store selected by user on search result page
                    //    if (Global.Storecategory.ToLower() == Global.SearchedResultSelectedStore.ToLower())
                    //    {
                    //        if (Global.Storecategory.ToLower() == Constant.ClothingStr.ToLower() || Global.Storecategory.ToLower() == Constant.StreetwearStr.ToLower() || Global.Storecategory.ToLower() == Constant.VintageStr.ToLower())
                    //        {
                    //            roots = MockProductData.Instance.GetCategoryListForAllStores(selectedcat, selectedsubcat);
                    //        }
                    //        else
                    //        {
                    //            roots = MockProductData.Instance.GetCategoryListForAllStores(selectedcat, selectedsubcat);
                    //        }
                    //    }
                    //    else
                    //    {
                    //        if (Global.SearchedResultSelectedStore == Constant.ClothingStr.ToLower() || Global.SearchedResultSelectedStore.ToLower() == Constant.StreetwearStr.ToLower() || Global.SearchedResultSelectedStore.ToLower() == Constant.VintageStr.ToLower())
                    //        {
                    //            roots = MockProductData.Instance.GetCategoryListForAllStores(selectedcat, selectedsubcat);
                    //        }
                    //        else
                    //        {
                    //            roots = MockProductData.Instance.GetCategoryListForAllStores(selectedcat, selectedsubcat);
                    //        }
                    //    }
                    //}
                    //else 
                    //{
                    //    //Condition added to show category list inside search result products view according to the store selected by user on search result page
                    //    if (Global.Storecategory.ToLower() == Global.SearchedResultSelectedStore.ToLower())
                    //    {
                    //        if (Global.Storecategory.ToLower() == Constant.ClothingStr.ToLower() || Global.Storecategory.ToLower() == Constant.StreetwearStr.ToLower() || Global.Storecategory.ToLower() == Constant.VintageStr.ToLower())
                    //        {
                    //            roots = MockProductData.Instance.GetCategoryList(selectedcat, selectedsubcat);
                    //            //roots = MockProductData.Instance.GetCommonCategoryListForAllStores(selectedcat, selectedsubcat);
                    //        }
                    //        else
                    //        {
                    //            roots = MockProductData.Instance.GetSneakesCatList(selectedcat, selectedsubcat);
                    //        }
                    //    }
                    //    else
                    //    {
                    //        if (Global.SearchedResultSelectedStore == Constant.ClothingStr.ToLower() || Global.SearchedResultSelectedStore.ToLower() == Constant.StreetwearStr.ToLower() || Global.SearchedResultSelectedStore.ToLower() == Constant.VintageStr.ToLower())
                    //        {
                    //            roots = MockProductData.Instance.GetCategoryList(selectedcat, selectedsubcat);
                    //        }
                    //        else
                    //        {
                    //            roots = MockProductData.Instance.GetSneakesCatList(selectedcat, selectedsubcat);
                    //        }
                    //    }
                    //}

                    IsExpandedHeader = true;
                    UserDialogs.Instance.HideLoading();
                    return false;
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                UserDialogs.Instance.HideLoading();
            }
            finally
            {
                
            }
        }

        #endregion

        #region Commands
        private Command<Nodes> _ItemSelectedCommand;
        public Command<Nodes> ItemSelectedCommand
        {
            get
            {
                return _ItemSelectedCommand ?? (_ItemSelectedCommand = new Command<Nodes>((x) => ItemSelectedMethod(x)));
            }

        }

        private Command _BackCommand;
        public Command BackCommand
        {
            get
            {
                return _BackCommand ?? (_BackCommand = new Command(async () =>
                {
                    try
                    {
                        IsTap = false;
                        await navigation.PopAsync(true);
                    }
                    catch (Exception ex)
                    {

                    }
                }
             ));
            }
        }
        #endregion

        #region Properties 
        //public List<Roots> roots {
        //    get; private set; 
        //}

        private List<Roots> _roots;
        public List<Roots> roots
        {
            get { return _roots; }
            set { _roots = value; OnPropertyChanged("roots"); }
        }

        private bool _IsExpandedHeader = false;
        public bool IsExpandedHeader
        {
            get { return _IsExpandedHeader; }
            set { _IsExpandedHeader = value; OnPropertyChanged("IsExpandedHeader"); }
        }


        #endregion

        #region Methods
        private void ItemSelectedMethod(Nodes obj)
        {
            try
            {
                Debug.WriteLine("Data" + obj);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
        #endregion
    }
}
