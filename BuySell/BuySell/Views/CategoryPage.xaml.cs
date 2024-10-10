using System;
using System.Collections.Generic;
using System.Diagnostics;
using BuySell.Helper;
using BuySell.ViewModel;
using Newtonsoft.Json;
using Xamarin.Forms;
using static BuySell.Model.CategoryModel;

namespace BuySell.Views
{
    public partial class CategoryPage : ContentPage
    {
        CategoryViewModel vm;
        string selectedcategory;
        string selectedsubcategory;
        bool IsFilter = false;

        #region Constructors
        public CategoryPage()
        {
            InitializeComponent();
            BindingContext = titleView.BindingContext = vm = new CategoryViewModel();
            rootslist.IsVisible = true;
            rootslistfromfilter.IsVisible = false;
        }
        public CategoryPage(string selectedcat, string selectedsubcat,string storeID)
        {
            InitializeComponent();
            IsFilter = true;
            if (selectedcat == "All")
            {
                BindingContext = titleView.BindingContext = vm = new CategoryViewModel(selectedcat, selectedsubcat, storeID);
            }
            else
            {
                BindingContext = titleView.BindingContext = vm = new CategoryViewModel(selectedcat, selectedsubcat, storeID);
            }
            selectedcategory = selectedcat;
            selectedsubcategory = selectedsubcat;
            rootslistfromfilter.IsVisible = true;
            rootslist.IsVisible = false;
        }
        #endregion

        //Method created to send main category using messaging center selected by user from men or women
        private void SelectCategory_Tapped(object sender, EventArgs e)
        {
            try
            {
                if (vm.IsTap)
                    return;
                vm.IsTap = true;
                var parameter = ((SubRoots)((TappedEventArgs)e).Parameter);
                //Condition for checking the call category page either from add item or select filter

                if (!IsFilter)
                    MessagingCenter.Send<object, SubRoots>("SelectedGenderCat", "SelectedGenderCat", parameter);
                else
                    MessagingCenter.Send<object, SubRoots>("SelectedGenderCatFilter", "SelectedGenderCatFilter", parameter);

                Navigation.PopAsync(true);

            }
            catch (Exception ex)
            {
                vm.IsTap = false;
                Debug.WriteLine(ex.Message);
            }
        }
        //Method created to send sub category using messaging center selected by user from men or women
        void SelectCatRootNode_Tapped(System.Object sender, System.EventArgs e)
        {
            try
            {
                if (vm.IsTap)
                    return;
                vm.IsTap = true;

                var parameter = (((TappedEventArgs)e).Parameter).ToDictionary<List<SubRoots>>();
                {
                    if (parameter != null && parameter.Value.Count >= 0)
                    {
                        if (!IsFilter)
                            MessagingCenter.Send<object, SelectSubRootCategory>("SelGenderCatSneakers", "SelGenderCatSneakers", parameter);
                        else
                            MessagingCenter.Send<object, SelectSubRootCategory>("SelGenderCatSneakersFilter", "SelGenderCatSneakersFilter", parameter);

                        MessagingCenter.Send<object, bool>("IsTapChanged", "IsTapChanged", false);
                        MessagingCenter.Send<object, bool>("IsTapChangedFilter", "IsTapChangedFilter", false);
                        Navigation.PopAsync(true);
                    }
                    else
                    {
                        vm.IsTap = false;
                    }
                }
            }
            catch (Exception ex)
            {
                vm.IsTap = false;
                Debug.WriteLine(ex.Message);
            }
        }
        protected override void OnAppearing()
        {
            vm.IsTap = false;
            vm.navigation = this.Navigation;
            base.OnAppearing();
        }
        protected override void OnDisappearing()
        {
            vm.IsTap = false;
            MessagingCenter.Send<object, bool>("IsTapChangedFilter", "IsTapChangedFilter", false);
            base.OnDisappearing();
        }
    }
    public static class ConvertToDictionary
    {
        public static SelectSubRootCategory ToDictionary<TValue>(this object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            var dictionary = JsonConvert.DeserializeObject<SelectSubRootCategory>(json);
            return dictionary;
        }
    }
}
