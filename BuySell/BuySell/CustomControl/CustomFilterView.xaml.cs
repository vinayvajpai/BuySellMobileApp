    using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using BuySell.Helper;
using BuySell.Model;
using BuySell.Model.LoginResponse;
using BuySell.Popup;
using BuySell.Utility;
using BuySell.ViewModel;
using BuySell.Views;
using ColorMine.ColorSpaces;
using Newtonsoft.Json.Linq;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using static System.Net.Mime.MediaTypeNames;
using static BuySell.Model.CategoryModel;
using static SQLite.SQLite3;

namespace BuySell.CustomControl
{
    public partial class CustomFilterView : ContentView
    {
        CustomFilterViewModel vm;
        public SubRoots subRoots;
        public SubRoots selectedCategory;
        public string KeyData = string.Empty;
        public event EventHandler<ObservableCollection<FilterModel>> FilterChanged;
        public List<string> SizeList = new List<string>();
        public List<double> SortedSizeList = new List<double>();
        public List<string> BrandList = new List<string>();
        public List<String> ConDbltostr = new List<String>();
        #region Constructor
        public CustomFilterView()
        {
            InitializeComponent();
            BindingContext = vm = new CustomFilterViewModel(this.Navigation);
            BindableLayout.SetItemsSource(filterList, vm.FilterList);

            ClearAllButton.IsVisible = false;
            MessagingCenter.Subscribe<object, bool>("IsTapChangedFilter", "IsTapChangedFilter", (sender, arg) =>
            {
                if (!arg)
                {
                    vm.IsTap = arg;
                }
            });
            MessagingCenter.Subscribe<object, SubRoots>("SelectedGenderCatFilter", "SelectedGenderCatFilter", (sender, arg) =>
            {
                if (arg != null)
                {
                    var str = arg.Root + " | " + arg.NodeTitle + " | " + arg.SubRoot;
                    selectedCategory = arg;
                    subRoots = arg;

                    var CheckOldSelection = vm.FilterList.ToList().Where(p => p.KEY.ToLower() == Constant.CategoryStr.ToLower()).ToList().FirstOrDefault();
                    if (CheckOldSelection.Value != Constant.CategoryStr)
                    {
                        vm.FilterList.Where(x => x.KEY == Constant.SizeStr).ToList().FirstOrDefault().Value = Constant.AllStr;
                        vm.FilterList.Where(x => x.KEY == Constant.SizeStr).ToList().FirstOrDefault().DownVisible = true;
                        //vm.FilterList.Where(x => x.KEY == Constant.SizeStr).ToList().FirstOrDefault().CrossVisible = false;
                    }

                    if (selectedCategory.SubRoot != null)
                    {
                        vm.FilterList.ToList().Where(p => p.KEY == Constant.CategoryStr).ToList().ForEach(p => { p.Value = selectedCategory.Root + " | " + selectedCategory.NodeTitle + " | " + selectedCategory.SubRoot.ToString(); });
                    }
                    else
                    {
                        vm.FilterList.ToList().Where(p => p.KEY == Constant.CategoryStr).ToList().ForEach(p => { p.Value = selectedCategory.Root + " | " + selectedCategory.NodeTitle.ToString(); });
                    }
                    vm.FilterList.Where(x => x.KEY == Constant.CategoryStr).ToList().FirstOrDefault().DownVisible = true;
                    //vm.FilterList.Where(x => x.KEY == "CATEGORY").ToList().FirstOrDefault().CrossVisible = true;
                    ClearAllButton.IsVisible = true;
                    switch (Global.SearchedResultSelectedStore.ToLower())
                    {
                        case "clothing":
                            ClearAllButton.BackgroundColor = Color.FromHex("#1567A6");
                            break;
                        case "streetwear":
                            ClearAllButton.BackgroundColor = Color.FromHex("#D04107");
                            break;
                        case "sneakers":
                            ClearAllButton.BackgroundColor = Color.FromHex("#C52036");
                            break;
                        case "vintage":
                            ClearAllButton.BackgroundColor = Color.FromHex("#467904");
                            break;
                        default:
                            ClearAllButton.BackgroundColor = Color.FromHex("#1567A6");
                            break;
                    }

                    if (FilterChanged != null)
                        FilterChanged.Invoke(sender, vm.FilterList);

                    //Global.Subcategory = null;
                    //TO send the selected filter list
                    MessagingCenter.Send<object, List<FilterModel>>("FilterList", "FilterList", vm.FilterList.ToList());
                }
            });
            MessagingCenter.Subscribe<object, string>("SelectPropertyValue", "SelectPropertyValue", (sender, arg) =>
            {
                if (arg != null)
                {
                    if (KeyData == Constant.ColorStr)
                    {
                        vm.FilterList.ToList().Where(p => p.KEY == Constant.ColorStr).ToList().ForEach(p => { p.Value = arg.ToString(); });
                        vm.FilterList.Where(x => x.KEY == Constant.ColorStr).ToList().FirstOrDefault().DownVisible = true;
                        //vm.FilterList.Where(x => x.KEY == Constant.ColorStr).ToList().FirstOrDefault().CrossVisible = true;
                    }
                    else if (KeyData == Constant.AvailabilityStr)
                    {
                        vm.FilterList.ToList().Where(p => p.KEY == Constant.AvailabilityStr).ToList().ForEach(p => { p.Value = arg.ToString(); });
                        vm.FilterList.Where(x => x.KEY == Constant.AvailabilityStr).ToList().FirstOrDefault().DownVisible = true;
                        //vm.FilterList.Where(x => x.KEY == Constant.AvailabilityStr).ToList().FirstOrDefault().CrossVisible = true;
                    }
                    else if (KeyData == Constant.SortStr)
                    {
                        vm.FilterList.Where(x => x.KEY == Constant.SortStr).ToList().FirstOrDefault().DownVisible = true;
                        //vm.FilterList.Where(x => x.KEY == "SORT").ToList().FirstOrDefault().CrossVisible = true;
                        vm.FilterList.ToList().Where(p => p.KEY == Constant.SortStr).ToList().ForEach(p => { p.Value = arg.ToString(); });
                    }
                    else if (KeyData == Constant.GenderStr)
                    {
                        vm.FilterList.Where(x => x.KEY == Constant.GenderStr).ToList().FirstOrDefault().DownVisible = true;
                        vm.FilterList.ToList().Where(p => p.KEY == Constant.GenderStr).ToList().ForEach(p => { p.Value = arg.ToString(); });
                        var checkCat = vm.FilterList.Where(x => x.KEY == Constant.CategoryStr).ToList().FirstOrDefault();
                        if (checkCat != null)
                        {
                            if (!string.IsNullOrWhiteSpace(checkCat.Value))
                            {
                                if (checkCat.Value.Contains("|"))
                                {
                                    var catArr = checkCat.Value.Split('|');
                                    if (catArr.Length > 1)
                                    {
                                        checkCat.Value = arg + "|" + catArr[1];
                                    }
                                    else if (catArr.Length > 2)
                                    {
                                        checkCat.Value = arg + "|" + catArr[1] + "|" + catArr[2];
                                    }
                                }
                                else
                                {
                                    checkCat.Value = checkCat.PreviousValue;
                                }
                            }
                        }
                    }
                    else if (KeyData == Constant.SizeStr)
                    {
                        vm.FilterList.ToList().Where(p => p.KEY == Constant.SizeStr).ToList().ForEach(p => { p.Value = arg.ToString(); });
                        vm.FilterList.Where(x => x.KEY == Constant.SizeStr).ToList().FirstOrDefault().DownVisible = true;
                        //vm.FilterList.Where(x => x.KEY == Constant.SizeStr).ToList().FirstOrDefault().CrossVisible = true;
                    }
                    else if (KeyData == Constant.PriceStr)
                    {
                        vm.FilterList.ToList().Where(p => p.KEY == Constant.PriceStr).ToList().ForEach(p => { p.Value = arg.ToString(); });
                        vm.FilterList.Where(x => x.KEY == Constant.PriceStr).ToList().FirstOrDefault().DownVisible = true;
                        //vm.FilterList.Where(x => x.KEY == Constant.PriceStr).ToList().FirstOrDefault().CrossVisible = true;
                    }
                    else if (KeyData == Constant.BrandStr)
                    {
                        vm.FilterList.ToList().Where(p => p.KEY == Constant.BrandStr).ToList().ForEach(p => { p.Value = arg.ToString(); });
                        vm.FilterList.Where(x => x.KEY == Constant.BrandStr).ToList().FirstOrDefault().DownVisible = true;
                        //vm.FilterList.Where(x => x.KEY == Constant.BrandStr).ToList().FirstOrDefault().CrossVisible = true;
                    }
                    else if (KeyData == Constant.ConditionStr)
                    {
                        vm.FilterList.ToList().Where(p => p.KEY == Constant.ConditionStr).ToList().ForEach(p => { p.Value = arg.ToString(); });
                        vm.FilterList.Where(x => x.KEY == Constant.ConditionStr).ToList().FirstOrDefault().DownVisible = true;
                        //vm.FilterList.Where(x => x.KEY == Constant.ConditionStr).ToList().FirstOrDefault().CrossVisible = true;
                    }
                    else if (KeyData == Constant.ShippingPriceStr)
                    {
                        vm.FilterList.ToList().Where(p => p.KEY == Constant.ShippingPriceStr).ToList().ForEach(p => { p.Value = arg.ToString(); });
                        vm.FilterList.Where(x => x.KEY == Constant.ShippingPriceStr).ToList().FirstOrDefault().DownVisible = true;
                        //vm.FilterList.Where(x => x.KEY == Constant.ShippingPriceStr).ToList().FirstOrDefault().CrossVisible = true;
                    }
                }

                ClearAllButton.IsVisible = true;
                switch (Global.SearchedResultSelectedStore.ToLower())
                {
                    case "clothing":
                        ClearAllButton.BackgroundColor = Color.FromHex("#1567A6");
                        break;
                    case "streetwear":
                        ClearAllButton.BackgroundColor = Color.FromHex("#D04107");
                        break;
                    case "sneakers":
                        ClearAllButton.BackgroundColor = Color.FromHex("#C52036");
                        break;
                    case "vintage":
                        ClearAllButton.BackgroundColor = Color.FromHex("#467904");
                        break;
                    default:
                        ClearAllButton.BackgroundColor = Color.FromHex("#1567A6");
                        break;
                }

                if (FilterChanged != null)
                    FilterChanged.Invoke(sender, vm.FilterList);

                //TO send the selected filter list
                MessagingCenter.Send<object, List<FilterModel>>("FilterList", "FilterList", vm.FilterList.ToList());
            });
            MessagingCenter.Subscribe<object, SelectSubRootCategory>("SelGenderCatSneakersFilter", "SelGenderCatSneakersFilter", (sender, arg) =>
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
                    selectedCategory = sub;
                    subRoots = sub;

                    var CheckOldSelection = vm.FilterList.ToList().Where(p => p.KEY == Constant.CategoryStr).ToList().FirstOrDefault();
                    if (CheckOldSelection.Value != Constant.CategoryStr)
                    {
                        vm.FilterList.Where(x => x.KEY == Constant.SizeStr).ToList().FirstOrDefault().Value = Constant.AllStr;
                        vm.FilterList.Where(x => x.KEY == Constant.SizeStr).ToList().FirstOrDefault().DownVisible = true;
                        //vm.FilterList.Where(x => x.KEY == Constant.SizeStr).ToList().FirstOrDefault().CrossVisible = false;
                    }
                    //Condition for checking the cateogry filter from shopby category if root is null
                    if (string.IsNullOrEmpty(selectedCategory.Root))
                    {
                        var selecteCatArr = vm.RootSelectedCategory.Split('|');
                        if (selecteCatArr.Length > 1)
                        {
                            vm.FilterList.ToList().Where(p => p.KEY == Constant.CategoryStr).ToList().ForEach(p => { p.Value = selecteCatArr[0] + " | " + selecteCatArr[1].ToTitleCase(TitleCase.First) + " | " + selectedCategory.NodeTitle.ToString(); });
                        }
                    }
                    else
                    {
                        vm.FilterList.ToList().Where(p => p.KEY == Constant.CategoryStr).ToList().ForEach(p => { p.Value = selectedCategory.Root + " | " + selectedCategory.NodeTitle.ToString(); });
                        Global.Subcategory = selectedCategory.NodeTitle.ToString();
                    }

                    vm.FilterList.Where(x => x.KEY == Constant.CategoryStr).ToList().FirstOrDefault().DownVisible = true;
                    //vm.FilterList.Where(x => x.KEY == "CATEGORY").ToList().FirstOrDefault().CrossVisible = true;
                }

                ClearAllButton.IsVisible = true;
                switch (Global.SearchedResultSelectedStore.ToLower())
                {
                    case "clothing":
                        ClearAllButton.BackgroundColor = Color.FromHex("#1567A6");
                        break;
                    case "streetwear":
                        ClearAllButton.BackgroundColor = Color.FromHex("#D04107");
                        break;
                    case "sneakers":
                        ClearAllButton.BackgroundColor = Color.FromHex("#C52036");
                        break;
                    case "vintage":
                        ClearAllButton.BackgroundColor = Color.FromHex("#467904");
                        break;
                    default:
                        ClearAllButton.BackgroundColor = Color.FromHex("#1567A6");
                        break;
                }

                if (FilterChanged != null)
                    FilterChanged.Invoke(sender, vm.FilterList);

                //Global.Subcategory = null;
                //TO send the selected filter list
                MessagingCenter.Send<object, List<FilterModel>>("FilterList", "FilterList", vm.FilterList.ToList());
            });
            vm.ShowAllCat();
        }

        ~CustomFilterView()
        {
            Global.ResetMessagingCenter();
        }
        #endregion

        #region Methods
        //Method created to display list of items  as per filter selected by the user
        private async void FilterSelected(object sender, EventArgs e)
        {
            try
            {
                var filterModel = (FilterModel)((TappedEventArgs)e).Parameter;
                KeyData = filterModel.KEY;
                //Toset data inside global variable which will use to filter recent list data
                Global.SelectedFilter = KeyData;
                var IsFilterApplied = vm.FilterList.Where(x => x.KEY.ToUpper() == KeyData.ToUpper()).FirstOrDefault();
                if (!IsFilterApplied.CrossVisible)
                {

                    switch (KeyData.Trim().ToUpper())
                    {
                        //show list items of sort
                        case "SORT":
                            //SelectPropertiesValuesMethod(1);
                            await GetPicketList(15, TitleName: "Sort");
                            break;

                        ////show list items of gender
                        //case "GENDER":
                        //    await GetPicketList(14, TitleName: "Gender");
                        //    var checkCat = vm.FilterList.Where(x => x.KEY == Constant.CategoryStr).ToList().FirstOrDefault();
                        //    if (checkCat != null)
                        //    {
                        //        checkCat.Value = checkCat.PreviousValue;
                        //    }
                        //    break;

                        //show category list items 
                        case "CATEGORY":

                            //try
                            //{
                            //    if (vm.IsTap)
                            //        return;
                            //    vm.IsTap = true;
                            //    var checkGender = vm.FilterList.Where(x => x.KEY == Constant.GenderStr).ToList().FirstOrDefault();
                            //    if (checkGender != null)
                            //    {
                            //        if ((checkGender.Value.ToLower().Contains("men") || checkGender.Value.ToLower().Contains("women")) && (Navigation.NavigationStack.LastOrDefault().GetType() == typeof(TopTrendingViewAllPage) || Navigation.NavigationStack.LastOrDefault().GetType() == typeof(SearchResultProductsView)))
                            //        {
                            //            await Navigation.PushAsync(new CategoryPage(checkGender.Value, null, ""));
                            //            return;
                            //        }
                            //        else if ((checkGender.Value.ToLower().Contains("men") || checkGender.Value.ToLower().Contains("women")) && Navigation.NavigationStack.LastOrDefault().GetType() == typeof(ProductListByCategoryView))
                            //        {
                            //            if (vm.RootSelectedCategory != null)
                            //            {
                            //                if (vm.RootSelectedCategory.Contains("|"))
                            //                {
                            //                    var selectedCatArr = vm.RootSelectedCategory.Split('|');
                            //                    if (selectedCatArr.Length > 1)
                            //                    {
                            //                        await Navigation.PushAsync(new CategoryPage(selectedCatArr[0].Trim(), selectedCatArr[1].Trim(),""));
                            //                        return;
                            //                    }
                            //                    else
                            //                    {
                            //                        await Navigation.PushAsync(new CategoryPage(selectedCatArr[0].Trim(), null,""));
                            //                        return;
                            //                    }
                            //                }
                            //            }
                            //        }
                            //        else if (Navigation.NavigationStack.LastOrDefault().GetType() == typeof(MyFavoritesPage) || Navigation.NavigationStack.LastOrDefault().GetType() == typeof(SellerClosetView)) 
                            //        {
                            //            await Navigation.PushAsync(new CategoryPage(checkGender.Value, null, "MyFavoriteOrMyStore"));
                            //            return;
                            //        }
                            //        else
                            //        {
                            //            await Navigation.PushAsync(new CategoryPage(null, null,""));
                            //            return;
                            //        }
                            //    }
                            //    else
                            //    {
                            //        vm.IsTap = false;
                            //        UserDialogs.Instance.Alert("Please select a Gender first");
                            //    }
                            try
                            {
                                if (vm.IsTap)
                                    return;
                                vm.IsTap = true;
                                #region Condtions for set the categories to all if it is on search and closet screen
                                if (Navigation.NavigationStack.LastOrDefault().GetType() == typeof(SearchResultProductsView))
                                {
                                    await Navigation.PushAsync(new CategoryPage(null, Global.SearchResultSelStore, Global.SearchResultSelStore));
                                    return;
                                }
                                if (Navigation.NavigationStack.LastOrDefault().GetType() == typeof(SellerClosetView))
                                {
                                    await Navigation.PushAsync(new CategoryPage(null, null, Global.StoreIndex.ToString()));
                                    return;
                                }
                                if (Navigation.NavigationStack.LastOrDefault().GetType() == typeof(MyFavoritesPage))
                                {
                                    await Navigation.PushAsync(new CategoryPage(null, null, Global.StoreIndex.ToString()));
                                    return;
                                }
                                #endregion
                                #region//Condition for select the category according to Root or its category
                                if (vm.RootSelectedCategory != null)
                                {
                                    if (vm.RootSelectedCategory.Contains("|"))
                                    {
                                        var selectedCatArr = vm.RootSelectedCategory.Split('|');
                                        if (selectedCatArr.Length > 1)
                                        {
                                            await Navigation.PushAsync(new CategoryPage(selectedCatArr[0].Trim(), selectedCatArr[1].Trim(),Global.StoreIndex.ToString()));
                                            return;
                                        }
                                        else
                                        {
                                            await Navigation.PushAsync(new CategoryPage(selectedCatArr[0].Trim(), null, Global.StoreIndex.ToString()));
                                            return;
                                        }
                                    }
                                }
                                #endregion

                                Global.Subcategory = null;
                                await Navigation.PushAsync(new CategoryPage(Global.GenderParam, Global.Subcategory,Global.StoreIndex.ToString()));
                                return;
                            }
                            catch (Exception ex)
                            {
                                vm.IsTap = false;
                                Debug.WriteLine(ex.Message);
                            }

                            break;
                        //show sizes list items based on category selected by the user
                        case "SIZE":
                            //var CheckCat = vm.FilterList.Where(x => x.KEY == Constant.CategoryStr).ToList().FirstOrDefault();
                            ////Condition for checking All category (Clothing, Streetwear and Vintage) sizes
                            ////Check with Global.SearchedResultSelectedStore in order to open category and size list of store which is selected at search result page.
                            //if (CheckCat != null && CheckCat.Value.Contains("|") && Global.SearchedResultSelectedStore.ToLower() != Constant.SneakersStr.ToLower())
                            //{
                            //    int seperaterCount = CheckCat.Value.ToCharArray().Count(c => c == '|');
                            //    if (seperaterCount >= 2)
                            //    {
                            //        MockProductData.Instance.GetCategoryList();
                            //        var catArray = CheckCat.Value.Split('|');
                            //        if (Navigation.NavigationStack.LastOrDefault().GetType() == typeof(SearchResultProductsView))
                            //        {
                            //        }
                            //        else if (Navigation.NavigationStack.LastOrDefault().GetType() == typeof(SellerClosetView))
                            //        {
                            //        }
                            //        else if (catArray.Length > 2)
                            //        {
                            //            subRoots = new SubRoots() { SubRoot = catArray[2].Trim().ToTitleCase(TitleCase.First), Gender = Global.GenderIndex == 1 ? "M" : "F", Root = Global.GenderIndex == 1 ? "Men" : "Women", NodeTitle = catArray[1].Trim().ToTitleCase(TitleCase.First) };
                            //        }
                            //        await GetPicketList(7, TitleName: "Size");
                            //        return;
                            //    }
                            //    //Condition for getting the value of subcategory if subcategory is null and select size
                            //    var selecteCatArr = CheckCat.Value.Split('|');
                            //    if (selecteCatArr.Length > 1)
                            //    {
                            //        MockProductData.Instance.GetCategoryList();
                            //        subRoots = new SubRoots() { SubRoot = null, Gender = Global.GenderIndex == 1 ? "M" : "F", Root = Global.GenderIndex == 1 ? "Men" : "Women", NodeTitle = selecteCatArr[1].Trim().ToTitleCase(TitleCase.First) };
                            //        await GetPicketList(7, TitleName: "Size");
                            //    }
                            //}
                            //else
                            //{
                            //    //Condition for checking sneakers size
                            //    if (CheckCat == null)
                            //    {
                            //        MockProductData.Instance.GetSneakesCatList();
                            //        subRoots = new SubRoots() { SubRoot = Global.Subcategory.Trim(), Gender = Global.GenderIndex == 1 ? "M" : "F", Root = Global.GenderIndex == 1 ? "Men" : "Women", NodeTitle = Global.Subcategory.Trim().ToTitleCase(TitleCase.First) };
                            //        await GetPicketList(7, TitleName: "Size");
                            //    }
                            //    else if (CheckCat != null && CheckCat.Value.ToLower().Contains("|") && Global.Subcategory != null)
                            //    {
                            //        MockProductData.Instance.GetSneakesCatList();
                            //        subRoots = new SubRoots() { SubRoot = Global.Subcategory.Trim(), Gender = Global.GenderIndex == 1 ? "M" : "F", Root = Global.GenderIndex == 1 ? "Men" : "Women", NodeTitle = Global.Subcategory.Trim().ToTitleCase(TitleCase.First) };
                            //        await GetPicketList(7, TitleName: "Size");

                            //        if (!CheckCat.Value.ToLower().Contains("|"))
                            //            Global.Subcategory = null;

                            //        return;
                            //    }
                            //    else
                            //    {
                            //        vm.IsTap = false;
                            //        UserDialogs.Instance.Alert("Please select a Category first");
                            //    }
                            //}
                            //SelectPickerList = new ObservableCollection<string>(SizeList.OrderBy(x => x).ToList());
                            var sortedValues = SizeList.Where(x => double.TryParse(x, out _)).Select(double.Parse).ToList().OrderBy(x => x);
                            SortedSizeList = ((List<double>)(IEnumerable<double>)sortedValues.ToList());
                            if(ConDbltostr.Count > 0)
                            {
                                ConDbltostr.Clear();
                            }
                            foreach(var item in SortedSizeList)
                            {
                                ConDbltostr.Add(item.ToString());
                            }
                            if (ConDbltostr.Count == SizeList.Count)
                            {
                                SelectPickerList = new ObservableCollection<string>(ConDbltostr.ToList());
                            }
                            else
                            {
                                SelectPickerList = new ObservableCollection<string>(ConDbltostr.Union(SizeList).ToList());
                            }
                            SelectPickerList.Insert(0, Constant.AllStr);
                            var productPropertyView = new ProductPropertyListView("Size", true);
                            productPropertyView.BindingContext = this;
                            await Navigation.PushAsync(productPropertyView);
                            break; 
                        //show brand list items 
                        case "BRAND":
                            // await GetPicketList(8, TitleName: "Brand");

                            try
                            {
                                if (vm.IsTap)
                                    return;
                                vm.IsTap = true;
                                await Navigation.PushAsync(new BrandPage(BrandList));
                                vm.IsTap = false;
                                return;
                            }
                            catch (Exception ex)
                            {
                                vm.IsTap = false;
                                Debug.WriteLine(ex.Message);
                            }
                            break;
                        //show other filter list items 
                        case "COLOR":
                            await GetPicketList(9, TitleName: "Color");
                            break;

                        case "CONDITION":
                            await GetPicketList(10, TitleName: "Condition");
                            break;

                        case "AVAILABILITY":
                            await GetPicketList(11, TitleName: "Availability");
                            break;

                        case "PRICE":
                            await GetPicketList(16, TitleName: "Price");
                            break;

                        case "SHIPPING PRICE":
                            await GetPicketList(13, TitleName: "Shipping Price");
                            break;

                        default:
                            UserDialogs.Instance.Alert("Filter Not Implemented");
                            break;
                    }
                }
                else
                {
                    vm.IsTap = false;
                    vm.FilterList.Where(x => x.KEY.ToUpper() == KeyData.ToUpper()).FirstOrDefault().Value = KeyData;
                    vm.FilterList.Where(x => x.KEY.ToUpper() == KeyData.ToUpper()).FirstOrDefault().DownVisible = true;
                    vm.FilterList.Where(x => x.KEY.ToUpper() == KeyData.ToUpper()).FirstOrDefault().CrossVisible = false;

                    foreach (var x in vm.FilterList)
                    {
                        if (x.KEY != x.Value)
                        {
                            ClearAllButton.IsVisible = true;
                            break;
                        }
                        else
                        {
                            ClearAllButton.IsVisible = false;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                vm.IsTap = false;
                Debug.Write(ex.Message);
            }
        }

       

        //select list items based on thier index and title
        public async Task GetPicketList(int index, string TitleName)
        {
            try
            {
                //if (vm.IsTap)
                //    return;
                //vm.IsTap = true;
                //SelectedPropertyIndex = index;
                SelectPickerList = MockProductData.Instance.GetAddItemOtherFieldData(index, subRoots);
                SelectPickerList.Insert(0, Constant.AllStr);
                var productPropertyView = new ProductPropertyListView(TitleName, true);
                productPropertyView.BindingContext = this;
                await Navigation.PushAsync(productPropertyView);
            }
            catch (Exception ex)
            {
                vm.IsTap = false;
                Debug.WriteLine(ex.Message);
            }
        }

        //Method created to set and reset values inside filter's view on clear button tapped
        public void ClearAllTapped(object sender, EventArgs e)
        {
            try
            {
                vm.FilterList.Clear();
                vm.FilterList.Add(new FilterModel() { KEY = Constant.SortStr, Value = Constant.AllStr, PreviousValue = Constant.AllStr, CrossVisible = false, DownVisible = true, IsKeyVisible = true });
                //vm.FilterList.Add(new FilterModel() { KEY = Constant.GenderStr, Value = vm.selGender, PreviousValue = Constant.AllStr, CrossVisible = false, DownVisible = true, IsKeyVisible = false });
                
                
                if ((Global.Subcategory != null && Global.StoreIndex == 2))
                {
                    if (Navigation.NavigationStack.LastOrDefault().GetType() == typeof(TopTrendingViewAllPage))
                    {
                        vm.FilterList.Add(new FilterModel()
                        {
                            KEY = Constant.CategoryStr,
                            Value = vm.IsShowCatAll ? Constant.AllStr : Global.GenderParam,
                            PreviousValue = vm.IsShowCatAll ? Constant.AllStr : Global.GenderParam,
                            CrossVisible = false,
                            DownVisible = true,
                            IsKeyVisible = true

                        });
                    }
                    if (Navigation.NavigationStack.LastOrDefault().GetType() == typeof(SearchResultProductsView))
                    {
                        vm.FilterList.Add(new FilterModel()
                        {
                            KEY = Constant.CategoryStr,
                            Value = vm.RootSelectedCategory,
                            PreviousValue = vm.RootSelectedCategory,
                            CrossVisible = false,
                            DownVisible = true,
                            IsKeyVisible = true
                        });
                    }
                    if (Navigation.NavigationStack.LastOrDefault().GetType() == typeof(SellerClosetView))
                    {
                        vm.FilterList.Add(new FilterModel()
                        {
                            KEY = Constant.CategoryStr,
                            Value = vm.RootSelectedCategory,
                            PreviousValue = vm.RootSelectedCategory,
                            CrossVisible = false,
                            DownVisible = true,
                            IsKeyVisible = true
                        });
                    }
                }
                else if((Global.Subcategory != null && Global.StoreIndex != 2) || (Global.Subcategory == null))
                {
                    vm.FilterList.Add(new FilterModel()
                    {
                        KEY = Constant.CategoryStr,
                        Value = vm.RootSelectedCategory,//vm.IsShowCatAll ? "All" : (Global.GenderParam + (string.IsNullOrEmpty(Global.Subcategory) ? "" : " | " + Global.Subcategory)),
                        PreviousValue = vm.RootSelectedCategory,
                        CrossVisible = false,
                        DownVisible = true,
                        IsKeyVisible = true
                    });
                }
                vm.FilterList.Add(new FilterModel() { KEY = Constant.SizeStr, Value = Constant.AllStr, PreviousValue = Constant.AllStr, CrossVisible = false, DownVisible = true, IsKeyVisible = true });
                vm.FilterList.Add(new FilterModel() { KEY = Constant.BrandStr, Value = Constant.AllStr, PreviousValue = Constant.AllStr, CrossVisible = false, DownVisible = true, IsKeyVisible = true });
                vm.FilterList.Add(new FilterModel() { KEY = Constant.ColorStr, Value = Constant.AllStr, PreviousValue = Constant.AllStr, CrossVisible = false, DownVisible = true, IsKeyVisible = true });
                vm.FilterList.Add(new FilterModel() { KEY = Constant.ConditionStr, Value = Constant.AllStr, PreviousValue = Constant.AllStr, CrossVisible = false, DownVisible = true, IsKeyVisible = true });
                vm.FilterList.Add(new FilterModel() { KEY = Constant.AvailabilityStr, Value = Constant.AllStr, PreviousValue = Constant.AllStr, CrossVisible = false, DownVisible = true, IsKeyVisible = true });
                vm.FilterList.Add(new FilterModel() { KEY = Constant.PriceStr, Value = Constant.AllStr, PreviousValue = Constant.AllStr, CrossVisible = false, DownVisible = true, IsKeyVisible = true });
                vm.FilterList.Add(new FilterModel() { KEY = Constant.ShippingPriceStr, Value = Constant.AllStr, PreviousValue = Constant.AllStr, CrossVisible = false, DownVisible = true, IsKeyVisible = true });
                ClearAllButton.IsVisible = false;
                vm.IsTap = false;
                MessagingCenter.Send<object, List<FilterModel>>("FilterList", "FilterList", vm.FilterList.ToList());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }
        private static void IsShowCatAllPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomFilterView)bindable;
            var viewModel = (CustomFilterViewModel)control.BindingContext;
            viewModel.IsShowCatAll = true;
            viewModel.ShowAllCat();
        }
        #endregion

        #region BindableProperty

        public static BindableProperty IsShowFilterCountProperty =
          BindableProperty.Create(nameof(IsShowFilterCount), typeof(bool), typeof(CustomFilterView), defaultValue: true, propertyChanged: IsShowFilterCountPropertyChanged);
        public bool IsShowFilterCount
        {
            get => (bool)GetValue(IsShowFilterCountProperty);
            set => SetValue(IsShowFilterCountProperty, value);
        }

        public static readonly BindableProperty ThemeColorColorProperty =
            BindableProperty.Create(nameof(ThemeColor), typeof(Color), typeof(CustomFilterView), Color.Blue,
                propertyChanged: OnThemeColorChanged);
        public Color ThemeColor
        {
            get { return (Color)GetValue(ThemeColorColorProperty); }
            set { SetValue(ThemeColorColorProperty, value); }
        }

        private static void OnThemeColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = bindable as CustomFilterView;
            if (control == null)
            {
                return;
            }
            control.boxViewColor.BackgroundColor = (Color)newValue;
        }


        private static void IsShowFilterCountPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomFilterView)bindable;
            control.lblFilterCount.IsVisible = Convert.ToBoolean(newValue);
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
        public static BindableProperty IsCatAllProperty =
              BindableProperty.Create(nameof(IsShowCatAll), typeof(bool), typeof(CustomFilterView), defaultValue: false, propertyChanged: IsShowCatAllPropertyChanged);
        public bool IsShowCatAll
        {
            get => (bool)GetValue(IsCatAllProperty);
            set => SetValue(IsCatAllProperty, value);
        }
        #endregion

    }
}

