using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Acr.UserDialogs;
using AutoMapper.Configuration.Annotations;
using BuySell.Model;
using BuySell.Model.LoginResponse;
using BuySell.Model.RestResponse;
using BuySell.Persistence;
using BuySell.View;
using BuySell.ViewModel;
using BuySell.WebServices;
using Newtonsoft.Json;
using Plugin.DeviceInfo.Abstractions;
using Stripe;
using Stripe.Issuing;
using Xamarin.Essentials;
using Xamarin.Forms;
using static BuySell.Model.CategoryModel;
using static SQLite.SQLite3;
using Application = Xamarin.Forms.Application;

namespace BuySell.Helper
{
    //Enums created for streetwear, clothing, sneakers and vintage themes
    public enum ThemesColor
    {
        BlueColor = 1,
        RedColor,
        OrangeColor,
        GreenColor
    }

    public enum ProductCategory
    {
        Cloths,
        Sneaker,
        Streetwear,
        Vintage
    }

    public static class Global
    {
        public static string AccessToken { get { return Constant.LoginUserData != null ? Constant.LoginUserData.Token : null; } }
        public static string Username;
        public static string Password;
        public static string Storecategory = Constant.ClothingStr;
        public static int GenderIndex = 1;
        public static string Subcategory;
        public static int StoreIndex = 1;
        public static byte[] ImageToBeDelete;
        public static decimal AftrducPrice;
        public static ImageSource UserImage;
        public static Image CroppedImage;
        public static byte[] BuySellimageByte;
        public static ThemesColor setThemeColor = ThemesColor.BlueColor;
        public static ProductCategory SetProductCategory = ProductCategory.Cloths;
        public static List<CartModel> globalCartList = new List<CartModel>();
        public static List<DashBoardModel> postProductList = new List<DashBoardModel>();
        public static List<DashBoardModel> OfferProductList = new List<DashBoardModel>();
        public static ObservableCollection<DashBoardModel> GlobalProductList = new ObservableCollection<DashBoardModel>();
        public static ObservableCollection<DashBoardModel> GlobalRecentProductList = new ObservableCollection<DashBoardModel>();
        public static ObservableCollection<AddAddressModel> GlobalAddressList = new ObservableCollection<AddAddressModel>();
        public static ObservableCollection<AddAddressModel> GlobalShipFromAddressList = new ObservableCollection<AddAddressModel>();
        public static ObservableCollection<CardListModel> GlobalCardList = new ObservableCollection<CardListModel>();
        public static ObservableCollection<BankAccountModel> BankAccountsList = new ObservableCollection<BankAccountModel>();
        public static List<Roots> RootsList = new List<Roots>();
        public static List<Roots> SneakersRootsList = new List<Roots>();
        public static List<SubRoots> CatBySubList = new List<SubRoots>();
        public static string BuyingPageTitle = string.Empty;
        public static List<string> listCancelResons = new List<string>()
        {
            "Can't find item","Changed mind","Item sold on other platform","Other"
        };
        public static string SeletedItems;
        public static string GenderParam = "Men";
        public static NavigationPage globalNav;
        public static SqlProductDB database = null;
        public static double TileWidth = 150;
        public static bool IsUploadedProduct = false;
        public static string SearchedResultSelectedStore = Storecategory;
        public static string SelectedState;
        public static string SearchResultSelStore;
        public static Placemark currentPlaceMark = null;
        public static bool IsReload = false;

        //Constant created to filter data in recent views
        public static string SelectedFilter;

        public static SqlProductDB GetConnection()
        {
            if (database == null)
                database = new SqlProductDB();
            return database;
        }

