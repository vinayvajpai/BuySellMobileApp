using System;
using System.Collections.Generic;
using System.Diagnostics;
using BuySell.Helper;
using BuySell.Model;
using BuySell.ViewModel;
using Xamarin.Forms;

namespace BuySell.Views
{
    public partial class SearchResultPage : ContentPage
    {
        SearchGroupViewModel ViewModel;

        public SearchResultPage(string SearchItem)
        {
            InitializeComponent();
            BindingContext = ViewModel = new SearchGroupViewModel(this.Navigation, SearchItem);
            ViewModel.navigation = this.Navigation;
            //searchtext.Text = SearchItem;
            //Global.SearchedResultSelectedStoreTheme = Global.GetThemeColor(Global.setThemeColor);
        }

        async void Back_Tapped(System.Object sender, System.EventArgs e)
        {
            try
            {
                if (Navigation.ModalStack.Count > 0)
                {
                    await Navigation.PopModalAsync(true);
                }
                else
                {
                    await Navigation.PopAsync(true);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        protected override void OnAppearing()
        {
            ViewModel.ThemeColor = Global.GetThemeColor(Global.setThemeColor);
            ViewModel.SelectedIndexHeaderTab = ((int)Global.setThemeColor);
            MessagingCenter.Send<object>(Constant.UpdateThemeStr, Constant.UpdateThemeStr);
            MessagingCenter.Send<object>(Constant.UpdateHeaderThemeStr, Constant.UpdateHeaderThemeStr);
            Global.ResetMessagingCenter();
            base.OnAppearing();

        }

        void ListView_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            try
            {
                if (e.SelectedItem != null)
                {
                    //Conditions added set Store type inside global and send category to search result products view page
                    var searchResultModel = (SearchResultModel)e.SelectedItem;
                    if (searchResultModel != null && ViewModel.searchResponseModel != null)
                    {
                        if (searchResultModel.Description.ToLower().Contains(Constant.ClothingStr.ToLower()) && searchResultModel.StoreID == "1")
                        {
                            Global.SearchedResultSelectedStore = Constant.ClothingStr.ToLower();
                            this.Navigation.PushAsync(new SearchResultProductsView(searchtext.Text, ViewModel.ClothingproductListResponses, searchResultModel.Description, searchResultModel.StoreID, ViewModel.searchResponseModel));
                        }
                        else if (searchResultModel.Description.ToLower().Contains(Constant.SneakersStr.ToLower()) && searchResultModel.StoreID == "2")
                        {
                            Global.SearchedResultSelectedStore = Constant.SneakersStr.ToLower();
                            this.Navigation.PushAsync(new SearchResultProductsView(searchtext.Text, ViewModel.SneekarproductListResponses, searchResultModel.Description, searchResultModel.StoreID, ViewModel.searchResponseModel));
                        }
                        else if (searchResultModel.Description.ToLower().Contains(Constant.StreetwearStr.ToLower()) && searchResultModel.StoreID == "3")
                        {
                            Global.SearchedResultSelectedStore = Constant.StreetwearStr.ToLower();
                            this.Navigation.PushAsync(new SearchResultProductsView(searchtext.Text, ViewModel.StreetproductListResponses, searchResultModel.Description, searchResultModel.StoreID, ViewModel.searchResponseModel));
                        }

                        else if (searchResultModel.Description.ToLower().Contains(Constant.VintageStr.ToLower()) && searchResultModel.StoreID == "4")
                        {
                            Global.SearchedResultSelectedStore = Constant.VintageStr.ToLower();
                            this.Navigation.PushAsync(new SearchResultProductsView(searchtext.Text, ViewModel.VintageproductListResponses, searchResultModel.Description, searchResultModel.StoreID, ViewModel.searchResponseModel));
                        }
                    }
                    lstProduct.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
