using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
using BuySell.Helper;
using BuySell.Model;
using BuySell.Model.RestResponse;
using BuySell.Utility;
using BuySell.View;
using BuySell.WebServices;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace BuySell.ViewModel
{
    public class ProductListByCategoryViewModel : BaseViewModel
    {
        INavigation navigation;
        MockProductData mockProductData;
        public int PageNumber = 1;
        //public int PageSize = 20;
        public  ProductPagingListResponse productPagingListResponse;
        string filterCategory;
        string categoryID = "0";
        int GenderIndex = Global.GenderIndex;
        public ProductFilterModel productFilterModel;
        #region Constructor
        public ProductListByCategoryViewModel(INavigation _nav, string category, List<DashBoardModel> dashBoardModels)
        {
            navigation = _nav;
            filterCategory = category;
            mockProductData = new MockProductData();
            ItemTreshold = 1;
            ItemTresholdReachedCommand = new Command(async () => await ItemsTresholdReached());
            Task.Run(async () =>
            {
                await CallGetProductListByCategoryMethod();
            });
        }
        public ProductListByCategoryViewModel(INavigation _nav, string category, string categoryID, List<DashBoardModel> dashBoardModels)
        {
            this.categoryID = categoryID;
            navigation = _nav;
            filterCategory = category;
            Global.Subcategory= category;
            mockProductData = new MockProductData();
            ItemTreshold = 1;
            ItemTresholdReachedCommand = new Command(async () => await ItemsTresholdReached());
            Task.Run(async () =>
            {
                await CallGetProductListByCategoryMethod();
            });
        }
        #endregion

        #region Properties
        private ObservableCollection<string> _sizeByProductList = new ObservableCollection<string>();
        public ObservableCollection<string> sizeByProductList
        {
            get { return _sizeByProductList; }
            set { _sizeByProductList = value; OnPropertyChanged("sizeByProductList"); }
        }
        private ObservableCollection<string> _brandList = new ObservableCollection<string>();
        public ObservableCollection<string> brandList
        {
            get { return _brandList; }
            set { _brandList = value; OnPropertyChanged("brandList"); }
        }
        public int _PageSize = 20;
        public int PageSize
        {
            get
            {
                return _PageSize;
            }
            set
            {
                _PageSize = value;
                OnPropertyChanged("PageSize");
            }
        }

        public bool _IsFilterChanged = true;
        public bool IsFilterChanged
        {
            get
            {
                return _IsFilterChanged;
            }
            set
            {
                _IsFilterChanged = value;
                OnPropertyChanged("IsFilterChanged");
            }
        }

        private bool _IsLoadMore;
        public bool IsLoadMore
        {
            get => _IsLoadMore;
            set
            {
                _IsLoadMore = value;
                OnPropertyChanged("IsLoadMore");
            }
        }

        private int _itemTreshold;
        public int ItemTreshold
        {
            get { return _itemTreshold; }
            set { SetProperty(ref _itemTreshold, value); }
        }

        private ObservableCollection<DashBoardModel> _ProductList = new ObservableCollection<DashBoardModel>();
        public ObservableCollection<DashBoardModel> ProductList
        {
            get { return _ProductList; }
            set { _ProductList = value; OnPropertyChanged("ProductList"); }
        }

        private ObservableCollection<DashBoardModel> _FilterProductList = new ObservableCollection<DashBoardModel>();
        public ObservableCollection<DashBoardModel> FilterProductList
        {
            get { return _FilterProductList; }
            set { _FilterProductList = value; OnPropertyChanged("FilterProductList"); }
        }

        private bool _IsShowEmpty = false;
        public bool IsShowEmpty
        {
            get { return _IsShowEmpty; }
            set { _IsShowEmpty = value; OnPropertyChanged("IsShowEmpty"); }
        }

        private bool _IsShowMore = false;
        public bool IsShowMore
        {
            get { return _IsShowMore; }
            set { _IsShowMore = value; OnPropertyChanged("IsShowMore"); }
        }

        private bool _ShowPreviousButton = false;
        public bool ShowPreviousButton
        {
            get { return _ShowPreviousButton; }
            set { _ShowPreviousButton = value; OnPropertyChanged("ShowPreviousButton"); }
        }

        private bool _ShowNextButton = false;
        public bool ShowNextButton
        {
            get { return _ShowNextButton; }
            set { _ShowNextButton = value; OnPropertyChanged("ShowNextButton"); }
        }

        private bool _ShowPreNext = false;
        public bool ShowPreNext
        {
            get { return _ShowPreNext; }
            set { _ShowPreNext = value; OnPropertyChanged("ShowPreNext"); }
        }

        #endregion

        #region Commands
        public Command ItemTresholdReachedCommand { get; set; }
        private Command _TopTrandingItemsCommand;
        public Command TopTrandingItemsCommand
        {
            get
            {
                return _TopTrandingItemsCommand ?? (_TopTrandingItemsCommand = new Command(async (obj) => await ExicuteTopTrandingCommond(obj)));
            }
        }
        private async Task ExicuteTopTrandingCommond(object obj)
        {
            if (IsTap)
                return;
            IsTap = true;
            Global.SetRecentViewList((DashBoardModel)obj);
            await navigation.PushAsync(new ItemDetailsPage((DashBoardModel)obj, false));
        }

        private Command _LoadMoreCommand;
        public Command LoadMoreCommand
        {
            get
            {
                return _LoadMoreCommand ?? (_LoadMoreCommand = new Command(async (index) =>
                {
                    if (productPagingListResponse != null)
                    {
                        if (productPagingListResponse.Data.HasNextPage)
                        {
                            IsBusy = true;
                            await Task.Delay(100);
                            PageNumber = productPagingListResponse.Data.NextPageNumber;
                            await GetProductListWithPaginationMethod();
                        }
                        else
                        {
                            IsShowMore = false;
                        }
                        //Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                    }
                }
                ));
            }
        }

        #endregion

        #region Methods
        public void GetTopTrendingItemsListMethod(string category)
        {
            try
            {
               //Condition added to show list of products at top trending view all page based on filter and store selected at Dashboard page

                if (Global.GenderParam != null)
                {
                    if (!Global.GenderParam.ToLower().Equals("all"))
                    {
                        if (Global.GenderParam.ToLower() == "men")
                        {
                            switch (category)
                            {
                                case "SHIRTS":
                                    if (category.Equals("Shirts".ToUpper()))
                                    {
                                        FilterProductList = new ObservableCollection<DashBoardModel>(ProductList.ToList().Where(p =>
                                          ((p.ProductCategory.ToLower().Contains(category.ToLower()) || p.ProductCategory.ToLower().Contains("Sweaters".ToLower()))
                                          || (p.ParentCategory.ToLower().Contains(category.ToLower())) || p.ParentCategory.ToLower().Contains("Sweaters".ToLower()))
                                          && p.GenderFullName.ToLower().Equals(Global.GenderParam.ToLower())
                                          && p.StoreType.ToLower() == Global.Storecategory.ToLower()));
                                    }
                                    break;

                                case "BOTTOMS":
                                    if (category.Equals("Bottoms".ToUpper()))
                                    {
                                        FilterProductList = new ObservableCollection<DashBoardModel>(ProductList.ToList().Where(p =>
                                          ((p.ProductCategory.ToLower().Contains(category.ToLower()) || p.ProductCategory.ToLower().Contains("Pants".ToLower()) || p.ProductCategory.ToLower().Contains("Shorts".ToLower()) || p.ProductCategory.ToLower().Contains("Jeans".ToLower()))
                                          || (p.ParentCategory.ToLower().Contains(category.ToLower())) || p.ParentCategory.ToLower().Contains("Pants".ToLower()) || p.ParentCategory.ToLower().Contains("Shorts".ToLower()) || p.ParentCategory.ToLower().Contains("Jeans".ToLower()))
                                          && p.GenderFullName.ToLower().Equals(Global.GenderParam.ToLower())
                                          && p.StoreType.ToLower() == Global.Storecategory.ToLower()));
                                    }
                                    break;

                                case "COATS":
                                    if (category.Equals("Coats".ToUpper()))
                                    {
                                        FilterProductList = new ObservableCollection<DashBoardModel>(ProductList.ToList().Where(p =>
                                          ((p.ProductCategory.ToLower().Contains(category.ToLower()) || p.ProductCategory.ToLower().Contains("Jackets & Coats".ToLower()))
                                          || (p.ParentCategory.ToLower().Contains(category.ToLower())) || p.ParentCategory.ToLower().Contains("Jackets & Coats".ToLower()))
                                          && p.GenderFullName.ToLower().Equals(Global.GenderParam.ToLower())
                                          && p.StoreType.ToLower() == Global.Storecategory.ToLower()));
                                    }
                                    break;


                                case "SUITS":
                                    if (category.Equals("Suits".ToUpper()))
                                    {
                                        FilterProductList = new ObservableCollection<DashBoardModel>(ProductList.ToList().Where(p =>
                                          ((p.ProductCategory.ToLower().Contains(category.ToLower()) || p.ProductCategory.ToLower().Contains("Suits & Tuxedos".ToLower()))
                                          || (p.ParentCategory.ToLower().Contains(category.ToLower())) || p.ParentCategory.ToLower().Contains("Suits & Tuxedos".ToLower()))
                                          && p.GenderFullName.ToLower().Equals(Global.GenderParam.ToLower())
                                          && p.StoreType.ToLower() == Global.Storecategory.ToLower()));
                                    }
                                    break;

                                case "SOCKS":
                                    if (category.Equals("Socks".ToUpper()))
                                    {
                                        FilterProductList = new ObservableCollection<DashBoardModel>(ProductList.ToList().Where(p =>
                                          ((p.ProductCategory.ToLower().Contains(category.ToLower()) || p.ProductCategory.ToLower().Contains("Socks & Underwear".ToLower()))
                                          || (p.ParentCategory.ToLower().Contains(category.ToLower())) || p.ParentCategory.ToLower().Contains("Socks & Underwear".ToLower()))
                                          && p.GenderFullName.ToLower().Equals(Global.GenderParam.ToLower())
                                          && p.StoreType.ToLower() == Global.Storecategory.ToLower()));
                                    }
                                    break;

                                default:
                                    FilterProductList = new ObservableCollection<DashBoardModel>(ProductList.ToList().Where(p => (p.ProductCategory.ToLower().Contains(category.ToLower()) || p.ParentCategory.ToLower().Contains(category.ToLower())) && p.GenderFullName.ToLower().Equals(Global.GenderParam.ToLower()) && p.StoreType.ToLower() == Global.Storecategory.ToLower()));
                                    break;
                            }
                        }
                        if (Global.GenderParam.ToLower() == "women")
                        {
                            switch (category)
                            {
                                case "TOPS":
                                    if (category.Equals("Tops".ToUpper()))
                                    {
                                        FilterProductList = new ObservableCollection<DashBoardModel>(ProductList.ToList().Where(p =>
                                          ((p.ProductCategory.ToLower().Contains(category.ToLower()) || p.ProductCategory.ToLower().Contains("Sweaters".ToLower()) || p.ProductCategory.ToLower().Contains("Sweaters & Tops".ToLower()) || p.ProductCategory.ToLower().Contains("Skirts".ToLower()))
                                          || (p.ParentCategory.ToLower().Contains(category.ToLower())) || p.ParentCategory.ToLower().Contains("Sweaters".ToLower()) || p.ParentCategory.ToLower().Contains("Sweaters & Tops".ToLower()) || p.ParentCategory.ToLower().Contains("Skirts".ToLower()))
                                          && p.GenderFullName.ToLower().Equals(Global.GenderParam.ToLower())
                                          && p.StoreType.ToLower() == Global.Storecategory.ToLower()));
                                    }
                                    break;
                                case "BOTTOMS":
                                    if (category.Equals("Bottoms".ToUpper()))
                                    {
                                        FilterProductList = new ObservableCollection<DashBoardModel>(ProductList.ToList().Where(p =>
                                          ((p.ProductCategory.ToLower().Contains(category.ToLower()) || p.ProductCategory.ToLower().Contains("Pants".ToLower()) || p.ProductCategory.ToLower().Contains("Shorts".ToLower()) || p.ProductCategory.ToLower().Contains("Skirts".ToLower()) || p.ProductCategory.ToLower().Contains("Jeans".ToLower()))
                                          || (p.ParentCategory.ToLower().Contains(category.ToLower())) || p.ParentCategory.ToLower().Contains("Pants".ToLower()) || p.ParentCategory.ToLower().Contains("Shorts".ToLower()) || p.ParentCategory.ToLower().Contains("Skirts".ToLower()) || p.ParentCategory.ToLower().Contains("Jeans".ToLower()))
                                          && p.GenderFullName.ToLower().Equals(Global.GenderParam.ToLower())
                                          && p.StoreType.ToLower() == Global.Storecategory.ToLower()));
                                    }
                                    break;

                                case "COATS":
                                    if (category.Equals("Coats".ToUpper()))
                                    {
                                        FilterProductList = new ObservableCollection<DashBoardModel>(ProductList.ToList().Where(p =>
                                          ((p.ProductCategory.ToLower().Contains(category.ToLower()) || p.ProductCategory.ToLower().Contains("Jackets & Coats".ToLower()))
                                          || (p.ParentCategory.ToLower().Contains(category.ToLower())) || p.ParentCategory.ToLower().Contains("Jackets & Coats".ToLower()))
                                          && p.GenderFullName.ToLower().Equals(Global.GenderParam.ToLower())
                                          && p.StoreType.ToLower() == Global.Storecategory.ToLower()));
                                    }
                                    break;

                                case "SUITS":
                                    if (category.Equals("Suits".ToUpper()))
                                    {
                                        FilterProductList = new ObservableCollection<DashBoardModel>(ProductList.ToList().Where(p =>
                                          ((p.ProductCategory.ToLower().Contains(category.ToLower()) || p.ProductCategory.ToLower().Contains("Suits & Separates".ToLower()) || p.ProductCategory.ToLower().Contains("Separates".ToLower()))
                                          || (p.ParentCategory.ToLower().Contains(category.ToLower())) || p.ParentCategory.ToLower().Contains("Suits & Separates".ToLower()) || p.ParentCategory.ToLower().Contains("Separates".ToLower()))
                                          && p.GenderFullName.ToLower().Equals(Global.GenderParam.ToLower())
                                          && p.StoreType.ToLower() == Global.Storecategory.ToLower()));
                                    }
                                    break;

                                case "OTHER":
                                    if (category.Equals("Other".ToUpper()))
                                    {
                                        FilterProductList = new ObservableCollection<DashBoardModel>(ProductList.ToList().Where(p =>
                                          ((p.ProductCategory.ToLower().Contains(category.ToLower()) || p.ProductCategory.ToLower().Contains("Sleepwear & Intimates".ToLower()) || p.ProductCategory.ToLower().Contains("Sleepwear".ToLower()) || p.ProductCategory.ToLower().Contains("Intimates".ToLower()))
                                          || (p.ParentCategory.ToLower().Contains(category.ToLower())) || p.ParentCategory.ToLower().Contains("Sleepwear & Intimates".ToLower()) || p.ParentCategory.ToLower().Contains("Sleepwear".ToLower()) || p.ParentCategory.ToLower().Contains("Intimates".ToLower()))
                                          && p.GenderFullName.ToLower().Equals(Global.GenderParam.ToLower())
                                          && p.StoreType.ToLower() == Global.Storecategory.ToLower()));
                                    }
                                    break;

                                default:
                                    FilterProductList = new ObservableCollection<DashBoardModel>(ProductList.ToList().Where(p => (p.ProductCategory.ToLower().Contains(category.ToLower()) || p.ParentCategory.ToLower().Contains(category.ToLower())) && p.GenderFullName.ToLower().Equals(Global.GenderParam.ToLower()) && p.StoreType.ToLower() == Global.Storecategory.ToLower()));
                                    break;
                            }
                        }
                        //    //FilterProductList = new ObservableCollection<DashBoardModel>(ProductList.ToList().Where(p => (p.ProductCategory.ToLower().Contains(category.ToLower()) || p.ParentCategory.ToLower().Contains(category.ToLower())) && p.GenderFullName.ToLower().Equals(Global.GenderParam.ToLower()) && p.StoreType.ToLower() == Global.Storecategory.ToLower()));
                        //}
                        //else
                        //    FilterProductList = new ObservableCollection<DashBoardModel>(ProductList.ToList().Where(p => (p.ProductCategory.ToLower().Contains(category.ToLower()) || p.ParentCategory.ToLower().Contains(category.ToLower()) && p.StoreType.ToLower() == Global.Storecategory.ToLower())));
                    }
                    else 
                    {
                        switch (category)
                        {
                            case "BOTTOMS":
                                if (category.Equals("Bottoms".ToUpper()))
                                {
                                    FilterProductList = new ObservableCollection<DashBoardModel>(ProductList.ToList().Where(p =>
                                      ((p.ProductCategory.ToLower().Contains(category.ToLower()) || p.ProductCategory.ToLower().Contains("Pants".ToLower()) || p.ProductCategory.ToLower().Contains("Shorts".ToLower()) || p.ProductCategory.ToLower().Contains("Skirts".ToLower()) || p.ProductCategory.ToLower().Contains("Jeans".ToLower()))
                                      || (p.ParentCategory.ToLower().Contains(category.ToLower())) || p.ParentCategory.ToLower().Contains("Pants".ToLower()) || p.ParentCategory.ToLower().Contains("Shorts".ToLower()) || p.ParentCategory.ToLower().Contains("Skirts".ToLower()) || p.ParentCategory.ToLower().Contains("Jeans".ToLower()))
                                      && p.StoreType.ToLower() == Global.Storecategory.ToLower()));
                                }
                                break;

                            default:
                                FilterProductList = new ObservableCollection<DashBoardModel>(ProductList.ToList().Where(p => (p.ProductCategory.ToLower().Contains(category.ToLower()) || p.ParentCategory.ToLower().Contains(category.ToLower()) && p.StoreType.ToLower() == Global.Storecategory.ToLower())));
                                break;
                        }
                    }
                    if (FilterProductList.Count == 0)
                    {
                        IsShowEmpty = true;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        string GetCategoryName(string category)
        {
            string newCategory = string.Empty;
            try
            {
                if (Global.GenderParam != null)
                {
                    if (!Global.GenderParam.ToLower().Equals("all"))
                    {
                        if (Global.GenderParam.ToLower() == "men")
                        {
                            switch (category)
                            {
                                case "SHIRTS":
                                    if (category.Equals("Shirts".ToUpper()))
                                    {

                                        newCategory = filterCategory+"_#_Shirts_#_Sweaters";
                                    }
                                    break;

                                case "BOTTOMS":
                                    if (category.Equals("Bottoms".ToUpper()))
                                    {
                                        newCategory = filterCategory + "_#_Jeans_#_Pants_#Shorts";
                                    }
                                    break;

                                case "COATS":
                                    if (category.Equals("Coats".ToUpper()))
                                    {
                                        newCategory = filterCategory + "_#_Jackets & Coats";
                                    }
                                    break;
                                case "SUITS":
                                    if (category.Equals("Suits".ToUpper()))
                                    {
                                        newCategory = filterCategory + "_#_Suits & Tuxedos";
                                    }
                                    break;

                                case "SOCKS":
                                    if (category.Equals("Socks".ToUpper()))
                                    {
                                        newCategory = filterCategory + "_#_Socks & Underwear";
                                    }
                                    break;

                                default:
                                    newCategory = filterCategory;
                                    break;
                            }
                        }
                        if (Global.GenderParam.ToLower() == "women")
                        {
                            switch (category)
                            {
                                case "TOPS":
                                    if (category.Equals("Tops".ToUpper()))
                                    {
                                        newCategory = filterCategory + "_#_Sweaters & Tops";
                                    }
                                    break;
                                case "BOTTOMS":
                                    if (category.Equals("Bottoms".ToUpper()))
                                    {
                                        newCategory = filterCategory + "_#_Jeans_#_Pants_#_Shorts_#_Skirts";
                                    }
                                    break;

                                case "COATS":
                                    if (category.Equals("Coats".ToUpper()))
                                    {
                                        newCategory = filterCategory + "_#_Jackets & Coats";
                                    }
                                    break;

                                case "SUITS":
                                    if (category.Equals("Suits".ToUpper()))
                                    {
                                        
                                        newCategory = filterCategory + "_#_Suits & Separates";
                                    }
                                    break;

                                case "OTHER":
                                    if (category.Equals("Other".ToUpper()))
                                    {
                                        newCategory = filterCategory + "_#_Intimates & Sleepwear_#_Other"; 
                                    }
                                    break;

                                default:
                                    newCategory = filterCategory;
                                    break;
                            }
                        }
                       
                    }
                    else
                    {
                        switch (category)
                        {
                            case "BOTTOMS":
                                if (category.Equals("Bottoms".ToUpper()))
                                {
                                    newCategory = filterCategory + "Jeans_#_Pants_#_Shorts";
                                }
                                break;

                            default:
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                    
            }

            return newCategory;
        }
        public async Task GetProductListWithPaginationMethod()
        {
            try
            {
                if(!Constant.IsConnected)
                {
                    UserDialogs.Instance.Toast("Internet not available");
                    IsTap = false;
                    return;
                }
                Console.WriteLine("Calling paging");
                var req = new ProductRequestModel();
                req.PageNumber = PageNumber;
                req.PageSize = PageSize;
                //var filterCat = GetCategoryName(filterCategory);
                req.ExtraParam = new ExtraParam()
                {
                    StoreId = Global.StoreIndex.ToString(),
                    ViaCategoryName = Global.GenderParam.ToLower().Equals("all") ? filterCategory : null,
                };
                string methodUrl;
                methodUrl = "/api/Product/GetAllProducts";
                RestResponseModel responseModel = await WebService.PostData(req, methodUrl, true);
                if (responseModel != null)
                {
                    if (responseModel.status_code == 200)
                    {
                        var productListResponses = JsonConvert.DeserializeObject<ProductPagingListResponse>(responseModel.response_body);
                        if (productListResponses != null)
                        {
                            if (productListResponses.StatusCode == 200)
                            {
                                if (productListResponses.Data != null)
                                {
                                    productPagingListResponse = productListResponses;
                                    if (ProductList.Count == 0)
                                    {
                                        ProductList = new ObservableCollection<DashBoardModel>(productListResponses.Data.Data);
                                    }
                                    else
                                    {
                                        var proLIst = ProductList.ToList();
                                        proLIst.AddRange(productListResponses.Data.Data.ToList());
                                        ProductList = new ObservableCollection<DashBoardModel>(proLIst.OrderByDescending(p => p.Id).ToList());

                                    }
                                    IsBusy = false;
                                }
                                else
                                {
                                    Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                                    UserDialogs.Instance.HideLoading();
                                    IsBusy = false;
                                }
                            }
                            else
                            {
                                Acr.UserDialogs.UserDialogs.Instance.Alert(productListResponses.Message);
                                UserDialogs.Instance.HideLoading();
                                IsBusy = false;
                            }
                        }
                        else
                        {
                            Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                            UserDialogs.Instance.HideLoading();
                            IsBusy = false;
                        }
                    }
                    else if (responseModel.status_code == 400)
                    {
                        if (!string.IsNullOrEmpty(responseModel.ErrorMessage))
                        {
                            Acr.UserDialogs.UserDialogs.Instance.Alert(responseModel.ErrorMessage);
                            UserDialogs.Instance.HideLoading();
                        }
                        else
                        {
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
                        Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                        UserDialogs.Instance.HideLoading();
                    }
                 }
                else
                {
                    Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                    UserDialogs.Instance.HideLoading();
                    IsBusy = false;
                }
            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Alert(ex.Message);
                UserDialogs.Instance.HideLoading();
                IsBusy = false;
            }
            finally
            {
                Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                IsBusy = false;
            }
            GetTopTrendingItemsListMethod(filterCategory);
            if (FilterProductList.Count < 10)
            {
                IsShowMore = true;
            }
            else
            {
                IsShowMore = false;
            }
        }
        public async Task GetProductListMethod()
        {
            try
            {
                if(!Constant.IsConnected)
                {
                    UserDialogs.Instance.Toast("Internet not available");
                    IsTap = false;
                    return;
                }
                //Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Loading");
                string methodUrl;
                methodUrl = "/api/Product/GetProductsV1";
                RestResponseModel responseModel = await WebService.GetData(methodUrl, true);
                if (responseModel != null)
                {
                    if (responseModel.status_code == 200)
                    {
                        var productListResponses = JsonConvert.DeserializeObject<ProductListResponse>(responseModel.response_body);
                        if (productListResponses != null)
                        {
                            if (productListResponses.StatusCode == 200)
                            {
                                if (productListResponses.Data != null)
                                {
                                    if (ProductList.Count == 0)
                                    {
                                        ProductList = new ObservableCollection<DashBoardModel>(productListResponses.Data);
                                    }
                                    else
                                    {
                                        var proLIst = ProductList.ToList();
                                        proLIst.AddRange(productListResponses.Data.ToList());
                                        ProductList = new ObservableCollection<DashBoardModel>(proLIst.OrderByDescending(p => p.Id).ToList());

                                    }
                                }
                                else
                                {
                                    Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                                    UserDialogs.Instance.HideLoading();
                                }
                            }
                            else
                            {
                                Acr.UserDialogs.UserDialogs.Instance.Alert(productListResponses.Message);
                                UserDialogs.Instance.HideLoading();
                            }
                        }
                        else
                        {
                            Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                            UserDialogs.Instance.HideLoading();
                        }
                    }
                    else if (responseModel.status_code == 400)
                    {
                        if (!string.IsNullOrEmpty(responseModel.ErrorMessage))
                        {
                            Acr.UserDialogs.UserDialogs.Instance.Alert(responseModel.ErrorMessage);
                            UserDialogs.Instance.HideLoading();
                        }
                        else
                        {
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
                        Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                        UserDialogs.Instance.HideLoading();
                    }
                }
                else
                {
                    Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                    UserDialogs.Instance.HideLoading();
                }
            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Alert(ex.Message);
                UserDialogs.Instance.HideLoading();
            }
            finally
            {
                Acr.UserDialogs.UserDialogs.Instance.HideLoading();
            }

            GetTopTrendingItemsListMethod(filterCategory);
            //await GetProductListWithPaginationMethod();
        }
        public async Task CallGetProductListByCategoryMethod()
        {
            try
            {
                Acr.UserDialogs.UserDialogs.Instance.ShowLoading();
                await Task.Delay(100);
                await Device.InvokeOnMainThreadAsync(async () =>
                {
                    await GetProductListByCategoryMethod();
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        public async Task GetProductListByCategoryMethod()
        {
            try
            {
                if(!Constant.IsConnected)
                {
                    UserDialogs.Instance.Toast("Internet not available");
                    IsTap = false;
                    return;
                }
                var req = new ProductRequestModel();
                req.PageNumber = PageNumber;
                req.PageSize = PageSize;
                string methodUrl;
                methodUrl = "/api/Product/GetAllProducts";
                if (filterCategory == "BOTTOMS & PANTS") 
                {
                    filterCategory = "BOTTOMS";
                }
                req.ExtraParam = new ExtraParam()
                {
                    StoreId = Global.StoreIndex.ToString(),
                    CategoryId = this.categoryID,//Convert.ToString(Global.GenderIndex),
                    ViaCategoryName = filterCategory
                };

                RestResponseModel responseModel = await WebService.PostData(req, methodUrl, true);
                if (responseModel != null)
                {
                    if (responseModel.status_code == 200)
                    {
                        var productListResponses = JsonConvert.DeserializeObject<ProductPagingListResponse>(responseModel.response_body);
                        if (productListResponses != null)
                        {
                            if (productListResponses.StatusCode == 200)
                            {
                                if (productListResponses.Data != null)
                                {
                                    productPagingListResponse = productListResponses;
                                    //ProductList = new ObservableCollection<DashBoardModel>(productListResponses.Data.Data.ToList());
                                    //ShowPreviousButton = productListResponses.Data.HasPreviousPage;
                                    //ShowNextButton = productListResponses.Data.HasNextPage;
                                    //if (ProductList.Count > 0)
                                    //{
                                    //    if(productListResponses.Data.TotalPages>1)
                                    //    {
                                    //        ShowPreNext = true;
                                    //    }
                                    //    else
                                    //    {
                                    //        ShowPreNext = false;
                                    //    }
                                    //}
                                    IsBusy = false;
                                    foreach (var item in productListResponses.Data.Data)
                                    {
                                        ProductList.Add(item);
                                    }

                                    if(productListResponses.Data.Data.Count() == 0)
                                    {
                                        ItemTreshold = -1;
                                    }

                                    if (ProductList.Count > 0)
                                    {
                                        if ((PageNumber * PageSize) < productListResponses.Data.TotalRowCount)
                                            TotalCount = "Showing 1 to " + (PageNumber * PageSize) + " of " + productListResponses.Data.TotalRowCount + " Items";
                                        else
                                            TotalCount = "Showing 1 to " + productListResponses.Data.TotalRowCount + " of " + productListResponses.Data.TotalRowCount + " Items";
                                    }
                                    PageNumber = productPagingListResponse.Data.NextPageNumber;
                                    sizeByProductList = new ObservableCollection<string>(ProductList.Select(s => s.Size).Distinct().ToList().ToList().OrderBy(x => x));
                                    brandList = new ObservableCollection<string>(ProductList.Select(s => s.Brand).Distinct().ToList().ToList().OrderBy(x => x));
                                    IsLoadMore = false;


                                }
                                else
                                {
                                    Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                                    UserDialogs.Instance.HideLoading();
                                    IsLoadMore = false;
                                }
                            }
                            else
                            {
                                Acr.UserDialogs.UserDialogs.Instance.Alert(productListResponses.Message);
                                UserDialogs.Instance.HideLoading();
                                IsLoadMore = false;
                            }
                        }
                        else
                        {
                            Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                            UserDialogs.Instance.HideLoading();
                            IsLoadMore = false;
                        }
                    }
                    else if (responseModel.status_code == 400)
                    {
                        if (!string.IsNullOrEmpty(responseModel.ErrorMessage))
                        {
                            Acr.UserDialogs.UserDialogs.Instance.Alert(responseModel.ErrorMessage);
                            UserDialogs.Instance.HideLoading();
                        }
                        else
                        {
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
                        Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                        UserDialogs.Instance.HideLoading();
                    }
                }
                else
                {
                    Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                    UserDialogs.Instance.HideLoading();
                    IsLoadMore = false;
                }
            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Alert(ex.Message);
                UserDialogs.Instance.HideLoading();
                IsLoadMore = false;
            }
            finally
            {
                Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                IsLoadMore = false;
                IsFilterChanged = false;
            }
            IsShowEmpty = ProductList.Count > 0 ? false : true;
            IsNoData = IsShowEmpty;
            IsShowFilter = !IsNoData;
            //await GetProductListWithPaginationMethod();
        }
        async Task ItemsTresholdReached()
        {
            if(ProductList.Count == 0 && IsFilterChanged == true)
            {
                return;
            }

            if (!Constant.IsConnected)
            {
                UserDialogs.Instance.Toast("Internet not available");
                IsTap = false;
                return;
            }
            if (productPagingListResponse != null)
            {
                if (productPagingListResponse.Data.HasNextPage == false)
                {
                    IsLoadMore = false;
                    IsBusy = false;
                    return;
                }
            }
            if (IsLoadMore)
                return;

            await Task.Run(async () => {
                IsLoadMore = true;
                await Task.Delay(50);

                try
                {
                    if (productFilterModel == null)
                    {
                        await GetProductListByCategoryMethod();
                    }
                    else
                    {
                        productFilterModel.PageNumber = PageNumber;
                        await GetProductByFilterMethod(productFilterModel);
                    }
                    //await GetProductListByCategoryMethod();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                }
                finally
                {
                    IsBusy = false;
                    Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                }
            });
        }
        private async Task GetProductByFilterMethod(ProductFilterModel productFilterModel)
        {
            int prevPageNumber = 0;
            try
            {
                if (!Constant.IsConnected)
                {
                    UserDialogs.Instance.Toast("Internet not available");
                    IsTap = false;
                    return;
                }
                prevPageNumber = PageNumber;
                //PageNumber = 1;
                productFilterModel.PageNumber = PageNumber;
                string methodUrl = "/api/Product/GetAllFilterProduct";
                RestResponseModel responseModel = await WebService.PostData(productFilterModel, methodUrl, true);
                if (responseModel != null)
                {
                    if (responseModel.status_code == 200)
                    {
                        var productListResponses = JsonConvert.DeserializeObject<ProductPagingListResponse>(responseModel.response_body);
                        if (productListResponses != null)
                        {
                            if (productListResponses.StatusCode == 200)
                            {
                                if (productListResponses.Data != null)
                                {
                                    //ProductList.Clear();
                                    productPagingListResponse = productListResponses;
                                    IsBusy = false;
                                    foreach (var item in productListResponses.Data.Data)
                                    {
                                        ProductList.Add(item);
                                    }

                                    if (productListResponses.Data.Data.Count() == 0)
                                    {
                                        ItemTreshold = -1;
                                    }
                                    if (ProductList.Count > 0)
                                    {
                                        if ((PageNumber * PageSize) < productListResponses.Data.TotalRowCount)
                                            TotalCount = "Showing 1 to " + (PageNumber * PageSize) + " of " + productListResponses.Data.TotalRowCount + " Items";
                                        else
                                            TotalCount = "Showing 1 to " + productListResponses.Data.TotalRowCount + " of " + productListResponses.Data.TotalRowCount + " Items";

                                    }
                                    PageNumber = productPagingListResponse.Data.NextPageNumber;
                                    sizeByProductList = new ObservableCollection<string>(ProductList.Select(s => s.Size).Distinct().ToList().ToList().OrderBy(x => x));
                                    brandList = new ObservableCollection<string>(ProductList.Select(s => s.Brand).Distinct().ToList().ToList().OrderBy(x => x));
                                    IsLoadMore = false;
                                }
                                else
                                {
                                    Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                                    UserDialogs.Instance.HideLoading();
                                    IsLoadMore = false;
                                }
                            }
                            else
                            {
                                Acr.UserDialogs.UserDialogs.Instance.Alert(productListResponses.Message);
                                UserDialogs.Instance.HideLoading();
                                IsLoadMore = false;
                            }
                        }
                        else
                        {
                            Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                            UserDialogs.Instance.HideLoading();
                            IsLoadMore = false;
                            PageNumber = prevPageNumber;
                        }
                    }
                    else if (responseModel.status_code == 400)
                    {
                        if (!string.IsNullOrEmpty(responseModel.ErrorMessage))
                        {
                            Acr.UserDialogs.UserDialogs.Instance.Alert(responseModel.ErrorMessage);
                            UserDialogs.Instance.HideLoading();
                        }
                        else
                        {
                            Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                            UserDialogs.Instance.HideLoading();
                        }
                        PageNumber = prevPageNumber;
                    }
                    else
                    {
                        Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                        UserDialogs.Instance.HideLoading();
                        PageNumber = prevPageNumber;
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
                    Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                    UserDialogs.Instance.HideLoading();
                    IsLoadMore = false;
                    PageNumber = prevPageNumber;
                }
            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Alert(ex.Message);
                UserDialogs.Instance.HideLoading();
                IsLoadMore = false;
                PageNumber = prevPageNumber;

            }
            finally
            {
                Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                IsFilterChanged = false;
            }
            IsNoData = ProductList.Count > 0 ? false : true;
        }
        public async void SetProductFilterRequest(List<FilterModel> listFilter)
        {
            try
            {
                productFilterModel = new ProductFilterModel();
                productFilterModel.StoreId = Convert.ToInt16(Global.StoreIndex);
                productFilterModel.Search = string.Empty;
                productFilterModel.PageSize = PageSize;
                productFilterModel.PageNumber = PageNumber;
                productFilterModel.Price = new PriceFilter();
                productFilterModel.ShippingPrice = new PriceFilter();
                productFilterModel.Sort = new SortingFilter();
                productFilterModel.LoggedUserId = null;
                productFilterModel.UserId = null;
                var selectedPrice = listFilter.Where(f => f.KEY == Constant.PriceStr).FirstOrDefault().Value;
                if (selectedPrice.Contains(Constant.UnderStr))
                {
                    productFilterModel.Price.IsApply = true;
                    productFilterModel.Price.FromPrice = Convert.ToDecimal(selectedPrice.Replace(Constant.UnderStr, "").Replace(" ", "").Replace("$", ""));
                    productFilterModel.Price.ToPrice = Convert.ToDecimal(selectedPrice.Replace(Constant.UnderStr, "").Replace(" ", "").Replace("$", ""));
                    productFilterModel.Price.PriceCompareOperator = PriceCompareFilter.LessThan;
                }
                else if (selectedPrice.Contains(Constant.OverStr))
                {
                    productFilterModel.Price.IsApply = true;
                    productFilterModel.Price.FromPrice = Convert.ToDecimal(selectedPrice.Replace(Constant.OverStr, "").Replace(" ", "").Replace("$", ""));
                    productFilterModel.Price.ToPrice = Convert.ToDecimal(selectedPrice.Replace(Constant.OverStr, "").Replace(" ", "").Replace("$", ""));
                    productFilterModel.Price.PriceCompareOperator = PriceCompareFilter.GreaterThan;
                }
                else
                {
                    var priceList = selectedPrice.Trim().Split('-');
                    if (priceList.Length > 1)
                    {
                        productFilterModel.Price.IsApply = true;
                        productFilterModel.Price.FromPrice = Convert.ToDecimal(priceList[0].Replace(" ", "").Replace("$", ""));
                        productFilterModel.Price.ToPrice = Convert.ToDecimal(priceList[1].Replace(" ", "").Replace("$", ""));
                        productFilterModel.Price.PriceCompareOperator = PriceCompareFilter.Between;
                    }
                }
                var selectedShipPrice = listFilter.Where(f => f.KEY == Constant.ShippingPriceStr).FirstOrDefault().Value;
                if (selectedShipPrice != null)
                {
                    if (selectedShipPrice.ToLower().Equals(Constant.AllStr.ToLower()))
                    {

                    }
                    else
                    {
                        productFilterModel.ShippingPrice.IsApply = true;
                        productFilterModel.ShippingPrice.FromPrice = 0;
                        productFilterModel.ShippingPrice.ToPrice = Convert.ToDecimal(selectedShipPrice.Replace(" ", "").Replace("$", ""));
                        productFilterModel.ShippingPrice.PriceCompareOperator = PriceCompareFilter.Between;
                    }
                }
                var selectedSort = listFilter.Where(f => f.KEY == Constant.SortStr).FirstOrDefault().Value;
                if (selectedSort.Contains(Constant.LowToHighStr))
                {
                    productFilterModel.Sort.IsApply = true;
                    productFilterModel.Sort.Sort = SortCompareFilter.PriceLowToHigh;
                }
                else if (selectedSort.Contains(Constant.HighToLowStr))
                {
                    productFilterModel.Sort.IsApply = true;
                    productFilterModel.Sort.Sort = SortCompareFilter.PriceHighToLow;
                }
                else if (selectedSort.ToLower().Contains(Constant.AllStr.ToLower()))
                {

                }
                else if (selectedSort.Contains(Constant.NewListingStr))
                {
                    productFilterModel.Sort.IsApply = true;
                    productFilterModel.Sort.Sort = SortCompareFilter.NewList;
                }
                var Availability = listFilter.Where(f => f.KEY == Constant.AvailabilityStr).FirstOrDefault().Value;
                if (Availability.ToLower().Equals(Constant.AllStr.ToLower()))
                {
                    productFilterModel.Availability = new List<string>();
                }
                else
                {
                    productFilterModel.Availability = new List<string>(){
                        Availability
                    };
                }

                var Condition = listFilter.Where(f => f.KEY == Constant.ConditionStr).FirstOrDefault().Value;
                if (Condition.ToLower().Equals(Constant.AllStr.ToLower()))
                {
                    productFilterModel.Condition = new List<string>();
                }
                else
                {
                    productFilterModel.Condition = new List<string>(){
                        Condition
                    };
                }

                var Colour = listFilter.Where(f => f.KEY == Constant.ColorStr).FirstOrDefault().Value;
                if (Colour.ToLower().Equals(Constant.AllStr.ToLower()))
                {
                    productFilterModel.Color = new List<string>();
                }
                else
                {
                    productFilterModel.Color = new List<string>(){
                        Colour
                    };
                }

                var Brand = listFilter.Where(f => f.KEY == Constant.BrandStr).FirstOrDefault().Value;
                if (Brand.ToLower().Equals(Constant.AllStr.ToLower()))
                {
                    productFilterModel.Brand = new List<string>();
                }
                else
                {
                    productFilterModel.Brand = new List<string>(){
                        Brand
                    };
                }

                var Size = listFilter.Where(f => f.KEY.Trim() == Constant.SizeStr).FirstOrDefault().Value;
                if (Size.ToLower().Trim().Equals(Constant.AllStr.ToLower()))
                {
                    productFilterModel.Size = new List<string>();
                }
                else
                {
                    productFilterModel.Size = new List<string>(){
                        //(Size.ToLower().Contains("one size"))?"OS":Size
                        Size
                    };
                }

                var Categories = listFilter.Where(f => f.KEY == Constant.CategoryStr).FirstOrDefault();
                if (Categories != null)
                {
                    if (Categories.Value.ToLower().Trim().Equals(Constant.AllStr.ToLower()))
                    {
                        productFilterModel.Categories = new List<string>();
                        productFilterModel.RootCategories = new List<int> { 1, 2 };
                    }
                    else
                    {
                        var filterCat = Categories.Value.Split('|');
                        if (filterCat.Length > 0)
                        {
                            if (filterCat[0].ToLower().Contains("all"))
                            {
                                if (filterCat.Length < 3 && filterCat.Length > 1)
                                {
                                    productFilterModel.RootCategories = new List<int> { 1, 2 };
                                    productFilterModel.Categories = new List<string>() {
                                filterCat[1].Trim()};
                                }
                                else
                                {

                                    productFilterModel.RootCategories = new List<int> { 1, 2 };
                                    productFilterModel.Categories = new List<string>() {
                                filterCat[1].Trim(),
                                filterCat[2].Trim()};
                                }
                            }
                            else if (filterCat.Length < 3 && filterCat.Length > 1)
                            {
                                if (filterCat[0].Trim().ToLower() == "men" || filterCat[0].Trim().ToLower() == "women")
                                {
                                    productFilterModel.RootCategories = new List<int> { filterCat[0].Trim().ToLower() == "men" ? 1 : 2 };
                                }
                                else if (filterCat[0].Trim().ToLower() == "all")
                                {
                                    productFilterModel.RootCategories = new List<int> { 1, 2 };
                                }
                                productFilterModel.Categories = new List<string>() {
                                filterCat[1].Trim().ToLower().Equals("bottoms & pants")?"BOTTOMS":filterCat[1].Trim()
                                //Global.GetExtraCategories(filterCat[1].Trim())
                            };
                            }
                            else if (filterCat.Length > 2)
                            {
                                if (filterCat[0].Trim().ToLower() == "men" || filterCat[0].Trim().ToLower() == "women")
                                {
                                    productFilterModel.RootCategories = new List<int> { filterCat[0].Trim().ToLower() == "men" ? 1 : 2 };
                                }
                                else if (filterCat[0].Trim().ToLower() == "all")
                                {
                                    productFilterModel.RootCategories = new List<int> { 1, 2 };
                                }
                                productFilterModel.Categories = new List<string>() {
                                //filterCat[1].Trim(),
                                 Global.GetExtraCategories(filterCat[1].Trim(), productFilterModel.RootCategories),
                                filterCat[2].Trim()
                            };
                            }
                            else if (filterCat.Length == 1)
                            {
                                productFilterModel.RootCategories = new List<int> { filterCat[0].Trim().ToLower() == "men" ? 1 : 2 };
                            }
                        }
                    }
                }
                //var Gender = listFilter.Where(f => f.KEY == Constant.GenderStr).FirstOrDefault().Value;
                //if (Gender.ToLower().Contains("men") || Gender.ToLower().Contains("women"))
                //{
                //    productFilterModel.RootCategories = new List<int> { Gender.ToLower() == "men" ? 1 : 2 };
                //}
                //else if (Gender.ToLower().Contains(Constant.AllStr.ToLower()))
                //{
                //    productFilterModel.RootCategories = new List<int> { 1, 2 };
                //}

                PageNumber = 1;
                ProductList.Clear();
                await GetProductByFilterMethod(productFilterModel);
            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.HideLoading();
            }
        }

        #endregion

    }
}