        public static bool IsValidExpiryDate(int month, int year)
        {
            var DateTimeNow = DateTime.Now;
            var MonthNow = DateTimeNow.Month;
            var Yearstring = DateTimeNow.Year.ToString().Substring(2, 2);
            var YearNow = Convert.ToInt16(Yearstring);

            if (year > YearNow)
            {
                return true;
            }

            else if (year == YearNow)
            {
                if (month >= MonthNow)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        //Method created to get selected store's name based on index
        public static string GetStoreName(int storeIndex)
        {
            if (storeIndex == 1)
                return Constant.ClothingStr;
            else if (storeIndex == 2)
                return Constant.SneakersStr;
            else if (storeIndex == 3)
                return Constant.StreetwearStr;
            else if (storeIndex == 4)
                return Constant.VintageStr;
            else
                return Constant.ClothingStr;
        }
        //Method created to get selected store's color 
        public static string GetThemeColor(ThemesColor themesColor)
        {
            string themeColor = "#1567A6";
            try
            {
                switch (themesColor)
                {
                    case ThemesColor.BlueColor:
                        {
                            themeColor = "#1567A6";
                        }
                        break;
                    case ThemesColor.GreenColor:
                        {
                            themeColor = "#467904";
                        }
                        break;
                    case ThemesColor.RedColor:
                        {
                            themeColor = "#C52036";
                        }
                        break;
                    case ThemesColor.OrangeColor:
                        {
                            themeColor = "#D04107";
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
            }
            return themeColor;
        }

        //Get products category name using theme color
        public static string GetProductCatName(ThemesColor themesColor)
        {
            string catName = Constant.ClothingStr;
            try
            {
                switch (themesColor)
                {
                    case ThemesColor.BlueColor:
                        {
                            catName = Constant.ClothingStr;
                        }
                        break;
                    case ThemesColor.GreenColor:
                        {
                            catName = Constant.VintageStr;
                        }
                        break;
                    case ThemesColor.RedColor:
                        {
                            catName = Constant.SneakersStr;
                        }
                        break;
                    case ThemesColor.OrangeColor:
                        {
                            catName = Constant.StreetwearStr;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
            }
            return catName;
        }
        //Method created to get products category  icon  using theme color
        public static string GetProductCatIcon(ThemesColor themesColor)
        {
            string catName = "ClothingHanger";
            try
            {
                switch (themesColor)
                {
                    case ThemesColor.BlueColor:
                        {
                            catName = "ClothingHanger";
                        }
                        break;
                    case ThemesColor.GreenColor:
                        {
                            catName = "Vintage";
                        }
                        break;
                    case ThemesColor.RedColor:
                        {
                            catName = "Sneakers";
                        }
                        break;
                    case ThemesColor.OrangeColor:
                        {
                            catName = "Streetwear";
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
            }
            return catName;
        }
        //Method created to get products category  logo  using theme color
        public static string GetProductCatLogo(ThemesColor themesColor)
        {
            string catName = "BuySell_logoclothing";
            try
            {
                switch (themesColor)
                {
                    case ThemesColor.BlueColor:
                        {
                            catName = "BuySell_logoclothing";
                        }
                        break;
                    case ThemesColor.GreenColor:
                        {
                            catName = "BuySell_logovintage";
                        }
                        break;
                    case ThemesColor.RedColor:
                        {
                            catName = "BuySell_logosneakers";
                        }
                        break;
                    case ThemesColor.OrangeColor:
                        {
                            catName = "BuySell_logostreetwear";
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
            }
            return catName;
        }

        public static async Task<bool> PopToPageWthReturn(Type destination, INavigation navigation)
        {
            if (destination == null)
            {
                return false;
            }

            List<Page> toRemove = new List<Page>();
            //First, we get the navigation stack as a list
            List<Page> pages = navigation.NavigationStack.ToList();
            //Then we invert it because it's from first to last and we need in the inverse order
            pages.Reverse();
            pages.RemoveAt(0);
            foreach (Page page in pages)
            {
                if (page.GetType() == destination)
                {
                    break; //We found it.
                }
                toRemove.Add(page);
            }

            foreach (Page rvPage in toRemove)
            {
                navigation.RemovePage(rvPage);
            }
            pages = null;
            await navigation.PopAsync(false);
            //GC.Collect();
            return true;
        }
        //Method created to set  theme color using index
        public static void SetThemeColor(int index)
        {
            var param = Convert.ToInt16(index);
            if (param == 1)
            {
                Global.setThemeColor = ThemesColor.BlueColor;
                Storecategory = Constant.ClothingStr;
            }
            else if (param == 2)
            {
                Global.setThemeColor = ThemesColor.RedColor;
                Storecategory = Constant.SneakersStr;
            }
            else if (param == 3)
            {
                Global.setThemeColor = ThemesColor.OrangeColor;
                Storecategory = Constant.StreetwearStr;
            }

            else if (param == 4)
            {
                Global.setThemeColor = ThemesColor.GreenColor;
                Storecategory = Constant.VintageStr;
            }
        }

        public static void SetFirstTimeLoad()
        {
            Application.Current.Properties["IsLoggedIn"] = true;
            Application.Current.SavePropertiesAsync();
        }

        public static bool CheckFirstTimeLoad()
        {
            return Application.Current.Properties.ContainsKey("IsLoggedIn");
        }

        public static bool IsHomePageExist(INavigation navigation)
        {
            bool doesPageExists = navigation.NavigationStack.Any(p => p is HomePage);
            return doesPageExists;
        }

        public static void RemoveHomePageAndInsertLogin(INavigation navigation)
        {
            bool doesPageExists = navigation.NavigationStack.Any(p => p is LoginPage);
            if (!doesPageExists)
            {
                var homePage = navigation.NavigationStack.Where(p => p.GetType() == typeof(HomePage)).FirstOrDefault();
                navigation.InsertPageBefore(new LoginPage(), homePage);
                navigation.RemovePage(homePage);
                navigation.PopToRootAsync();
            }
            else
            {
                navigation.PopToRootAsync();
            }
        }

        public static void InsertLogin(INavigation navigation)
        {
            bool doesPageExists = navigation.NavigationStack.Any(p => p is LoginPage);
            if (!doesPageExists)
            {
                var homePage = navigation.NavigationStack.Where(p => p.GetType() == typeof(HomePage)).FirstOrDefault();
                navigation.InsertPageBefore(new LoginPage(), homePage);
                navigation.RemovePage(homePage);
            }

        }
        //Method created for email validation
        public static bool IsValidateEmail(string email)
        {
            try
            {
                return Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                //@"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$");
            }
            catch
            {
                return false;
            }
        }
        //Method created for phone number validation
        public static bool IsValidatePhoneNumber(string phoneNumber)
        {
            try
            {
                return Regex.IsMatch(phoneNumber, @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$");
            }
            catch
            {
                return false;
            }
        }

        public static bool IsValidatedecimalNumber(string DecimalNumber)
        {
            try
            {
                return Regex.IsMatch(DecimalNumber, @"/^\d*(\.)?(\d{0,2})?$/");
            }
            catch
            {
                return false;
            }
        }

        public static ImageSource ConvertBase64ToImageSource(this string base64)
        {
            if (string.IsNullOrEmpty(base64))
            {
                return ImageSource.FromFile("Profile");
            }
            return Xamarin.Forms.ImageSource.FromStream(
             () => new MemoryStream(Convert.FromBase64String(base64)));
        }

        public static string ConvertImagesourceToBase64(this ImageSource imageSource)
        {
            StreamImageSource streamImageSource = (StreamImageSource)imageSource;
            System.Threading.CancellationToken cancellationToken = System.Threading.CancellationToken.None;
            Task<Stream> task = streamImageSource.Stream(cancellationToken);
            Stream stream = task.Result;
            byte[] imageData;
            //Convert Image Stream to Byte Array  
            MemoryStream ms = new MemoryStream();
            stream.CopyTo(ms);
            imageData = ms.ToArray();
            return Convert.ToBase64String(imageData);
        }

        public static string GetFileExtentionUsingURL(string url)
        {
            FileInfo fi = new FileInfo(url);
            return fi.Extension;
        }

        public static void SetValueInProperties(string key, string value)
        {
            if (Application.Current.Properties.ContainsKey(key))
            {
                Application.Current.Properties.Remove(key);
                Application.Current.SavePropertiesAsync();
            }
            Application.Current.Properties.Add(key, value);
            Application.Current.SavePropertiesAsync();
        }

        public static object GetValueInProperties(string key)
        {
            return Application.Current.Properties[key];
        }

        public static void ResetMessagingCenter(ContentPage page)
        {
            MessagingCenter.Unsubscribe<object, string>("SelectPropertyValue", "SelectPropertyValue");
            MessagingCenter.Unsubscribe<object, byte[]>("IsImgAdd", "IsImgAdd");
            MessagingCenter.Unsubscribe<object, byte[]>("ImageToBeDelete", "ImageToBeDelete");
            MessagingCenter.Unsubscribe<object, SubRoots>("SelectedGenderCat", "SelectedGenderCat");
            MessagingCenter.Unsubscribe<object, List<ImageSource>>("IsReorder", "IsReorder");
            MessagingCenter.Unsubscribe<object, SelectSubRootCategory>("SelGenderCatSneakers", "SelGenderCatSneakers");
            MessagingCenter.Unsubscribe<object, SubRoots>("SelectedGenderCatFilter", "SelectedGenderCatFilter");
            MessagingCenter.Unsubscribe<object, SelectSubRootCategory>("SelGenderCatSneakersFilter", "SelGenderCatSneakersFilter");
            MessagingCenter.Unsubscribe<object, List<FilterModel>>("FilterList", "FilterList");
        }

        public static void ResetMessagingCenter()
        {
            MessagingCenter.Unsubscribe<object, string>("SelectPropertyValue", "SelectPropertyValue");
            MessagingCenter.Unsubscribe<object, byte[]>("IsImgAdd", "IsImgAdd");
            MessagingCenter.Unsubscribe<object, byte[]>("ImageToBeDelete", "ImageToBeDelete");
            MessagingCenter.Unsubscribe<object, List<ImageSource>>("IsReorder", "IsReorder");
            MessagingCenter.Unsubscribe<object, SubRoots>("SelectedGenderCat", "SelectedGenderCat");
            MessagingCenter.Unsubscribe<object, SelectSubRootCategory>("SelGenderCatSneakers", "SelGenderCatSneakers");
            MessagingCenter.Unsubscribe<object, SubRoots>("SelectedGenderCatFilter", "SelectedGenderCatFilter");
            MessagingCenter.Unsubscribe<object, SelectSubRootCategory>("SelGenderCatSneakersFilter", "SelGenderCatSneakersFilter");
            MessagingCenter.Unsubscribe<object, List<FilterModel>>("FilterList", "FilterList");
        }

        public static void ResetMessagingCenterForEdit()
        {
            MessagingCenter.Unsubscribe<object, string>("SelectPropertyValue", "SelectPropertyValue");
            MessagingCenter.Unsubscribe<object, byte[]>("IsImgAdd", "IsImgAdd");
            MessagingCenter.Unsubscribe<object, byte[]>("ImageToBeDelete", "ImageToBeDelete");
            MessagingCenter.Unsubscribe<object, List<ImageSource>>("IsReorder", "IsReorder");
            MessagingCenter.Unsubscribe<object, SubRoots>("SelectedGenderCat", "SelectedGenderCat");
            MessagingCenter.Unsubscribe<object, SelectSubRootCategory>("SelGenderCatSneakers", "SelGenderCatSneakers");
            MessagingCenter.Unsubscribe<object, List<FilterModel>>("FilterList", "FilterList");
        }
        //Method created to get Selected theme index number using store type
        public static int GetSelectedThemeColorIndex(string StoreType)
        {
            int index = 0;
            if (StoreType.ToLower() == Constant.ClothingStr.ToLower())
            {
                return 1;
            }
            else if (StoreType.ToLower() == Constant.SneakersStr.ToLower())
            {
                return 2;
            }
            else if (StoreType.ToLower() == Constant.StreetwearStr.ToLower())
            {
                return 3;
            }
            else if (StoreType.ToLower() == Constant.VintageStr.ToLower())
            {
                return 4;
            }

            index = 1;
            return index;
        }
        //Method created to get Selected theme color number using index
        public static ThemesColor GetThemeColorUsingIndex(int index)
        {
            var param = Convert.ToInt16(index);
            if (param == 1)
            {
                return ThemesColor.BlueColor;

            }
            else if (param == 2)
            {
                return ThemesColor.RedColor;
            }
            else if (param == 3)
            {
                return ThemesColor.OrangeColor;
            }
            else if (param == 4)
            {
                return ThemesColor.GreenColor;
            }

            return ThemesColor.BlueColor;
        }

        public static string GetHexColByName(string HexColorName)
        {
            string colName = "Red";
            try
            {
                switch (HexColorName)
                {
                    case "Red":
                        {
                            colName = "#FF0000";
                        }
                        break;
                    case "Pink":
                        {
                            colName = "#FFC0CB";
                        }
                        break;
                    case "Orange":
                        {
                            colName = "#FFA500";
                        }
                        break;
                    case "Yellow":
                        {
                            colName = "#FFFF00";
                        }
                        break;
                    case "Green":
                        {
                            colName = "#008000";
                        }
                        break;
                    case "Blue":
                        {
                            colName = "#0000FF";
                        }
                        break;
                    case "Purple":
                        {
                            colName = "#800080";
                        }
                        break;
                    case "Gold":
                        {
                            colName = "#FFD700";
                        }
                        break;
                    case "Silver":
                        {
                            colName = "#C0C0C0";
                        }
                        break;
                    case "Black":
                        {
                            colName = "#000000";
                        }
                        break;
                    case "Gray":
                        {
                            colName = "#808080";
                        }
                        break;
                    case "White":
                        {
                            colName = "#FFFFFF";
                        }
                        break;
                    case "Cream":
                        {
                            colName = "#FFFDD0";
                        }
                        break;
                    case "Brown":
                        {
                            colName = "#704214";
                        }
                        break;
                    case "Tan":
                        {
                            colName = "#D2B48C";
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
            }
            return colName;
        }

        public static void SetRecentViewList(DashBoardModel dashBoard)
        {
            try
            {
                var list = Global.database.GetRecentProductByUserID(Constant.LoginUserData != null ? Constant.LoginUserData.Id : 0);
                if (list.ToList().Count > 0)
                {
                    var pro = Global.GlobalRecentProductList.Where(p => p.Id == dashBoard.Id).FirstOrDefault();
                    if (pro == null)
                    {
                        dashBoard.RecentViewItemDate = DateTime.Now;
                        Global.GlobalRecentProductList.Add(dashBoard);
                        //break;
                        Global.database.Insert(new RecentProductModel()
                        {
                            UserId = Constant.LoginUserData != null ? Constant.LoginUserData.Id : 0,
                            ProductId = dashBoard.Id,
                            ProductJson = JsonConvert.SerializeObject(dashBoard, new JsonSerializerSettings()
                            {
                                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                            }),
                            RecentViewItemDate = dashBoard.RecentViewItemDate
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        #region get shipping list 
        public static async void GetAllShippingAddress()
        {
            try
            {
                if (!Constant.IsConnected)
                {
                    UserDialogs.Instance.Toast("Internet not available");
                    return;
                }

                Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Loading, please wait...");
                await Task.Delay(50);
                string methodUrl = $"/api/Account/ListShippingAddress?id={Constant.LoginUserData.Id}";
                if (!string.IsNullOrWhiteSpace(methodUrl))
                {
                    RestResponseModel responseModel = await WebService.GetData(methodUrl, true);
                    if (responseModel != null)
                    {
                        if (responseModel.status_code == 200)
                        {
                            Global.GlobalAddressList = JsonConvert.DeserializeObject<ObservableCollection<AddAddressModel>>(responseModel.response_body);

                            if (Global.GlobalAddressList != null)
                            {
                                if (Global.GlobalAddressList.Count > 0)
                                    Constant.globalSelectedAddress = Global.GlobalAddressList.Where(account => account.IsDefault == true).FirstOrDefault();
                                else
                                    Constant.globalSelectedAddress = null;
                            }
                            else
                            {
                                Constant.globalSelectedAddress = null;
                            }
                            UserDialogs.Instance.HideLoading();
                        }
                        else if (responseModel.status_code == 500)
                        {
                            ResponseBodyModel responseBodyModel = JsonConvert.DeserializeObject<ResponseBodyModel>(responseModel.response_body);
                            Acr.UserDialogs.UserDialogs.Instance.Alert(responseBodyModel.Message);
                            UserDialogs.Instance.HideLoading();
                        }
                        else
                        {
                            Acr.UserDialogs.UserDialogs.Instance.Alert(responseModel.ErrorMessage);
                            UserDialogs.Instance.HideLoading();
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
                    Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                    UserDialogs.Instance.HideLoading();
                }
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                Debug.WriteLine(ex.Message);
            }
        }

        #endregion

        #region Get Credit Card List
        public static async void GetCreditCardList()
        {
            try
            {
                try
                {
                //    string methodUrl = $"/api/Account/AddCard?id={Constant.LoginUserData.Id}";
                //    if (!string.IsNullOrWhiteSpace(methodUrl))
                //    {
                //        RestResponseModel responseModel = await WebService.GetData(methodUrl, true);
                //        if (responseModel != null)
                //        {
                //            if (responseModel.status_code == 200)
                //            {
                //                Global.GlobalCardList = JsonConvert.DeserializeObject<ObservableCollection<CardModel>>(responseModel.response_body);

                //                if (Global.GlobalCardList != null)
                //                {
                //                    if (Global.GlobalCardList.Count > 0)
                //                        Constant.globalAddedCard = Global.GlobalCardList.Where(account => account.IsDefault == true).FirstOrDefault();
                //                    else
                //                        Constant.globalAddedCard = new CardModel();
                //                }
                //                else
                //                {
                //                    Constant.globalAddedCard = new CardModel();
                //                }
                //            }
                //            else
                //            {
                //                Acr.UserDialogs.UserDialogs.Instance.Alert(responseModel.ErrorMessage);
                //                UserDialogs.Instance.HideLoading();
                //            }
                //        }
                //        else
                //        {
                //            Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                //            UserDialogs.Instance.HideLoading();
                //        }
                //    }
                //    else
                //    {
                //        Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                //        UserDialogs.Instance.HideLoading();
                //    }
                }
                catch (Exception ex)
                {
                    UserDialogs.Instance.HideLoading();
                    Debug.WriteLine(ex.Message);
                }

            }
            catch (Exception ex)
            {

            }

        }
        #endregion


        async public static Task<string> GetBrandJson()
        {
            try
            {
                using (var stream = await FileSystem.OpenAppPackageFileAsync("Brand.json"))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        var fileContents = await reader.ReadToEndAsync();
                        return fileContents;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return string.Empty;
        }

        public static string GetCardICon(string cardNo)
        {
            if (cardNo != null)
            {
                if (cardNo.Length >= 7)
                {
                    if (Regex.Match(cardNo, @"^4[0-9]{12}(?:[0-9]{3})?$").Success)
                    {
                        return "Card_VISA";
                    }

                    else if (Regex.Match(cardNo, @"^(?:5[1-5][0-9]{2}|222[1-9]|22[3-9][0-9]|2[3-6][0-9]{2}|27[01][0-9]|2720)[0-9]{12}$").Success)
                    {
                        return "Card_Mastercard";
                    }

                    else if (Regex.Match(cardNo, @"^3[47][0-9]{13}$").Success)
                    {
                        return "Card_Amex";
                    }

                    else if (Regex.Match(cardNo, @"^6(?:011|5[0-9]{2})[0-9]{12}$").Success)
                    {
                        return "Card_Discover";
                    }

                    else if (Regex.Match(cardNo, @"^(?:2131|1800|35\d{3})\d{11}$").Success)
                    {
                        return "card_jcb";
                    }
                    else
                    {
                        return "defaultcard";
                    }

                }
            }
            return "defaultcard";
        }

        //Method created to show description inside item details page at  item condition field.
        public static string ItemConditionDescription(string condition)
        {
            var description = "";
            switch (condition)
            {
                case "NWT":
                    description = "New with price tag attached.";
                    break;
                case "NWOT":
                    description = "New with price tag removed.";
                    break;
                case "Used":
                    description = "Preowned.";
                    break;
                default:
                    break;
            }
            return description;
        }

        public static string GetExtraCategories(string category, List<int> gender)
        {
            try
            {
                if (gender[0] == 1)
                {
                    var keywordsMen = new Dictionary<string, string[]>();
                    //keywordsMen.Add("shirts", new string[] { "Shirts", "Shirt", "Sweaters" });
                    //keywordsMen.Add("bottoms", new string[] { "Jeans", "pants", "Shorts" });
                    keywordsMen.Add("coats", new string[] { "Jackets & Coats" });
                    keywordsMen.Add("suits", new string[] { "Suits & Tuxedos" });
                    keywordsMen.Add("socks", new string[] { "Socks & Underwear" });
                    return keywordsMen[category.ToLower()][0];
                }
                else
                {
                    var keywordsWomen = new Dictionary<string, string[]>();
                    //keywordsWomen.Add("tops", new string[] { "Tops", "Sweaters" });
                    //keywordsWomen.Add("bottoms", new string[] { "Jeans", "pants", "Shorts", "Skirts" });
                    keywordsWomen.Add("coats", new string[] { "Jackets & Coats" });
                    keywordsWomen.Add("suits", new string[] { "Suits & Separates" });
                    keywordsWomen.Add("swim", new string[] { "Swimsuits" });
                    keywordsWomen.Add("jewelry", new string[] { "Jewelry" });
                    //keywordsWomen.Add("other", new string[] { "Intimates & Sleepwear", "Other" });
                    return keywordsWomen[category.ToLower()][0];
                }
            }
            catch (Exception ex)
            {

            }
            return category;
        }

        public static void ShowActionAlert(string message, string okText = "OK", Action okAction = null)
        {
            var alertConfig = new AlertConfig
            {
                Message = message,
                OkText = "OK",
                OnAction = okAction
            };
            Acr.UserDialogs.UserDialogs.Instance.Alert(alertConfig);
        }

        public async static void LogoutConfirm()
        {
            var res = await Acr.UserDialogs.UserDialogs.Instance.ConfirmAsync(Constant.logoutConfirmMsgStr, "Logout", Constant.OKStr, Constant.CancelStr);
            if (res)
            {
                //Global.GlobalRecentProductList.Clear();
                Constant.LoginUserData = null;
                Global.Username = null;
                Global.Password = null;
                Global.GenderIndex = 1;
                Global.StoreIndex = 1;
                Global.Storecategory = Constant.ClothingStr;
                Constant.globalAddedCard = new CardListModel();
                Constant.globalBankAccount = new BankAccountModel();
                Constant.globalSelectedAddress = new AddAddressModel();
                Constant.globalSelectedFromAddress = new AddAddressModel();
                Constant.globalTax = 0;
                Global.globalCartList = new List<CartModel>();
                Global.GlobalAddressList = new ObservableCollection<AddAddressModel>();
                Global.GlobalShipFromAddressList = new ObservableCollection<AddAddressModel>();
                Global.GlobalCardList = new ObservableCollection<CardListModel>();
                Global.BankAccountsList = new ObservableCollection<BankAccountModel>();

                Application.Current.Properties.Clear();
                Settings.FirstRun = true;
                await Application.Current.SavePropertiesAsync();
                App.Current.MainPage = new NavigationPage(new LoginPage());

            }
        }

        public async static void LogoutWithoutConfirm()
        {
            //Global.GlobalRecentProductList.Clear();
            Constant.LoginUserData = null;
            Global.Username = null;
            Global.Password = null;
            Global.GenderIndex = 1;
            Global.StoreIndex = 1;
            Global.Storecategory = Constant.ClothingStr;
            Constant.globalAddedCard = new CardListModel();
            Constant.globalBankAccount = new BankAccountModel();
            Constant.globalSelectedAddress = new AddAddressModel();
            Constant.globalSelectedFromAddress = new AddAddressModel();
            Constant.globalTax = 0;
            Global.globalCartList = new List<CartModel>();
            Global.GlobalAddressList = new ObservableCollection<AddAddressModel>();
            Global.GlobalShipFromAddressList = new ObservableCollection<AddAddressModel>();
            Global.GlobalCardList = new ObservableCollection<CardListModel>();
            Global.BankAccountsList = new ObservableCollection<BankAccountModel>();
            Application.Current.Properties.Clear();
            await Application.Current.SavePropertiesAsync();
            App.Current.MainPage = new NavigationPage(new LoginPage());

        }

        public static string GetTwoLetterforState(string state)
        {
            try
            {
                List<string> StateName = new List<string>() { "ALABAMA", "ALASKA", "AMERICAN SAMOA", "ARIZONA", "ARKANSAS", "CALIFORNIA", "COLORADO", "CONNECTICUT", "DELAWARE", "DISTRICT OF COLUMBIA", "FLORIDA", "GEORGIA", "GUAM", "HAWAII", "IDAHO", "ILLINOIS", "INDIANA", "IOWA", "KANSAS", "KENTUCKY", "LOUISIANA", "MAINE", "MARYLAND", "MASSACHUSETTS", "MICHIGAN", "MINNESOTA", "MISSISSIPPI", "MISSOURI", "MONTANA", "NEBRASKA", "NEVADA", "NEW HAMPSHIRE", "NEW JERSEY", "NEW MEXICO", "NEW YORK", "NORTH CAROLINA", "NORTH DAKOTA", "NORTHERN MARIANA IS", "OHIO", "OKLAHOMA", "OREGON", "PENNSYLVANIA", "PUERTO RICO", "RHODE ISLAND", "SOUTH CAROLINA", "SOUTH DAKOTA", "TENNESSEE", "TEXAS", "UTAH", "VERMONT", "VIRGINIA", "VIRGIN ISLANDS", "WASHINGTON", "WEST VIRGINIA", "WISCONSIN", "WYOMING" };
                List<string> StateAbb = new List<string>() { "AL", "AK", "AS", "AZ", "AR", "CA", "CO", "CT", "DE", "DC", "FL", "GA", "GU", "HI", "ID", "IL", "IN", "IA", "KS", "KY", "LA", "ME", "MD", "MA", "MI", "MN", "MS", "MO", "MT", "NE", "NV", "NH", "NJ", "NM", "NY", "NC", "ND", "MP", "OH", "OK", "OR", "PA", "PR", "RI", "SC", "SD", "TN", "TX", "UT", "VT", "VA", "VI", "WA", "WV", "WI", "WY" };


                if (state != null)
                {
                    if (!string.IsNullOrWhiteSpace(state))
                    {
                        var Stateindex = StateName.IndexOf(StateName.Where(x => x.ToLower() == state.ToLower()).FirstOrDefault());
                        var StateAbbLetter = StateAbb.ToArray()[Stateindex];
                        return StateAbbLetter;
                    }
                }
                return state;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return state;
            }
        
        }

        public static string GetFullNameforState(string stateletters)
        {
            try
            {
                List<string> StateName = new List<string>() { "ALABAMA", "ALASKA", "AMERICAN SAMOA", "ARIZONA", "ARKANSAS", "CALIFORNIA", "COLORADO", "CONNECTICUT", "DELAWARE", "DISTRICT OF COLUMBIA", "FLORIDA", "GEORGIA", "GUAM", "HAWAII", "IDAHO", "ILLINOIS", "INDIANA", "IOWA", "KANSAS", "KENTUCKY", "LOUISIANA", "MAINE", "MARYLAND", "MASSACHUSETTS", "MICHIGAN", "MINNESOTA", "MISSISSIPPI", "MISSOURI", "MONTANA", "NEBRASKA", "NEVADA", "NEW HAMPSHIRE", "NEW JERSEY", "NEW MEXICO", "NEW YORK", "NORTH CAROLINA", "NORTH DAKOTA", "NORTHERN MARIANA IS", "OHIO", "OKLAHOMA", "OREGON", "PENNSYLVANIA", "PUERTO RICO", "RHODE ISLAND", "SOUTH CAROLINA", "SOUTH DAKOTA", "TENNESSEE", "TEXAS", "UTAH", "VERMONT", "VIRGINIA", "VIRGIN ISLANDS", "WASHINGTON", "WEST VIRGINIA", "WISCONSIN", "WYOMING" };
                List<string> StateAbb = new List<string>() { "AL", "AK", "AS", "AZ", "AR", "CA", "CO", "CT", "DE", "DC", "FL", "GA", "GU", "HI", "ID", "IL", "IN", "IA", "KS", "KY", "LA", "ME", "MD", "MA", "MI", "MN", "MS", "MO", "MT", "NE", "NV", "NH", "NJ", "NM", "NY", "NC", "ND", "MP", "OH", "OK", "OR", "PA", "PR", "RI", "SC", "SD", "TN", "TX", "UT", "VT", "VA", "VI", "WA", "WV", "WI", "WY" };


                if (stateletters != null)
                {
                    if (!string.IsNullOrWhiteSpace(stateletters))
                    {
                        var Stateindex = StateAbb.IndexOf(StateAbb.Where(x => x.ToLower() == stateletters.ToLower()).FirstOrDefault());
                        var StateAbbLetter = StateName.ToArray()[Stateindex];
                        return StateAbbLetter;
                    }
                }
                return stateletters;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return stateletters;
            }

        }

        public static string PaymentErrorMessage(string errorMessageCode)
        {
            try
            {
                var errorMsg = string.Empty;
                switch(errorMessageCode)
                {
                    case "authentication_required":
                           errorMsg =  "The card was declined as the transaction requires authentication.";
                        return errorMsg;

                    case "approve_with_id":
                        errorMsg = "The payment can’t be authorized.";
                        return errorMsg;

                    case "call_issuer":
                        errorMsg = "The card was declined for an unknown reason.";
                        return errorMsg;

                    case "card_not_supported":
                        errorMsg = "The card does not support this type of purchase.";
                        return errorMsg;

                    case "card_velocity_exceeded":
                        errorMsg = "The customer has exceeded the balance or credit limit available on their card.";
                        return errorMsg;

                    case "currency_not_supported":
                        errorMsg = "The card does not support the specified currency.";
                        return errorMsg;

                    case "do_not_honor":
                        errorMsg = "The card was declined for an unknown reason.";
                        return errorMsg;

                    case "do_not_try_again":
                        errorMsg = "The card was declined for an unknown reason.";
                        return errorMsg;

                    case "duplicate_transaction":
                        errorMsg = "A transaction with identical amount and credit card information was submitted very recently.";
                        return errorMsg;

                    case "expired_card":
                        errorMsg = "Expired card";
                        return errorMsg;

                    case "fraudulent":
                        errorMsg = "The payment was declined because Stripe suspects that it’s fraudulent.";
                        return errorMsg;

                    case "generic_decline":
                        errorMsg = "Card declined";
                        return errorMsg;

                    case "incorrect_number":
                        errorMsg = "The card number is incorrect.";
                        return errorMsg;

                    case "incorrect_cvc":
                        errorMsg = "Incorrect CVC";
                        return errorMsg;

                    case "incorrect_pin":
                        errorMsg = "The PIN entered is incorrect. ";
                        return errorMsg;

                    case "incorrect_zip":
                        errorMsg = "The postal code is incorrect.";
                        return errorMsg;

                    case "insufficient_funds":
                        errorMsg = "Card declined";
                        return errorMsg;

                    case "invalid_account":
                        errorMsg = "The card, or account the card is connected to, is invalid.";
                        return errorMsg;

                    case "invalid_amount":
                        errorMsg = "The payment amount is invalid, or exceeds the amount that’s allowed.";
                        return errorMsg;

                    case "invalid_cvc":
                        errorMsg = "The CVC number is incorrect.";
                        return errorMsg;

                    case "invalid_expiry_month":
                        errorMsg = "The expiration month is invalid.";
                        return errorMsg;

                    case "invalid_expiry_year":
                        errorMsg = "The expiration year is invalid.";
                        return errorMsg;

                    case "invalid_number":
                        errorMsg = "The card number is incorrect.";
                        return errorMsg;

                    case "issuer_not_available":
                        errorMsg = "The card issuer couldn’t be reached, so the payment couldn’t be authorized.";
                        return errorMsg;

                    case "lost_card":
                        errorMsg = "Card declined";
                        return errorMsg;

                    case "merchant_blacklist":
                        errorMsg = "The payment was declined because it matches a value on the Stripe user’s block list.";
                        return errorMsg;

                    case "new_account_information_available":
                        errorMsg = "The card, or account the card is connected to, is invalid.";
                        return errorMsg;

                    case "no_action_taken":
                        errorMsg = "The card was declined for an unknown reason.";
                        return errorMsg;

                    case "not_permitted":
                        errorMsg = "The payment isn’t permitted.";
                        return errorMsg;

                    case "offline_pin_required":
                        errorMsg = "The card was declined because it requires a PIN.";
                        return errorMsg;

                    case "online_or_offline_pin_required":
                        errorMsg = "The card was declined as it requires a PIN.";
                        return errorMsg;

                    case "pickup_card":
                        errorMsg = "The customer can’t use this card to make this payment";
                        return errorMsg;

                    case "pin_try_exceeded":
                        errorMsg = "The allowable number of PIN tries was exceeded.";
                        return errorMsg;

                    case "processing_error":
                        errorMsg = "Processing error decline";
                        return errorMsg;

                    case "reenter_transaction":
                        errorMsg = "The payment couldn’t be processed by the issuer for an unknown reason.";
                        return errorMsg;

                    case "restricted_card":
                        errorMsg = "The customer can’t use this card to make this payment ";
                        return errorMsg;

                    case "revocation_of_all_authorizations":
                        errorMsg = "The card was declined for an unknown reason.";
                        return errorMsg;

                    case "revocation_of_authorization":
                        errorMsg = "The card was declined for an unknown reason.";
                        return errorMsg;

                    case "security_violation":
                        errorMsg = "The card was declined for an unknown reason.";
                        return errorMsg;

                    case "service_not_allowed":
                        errorMsg = "The card was declined for an unknown reason.";
                        return errorMsg;

                    case "stolen_card":
                        errorMsg = "Card declined";
                        return errorMsg;

                    case "stop_payment_order":
                        errorMsg = "The card was declined for an unknown reason.";
                        return errorMsg;

                    case "testmode_decline":
                        errorMsg = "A Stripe test card number was used.";
                        return errorMsg;
                    
                    case "transaction_not_allowed":
                        errorMsg = "The card was declined for an unknown reason.";
                        return errorMsg;
                    
                    case "try_again_later":
                        errorMsg = "The card was declined for an unknown reason.";
                        return errorMsg; 
                    
                    case "withdrawal_count_limit_exceeded":
                        errorMsg = "The customer has exceeded the balance or credit limit available on their card.";
                        return errorMsg;

                     default:
                        errorMsg = "Card declined";
                        return errorMsg;

                }

            }
            catch (Exception ex)
            {
               Debug.WriteLine(ex.Message);
               var errorMsg = "Card declined";
                return errorMsg;
            }
        }

        public static ShipmentStatus GetShipmentStatus(int status)
        {
            try
            {
                switch (status)
                {
                    case 0:
                        {
                            return new ShipmentStatus()
                            {
                                StatusColor = "#1567A6",
                                StatusType = "Pending Shipment"
                            };
                        }
                    case 1:
                        {
                            return new ShipmentStatus()
                            {
                                StatusColor = "#73C16A",
                                StatusType = "Shipment Created"
                            };
                        }
                    case 2:
                        {
                            return new ShipmentStatus()
                            {
                                StatusColor = "#73C16A",
                                StatusType = "In Transit"
                            };

                        }
                    case 3:
                        {

                            return new ShipmentStatus()
                            {
                                StatusColor = "#EB9748",
                                StatusType = "Delivered"
                            };
                        }
                    case 4:
                        {
                            return new ShipmentStatus()
                            {
                                StatusColor = "#EB9748",
                                StatusType = "Accepted"
                            };
                        }
                    case 5:
                        {
                            return new ShipmentStatus()
                            {
                                StatusColor = "#FF0000",
                                StatusType = "Cancelled"
                            };

                        }
                    case 6:
                        {
                            return new ShipmentStatus()
                            {
                                StatusColor = "#ffe342",
                                StatusType = "In Dispute"
                            };

                        }
                    default:
                        {
                            return new ShipmentStatus()
                            {
                                StatusColor = "#1567A6",
                                StatusType = "Pending shipment"
                            };
                        }
                        
                }
            }
            catch (Exception ex)
            {

            }
            return new ShipmentStatus()
            {
                StatusColor = "",
                StatusType = "Pending shipment"
            };
        }

        public static FormattedAddress AddressFormatter(ShippingAddress shippingAddress)
        {
            FormattedAddress FullAddress = new FormattedAddress();
            if(shippingAddress != null)
            {
                if(!string.IsNullOrEmpty(shippingAddress.FullName))
                {
                    FullAddress.Name = shippingAddress.FullName;
                }

                if(!string.IsNullOrEmpty(shippingAddress.AddressLine1))
                {
                    FullAddress.AddressLine1 = shippingAddress.AddressLine1;
                }

                if(!string.IsNullOrEmpty(shippingAddress.AddressLine2))
                {
                    FullAddress.AddressLine1 += ", " + shippingAddress.AddressLine2;
                }

                if(!string.IsNullOrEmpty(shippingAddress.City))
                {
                    FullAddress.AddressLine2 = shippingAddress.City + ", ";
                }

                if(!string.IsNullOrEmpty(shippingAddress.State))
                {
                    FullAddress.AddressLine2 += shippingAddress.State;
                }

                if (!string.IsNullOrEmpty(shippingAddress.ZipCode))
                {
                    FullAddress.AddressLine2 += " " +  shippingAddress.ZipCode;
                }
            }

            return FullAddress;
        }


        public static string ProdDetailsFormatter(string price, string size, string brand)
        {
            string formatteddata = string.Empty;
      
            if (!string.IsNullOrEmpty(price))
            {
                formatteddata = price + " | ";
            }

            if (!string.IsNullOrEmpty(size))
            {
                formatteddata += size + " | ";
            }

            if (!string.IsNullOrEmpty(brand))
            {
                formatteddata += brand;
            }

            return formatteddata;
        }



        public static void AlertWithAction(string message,Action callBack, string actionTitle = "OK",string title="")
        {
            var alertConfig = new AlertConfig
            {
                Message = message,
                OkText = actionTitle,
                OnAction = callBack,
                Title = title
            };
            Acr.UserDialogs.UserDialogs.Instance.Alert(alertConfig);

        }

        public static async Task PopToPage<T>(INavigation navigation)
        {
            //First, we get the navigation stack as a list
            var pages = navigation.NavigationStack.ToList();

            //Then we invert it because it's from first to last and we need in the inverse order
            pages.Reverse();

            //Then we discard the current page
            pages.RemoveAt(0);

            var toRemove = new List<Page>();

            var tipoPagina = typeof(T);

            foreach (var page in pages)
            {
                if (page.GetType() == tipoPagina)
                    break;

                toRemove.Add(page);
            }

            foreach (var rvPage in toRemove)
            {
                navigation.RemovePage(rvPage);
            }

            await navigation.PopAsync();
        }

    }


    public static class StringHelper
    {
        private static CultureInfo ci = new CultureInfo("en-US");
        //Convert all first latter
        public static string ToTitleCase(this string str)
        {
            str = str.ToLower();
            var strArray = str.Split(' ');
            if (strArray.Length > 1)
            {
                strArray[0] = ci.TextInfo.ToTitleCase(strArray[0]);
                return string.Join(" ", strArray);
            }
            return ci.TextInfo.ToTitleCase(str);
        }

        public static string ToTitleCase(this string str, TitleCase tcase)
        {
            str = str.ToLower();
            switch (tcase)
            {
                case TitleCase.First:
                    var strArray = str.Split(' ');
                    if (strArray.Length > 1)
                    {
                        strArray[0] = ci.TextInfo.ToTitleCase(strArray[0]);
                        return string.Join(" ", strArray);
                    }
                    break;
                case TitleCase.All:
                    return ci.TextInfo.ToTitleCase(str);
                default:
                    break;
            }
            return ci.TextInfo.ToTitleCase(str);
        }


    }

    public enum TitleCase
    {
        First,
        All
    }

    public class ShipmentStatus : BaseViewModelWithoutProperty
    {
        private string _statuscolor;
       public string StatusColor 
        {
            get
            {
                return _statuscolor;
            }
            set
            {
                _statuscolor  = value;
                OnPropertyChanged("StatusColor");
            }
        }
        private string _StatusType;
        public string StatusType 
        {
            get
            {
                return _StatusType;
            }
            set
            {
                _StatusType = value;
                OnPropertyChanged("StatusColor");
            }
        }
    }

    public static class Settings
    {
        public static bool FirstRun
        {
            get => Preferences.Get(nameof(FirstRun), true);
            set => Preferences.Set(nameof(FirstRun), value);
        }
    }

    public class FormattedAddress
    {
        public string Name { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
    }
}
