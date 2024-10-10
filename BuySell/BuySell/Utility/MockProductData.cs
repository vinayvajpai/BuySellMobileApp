using BuySell.Helper;
using BuySell.Model;
using BuySell.View;
using BuySell.ViewModel;
using BuySell.Views;
using FFImageLoading;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using static BuySell.Model.CategoryModel;

namespace BuySell.Utility
{
    internal class MockProductData
    {

        #region MockProductData Instance
        private static MockProductData instance = null;
        public INavigation nav = null;
        public static MockProductData Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MockProductData();
                }
                return instance;
            }
        }
        #endregion

        #region MakeAnOfferFAQs Text
        public static string MakeAnOfferFAQs = @"

       <p style ='text-align:center'> This is where you can negotiate a better price directly with the seller!</p>

        <p style='margin:10px'>Here are some FAQs about offers...</p>

        <p style='margin:10px'>-Offers are private</p>

        <p style='margin:10px'>-Offers are binding and you must have an account with a valid payment method and shipping address on file to make one.</p>

        <p style='margin:10px'>-Sales tax is estimated at the time of your offer and will be finalized when the seller accepts your offer.</p>

        <p style='margin:10px'>-Offers expire after 24 hours.After this time expires, you can make another offer.</p>

        <p style='margin:10px'>-Within the 24 hour period a seller can accept your offer, counter your offer with another price, or decline.</p>

        <p style='margin:10px'>-If your offer is <strong>accepted</strong>, a final price will be calculated and you will be charged.For information about what happend next please see our<strong> FAQs</strong>.</p>

        <p style='margin:10px'>-If you receive a <strong>counter offer</strong>, you can accept, counter again, or decline.</p>

        <p style='margin:10px'>-If your offer is <strong>declined</strong>, you can make another offer before the original 24 hours expire.</p>

        <p style='margin:10px'>*Be advised that other buyers can submit offers or Buy Now while your offer is pending.Acceptance of offers is at the discretion of the seller. All other orders will be cancelled when the seller accepta an offer or a purchase is made via Buy Now.</p>

        <p style='margin:10px'>Need more help? <u>Contact Us</u></p> ";

        #endregion

        #region GetBannerList Method
        //public List<BannerModel> GetBannerList()
        //{
        //    var TempBannerList = new List<BannerModel>()
        //        {
        //            new BannerModel()
        //            {
        //                BannerImage="SlideshowCM_1",
        //                Store = "Clothing",
        //                GenderType = "M"

        //            },
        //             new BannerModel()
        //            {
        //                 BannerImage="SlideshowCM_2",
        //                 Store = "Clothing",
        //                 GenderType = "M"
        //            },
        //             new BannerModel()
        //            {
        //                 BannerImage="SlideshowCF_1",
        //                 Store = "Clothing",
        //                 GenderType = "F"
        //            },
        //            new BannerModel()
        //            {
        //                 BannerImage="SlideshowCF_2",
        //                 Store = "Clothing",
        //                 GenderType = "F"
        //            },
        //            new BannerModel()
        //            {
        //                BannerImage="SlideshowSM_1",
        //                Store = "Sneakers",
        //                GenderType = "M"

        //            },
        //            new BannerModel()
        //            {
        //                BannerImage="SlideshowSM_2",
        //                Store = "Sneakers",
        //                GenderType = "M"

        //            },
        //            new BannerModel()
        //            {
        //                BannerImage="SlideshowSF_1",
        //                Store = "Sneakers",
        //                GenderType = "F"

        //            },
        //            new BannerModel()
        //            {
        //                BannerImage="SlideshowSF_2",
        //                Store = "Sneakers",
        //                GenderType = "F"

        //            },
        //            new BannerModel()
        //            {
        //                BannerImage="SlideshowSTM_1",
        //                Store = "Streetwear",
        //                GenderType = "M"

        //            },
        //            new BannerModel()
        //            {
        //                BannerImage="SlideshowSTM_2",
        //                Store = "Streetwear",
        //                GenderType = "M"

        //            },
        //            new BannerModel()
        //            {
        //                BannerImage="SlideshowSTF_1",
        //                Store = "Streetwear",
        //                GenderType = "F"

        //            },
        //            new BannerModel()
        //            {
        //                BannerImage="SlideshowSTF_2",
        //                Store = "Streetwear",
        //                GenderType = "F"

        //            },
        //            new BannerModel()
        //            {
        //                BannerImage="SlideshowVM_1",
        //                Store = "Vintage",
        //                GenderType = "M"

        //            },
        //            new BannerModel()
        //            {
        //                BannerImage="SlideshowVM_2",
        //                Store = "Vintage",
        //                GenderType = "M"

        //            },
        //            new BannerModel()
        //            {
        //                BannerImage="SlideshowVF_1",
        //                Store = "Vintage",
        //                GenderType = "F"

        //            },
        //            new BannerModel()
        //            {
        //                BannerImage="SlideshowVF_2",
        //                Store = "Vintage",
        //                GenderType = "F"

        //            },
        //        };

        //    return TempBannerList;
        //}
        #endregion

        #region FilterBannerList Method
        //Method created to show Banner data on filter page based on gender type and store type
        public List<BannerModel> FilterBannerList(string StoreType, int GenderType)
        {
            //var BannerList = GetBennerListMethod();
            var BannerList = new List<BannerModel>();
            if (GenderType == 1)
            {
                return BannerList.Where(x => x.GenderType.ToLower() == "m" && x.Store.ToString() == StoreType.ToLower()).ToList();
            }

            else if (GenderType == 2)
            {
                return BannerList.Where(x => x.GenderType.ToLower() == "f" && x.Store.ToString() == StoreType.ToLower()).ToList();
            }

            else
            {
                return BannerList.Where(x => x.Store.ToString() == StoreType.ToLower()).ToList();
            }
        }
        //Method created to show Filtered data for banner get from api based on gender type and store type
        public List<BannerModel> FilterBannerListByAPI(string StoreType, int GenderType, List<BannerModel> BannerList)
        {
            var tempBannerList = new List<BannerModel>();
            if (GenderType == 1)
            {
                tempBannerList = BannerList.Where(x => x.GenderType.ToLower() == "m" && x.Store.ToLower() == StoreType.ToLower()).ToList();
            }

            else if (GenderType == 2)
            {
                tempBannerList = BannerList.Where(x => x.GenderType.ToLower() == "f" && x.Store.ToLower() == StoreType.ToLower()).ToList();
            }

            else
            {
                tempBannerList = BannerList.Where(x => x.Store.ToLower() == StoreType.ToLower()).ToList();
            }

            return tempBannerList;
        }

        #endregion

        #region GetProductList Method
        public List<DashBoardModel> GetProductList()
        {

            var ProductList = new ObservableCollection<DashBoardModel>(Global.postProductList).ToList();
            return GetMasterDataList();
        }
        #endregion

        #region FilterProductList Method
        //Method created to show filtered data for horizontal products list inside dashboard page
        public List<DashBoardModel> FilterProductList(string StoreType, int GenderType)
        {
            var ProductList = GetProductList();
            if (GenderType == 1)
            {
                return ProductList.Where(x => x.Gender.ToLower() == "m" && x.StoreType.ToLower() == StoreType.ToLower()).ToList();
            }

            else if (GenderType == 2)
            {
                return ProductList.Where(x => x.Gender.ToLower() == "f" && x.StoreType.ToLower() == StoreType.ToLower()).ToList();
            }

            else
            {
                return ProductList.Where(x => x.StoreType.ToLower() == StoreType.ToLower()).ToList();
            }
        }
        //Method created to show api data for horizontal products list inside dashboard page based on gender type and store type
        public List<DashBoardModel> FilterProductListByAPI(string StoreType, int GenderType, List<DashBoardModel> ProductList)
        {
            //var ProductList = GetProductList();
            var tempProductList = new List<DashBoardModel>();
            if (GenderType == 1)
            {
                tempProductList = ProductList.Where(x => x.Gender.ToLower() == "m" && x.StoreType.ToLower() == StoreType.ToLower()).OrderByDescending(p => p.Id).ToList();
            }
            else if (GenderType == 2)
            {
                tempProductList = ProductList.Where(x => x.Gender.ToLower() == "f" && x.StoreType.ToLower() == StoreType.ToLower()).OrderByDescending(p => p.Id).ToList();
            }

            else
            {
                tempProductList = ProductList.Where(x => x.StoreType.ToLower() == StoreType.ToLower()).OrderByDescending(p => p.Id).ToList();
            }

            return tempProductList;
        }

        #endregion

        #region GetMasterDataList Method
        public List<DashBoardModel> GetMasterDataList()
        {
            //if (Global.GlobalProductList == null || Global.GlobalProductList.Count == 0)
            //{
            //    var ProductList = new ObservableCollection<DashBoardModel>(Global.postProductList).ToList();
            //    try
            //    {
            //        string storeType = "Clothing";

            //        var clothingResM = new DashBoardModel()
            //        {
            //            Id = 1,
            //            ProductImage = "MensClothingTeeShirtTag",
            //            ProductName = "Rare beaded 7 TeeShirt",
            //            Discription = "Rare beaded 7 TeeShirt in awesome preloved. Size 31. Runs true to size.",//"Blue solid woven maxi dress, has a round short sleeves and flared hem...",
            //            Price = "$350",
            //            Size = "SIZE XL",
            //            SizeValue = "XL",
            //            ProductRating = "UnfillHeart",
            //            Gender = "M",
            //            ProductCondition = "Used",
            //            ProductColor = "Black",
            //            ProductCategory = "TeeShirt",
            //            ThemeCol = Global.GetThemeColor(Global.setThemeColor),
            //            Brand = "Supreme",
            //            StoreType = storeType
            //        };
            //        clothingResM.otherImages = new List<ImageSource>();
            //        clothingResM.otherImages.Add("MensClothingTeeShirt1");
            //        clothingResM.otherImages.Add("MensClothingTeeShirt2");

            //        ProductList.Add(clothingResM);

            //        var clothingResM1 = new DashBoardModel()
            //        {
            //            Id = 2,
            //            ProductImage = "MensClothingJacketTag",
            //            ProductName = "Rare beaded 7 Jacket",
            //            Discription = "Rare beaded 7 Jacket in awesome preloved. Size 31. Runs true to size.",//"Blue solid woven maxi dress, has a round neck short sleeves and flared hem...",
            //            Price = "$350",
            //            Size = "SIZE XL",
            //            SizeValue = "XL",
            //            ProductRating = "UnfillHeart",
            //            Gender = "M",
            //            ProductCondition = "NWT",
            //            ProductColor = "Tan",
            //            ProductCategory = "Jacket",
            //            ThemeCol = Global.GetThemeColor(Global.setThemeColor),
            //            Brand = "Reebok",
            //            StoreType = storeType
            //        };
            //        clothingResM1.otherImages = new List<ImageSource>();
            //        clothingResM1.otherImages.Add("MensClothingJacket1");
            //        clothingResM1.otherImages.Add("MensClothingJacket2");
            //        clothingResM1.otherImages.Add("MensClothingJacket3");
            //        clothingResM1.otherImages.Add("MensClothingJacket4");
            //        clothingResM1.otherImages.Add("MensClothingJacket5");

            //        ProductList.Add(clothingResM1);

            //        var clothingResW = new DashBoardModel()
            //        {
            //            Id = 3,
            //            ProductImage = "ClothingWTopTag",
            //            ProductName = "Rare beaded 7 Top",
            //            Discription = "Rare beaded 7 Top in awesome preloved. Size 31. Runs true to size.",
            //            Price = "$350",
            //            Size = "SIZE XL",
            //            SizeValue = "XL",
            //            ProductRating = "UnfillHeart",
            //            Gender = "F",
            //            ProductCondition = "Uesd",
            //            ProductColor = "Orange",
            //            ProductCategory = "Top",
            //            ThemeCol = Global.GetThemeColor(Global.setThemeColor),
            //            Brand = "Coogi",
            //            StoreType = storeType
            //        };
            //        clothingResW.otherImages = new List<ImageSource>();
            //        clothingResW.otherImages.Add("ClothingWTop1");
            //        clothingResW.otherImages.Add("ClothingWTop2");
            //        clothingResW.otherImages.Add("ClothingWTop3");
            //        clothingResW.otherImages.Add("ClothingWTop4");

            //        ProductList.Add(clothingResW);

            //        var clothingResW1 = new DashBoardModel()
            //        {
            //            Id = 4,
            //            ProductImage = "ClothingWJeansTag",
            //            ProductName = "Rare beaded 7 Jeans",
            //            Discription = "Rare beaded 7 Jeans in awesome preloved. Size 31. Runs true to size.",
            //            Price = "$350",
            //            Size = "SIZE 31",
            //            SizeValue = "31",
            //            ProductRating = "UnfillHeart",
            //            Gender = "F",
            //            ProductCondition = "NWOT",
            //            ProductColor = "Blue",
            //            ProductCategory = "Jeans",
            //            ThemeCol = Global.GetThemeColor(Global.setThemeColor),
            //            Brand = "Adidas",
            //            StoreType = storeType
            //        };
            //        clothingResW1.otherImages = new List<ImageSource>();
            //        clothingResW1.otherImages.Add("ClothingWJeans1");
            //        clothingResW1.otherImages.Add("ClothingWJeans2");
            //        clothingResW1.otherImages.Add("ClothingWJeans3");

            //        ProductList.Add(clothingResW1);


            //        storeType = "Sneakers";
            //        var sneakersResM = new DashBoardModel()
            //        {
            //            Id = 5,
            //            ProductImage = "MensSneakersAdidasTag",
            //            ProductName = "Cheetah Adidas Highs",
            //            Discription = "Sweet sneakers in preloved condition. Lots of life left.",//"Blue solid woven has a round neck",
            //            Price = "$470",
            //            Size = "10",
            //            SizeValue = "10",
            //            ProductRating = "UnfillHeart",
            //            Gender = "M",
            //            ProductCondition = "Used",
            //            ProductColor = "Blue",
            //            ProductCategory = "Shoes",
            //            ThemeCol = Global.GetThemeColor(Global.setThemeColor),
            //            Brand = "Adidas",
            //            StoreType = storeType
            //        };
            //        sneakersResM.otherImages = new List<ImageSource>();
            //        sneakersResM.otherImages.Add("MensSneakersAdidas1");
            //        sneakersResM.otherImages.Add("MensSneakersAdidas2");
            //        sneakersResM.otherImages.Add("MensSneakersAdidas3");
            //        sneakersResM.otherImages.Add("MensSneakersAdidas4");

            //        ProductList.Add(sneakersResM);

            //        var sneakersResM1 = new DashBoardModel()
            //        {
            //            Id = 6,
            //            ProductImage = "MensSneakersGriffeyTag",
            //            ProductName = "Cheetah Griffey Highs",
            //            Discription = "Sweet sneakers in preloved condition. Lots of life left.",//"Blue solid woven has a round neck",
            //            Price = "$470",
            //            Size = "5.5",
            //            SizeValue = "5.5",
            //            ProductRating = "UnfillHeart",
            //            Gender = "M",
            //            ProductCondition = "NWT",
            //            ProductColor = "Black",
            //            ProductCategory = "Shoes",
            //            ThemeCol = Global.GetThemeColor(Global.setThemeColor),
            //            Brand = "Griffey",
            //            StoreType = storeType
            //        };
            //        sneakersResM1.otherImages = new List<ImageSource>();
            //        sneakersResM1.otherImages.Add("MensSneakersGriffey1");
            //        sneakersResM1.otherImages.Add("MensSneakersGriffey2");
            //        sneakersResM1.otherImages.Add("MensSneakersGriffey3");
            //        sneakersResM1.otherImages.Add("MensSneakersGriffey4");
            //        sneakersResM1.otherImages.Add("MensSneakersGriffey5");
            //        sneakersResM1.otherImages.Add("MensSneakersGriffey6");

            //        ProductList.Add(sneakersResM1);

            //        var sneakersResM2 = new DashBoardModel()
            //        {
            //            Id = 7,
            //            ProductImage = "MensSneakersJordanTag",
            //            ProductName = "Cheetah Jordan Highs",
            //            Discription = "Sweet sneakers in preloved condition. Lots of life left.",//"Blue solid woven has a round neck",
            //            Price = "$470",
            //            Size = "9.5",
            //            SizeValue = "9.5",
            //            ProductRating = "UnfillHeart",
            //            Gender = "M",
            //            ProductCondition = "NWOT",
            //            ProductColor = "White",
            //            ProductCategory = "Shoes",
            //            ThemeCol = Global.GetThemeColor(Global.setThemeColor),
            //            Brand = "Jordan",
            //            StoreType = storeType
            //        };
            //        sneakersResM2.otherImages = new List<ImageSource>();
            //        sneakersResM2.otherImages.Add("MensSneakersJordan1");
            //        sneakersResM2.otherImages.Add("MensSneakersJordan2");
            //        sneakersResM2.otherImages.Add("MensSneakersJordan3");
            //        sneakersResM2.otherImages.Add("MensSneakersJordan4");

            //        ProductList.Add(sneakersResM2);

            //        var sneakersResW = new DashBoardModel()
            //        {
            //            Id = 8,
            //            ProductImage = "SneakersWHighTag",
            //            ProductName = "Cheetah Nike Highs",
            //            Discription = "Sweet sneakers in preloved condition. Lots of life left.",
            //            Price = "$470",
            //            Size = "7.5",
            //            SizeValue = "7.5",
            //            ProductRating = "UnfillHeart",
            //            Gender = "F",
            //            ProductCondition = "NWT",
            //            ProductColor = "Black/Yellow",
            //            ProductCategory = "Shoes",
            //            ThemeCol = Global.GetThemeColor(Global.setThemeColor),
            //            Brand = "Nike",
            //            StoreType = storeType
            //        };

            //        sneakersResW.otherImages = new List<ImageSource>();
            //        sneakersResW.otherImages.Add("SneakersWHigh1");
            //        sneakersResW.otherImages.Add("SneakersWHigh2");
            //        sneakersResW.otherImages.Add("SneakersWHigh3");
            //        sneakersResW.otherImages.Add("SneakersWHigh4");
            //        sneakersResW.otherImages.Add("SneakersWHigh5");

            //        ProductList.Add(sneakersResW);

            //        var sneakersResW1 = new DashBoardModel()
            //        {
            //            Id = 9,
            //            ProductImage = "SneakersWNikeTag",
            //            ProductName = "Nike Running",
            //            Discription = "Blue and gray Nike Running shoes in EUC.",
            //            Price = "$470",
            //            Size = "9",
            //            SizeValue = "9",
            //            ProductRating = "UnfillHeart",
            //            Gender = "F",
            //            ProductCondition = "Used",
            //            ProductColor = "Gray/Blue",
            //            ProductCategory = "Shoes",
            //            ThemeCol = Global.GetThemeColor(Global.setThemeColor),
            //            Brand = "Nike",
            //            StoreType = storeType
            //        };

            //        sneakersResW1.otherImages = new List<ImageSource>();
            //        sneakersResW1.otherImages.Add("SneakersWNike1");
            //        sneakersResW1.otherImages.Add("SneakersWNike2");
            //        sneakersResW1.otherImages.Add("SneakersWNike3");
            //        sneakersResW1.otherImages.Add("SneakersWNike4");

            //        ProductList.Add(sneakersResW1);

            //        var sneakersResW2 = new DashBoardModel()
            //        {
            //            Id = 10,
            //            ProductImage = "SneakersWStarsTag",
            //            ProductName = "Golden Goose GGDB/SSTAR",
            //            Discription = "Golden Goose Star Sneakers only worn a few times and in great condition.",
            //            Price = "$470",
            //            Size = "3.9",
            //            SizeValue = "3.9",
            //            ProductRating = "UnfillHeart",
            //            Gender = "F",
            //            ProductCondition = "NWT",
            //            ProductColor = "White",
            //            ProductCategory = "Shoes",
            //            ThemeCol = Global.GetThemeColor(Global.setThemeColor),
            //            Brand = "Golden Goose",
            //            StoreType = storeType
            //        };

            //        sneakersResW2.otherImages = new List<ImageSource>();
            //        sneakersResW2.otherImages.Add("SneakersWStars1");
            //        sneakersResW2.otherImages.Add("SneakersWStars2");
            //        sneakersResW2.otherImages.Add("SneakersWStars3");
            //        sneakersResW2.otherImages.Add("SneakersWStars4");
            //        sneakersResW2.otherImages.Add("SneakersWStars5");
            //        sneakersResW2.otherImages.Add("SneakersWStars6");

            //        ProductList.Add(sneakersResW2);

            //        storeType = "Streetwear";
            //        var streetwearResM = new DashBoardModel()
            //        {
            //            Id = 11,
            //            ProductImage = "StreetwearMSupremeTag",
            //            ProductName = "Supreme x The North Face “Photo” Tee shirt",
            //            Discription = "New Condition.",//"Blue solid woven has a round neck",
            //            Price = "$470",
            //            Size = "SIZE XL",
            //            SizeValue = "XL",
            //            ProductRating = "UnfillHeart",
            //            Gender = "M",
            //            ProductCondition = "NWT",
            //            ProductColor = "Red",
            //            ProductCategory = "TeeShirt",
            //            ThemeCol = Global.GetThemeColor(Global.setThemeColor),
            //            Brand = "Coogi",
            //            StoreType = storeType
            //        };

            //        streetwearResM.otherImages = new List<ImageSource>();
            //        streetwearResM.otherImages.Add("StreetwearMSupreme1");
            //        streetwearResM.otherImages.Add("StreetwearMSupreme2");
            //        streetwearResM.otherImages.Add("StreetwearMSupreme3");

            //        ProductList.Add(streetwearResM);

            //        var streetwearResM1 = new DashBoardModel()
            //        {
            //            Id = 12,
            //            ProductImage = "StreetwearMSupremeHatTag",
            //            ProductName = "Supreme x Raiders x 47 beanie winter cap",
            //            Discription = "New in bag. Never worn. Perfect condition.",//"Blue solid woven has a round neck",
            //            Price = "$470",
            //            Size = "SIZE OS",
            //            SizeValue = "OS",
            //            ProductRating = "UnfillHeart",
            //            Gender = "M",
            //            ProductCondition = "Used",
            //            ProductColor = "Tan",
            //            ProductCategory = "Cap",
            //            ThemeCol = Global.GetThemeColor(Global.setThemeColor),
            //            Brand = "Supreme",
            //            StoreType = storeType
            //        };

            //        streetwearResM1.otherImages = new List<ImageSource>();
            //        streetwearResM1.otherImages.Add("StreetwearMSupremeHat1");
            //        streetwearResM1.otherImages.Add("StreetwearMSupremeHat2");
            //        streetwearResM1.otherImages.Add("StreetwearMSupremeHat3");

            //        ProductList.Add(streetwearResM1);

            //        var streetwearResM2 = new DashBoardModel()
            //        {
            //            Id = 13,
            //            ProductImage = "StreetwearMJacketTag",
            //            ProductName = "Jacket x Raiders x 47 beanie winter Outwear",
            //            Discription = "New in bag. Never worn. Perfect condition.",//"Blue solid woven has a round neck",
            //            Price = "$470",
            //            Size = "SIZE OS",
            //            SizeValue = "OS",
            //            ProductRating = "UnfillHeart",
            //            Gender = "M",
            //            ProductCondition = "NWT",
            //            ProductColor = "Tan",
            //            ProductCategory = "Outwear",
            //            ThemeCol = Global.GetThemeColor(Global.setThemeColor),
            //            Brand = "Nike",
            //            StoreType = storeType
            //        };

            //        streetwearResM2.otherImages = new List<ImageSource>();
            //        streetwearResM2.otherImages.Add("StreetwearMJacket1");
            //        streetwearResM2.otherImages.Add("StreetwearMJacket2");
            //        streetwearResM2.otherImages.Add("StreetwearMJacket3");
            //        streetwearResM2.otherImages.Add("StreetwearMJacket4");
            //        streetwearResM2.otherImages.Add("StreetwearMJacket5");

            //        ProductList.Add(streetwearResM2);

            //        var streetwearResW = new DashBoardModel()
            //        {
            //            Id = 14,
            //            ProductImage = "StreetwearWHoodieTag",
            //            ProductName = "Hoodie x Raiders x 47 beanie winter Hoodie",
            //            Discription = "New in bag. Never worn. Perfect condition.",//"Blue solid woven has a round neck",
            //            Price = "$470",
            //            Size = "SIZE XL",
            //            SizeValue = "XL",
            //            ProductRating = "UnfillHeart",
            //            Gender = "F",
            //            ProductCondition = "Used",
            //            ProductColor = "Black",
            //            ProductCategory = "Outwear",
            //            ThemeCol = Global.GetThemeColor(Global.setThemeColor),
            //            Brand = "Adidas",
            //            StoreType = storeType
            //        };
            //        streetwearResW.otherImages = new List<ImageSource>();
            //        streetwearResW.otherImages.Add("StreetwearWHoodie1");
            //        streetwearResW.otherImages.Add("StreetwearWHoodie2");
            //        streetwearResW.otherImages.Add("StreetwearWHoodie3");
            //        streetwearResW.otherImages.Add("StreetwearWHoodie4");
            //        streetwearResW.otherImages.Add("StreetwearWHoodie5");
            //        streetwearResW.otherImages.Add("StreetwearWHoodie6");

            //        ProductList.Add(streetwearResW);

            //        var streetwearResW1 = new DashBoardModel()
            //        {
            //            Id = 15,
            //            ProductImage = "StreetwearWSunglassesTag",
            //            ProductName = "New Sunglasses",
            //            Discription = "New in bag. Never worn. Perfect condition.",//"Blue solid woven has a round neck",
            //            Price = "$470",
            //            Size = "SIZE XL",
            //            SizeValue = "XL",
            //            ProductRating = "UnfillHeart",
            //            Gender = "F",
            //            ProductCondition = "NWT",
            //            ProductColor = "Black",
            //            ProductCategory = "Accessories",
            //            ThemeCol = Global.GetThemeColor(Global.setThemeColor),
            //            Brand = "Nike",
            //            StoreType = storeType
            //        };

            //        streetwearResW1.otherImages = new List<ImageSource>();
            //        streetwearResW1.otherImages.Add("StreetwearWSunglasses1");
            //        streetwearResW1.otherImages.Add("StreetwearWSunglasses2");
            //        streetwearResW1.otherImages.Add("StreetwearWSunglasses3");
            //        streetwearResW1.otherImages.Add("StreetwearWSunglasses4");

            //        ProductList.Add(streetwearResW1);

            //        var streetwearResW2 = new DashBoardModel()
            //        {
            //            Id = 16,
            //            ProductImage = "StreetwearWTeeTag",
            //            ProductName = "TeeShirt x Raiders x 47.",
            //            Discription = "New in bag. Never worn. Perfect condition.",//"Blue solid woven has a round neck",
            //            Price = "$470",
            //            Size = "SIZE XL",
            //            SizeValue = "XL",
            //            ProductRating = "UnfillHeart",
            //            Gender = "F",
            //            ProductCondition = "NWOT",
            //            ProductColor = "Black",
            //            ProductCategory = "TeeShirt",
            //            ThemeCol = Global.GetThemeColor(Global.setThemeColor),
            //            Brand = "Reebok",
            //            StoreType = storeType
            //        };

            //        streetwearResW2.otherImages = new List<ImageSource>();
            //        streetwearResW2.otherImages.Add("StreetwearWTee1");
            //        streetwearResW2.otherImages.Add("StreetwearWTee2");
            //        streetwearResW2.otherImages.Add("StreetwearWTee3");

            //        ProductList.Add(streetwearResW2);

            //        storeType = "Vintage";

            //        var vintageResM = new DashBoardModel()
            //        {
            //            Id = 17,
            //            ProductImage = "MensVintageJerseyTag",
            //            ProductName = "90’s Philadelphia Eagles Ricky Watters Reversible NFL jersey",
            //            Discription = "Great condition. Home and away reversible. Heavyweight and so awesome. Go Eagles!",//"Blue solid woven has a round neck",
            //            Price = "$470",
            //            Size = "SIZE XL",
            //            SizeValue = "XL",
            //            ProductRating = "UnfillHeart",
            //            Gender = "M",
            //            ProductCondition = "Used",
            //            ProductColor = "Black",
            //            ProductCategory = "Jersey",
            //            ThemeCol = Global.GetThemeColor(Global.setThemeColor),
            //            Brand = "Supreme",
            //            StoreType = storeType
            //        };
            //        vintageResM.otherImages = new List<ImageSource>();
            //        vintageResM.otherImages.Add("MensVintageJersey1");
            //        vintageResM.otherImages.Add("MensVintageJersey2");
            //        vintageResM.otherImages.Add("MensVintageJersey3");
            //        vintageResM.otherImages.Add("MensVintageJersey4");

            //        ProductList.Add(vintageResM);

            //        var vintageResM1 = new DashBoardModel()
            //        {
            //            Id = 18,
            //            ProductImage = "MensVintageSweaterTag",
            //            ProductName = "COOGI Classics Sweater from Australia",
            //            Discription = "Wonderful condition. Only wore it a few times. Looks almost new.",//"Blue solid woven has a round neck",
            //            Price = "$470",
            //            Size = "SIZE XL",
            //            SizeValue = "XL",
            //            ProductRating = "UnfillHeart",
            //            Gender = "M",
            //            ProductCondition = "Used",
            //            ProductColor = "Orange",
            //            ProductCategory = "Sweaters",
            //            ThemeCol = Global.GetThemeColor(Global.setThemeColor),
            //            Brand = "Coogi",
            //            StoreType = storeType
            //        };
            //        vintageResM1.otherImages = new List<ImageSource>();
            //        vintageResM1.otherImages.Add("MensVintageSweater1");
            //        vintageResM1.otherImages.Add("MensVintageSweater2");
            //        vintageResM1.otherImages.Add("MensVintageSweater3");
            //        vintageResM1.otherImages.Add("MensVintageSweater4");

            //        ProductList.Add(vintageResM1);

            //        var vintageResM2 = new DashBoardModel()
            //        {
            //            Id = 19,
            //            ProductImage = "MensVintageTeeShirtTag",
            //            ProductName = "COOGI Classics Tops from Australia",
            //            Discription = "Wonderful condition. Only wore it a few times. Looks almost new.",//"Blue solid woven has a round neck",
            //            Price = "$470",
            //            Size = "SIZE XL",
            //            SizeValue = "XL",
            //            ProductRating = "UnfillHeart",
            //            Gender = "M",
            //            ProductCondition = "NWT",
            //            ProductColor = "Orange",
            //            ProductCategory = "Tops",
            //            ThemeCol = Global.GetThemeColor(Global.setThemeColor),
            //            Brand = "Nike",
            //            StoreType = storeType
            //        };
            //        vintageResM2.otherImages = new List<ImageSource>();
            //        vintageResM2.otherImages.Add("MensVintageTeeShirt1");
            //        vintageResM2.otherImages.Add("MensVintageTeeShirt2");
            //        vintageResM2.otherImages.Add("MensVintageTeeShirt3");

            //        ProductList.Add(vintageResM2);

            //        var vintageResW = new DashBoardModel()
            //        {
            //            Id = 20,
            //            ProductImage = "VintageWGlassesTag",
            //            ProductName = "COOGI Classics Glasses from Australia",
            //            Discription = "Wonderful condition. Only wore it a few times. Looks almost new.",//"Blue solid woven has a round neck",
            //            Price = "$470",
            //            Size = "SIZE XL",
            //            SizeValue = "XL",
            //            ProductRating = "UnfillHeart",
            //            Gender = "F",
            //            ProductCondition = "NWT",
            //            ProductColor = "Red",
            //            ProductCategory = "Accessories",
            //            ThemeCol = Global.GetThemeColor(Global.setThemeColor),
            //            Brand = "Coogi",
            //            StoreType = storeType,
            //        };
            //        vintageResW.otherImages = new List<ImageSource>();
            //        vintageResW.otherImages.Add("VintageWGlasses1");
            //        vintageResW.otherImages.Add("VintageWGlasses2");
            //        vintageResW.otherImages.Add("VintageWGlasses3");
            //        vintageResW.otherImages.Add("VintageWGlasses4");
            //        vintageResW.otherImages.Add("VintageWGlasses5");
            //        vintageResW.otherImages.Add("VintageWGlasses6");

            //        ProductList.Add(vintageResW);

            //        var vintageResW1 = new DashBoardModel()
            //        {
            //            Id = 21,
            //            ProductImage = "VintageWPinkTag",
            //            ProductName = "COOGI Classics Pink from Australia",
            //            Discription = "Wonderful condition. Only wore it a few times. Looks almost new.",//"Blue solid woven has a round neck",
            //            Price = "$470",
            //            Size = "SIZE XL",
            //            SizeValue = "XL",
            //            ProductRating = "UnfillHeart",
            //            Gender = "F",
            //            ProductCondition = "Used",
            //            ProductColor = "Black",
            //            ProductCategory = "Tops",
            //            ThemeCol = Global.GetThemeColor(Global.setThemeColor),
            //            Brand = "Coogi",
            //            StoreType = storeType,
            //        };
            //        vintageResW1.otherImages = new List<ImageSource>();
            //        vintageResW1.otherImages.Add("VintageWPink1");
            //        vintageResW1.otherImages.Add("VintageWPink2");
            //        vintageResW1.otherImages.Add("VintageWPink3");

            //        ProductList.Add(vintageResW1);

            //        var vintageResW2 = new DashBoardModel()
            //        {
            //            Id = 22,
            //            ProductImage = "VintageWBathingSuitTag",
            //            ProductName = "COOGI Classics BathingSuit from Australia",
            //            Discription = "Wonderful condition. Only wore it a few times. Looks almost new.",//"Blue solid woven has a round neck",
            //            Price = "$470",
            //            Size = "SIZE XL",
            //            SizeValue = "XL",
            //            ProductRating = "UnfillHeart",
            //            Gender = "F",
            //            ProductCondition = "NWOT",
            //            ProductColor = "Black",
            //            ProductCategory = "Tops",
            //            ThemeCol = Global.GetThemeColor(Global.setThemeColor),
            //            Brand = "Nike",
            //            StoreType = storeType,
            //        };
            //        vintageResW2.otherImages = new List<ImageSource>();
            //        vintageResW2.otherImages.Add("VintageWBathingSuit1");
            //        vintageResW2.otherImages.Add("VintageWBathingSuit2");
            //        vintageResW2.otherImages.Add("VintageWBathingSuit3");
            //        vintageResW2.otherImages.Add("VintageWBathingSuit4");
            //        vintageResW2.otherImages.Add("VintageWBathingSuit5");
            //        vintageResW2.otherImages.Add("VintageWBathingSuit6");

            //        ProductList.Add(vintageResW2);
            //    }
            //    catch (Exception ex)
            //    {
            //        Debug.WriteLine(ex.Message);
            //    }
            //    ProductList.ToList().ForEach(u => { u.ThemeCol = Global.GetThemeColor(Global.setThemeColor); });
            //    Global.GlobalProductList = new ObservableCollection<DashBoardModel>(ProductList);
            //    return ProductList;
            //}
            //else
            //{
            //    var tempList = new ObservableCollection<DashBoardModel>(Global.GlobalProductList);
            //    var ProductList = new ObservableCollection<DashBoardModel>(Global.postProductList).ToList();
            //    Global.GlobalProductList.Clear();
            //    Global.GlobalProductList = new ObservableCollection<DashBoardModel>(tempList);
            //    Global.GlobalProductList.ToList().ForEach(u => { u.ThemeCol = Global.GetThemeColor(Global.setThemeColor); });
            //    return Global.GlobalProductList.ToList().Concat(ProductList).ToList();
            //}
            return Global.GlobalProductList.ToList();
        }

        #endregion

        #region GetCategoryList Method
        //Method created to show Main Categories (Men, Women) list and thier sub category list and to show categories inside sub categories for clothing, streetwear and vintage
        public List<Roots> GetCategoryList()
        {
            List<Roots> roots;
            //Categories inside   sub categories lof men and women both
            List<SubRoots> subRootWAccessories = new List<SubRoots>() { new SubRoots() { SubRoot = "Belts", Gender = "F", Root = "Women", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Electronics Cases", Gender = "F", Root = "Women", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Face Masks", Gender = "F", Root = "Women", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Glasses", Gender = "F", Root = "Women", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Gloves & Mittens", Gender = "F", Root = "Women", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Hair Accessories", Gender = "F", Root = "Women", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Hats", Gender = "F", Root = "Women", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Hosiery", Gender = "F", Root = "Women", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Scarves & Wraps", Gender = "F", Root = "Women", NodeTitle = "Accessories" }
            //, new SubRoots() { SubRoot = "Socks", Gender = "F", Root = "Women", NodeTitle = "Accessories" }
            , new SubRoots() { SubRoot = "Sunglasses", Gender = "F", Root = "Women", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Umbrellas", Gender = "F", Root = "Women", NodeTitle = "Accessories" }, 
                //new SubRoots() { SubRoot = "Watches", Gender = "F", Root = "Women", NodeTitle = "Accessories" } 
            };
            List<SubRoots> subRootWBages = new List<SubRoots>() { new SubRoots() { SubRoot = "Baby Bags", Gender = "F", Root = "Women", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Backpacks", Gender = "F", Root = "Women", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Belt Bags", Gender = "F", Root = "Women", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Clutches & Wristlets", Gender = "F", Root = "Women", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Cosmetic Bags", Gender = "F", Root = "Women", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Crossbody Bags", Gender = "F", Root = "Women", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Hobos", Gender = "F", Root = "Women", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Laptop Bags", Gender = "F", Root = "Women", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Luggage & Travel Bags", Gender = "F", Root = "Women", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Satchels", Gender = "F", Root = "Women", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Shoulder Bags", Gender = "F", Root = "Women", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Totes", Gender = "F", Root = "Women", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Wallets", Gender = "F", Root = "Women", NodeTitle = "Bags" } };
            List<SubRoots> subRootWDresses = new List<SubRoots>() { new SubRoots() { SubRoot = "Asymmetrical", Gender = "F", Root = "Women", NodeTitle = "Dresses" }, new SubRoots() { SubRoot = "High Low", Gender = "F", Root = "Women", NodeTitle = "Dresses" }, new SubRoots() { SubRoot = "Long Sleeve", Gender = "F", Root = "Women", NodeTitle = "Dresses" }, new SubRoots() { SubRoot = "Maxi", Gender = "F", Root = "Women", NodeTitle = "Dresses" }, new SubRoots() { SubRoot = "Midi", Gender = "F", Root = "Women", NodeTitle = "Dresses" }, new SubRoots() { SubRoot = "Mini", Gender = "F", Root = "Women", NodeTitle = "Dresses" }, new SubRoots() { SubRoot = "Prom", Gender = "F", Root = "Women", NodeTitle = "Dresses" }, new SubRoots() { SubRoot = "Rompers", Gender = "F", Root = "Women", NodeTitle = "Dresses" }, new SubRoots() { SubRoot = "Strapless", Gender = "F", Root = "Women", NodeTitle = "Dresses" }, new SubRoots() { SubRoot = "Wedding", Gender = "F", Root = "Women", NodeTitle = "Dresses" } };
            List<SubRoots> subRootWIntimatesSleepwear = new List<SubRoots>() { new SubRoots() { SubRoot = "Bras", Gender = "F", Root = "Women", NodeTitle = "Intimates & Sleepwear" }, new SubRoots() { SubRoot = "Chemises & Slips", Gender = "F", Root = "Women", NodeTitle = "Intimates & Sleepwear" }, new SubRoots() { SubRoot = "Pajamas", Gender = "F", Root = "Women", NodeTitle = "Intimates & Sleepwear" }, new SubRoots() { SubRoot = "Panties", Gender = "F", Root = "Women", NodeTitle = "Intimates & Sleepwear" }, new SubRoots() { SubRoot = "Robes", Gender = "F", Root = "Women", NodeTitle = "Intimates & Sleepwear" }, new SubRoots() { SubRoot = "Shapewear", Gender = "F", Root = "Women", NodeTitle = "Intimates & Sleepwear" } };
            List<SubRoots> subRootWJacketsCoats = new List<SubRoots>() { new SubRoots() { SubRoot = "Active", Gender = "F", Root = "Women", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Blazers", Gender = "F", Root = "Women", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Bombers", Gender = "F", Root = "Women", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Capes & Ponchos", Gender = "F", Root = "Women", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Denim & Utility", Gender = "F", Root = "Women", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Down & Puffers", Gender = "F", Root = "Women", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Fleece", Gender = "F", Root = "Women", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Fur & Faux Fur", Gender = "F", Root = "Women", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Leather & Faux Leather", Gender = "F", Root = "Women", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Rain Coats", Gender = "F", Root = "Women", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Ski", Gender = "F", Root = "Women", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Teddy", Gender = "F", Root = "Women", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Trench Coats", Gender = "F", Root = "Women", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Varsity", Gender = "F", Root = "Women", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Vests", Gender = "F", Root = "Women", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Windbreakers", Gender = "F", Root = "Women", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Wool & Pea Coats", Gender = "F", Root = "Women", NodeTitle = "Jackets & Coats" } };
            if (Global.Storecategory == Constant.ClothingStr)
            {
                Global.CatBySubList = new List<SubRoots>() { new SubRoots() { SubRoot = "Bootcut", Gender = "F", Root = "Women", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Crooped & Ankle", Gender = "F", Root = "Women", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Flare & Wide Leg", Gender = "F", Root = "Women", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "High Rise", Gender = "F", Root = "Women", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Jeggings", Gender = "F", Root = "Women", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Relaxed", Gender = "F", Root = "Women", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Skinny", Gender = "F", Root = "Women", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Straight", Gender = "F", Root = "Women", NodeTitle = "Jeans" } };
            }
            else
            {
                Global.CatBySubList = new List<SubRoots>() { new SubRoots() { SubRoot = "Bootcut", Gender = "F", Root = "Women", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Crooped & Ankle", Gender = "F", Root = "Women", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Flare & Wide Leg", Gender = "F", Root = "Women", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "High Rise", Gender = "F", Root = "Women", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Jeggings", Gender = "F", Root = "Women", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Relaxed", Gender = "F", Root = "Women", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Skinny", Gender = "F", Root = "Women", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Straight", Gender = "F", Root = "Women", NodeTitle = "Jeans" } };
            }
            List<SubRoots> subRootWJeans = Global.CatBySubList;
            List<SubRoots> subRootWJewelry = new List<SubRoots>() { new SubRoots() { SubRoot = "Bracelets", Gender = "F", Root = "Women", NodeTitle = "Jewelry" }, new SubRoots() { SubRoot = "Brooches", Gender = "F", Root = "Women", NodeTitle = "Jewelry" }, new SubRoots() { SubRoot = "Earrings", Gender = "F", Root = "Women", NodeTitle = "Jewelry" }, new SubRoots() { SubRoot = "Necklaces", Gender = "F", Root = "Women", NodeTitle = "Jewelry" }, new SubRoots() { SubRoot = "Rings", Gender = "F", Root = "Women", NodeTitle = "Jewelry" }, new SubRoots() { SubRoot = "Watches", Gender = "F", Root = "Women", NodeTitle = "Jewelry" } };
            List<SubRoots> subRootWPants = new List<SubRoots>() { new SubRoots() { SubRoot = "Bootcut & Flare", Gender = "F", Root = "Women", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "Crops & Capris", Gender = "F", Root = "Women", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "High Waisted", Gender = "F", Root = "Women", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "Leggings", Gender = "F", Root = "Women", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "Overalls & Jumpsuits", Gender = "F", Root = "Women", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "Skinny", Gender = "F", Root = "Women", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "Straight", Gender = "F", Root = "Women", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "Sweatpants & Joggers", Gender = "F", Root = "Women", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "Trousers & Wide Leg", Gender = "F", Root = "Women", NodeTitle = "Pants" } };
            List<SubRoots> subRootWShoes = new List<SubRoots>() { new SubRoots() { SubRoot = "Boots & Booties", Gender = "F", Root = "Women", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Clogs & Mules", Gender = "F", Root = "Women", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Espadrilles", Gender = "F", Root = "Women", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Flats & Loafers", Gender = "F", Root = "Women", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Heels", Gender = "F", Root = "Women", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Narrow", Gender = "F", Root = "Women", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Sandals", Gender = "F", Root = "Women", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Single Shoes", Gender = "F", Root = "Women", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Slippers", Gender = "F", Root = "Women", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Wedges", Gender = "F", Root = "Women", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Wide", Gender = "F", Root = "Women", NodeTitle = "Shoes" } };
            List<SubRoots> subRootWShorts = new List<SubRoots>() { new SubRoots() { SubRoot = "Active", Gender = "F", Root = "Women", NodeTitle = "Shorts" }, new SubRoots() { SubRoot = "Bermudas", Gender = "F", Root = "Women", NodeTitle = "Shorts" }, new SubRoots() { SubRoot = "Bikers", Gender = "F", Root = "Women", NodeTitle = "Shorts" }, new SubRoots() { SubRoot = "Cargo", Gender = "F", Root = "Women", NodeTitle = "Shorts" }, new SubRoots() { SubRoot = "Chino", Gender = "F", Root = "Women", NodeTitle = "Shorts" }, new SubRoots() { SubRoot = "Denim", Gender = "F", Root = "Women", NodeTitle = "Shorts" }, new SubRoots() { SubRoot = "High Waist", Gender = "F", Root = "Women", NodeTitle = "Shorts" }, new SubRoots() { SubRoot = "Short Shorts", Gender = "F", Root = "Women", NodeTitle = "Shorts" }, new SubRoots() { SubRoot = "Skorts", Gender = "F", Root = "Women", NodeTitle = "Shorts" } };
            List<SubRoots> subRootWSkirts = new List<SubRoots>() { new SubRoots() { SubRoot = "A Line", Gender = "F", Root = "Women", NodeTitle = "Skirts" }, new SubRoots() { SubRoot = "Asymmetrical", Gender = "F", Root = "Women", NodeTitle = "Skirts" }, new SubRoots() { SubRoot = "Denim", Gender = "F", Root = "Women", NodeTitle = "Skirts" }, new SubRoots() { SubRoot = "High Low", Gender = "F", Root = "Women", NodeTitle = "Skirts" }, new SubRoots() { SubRoot = "Maxi", Gender = "F", Root = "Women", NodeTitle = "Skirts" }, new SubRoots() { SubRoot = "Midi", Gender = "F", Root = "Women", NodeTitle = "Skirts" }, new SubRoots() { SubRoot = "Mini", Gender = "F", Root = "Women", NodeTitle = "Skirts" }, new SubRoots() { SubRoot = "Pencil", Gender = "F", Root = "Women", NodeTitle = "Skirts" }, new SubRoots() { SubRoot = "Pleated", Gender = "F", Root = "Women", NodeTitle = "Skirts" } };
            List<SubRoots> subRootWSuitsSeparates = new List<SubRoots>() { };
            List<SubRoots> subRootWSweaters = new List<SubRoots>() { new SubRoots() { SubRoot = "Cardigans", Gender = "F", Root = "Women", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Cashmere", Gender = "F", Root = "Women", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Cold Shoulder", Gender = "F", Root = "Women", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Cowl & Turtle Neck", Gender = "F", Root = "Women", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Crew & Scoop Necks", Gender = "F", Root = "Women", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Cropped", Gender = "F", Root = "Women", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Hoodie", Gender = "F", Root = "Women", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Ponchos", Gender = "F", Root = "Women", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Shrugs", Gender = "F", Root = "Women", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Tunic", Gender = "F", Root = "Women", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Vest", Gender = "F", Root = "Women", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "V-Neck", Gender = "F", Root = "Women", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Wrap", Gender = "F", Root = "Women", NodeTitle = "Sweaters" } };
            List<SubRoots> subRootWSwimsuits = new List<SubRoots>() { new SubRoots() { SubRoot = "Bikinis", Gender = "F", Root = "Women", NodeTitle = "Swimsuits" }, new SubRoots() { SubRoot = "Cover-Ups", Gender = "F", Root = "Women", NodeTitle = "Swimsuits" }, new SubRoots() { SubRoot = "One Pieces", Gender = "F", Root = "Women", NodeTitle = "Swimsuits" }, new SubRoots() { SubRoot = "Tankinis", Gender = "F", Root = "Women", NodeTitle = "Swimsuits" } };
            //List<SubRoots> subRootWTops = new List<SubRoots>() { new SubRoots() { SubRoot = "Blouses", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Bodysuits", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Button Down Shirts", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Cowl & Turtleneck", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Crop Tops", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Sweatshirts & Hoodies", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Tank Tops", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Tees: Long Sleeve", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Tees: Short Sleeve", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Tunics", Gender = "F", Root = "Women", NodeTitle = "Tops" } };
            List<SubRoots> subRootWTops = new List<SubRoots>() { new SubRoots() { SubRoot = "Blouses", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Bodysuits", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Button Down Shirts", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Camisoles", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Cardigans and Shrugs", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Cowl & Turtleneck", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Crop Tops", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Jerseys", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Muscle Tees", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Sweatshirts & Hoodies", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Tank Tops", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Tees: Long Sleeve", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Tees: Short Sleeve", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Tunics", Gender = "F", Root = "Women", NodeTitle = "Tops" } };
            List<SubRoots> subRootWOtherClothing = new List<SubRoots>() { };

            if (Global.Storecategory == Constant.StreetwearStr)
            {
                Global.CatBySubList = new List<SubRoots>() { new SubRoots() { SubRoot = "Belts", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Electronics Cases", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Face Masks", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Glasses", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Gloves & Scarves", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Hats", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Jewelry & Watches", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Pajamas & Robes", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Sunglasses", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Ties & Pocket Squares", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Umbrellas", Gender = "M", Root = "Men", NodeTitle = "Accessories" } };
            }
            else
            {
                Global.CatBySubList = new List<SubRoots>() { new SubRoots() { SubRoot = "Belts", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Electronics Cases", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Face Masks", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Glasses", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Gloves & Scarves", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Hats", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Jewelry & Watches", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Pajamas & Robes", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Sunglasses", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Ties & Pocket Squares", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Umbrellas", Gender = "M", Root = "Men", NodeTitle = "Accessories" } };
            }
            List<SubRoots> subRootMAccessories = Global.CatBySubList;
            List<SubRoots> subRootMBages = new List<SubRoots>() { new SubRoots() { SubRoot = "Backpacks", Gender = "M", Root = "Men", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Belt Bags", Gender = "M", Root = "Men", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Briefcases", Gender = "M", Root = "Men", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Duffle Bags", Gender = "M", Root = "Men", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Laptop Bags", Gender = "M", Root = "Men", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Luggage & Travel Bags", Gender = "M", Root = "Men", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Messenger Bags", Gender = "M", Root = "Men", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Toiletry Bags", Gender = "M", Root = "Men", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Wallets", Gender = "M", Root = "Men", NodeTitle = "Bags" } };
            if (Global.Storecategory == Constant.StreetwearStr)
            {
                //Global.CatBySubList = new List<SubRoots>() { new SubRoots() { SubRoot = "Active", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Blazers", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Bombers", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Capes", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Denim & Utility", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Down & Puffers", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Fleece", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Fur & Faux Fur", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Leather & Faux Leather", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Rain Coats", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Ski", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Sport Coats", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Trench Coats", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Varsity", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Vests", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Wool & Pea Coats ", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" } };
                Global.CatBySubList = new List<SubRoots>() { new SubRoots() { SubRoot = "Active", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Blazers", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Bombers", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Capes", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Denim & Utility", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Down & Puffers", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Fleece", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Fur & Faux Fur", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Leather & Faux Leather", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Rain Coats", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Ski", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Trench Coats", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Varsity", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Vests", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Wool & Pea Coats ", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" } };
            }
            else
            {
                //Global.CatBySubList = new List<SubRoots>() { new SubRoots() { SubRoot = "Active", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Blazers", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Bombers", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Capes", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Denim & Utility", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Down & Puffers", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Fleece", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Fur & Faux Fur", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Leather & Faux Leather", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Rain Coats", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Ski", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Sport Coats", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Trench Coats", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Varsity", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Vests", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Windbreakers", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Wool & Pea Coats ", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" } };
                Global.CatBySubList = new List<SubRoots>() { new SubRoots() { SubRoot = "Active", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Blazers", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Bombers", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Capes", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Denim & Utility", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Down & Puffers", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Fleece", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Fur & Faux Fur", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Leather & Faux Leather", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Rain Coats", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Ski", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Trench Coats", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Varsity", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Vests", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Windbreakers", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Wool & Pea Coats ", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" } };
            }
            List<SubRoots> subRootMJacketsCoats = Global.CatBySubList;
            List<SubRoots> subRootMJeans = new List<SubRoots>() { new SubRoots() { SubRoot = "Bootcut", Gender = "M", Root = "Men", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Relaxed", Gender = "M", Root = "Men", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Skinny", Gender = "M", Root = "Men", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Slim", Gender = "M", Root = "Men", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Straight", Gender = "M", Root = "Men", NodeTitle = "Jeans" } };
            List<SubRoots> subRootMPants = new List<SubRoots>() { new SubRoots() { SubRoot = "Cargo", Gender = "M", Root = "Men", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "Chinos & Khakis", Gender = "M", Root = "Men", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "Corduroy", Gender = "M", Root = "Men", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "Dress", Gender = "M", Root = "Men", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "Leggings", Gender = "M", Root = "Men", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "Overalls & Jumpsuits", Gender = "M", Root = "Men", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "Sweatpants & Joggers", Gender = "M", Root = "Men", NodeTitle = "Pants" } };
            if (Global.Storecategory == Constant.StreetwearStr)
            {
                Global.CatBySubList = new List<SubRoots>() { new SubRoots() { SubRoot = "Casual Button Downs", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Dress Shirts", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Jerseys", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Polos", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Sweatshirts & Hoodies", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Tank Tops", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Tees: Long Sleeve", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Tees: Short Sleeve", Gender = "M", Root = "Men", NodeTitle = "Shirts" } };
            }
            else if (Global.Storecategory != Constant.VintageStr)
            {

                Global.CatBySubList = new List<SubRoots>() { new SubRoots() { SubRoot = "Casual Button Downs", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Dress Shirts", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Jerseys", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Polos", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Sweatshirts & Hoodies", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Tank Tops", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Tees: Long Sleeve", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Tees: Short Sleeve", Gender = "M", Root = "Men", NodeTitle = "Shirts" } };
            }
            if (Global.Storecategory == Constant.VintageStr)
            {
                Global.CatBySubList = new List<SubRoots>() { new SubRoots() { SubRoot = "Casual Button Downs", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Dress Shirts", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Jerseys", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Polos", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Sweatshirts & Hoodies", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Tank Tops", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Tees: Long Sleeve", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Tees: Short Sleeve", Gender = "M", Root = "Men", NodeTitle = "Shirts" } };
            }
            else if (Global.Storecategory != Constant.StreetwearStr)
            {

                Global.CatBySubList = new List<SubRoots>() { new SubRoots() { SubRoot = "Casual Button Downs", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Dress Shirts", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Jerseys", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Polos", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Sweatshirts & Hoodies", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Tank Tops", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Tees: Long Sleeve", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Tees: Short Sleeve", Gender = "M", Root = "Men", NodeTitle = "Shirts" } };
            }
            List<SubRoots> subRootMShirts = Global.CatBySubList;
            List<SubRoots> subRootMShoes = new List<SubRoots>() { new SubRoots() { SubRoot = "Boat Shoes", Gender = "M", Root = "Men", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Boots", Gender = "M", Root = "Men", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Casual Shoes", Gender = "M", Root = "Men", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Dress Shoes", Gender = "M", Root = "Men", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Loafers & Drivers", Gender = "M", Root = "Men", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Sandals & Flip Flops", Gender = "M", Root = "Men", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Slippers", Gender = "M", Root = "Men", NodeTitle = "Shoes" } };
            List<SubRoots> subRootMShorts = new List<SubRoots>() { new SubRoots() { SubRoot = "Athletic", Gender = "M", Root = "Men", NodeTitle = "Shorts" }, new SubRoots() { SubRoot = "Cargo", Gender = "M", Root = "Men", NodeTitle = "Shorts" }, new SubRoots() { SubRoot = "Chino", Gender = "M", Root = "Men", NodeTitle = "Shorts" }, new SubRoots() { SubRoot = "Denim", Gender = "M", Root = "Men", NodeTitle = "Shorts" }, new SubRoots() { SubRoot = "Hybrids", Gender = "M", Root = "Men", NodeTitle = "Shorts" } };
            List<SubRoots> subRootMSuitsTuxedos = new List<SubRoots>() { };
            if (Global.Storecategory == Constant.VintageStr)
            {
                Global.CatBySubList = new List<SubRoots>() { new SubRoots() { SubRoot = "Cardigans", Gender = "M", Root = "Men", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Cashmere", Gender = "M", Root = "Men", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Hoodie", Gender = "M", Root = "Men", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Turtlenecks", Gender = "M", Root = "Men", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Vest", Gender = "M", Root = "Men", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "V-Neck", Gender = "M", Root = "Men", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Zip-Up", Gender = "M", Root = "Men", NodeTitle = "Sweaters" } };
            }
            else
            {
                Global.CatBySubList = new List<SubRoots>() { new SubRoots() { SubRoot = "Cardigans", Gender = "M", Root = "Men", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Cashmere", Gender = "M", Root = "Men", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Crewnecks", Gender = "M", Root = "Men", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Hoodie", Gender = "M", Root = "Men", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Turtlenecks", Gender = "M", Root = "Men", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Vest", Gender = "M", Root = "Men", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "V-Neck", Gender = "M", Root = "Men", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Zip-Up", Gender = "M", Root = "Men", NodeTitle = "Sweaters" } };
            }
            List<SubRoots> subRootMSweaters = Global.CatBySubList;
            List<SubRoots> subRootMSocksandUnderwear = new List<SubRoots>() { new SubRoots() { SubRoot = "Athletic Socks", Gender = "M", Root = "Men", NodeTitle = "Socks & Underwear" }, new SubRoots() { SubRoot = "Boxer Briefs", Gender = "M", Root = "Men", NodeTitle = "Socks & Underwear" }, new SubRoots() { SubRoot = "Boxers", Gender = "M", Root = "Men", NodeTitle = "Socks & Underwear" }, new SubRoots() { SubRoot = "Briefs", Gender = "M", Root = "Men", NodeTitle = "Socks & Underwear" }, new SubRoots() { SubRoot = "Casual Socks", Gender = "M", Root = "Men", NodeTitle = "Socks & Underwear" }, new SubRoots() { SubRoot = "Dress Socks", Gender = "M", Root = "Men", NodeTitle = "Socks & Underwear" }, new SubRoots() { SubRoot = "Undershirts", Gender = "M", Root = "Men", NodeTitle = "Socks & Underwear" } };
            List<SubRoots> subRootMSwim = new List<SubRoots>() { new SubRoots() { SubRoot = "Board Shorts", Gender = "M", Root = "Men", NodeTitle = "Swim" }, new SubRoots() { SubRoot = "Hybrids", Gender = "M", Root = "Men", NodeTitle = "Swim" }, new SubRoots() { SubRoot = "Rash Guard", Gender = "M", Root = "Men", NodeTitle = "Swim" }, new SubRoots() { SubRoot = "Swim Trunks", Gender = "M", Root = "Men", NodeTitle = "Swim" } };

            List<SubRoots> subRootMOtherClothing = new List<SubRoots>() { };

            //Sub category list of men and women inside main category
            Dictionary<Nodes, List<SubRoots>> nodeW = new Dictionary<Nodes, List<SubRoots>>();
            Dictionary<Nodes, List<SubRoots>> nodeMen = new Dictionary<Nodes, List<SubRoots>>();

            nodeW.Add(new Nodes() { NodeTitle = "Accessories", IsShowMore = true, Root = "Women" }, subRootWAccessories);
            nodeW.Add(new Nodes() { NodeTitle = "Bags", IsShowMore = true, Root = "Women" }, subRootWBages);
            nodeW.Add(new Nodes() { NodeTitle = "Dresses", IsShowMore = true, Root = "Women" }, subRootWDresses);
            nodeW.Add(new Nodes() { NodeTitle = "Intimates & Sleepwear", IsShowMore = true, Root = "Women" }, subRootWIntimatesSleepwear);
            nodeW.Add(new Nodes() { NodeTitle = "Jackets & Coats", IsShowMore = true, Root = "Women" }, subRootWJacketsCoats);
            nodeW.Add(new Nodes() { NodeTitle = "Jeans", IsShowMore = true, Root = "Women" }, subRootWJeans);
            nodeW.Add(new Nodes() { NodeTitle = "Jewelry", IsShowMore = true, Root = "Women" }, subRootWJewelry);
            nodeW.Add(new Nodes() { NodeTitle = "Pants", IsShowMore = true, Root = "Women" }, subRootWPants);
            if (Global.SearchedResultSelectedStore.ToLower() == Constant.ClothingStr.ToLower() || Global.SearchedResultSelectedStore.ToLower() == Constant.VintageStr.ToLower()) 
            {
                nodeW.Add(new Nodes() { NodeTitle = "Shoes", IsShowMore = true, Root = "Women" }, subRootWShoes);
            }
            nodeW.Add(new Nodes() { NodeTitle = "Shorts", IsShowMore = true, Root = "Women" }, subRootWShorts);
            nodeW.Add(new Nodes() { NodeTitle = "Skirts", IsShowMore = true, Root = "Women" }, subRootWSkirts);
            nodeW.Add(new Nodes() { NodeTitle = "Suits & Separates", IsShowMore = false, Root = "Women" }, subRootWSuitsSeparates);
            nodeW.Add(new Nodes() { NodeTitle = "Sweaters", IsShowMore = true, Root = "Women" }, subRootWSweaters);
            nodeW.Add(new Nodes() { NodeTitle = "Swimsuits", IsShowMore = true, Root = "Women" }, subRootWSwimsuits);
            nodeW.Add(new Nodes() { NodeTitle = "Tops", IsShowMore = true, Root = "Women" }, subRootWTops);
            nodeW.Add(new Nodes() { NodeTitle = "Other", IsShowMore = false, Root = "Women" }, subRootWOtherClothing);

            nodeMen.Add(new Nodes() { NodeTitle = "Accessories", IsShowMore = true, Root = "Men" }, subRootMAccessories);
            nodeMen.Add(new Nodes() { NodeTitle = "Bags", IsShowMore = true, Root = "Men" }, subRootMBages);
            nodeMen.Add(new Nodes() { NodeTitle = "Jackets & Coats", IsShowMore = true, Root = "Men" }, subRootMJacketsCoats);
            nodeMen.Add(new Nodes() { NodeTitle = "Jeans", IsShowMore = true, Root = "Men" }, subRootMJeans);
            nodeMen.Add(new Nodes() { NodeTitle = "Pants", IsShowMore = true, Root = "Men" }, subRootMPants);
            nodeMen.Add(new Nodes() { NodeTitle = "Shirts", IsShowMore = true, Root = "Men" }, subRootMShirts);
            if (Global.SearchedResultSelectedStore.ToLower() == Constant.ClothingStr.ToLower() || Global.SearchedResultSelectedStore.ToLower() == Constant.VintageStr.ToLower())
            {
                nodeMen.Add(new Nodes() { NodeTitle = "Shoes", IsShowMore = true, Root = "Men" }, subRootMShoes);
            }
            nodeMen.Add(new Nodes() { NodeTitle = "Shorts", IsShowMore = true, Root = "Men" }, subRootMShorts);
            if (Global.Storecategory == Constant.VintageStr)
            {
                //nodeMen.Add(new Nodes() { IsShowMore = false}, subRootMSocksandUnderwear);
            }
            else
            {
                nodeMen.Add(new Nodes() { NodeTitle = "Socks & Underwear", IsShowMore = true, Root = "Men" }, subRootMSocksandUnderwear);
            }
            nodeMen.Add(new Nodes() { NodeTitle = "Suits & Tuxedos", IsShowMore = false, Root = "Men" }, subRootMSuitsTuxedos);
            nodeMen.Add(new Nodes() { NodeTitle = "Sweaters", IsShowMore = true, Root = "Men" }, subRootMSweaters);
            nodeMen.Add(new Nodes() { NodeTitle = "Swim", IsShowMore = true, Root = "Men" }, subRootMSwim);
            nodeMen.Add(new Nodes() { NodeTitle = "Other", IsShowMore = false, Root = "Men" }, subRootMOtherClothing);

            //Main category list
            roots = new List<Roots>()
            {
                new Roots(){ Root="Women", Node=nodeW},
                new Roots(){ Root="Men", Node=nodeMen}
            };

            Global.RootsList = roots;
            return roots;
        }
        #endregion

        #region GetSneakesCatList method
        //Method created to show Main Categories (Men, Women) list and thier sub category list and to show categories inside sub categories for sneakers
        public List<Roots> GetSneakesCatList()
        {
            List<Roots> roots;
            Dictionary<Nodes, List<SubRoots>> nodeW = new Dictionary<Nodes, List<SubRoots>>();
            Dictionary<Nodes, List<SubRoots>> nodeMen = new Dictionary<Nodes, List<SubRoots>>();

            //sub categories inside women
            #region Sneakers Category list for Women
            List<SubRoots> subRootWBasketball = new List<SubRoots>() { };
            List<SubRoots> subRootWCasual = new List<SubRoots>() { };
            List<SubRoots> subRootWLuxury = new List<SubRoots>() { };
            List<SubRoots> subRootWRunning = new List<SubRoots>() { };
            List<SubRoots> subRootWSoccer = new List<SubRoots>() { };
            List<SubRoots> subRootWSkateboard = new List<SubRoots>() { };
            List<SubRoots> subRootWSlides = new List<SubRoots>() { };
            List<SubRoots> subRootWTennis = new List<SubRoots>() { };
            List<SubRoots> subRootWTrackField = new List<SubRoots>() { };
            List<SubRoots> subRootWTraining = new List<SubRoots>() { };
            List<SubRoots> subRootWWalking = new List<SubRoots>() { };

            nodeW.Add(new Nodes() { NodeTitle = "Basketball", Gender = "F", IsShowMore = false, Root = "Women" }, subRootWBasketball);
            nodeW.Add(new Nodes() { NodeTitle = "Casual", Gender = "F", IsShowMore = false, Root = "Women" }, subRootWCasual);
            nodeW.Add(new Nodes() { NodeTitle = "Luxury", Gender = "F", IsShowMore = false, Root = "Women" }, subRootWLuxury);
            nodeW.Add(new Nodes() { NodeTitle = "Running", Gender = "F", IsShowMore = false, Root = "Women" }, subRootWRunning);
            nodeW.Add(new Nodes() { NodeTitle = "Soccer", Gender = "F", IsShowMore = false, Root = "Women" }, subRootWSoccer);
            nodeW.Add(new Nodes() { NodeTitle = "Skateboard", Gender = "F", IsShowMore = false, Root = "Women" }, subRootWSkateboard);
            nodeW.Add(new Nodes() { NodeTitle = "Slides", Gender = "F", IsShowMore = false, Root = "Women" }, subRootWSlides);
            nodeW.Add(new Nodes() { NodeTitle = "Tennis", Gender = "F", IsShowMore = false, Root = "Women" }, subRootWTennis);
            nodeW.Add(new Nodes() { NodeTitle = "Track & Field", Gender = "F", IsShowMore = false, Root = "Women" }, subRootWTrackField);
            nodeW.Add(new Nodes() { NodeTitle = "Training", Gender = "F", IsShowMore = false, Root = "Women" }, subRootWTraining);
            nodeW.Add(new Nodes() { NodeTitle = "Walking", Gender = "F", IsShowMore = false, Root = "Women" }, subRootWWalking);


            //List<SubRoots> subRootWSneakers = new List<SubRoots>() { new SubRoots() { SubRoot = "Basketball", Gender = "F", Root = "Women", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Casual", Gender = "F", Root = "Women", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Luxury", Gender = "F", Root = "Women", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Running", Gender = "F", Root = "Women", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Soccer", Gender = "F", Root = "Women", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Skateboard", Gender = "F", Root = "Women", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Slides", Gender = "F", Root = "Women", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Tennis", Gender = "F", Root = "Women", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Track & Field", Gender = "F", Root = "Women", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Training", Gender = "F", Root = "Women", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Walking", Gender = "F", Root = "Women", NodeTitle = "Sneakers" } };

            //nodeW.Add(new Nodes() { NodeTitle = "Sneakers", Gender = "F", IsShowMore = true, Root = "Women" }, subRootWSneakers);
            #endregion

            //sub categories inside men
            #region Sneakers Category list for Men

            List<SubRoots> subRootMBasketball = new List<SubRoots>() { };
            List<SubRoots> subRootMCasual = new List<SubRoots>() { };
            List<SubRoots> subRootMLuxury = new List<SubRoots>() { };
            List<SubRoots> subRootMRunning = new List<SubRoots>() { };
            List<SubRoots> subRootMSoccer = new List<SubRoots>() { };
            List<SubRoots> subRootMSkateboard = new List<SubRoots>() { };
            List<SubRoots> subRootMSlides = new List<SubRoots>() { };
            List<SubRoots> subRootMTennis = new List<SubRoots>() { };
            List<SubRoots> subRootMTrackField = new List<SubRoots>() { };
            List<SubRoots> subRootMTraining = new List<SubRoots>() { };
            List<SubRoots> subRootMWalking = new List<SubRoots>() { };

            nodeMen.Add(new Nodes() { NodeTitle = "Basketball", Gender = "M", IsShowMore = false, Root = "Men" }, subRootMBasketball);
            nodeMen.Add(new Nodes() { NodeTitle = "Casual", Gender = "M", IsShowMore = false, Root = "Men" }, subRootMCasual);
            nodeMen.Add(new Nodes() { NodeTitle = "Luxury", Gender = "M", IsShowMore = false, Root = "Men" }, subRootMLuxury);
            nodeMen.Add(new Nodes() { NodeTitle = "Running", Gender = "M", IsShowMore = false, Root = "Men" }, subRootMRunning);
            nodeMen.Add(new Nodes() { NodeTitle = "Soccer", Gender = "M", IsShowMore = false, Root = "Men" }, subRootMSoccer);
            nodeMen.Add(new Nodes() { NodeTitle = "Skateboard", Gender = "M", IsShowMore = false, Root = "Men" }, subRootMSkateboard);
            nodeMen.Add(new Nodes() { NodeTitle = "Slides", Gender = "M", IsShowMore = false, Root = "Men" }, subRootMSlides);
            nodeMen.Add(new Nodes() { NodeTitle = "Tennis", Gender = "M", IsShowMore = false, Root = "Men" }, subRootMTennis);
            nodeMen.Add(new Nodes() { NodeTitle = "Track & Field", Gender = "M", IsShowMore = false, Root = "Men" }, subRootMTrackField);
            nodeMen.Add(new Nodes() { NodeTitle = "Training", Gender = "M", Root = "Men", IsShowMore = false }, subRootMTraining);
            nodeMen.Add(new Nodes() { NodeTitle = "Walking", Gender = "M", Root = "Men", IsShowMore = false }, subRootMWalking);

            //List<SubRoots> subRootMSneakers = new List<SubRoots>() { new SubRoots() { SubRoot = "Basketball", Gender = "M", Root = "Men", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Casual", Gender = "M", Root = "Men", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Luxury", Gender = "M", Root = "Men", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Running", Gender = "M", Root = "Men", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Soccer", Gender = "M", Root = "Men", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Skateboard", Gender = "M", Root = "Men", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Slides", Gender = "M", Root = "Men", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Tennis", Gender = "M", Root = "Men", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Track & Field", Gender = "M", Root = "Men", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Training", Gender = "M", Root = "Men", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Walking", Gender = "M", Root = "Men", NodeTitle = "Sneakers" } };

            //nodeMen.Add(new Nodes() { NodeTitle = "Sneakers", Gender = "M", IsShowMore = true, Root = "Men" }, subRootMSneakers);
            #endregion

            roots = new List<Roots>()
            {
                new Roots(){ Root="Women", Node=nodeW},
                new Roots(){ Root="Men", Node=nodeMen}
            };
            Global.SneakersRootsList = roots;
            return roots;
        }
        #endregion

        #region SeletedCustomSizeMethod Method
        //Method created to show size list as per category selected using custom filters, add listing and edit listing for Clothing, sneakers, and vintage stores
        public ObservableCollection<string> SeletedCustomSizeMethod(SubRoots subRoots)
        {
            ObservableCollection<string> SizeList = new ObservableCollection<string>();
            try
            {
                if (Global.RootsList != null)
                {
                    if (Global.RootsList.Count == 0)
                    {
                        GetCategoryList();
                    }
                }
                string keyNodes = string.Empty;
                string subKeyNodes = string.Empty;
                string gender = "men";
                if (subRoots.SubRoot != null && (subRoots.Gender.ToUpper() != "F" || subRoots.Gender.ToUpper() != "WOMEN"))
                {
                    if (subRoots.Gender.ToUpper() == "F" || subRoots.Gender.ToUpper() == "WOMEN")
                    {
                        gender = "women";
                    }
                    var node = Global.RootsList.Where(x => x.Root.ToLower() == gender.ToLower()).ToList().FirstOrDefault();
                    Dictionary<Nodes, List<SubRoots>> dic = node.Node;
                    foreach (var keyNode in dic)
                    {
                        var objSubRool = keyNode.Value.Where(s => s.SubRoot.ToLower().Trim() == subRoots.SubRoot.ToLower().Trim()).FirstOrDefault();
                        if (objSubRool != null)
                        {
                            keyNodes = keyNode.Key.NodeTitle;
                            subKeyNodes = subRoots.SubRoot;
                            //if (subRoots.Gender.ToUpper() == "M")
                            //{
                            //    if (!string.IsNullOrEmpty(subRoots.SubRoot))
                            //    {
                            //        keyNodes = subKeyNodes;
                            //    }
                            //}
                            break;
                        }
                    }
                }
                else if (subRoots.Root != null && subRoots.NodeTitle != null)
                {
                    subRoots.Gender = subRoots.Root.ToLower() != "men" ? "F" : "M";
                    keyNodes = subRoots.NodeTitle;
                    subKeyNodes = subRoots.SubRoot;
                    //if (subRoots.Gender.ToUpper() == "M")
                    //{
                    //    if (!string.IsNullOrEmpty(subRoots.SubRoot))
                    //    {
                    //        keyNodes = subKeyNodes;
                    //    }
                    //}
                }

                if (keyNodes == string.Empty)
                {
                    keyNodes = subRoots.SubRoot;
                }
                //List of sizes as per store and category selected
                if (subRoots.Gender.ToUpper() == "M" || subRoots.Gender.ToUpper() == "MEN")
                {
                    switch (keyNodes.ToLower().Trim())
                    {
                        case "accessories":
                            SizeList = new ObservableCollection<string>() { "One Size", "Custom" };
                            break;

                        case "bags":
                            SizeList = new ObservableCollection<string>() { "One Size", "Custom" };
                            break;

                        case "jackets & coats":
                            SizeList = new ObservableCollection<string>() { "One Size", "Custom", "XS", "S", "M", "L", "XL", "XXL", "3XL", "34L", "36L", "38L", "40L", "42L", "44L", "46L", "48L", "50L", "52L", "54L", "56L", "34R", "36R", "38R", "40R", "42R", "44R", "46R", "48R", "50R", "52R", "54R", "56R", "34S", "36S", "38S", "40S", "42S", "44S", "46S", "48S", "50S", "52S", "54S", "56S" };
                            break;

                        case "coats":
                            SizeList = new ObservableCollection<string>() { "One Size", "Custom", "XS", "S", "M", "L", "XL", "XXL", "3XL", "34L", "36L", "38L", "40L", "42L", "44L", "46L", "48L", "50L", "52L", "54L", "56L", "34R", "36R", "38R", "40R", "42R", "44R", "46R", "48R", "50R", "52R", "54R", "56R", "34S", "36S", "38S", "40S", "42S", "44S", "46S", "48S", "50S", "52S", "54S", "56S" };
                            break;

                        case "jeans":
                            SizeList = new ObservableCollection<string>() { "One Size", "Custom", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "42", "43", "44" };
                            break;

                        case "pants":
                            SizeList = new ObservableCollection<string>() { "One Size", "Custom", "XS", "S", "M", "L", "XL", "XXL", "3XL", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "42", "43", "44" };
                            break;

                        case "shirts":
                            SizeList = new ObservableCollection<string>() { "One Size", "Custom", "XS", "S", "M", "L", "XL", "XXL", "3XL", "14.5", "15", "15.5", "16", "16.5", "17", "17.5", "18", "18.5", "19", "19.5", "20" };
                            break;

                        case "shoes":
                            SizeList = new ObservableCollection<string>() { "One Size", "Custom", "4", "4.5", "5", "5.5", "6", "6.5", "7", "7.5", "8", "8.5", "9", "9.5", "10", "10.5", "11", "11.5", "12", "12.5", "13", "13.5", "14", "14.5", "15", "15.5", "16" };
                            break;

                        case "shorts":
                            SizeList = new ObservableCollection<string>() { "One Size", "Custom", "XS", "S", "M", "L", "XL", "XXL", "3XL", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "42", "43", "44" };
                            break;

                        case "suits & tuxedos":
                            SizeList = new ObservableCollection<string>() {"One Size", "Custom", "34L", "35L", "36L", "37L", "38L", "39L", "40L", "41L", "42L", "43L", "44L", "45L", "46L", "47L", "48L", "49L", "50L", "51L", "52L", "53L", "54L", "55L", "56L","34R", "35R", "36R", "37R", "38R", "39R", "40R", "41R", "42R", "43R", "44R", "45R", "46R", "47R", "48R", "49R", "50R", "51R", "52R", "53R", "54R", "55R", "56R","34S", "35S", "36S", "37S", "38S", "39S", "40S", "41S", "42S", "43S", "44S", "45S", "46S", "47S", "48S", "49S", "50S", "51S", "52S", "53S", "54S", "55S", "56S" };
                            break;

                        case "suits":
                            SizeList = new ObservableCollection<string>() {"One Size", "Custom", "34L", "35L", "36L", "37L", "38L", "39L", "40L", "41L", "42L", "43L", "44L", "45L", "46L", "47L", "48L", "49L", "50L", "51L", "52L", "53L", "54L", "55L", "56L","34R", "35R", "36R", "37R", "38R", "39R", "40R", "41R", "42R", "43R", "44R", "45R", "46R", "47R", "48R", "49R", "50R", "51R", "52R", "53R", "54R", "55R", "56R","34S", "35S", "36S", "37S", "38S", "39S", "40S", "41S", "42S", "43S", "44S", "45S", "46S", "47S", "48S", "49S", "50S", "51S", "52S", "53S", "54S", "55S", "56S"};
                            break;

                        case "sweaters":
                            SizeList = new ObservableCollection<string>() { "One Size", "Custom", "XS", "S", "M", "L", "XL", "XXL", "3XL" };
                            break;

                        case "swim":
                            SizeList = new ObservableCollection<string>() { "One Size", "Custom", "XS", "S", "M", "L", "XL", "XXL", "3XL" };
                            break;

                        case "socks & underwear":
                            SizeList = new ObservableCollection<string>() { "One Size", "Custom", "XS", "S", "M", "L", "XL", "XXL", "3XL" };
                            break;

                        case "socks":
                            SizeList = new ObservableCollection<string>() { "One Size", "Custom", "XS", "S", "M", "L", "XL", "XXL", "3XL" };
                            break;

                        case "other":
                            SizeList = new ObservableCollection<string>() { "One Size", "Custom" };
                            //var sizeList = new ObservableCollection<string>() { "One Size", "Custom", "XS", "S", "M", "L", "XL", "XXL", "3XL",  "XS", "S", "M", "L", "XL", "XXL", "3XL", "Custom", "XS", "S", "M", "L", "XL", "XXL", "3XL", "Custom", "34S", "34R", "34L", "35S", "35R", "35L", "36S", "36R", "36L", "37S", "37R", "37L", "38S", "38R", "38L", "39S", "39R", "39L", "40S", "40R", "40L", "41S", "41R", "41L", "42S", "42R", "42L", "43S", "43R", "43L", "44S", "44R", "44L", "45S", "45R", "45L", "46S", "46R", "46L", "47S", "47R", "47L", "48S", "48R", "48L", "49S", "49R", "49L", "50S", "50R", "50L", "51S", "51R", "51L", "52S", "52R", "52L", "53S", "53R", "53L", "54S", "54R", "54L", "55S", "55R", "55L", "56S", "56R", "56L", "Custom", "XS", "S", "M", "L", "XL", "XXL", "3XL", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "42", "43", "44", "Custom", "4", "4.5", "5", "5.5", "6", "6.5", "7", "7.5", "8", "8.5", "9", "9.5", "10", "10.5", "11", "11.5", "12", "12.5", "13", "13.5", "14", "14.5", "15", "15.5", "16", "Custom", "XS", "S", "M", "L", "XL", "XXL", "3XL", "14.5", "15", "15.5", "16", "16.5", "17", "17.5", "18", "18.5", "19", "19.5", "20", "Custom", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "42", "43", "44", "Custom", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "42", "43", "44", "Custom", "XS", "S", "M", "L", "XL", "XXL", "3XL", "Custom", "One Size", "Custom", "XS", "S", "M", "L", "XL", "XXL", "3XL", "One Size", "Custom", "XS", "S", "M", "L", "XL", "XXL", "3XL", "One Size", "Custom", "XS", "S", "M", "L", "XL", "XXL", "3XL", "One Size", "Custom", "XS", "S", "M", "L", "XL", "XXL", "3XL", "One Size", "Custom", "One Size", "Custom", "XS", "S", "M", "L", "XL", "XXL", "3XL", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44" };
                            //SizeList = new ObservableCollection<string>(sizeList.Distinct().ToList().OrderBy(x => x));
                            break;

                        //EXTRA Category Sizes Added of wo  men
                        case "dresses":
                            SizeList = new ObservableCollection<string>() { "One Size", "Custom", "XXS", "XS", "S", "M", "L", "XL", "XXL", "XXXL", "0X", "1X", "2X", "3X", "4X", "5X", "00", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "14W", "15", "16", "16W", "17", "18", "18W", "20", "20W", "22", "22W", "24", "24W", "26", "26W", "28", "28W", "30", "30W", "32", "32W", "00P", "0P", "2P", "4P", "6P", "8P", "10P", "12P", "14P", "16P", "18P", "20P", "XXSP", "XSP", "SP", "MP", "LP", "XLP", "XXLP" };
                            break;

                        case "intimates & sleepwear":
                            SizeList = new ObservableCollection<string>() { "One Size", "Custom", "XXS", "XS", "S", "M", "L", "XL", "30A", "32A", "34A", "36A", "38A", "40A", "42A", "44A", "46A", "48A","32AA", "34AA", "36AA", "38AA", "40AA", "42AA", "44AA", "46AA", "48AA", "30B", "32B", "34B", "36B", "38B", "40B", "42B", "44B", "46B", "48B", "32C", "34C", "36C", "38C", "40C", "42C", "44C", "46C", "48C", "32D", "34D", "36D", "38D", "40D", "42D", "44D", "46D", "48D", "32E(DD)", "34E(DD)", "36E(DD)", "38E(DD)", "40E(DD)", "42E(DD)", "44E(DD)", "46E(DD)", "48E(DD)", "32F(DDD)" , "34F(DDD)", "36F(DDD)", "38F(DDD)", "40F(DDD)", "42F(DDD)", "44F(DDD)", "46F(DDD)", "48F(DDD)", "32G(4D)" , "34G(4D)", "36G(4D)", "38G(4D)", "40G(4D)", "42G(4D)", "44G(4D)", "46G(4D)", "48G(4D)", "32H(5D)" , "34H(5D)", "36H(5D)", "38H(5D)", "40H(5D)", "42H(5D)","44H(5D)", "46H(5D)", "48H(5D)"};
                            break;

                        case "jewelry":
                            SizeList = new ObservableCollection<string>() { "One Size", "Custom" };
                            break;

                        case "skirts":
                            SizeList = new ObservableCollection<string>() { "One Size", "Custom", "XXS", "XS", "S", "M", "L", "XL", "XXL", "XXXL", "0X", "1X", "2X", "3X", "4X", "5X", "00", "0", "2", "4", "6", "8", "10", "12", "14", "14W", "16", "16W", "18", "18W", "20", "20W", "22", "22W", "23", "24", "24W", "25", "26", "26W", "27", "28", "28W", "29", "30", "30W", "31", "32", "32W", "33", "34", "00P", "0P", "2P", "4P", "6P", "8P", "10P", "12P", "14P", "16P", "18P", "20P", "23P", "24P", "25P", "26P", "27P", "28P", "29P", "30P", "31P", "32P", "33P", "34P", "XXSP", "XSP", "SP", "MP", "LP", "XLP", "XXLP" };
                            break;

                        case "suits & separates":
                            SizeList = new ObservableCollection<string>() { "One Size", "Custom", "XXS", "XS", "S", "M", "L", "XL", "XXL", "XXXL", "0X", "1X", "2X", "3X", "4X", "5X", "00", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "14W", "15", "16", "16W", "17", "18", "18W", "20", "20W", "22", "22W", "24", "24W", "26", "26W", "28", "28W", "30", "30W", "32", "32W", "00P", "0P", "2P", "4P", "6P", "8P", "10P", "12P", "14P", "16P", "18P", "20P", "XXSP", "XSP", "SP", "MP", "LP", "XLP", "XXLP" };
                            break;

                        case "swimsuits":
                            SizeList = new ObservableCollection<string>() { "One Size", "Custom", "XXS", "XS", "S", "M", "L", "XL", "XXL", "XXXL", "0X", "1X", "2X", "3X", "4X", "5X", "00", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "14W", "15", "16", "16W", "17", "18", "18W", "20", "20W", "22", "22W", "24", "24W", "26", "26W", "28", "28W", "30", "30W", "32", "32W", "00P", "0P", "2P", "4P", "6P", "8P", "10P", "12P", "14P", "16P", "18P", "20P", "XXSP", "XSP", "SP", "MP", "LP", "XLP", "XXLP" };
                            break;

                        case "tops":
                            SizeList = new ObservableCollection<string>() { "One Size", "Custom", "XXS", "XS", "S", "M", "L", "XL", "XXL", "XXXL", "0X", "1X", "2X", "3X", "4X", "5X", "00", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "14W", "15", "16", "16W", "17", "18", "18W", "20", "20W", "22", "22W", "24", "24W", "26", "26W", "28", "28W", "30", "30W", "32", "32W", "00P", "0P", "2P", "4P", "6P", "8P", "10P", "12P", "14P", "16P", "18P", "20P", "XXSP", "XSP", "SP", "MP", "LP", "XLP", "XXLP" };
                            break;

                        case "bottoms":
                            var sizeList = new ObservableCollection<string>() { "One Size", "Custom", "XXS", "XS", "S", "M", "L", "XL", "XXL", "XXXL", "0X", "1X", "2X", "3X", "4X", "5X", "00", "0", "2", "4", "6", "8", "10", "12", "14", "14W", "16", "16W", "18", "18W", "20", "20W", "22", "22W", "23", "24", "24W", "25", "26", "26W", "27", "28", "28W", "29", "30", "30W", "31", "32", "32W", "33", "34", "00P", "0P", "2P", "4P", "6P", "8P", "10P", "12P", "14P", "16P", "18P", "20P", "23P", "24P", "25P", "26P", "27P", "28P", "29P", "30P", "31P", "32P", "33P", "34P", "XXSP", "XSP", "SP", "MP", "LP", "XLP", "XXLP", "XXS", "XS", "S", "M", "L", "XL", "XXL", "XXXL", "0X", "1X", "2X", "3X", "4X", "5X", "00", "0", "2", "4", "6", "8", "10", "12", "14", "14W", "16", "16W", "18", "18W", "20", "20W", "22", "22W", "23", "24", "24W", "25", "26", "26W", "27", "28", "28W", "29", "30", "30W", "31", "32", "32W", "33", "34", "00P", "0P", "2P", "4P", "6P", "8P", "10P", "12P", "14P", "16P", "18P", "20P", "23P", "24P", "25P", "26P", "27P", "28P", "29P", "30P", "31P", "32P", "33P", "34P", "XXSP", "XSP", "SP", "MP", "LP", "XLP", "XXLP", "XXS", "XS", "S", "M", "L", "XL", "XXL", "XXXL", "0X", "1X", "2X", "3X", "4X", "5X", "00", "0", "2", "4", "6", "8", "10", "12", "14", "14W", "16", "16W", "18", "18W", "20", "20W", "22", "22W", "23", "24", "24W", "25", "26", "26W", "27", "28", "28W", "29", "30", "30W", "31", "32", "32W", "33", "34", "00P", "0P", "2P", "4P", "6P", "8P", "10P", "12P", "14P", "16P", "18P", "20P", "23P", "24P", "25P", "26P", "27P", "28P", "29P", "30P", "31P", "32P", "33P", "34P", "XXSP", "XSP", "SP", "MP", "LP", "XLP", "XXLP", "XXS", "XS", "S", "M", "L", "XL", "XXL", "XXXL", "0X", "1X", "2X", "3X", "4X", "5X", "00", "0", "2", "4", "6", "8", "10", "12", "14", "14W", "16", "16W", "18", "18W", "20", "20W", "22", "22W", "23", "24", "24W", "25", "26", "26W", "27", "28", "28W", "29", "30", "30W", "31", "32", "32W", "33", "34", "00P", "0P", "2P", "4P", "6P", "8P", "10P", "12P", "14P", "16P", "18P", "20P", "23P", "24P", "25P", "26P", "27P", "28P", "29P", "30P", "31P", "32P", "33P", "34P", "XXSP", "XSP", "SP", "MP", "LP", "XLP", "XXLP" };
                            SizeList = new ObservableCollection<string>(sizeList.Distinct().ToList());
                            break;


                    }
                    //Show sizes for speciific sub category
                    if (subKeyNodes != null)
                    {
                        switch (subKeyNodes.ToLower().Trim())
                        {
                            case "belts":
                                SizeList = new ObservableCollection<string>() { "One Size", "Custom", "XS", "S", "M", "L", "XL", "XXL", "3XL", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44" };
                                break;

                            case "hats":
                                SizeList = new ObservableCollection<string>() { "One Size", "Custom", "XS", "S", "M", "L", "XL", "XXL", "3XL" };
                                break;

                            case "face masks":
                                SizeList = new ObservableCollection<string>() { "One Size", "Custom", "XS", "S", "M", "L", "XL", "XXL", "3XL" };
                                break;

                            case "gloves & scarves":
                                SizeList = new ObservableCollection<string>() { "One Size", "Custom", "XS", "S", "M", "L", "XL", "XXL", "3XL" };
                                break;

                            case "pajamas & robes":
                                SizeList = new ObservableCollection<string>() { "One Size", "Custom", "XS", "S", "M", "L", "XL", "XXL", "3XL" };
                                break;
                        }
                    }
                }
                //Show sizes inside women's category
                if (subRoots.Gender.ToUpper() == "F" || subRoots.Gender.ToUpper() == "WOMEN")
                {
                    switch (keyNodes.ToLower().Trim())
                    {
                        case "accessories":
                            SizeList = new ObservableCollection<string>() { "One Size", "Custom", "XXS", "XS", "S", "M", "L", "XL", "XXL", "0X", "1X", "2X", "3X", "4X", "5X" };
                            break;

                        case "bags":
                            SizeList = new ObservableCollection<string>() { "One Size", "Custom" };
                            break;

                        case "dresses":
                            SizeList = new ObservableCollection<string>() { "One Size", "Custom", "XXS", "XS", "S", "M", "L", "XL", "XXL", "XXXL", "0X", "1X", "2X", "3X", "4X", "5X", "00", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "14W", "15", "16", "16W", "17", "18", "18W", "20", "20W", "22", "22W", "24", "24W", "26", "26W", "28", "28W", "30", "30W", "32", "32W", "00P", "0P", "2P", "4P", "6P", "8P", "10P", "12P", "14P", "16P", "18P", "20P", "XXSP", "XSP", "SP", "MP", "LP", "XLP", "XXLP" };
                            break;

                        case "intimates & sleepwear":
                            SizeList = new ObservableCollection<string>() { "One Size", "Custom", "XXS", "XS", "S", "M", "L", "XL", "30A", "32A", "34A", "36A", "38A", "40A", "42A", "44A", "46A", "48A", "32AA", "34AA", "36AA", "38AA", "40AA", "42AA", "44AA", "46AA", "48AA", "30B", "32B", "34B", "36B", "38B", "40B", "42B", "44B", "46B", "48B", "32C", "34C", "36C", "38C", "40C", "42C", "44C", "46C", "48C", "32D", "34D", "36D", "38D", "40D", "42D", "44D", "46D", "48D", "32E(DD)", "34E(DD)", "36E(DD)", "38E(DD)", "40E(DD)", "42E(DD)", "44E(DD)", "46E(DD)", "48E(DD)", "32F(DDD)", "34F(DDD)", "36F(DDD)", "38F(DDD)", "40F(DDD)", "42F(DDD)", "44F(DDD)", "46F(DDD)", "48F(DDD)", "32G(4D)", "34G(4D)", "36G(4D)", "38G(4D)", "40G(4D)", "42G(4D)", "44G(4D)", "46G(4D)", "48G(4D)", "32H(5D)", "34H(5D)", "36H(5D)", "38H(5D)", "40H(5D)", "42H(5D)", "44H(5D)", "46H(5D)", "48H(5D)" };
                            break;

                        case "jackets & coats":
                            SizeList = new ObservableCollection<string>() { "One Size", "Custom", "XXS", "XS", "S", "M", "L", "XL", "XXL", "XXXL", "0X", "1X", "2X", "3X", "4X", "5X", "00", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "14W", "15", "16", "16W", "17", "18", "18W", "20", "20W", "22", "22W", "24", "24W", "26", "26W", "28", "28W", "30", "30W", "32", "32W", "00P", "0P", "2P", "4P", "6P", "8P", "10P", "12P", "14P", "16P", "18P", "20P", "XXSP", "XSP", "SP", "MP", "LP", "XLP", "XXLP" };
                            break;

                        case "coats":
                            SizeList = new ObservableCollection<string>() { "One Size", "Custom", "XXS", "XS", "S", "M", "L", "XL", "XXL", "XXXL", "0X", "1X", "2X", "3X", "4X", "5X", "00", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "14W", "15", "16", "16W", "17", "18", "18W", "20", "20W", "22", "22W", "24", "24W", "26", "26W", "28", "28W", "30", "30W", "32", "32W", "00P", "0P", "2P", "4P", "6P", "8P", "10P", "12P", "14P", "16P", "18P", "20P", "XXSP", "XSP", "SP", "MP", "LP", "XLP", "XXLP" };
                            break;

                        case "jeans":
                            SizeList = new ObservableCollection<string>() { "One Size", "Custom", "XXS", "XS", "S", "M", "L", "XL", "XXL", "XXXL", "0X", "1X", "2X", "3X", "4X", "5X", "00", "0", "2", "4", "6", "8", "10", "12", "14", "14W", "16", "16W", "18", "18W", "20", "20W", "22", "22W", "23", "24", "24W", "25", "26", "26W", "27", "28", "28W", "29", "30", "30W", "31", "32", "32W", "33", "34", "00P", "0P", "2P", "4P", "6P", "8P", "10P", "12P", "14P", "16P", "18P", "20P", "23P", "24P", "25P", "26P", "27P", "28P", "29P", "30P", "31P", "32P", "33P", "34P", "XXSP", "XSP", "SP", "MP", "LP", "XLP", "XXLP" };
                            break;

                        case "jewelry":
                            SizeList = new ObservableCollection<string>() { "One Size", "Custom" };
                            break;

                        case "pants":
                            SizeList = new ObservableCollection<string>() { "One Size", "Custom", "XXS", "XS", "S", "M", "L", "XL", "XXL", "XXXL", "0X", "1X", "2X", "3X", "4X", "5X", "00", "0", "2", "4", "6", "8", "10", "12", "14", "14W", "16", "16W", "18", "18W", "20", "20W", "22", "22W", "23", "24", "24W", "25", "26", "26W", "27", "28", "28W", "29", "30", "30W", "31", "32", "32W", "33", "34", "00P", "0P", "2P", "4P", "6P", "8P", "10P", "12P", "14P", "16P", "18P", "20P", "23P", "24P", "25P", "26P", "27P", "28P", "29P", "30P", "31P", "32P", "33P", "34P", "XXSP", "XSP", "SP", "MP", "LP", "XLP", "XXLP" };
                            break;

                        case "shoes":
                            SizeList = new ObservableCollection<string>() { "One Size", "Custom", "5", "5.5", "6", "6.5", "7", "7.5", "8", "8.5", "9", "9.5", "10", "10.5", "11", "11.5", "12", "12.5", "13" };
                            break;

                        case "shorts":
                            SizeList = new ObservableCollection<string>() { "One Size", "Custom", "XXS", "XS", "S", "M", "L", "XL", "XXL", "XXXL", "0X", "1X", "2X", "3X", "4X", "5X", "00", "0", "2", "4", "6", "8", "10", "12", "14", "14W", "16", "16W", "18", "18W", "20", "20W", "22", "22W", "23", "24", "24W", "25", "26", "26W", "27", "28", "28W", "29", "30", "30W", "31", "32", "32W", "33", "34", "00P", "0P", "2P", "4P", "6P", "8P", "10P", "12P", "14P", "16P", "18P", "20P", "23P", "24P", "25P", "26P", "27P", "28P", "29P", "30P", "31P", "32P", "33P", "34P", "XXSP", "XSP", "SP", "MP", "LP", "XLP", "XXLP" };
                            break;

                        case "skirts":
                            SizeList = new ObservableCollection<string>() { "One Size", "Custom", "XXS", "XS", "S", "M", "L", "XL", "XXL", "XXXL", "0X", "1X", "2X", "3X", "4X", "5X", "00", "0", "2", "4", "6", "8", "10", "12", "14", "14W", "16", "16W", "18", "18W", "20", "20W", "22", "22W", "23", "24", "24W", "25", "26", "26W", "27", "28", "28W", "29", "30", "30W", "31", "32", "32W", "33", "34", "00P", "0P", "2P", "4P", "6P", "8P", "10P", "12P", "14P", "16P", "18P", "20P", "23P", "24P", "25P", "26P", "27P", "28P", "29P", "30P", "31P", "32P", "33P", "34P", "XXSP", "XSP", "SP", "MP", "LP", "XLP", "XXLP" };
                            break;

                        case "suits & separates":
                            SizeList = new ObservableCollection<string>() { "One Size", "Custom", "XXS", "XS", "S", "M", "L", "XL", "XXL", "XXXL", "0X", "1X", "2X", "3X", "4X", "5X", "00", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "14W", "15", "16", "16W", "17", "18", "18W", "20", "20W", "22", "22W", "24", "24W", "26", "26W", "28", "28W", "30", "30W", "32", "32W", "00P", "0P", "2P", "4P", "6P", "8P", "10P", "12P", "14P", "16P", "18P", "20P", "XXSP", "XSP", "SP", "MP", "LP", "XLP", "XXLP" };
                            break;

                        case "suits":
                            SizeList = new ObservableCollection<string>() { "One Size", "Custom", "XXS", "XS", "S", "M", "L", "XL", "XXL", "XXXL", "0X", "1X", "2X", "3X", "4X", "5X", "00", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "14W", "15", "16", "16W", "17", "18", "18W", "20", "20W", "22", "22W", "24", "24W", "26", "26W", "28", "28W", "30", "30W", "32", "32W", "00P", "0P", "2P", "4P", "6P", "8P", "10P", "12P", "14P", "16P", "18P", "20P", "XXSP", "XSP", "SP", "MP", "LP", "XLP", "XXLP" };
                            break;

                        case "sweaters":
                            SizeList = new ObservableCollection<string>() { "One Size", "Custom", "XXS", "XS", "S", "M", "L", "XL", "XXL", "XXXL", "0X", "1X", "2X", "3X", "4X", "5X", "00", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "14W", "15", "16", "16W", "17", "18", "18W", "20", "20W", "22", "22W", "24", "24W", "26", "26W", "28", "28W", "30", "30W", "32", "32W", "00P", "0P", "2P", "4P", "6P", "8P", "10P", "12P", "14P", "16P", "18P", "20P", "XXSP", "XSP", "SP", "MP", "LP", "XLP", "XXLP" };
                            break;

                        case "swimsuits":
                            SizeList = new ObservableCollection<string>() { "One Size", "Custom", "XXS", "XS", "S", "M", "L", "XL", "XXL", "XXXL", "0X", "1X", "2X", "3X", "4X", "5X", "00", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "14W", "15", "16", "16W", "17", "18", "18W", "20", "20W", "22", "22W", "24", "24W", "26", "26W", "28", "28W", "30", "30W", "32", "32W", "00P", "0P", "2P", "4P", "6P", "8P", "10P", "12P", "14P", "16P", "18P", "20P", "XXSP", "XSP", "SP", "MP", "LP", "XLP", "XXLP" };
                            break;

                        case "swim":
                            SizeList = new ObservableCollection<string>() { "One Size", "Custom", "XXS", "XS", "S", "M", "L", "XL", "XXL", "XXXL", "0X", "1X", "2X", "3X", "4X", "5X", "00", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "14W", "15", "16", "16W", "17", "18", "18W", "20", "20W", "22", "22W", "24", "24W", "26", "26W", "28", "28W", "30", "30W", "32", "32W", "00P", "0P", "2P", "4P", "6P", "8P", "10P", "12P", "14P", "16P", "18P", "20P", "XXSP", "XSP", "SP", "MP", "LP", "XLP", "XXLP" };
                            break;

                        case "tops":
                            SizeList = new ObservableCollection<string>() { "One Size", "Custom", "XXS", "XS", "S", "M", "L", "XL", "XXL", "XXXL", "0X", "1X", "2X", "3X", "4X", "5X", "00", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "14W", "15", "16", "16W", "17", "18", "18W", "20", "20W", "22", "22W", "24", "24W", "26", "26W", "28", "28W", "30", "30W", "32", "32W", "00P", "0P", "2P", "4P", "6P", "8P", "10P", "12P", "14P", "16P", "18P", "20P", "XXSP", "XSP", "SP", "MP", "LP", "XLP", "XXLP" };
                            break;

                        case "bottoms":
                            var sizeList = new ObservableCollection<string>() { "One Size", "Custom", "XXS", "XS", "S", "M", "L", "XL", "XXL", "XXXL", "0X", "1X", "2X", "3X", "4X", "5X", "00", "0", "2", "4", "6", "8", "10", "12", "14", "14W", "16", "16W", "18", "18W", "20", "20W", "22", "22W", "23", "24", "24W", "25", "26", "26W", "27", "28", "28W", "29", "30", "30W", "31", "32", "32W", "33", "34", "00P", "0P", "2P", "4P", "6P", "8P", "10P", "12P", "14P", "16P", "18P", "20P", "23P", "24P", "25P", "26P", "27P", "28P", "29P", "30P", "31P", "32P", "33P", "34P", "XXSP", "XSP", "SP", "MP", "LP", "XLP", "XXLP", "XXS", "XS", "S", "M", "L", "XL", "XXL", "XXXL", "0X", "1X", "2X", "3X", "4X", "5X", "00", "0", "2", "4", "6", "8", "10", "12", "14", "14W", "16", "16W", "18", "18W", "20", "20W", "22", "22W", "23", "24", "24W", "25", "26", "26W", "27", "28", "28W", "29", "30", "30W", "31", "32", "32W", "33", "34", "00P", "0P", "2P", "4P", "6P", "8P", "10P", "12P", "14P", "16P", "18P", "20P", "23P", "24P", "25P", "26P", "27P", "28P", "29P", "30P", "31P", "32P", "33P", "34P", "XXSP", "XSP", "SP", "MP", "LP", "XLP", "XXLP", "XXS", "XS", "S", "M", "L", "XL", "XXL", "XXXL", "0X", "1X", "2X", "3X", "4X", "5X", "00", "0", "2", "4", "6", "8", "10", "12", "14", "14W", "16", "16W", "18", "18W", "20", "20W", "22", "22W", "23", "24", "24W", "25", "26", "26W", "27", "28", "28W", "29", "30", "30W", "31", "32", "32W", "33", "34", "00P", "0P", "2P", "4P", "6P", "8P", "10P", "12P", "14P", "16P", "18P", "20P", "23P", "24P", "25P", "26P", "27P", "28P", "29P", "30P", "31P", "32P", "33P", "34P", "XXSP", "XSP", "SP", "MP", "LP", "XLP", "XXLP", "XXS", "XS", "S", "M", "L", "XL", "XXL", "XXXL", "0X", "1X", "2X", "3X", "4X", "5X", "00", "0", "2", "4", "6", "8", "10", "12", "14", "14W", "16", "16W", "18", "18W", "20", "20W", "22", "22W", "23", "24", "24W", "25", "26", "26W", "27", "28", "28W", "29", "30", "30W", "31", "32", "32W", "33", "34", "00P", "0P", "2P", "4P", "6P", "8P", "10P", "12P", "14P", "16P", "18P", "20P", "23P", "24P", "25P", "26P", "27P", "28P", "29P", "30P", "31P", "32P", "33P", "34P", "XXSP", "XSP", "SP", "MP", "LP", "XLP", "XXLP" };
                            SizeList = new ObservableCollection<string>(sizeList.Distinct().ToList());
                            break;

                        case "other":
                            SizeList = new ObservableCollection<string>() { "One Size", "Custom" };
                           
                            //var sizeList = new ObservableCollection<string>() { "One Size", "Custom", "5", "5.5", "6", "6.5", "7", "7.5", "8", "8.5", "9", "9.5", "10", "10.5", "11", "11.5", "12", "12.5", "13", "Custom", "00", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "15", "17", "XXS", "XS", "S", "M", "L", "XL", "14", "14W", "16", "16W", "18", "18W", "20", "20W", "22", "22W", "24", "24W", "26", "26W", "28", "28W", "30", "30W", "32", "32W", "XXL", "XXXL",  "1X", "2X", "3X", "4X", "5X", "00P", "0P", "2P", "4P", "6P", "8P", "10P", "12P", "14P", "16P", "18P", "20P", "XXSP", "XSP", "SP", "MP", "LP", "XLP", "XXLP", "00", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "15", "17", "XXS", "XS", "S", "M", "L", "XL", "14", "14W", "16", "16W", "18", "18W", "20", "20W", "22", "22W", "24", "24W", "26", "26W", "28", "28W", "30", "30W", "32", "32W", "XXL", "XXXL", "1X", "2X", "3X", "4X", "5X", "00P", "0P", "2P", "4P", "6P", "8P", "10P", "12P", "14P", "16P", "18P", "20P", "XXSP", "XSP", "SP", "MP", "LP", "XLP", "XXLP", "00", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "15", "17", "XXS", "XS", "S", "M", "L", "XL", "14", "14W", "16", "16W", "18", "18W", "20", "20W", "22", "22W", "24", "24W", "26", "26W", "28", "28W", "30", "30W", "32", "32W", "XXL", "XXXL", "0XL", "1X", "2X", "3X", "4X", "5X", "00P", "0P", "2P", "4P", "6P", "8P", "10P", "12P", "14P", "16P", "18P", "20P", "XXSP", "XSP", "SP", "MP", "LP", "XLP", "XXLP", "00", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "15", "17", "XXS", "XS", "S", "M", "L", "XL", "14", "14W", "16", "16W", "18", "18W", "20", "20W", "22", "22W", "24", "24W", "26", "26W", "28", "28W", "30", "30W", "32", "32W", "XXL", "XXXL", "0XL", "1X", "2X", "3X", "4X", "5X", "00P", "0P", "2P", "4P", "6P", "8P", "10P", "12P", "14P", "16P", "18P", "20P", "XXSP", "XSP", "SP", "MP", "LP", "XLP", "XXLP", "XXS", "XS", "S", "M", "L", "XL", "XXL", "0X", "1X", "2X", "3X", "4X", "5X", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "00", "0", "2", "4", "6", "8", "10", "12", "14", "14W", "16", "16W", "18", "18W", "20", "20W", "22", "22W", "24", "24W", "26", "26W", "28", "28W", "30", "30W", "32", "32W", "XXL", "XXXL", "0XL", "1X", "2X", "3X", "4X", "5X", "23P", "24P", "25P", "26P", "27P", "28P", "29P", "30P", "31P", "32P", "33P", "34P", "00P", "0P", "2P", "4P", "6P", "8P", "10P", "12P", "14P", "16P", "18P", "20P", "XXSP", "XSP", "SP", "MP", "LP", "XLP", "XXLP", "XXS", "XS", "S", "M", "L", "XL", "XXL", "0X", "1X", "2X", "3X", "4X", "5X", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "00", "0", "2", "4", "6", "8", "10", "12", "14", "14W", "16", "16W", "18", "18W", "20", "20W", "22", "22W", "24", "24W", "26", "26W", "28", "28W", "30", "30W", "32", "32W", "XXL", "XXXL", "0XL", "1X", "2X", "3X", "4X", "5X", "23P", "24P", "25P", "26P", "27P", "28P", "29P", "30P", "31P", "32P", "33P", "34P", "00P", "0P", "2P", "4P", "6P", "8P", "10P", "12P", "14P", "16P", "18P", "20P", "XXSP", "XSP", "SP", "MP", "LP", "XLP", "XXLP", "XXS", "XS", "S", "M", "L", "XL", "XXL", "0X", "1X", "2X", "3X", "4X", "5X", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "00", "0", "2", "4", "6", "8", "10", "12", "14", "14W", "16", "16W", "18", "18W", "20", "20W", "22", "22W", "24", "24W", "26", "26W", "28", "28W", "30", "30W", "32", "32W", "XXL", "XXXL", "0XL", "1X", "2X", "3X", "4X", "5X", "23P", "24P", "25P", "26P", "27P", "28P", "29P", "30P", "31P", "32P", "33P", "34P", "00P", "0P", "2P", "4P", "6P", "8P", "10P", "12P", "14P", "16P", "18P", "20P", "XXSP", "XSP", "SP", "MP", "LP", "XLP", "XXLP", "XXS", "XS", "S", "M", "L", "XL", "XXL", "0X", "1X", "2X", "3X", "4X", "5X", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "00", "0", "2", "4", "6", "8", "10", "12", "14", "14W", "16", "16W", "18", "18W", "20", "20W", "22", "22W", "24", "24W", "26", "26W", "28", "28W", "30", "30W", "32", "32W", "XXL", "XXXL", "0XL", "1X", "2X", "3X", "4X", "5X", "23P", "24P", "25P", "26P", "27P", "28P", "29P", "30P", "31P", "32P", "33P", "34P", "00P", "0P", "2P", "4P", "6P", "8P", "10P", "12P", "14P", "16P", "18P", "20P", "XXSP", "XSP", "SP", "MP", "LP", "XLP", "XXLP", "XXS", "XS", "S", "M", "L", "XL", "XXL", "0X", "1X", "2X", "3X", "4X", "5X", "XXS", "XS", "S", "M", "L", "XL", "XXL", "0X", "1X", "2X", "3X", "4X", "5X", "00", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "15", "17", "XXS", "XS", "S", "M", "L", "XL", "14", "14W", "16", "16W", "18", "18W", "20", "20W", "22", "22W", "24", "24W", "26", "26W", "28", "28W", "30", "30W", "32", "32W", "XXL", "XXXL", "0XL", "1X", "2X", "3X", "4X", "5X", "00P", "0P", "2P", "4P", "6P", "8P", "10P", "12P", "14P", "16P", "18P", "20P", "XXSP", "XSP", "SP", "MP", "LP", "XLP", "XXLP", "XXS", "XS", "S", "M", "L", "XL", "30A", "30B", "32AA", "32A", "32B", "32C", "32D", "32E(DD)", "32F(DDD)", "32G(4D)", "32H(5D)", "34AA", "34A", "34B", "34C", "34D", "34E(DD)", "34F(DDD)", "34G(4D)", "34H(5D)", "36AA", "36A", "36B", "36C", "36D", "36E(DD)", "36F(DDD)", "36G(4D)", "36H(5D)", "38AA", "38A", "38B", "38C", "38D", "38E(DD)", "38F(DDD)", "38G(4D)", "38H(5D)", "40AA", "40A", "40B", "40C", "40D", "40E(DD)", "40F(DDD)", "40G(4D)", "40H(5D)", "42AA", "42A", "42B", "42C", "42D", "42E(DD)", "42F(DDD)", "42G(4D)", "42H(5D)", "44AA", "44A", "44B", "44C", "44D", "44E(DD)", "44F(DDD)", "44G(4D)", "44H(5D)", "46AA", "46A", "46B", "46C", "46D", "46E(DD)", "46F(DDD)", "46G(4D)", "46H(5D)", "48AA", "48A", "48B", "48C", "48D", "48E(DD)", "48F(DDD)", "48G(4D)", "48H(5D)", "00", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "15", "17", "XXS", "XS", "S", "M", "L", "XL", "14", "14W", "16", "16W", "18", "18W", "20", "20W", "22", "22W", "24", "24W", "26", "26W", "28", "28W", "30", "30W", "32", "32W", "XXL", "XXXL", "0XL", "1X", "2X", "3X", "4X", "5X", "00P", "0P", "2P", "4P", "6P", "8P", "10P", "12P", "14P", "16P", "18P", "20P", "XXSP", "XSP", "SP", "MP", "LP", "XLP", "XXLP" };
                            //SizeList = new ObservableCollection<string>(sizeList.Distinct().ToList().OrderBy(x => x));
                            break;

                            //Extra Category sizes added of men

                        case "shirts":
                            SizeList = new ObservableCollection<string>() { "One Size", "Custom", "XS", "S", "M", "L", "XL", "XXL", "3XL", "14.5", "15", "15.5", "16", "16.5", "17", "17.5", "18", "18.5", "19", "19.5", "20" };
                            break;

                        case "suits & tuxedos":
                            SizeList = new ObservableCollection<string>() { "One Size", "Custom", "34L", "35L", "36L", "37L", "38L", "39L", "40L", "41L", "42L", "43L", "44L", "45L", "46L", "47L", "48L", "49L", "50L", "51L", "52L", "53L", "54L", "55L", "56L", "34R", "35R", "36R", "37R", "38R", "39R", "40R", "41R", "42R", "43R", "44R", "45R", "46R", "47R", "48R", "49R", "50R", "51R", "52R", "53R", "54R", "55R", "56R", "34S", "35S", "36S", "37S", "38S", "39S", "40S", "41S", "42S", "43S", "44S", "45S", "46S", "47S", "48S", "49S", "50S", "51S", "52S", "53S", "54S", "55S", "56S" };
                            break;

                        case "socks & underwear":
                            SizeList = new ObservableCollection<string>() { "One Size", "Custom", "XS", "S", "M", "L", "XL", "XXL", "3XL" };
                            break;

                    }
                }
                //SizeList = new ObservableCollection<string>() { "One Size", "Custom", "ALL WOMENS Sizes" };
                return SizeList;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return SizeList;
        }
        #endregion

        //Method created to show size list as per category selected using custom filters, add listing and edit listing for Sneakers stores (Men and Women)
        #region SneakersSeletedCustomSizeMethod Method
        public ObservableCollection<string> SneakersSeletedCustomSizeMethod(SubRoots subRoots)
        {
            ObservableCollection<string> SizeList = new ObservableCollection<string>();
            try
            {
                string keyNodes = string.Empty;
                string subKeyNodes = string.Empty;
                SubRoots SneakSubRoots = null;
                string gender = "men";
                if (subRoots.Gender.ToUpper() == "F" || subRoots.Gender.ToUpper() == "WOMEN")
                {
                    gender = "women";
                }
                var node = Global.SneakersRootsList.Where(x => x.Root.ToLower().Trim() == gender.ToLower().Trim()).ToList().FirstOrDefault();
                Dictionary<Nodes, List<SubRoots>> dic = node.Node;
                foreach (var keyNode in dic)
                {
                    if (subRoots.SubRoot != null)
                    {
                        var objSubRool = keyNode.Value.Where(s => s.SubRoot.ToLower().Trim() == subRoots.SubRoot.ToLower().Trim()).FirstOrDefault();
                        if (objSubRool != null)
                        {
                            keyNodes = keyNode.Key.NodeTitle;
                            subKeyNodes = subRoots.SubRoot;
                            if (subRoots.Gender.ToUpper() == "M" || subRoots.Gender.ToUpper() == "F")
                            {
                                if (!string.IsNullOrEmpty(subRoots.SubRoot))
                                {
                                    keyNodes = subKeyNodes;
                                }
                            }
                            break;
                        }
                    }
                    else
                    {
                        var objSubRool = keyNode.Key.Gender;
                        if (objSubRool != null)
                        {
                            keyNodes = keyNode.Key.NodeTitle;
                            break;
                        }
                    }
                }
                if (keyNodes == string.Empty)
                {
                    keyNodes = subRoots.SubRoot;
                }
                //Show sizes for men's category choosed by user
                if (subRoots.Gender.ToUpper() == "M" || subRoots.Gender.ToUpper() == "MEN")
                {
                    switch (keyNodes.Trim())
                    {
                        case "Basketball":
                            SizeList = new ObservableCollection<string>() { "4", "4.5", "5", "5.5", "6", "6.5", "7", "7.5", "8", "8.5", "9", "9.5", "10", "10.5", "11", "11.5", "12", "12.5", "13", "13.5", "14", "14.5", "15", "15.5", "16" };
                            break;

                        case "Casual":
                            SizeList = new ObservableCollection<string>() { "4", "4.5", "5", "5.5", "6", "6.5", "7", "7.5", "8", "8.5", "9", "9.5", "10", "10.5", "11", "11.5", "12", "12.5", "13", "13.5", "14", "14.5", "15", "15.5", "16" };
                            break;

                        case "Luxury":
                            SizeList = new ObservableCollection<string>() { "4", "4.5", "5", "5.5", "6", "6.5", "7", "7.5", "8", "8.5", "9", "9.5", "10", "10.5", "11", "11.5", "12", "12.5", "13", "13.5", "14", "14.5", "15", "15.5", "16" };
                            break;

                        case "Running":
                            SizeList = new ObservableCollection<string>() { "4", "4.5", "5", "5.5", "6", "6.5", "7", "7.5", "8", "8.5", "9", "9.5", "10", "10.5", "11", "11.5", "12", "12.5", "13", "13.5", "14", "14.5", "15", "15.5", "16" };
                            break;

                        case "Soccer":
                            SizeList = new ObservableCollection<string>() { "4", "4.5", "5", "5.5", "6", "6.5", "7", "7.5", "8", "8.5", "9", "9.5", "10", "10.5", "11", "11.5", "12", "12.5", "13", "13.5", "14", "14.5", "15", "15.5", "16" };
                            break;

                        case "Skateboard":
                            SizeList = new ObservableCollection<string>() { "4", "4.5", "5", "5.5", "6", "6.5", "7", "7.5", "8", "8.5", "9", "9.5", "10", "10.5", "11", "11.5", "12", "12.5", "13", "13.5", "14", "14.5", "15", "15.5", "16" };
                            break;

                        case "Slides":
                            SizeList = new ObservableCollection<string>() { "4", "4.5", "5", "5.5", "6", "6.5", "7", "7.5", "8", "8.5", "9", "9.5", "10", "10.5", "11", "11.5", "12", "12.5", "13", "13.5", "14", "14.5", "15", "15.5", "16" };
                            break;

                        case "Tennis":
                            SizeList = new ObservableCollection<string>() { "4", "4.5", "5", "5.5", "6", "6.5", "7", "7.5", "8", "8.5", "9", "9.5", "10", "10.5", "11", "11.5", "12", "12.5", "13", "13.5", "14", "14.5", "15", "15.5", "16" };
                            break;

                        case "Track & Field":
                            SizeList = new ObservableCollection<string>() { "4", "4.5", "5", "5.5", "6", "6.5", "7", "7.5", "8", "8.5", "9", "9.5", "10", "10.5", "11", "11.5", "12", "12.5", "13", "13.5", "14", "14.5", "15", "15.5", "16" };
                            break;

                        case "Training":
                            SizeList = new ObservableCollection<string>() { "4", "4.5", "5", "5.5", "6", "6.5", "7", "7.5", "8", "8.5", "9", "9.5", "10", "10.5", "11", "11.5", "12", "12.5", "13", "13.5", "14", "14.5", "15", "15.5", "16" };
                            break;

                        case "Walking":
                            SizeList = new ObservableCollection<string>() { "4", "4.5", "5", "5.5", "6", "6.5", "7", "7.5", "8", "8.5", "9", "9.5", "10", "10.5", "11", "11.5", "12", "12.5", "13", "13.5", "14", "14.5", "15", "15.5", "16" };
                            break;
                    }
                }
                //Show sizes for mwomen's category choosed by user sneakers store
                if (subRoots.Gender.ToUpper() == "F" || subRoots.Gender.ToUpper() == "WOMEN")
                {
                    switch (keyNodes.Trim())
                    {
                        case "Basketball":
                            SizeList = new ObservableCollection<string>() { "4", "4.5", "5", "5.5", "6", "6.5", "7", "7.5", "8", "8.5", "9", "9.5", "10", "10.5", "11", "11.5", "12", "12.5", "13" };
                            break;

                        case "Casual":
                            SizeList = new ObservableCollection<string>() { "4", "4.5", "5", "5.5", "6", "6.5", "7", "7.5", "8", "8.5", "9", "9.5", "10", "10.5", "11", "11.5", "12", "12.5", "13" };
                            break;

                        case "Luxury":
                            SizeList = new ObservableCollection<string>() { "4", "4.5", "5", "5.5", "6", "6.5", "7", "7.5", "8", "8.5", "9", "9.5", "10", "10.5", "11", "11.5", "12", "12.5", "13" };
                            break;

                        case "Running":
                            SizeList = new ObservableCollection<string>() { "4", "4.5", "5", "5.5", "6", "6.5", "7", "7.5", "8", "8.5", "9", "9.5", "10", "10.5", "11", "11.5", "12", "12.5", "13" };
                            break;

                        case "Soccer":
                            SizeList = new ObservableCollection<string>() { "4", "4.5", "5", "5.5", "6", "6.5", "7", "7.5", "8", "8.5", "9", "9.5", "10", "10.5", "11", "11.5", "12", "12.5", "13" };
                            break;

                        case "Skateboard":
                            SizeList = new ObservableCollection<string>() { "4", "4.5", "5", "5.5", "6", "6.5", "7", "7.5", "8", "8.5", "9", "9.5", "10", "10.5", "11", "11.5", "12", "12.5", "13" };
                            break;

                        case "Slides":
                            SizeList = new ObservableCollection<string>() { "4", "4.5", "5", "5.5", "6", "6.5", "7", "7.5", "8", "8.5", "9", "9.5", "10", "10.5", "11", "11.5", "12", "12.5", "13" };
                            break;

                        case "Tennis":
                            SizeList = new ObservableCollection<string>() { "4", "4.5", "5", "5.5", "6", "6.5", "7", "7.5", "8", "8.5", "9", "9.5", "10", "10.5", "11", "11.5", "12", "12.5", "13" };
                            break;

                        case "Track & Field":
                            SizeList = new ObservableCollection<string>() { "4", "4.5", "5", "5.5", "6", "6.5", "7", "7.5", "8", "8.5", "9", "9.5", "10", "10.5", "11", "11.5", "12", "12.5", "13" };
                            break;

                        case "Training":
                            SizeList = new ObservableCollection<string>() { "4", "4.5", "5", "5.5", "6", "6.5", "7", "7.5", "8", "8.5", "9", "9.5", "10", "10.5", "11", "11.5", "12", "12.5", "13" };
                            break;

                        case "Walking":
                            SizeList = new ObservableCollection<string>() { "4", "4.5", "5", "5.5", "6", "6.5", "7", "7.5", "8", "8.5", "9", "9.5", "10", "10.5", "11", "11.5", "12", "12.5", "13" };
                            break;
                    }
                }

                if (subRoots.Gender.ToUpper() == "M" || subRoots.Gender.ToUpper() == "MEN")
                {
                    switch (keyNodes.Trim())
                    {
                        case "Sneakers":
                            SizeList = new ObservableCollection<string>() { "4", "4.5", "5", "5.5", "6", "6.5", "7", "7.5", "8", "8.5", "9", "9.5", "10", "10.5", "11", "11.5", "12", "12.5", "13", "13.5", "14", "14.5", "15", "15.5", "16" };
                            break;
                    }
                }
                if (subRoots.Gender.ToUpper() == "F" || subRoots.Gender.ToUpper() == "WOMEN")
                {
                    switch (keyNodes.Trim())
                    {
                        case "Sneakers":
                            SizeList = new ObservableCollection<string>() { "4", "4.5", "5", "5.5", "6", "6.5", "7", "7.5", "8", "8.5", "9", "9.5", "10", "10.5", "11", "11.5", "12", "12.5", "13" };
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return SizeList;
        }
        #endregion

        #region Get the list of other fields value in Add item details screen
        //Method created to show list of various filters like Shipping price, colors, sort, brand etc
        public ObservableCollection<string> GetAddItemOtherFieldData(int index, SubRoots subRoots)
        {
            ObservableCollection<string> SelectPickerList = new ObservableCollection<string>();
            if (index == 4)
            {
                SelectPickerList = new ObservableCollection<string>() { "1", "2", "3" };
            }
            else if (index == 5)
            {
                SelectPickerList = new ObservableCollection<string>() { "S", "M", "L", "XL", "XXL" };
            }
            else if (index == 6)
            {
                SelectPickerList = new ObservableCollection<string>() { "One"};//, "Multiple" 
            }
            else if (index == 7)
            {
                //Condition added to show category list inside search result products view according to the store selected by user on search result page
                if (Global.Storecategory.ToLower() == Global.SearchedResultSelectedStore.ToLower())
                {
                    if (Global.Storecategory.ToLower() == Constant.ClothingStr.ToLower() || Global.Storecategory.ToLower() == Constant.StreetwearStr.ToLower() || Global.Storecategory.ToLower() == Constant.VintageStr.ToLower())
                    {
                        if (Global.GenderParam == "All")
                        {
                            //This method for all category sizes (Clothing, Streetwear and Vintage)
                            SelectPickerList = MockProductData.Instance.AllCatSelCustomSizeMethod(subRoots);
                        }
                        else
                        {
                            //This method for Men and Women category size (Clothing, Streetwear and Vintage)
                            SelectPickerList = MockProductData.Instance.SeletedCustomSizeMethod(subRoots);
                        }
                    }
                    else
                    {
                        if (Global.GenderParam == "All")
                        {
                            //This method for all Sneakers category sizes
                            SelectPickerList = MockProductData.Instance.SneakersSelCusCommonSizeMethod(subRoots);
                        }
                        else
                        {
                            //This method for Men and Women Sneakers category sizes
                            SelectPickerList = MockProductData.Instance.SneakersSeletedCustomSizeMethod(subRoots);
                        }
                    }
                }
                else
                {
                    if (Global.SearchedResultSelectedStore.ToLower() == Constant.ClothingStr.ToLower() || Global.SearchedResultSelectedStore.ToLower() == Constant.StreetwearStr.ToLower() || Global.SearchedResultSelectedStore.ToLower() == Constant.VintageStr.ToLower())
                    {
                        if (Global.GenderParam == "All")
                        {
                            //This method for all category sizes (Clothing, Streetwear and Vintage)
                            SelectPickerList = MockProductData.Instance.AllCatSelCustomSizeMethod(subRoots);
                        }
                        else
                        {
                            //This method for Men and Women category size (Clothing, Streetwear and Vintage)
                            SelectPickerList = MockProductData.Instance.SeletedCustomSizeMethod(subRoots);
                        }
                    }
                    else
                    {
                        if (Global.GenderParam == "All")
                        {
                            //This method for all Sneakers category sizes
                            SelectPickerList = MockProductData.Instance.SneakersSelCusCommonSizeMethod(subRoots);
                        }
                        else
                        {
                            //This method for Men and Women Sneakers category sizes
                            SelectPickerList = MockProductData.Instance.SneakersSeletedCustomSizeMethod(subRoots);
                        }
                    }
                }
            }
            else if (index == 8)
            {
                SelectPickerList = new ObservableCollection<string>() { "Supreme", "Coogi", "Reebok", "Adidas", "Wrogen", "Nike", "Wrangler" };
            }
            else if (index == 9)
            {
                SelectPickerList = new ObservableCollection<string>() { "Red", "Pink", "Orange", "Yellow", "Green", "Blue", "Purple", "Gold", "Silver", "Black", "Gray", "White", "Cream", "Brown", "Tan", "Camo", "Multi-Color" };
            }
            else if (index == 10)
            {
                SelectPickerList = new ObservableCollection<string>() { "NWT(New with price tag attached)", "NWOT(New with price tag removed)", "Used(Preowned)" }; //"Good","Bad", "Damaged", "Refurbished"
            }
            else if (index == 11)
            {
                SelectPickerList = new ObservableCollection<string>() { "For Sale", "Not For Sale", "Sold" };
            }
            //else if (index == 12)
            //{
            //    SelectPickerList = new ObservableCollection<string>() { "$123", " $130", "140" };
            //}
            else if (index == 13)
            {
                SelectPickerList = new ObservableCollection<string>() { "$8", " $11", "$14" };
            }
            else if (index == 14)
            {
                SelectPickerList = new ObservableCollection<string>() { "Men", "Women" };
            }
            else if (index == 15)
            {
                if (nav != null && nav.NavigationStack.Count >= 1)
                {
                    if (nav.NavigationStack.LastOrDefault().GetType() == typeof(TopTrendingViewAllPage))
                    {
                        SelectPickerList = new ObservableCollection<string>() { "Price: Low to High", "Price: High to Low" };
                        return SelectPickerList;
                    }
                    else
                    {
                        SelectPickerList = new ObservableCollection<string>() { "Price: Low to High", "Price: High to Low", "New Listing" };
                    }
                }
                else
                {
                    SelectPickerList = new ObservableCollection<string>() { "Price: Low to High", "Price: High to Low", "New Listing" };
                }
            }
            else if (index == 16)
            {
                SelectPickerList = new ObservableCollection<string>() { "Under $25", "$25 - $50", "$50 - $100", "$100 - $250", "$250 - $500", "Over $500" };
            }

            return SelectPickerList;
        }
        #endregion

        #region Get list of shop by category
        //Method created to show text and images at dashboard page Shop by category module
        public ObservableCollection<ShopByCategoryModel> GetShopByCatList(int SeletedFilter, int SelectedIndexHeaderTab)
        {
            ObservableCollection<ShopByCategoryModel> ShopByCategoryList = new ObservableCollection<ShopByCategoryModel>();
            try
            {
                //Show list for Clothing 
                if (SelectedIndexHeaderTab != 2 && SelectedIndexHeaderTab != 3 && SelectedIndexHeaderTab != 4)
                {
                    //Show list for men clothing
                    if (SeletedFilter == 1)
                    {
                        ShopByCategoryList = new ObservableCollection<ShopByCategoryModel>()
                        {
                            new ShopByCategoryModel()
                            {
                                CategoryImage1="Tops",
                                CategoryImage2="Pants",
                                CategoryImage3="MBag",

                                CategoryName1="Shirts",
                                CategoryName2="Pants",//jeans,pants &shorts
                                CategoryName3="Bags"
                            },
                            new ShopByCategoryModel()
                            {
                                CategoryImage1="Outwear",
                                CategoryImage2="MShoes",
                                CategoryImage3="Accesories",

                                CategoryName1="Coats",
                                CategoryName2="Shoes",
                                CategoryName3="Accessories",
                            },
                            new ShopByCategoryModel()
                            {
                                CategoryImage1="Suits",
                                CategoryImage2="Socks",
                                CategoryImage3="SwimM",

                                CategoryName1="Suits",
                                CategoryName2="Socks",
                                CategoryName3="Swim",
                            },
                            new ShopByCategoryModel()
                            {
                                CategoryImage1="Other",
                                CategoryName1="Other",
                            }
                        };
                    }
                    //Show list for women clothing
                    if (SeletedFilter == 2)
                    {
                        ShopByCategoryList = new ObservableCollection<ShopByCategoryModel>()
                        {
                            new ShopByCategoryModel()
                            {
                                CategoryImage1="TopsF",
                                CategoryImage2="Pants",
                                CategoryImage3="Handbags",

                                CategoryName1="Tops",
                                CategoryName2="Bottoms",//jeans,pants,shorts & skirts
                                CategoryName3="Bags"
                            },
                            new ShopByCategoryModel()
                            {
                                CategoryImage1="Dresses",
                                CategoryImage2="Shoes",
                                CategoryImage3="Accesories",

                                CategoryName1="Dresses",
                                CategoryName2="Shoes",
                                CategoryName3="Accessories"
                            },
                            new ShopByCategoryModel()
                            {
                                CategoryImage1="Outwear",
                                CategoryImage2="Suits",
                                CategoryImage3="SwimF",

                                CategoryName1="Coats",
                                CategoryName2="Suits",
                                CategoryName3="Swim"
                            },
                            new ShopByCategoryModel()
                            {
                                CategoryImage1="Jewelry",
                                CategoryImage2="Other",
                                CategoryName1="Jewelry",
                                CategoryName2="Other",
                            },
                        };
                    }
                    //Show list for all clothing
                    if (SeletedFilter == 3)
                    {
                        ShopByCategoryList = new ObservableCollection<ShopByCategoryModel>()
                        {
                            new ShopByCategoryModel()
                            {
                                CategoryImage1="Tops",
                                CategoryImage2="Pants",
                                CategoryImage3="MBag",

                                CategoryName1="Shirts",//Shirts & Sweaters
                                CategoryName2="Bottoms & Pants",//Jeans, Pants, Shorts for Men and Jeans, Pants, Shorts and Skirts for Women
                                CategoryName3="Bags"
                            },
                            new ShopByCategoryModel()
                            {
                                CategoryImage1="TopsF",
                                CategoryImage2="Dresses",
                                CategoryImage3="Shoes",

                                CategoryName1="Tops",
                                CategoryName2="Dresses",//Jeans, Pants, Shorts, Skirts 
                                CategoryName3="Shoes"
                            },
                            new ShopByCategoryModel()
                            {

                                CategoryImage1="Accesories",
                                CategoryImage2="Outwear",
                                CategoryImage3="Suits",

                                CategoryName1="Accessories",
                                CategoryName2="Coats",
                                CategoryName3="Suits"
                            },
                            new ShopByCategoryModel()
                            {
                                CategoryImage1="MShoes",
                                CategoryImage2="SwimM",
                                CategoryImage3="Socks",

                                CategoryName1="Shoes",
                                CategoryName2="Swim",
                                CategoryName3="Socks"
                            },
                            new ShopByCategoryModel()
                            {

                                CategoryImage1="Jewelry",
                                CategoryImage2="Other",

                                CategoryName1="Jewelry",
                                CategoryName2="Other",
                            },
                        };
                    }
                }
                //Show list for  Streetwear
                else if (SelectedIndexHeaderTab == 3)
                {
                    //Show list for men streetwear
                    if (SeletedFilter == 1)
                    {
                        ShopByCategoryList = new ObservableCollection<ShopByCategoryModel>()
                        {
                            new ShopByCategoryModel()
                            {
                                CategoryImage1="Tops",
                                CategoryImage2="Pants",
                                CategoryImage3="MBag",

                                CategoryName1="Shirts",
                                CategoryName2="Pants",//jeans,pants &shorts
                                CategoryName3="Bags"
                            },
                            new ShopByCategoryModel()
                            {
                                CategoryImage1="Outwear",
                                CategoryImage2="Suits",
                                CategoryImage3="Accesories",

                                CategoryName1="Coats",
                                CategoryName2="Suits",
                                CategoryName3="Accessories",
                            },
                            new ShopByCategoryModel()
                            {
                                CategoryImage1="Socks",
                                CategoryImage2="SwimM",
                                CategoryImage3="Other",

                                CategoryName1="Socks",
                                CategoryName2="Swim",
                                CategoryName3="Other",
                            },
                        };
                    }
                    //Show list for women streetwear
                    if (SeletedFilter == 2)
                    {
                        ShopByCategoryList = new ObservableCollection<ShopByCategoryModel>()
                        {
                            new ShopByCategoryModel()
                            {
                                CategoryImage1="TopsF",
                                CategoryImage2="Pants",
                                CategoryImage3="Handbags",

                                CategoryName1="Tops",
                                CategoryName2="Bottoms",//jeans,pants,shorts & skirts
                                CategoryName3="Bags"
                            },
                            new ShopByCategoryModel()
                            {
                                CategoryImage1="Dresses",
                                CategoryImage2="Jewelry",
                                CategoryImage3="Accesories",

                                CategoryName1="Dresses",
                                CategoryName2="Jewelry",
                                CategoryName3="Accessories"
                            },
                            new ShopByCategoryModel()
                            {
                                CategoryImage1="Outwear",
                                CategoryImage2="Suits",
                                CategoryImage3="SwimF",

                                CategoryName1="Coats",
                                CategoryName2="Suits",
                                CategoryName3="Swim"
                            },
                            new ShopByCategoryModel()
                            {
                                CategoryImage1="Other",
                                CategoryName1="Other",
                            },
                        };
                    }
                    //Show list for all streetwear
                    if (SeletedFilter == 3)
                    {
                        ShopByCategoryList = new ObservableCollection<ShopByCategoryModel>()
                        {
                            new ShopByCategoryModel()
                            {
                                CategoryImage1="Tops",
                                CategoryImage2="Pants",
                                CategoryImage3="MBag",

                                CategoryName1="Shirts",//Shirts & Sweaters
                                CategoryName2="Bottoms & Pants",//Jeans, Pants, Shorts for Men and Jeans, Pants, Shorts and Skirts for Women
                                CategoryName3="Bags"
                            },
                            new ShopByCategoryModel()
                            {
                                CategoryImage1="TopsF",
                                CategoryImage2="Dresses",
                                CategoryImage3="Jewelry",

                                CategoryName1="Tops",
                                CategoryName2="Dresses",//Jeans, Pants, Shorts, Skirts 
                                CategoryName3="Jewelry",
                            },
                            new ShopByCategoryModel()
                            {
                                CategoryImage1="Accesories",
                                CategoryImage2="Outwear",
                                CategoryImage3="Suits",

                                CategoryName1="Accessories",
                                CategoryName2="Coats",
                                CategoryName3="Suits"
                            },
                            new ShopByCategoryModel()
                            {
                                CategoryImage3="Other",
                                CategoryImage2="SwimM",
                                CategoryImage1="Socks",

                                CategoryName3="Other",
                                CategoryName2="Swim",
                                CategoryName1="Socks"
                            }
                        };
                    }
                }
                //show list for vintage
                else if (SelectedIndexHeaderTab == 4)
                {
                    //Show list for men vintage
                    if (SeletedFilter == 1)
                    {
                        ShopByCategoryList = new ObservableCollection<ShopByCategoryModel>()
                        {
                            new ShopByCategoryModel()
                            {
                                CategoryImage1="Tops",
                                CategoryImage2="Pants",
                                CategoryImage3="MBag",

                                CategoryName1="Shirts",
                                CategoryName2="Pants",//jeans,pants and shorts
                                CategoryName3="Bags"
                            },
                            new ShopByCategoryModel()
                            {
                                CategoryImage1="Outwear",
                                CategoryImage2="MShoes",
                                CategoryImage3="Accesories",

                                CategoryName1="Coats",
                                CategoryName2="Shoes",
                                CategoryName3="Accessories",
                            },
                            new ShopByCategoryModel()
                            {
                                CategoryImage1="Suits",
                                CategoryImage2="SwimM",
                                CategoryImage3="Other",

                                CategoryName1="Suits",
                                CategoryName2="Swim",
                                CategoryName3="Other"
                            },

                        };
                    }
                    //Show list for women vintage
                    if (SeletedFilter == 2)
                    {
                        ShopByCategoryList = new ObservableCollection<ShopByCategoryModel>()
                        {
                            new ShopByCategoryModel()
                            {
                                CategoryImage1="TopsF",
                                CategoryImage2="Pants",
                                CategoryImage3="Handbags",

                                CategoryName1="Tops",
                                CategoryName2="Bottoms",//jeans,pants, shorts and skirts
                                CategoryName3="Bags"
                            },
                            new ShopByCategoryModel()
                            {
                                CategoryImage1="Dresses",
                                CategoryImage2="Shoes",
                                CategoryImage3="Accesories",

                                CategoryName1="Dresses",
                                CategoryName2="Shoes",
                                CategoryName3="Accessories"
                            },
                            new ShopByCategoryModel()
                            {
                                CategoryImage1="Outwear",
                                CategoryImage2="Suits",
                                CategoryImage3="SwimF",

                                CategoryName1="Coats",
                                CategoryName2="Suits",
                                CategoryName3="Swim"
                            },
                            new ShopByCategoryModel()
                            {
                                CategoryImage1="Jewelry",
                                CategoryImage2="Other",

                                CategoryName1="Jewelry",
                                CategoryName2="Other",
                            },
                        };
                    }
                    //Show list for all vintage
                    if (SeletedFilter == 3)
                    {
                        ShopByCategoryList = new ObservableCollection<ShopByCategoryModel>()
                        {
                            new ShopByCategoryModel()
                            {
                                CategoryImage1="Tops",
                                CategoryImage2="Pants",
                                CategoryImage3="MBag",

                                CategoryName1="Shirts",//Shirts & Sweaters
                                CategoryName2="Bottoms & Pants",//Jeans, Pants, Shorts
                                CategoryName3="Bags"
                            },
                            new ShopByCategoryModel()
                            {

                                CategoryImage1="TopsF",
                                CategoryImage2="Dresses",
                                CategoryImage3="Shoes",

                                CategoryName1="Tops",
                                CategoryName2="Dresses",//Jeans, Pants, Shorts, Skirts 
                                CategoryName3="Shoes"
                            },
                            new ShopByCategoryModel()
                            {


                                CategoryImage1="Accesories",
                                CategoryImage2="Outwear",
                                CategoryImage3="Suits",

                                CategoryName1="Accessories",
                                CategoryName2="Coats",
                                CategoryName3="Suits"
                            },
                            new ShopByCategoryModel()
                            {


                                CategoryImage1="MShoes",
                                CategoryImage2="SwimM",
                                CategoryImage3="Jewelry",

                                CategoryName1="Shoes",
                                CategoryName2="Swim",
                                CategoryName3="Jewelry",

                            },
                            new ShopByCategoryModel()
                            {
                               CategoryImage1="Other",
                               CategoryName1="Other",
                            },
                        };
                    }
                }
                else
                {
                    //Show list for sneakers men, women and all
                    ShopByCategoryList = new ObservableCollection<ShopByCategoryModel>()
                    {
                        new ShopByCategoryModel()
                        {
                            CategoryImage1="Basketball",
                            CategoryImage2="Casual",
                            CategoryImage3="Luxury",

                            CategoryName1="Basketball",
                            CategoryName2="Casual",
                            CategoryName3="Luxury"
                        },
                        new ShopByCategoryModel()
                        {
                            CategoryImage1="Running",
                            CategoryImage2="Skate",
                            CategoryImage3="Slide",

                            CategoryName1="Running",
                            CategoryName2="Skate",
                            CategoryName3="Slides"
                        },
                        new ShopByCategoryModel()
                        {
                            CategoryImage1="Soccer",
                            CategoryImage2="Tennis",
                            CategoryImage3="Trackfield",
                            CategoryName1="Soccer",
                            CategoryName2="Tennis",
                            CategoryName3="Track"
                        },
                        new ShopByCategoryModel()
                        {

                            CategoryImage1="Training",
                            CategoryImage2="Walking",

                            CategoryName1="Training",
                            CategoryName2="Walking",

                        },
                    };
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return ShopByCategoryList;
        }
        #endregion

        #region GetCategoryList (from filter) Method
        //Method  specially created for custom filter category list because category list items will show based on selected category and sub category for vintage, clothing and streetwears store
        public List<Roots> GetCategoryList(string selectedcat, string selectedsubcat)
        {
            //Category list for Women
            #region Category list for Women
            List<Roots> roots;
            List<SubRoots> subRootWAccessories = new List<SubRoots>() { new SubRoots() { SubRoot = "Belts", Gender = "F", Root = "Women", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Electronics Cases", Gender = "F", Root = "Women", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Face Masks", Gender = "F", Root = "Women", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Glasses", Gender = "F", Root = "Women", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Gloves & Mittens", Gender = "F", Root = "Women", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Hair Accessories", Gender = "F", Root = "Women", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Hats", Gender = "F", Root = "Women", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Hosiery", Gender = "F", Root = "Women", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Scarves & Wraps", Gender = "F", Root = "Women", NodeTitle = "Accessories" }, 
                //new SubRoots() { SubRoot = "Socks", Gender = "F", Root = "Women", NodeTitle = "Accessories" }, 
                new SubRoots() { SubRoot = "Sunglasses", Gender = "F", Root = "Women", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Umbrellas", Gender = "F", Root = "Women", NodeTitle = "Accessories" }, 
                //new SubRoots() { SubRoot = "Watches", Gender = "F", Root = "Women", NodeTitle = "Accessories" } 
            };
            List<SubRoots> subRootWBages = new List<SubRoots>() { new SubRoots() { SubRoot = "Baby Bags", Gender = "F", Root = "Women", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Backpacks", Gender = "F", Root = "Women", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Belt Bags", Gender = "F", Root = "Women", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Clutches & Wristlets", Gender = "F", Root = "Women", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Cosmetic Bags", Gender = "F", Root = "Women", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Crossbody Bags", Gender = "F", Root = "Women", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Hobos", Gender = "F", Root = "Women", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Laptop Bags", Gender = "F", Root = "Women", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Luggage & Travel Bags", Gender = "F", Root = "Women", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Satchels", Gender = "F", Root = "Women", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Shoulder Bags", Gender = "F", Root = "Women", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Totes", Gender = "F", Root = "Women", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Wallets", Gender = "F", Root = "Women", NodeTitle = "Bags" } };
            List<SubRoots> subRootWDresses = new List<SubRoots>() { new SubRoots() { SubRoot = "Asymmetrical", Gender = "F", Root = "Women", NodeTitle = "Dresses" }, new SubRoots() { SubRoot = "High Low", Gender = "F", Root = "Women", NodeTitle = "Dresses" }, new SubRoots() { SubRoot = "Long Sleeve", Gender = "F", Root = "Women", NodeTitle = "Dresses" }, new SubRoots() { SubRoot = "Maxi", Gender = "F", Root = "Women", NodeTitle = "Dresses" }, new SubRoots() { SubRoot = "Midi", Gender = "F", Root = "Women", NodeTitle = "Dresses" }, new SubRoots() { SubRoot = "Mini", Gender = "F", Root = "Women", NodeTitle = "Dresses" }, new SubRoots() { SubRoot = "Prom", Gender = "F", Root = "Women", NodeTitle = "Dresses" }, new SubRoots() { SubRoot = "Rompers", Gender = "F", Root = "Women", NodeTitle = "Dresses" }, new SubRoots() { SubRoot = "Strapless", Gender = "F", Root = "Women", NodeTitle = "Dresses" }, new SubRoots() { SubRoot = "Wedding", Gender = "F", Root = "Women", NodeTitle = "Dresses" } };
            List<SubRoots> subRootWIntimatesSleepwear = new List<SubRoots>() { new SubRoots() { SubRoot = "Bras", Gender = "F", Root = "Women", NodeTitle = "Intimates & Sleepwear" }, new SubRoots() { SubRoot = "Chemises & Slips", Gender = "F", Root = "Women", NodeTitle = "Intimates & Sleepwear" }, new SubRoots() { SubRoot = "Pajamas", Gender = "F", Root = "Women", NodeTitle = "Intimates & Sleepwear" }, new SubRoots() { SubRoot = "Panties", Gender = "F", Root = "Women", NodeTitle = "Intimates & Sleepwear" }, new SubRoots() { SubRoot = "Robes", Gender = "F", Root = "Women", NodeTitle = "Intimates & Sleepwear" }, new SubRoots() { SubRoot = "Shapewear", Gender = "F", Root = "Women", NodeTitle = "Intimates & Sleepwear" } };
            List<SubRoots> subRootWJacketsCoats = new List<SubRoots>() { new SubRoots() { SubRoot = "Active", Gender = "F", Root = "Women", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Blazers", Gender = "F", Root = "Women", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Bombers", Gender = "F", Root = "Women", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Capes & Ponchos", Gender = "F", Root = "Women", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Denim & Utility", Gender = "F", Root = "Women", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Down & Puffers", Gender = "F", Root = "Women", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Fleece", Gender = "F", Root = "Women", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Fur & Faux Fur", Gender = "F", Root = "Women", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Leather & Faux Leather", Gender = "F", Root = "Women", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Rain Coats", Gender = "F", Root = "Women", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Ski", Gender = "F", Root = "Women", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Teddy", Gender = "F", Root = "Women", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Trench Coats", Gender = "F", Root = "Women", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Varsity", Gender = "F", Root = "Women", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Vests", Gender = "F", Root = "Women", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Windbreakers", Gender = "F", Root = "Women", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Wool & Pea Coats", Gender = "F", Root = "Women", NodeTitle = "Jackets & Coats" } };
            if (Global.Storecategory == Constant.ClothingStr)
            {
                Global.CatBySubList = new List<SubRoots>() { new SubRoots() { SubRoot = "Bootcut", Gender = "F", Root = "Women", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Crooped & Ankle", Gender = "F", Root = "Women", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Flare & Wide Leg", Gender = "F", Root = "Women", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "High Rise", Gender = "F", Root = "Women", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Jeggings", Gender = "F", Root = "Women", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Relaxed", Gender = "F", Root = "Women", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Skinny", Gender = "F", Root = "Women", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Straight", Gender = "F", Root = "Women", NodeTitle = "Jeans" } };
            }
            else
            {
                Global.CatBySubList = new List<SubRoots>() { new SubRoots() { SubRoot = "Bootcut", Gender = "F", Root = "Women", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Crooped & Ankle", Gender = "F", Root = "Women", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Flare & Wide Leg", Gender = "F", Root = "Women", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "High Rise", Gender = "F", Root = "Women", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Jeggings", Gender = "F", Root = "Women", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Relaxed", Gender = "F", Root = "Women", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Skinny", Gender = "F", Root = "Women", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Straight", Gender = "F", Root = "Women", NodeTitle = "Jeans" } };
            }
            List<SubRoots> subRootWJeans = Global.CatBySubList;
            List<SubRoots> subRootWJewelry = new List<SubRoots>() { new SubRoots() { SubRoot = "Bracelets", Gender = "F", Root = "Women", NodeTitle = "Jewelry" }, new SubRoots() { SubRoot = "Brooches", Gender = "F", Root = "Women", NodeTitle = "Jewelry" }, new SubRoots() { SubRoot = "Earrings", Gender = "F", Root = "Women", NodeTitle = "Jewelry" }, new SubRoots() { SubRoot = "Necklaces", Gender = "F", Root = "Women", NodeTitle = "Jewelry" }, new SubRoots() { SubRoot = "Rings", Gender = "F", Root = "Women", NodeTitle = "Jewelry" }, new SubRoots() { SubRoot = "Watches", Gender = "F", Root = "Women", NodeTitle = "Jewelry" } };
            List<SubRoots> subRootWPants = new List<SubRoots>() { new SubRoots() { SubRoot = "Bootcut & Flare", Gender = "F", Root = "Women", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "Crops & Capris", Gender = "F", Root = "Women", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "High Waisted", Gender = "F", Root = "Women", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "Leggings", Gender = "F", Root = "Women", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "Overalls & Jumpsuits", Gender = "F", Root = "Women", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "Skinny", Gender = "F", Root = "Women", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "Straight", Gender = "F", Root = "Women", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "Sweatpants & Joggers", Gender = "F", Root = "Women", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "Trousers & Wide Leg", Gender = "F", Root = "Women", NodeTitle = "Pants" } };
            List<SubRoots> subRootWShoes = new List<SubRoots>() { new SubRoots() { SubRoot = "Boots & Booties", Gender = "F", Root = "Women", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Clogs & Mules", Gender = "F", Root = "Women", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Espadrilles", Gender = "F", Root = "Women", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Flats & Loafers", Gender = "F", Root = "Women", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Heels", Gender = "F", Root = "Women", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Narrow", Gender = "F", Root = "Women", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Sandals", Gender = "F", Root = "Women", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Single Shoes", Gender = "F", Root = "Women", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Slippers", Gender = "F", Root = "Women", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Wedges", Gender = "F", Root = "Women", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Wide", Gender = "F", Root = "Women", NodeTitle = "Shoes" } };
            List<SubRoots> subRootWShorts = new List<SubRoots>() { new SubRoots() { SubRoot = "Active", Gender = "F", Root = "Women", NodeTitle = "Shorts" }, new SubRoots() { SubRoot = "Bermudas", Gender = "F", Root = "Women", NodeTitle = "Shorts" }, new SubRoots() { SubRoot = "Bikers", Gender = "F", Root = "Women", NodeTitle = "Shorts" }, new SubRoots() { SubRoot = "Cargo", Gender = "F", Root = "Women", NodeTitle = "Shorts" }, new SubRoots() { SubRoot = "Chino", Gender = "F", Root = "Women", NodeTitle = "Shorts" }, new SubRoots() { SubRoot = "Denim", Gender = "F", Root = "Women", NodeTitle = "Shorts" }, new SubRoots() { SubRoot = "High Waist", Gender = "F", Root = "Women", NodeTitle = "Shorts" }, new SubRoots() { SubRoot = "Short Shorts", Gender = "F", Root = "Women", NodeTitle = "Shorts" }, new SubRoots() { SubRoot = "Skorts", Gender = "F", Root = "Women", NodeTitle = "Shorts" } };
            List<SubRoots> subRootWSkirts = new List<SubRoots>() { new SubRoots() { SubRoot = "A Line", Gender = "F", Root = "Women", NodeTitle = "Skirts" }, new SubRoots() { SubRoot = "Asymmetrical", Gender = "F", Root = "Women", NodeTitle = "Skirts" }, new SubRoots() { SubRoot = "Denim", Gender = "F", Root = "Women", NodeTitle = "Skirts" }, new SubRoots() { SubRoot = "High Low", Gender = "F", Root = "Women", NodeTitle = "Skirts" }, new SubRoots() { SubRoot = "Maxi", Gender = "F", Root = "Women", NodeTitle = "Skirts" }, new SubRoots() { SubRoot = "Midi", Gender = "F", Root = "Women", NodeTitle = "Skirts" }, new SubRoots() { SubRoot = "Mini", Gender = "F", Root = "Women", NodeTitle = "Skirts" }, new SubRoots() { SubRoot = "Pencil", Gender = "F", Root = "Women", NodeTitle = "Skirts" }, new SubRoots() { SubRoot = "Pleated", Gender = "F", Root = "Women", NodeTitle = "Skirts" } };
            List<SubRoots> subRootWSuitsSeparates = new List<SubRoots>() { };
            List<SubRoots> subRootWSweaters = new List<SubRoots>() { new SubRoots() { SubRoot = "Cardigans", Gender = "F", Root = "Women", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Cashmere", Gender = "F", Root = "Women", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Cold Shoulder", Gender = "F", Root = "Women", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Cowl & Turtle Neck", Gender = "F", Root = "Women", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Crew & Scoop Necks", Gender = "F", Root = "Women", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Cropped", Gender = "F", Root = "Women", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Hoodie", Gender = "F", Root = "Women", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Ponchos", Gender = "F", Root = "Women", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Shrugs", Gender = "F", Root = "Women", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Tunic", Gender = "F", Root = "Women", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Vest", Gender = "F", Root = "Women", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "V-Neck", Gender = "F", Root = "Women", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Wrap", Gender = "F", Root = "Women", NodeTitle = "Sweaters" } };
            List<SubRoots> subRootWSwimsuits = new List<SubRoots>() { new SubRoots() { SubRoot = "Bikinis", Gender = "F", Root = "Women", NodeTitle = "Swimsuits" }, new SubRoots() { SubRoot = "Cover-Ups", Gender = "F", Root = "Women", NodeTitle = "Swimsuits" }, new SubRoots() { SubRoot = "One Pieces", Gender = "F", Root = "Women", NodeTitle = "Swimsuits" }, new SubRoots() { SubRoot = "Tankinis", Gender = "F", Root = "Women", NodeTitle = "Swimsuits" } };
            //List<SubRoots> subRootWTops = new List<SubRoots>() { new SubRoots() { SubRoot = "Blouses", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Bodysuits", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Button Down Shirts", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Cowl & Turtleneck", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Crop Tops", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Sweatshirts & Hoodies", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Tank Tops", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Tees: Long Sleeve", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Tees: Short Sleeve", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Tunics", Gender = "F", Root = "Women", NodeTitle = "Tops" } };
            List<SubRoots> subRootWTops = new List<SubRoots>() { new SubRoots() { SubRoot = "Blouses", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Bodysuits", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Button Down Shirts", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Camisoles", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Cardigans and Shrugs", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Cowl & Turtleneck", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Crop Tops", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Jerseys", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Muscle Tees", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Sweatshirts & Hoodies", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Tank Tops", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Tees: Long Sleeve", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Tees: Short Sleeve", Gender = "F", Root = "Women", NodeTitle = "Tops" } };
            List<SubRoots> subRootWBottoms = new List<SubRoots>() { new SubRoots() { SubRoot = "Bootcut", Gender = "F", Root = "Women", NodeTitle = "Bottoms" },new SubRoots() { SubRoot = "Crooped & Ankle", Gender = "F", Root = "Women", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Flare & Wide Leg", Gender = "F", Root = "Women", NodeTitle = "Bottoms" },new SubRoots() { SubRoot = "High Rise", Gender = "F", Root = "Women", NodeTitle = "Bottoms" },new SubRoots() { SubRoot = "Jeggings", Gender = "F", Root = "Women", NodeTitle = "Bottoms" },new SubRoots() { SubRoot = "Relaxed", Gender = "F", Root = "Women", NodeTitle = "Bottoms" },new SubRoots() { SubRoot = "Skinny", Gender = "F", Root = "Women", NodeTitle = "Bottoms" },new SubRoots() { SubRoot = "Straight", Gender = "F", Root = "Women", NodeTitle = "Bottoms" },new SubRoots() { SubRoot = "Tunics", Gender = "F", Root = "Women", NodeTitle = "Bottoms" },new SubRoots() { SubRoot = "Bootcut & Flare", Gender = "F", Root = "Women", NodeTitle = "Bottoms" },new SubRoots() { SubRoot = "Crops & Capris", Gender = "F", Root = "Women", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "High Waisted", Gender = "F", Root = "Women", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Overalls & Jumpsuits", Gender = "F", Root = "Women", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Sweatpants & Joggers", Gender = "F", Root = "Women", NodeTitle = "Bottoms" },new SubRoots() { SubRoot = "Trousers & Wide Leg", Gender = "F", Root = "Women", NodeTitle = "Pants" },new SubRoots() { SubRoot = "Active", Gender = "F", Root = "Women", NodeTitle = "Bottoms" },new SubRoots() { SubRoot = "Bermudas", Gender = "F", Root = "Women", NodeTitle = "Bottoms" },new SubRoots() { SubRoot = "Bikers", Gender = "F", Root = "Women", NodeTitle = "Shorts" },new SubRoots() { SubRoot = "Cargo", Gender = "F", Root = "Women", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Chino", Gender = "F", Root = "Women", NodeTitle = "Shorts" },new SubRoots() { SubRoot = "Denim", Gender = "F", Root = "Women", NodeTitle = "Bottoms" },new SubRoots() { SubRoot = "Short Shorts", Gender = "F", Root = "Women", NodeTitle = "Bottoms" },new SubRoots() { SubRoot = "Skorts", Gender = "F", Root = "Women", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "A Line", Gender = "F", Root = "Women", NodeTitle = "Bottoms" },new SubRoots() { SubRoot = "Asymmetrical", Gender = "F", Root = "Women", NodeTitle = "Bottoms" },new SubRoots() { SubRoot = "High Low", Gender = "F", Root = "Women", NodeTitle = "Bottoms" },new SubRoots() { SubRoot = "Maxi", Gender = "F", Root = "Women", NodeTitle = "Bottoms" },new SubRoots() { SubRoot = "Midi", Gender = "F", Root = "Women", NodeTitle = "Bottoms" },new SubRoots() { SubRoot = "Mini", Gender = "F", Root = "Women", NodeTitle = "Bottoms" },new SubRoots() { SubRoot = "Pencil", Gender = "F", Root = "Women", NodeTitle = "Bottoms" },new SubRoots() { SubRoot = "Pleated", Gender = "F", Root = "Women", NodeTitle = "Bottoms" } };
            List<SubRoots> subRootWOtherClothing = new List<SubRoots>() { };

            Dictionary<Nodes, List<SubRoots>> nodeW = new Dictionary<Nodes, List<SubRoots>>();

            nodeW.Add(new Nodes() { NodeTitle = "Accessories", IsShowMore = true, Root = "Women" }, subRootWAccessories);
            nodeW.Add(new Nodes() { NodeTitle = "Bags", IsShowMore = true, Root = "Women" }, subRootWBages);
            nodeW.Add(new Nodes() { NodeTitle = "Dresses", IsShowMore = true, Root = "Women" }, subRootWDresses);
            nodeW.Add(new Nodes() { NodeTitle = "Intimates & Sleepwear", IsShowMore = true, Root = "Women" }, subRootWIntimatesSleepwear);
            nodeW.Add(new Nodes() { NodeTitle = "Jackets & Coats", IsShowMore = true, Root = "Women" }, subRootWJacketsCoats);
            nodeW.Add(new Nodes() { NodeTitle = "Jeans", IsShowMore = true, Root = "Women" }, subRootWJeans);
            nodeW.Add(new Nodes() { NodeTitle = "Jewelry", IsShowMore = true, Root = "Women" }, subRootWJewelry);
            nodeW.Add(new Nodes() { NodeTitle = "Pants", IsShowMore = true, Root = "Women" }, subRootWPants);
            if (Global.SearchedResultSelectedStore.ToLower() == Constant.ClothingStr.ToLower() || Global.SearchedResultSelectedStore.ToLower() == Constant.VintageStr.ToLower())
            {
                nodeW.Add(new Nodes() { NodeTitle = "Shoes", IsShowMore = true, Root = "Women" }, subRootWShoes);
            }
            nodeW.Add(new Nodes() { NodeTitle = "Shorts", IsShowMore = true, Root = "Women" }, subRootWShorts);
            nodeW.Add(new Nodes() { NodeTitle = "Skirts", IsShowMore = true, Root = "Women" }, subRootWSkirts);
            nodeW.Add(new Nodes() { NodeTitle = "Suits & Separates", IsShowMore = false, Root = "Women" }, subRootWSuitsSeparates);
            nodeW.Add(new Nodes() { NodeTitle = "Sweaters", IsShowMore = true, Root = "Women" }, subRootWSweaters);
            nodeW.Add(new Nodes() { NodeTitle = "Swimsuits", IsShowMore = true, Root = "Women" }, subRootWSwimsuits);
            nodeW.Add(new Nodes() { NodeTitle = "Tops", IsShowMore = true, Root = "Women" }, subRootWTops);
            //nodeW.Add(new Nodes() { NodeTitle = "Bottoms", IsShowMore = true, Root = "Women" }, subRootWBottoms);
            nodeW.Add(new Nodes() { NodeTitle = "Other", IsShowMore = false, Root = "Women" }, subRootWOtherClothing);
            #endregion

            //Category list for Men
            #region Category list for Men
            if (Global.Storecategory == Constant.StreetwearStr)
            {
                Global.CatBySubList = new List<SubRoots>() { new SubRoots() { SubRoot = "Belts", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Electronics Cases", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Face Masks", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Glasses", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Gloves & Scarves", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Hats", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Jewelry & Watches", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Pajamas & Robes", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Sunglasses", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Ties & Pocket Squares", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Umbrellas", Gender = "M", Root = "Men", NodeTitle = "Accessories" } };
            }
            else
            {
                Global.CatBySubList = new List<SubRoots>() { new SubRoots() { SubRoot = "Belts", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Electronics Cases", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Face Masks", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Glasses", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Gloves & Scarves", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Hats", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Jewelry & Watches", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Pajamas & Robes", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Sunglasses", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Ties & Pocket Squares", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Umbrellas", Gender = "M", Root = "Men", NodeTitle = "Accessories" } };
            }
            List<SubRoots> subRootMAccessories = Global.CatBySubList;
            List<SubRoots> subRootMBages = new List<SubRoots>() { new SubRoots() { SubRoot = "Backpacks", Gender = "M", Root = "Men", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Belt Bags", Gender = "M", Root = "Men", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Briefcases", Gender = "M", Root = "Men", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Duffle Bags", Gender = "M", Root = "Men", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Laptop Bags", Gender = "M", Root = "Men", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Luggage & Travel Bags", Gender = "M", Root = "Men", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Messenger Bags", Gender = "M", Root = "Men", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Toiletry Bags", Gender = "M", Root = "Men", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Wallets", Gender = "M", Root = "Men", NodeTitle = "Bags" } };
            if (Global.Storecategory == Constant.StreetwearStr)
            {
                //Global.CatBySubList = new List<SubRoots>() { new SubRoots() { SubRoot = "Active", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Blazers", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Bombers", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Capes", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Denim & Utility", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Down & Puffers", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Fleece", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Fur & Faux Fur", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Leather & Faux Leather", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Rain Coats", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Ski", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Sport Coats", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Trench Coats", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Varsity", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Vests", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Wool & Pea Coats ", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" } };
                Global.CatBySubList = new List<SubRoots>() { new SubRoots() { SubRoot = "Active", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Blazers", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Bombers", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Capes", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Denim & Utility", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Down & Puffers", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Fleece", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Fur & Faux Fur", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Leather & Faux Leather", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Rain Coats", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Ski", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Trench Coats", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Varsity", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Vests", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Wool & Pea Coats ", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" } };
            }
            else
            {
                //Global.CatBySubList = new List<SubRoots>() { new SubRoots() { SubRoot = "Active", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Blazers", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Bombers", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Capes", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Denim & Utility", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Down & Puffers", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Fleece", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Fur & Faux Fur", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Leather & Faux Leather", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Rain Coats", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Ski", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Sport Coats", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Trench Coats", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Varsity", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Vests", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Windbreakers", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Wool & Pea Coats ", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" } };
                Global.CatBySubList = new List<SubRoots>() { new SubRoots() { SubRoot = "Active", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Blazers", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Bombers", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Capes", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Denim & Utility", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Down & Puffers", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Fleece", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Fur & Faux Fur", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Leather & Faux Leather", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Rain Coats", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Ski", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Trench Coats", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Varsity", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Vests", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Windbreakers", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Wool & Pea Coats ", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" } };
            }
            List<SubRoots> subRootMJacketsCoats = Global.CatBySubList;
            List<SubRoots> subRootMJeans = new List<SubRoots>() { new SubRoots() { SubRoot = "Bootcut", Gender = "M", Root = "Men", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Relaxed", Gender = "M", Root = "Men", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Skinny", Gender = "M", Root = "Men", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Slim", Gender = "M", Root = "Men", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Straight", Gender = "M", Root = "Men", NodeTitle = "Jeans" } };
            List<SubRoots> subRootMPants = new List<SubRoots>() { new SubRoots() { SubRoot = "Cargo", Gender = "M", Root = "Men", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "Chinos & Khakis", Gender = "M", Root = "Men", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "Corduroy", Gender = "M", Root = "Men", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "Dress", Gender = "M", Root = "Men", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "Leggings", Gender = "M", Root = "Men", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "Overalls & Jumpsuits", Gender = "M", Root = "Men", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "Sweatpants & Joggers", Gender = "M", Root = "Men", NodeTitle = "Pants" } };
            if (Global.Storecategory == Constant.StreetwearStr)
            {
                Global.CatBySubList = new List<SubRoots>() { new SubRoots() { SubRoot = "Casual Button Downs", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Dress Shirts", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Jerseys", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Polos", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Sweatshirts & Hoodies", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Tank Tops", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Tees: Long Sleeve", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Tees: Short Sleeve", Gender = "M", Root = "Men", NodeTitle = "Shirts" } };
            }
            else if (Global.Storecategory != Constant.VintageStr)
            {

                Global.CatBySubList = new List<SubRoots>() { new SubRoots() { SubRoot = "Casual Button Downs", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Dress Shirts", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Jerseys", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Polos", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Sweatshirts & Hoodies", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Tank Tops", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Tees: Long Sleeve", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Tees: Short Sleeve", Gender = "M", Root = "Men", NodeTitle = "Shirts" } };
            }
            if (Global.Storecategory == Constant.VintageStr)
            {
                Global.CatBySubList = new List<SubRoots>() { new SubRoots() { SubRoot = "Casual Button Downs", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Dress Shirts", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Jerseys", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Polos", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Sweatshirts & Hoodies", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Tank Tops", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Tees: Long Sleeve", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Tees: Short Sleeve", Gender = "M", Root = "Men", NodeTitle = "Shirts" } };
            }
            else if (Global.Storecategory != Constant.StreetwearStr)
            {

                Global.CatBySubList = new List<SubRoots>() { new SubRoots() { SubRoot = "Casual Button Downs", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Dress Shirts", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Jerseys", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Polos", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Sweatshirts & Hoodies", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Tank Tops", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Tees: Long Sleeve", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Tees: Short Sleeve", Gender = "M", Root = "Men", NodeTitle = "Shirts" } };
            }
            List<SubRoots> subRootMShirts = Global.CatBySubList;
            List<SubRoots> subRootMShoes = new List<SubRoots>() { new SubRoots() { SubRoot = "Boat Shoes", Gender = "M", Root = "Men", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Boots", Gender = "M", Root = "Men", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Casual Shoes", Gender = "M", Root = "Men", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Dress Shoes", Gender = "M", Root = "Men", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Loafers & Drivers", Gender = "M", Root = "Men", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Sandals & Flip Flops", Gender = "M", Root = "Men", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Slippers", Gender = "M", Root = "Men", NodeTitle = "Shoes" } };
            List<SubRoots> subRootMShorts = new List<SubRoots>() { new SubRoots() { SubRoot = "Athletic", Gender = "M", Root = "Men", NodeTitle = "Shorts" }, new SubRoots() { SubRoot = "Cargo", Gender = "M", Root = "Men", NodeTitle = "Shorts" }, new SubRoots() { SubRoot = "Chino", Gender = "M", Root = "Men", NodeTitle = "Shorts" }, new SubRoots() { SubRoot = "Denim", Gender = "M", Root = "Men", NodeTitle = "Shorts" }, new SubRoots() { SubRoot = "Hybrids", Gender = "M", Root = "Men", NodeTitle = "Shorts" } };
            List<SubRoots> subRootMSuitsTuxedos = new List<SubRoots>() { };
            if (Global.Storecategory == Constant.VintageStr)
            {
                Global.CatBySubList = new List<SubRoots>() { new SubRoots() { SubRoot = "Cardigans", Gender = "M", Root = "Men", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Cashmere", Gender = "M", Root = "Men", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Hoodie", Gender = "M", Root = "Men", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Turtlenecks", Gender = "M", Root = "Men", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Vest", Gender = "M", Root = "Men", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "V-Neck", Gender = "M", Root = "Men", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Zip-Up", Gender = "M", Root = "Men", NodeTitle = "Sweaters" } };
            }
            else
            {
                Global.CatBySubList = new List<SubRoots>() { new SubRoots() { SubRoot = "Cardigans", Gender = "M", Root = "Men", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Cashmere", Gender = "M", Root = "Men", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Crewnecks", Gender = "M", Root = "Men", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Hoodie", Gender = "M", Root = "Men", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Turtlenecks", Gender = "M", Root = "Men", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Vest", Gender = "M", Root = "Men", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "V-Neck", Gender = "M", Root = "Men", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Zip-Up", Gender = "M", Root = "Men", NodeTitle = "Sweaters" } };
            }
            List<SubRoots> subRootMSweaters = Global.CatBySubList;
            List<SubRoots> subRootMSocksandUnderwear = new List<SubRoots>() { new SubRoots() { SubRoot = "Athletic Socks", Gender = "M", Root = "Men", NodeTitle = "Socks & Underwear" }, new SubRoots() { SubRoot = "Boxer Briefs", Gender = "M", Root = "Men", NodeTitle = "Socks & Underwear" }, new SubRoots() { SubRoot = "Boxers", Gender = "M", Root = "Men", NodeTitle = "Socks & Underwear" }, new SubRoots() { SubRoot = "Briefs", Gender = "M", Root = "Men", NodeTitle = "Socks & Underwear" }, new SubRoots() { SubRoot = "Casual Socks", Gender = "M", Root = "Men", NodeTitle = "Socks & Underwear" }, new SubRoots() { SubRoot = "Dress Socks", Gender = "M", Root = "Men", NodeTitle = "Socks & Underwear" }, new SubRoots() { SubRoot = "Undershirts", Gender = "M", Root = "Men", NodeTitle = "Socks & Underwear" } };
            List<SubRoots> subRootMSwim = new List<SubRoots>() { new SubRoots() { SubRoot = "Board Shorts", Gender = "M", Root = "Men", NodeTitle = "Swim" }, new SubRoots() { SubRoot = "Hybrids", Gender = "M", Root = "Men", NodeTitle = "Swim" }, new SubRoots() { SubRoot = "Rash Guard", Gender = "M", Root = "Men", NodeTitle = "Swim" }, new SubRoots() { SubRoot = "Swim Trunks", Gender = "M", Root = "Men", NodeTitle = "Swim" } };
            List<SubRoots> subRootMOtherClothing = new List<SubRoots>() { };

            Dictionary<Nodes, List<SubRoots>> nodeMen = new Dictionary<Nodes, List<SubRoots>>();

            nodeMen.Add(new Nodes() { NodeTitle = "Accessories", IsShowMore = true, Root = "Men" }, subRootMAccessories);
            nodeMen.Add(new Nodes() { NodeTitle = "Bags", IsShowMore = true, Root = "Men" }, subRootMBages);
            nodeMen.Add(new Nodes() { NodeTitle = "Jackets & Coats", IsShowMore = true, Root = "Men" }, subRootMJacketsCoats);
            nodeMen.Add(new Nodes() { NodeTitle = "Jeans", IsShowMore = true, Root = "Men" }, subRootMJeans);
            nodeMen.Add(new Nodes() { NodeTitle = "Pants", IsShowMore = true, Root = "Men" }, subRootMPants);
            nodeMen.Add(new Nodes() { NodeTitle = "Shirts", IsShowMore = true, Root = "Men" }, subRootMShirts);
            if (Global.SearchedResultSelectedStore.ToLower() == Constant.ClothingStr.ToLower() || Global.SearchedResultSelectedStore.ToLower() == Constant.VintageStr.ToLower())
            {
                nodeMen.Add(new Nodes() { NodeTitle = "Shoes", IsShowMore = true, Root = "Men" }, subRootMShoes);
            }
            nodeMen.Add(new Nodes() { NodeTitle = "Shorts", IsShowMore = true, Root = "Men" }, subRootMShorts);
            if (Global.Storecategory == Constant.VintageStr)
            {
                //nodeMen.Add(new Nodes() { IsShowMore = false}, subRootMSocksandUnderwear);
            }
            else
            {
                nodeMen.Add(new Nodes() { NodeTitle = "Socks & Underwear", IsShowMore = true, Root = "Men" }, subRootMSocksandUnderwear);
            }
            nodeMen.Add(new Nodes() { NodeTitle = "Suits & Tuxedos", IsShowMore = false, Root = "Men" }, subRootMSuitsTuxedos);
            nodeMen.Add(new Nodes() { NodeTitle = "Sweaters", IsShowMore = true, Root = "Men" }, subRootMSweaters);
            nodeMen.Add(new Nodes() { NodeTitle = "Swim", IsShowMore = true, Root = "Men" }, subRootMSwim);
            nodeMen.Add(new Nodes() { NodeTitle = "Other", IsShowMore = false, Root = "Men" }, subRootMOtherClothing);
            #endregion

            roots = new List<Roots>()
            {
                new Roots(){ Root="Women", Node=nodeW},
                new Roots(){ Root="Men", Node=nodeMen},
            };

            if (selectedsubcat != null && selectedcat != null)
            {
                if(selectedsubcat.ToLower().Contains("COATS".ToLower()))
                {
                    selectedsubcat = "JACKETS & COATS";
                }
                if (selectedsubcat.ToLower().Contains("SUITS".ToLower()))
                {
                    selectedsubcat = "SUITS & TUXEDOS";
                }
                if (selectedsubcat.ToLower().Contains("SOCKS".ToLower()))
                {
                    selectedsubcat = "socks & underwear";
                }
                if (selectedsubcat.ToLower().Contains("Swim".ToLower()) && selectedcat.Contains("Women"))
                {
                    selectedsubcat = "Swimsuits";
                }
                var selectedroot = roots.Where(x => x.Root.ToLower() == selectedcat.ToLower()).ToList();
                var subRootsList = selectedroot.Select(r => r.Node.Keys.Where(k => k.NodeTitle.ToLower() == selectedsubcat.ToLower()).FirstOrDefault()).FirstOrDefault();
                var res = selectedroot.Select(x => x.Node[subRootsList]).ToList().FirstOrDefault();

                List<SubRoots> subRoots = new List<SubRoots>();
                Dictionary<Nodes, List<SubRoots>> SubnodeDicn = new Dictionary<Nodes, List<SubRoots>>();
                foreach (var item in res)
                {
                    var node = new Nodes()
                    {
                        IsShowMore = item.IsShowMore,
                        NodeTitle = item.SubRoot,
                        Gender = item.Gender
                    };
                    SubnodeDicn.Add(node, subRoots);
                }

                List<Roots> rootsa = new List<Roots>
                {
                    new Roots()
                    {
                        Root = selectedsubcat,
                        Node = SubnodeDicn
                    }
                };
                return Global.RootsList = rootsa;
            }
            else if (selectedcat != null)
            {
                return Global.RootsList = roots.Where(x => x.Root.ToLower() == selectedcat.ToLower()).ToList();
            }
            else
            {
                Global.RootsList = roots;
            }
            return roots;
        }
        #endregion

        #region GetSneakesCatList (from filter) method
        //Method  specially created for custom filter category list because category list items will show based on selected category and sub category for sneakers store
        public List<Roots> GetSneakesCatList(string selectedcat, string selectedsubcat)
        {
            List<Roots> roots;

            Dictionary<Nodes, List<SubRoots>> nodeW = new Dictionary<Nodes, List<SubRoots>>();
            Dictionary<Nodes, List<SubRoots>> nodeMen = new Dictionary<Nodes, List<SubRoots>>();

            // Categories list for Women
            #region Category list for Women
            //List<SubRoots> subRootWSneakers = new List<SubRoots>() { new SubRoots() { SubRoot = "Basketball", Gender = "F", Root = "Women", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Casual", Gender = "F", Root = "Women", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Luxury", Gender = "F", Root = "Women", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Running", Gender = "F", Root = "Women", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Soccer", Gender = "F", Root = "Women", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Skateboard", Gender = "F", Root = "Women", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Slides", Gender = "F", Root = "Women", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Tennis", Gender = "F", Root = "Women", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Track & Field", Gender = "F", Root = "Women", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Training", Gender = "F", Root = "Women", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Walking", Gender = "F", Root = "Women", NodeTitle = "Sneakers" } };

            //nodeW.Add(new Nodes() { NodeTitle = "Sneakers", Gender = "F", IsShowMore = true, Root = "Women" }, subRootWSneakers);

            List<SubRoots> subRootWBasketball = new List<SubRoots>() { };
            List<SubRoots> subRootWCasual = new List<SubRoots>() { };
            List<SubRoots> subRootWLuxury = new List<SubRoots>() { };
            List<SubRoots> subRootWRunning = new List<SubRoots>() { };
            List<SubRoots> subRootWSoccer = new List<SubRoots>() { };
            List<SubRoots> subRootWSkateboard = new List<SubRoots>() { };
            List<SubRoots> subRootWSlides = new List<SubRoots>() { };
            List<SubRoots> subRootWTennis = new List<SubRoots>() { };
            List<SubRoots> subRootWTrackField = new List<SubRoots>() { };
            List<SubRoots> subRootWTraining = new List<SubRoots>() { };
            List<SubRoots> subRootWWalking = new List<SubRoots>() { };

            nodeW.Add(new Nodes() { NodeTitle = "Basketball", Gender = "F", IsShowMore = false, Root = "Women" }, subRootWBasketball);
            nodeW.Add(new Nodes() { NodeTitle = "Casual", Gender = "F", IsShowMore = false, Root = "Women" }, subRootWCasual);
            nodeW.Add(new Nodes() { NodeTitle = "Luxury", Gender = "F", IsShowMore = false, Root = "Women" }, subRootWLuxury);
            nodeW.Add(new Nodes() { NodeTitle = "Running", Gender = "F", IsShowMore = false, Root = "Women" }, subRootWRunning);
            nodeW.Add(new Nodes() { NodeTitle = "Soccer", Gender = "F", IsShowMore = false, Root = "Women" }, subRootWSoccer);
            nodeW.Add(new Nodes() { NodeTitle = "Skateboard", Gender = "F", IsShowMore = false, Root = "Women" }, subRootWSkateboard);
            nodeW.Add(new Nodes() { NodeTitle = "Slides", Gender = "F", IsShowMore = false, Root = "Women" }, subRootWSlides);
            nodeW.Add(new Nodes() { NodeTitle = "Tennis", Gender = "F", IsShowMore = false, Root = "Women" }, subRootWTennis);
            nodeW.Add(new Nodes() { NodeTitle = "Track & Field", Gender = "F", IsShowMore = false, Root = "Women" }, subRootWTrackField);
            nodeW.Add(new Nodes() { NodeTitle = "Training", Gender = "F", IsShowMore = false, Root = "Women" }, subRootWTraining);
            nodeW.Add(new Nodes() { NodeTitle = "Walking", Gender = "F", IsShowMore = false, Root = "Women" }, subRootWWalking);
            #endregion

            // Categories list for Men
            #region Category list for Men
            //List<SubRoots> subRootMSneakers = new List<SubRoots>() { new SubRoots() { SubRoot = "Basketball", Gender = "M", Root = "Men", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Casual", Gender = "M", Root = "Men", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Luxury", Gender = "M", Root = "Men", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Running", Gender = "M", Root = "Men", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Soccer", Gender = "M", Root = "Men", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Skateboard", Gender = "M", Root = "Men", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Slides", Gender = "M", Root = "Men", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Tennis", Gender = "M", Root = "Men", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Track & Field", Gender = "M", Root = "Men", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Training", Gender = "M", Root = "Men", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Walking", Gender = "M", Root = "Men", NodeTitle = "Sneakers" } };

            //nodeMen.Add(new Nodes() { NodeTitle = "Sneakers", Gender = "M", IsShowMore = true, Root = "Men" }, subRootMSneakers);

            List<SubRoots> subRootMBasketball = new List<SubRoots>() { };
            List<SubRoots> subRootMCasual = new List<SubRoots>() { };
            List<SubRoots> subRootMLuxury = new List<SubRoots>() { };
            List<SubRoots> subRootMRunning = new List<SubRoots>() { };
            List<SubRoots> subRootMSoccer = new List<SubRoots>() { };
            List<SubRoots> subRootMSkateboard = new List<SubRoots>() { };
            List<SubRoots> subRootMSlides = new List<SubRoots>() { };
            List<SubRoots> subRootMTennis = new List<SubRoots>() { };
            List<SubRoots> subRootMTrackField = new List<SubRoots>() { };
            List<SubRoots> subRootMTraining = new List<SubRoots>() { };
            List<SubRoots> subRootMWalking = new List<SubRoots>() { };

            nodeMen.Add(new Nodes() { NodeTitle = "Basketball", Gender = "M", IsShowMore = false, Root = "Men" }, subRootMBasketball);
            nodeMen.Add(new Nodes() { NodeTitle = "Casual", Gender = "M", IsShowMore = false, Root = "Men" }, subRootMCasual);
            nodeMen.Add(new Nodes() { NodeTitle = "Luxury", Gender = "M", IsShowMore = false, Root = "Men" }, subRootMLuxury);
            nodeMen.Add(new Nodes() { NodeTitle = "Running", Gender = "M", IsShowMore = false, Root = "Men" }, subRootMRunning);
            nodeMen.Add(new Nodes() { NodeTitle = "Soccer", Gender = "M", IsShowMore = false, Root = "Men" }, subRootMSoccer);
            nodeMen.Add(new Nodes() { NodeTitle = "Skateboard", Gender = "M", IsShowMore = false, Root = "Men" }, subRootMSkateboard);
            nodeMen.Add(new Nodes() { NodeTitle = "Slides", Gender = "M", IsShowMore = false, Root = "Men" }, subRootMSlides);
            nodeMen.Add(new Nodes() { NodeTitle = "Tennis", Gender = "M", IsShowMore = false, Root = "Men" }, subRootMTennis);
            nodeMen.Add(new Nodes() { NodeTitle = "Track & Field", Gender = "M", IsShowMore = false, Root = "Men" }, subRootMTrackField);
            nodeMen.Add(new Nodes() { NodeTitle = "Training", Gender = "M", Root = "Men", IsShowMore = false }, subRootMTraining);
            nodeMen.Add(new Nodes() { NodeTitle = "Walking", Gender = "M", Root = "Men", IsShowMore = false }, subRootMWalking);
            #endregion

            roots = new List<Roots>()
            {
                new Roots(){ Root="Women", Node=nodeW},
                new Roots(){ Root="Men", Node=nodeMen}
            };

            if (selectedsubcat != null)
            {
                var selectedroot = roots.Where(x => x.Root.ToLower() == selectedcat.ToLower()).ToList();
                var subRootsList = selectedroot.Select(r => r.Node.Keys.Where(k => k.NodeTitle.ToLower() == selectedsubcat.ToLower()).FirstOrDefault()).FirstOrDefault();
                var res = selectedroot.Select(x => x.Node[subRootsList]).ToList().FirstOrDefault();

                List<SubRoots> subRoots = new List<SubRoots>();
                Dictionary<Nodes, List<SubRoots>> SubnodeDicn = new Dictionary<Nodes, List<SubRoots>>();
                foreach (var item in res)
                {
                    var node = new Nodes()
                    {
                        IsShowMore = item.IsShowMore,
                        NodeTitle = item.SubRoot,
                        Gender = item.Gender
                    };
                    SubnodeDicn.Add(node, subRoots);
                }
                List<Roots> rootsa = new List<Roots>
                {
                    new Roots()
                    {
                        Root = selectedsubcat,
                        Node = SubnodeDicn
                    }
                };
                return Global.SneakersRootsList = rootsa;
            }
            else if (selectedcat != null)
            {
                return Global.SneakersRootsList = roots.Where(x => x.Root.ToLower() == selectedcat.ToLower()).ToList();
            }
            else
            {
                Global.SneakersRootsList = roots;
            }

            Global.SneakersRootsList = roots;
            return roots;
        }
        #endregion

        #region SneakersSelCusCommonSizeMethod Method
        // Common method for all Sneakers sizes
        public ObservableCollection<string> SneakersSelCusCommonSizeMethod(SubRoots subRoots)
        {
            ObservableCollection<string> SizeList = new ObservableCollection<string>();
            string keyNodes = string.Empty;
            SubRoots SneakSubRoots = null;
            string gender = "men";
            if (subRoots.Gender.ToUpper() == "F" || subRoots.Gender.ToUpper() == "WOMEN")
            {
                gender = "women";
            }
            var node = Global.SneakersRootsList.Where(x => x.Root.ToLower() == gender.ToLower()).ToList().FirstOrDefault();
            Dictionary<Nodes, List<SubRoots>> dic = node.Node;
            foreach (var keyNode in dic)
            {
                var objSubRool = keyNode.Key.Gender;
                if (objSubRool != null)
                {
                    keyNodes = keyNode.Key.NodeTitle;
                    break;
                }
            }
            switch (keyNodes)
            {
                case "Basketball":
                    SizeList = new ObservableCollection<string>() { "4", "4.5", "5", "5.5", "6", "6.5", "7", "7.5", "8", "8.5", "9", "9.5", "10", "10.5", "11", "11.5", "12", "12.5", "13", "13.5", "14", "14.5", "15", "15.5", "16" };
                    break;

                case "Casual":
                    SizeList = new ObservableCollection<string>() { "4", "4.5", "5", "5.5", "6", "6.5", "7", "7.5", "8", "8.5", "9", "9.5", "10", "10.5", "11", "11.5", "12", "12.5", "13", "13.5", "14", "14.5", "15", "15.5", "16" };
                    break;

                case "Luxury":
                    SizeList = new ObservableCollection<string>() { "4", "4.5", "5", "5.5", "6", "6.5", "7", "7.5", "8", "8.5", "9", "9.5", "10", "10.5", "11", "11.5", "12", "12.5", "13", "13.5", "14", "14.5", "15", "15.5", "16" };
                    break;

                case "Running":
                    SizeList = new ObservableCollection<string>() { "4", "4.5", "5", "5.5", "6", "6.5", "7", "7.5", "8", "8.5", "9", "9.5", "10", "10.5", "11", "11.5", "12", "12.5", "13", "13.5", "14", "14.5", "15", "15.5", "16" };
                    break;

                case "Soccer":
                    SizeList = new ObservableCollection<string>() { "4", "4.5", "5", "5.5", "6", "6.5", "7", "7.5", "8", "8.5", "9", "9.5", "10", "10.5", "11", "11.5", "12", "12.5", "13", "13.5", "14", "14.5", "15", "15.5", "16" };
                    break;

                case "Skateboard":
                    SizeList = new ObservableCollection<string>() { "4", "4.5", "5", "5.5", "6", "6.5", "7", "7.5", "8", "8.5", "9", "9.5", "10", "10.5", "11", "11.5", "12", "12.5", "13", "13.5", "14", "14.5", "15", "15.5", "16" };
                    break;

                case "Slides":
                    SizeList = new ObservableCollection<string>() { "4", "4.5", "5", "5.5", "6", "6.5", "7", "7.5", "8", "8.5", "9", "9.5", "10", "10.5", "11", "11.5", "12", "12.5", "13", "13.5", "14", "14.5", "15", "15.5", "16" };
                    break;

                case "Tennis":
                    SizeList = new ObservableCollection<string>() { "4", "4.5", "5", "5.5", "6", "6.5", "7", "7.5", "8", "8.5", "9", "9.5", "10", "10.5", "11", "11.5", "12", "12.5", "13", "13.5", "14", "14.5", "15", "15.5", "16" };
                    break;

                case "Track & Field":
                    SizeList = new ObservableCollection<string>() { "4", "4.5", "5", "5.5", "6", "6.5", "7", "7.5", "8", "8.5", "9", "9.5", "10", "10.5", "11", "11.5", "12", "12.5", "13", "13.5", "14", "14.5", "15", "15.5", "16" };
                    break;

                case "Training":
                    SizeList = new ObservableCollection<string>() { "4", "4.5", "5", "5.5", "6", "6.5", "7", "7.5", "8", "8.5", "9", "9.5", "10", "10.5", "11", "11.5", "12", "12.5", "13", "13.5", "14", "14.5", "15", "15.5", "16" };
                    break;

                case "Walking":
                    SizeList = new ObservableCollection<string>() { "4", "4.5", "5", "5.5", "6", "6.5", "7", "7.5", "8", "8.5", "9", "9.5", "10", "10.5", "11", "11.5", "12", "12.5", "13", "13.5", "14", "14.5", "15", "15.5", "16" };
                    break;
            }
            return SizeList;
        }
        #endregion

        #region AllCatSelCustomSizeMethod Method
        // Common method for All category sizes (Clothing, Streetwear and Vintage)
        public ObservableCollection<string> AllCatSelCustomSizeMethod(SubRoots subRoots)
        {
            ObservableCollection<string> SizeList = new ObservableCollection<string>();
            try
            {
                if (Global.RootsList != null)
                {
                    if (Global.RootsList.Count == 0)
                    {
                        GetCategoryList();
                    }
                }
                string keyNodes = string.Empty;
                string subKeyNodes = string.Empty;
                string gender = "men";
                if (subRoots.SubRoot != null && (subRoots.Gender.ToUpper() != "F" || subRoots.Gender.ToUpper() != "WOMEN"))
                {
                    if (subRoots.Gender.ToUpper() == "F" || subRoots.Gender.ToUpper() == "WOMEN")
                    {
                        gender = "women";
                    }
                    var node = Global.RootsList.Where(x => x.Root.ToLower() == gender.ToLower()).ToList().FirstOrDefault();
                    Dictionary<Nodes, List<SubRoots>> dic = node.Node;
                    foreach (var keyNode in dic)
                    {
                        var objSubRool = keyNode.Value.Where(s => s.SubRoot.ToLower().Trim() == subRoots.SubRoot.ToLower().Trim()).FirstOrDefault();
                        if (objSubRool != null)
                        {
                            keyNodes = keyNode.Key.NodeTitle;
                            subKeyNodes = subRoots.SubRoot;
                            break;
                        }
                    }
                }
                else if (subRoots.Root != null && subRoots.NodeTitle != null)
                {
                    subRoots.Gender = subRoots.Root.ToLower() != "men" ? "F" : "M";
                    keyNodes = subRoots.NodeTitle;
                    subKeyNodes = subRoots.SubRoot;
                }
                switch (keyNodes.ToLower())
                {
                    case "jackets & coats":
                        var jcSizeList = new ObservableCollection<string>() { "One Size", "Custom", "XXS", "XS", "S", "M", "L", "XL", "XXL", "XXXL", "0X", "1X", "2X", "3X", "4X", "5X", "00", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "14W", "15", "16", "16W", "17", "18", "18W", "20", "20W", "22", "22W", "24", "24W", "26", "26W", "28", "28W", "30", "30W", "32", "32W", "00P", "0P", "2P", "4P", "6P", "8P", "10P", "12P", "14P", "16P", "18P", "20P", "XXSP", "XSP", "SP", "MP", "LP", "XLP", "XXLP", "XS", "S", "M", "L", "XL", "XXL", "3XL", "34S", "36S", "38S", "40S", "42S", "44S", "46S", "48S", "50S", "52S", "54S", "56S", "34R", "36R", "38R", "40R", "42R", "44R", "46R", "48R", "50R", "52R", "54R", "56R", "34L", "36L", "38L", "40L", "42L", "44L", "46L", "48L", "50L", "52L", "54L", "56L" };
                        SizeList = new ObservableCollection<string>(jcSizeList.Distinct().ToList());
                        break;

                    case "coats":
                        var coatsSizeList = new ObservableCollection<string>() { "One Size", "Custom", "XXS", "XS", "S", "M", "L", "XL", "XXL", "XXXL", "0X", "1X", "2X", "3X", "4X", "5X", "00", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "14W", "15", "16", "16W", "17", "18", "18W", "20", "20W", "22", "22W", "24", "24W", "26", "26W", "28", "28W", "30", "30W", "32", "32W", "00P", "0P", "2P", "4P", "6P", "8P", "10P", "12P", "14P", "16P", "18P", "20P", "XXSP", "XSP", "SP", "MP", "LP", "XLP", "XXLP", "XS", "S", "M", "L", "XL", "XXL", "3XL", "34S", "36S", "38S", "40S", "42S", "44S", "46S", "48S", "50S", "52S", "54S", "56S", "34R", "36R", "38R", "40R", "42R", "44R", "46R", "48R", "50R", "52R", "54R", "56R", "34L", "36L", "38L", "40L", "42L", "44L", "46L", "48L", "50L", "52L", "54L", "56L" };
                        SizeList = new ObservableCollection<string>(coatsSizeList.Distinct().ToList());
                        break;

                    case "jeans":
                        var jeansSizeList = new ObservableCollection<string>() { "One Size", "Custom", "XXS", "XS", "S", "M", "L", "XL", "XXL", "XXXL", "0X", "1X", "2X", "3X", "4X", "5X", "00", "0", "2", "4", "6", "8", "10", "12", "14", "14W", "16", "16W", "18", "18W", "20", "20W", "22", "22W", "23", "24", "24W", "25", "26", "26W", "27", "28", "28W", "29", "30", "30W", "31", "32", "32W", "33", "34", "00P", "0P", "2P", "4P", "6P", "8P", "10P", "12P", "14P", "16P", "18P", "20P", "23P", "24P", "25P", "26P", "27P", "28P", "29P", "30P", "31P", "32P", "33P", "34P", "XXSP", "XSP", "SP", "MP", "LP", "XLP", "XXLP", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "42", "43", "44" };
                        SizeList = new ObservableCollection<string>(jeansSizeList.Distinct().ToList());
                        break;

                    case "pants":
                        var pantsSizeList = new ObservableCollection<string>() { "One Size", "Custom", "XXS", "XS", "S", "M", "L", "XL", "XXL", "XXXL", "0X", "1X", "2X", "3X", "4X", "5X", "00", "0", "2", "4", "6", "8", "10", "12", "14", "14W", "16", "16W", "18", "18W", "20", "20W", "22", "22W", "23", "24", "24W", "25", "26", "26W", "27", "28", "28W", "29", "30", "30W", "31", "32", "32W", "33", "34", "00P", "0P", "2P", "4P", "6P", "8P", "10P", "12P", "14P", "16P", "18P", "20P", "23P", "24P", "25P", "26P", "27P", "28P", "29P", "30P", "31P", "32P", "33P", "34P", "XXSP", "XSP", "SP", "MP", "LP", "XLP", "XXLP", "XS", "S", "M", "L", "XL", "XXL", "3XL", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "42", "43", "44" };
                        SizeList = new ObservableCollection<string>(pantsSizeList.Distinct().ToList());
                        break;

                    case "bottoms & pants":
                        var bpSizeList = new ObservableCollection<string>() { "One Size", "Custom", "XXS", "XS", "S", "M", "L", "XL", "XXL", "XXXL", "0X", "1X", "2X", "3X", "4X", "5X", "00", "0", "2", "4", "6", "8", "10", "12", "14", "14W", "16", "16W", "18", "18W", "20", "20W", "22", "22W", "23", "24", "24W", "25", "26", "26W", "27", "28", "28W", "29", "30", "30W", "31", "32", "32W", "33", "34", "00P", "0P", "2P", "4P", "6P", "8P", "10P", "12P", "14P", "16P", "18P", "20P", "23P", "24P", "25P", "26P", "27P", "28P", "29P", "30P", "31P", "32P", "33P", "34P", "XXSP", "XSP", "SP", "MP", "LP", "XLP", "XXLP", "XS", "S", "M", "L", "XL", "XXL", "3XL", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "42", "43", "44" };
                        SizeList = new ObservableCollection<string>(bpSizeList.Distinct().ToList());
                        break;

                    case "shirts":
                        SizeList = new ObservableCollection<string>() { "One Size", "Custom", "XS", "S", "M", "L", "XL", "XXL", "3XL", "14.5", "15", "15.5", "16", "16.5", "17", "17.5", "18", "18.5", "19", "19.5", "20" };
                        break;

                    case "shoes":
                        var shoesSizeList = new ObservableCollection<string>() { "One Size", "Custom", "5", "5.5", "6", "6.5", "7", "7.5", "8", "8.5", "9", "9.5", "10", "10.5", "11", "11.5", "12", "12.5", "13", "4", "4.5", "5", "5.5", "6", "6.5", "7", "7.5", "8", "8.5", "9", "9.5", "10", "10.5", "11", "11.5", "12", "12.5", "13", "13.5", "14", "14.5", "15", "15.5", "16" };
                        SizeList = new ObservableCollection<string>(shoesSizeList.Distinct().ToList());
                        break;

                    case "shorts":
                        var shortsSizeList = new ObservableCollection<string>() { "One Size", "Custom", "XXS", "XS", "S", "M", "L", "XL", "XXL", "XXXL", "0X", "1X", "2X", "3X", "4X", "5X", "00", "0", "2", "4", "6", "8", "10", "12", "14", "14W", "16", "16W", "18", "18W", "20", "20W", "22", "22W", "23", "24", "24W", "25", "26", "26W", "27", "28", "28W", "29", "30", "30W", "31", "32", "32W", "33", "34", "00P", "0P", "2P", "4P", "6P", "8P", "10P", "12P", "14P", "16P", "18P", "20P", "23P", "24P", "25P", "26P", "27P", "28P", "29P", "30P", "31P", "32P", "33P", "34P", "XXSP", "XSP", "SP", "MP", "LP", "XLP", "XXLP", "XS", "S", "M", "L", "XL", "XXL", "3XL", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "42", "43", "44" };
                        SizeList = new ObservableCollection<string>(shortsSizeList.Distinct().ToList());
                        break;

                    case "suits & tuxedos":
                        SizeList = new ObservableCollection<string>() { "One Size", "Custom", "34S", "34R", "34L", "35S", "35R", "35L", "36S", "36R", "36L", "37S", "37R", "37L", "38S", "38R", "38L", "39S", "39R", "39L", "40S", "40R", "40L", "41S", "41R", "41L", "42S", "42R", "42L", "43S", "43R", "43L", "44S", "44R", "44L", "45S", "45R", "45L", "46S", "46R", "46L", "47S", "47R", "47L", "48S", "48R", "48L", "49S", "49R", "49L", "50S", "50R", "50L", "51S", "51R", "51L", "52S", "52R", "52L", "53S", "53R", "53L", "54S", "54R", "54L", "55S", "55R", "55L", "56S", "56R", "56L" };
                        break;

                    case "suits":
                        var suitsSizeList = new ObservableCollection<string>() { "One Size", "Custom", "XXS", "XS", "S", "M", "L", "XL", "XXL", "XXXL", "0X", "1X", "2X", "3X", "4X", "5X", "00", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "14W", "15", "16", "16W", "17", "18", "18W", "20", "20W", "22", "22W", "24", "24W", "26", "26W", "28", "28W", "30", "30W", "32", "32W", "00P", "0P", "2P", "4P", "6P", "8P", "10P", "12P", "14P", "16P", "18P", "20P", "XXSP", "XSP", "SP", "MP", "LP", "XLP", "XXLP", "34S", "34R", "34L", "35S", "35R", "35L", "36S", "36R", "36L", "37S", "37R", "37L", "38S", "38R", "38L", "39S", "39R", "39L", "40S", "40R", "40L", "41S", "41R", "41L", "42S", "42R", "42L", "43S", "43R", "43L", "44S", "44R", "44L", "45S", "45R", "45L", "46S", "46R", "46L", "47S", "47R", "47L", "48S", "48R", "48L", "49S", "49R", "49L", "50S", "50R", "50L", "51S", "51R", "51L", "52S", "52R", "52L", "53S", "53R", "53L", "54S", "54R", "54L", "55S", "55R", "55L", "56S", "56R", "56L" };
                        SizeList = new ObservableCollection<string>(suitsSizeList.Distinct().ToList());
                        break;

                    case "sweaters":
                        var sweatersSizeList = new ObservableCollection<string>() { "One Size", "Custom", "XXS", "XS", "S", "M", "L", "XL", "XXL", "XXXL", "0X", "1X", "2X", "3X", "4X", "5X", "00", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "14W", "15", "16", "16W", "17", "18", "18W", "20", "20W", "22", "22W", "24", "24W", "26", "26W", "28", "28W", "30", "30W", "32", "32W", "00P", "0P", "2P", "4P", "6P", "8P", "10P", "12P", "14P", "16P", "18P", "20P", "XXSP", "XSP", "SP", "MP", "LP", "XLP", "XXLP", "XS", "S", "M", "L", "XL", "XXL", "3XL" };
                        SizeList = new ObservableCollection<string>(sweatersSizeList.Distinct().ToList());
                        break;

                    case "swim":
                        SizeList = new ObservableCollection<string>() { "One Size", "Custom", "XS", "S", "M", "L", "XL", "XXL", "3XL" };
                        break;

                    case "socks & underwear":
                        SizeList = new ObservableCollection<string>() { "One Size", "Custom", "XS", "S", "M", "L", "XL", "XXL", "3XL" };
                        break;

                    case "socks":
                        SizeList = new ObservableCollection<string>() { "One Size", "Custom", "XS", "S", "M", "L", "XL", "XXL", "3XL" };
                        break;

                    case "accessories":
                        SizeList = new ObservableCollection<string>() { "One Size", "Custom", "XXS", "XS", "S", "M", "L", "XL", "XXL", "0X", "1X", "2X", "3X", "4X", "5X" };
                        break;

                    case "bags":
                        SizeList = new ObservableCollection<string>() { "One Size", "Custom" };
                        break;

                    case "dresses":
                        SizeList = new ObservableCollection<string>() { "One Size", "Custom", "XXS", "XS", "S", "M", "L", "XL", "XXL", "XXXL", "0X", "1X", "2X", "3X", "4X", "5X", "00", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "14W", "15", "16", "16W", "17", "18", "18W", "20", "20W", "22", "22W", "24", "24W", "26", "26W", "28", "28W", "30", "30W", "32", "32W", "00P", "0P", "2P", "4P", "6P", "8P", "10P", "12P", "14P", "16P", "18P", "20P", "XXSP", "XSP", "SP", "MP", "LP", "XLP", "XXLP" };
                        break;

                    case "intimates & sleepwear":
                        SizeList = new ObservableCollection<string>() { "One Size", "Custom", "XXS", "XS", "S", "M", "L", "XL", "30A", "30B", "32AA", "32A", "32B", "32C", "32D", "32E(DD)", "32F(DDD)", "32G(4D)", "32H(5D)", "34AA", "34A", "34B", "34C", "34D", "34E(DD)", "34F(DDD)", "34G(4D)", "34H(5D)", "36AA", "36A", "36B", "36C", "36D", "36E(DD)", "36F(DDD)", "36G(4D)", "36H(5D)", "38AA", "38A", "38B", "38C", "38D", "38E(DD)", "38F(DDD)", "38G(4D)", "38H(5D)", "40AA", "40A", "40B", "40C", "40D", "40E(DD)", "40F(DDD)", "40G(4D)", "40H(5D)", "42AA", "42A", "42B", "42C", "42D", "42E(DD)", "42F(DDD)", "42G(4D)", "42H(5D)", "44AA", "44A", "44B", "44C", "44D", "44E(DD)", "44F(DDD)", "44G(4D)", "44H(5D)", "46AA", "46A", "46B", "46C", "46D", "46E(DD)", "46F(DDD)", "46G(4D)", "46H(5D)", "48AA", "48A", "48B", "48C", "48D", "48E(DD)", "48F(DDD)", "48G(4D)", "48H(5D)" };
                        break;

                    case "jewelry":
                        SizeList = new ObservableCollection<string>() { "One Size", "Custom" };
                        break;

                    case "skirts":
                        SizeList = new ObservableCollection<string>() { "One Size", "Custom", "XXS", "XS", "S", "M", "L", "XL", "XXL", "XXXL", "0X", "1X", "2X", "3X", "4X", "5X", "00", "0", "2", "4", "6", "8", "10", "12", "14", "14W", "16", "16W", "18", "18W", "20", "20W", "22", "22W", "23", "24", "24W", "25", "26", "26W", "27", "28", "28W", "29", "30", "30W", "31", "32", "32W", "33", "34", "00P", "0P", "2P", "4P", "6P", "8P", "10P", "12P", "14P", "16P", "18P", "20P", "23P", "24P", "25P", "26P", "27P", "28P", "29P", "30P", "31P", "32P", "33P", "34P", "XXSP", "XSP", "SP", "MP", "LP", "XLP", "XXLP" };
                        break;

                    case "suits & separates":
                        SizeList = new ObservableCollection<string>() { "One Size", "Custom", "XXS", "XS", "S", "M", "L", "XL", "XXL", "XXXL", "0X", "1X", "2X", "3X", "4X", "5X", "00", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "14W", "15", "16", "16W", "17", "18", "18W", "20", "20W", "22", "22W", "24", "24W", "26", "26W", "28", "28W", "30", "30W", "32", "32W", "00P", "0P", "2P", "4P", "6P", "8P", "10P", "12P", "14P", "16P", "18P", "20P", "XXSP", "XSP", "SP", "MP", "LP", "XLP", "XXLP" };
                        break;


                    case "swimsuits":
                        SizeList = new ObservableCollection<string>() { "One Size", "Custom", "XXS", "XS", "S", "M", "L", "XL", "XXL", "XXXL", "0X", "1X", "2X", "3X", "4X", "5X", "00", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "14W", "15", "16", "16W", "17", "18", "18W", "20", "20W", "22", "22W", "24", "24W", "26", "26W", "28", "28W", "30", "30W", "32", "32W", "00P", "0P", "2P", "4P", "6P", "8P", "10P", "12P", "14P", "16P", "18P", "20P", "XXSP", "XSP", "SP", "MP", "LP", "XLP", "XXLP" };
                        break;

                    case "tops":
                        SizeList = new ObservableCollection<string>() { "One Size", "Custom", "XXS", "XS", "S", "M", "L", "XL", "XXL", "XXXL", "0X", "1X", "2X", "3X", "4X", "5X", "00", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "14W", "15", "16", "16W", "17", "18", "18W", "20", "20W", "22", "22W", "24", "24W", "26", "26W", "28", "28W", "30", "30W", "32", "32W", "00P", "0P", "2P", "4P", "6P", "8P", "10P", "12P", "14P", "16P", "18P", "20P", "XXSP", "XSP", "SP", "MP", "LP", "XLP", "XXLP" };
                        break;

                    case "other":
                        SizeList = new ObservableCollection<string>() { "One Size", "Custom" };
                        break;
                }
                switch (subKeyNodes)
                {
                    case "belts":
                        SizeList = new ObservableCollection<string>() { "One Size", "Custom", "XS", "S", "M", "L", "XL", "XXL", "3XL", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44" };
                        break;

                    case "hats":
                        SizeList = new ObservableCollection<string>() { "One Size", "Custom", "XS", "S", "M", "L", "XL", "XXL", "3XL" };
                        break;

                    case "face masks":
                        SizeList = new ObservableCollection<string>() { "One Size", "Custom", "XS", "S", "M", "L", "XL", "XXL", "3XL" };
                        break;

                    case "gloves & scarves":
                        SizeList = new ObservableCollection<string>() { "One Size", "Custom", "XS", "S", "M", "L", "XL", "XXL", "3XL" };
                        break;

                    case "pajamas & robes":
                        SizeList = new ObservableCollection<string>() { "One Size", "Custom", "XS", "S", "M", "L", "XL", "XXL", "3XL" };
                        break;
                }
                return SizeList;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return SizeList;
        }
        #endregion

        #region Common Method created to show all categories inside All stores (Clothing, Sneakers, Streetwear, Vintage)

        public List<Roots> GetCategoryListForAllStores(string selectedcat, string selectedsubcat)
        {
            List<Roots> roots;
            List<SubRoots> subRootWAccessories = new List<SubRoots>() { new SubRoots() { SubRoot = "Belts", Gender = "F", Root = "Women", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Electronics Cases", Gender = "F", Root = "Women", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Face Masks", Gender = "F", Root = "Women", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Glasses", Gender = "F", Root = "Women", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Gloves & Mittens", Gender = "F", Root = "Women", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Hair Accessories", Gender = "F", Root = "Women", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Hats", Gender = "F", Root = "Women", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Hosiery", Gender = "F", Root = "Women", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Scarves & Wraps", Gender = "F", Root = "Women", NodeTitle = "Accessories" }, 
                //new SubRoots() { SubRoot = "Socks", Gender = "F", Root = "Women", NodeTitle = "Accessories" }, 
                new SubRoots() { SubRoot = "Sunglasses", Gender = "F", Root = "Women", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Umbrellas", Gender = "F", Root = "Women", NodeTitle = "Accessories" }, 
                //new SubRoots() { SubRoot = "Watches", Gender = "F", Root = "Women", NodeTitle = "Accessories" } 
            };
            List<SubRoots> subRootWBages = new List<SubRoots>() { new SubRoots() { SubRoot = "Baby Bags", Gender = "F", Root = "Women", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Backpacks", Gender = "F", Root = "Women", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Belt Bags", Gender = "F", Root = "Women", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Clutches & Wristlets", Gender = "F", Root = "Women", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Cosmetic Bags", Gender = "F", Root = "Women", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Crossbody Bags", Gender = "F", Root = "Women", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Hobos", Gender = "F", Root = "Women", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Laptop Bags", Gender = "F", Root = "Women", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Luggage & Travel Bags", Gender = "F", Root = "Women", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Satchels", Gender = "F", Root = "Women", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Shoulder Bags", Gender = "F", Root = "Women", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Totes", Gender = "F", Root = "Women", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Wallets", Gender = "F", Root = "Women", NodeTitle = "Bags" } };
            List<SubRoots> subRootWDresses = new List<SubRoots>() { new SubRoots() { SubRoot = "Asymmetrical", Gender = "F", Root = "Women", NodeTitle = "Dresses" }, new SubRoots() { SubRoot = "High Low", Gender = "F", Root = "Women", NodeTitle = "Dresses" }, new SubRoots() { SubRoot = "Long Sleeve", Gender = "F", Root = "Women", NodeTitle = "Dresses" }, new SubRoots() { SubRoot = "Maxi", Gender = "F", Root = "Women", NodeTitle = "Dresses" }, new SubRoots() { SubRoot = "Midi", Gender = "F", Root = "Women", NodeTitle = "Dresses" }, new SubRoots() { SubRoot = "Mini", Gender = "F", Root = "Women", NodeTitle = "Dresses" }, new SubRoots() { SubRoot = "Prom", Gender = "F", Root = "Women", NodeTitle = "Dresses" }, new SubRoots() { SubRoot = "Rompers", Gender = "F", Root = "Women", NodeTitle = "Dresses" }, new SubRoots() { SubRoot = "Strapless", Gender = "F", Root = "Women", NodeTitle = "Dresses" }, new SubRoots() { SubRoot = "Wedding", Gender = "F", Root = "Women", NodeTitle = "Dresses" } };
            List<SubRoots> subRootWIntimatesSleepwear = new List<SubRoots>() { new SubRoots() { SubRoot = "Bras", Gender = "F", Root = "Women", NodeTitle = "Intimates & Sleepwear" }, new SubRoots() { SubRoot = "Chemises & Slips", Gender = "F", Root = "Women", NodeTitle = "Intimates & Sleepwear" }, new SubRoots() { SubRoot = "Pajamas", Gender = "F", Root = "Women", NodeTitle = "Intimates & Sleepwear" }, new SubRoots() { SubRoot = "Panties", Gender = "F", Root = "Women", NodeTitle = "Intimates & Sleepwear" }, new SubRoots() { SubRoot = "Robes", Gender = "F", Root = "Women", NodeTitle = "Intimates & Sleepwear" }, new SubRoots() { SubRoot = "Shapewear", Gender = "F", Root = "Women", NodeTitle = "Intimates & Sleepwear" } };
            List<SubRoots> subRootWJacketsCoats = new List<SubRoots>() { new SubRoots() { SubRoot = "Active", Gender = "F", Root = "Women", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Blazers", Gender = "F", Root = "Women", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Bombers", Gender = "F", Root = "Women", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Capes & Ponchos", Gender = "F", Root = "Women", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Denim & Utility", Gender = "F", Root = "Women", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Down & Puffers", Gender = "F", Root = "Women", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Fleece", Gender = "F", Root = "Women", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Fur & Faux Fur", Gender = "F", Root = "Women", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Leather & Faux Leather", Gender = "F", Root = "Women", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Rain Coats", Gender = "F", Root = "Women", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Ski", Gender = "F", Root = "Women", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Teddy", Gender = "F", Root = "Women", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Trench Coats", Gender = "F", Root = "Women", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Varsity", Gender = "F", Root = "Women", NodeTitle = "Jackets & Coats" },new SubRoots() { SubRoot = "Vests", Gender = "F", Root = "Women", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Windbreakers", Gender = "F", Root = "Women", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Wool & Pea Coats", Gender = "F", Root = "Women", NodeTitle = "Jackets & Coats" } };
            if (Global.Storecategory == Constant.ClothingStr)
            {
                Global.CatBySubList = new List<SubRoots>() { new SubRoots() { SubRoot = "Bootcut", Gender = "F", Root = "Women", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Crooped & Ankle", Gender = "F", Root = "Women", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Flare & Wide Leg", Gender = "F", Root = "Women", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "High Rise", Gender = "F", Root = "Women", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Jeggings", Gender = "F", Root = "Women", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Relaxed", Gender = "F", Root = "Women", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Skinny", Gender = "F", Root = "Women", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Straight", Gender = "F", Root = "Women", NodeTitle = "Jeans" } };
            }
            else
            {
                Global.CatBySubList = new List<SubRoots>() { new SubRoots() { SubRoot = "Bootcut", Gender = "F", Root = "Women", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Crooped & Ankle", Gender = "F", Root = "Women", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Flare & Wide Leg", Gender = "F", Root = "Women", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "High Rise", Gender = "F", Root = "Women", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Jeggings", Gender = "F", Root = "Women", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Relaxed", Gender = "F", Root = "Women", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Skinny", Gender = "F", Root = "Women", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Straight", Gender = "F", Root = "Women", NodeTitle = "Jeans" } };
            }
            List<SubRoots> subRootWJeans = Global.CatBySubList;
            List<SubRoots> subRootWJewelry = new List<SubRoots>() { new SubRoots() { SubRoot = "Bracelets", Gender = "F", Root = "Women", NodeTitle = "Jewelry" }, new SubRoots() { SubRoot = "Brooches", Gender = "F", Root = "Women", NodeTitle = "Jewelry" }, new SubRoots() { SubRoot = "Earrings", Gender = "F", Root = "Women", NodeTitle = "Jewelry" }, new SubRoots() { SubRoot = "Necklaces", Gender = "F", Root = "Women", NodeTitle = "Jewelry" }, new SubRoots() { SubRoot = "Rings", Gender = "F", Root = "Women", NodeTitle = "Jewelry" }, new SubRoots() { SubRoot = "Watches", Gender = "F", Root = "Women", NodeTitle = "Jewelry" } };
            List<SubRoots> subRootWPants = new List<SubRoots>() { new SubRoots() { SubRoot = "Bootcut & Flare", Gender = "F", Root = "Women", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "Crops & Capris", Gender = "F", Root = "Women", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "High Waisted", Gender = "F", Root = "Women", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "Leggings", Gender = "F", Root = "Women", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "Overalls & Jumpsuits", Gender = "F", Root = "Women", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "Skinny", Gender = "F", Root = "Women", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "Straight", Gender = "F", Root = "Women", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "Sweatpants & Joggers", Gender = "F", Root = "Women", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "Trousers & Wide Leg", Gender = "F", Root = "Women", NodeTitle = "Pants" } };
            List<SubRoots> subRootWShoes = new List<SubRoots>() { new SubRoots() { SubRoot = "Boots & Booties", Gender = "F", Root = "Women", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Clogs & Mules", Gender = "F", Root = "Women", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Espadrilles", Gender = "F", Root = "Women", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Flats & Loafers", Gender = "F", Root = "Women", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Heels", Gender = "F", Root = "Women", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Narrow", Gender = "F", Root = "Women", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Sandals", Gender = "F", Root = "Women", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Single Shoes", Gender = "F", Root = "Women", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Slippers", Gender = "F", Root = "Women", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Wedges", Gender = "F", Root = "Women", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Wide", Gender = "F", Root = "Women", NodeTitle = "Shoes" } };
            List<SubRoots> subRootWShorts = new List<SubRoots>() { new SubRoots() { SubRoot = "Active", Gender = "F", Root = "Women", NodeTitle = "Shorts" }, new SubRoots() { SubRoot = "Bermudas", Gender = "F", Root = "Women", NodeTitle = "Shorts" }, new SubRoots() { SubRoot = "Bikers", Gender = "F", Root = "Women", NodeTitle = "Shorts" }, new SubRoots() { SubRoot = "Cargo", Gender = "F", Root = "Women", NodeTitle = "Shorts" }, new SubRoots() { SubRoot = "Chino", Gender = "F", Root = "Women", NodeTitle = "Shorts" }, new SubRoots() { SubRoot = "Denim", Gender = "F", Root = "Women", NodeTitle = "Shorts" }, new SubRoots() { SubRoot = "High Waist", Gender = "F", Root = "Women", NodeTitle = "Shorts" }, new SubRoots() { SubRoot = "Short Shorts", Gender = "F", Root = "Women", NodeTitle = "Shorts" }, new SubRoots() { SubRoot = "Skorts", Gender = "F", Root = "Women", NodeTitle = "Shorts" } };
            List<SubRoots> subRootWSkirts = new List<SubRoots>() { new SubRoots() { SubRoot = "A Line", Gender = "F", Root = "Women", NodeTitle = "Skirts" }, new SubRoots() { SubRoot = "Asymmetrical", Gender = "F", Root = "Women", NodeTitle = "Skirts" }, new SubRoots() { SubRoot = "Denim", Gender = "F", Root = "Women", NodeTitle = "Skirts" }, new SubRoots() { SubRoot = "High Low", Gender = "F", Root = "Women", NodeTitle = "Skirts" }, new SubRoots() { SubRoot = "Maxi", Gender = "F", Root = "Women", NodeTitle = "Skirts" }, new SubRoots() { SubRoot = "Midi", Gender = "F", Root = "Women", NodeTitle = "Skirts" }, new SubRoots() { SubRoot = "Mini", Gender = "F", Root = "Women", NodeTitle = "Skirts" }, new SubRoots() { SubRoot = "Pencil", Gender = "F", Root = "Women", NodeTitle = "Skirts" }, new SubRoots() { SubRoot = "Pleated", Gender = "F", Root = "Women", NodeTitle = "Skirts" } };
            List<SubRoots> subRootWSuitsSeparates = new List<SubRoots>() { };
            List<SubRoots> subRootWSweaters = new List<SubRoots>() { new SubRoots() { SubRoot = "Cardigans", Gender = "F", Root = "Women", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Cashmere", Gender = "F", Root = "Women", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Cold Shoulder", Gender = "F", Root = "Women", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Cowl & Turtle Neck", Gender = "F", Root = "Women", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Crew & Scoop Necks", Gender = "F", Root = "Women", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Cropped", Gender = "F", Root = "Women", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Hoodie", Gender = "F", Root = "Women", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Ponchos", Gender = "F", Root = "Women", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Shrugs", Gender = "F", Root = "Women", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Tunic", Gender = "F", Root = "Women", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Vest", Gender = "F", Root = "Women", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "V-Neck", Gender = "F", Root = "Women", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Wrap", Gender = "F", Root = "Women", NodeTitle = "Sweaters" } };
            List<SubRoots> subRootWSwimsuits = new List<SubRoots>() { new SubRoots() { SubRoot = "Bikinis", Gender = "F", Root = "Women", NodeTitle = "Swimsuits" }, new SubRoots() { SubRoot = "Cover-Ups", Gender = "F", Root = "Women", NodeTitle = "Swimsuits" }, new SubRoots() { SubRoot = "One Pieces", Gender = "F", Root = "Women", NodeTitle = "Swimsuits" }, new SubRoots() { SubRoot = "Tankinis", Gender = "F", Root = "Women", NodeTitle = "Swimsuits" } };
            //List<SubRoots> subRootWTops = new List<SubRoots>() { new SubRoots() { SubRoot = "Blouses", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Bodysuits", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Button Down Shirts", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Cowl & Turtleneck", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Crop Tops", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Sweatshirts & Hoodies", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Tank Tops", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Tees: Long Sleeve", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Tees: Short Sleeve", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Tunics", Gender = "F", Root = "Women", NodeTitle = "Tops" } };
            List<SubRoots> subRootWTops = new List<SubRoots>() { new SubRoots() { SubRoot = "Blouses", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Bodysuits", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Button Down Shirts", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Camisoles", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Cardigans and Shrugs", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Cowl & Turtleneck", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Crop Tops", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Jerseys", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Muscle Tees", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Sweatshirts & Hoodies", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Tank Tops", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Tees: Long Sleeve", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Tees: Short Sleeve", Gender = "F", Root = "Women", NodeTitle = "Tops" } };
            List<SubRoots> subRootWBottoms = new List<SubRoots>() { new SubRoots() { SubRoot = "Bootcut", Gender = "F", Root = "Women", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Crooped & Ankle", Gender = "F", Root = "Women", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Flare & Wide Leg", Gender = "F", Root = "Women", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "High Rise", Gender = "F", Root = "Women", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Jeggings", Gender = "F", Root = "Women", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Relaxed", Gender = "F", Root = "Women", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Skinny", Gender = "F", Root = "Women", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Straight", Gender = "F", Root = "Women", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Tunics", Gender = "F", Root = "Women", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Bootcut & Flare", Gender = "F", Root = "Women", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Crops & Capris", Gender = "F", Root = "Women", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "High Waisted", Gender = "F", Root = "Women", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Overalls & Jumpsuits", Gender = "F", Root = "Women", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Sweatpants & Joggers", Gender = "F", Root = "Women", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Trousers & Wide Leg", Gender = "F", Root = "Women", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "Active", Gender = "F", Root = "Women", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Bermudas", Gender = "F", Root = "Women", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Bikers", Gender = "F", Root = "Women", NodeTitle = "Shorts" }, new SubRoots() { SubRoot = "Cargo", Gender = "F", Root = "Women", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Chino", Gender = "F", Root = "Women", NodeTitle = "Shorts" }, new SubRoots() { SubRoot = "Denim", Gender = "F", Root = "Women", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Short Shorts", Gender = "F", Root = "Women", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Skorts", Gender = "F", Root = "Women", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "A Line", Gender = "F", Root = "Women", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Asymmetrical", Gender = "F", Root = "Women", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "High Low", Gender = "F", Root = "Women", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Maxi", Gender = "F", Root = "Women", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Midi", Gender = "F", Root = "Women", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Mini", Gender = "F", Root = "Women", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Pencil", Gender = "F", Root = "Women", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Pleated", Gender = "F", Root = "Women", NodeTitle = "Bottoms" } };
            List<SubRoots> subRootWOtherClothing = new List<SubRoots>() { };

            if (Global.Storecategory == Constant.StreetwearStr)
            {
                Global.CatBySubList = new List<SubRoots>() { new SubRoots() { SubRoot = "Belts", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Electronics Cases", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Face Masks", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Glasses", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Gloves & Scarves", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Hats", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Jewelry & Watches", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Pajamas & Robes", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Sunglasses", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Ties & Pocket Squares", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Umbrellas", Gender = "M", Root = "Men", NodeTitle = "Accessories" } };
            }
            else
            {
                Global.CatBySubList = new List<SubRoots>() { new SubRoots() { SubRoot = "Belts", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Electronics Cases", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Face Masks", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Glasses", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Gloves & Scarves", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Hats", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Jewelry & Watches", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Pajamas & Robes", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Sunglasses", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Ties & Pocket Squares", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Umbrellas", Gender = "M", Root = "Men", NodeTitle = "Accessories" } };
            }
            List<SubRoots> subRootMAccessories = Global.CatBySubList;
            List<SubRoots> subRootMBages = new List<SubRoots>() { new SubRoots() { SubRoot = "Backpacks", Gender = "M", Root = "Men", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Belt Bags", Gender = "M", Root = "Men", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Briefcases", Gender = "M", Root = "Men", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Duffle Bags", Gender = "M", Root = "Men", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Laptop Bags", Gender = "M", Root = "Men", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Luggage & Travel Bags", Gender = "M", Root = "Men", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Messenger Bags", Gender = "M", Root = "Men", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Toiletry Bags", Gender = "M", Root = "Men", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Wallets", Gender = "M", Root = "Men", NodeTitle = "Bags" } };
            if (Global.Storecategory == Constant.StreetwearStr)
            {
                //Global.CatBySubList = new List<SubRoots>() { new SubRoots() { SubRoot = "Active", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Blazers", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Bombers", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Capes", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Denim & Utility", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Down & Puffers", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Fleece", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Fur & Faux Fur", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Leather & Faux Leather", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Rain Coats", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Ski", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Sport Coats", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Trench Coats", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Varsity", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Vests", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Wool & Pea Coats ", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" } };
                Global.CatBySubList = new List<SubRoots>() { new SubRoots() { SubRoot = "Active", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Blazers", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Bombers", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Capes", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Denim & Utility", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Down & Puffers", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Fleece", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Fur & Faux Fur", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Leather & Faux Leather", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Rain Coats", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Ski", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Trench Coats", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Varsity", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Vests", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Wool & Pea Coats ", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" } };
            }
            else
            {
                //Global.CatBySubList = new List<SubRoots>() { new SubRoots() { SubRoot = "Active", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Blazers", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Bombers", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Capes", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Denim & Utility", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Down & Puffers", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Fleece", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Fur & Faux Fur", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Leather & Faux Leather", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Rain Coats", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Ski", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Sport Coats", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Trench Coats", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Varsity", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Vests", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Windbreakers", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Wool & Pea Coats ", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" } };
                Global.CatBySubList = new List<SubRoots>() { new SubRoots() { SubRoot = "Active", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Blazers", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Bombers", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Capes", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Denim & Utility", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Down & Puffers", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Fleece", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Fur & Faux Fur", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Leather & Faux Leather", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Rain Coats", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Ski", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Trench Coats", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Varsity", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Vests", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Windbreakers", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Wool & Pea Coats ", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" } };
            }
            List<SubRoots> subRootMJacketsCoats = Global.CatBySubList;
            List<SubRoots> subRootMJeans = new List<SubRoots>() { new SubRoots() { SubRoot = "Bootcut", Gender = "M", Root = "Men", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Relaxed", Gender = "M", Root = "Men", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Skinny", Gender = "M", Root = "Men", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Slim", Gender = "M", Root = "Men", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Straight", Gender = "M", Root = "Men", NodeTitle = "Jeans" } };
            List<SubRoots> subRootMPants = new List<SubRoots>() { new SubRoots() { SubRoot = "Cargo", Gender = "M", Root = "Men", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "Chinos & Khakis", Gender = "M", Root = "Men", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "Corduroy", Gender = "M", Root = "Men", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "Dress", Gender = "M", Root = "Men", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "Leggings", Gender = "M", Root = "Men", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "Overalls & Jumpsuits", Gender = "M", Root = "Men", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "Sweatpants & Joggers", Gender = "M", Root = "Men", NodeTitle = "Pants" } };
            if (Global.Storecategory == Constant.StreetwearStr)
            {
                Global.CatBySubList = new List<SubRoots>() { new SubRoots() { SubRoot = "Casual Button Downs", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Dress Shirts", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Jerseys", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Polos", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Sweatshirts & Hoodies", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Tank Tops", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Tees: Long Sleeve", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Tees: Short Sleeve", Gender = "M", Root = "Men", NodeTitle = "Shirts" } };
            }
            else if (Global.Storecategory != Constant.VintageStr)
            {

                Global.CatBySubList = new List<SubRoots>() { new SubRoots() { SubRoot = "Casual Button Downs", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Dress Shirts", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Jerseys", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Polos", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Sweatshirts & Hoodies", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Tank Tops", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Tees: Long Sleeve", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Tees: Short Sleeve", Gender = "M", Root = "Men", NodeTitle = "Shirts" } };
            }
            if (Global.Storecategory == Constant.VintageStr)
            {
                Global.CatBySubList = new List<SubRoots>() { new SubRoots() { SubRoot = "Casual Button Downs", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Dress Shirts", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Polos", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Sweatshirts & Hoodies", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Tank Tops", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Tees: Long Sleeve", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Tees: Short Sleeve", Gender = "M", Root = "Men", NodeTitle = "Shirts" } };
            }
            else if (Global.Storecategory != Constant.StreetwearStr)
            {

                Global.CatBySubList = new List<SubRoots>() { new SubRoots() { SubRoot = "Casual Button Downs", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Dress Shirts", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Jerseys", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Polos", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Sweatshirts & Hoodies", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Tank Tops", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Tees: Long Sleeve", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Tees: Short Sleeve", Gender = "M", Root = "Men", NodeTitle = "Shirts" } };
            }
            List<SubRoots> subRootMShirts = Global.CatBySubList;
            List<SubRoots> subRootMShoes = new List<SubRoots>() { new SubRoots() { SubRoot = "Boat Shoes", Gender = "M", Root = "Men", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Boots", Gender = "M", Root = "Men", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Casual Shoes", Gender = "M", Root = "Men", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Dress Shoes", Gender = "M", Root = "Men", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Loafers & Drivers", Gender = "M", Root = "Men", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Sandals & Flip Flops", Gender = "M", Root = "Men", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Slippers", Gender = "M", Root = "Men", NodeTitle = "Shoes" } };
            List<SubRoots> subRootMShorts = new List<SubRoots>() { new SubRoots() { SubRoot = "Athletic", Gender = "M", Root = "Men", NodeTitle = "Shorts" }, new SubRoots() { SubRoot = "Cargo", Gender = "M", Root = "Men", NodeTitle = "Shorts" }, new SubRoots() { SubRoot = "Chino", Gender = "M", Root = "Men", NodeTitle = "Shorts" }, new SubRoots() { SubRoot = "Denim", Gender = "M", Root = "Men", NodeTitle = "Shorts" }, new SubRoots() { SubRoot = "Hybrids", Gender = "M", Root = "Men", NodeTitle = "Shorts" } };
            List<SubRoots> subRootMSuitsTuxedos = new List<SubRoots>() { };
            if (Global.Storecategory == Constant.VintageStr)
            {
                Global.CatBySubList = new List<SubRoots>() { new SubRoots() { SubRoot = "Cardigans", Gender = "M", Root = "Men", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Cashmere", Gender = "M", Root = "Men", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Hoodie", Gender = "M", Root = "Men", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Turtlenecks", Gender = "M", Root = "Men", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Vest", Gender = "M", Root = "Men", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "V-Neck", Gender = "M", Root = "Men", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Zip-Up", Gender = "M", Root = "Men", NodeTitle = "Sweaters" } };
            }
            else
            {
                Global.CatBySubList = new List<SubRoots>() { new SubRoots() { SubRoot = "Cardigans", Gender = "M", Root = "Men", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Cashmere", Gender = "M", Root = "Men", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Crewnecks", Gender = "M", Root = "Men", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Hoodie", Gender = "M", Root = "Men", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Turtlenecks", Gender = "M", Root = "Men", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Vest", Gender = "M", Root = "Men", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "V-Neck", Gender = "M", Root = "Men", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Zip-Up", Gender = "M", Root = "Men", NodeTitle = "Sweaters" } };
            }
            List<SubRoots> subRootMSweaters = Global.CatBySubList;
            List<SubRoots> subRootMSocksandUnderwear = new List<SubRoots>() { new SubRoots() { SubRoot = "Athletic Socks", Gender = "M", Root = "Men", NodeTitle = "Socks & Underwear" }, new SubRoots() { SubRoot = "Boxer Briefs", Gender = "M", Root = "Men", NodeTitle = "Socks & Underwear" }, new SubRoots() { SubRoot = "Boxers", Gender = "M", Root = "Men", NodeTitle = "Socks & Underwear" }, new SubRoots() { SubRoot = "Briefs", Gender = "M", Root = "Men", NodeTitle = "Socks & Underwear" }, new SubRoots() { SubRoot = "Casual Socks", Gender = "M", Root = "Men", NodeTitle = "Socks & Underwear" }, new SubRoots() { SubRoot = "Dress Socks", Gender = "M", Root = "Men", NodeTitle = "Socks & Underwear" }, new SubRoots() { SubRoot = "Undershirts", Gender = "M", Root = "Men", NodeTitle = "Socks & Underwear" } };
            List<SubRoots> subRootMSwim = new List<SubRoots>() { new SubRoots() { SubRoot = "Board Shorts", Gender = "M", Root = "Men", NodeTitle = "Swim" }, new SubRoots() { SubRoot = "Hybrids", Gender = "M", Root = "Men", NodeTitle = "Swim" }, new SubRoots() { SubRoot = "Rash Guard", Gender = "M", Root = "Men", NodeTitle = "Swim" }, new SubRoots() { SubRoot = "Swim Trunks", Gender = "M", Root = "Men", NodeTitle = "Swim" } };

            List<SubRoots> subRootMOtherClothing = new List<SubRoots>() { };

            //Added for sneakers
            List<SubRoots> subRootWBasketball = new List<SubRoots>() { };
            List<SubRoots> subRootWCasual = new List<SubRoots>() { };
            List<SubRoots> subRootWLuxury = new List<SubRoots>() { };
            List<SubRoots> subRootWRunning = new List<SubRoots>() { };
            List<SubRoots> subRootWSoccer = new List<SubRoots>() { };
            List<SubRoots> subRootWSkateboard = new List<SubRoots>() { };
            List<SubRoots> subRootWSlides = new List<SubRoots>() { };
            List<SubRoots> subRootWTennis = new List<SubRoots>() { };
            List<SubRoots> subRootWTrackField = new List<SubRoots>() { };
            List<SubRoots> subRootWTraining = new List<SubRoots>() { };
            List<SubRoots> subRootWWalking = new List<SubRoots>() { };

            List<SubRoots> subRootMBasketball = new List<SubRoots>() { };
            List<SubRoots> subRootMCasual = new List<SubRoots>() { };
            List<SubRoots> subRootMLuxury = new List<SubRoots>() { };
            List<SubRoots> subRootMRunning = new List<SubRoots>() { };
            List<SubRoots> subRootMSoccer = new List<SubRoots>() { };
            List<SubRoots> subRootMSkateboard = new List<SubRoots>() { };
            List<SubRoots> subRootMSlides = new List<SubRoots>() { };
            List<SubRoots> subRootMTennis = new List<SubRoots>() { };
            List<SubRoots> subRootMTrackField = new List<SubRoots>() { };
            List<SubRoots> subRootMTraining = new List<SubRoots>() { };
            List<SubRoots> subRootMWalking = new List<SubRoots>() { };

            Dictionary<Nodes, List<SubRoots>> nodeW = new Dictionary<Nodes, List<SubRoots>>();
            Dictionary<Nodes, List<SubRoots>> nodeMen = new Dictionary<Nodes, List<SubRoots>>();

            //Women list
            nodeW.Add(new Nodes() { NodeTitle = "Accessories", IsShowMore = true, Root = "Women" }, subRootWAccessories);
            nodeW.Add(new Nodes() { NodeTitle = "Bags", IsShowMore = true, Root = "Women" }, subRootWBages);
            nodeW.Add(new Nodes() { NodeTitle = "Dresses", IsShowMore = true, Root = "Women" }, subRootWDresses);
            nodeW.Add(new Nodes() { NodeTitle = "Intimates & Sleepwear", IsShowMore = true, Root = "Women" }, subRootWIntimatesSleepwear);
            nodeW.Add(new Nodes() { NodeTitle = "Jackets & Coats", IsShowMore = true, Root = "Women" }, subRootWJacketsCoats);
            nodeW.Add(new Nodes() { NodeTitle = "Jeans", IsShowMore = true, Root = "Women" }, subRootWJeans);
            nodeW.Add(new Nodes() { NodeTitle = "Jewelry", IsShowMore = true, Root = "Women" }, subRootWJewelry);
            nodeW.Add(new Nodes() { NodeTitle = "Pants", IsShowMore = true, Root = "Women" }, subRootWPants);
            if (Global.SearchedResultSelectedStore.ToLower() == Constant.ClothingStr.ToLower() || Global.SearchedResultSelectedStore.ToLower() == Constant.VintageStr.ToLower())
            {
                nodeW.Add(new Nodes() { NodeTitle = "Shoes", IsShowMore = true, Root = "Women" }, subRootWShoes);
            }
            nodeW.Add(new Nodes() { NodeTitle = "Shorts", IsShowMore = true, Root = "Women" }, subRootWShorts);
            nodeW.Add(new Nodes() { NodeTitle = "Skirts", IsShowMore = true, Root = "Women" }, subRootWSkirts);
            nodeW.Add(new Nodes() { NodeTitle = "Suits & Separates", IsShowMore = false, Root = "Women" }, subRootWSuitsSeparates);
            nodeW.Add(new Nodes() { NodeTitle = "Sweaters", IsShowMore = true, Root = "Women" }, subRootWSweaters);
            nodeW.Add(new Nodes() { NodeTitle = "Swimsuits", IsShowMore = true, Root = "Women" }, subRootWSwimsuits);
            nodeW.Add(new Nodes() { NodeTitle = "Tops", IsShowMore = true, Root = "Women" }, subRootWTops);
            //nodeW.Add(new Nodes() { NodeTitle = "Bottoms", IsShowMore = true, Root = "Women" }, subRootWBottoms);
            nodeW.Add(new Nodes() { NodeTitle = "Other", IsShowMore = false, Root = "Women" }, subRootWOtherClothing);

            //Added for sneakers women
            nodeW.Add(new Nodes() { NodeTitle = "Basketball", Gender = "F", IsShowMore = false, Root = "Women" }, subRootWBasketball);
            nodeW.Add(new Nodes() { NodeTitle = "Casual", Gender = "F", IsShowMore = false, Root = "Women" }, subRootWCasual);
            nodeW.Add(new Nodes() { NodeTitle = "Luxury", Gender = "F", IsShowMore = false, Root = "Women" }, subRootWLuxury);
            nodeW.Add(new Nodes() { NodeTitle = "Running", Gender = "F", IsShowMore = false, Root = "Women" }, subRootWRunning);
            nodeW.Add(new Nodes() { NodeTitle = "Soccer", Gender = "F", IsShowMore = false, Root = "Women" }, subRootWSoccer);
            nodeW.Add(new Nodes() { NodeTitle = "Skateboard", Gender = "F", IsShowMore = false, Root = "Women" }, subRootWSkateboard);
            nodeW.Add(new Nodes() { NodeTitle = "Slides", Gender = "F", IsShowMore = false, Root = "Women" }, subRootWSlides);
            nodeW.Add(new Nodes() { NodeTitle = "Tennis", Gender = "F", IsShowMore = false, Root = "Women" }, subRootWTennis);
            nodeW.Add(new Nodes() { NodeTitle = "Track & Field", Gender = "F", IsShowMore = false, Root = "Women" }, subRootWTrackField);
            nodeW.Add(new Nodes() { NodeTitle = "Training", Gender = "F", IsShowMore = false, Root = "Women" }, subRootWTraining);
            nodeW.Add(new Nodes() { NodeTitle = "Walking", Gender = "F", IsShowMore = false, Root = "Women" }, subRootWWalking);

            
            //Men list
            nodeMen.Add(new Nodes() { NodeTitle = "Accessories", IsShowMore = true, Root = "Men" }, subRootMAccessories);
            nodeMen.Add(new Nodes() { NodeTitle = "Bags", IsShowMore = true, Root = "Men" }, subRootMBages);
            nodeMen.Add(new Nodes() { NodeTitle = "Jackets & Coats", IsShowMore = true, Root = "Men" }, subRootMJacketsCoats);
            nodeMen.Add(new Nodes() { NodeTitle = "Jeans", IsShowMore = true, Root = "Men" }, subRootMJeans);
            nodeMen.Add(new Nodes() { NodeTitle = "Pants", IsShowMore = true, Root = "Men" }, subRootMPants);
            nodeMen.Add(new Nodes() { NodeTitle = "Shirts", IsShowMore = true, Root = "Men" }, subRootMShirts);
            if (Global.SearchedResultSelectedStore.ToLower() == Constant.ClothingStr.ToLower() || Global.SearchedResultSelectedStore.ToLower() == Constant.VintageStr.ToLower())
            {
                nodeMen.Add(new Nodes() { NodeTitle = "Shoes", IsShowMore = true, Root = "Men" }, subRootMShoes);
            }
            nodeMen.Add(new Nodes() { NodeTitle = "Shorts", IsShowMore = true, Root = "Men" }, subRootMShorts);
            if (Global.Storecategory == Constant.VintageStr)
            {
                //nodeMen.Add(new Nodes() { IsShowMore = false}, subRootMSocksandUnderwear);
            }
            else
            {
                nodeMen.Add(new Nodes() { NodeTitle = "Socks & Underwear", IsShowMore = true, Root = "Men" }, subRootMSocksandUnderwear);
            }
            nodeMen.Add(new Nodes() { NodeTitle = "Suits & Tuxedos", IsShowMore = false, Root = "Men" }, subRootMSuitsTuxedos);
            nodeMen.Add(new Nodes() { NodeTitle = "Sweaters", IsShowMore = true, Root = "Men" }, subRootMSweaters);
            nodeMen.Add(new Nodes() { NodeTitle = "Swim", IsShowMore = true, Root = "Men" }, subRootMSwim);
            nodeMen.Add(new Nodes() { NodeTitle = "Other", IsShowMore = false, Root = "Men" }, subRootMOtherClothing);

            //Added for sneakers men
            nodeMen.Add(new Nodes() { NodeTitle = "Basketball", Gender = "M", IsShowMore = false, Root = "Men" }, subRootMBasketball);
            nodeMen.Add(new Nodes() { NodeTitle = "Casual", Gender = "M", IsShowMore = false, Root = "Men" }, subRootMCasual);
            nodeMen.Add(new Nodes() { NodeTitle = "Luxury", Gender = "M", IsShowMore = false, Root = "Men" }, subRootMLuxury);
            nodeMen.Add(new Nodes() { NodeTitle = "Running", Gender = "M", IsShowMore = false, Root = "Men" }, subRootMRunning);
            nodeMen.Add(new Nodes() { NodeTitle = "Soccer", Gender = "M", IsShowMore = false, Root = "Men" }, subRootMSoccer);
            nodeMen.Add(new Nodes() { NodeTitle = "Skateboard", Gender = "M", IsShowMore = false, Root = "Men" }, subRootMSkateboard);
            nodeMen.Add(new Nodes() { NodeTitle = "Slides", Gender = "M", IsShowMore = false, Root = "Men" }, subRootMSlides);
            nodeMen.Add(new Nodes() { NodeTitle = "Tennis", Gender = "M", IsShowMore = false, Root = "Men" }, subRootMTennis);
            nodeMen.Add(new Nodes() { NodeTitle = "Track & Field", Gender = "M", IsShowMore = false, Root = "Men" }, subRootMTrackField);
            nodeMen.Add(new Nodes() { NodeTitle = "Training", Gender = "M", Root = "Men", IsShowMore = false }, subRootMTraining);
            nodeMen.Add(new Nodes() { NodeTitle = "Walking", Gender = "M", Root = "Men", IsShowMore = false }, subRootMWalking);

            roots = new List<Roots>()
            {
                new Roots(){ Root="Women", Node=nodeW},
                new Roots(){ Root="Men", Node=nodeMen}
            };

            if (selectedsubcat != null && selectedcat != null)
            {
                if (selectedsubcat.ToLower().Contains("COATS".ToLower()))
                {
                    selectedsubcat = "JACKETS & COATS";
                }
                if (selectedsubcat.ToLower().Contains("SUITS".ToLower()))
                {
                    selectedsubcat = "SUITS & TUXEDOS";
                }
                if (selectedsubcat.ToLower().Contains("SOCKS".ToLower()))
                {
                    selectedsubcat = "socks & underwear";
                }
                if (selectedsubcat.ToLower().Contains("Swim".ToLower()) && selectedcat.Contains("Women"))
                {
                    selectedsubcat = "Swimsuits";
                }
                var selectedroot = roots.Where(x => x.Root.ToLower() == selectedcat.ToLower()).ToList();
                var subRootsList = selectedroot.Select(r => r.Node.Keys.Where(k => k.NodeTitle.ToLower() == selectedsubcat.ToLower()).FirstOrDefault()).FirstOrDefault();
                var res = selectedroot.Select(x => x.Node[subRootsList]).ToList().FirstOrDefault();

                List<SubRoots> subRoots = new List<SubRoots>();
                Dictionary<Nodes, List<SubRoots>> SubnodeDicn = new Dictionary<Nodes, List<SubRoots>>();
                foreach (var item in res)
                {
                    var node = new Nodes()
                    {
                        IsShowMore = item.IsShowMore,
                        NodeTitle = item.SubRoot,
                        Gender = item.Gender
                    };
                    SubnodeDicn.Add(node, subRoots);
                }

                List<Roots> rootsa = new List<Roots>
                {
                    new Roots()
                    {
                        Root = selectedsubcat,
                        Node = SubnodeDicn
                    }
                };
                return Global.RootsList = rootsa;
            }
            else if (selectedcat != null)
            {
                return Global.RootsList = roots.Where(x => x.Root.ToLower() == selectedcat.ToLower()).ToList();
            }
            else
            {
                Global.RootsList = roots;
            }
            return roots;
        }
        #endregion

        #region Common Method created to show all categories inside All stores (Clothing, Sneakers, Streetwear, Vintage)
        public List<Roots> GetCommonCategoryListForAllStores(string selectedcat, string selectedsubcat)
        {
            List<Roots> roots;

            //Categories list for All
            #region Category list for All
            Dictionary<Nodes, List<SubRoots>> nodeAll = new Dictionary<Nodes, List<SubRoots>>();

            List<SubRoots> subRootAllAccessories = new List<SubRoots>() { new SubRoots() { SubRoot = "Belts", Gender = "All", Root = "All", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Electronics Cases", Gender = "All", Root = "All", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Glasses", Gender = "All", Root = "All", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Gloves & Scarves", Gender = "All", Root = "All", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Gloves & Mittens", Gender = "All", Root = "All", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Hair Accessories", Gender = "All", Root = "All", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Hats", Gender = "All", Root = "All", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Hosiery", Gender = "All", Root = "All", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Jewelry & Watches", Gender = "All", Root = "All", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Pajamas & Robes", Gender = "All", Root = "All", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Scarves & Wraps", Gender = "All", Root = "All", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Sunglasses", Gender = "All", Root = "All", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Ties & Pocket Squares", Gender = "All", Root = "All", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Umbrellas", Gender = "All", Root = "All", NodeTitle = "Accessories" } };// , new SubRoots() { SubRoot = "Face Masks", Gender = "All", Root = "All", NodeTitle = "Accessories" }, , new SubRoots() { SubRoot = "Socks", Gender = "All", Root = "All", NodeTitle = "Accessories" }, , new SubRoots() { SubRoot = "Watches", Gender = "All", Root = "All", NodeTitle = "Accessories" }
            List<SubRoots> subRootAllBages = new List<SubRoots>() { new SubRoots() { SubRoot = "Baby Bags", Gender = "All", Root = "All", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Backpacks", Gender = "All", Root = "All", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Belt Bags", Gender = "All", Root = "All", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Briefcases", Gender = "All", Root = "All", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Clutches & Wristlets", Gender = "All", Root = "All", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Cosmetic Bags", Gender = "All", Root = "All", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Crossbody Bags", Gender = "All", Root = "All", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Duffle Bags", Gender = "All", Root = "All", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Hobos", Gender = "All", Root = "All", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Laptop Bags", Gender = "All", Root = "All", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Luggage & Travel Bags", Gender = "All", Root = "All", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Messenger Bags ", Gender = "All", Root = "All", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Satchels", Gender = "All", Root = "All", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Shoulder Bags", Gender = "All", Root = "All", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Totes", Gender = "All", Root = "All", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Toiletry Bags", Gender = "All", Root = "All", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Wallets", Gender = "All", Root = "All", NodeTitle = "Bags" } };
            List<SubRoots> subRootAllDresses = new List<SubRoots>() { new SubRoots() { SubRoot = "Asymmetrical", Gender = "All", Root = "All", NodeTitle = "Dresses" }, new SubRoots() { SubRoot = "High Low", Gender = "All", Root = "All", NodeTitle = "Dresses" }, new SubRoots() { SubRoot = "Long Sleeve", Gender = "All", Root = "All", NodeTitle = "Dresses" }, new SubRoots() { SubRoot = "Maxi", Gender = "All", Root = "All", NodeTitle = "Dresses" }, new SubRoots() { SubRoot = "Midi", Gender = "All", Root = "All", NodeTitle = "Dresses" }, new SubRoots() { SubRoot = "Mini", Gender = "All", Root = "All", NodeTitle = "Dresses" }, new SubRoots() { SubRoot = "Prom", Gender = "All", Root = "All", NodeTitle = "Dresses" }, new SubRoots() { SubRoot = "Rompers", Gender = "All", Root = "All", NodeTitle = "Dresses" }, new SubRoots() { SubRoot = "Strapless", Gender = "All", Root = "All", NodeTitle = "Dresses" }, new SubRoots() { SubRoot = "Wedding", Gender = "All", Root = "All", NodeTitle = "Dresses" } };
            List<SubRoots> subRootAllIntimatesSleepwear = new List<SubRoots>() { new SubRoots() { SubRoot = "Bras", Gender = "All", Root = "All", NodeTitle = "Intimates & Sleepwear" }, new SubRoots() { SubRoot = "Chemises & Slips", Gender = "All", Root = "All", NodeTitle = "Intimates & Sleepwear" }, new SubRoots() { SubRoot = "Pajamas", Gender = "All", Root = "All", NodeTitle = "Intimates & Sleepwear" }, new SubRoots() { SubRoot = "Panties", Gender = "All", Root = "All", NodeTitle = "Intimates & Sleepwear" }, new SubRoots() { SubRoot = "Robes", Gender = "All", Root = "All", NodeTitle = "Intimates & Sleepwear" }, new SubRoots() { SubRoot = "Shapewear", Gender = "All", Root = "All", NodeTitle = "Intimates & Sleepwear" } };
            List<SubRoots> subRootAllJacketsCoats = new List<SubRoots>() { new SubRoots() { SubRoot = "Active", Gender = "All", Root = "All", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Blazers", Gender = "All", Root = "All", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Bombers", Gender = "All", Root = "All", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Capes & Ponchos", Gender = "All", Root = "All", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Capes", Gender = "All", Root = "All", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Denim & Utility", Gender = "All", Root = "All", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Down & Puffers", Gender = "All", Root = "All", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Fleece", Gender = "All", Root = "All", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Fur & Faux Fur", Gender = "All", Root = "All", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Leather & Faux Leather", Gender = "All", Root = "All", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Rain Coats", Gender = "All", Root = "All", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Ski", Gender = "All", Root = "All", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Teddy", Gender = "All", Root = "All", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Trench Coats", Gender = "All", Root = "All", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Varsity", Gender = "All", Root = "All", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Vests", Gender = "All", Root = "All", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Windbreakers", Gender = "All", Root = "All", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Wool & Pea Coats", Gender = "All", Root = "All", NodeTitle = "Jackets & Coats" } };
            if (Global.Storecategory == Constant.ClothingStr)
            {
                Global.CatBySubList = new List<SubRoots>() { new SubRoots() { SubRoot = "Bootcut", Gender = "All", Root = "All", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Crooped & Ankle", Gender = "All", Root = "All", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Flare & Wide Leg", Gender = "All", Root = "All", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "High Rise", Gender = "All", Root = "All", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Jeggings", Gender = "All", Root = "All", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Relaxed", Gender = "All", Root = "All", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Skinny", Gender = "All", Root = "All", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Slim", Gender = "All", Root = "All", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Straight", Gender = "All", Root = "All", NodeTitle = "Jeans" } };
            }
            else
            {
                Global.CatBySubList = new List<SubRoots>() { new SubRoots() { SubRoot = "Bootcut", Gender = "All", Root = "All", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Crooped & Ankle", Gender = "All", Root = "All", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Flare & Wide Leg", Gender = "All", Root = "All", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "High Rise", Gender = "All", Root = "All", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Jeggings", Gender = "All", Root = "All", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Relaxed", Gender = "All", Root = "All", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Skinny", Gender = "All", Root = "All", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Slim", Gender = "All", Root = "All", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Straight", Gender = "All", Root = "All", NodeTitle = "Jeans" } };
            }
            List<SubRoots> subRootAllJeans = Global.CatBySubList;
            List<SubRoots> subRootAllJewelry = new List<SubRoots>() { new SubRoots() { SubRoot = "Bracelets", Gender = "All", Root = "All", NodeTitle = "Jewelry" }, new SubRoots() { SubRoot = "Brooches", Gender = "All", Root = "All", NodeTitle = "Jewelry" }, new SubRoots() { SubRoot = "Earrings", Gender = "All", Root = "All", NodeTitle = "Jewelry" }, new SubRoots() { SubRoot = "Necklaces", Gender = "All", Root = "All", NodeTitle = "Jewelry" }, new SubRoots() { SubRoot = "Rings", Gender = "All", Root = "All", NodeTitle = "Jewelry" },new SubRoots() { SubRoot = "Watches", Gender = "All", Root = "All", NodeTitle = "Jewelry" } };
            List<SubRoots> subRootAllPants = new List<SubRoots>() { new SubRoots() { SubRoot = "Bootcut & Flare", Gender = "All", Root = "All", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "Crops & Capris", Gender = "All", Root = "All", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "Cargo", Gender = "All", Root = "All", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "Chinos & Khakis", Gender = "All", Root = "All", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "Corduroy", Gender = "All", Root = "All", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "Dress", Gender = "All", Root = "All", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "High Waisted", Gender = "All", Root = "All", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "Leggings", Gender = "All", Root = "All", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "Overalls & Jumpsuits", Gender = "All", Root = "All", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "Skinny", Gender = "All", Root = "All", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "Straight", Gender = "All", Root = "All", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "Sweatpants & Joggers", Gender = "All", Root = "All", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "Trousers & Wide Leg", Gender = "All", Root = "All", NodeTitle = "Pants" } };
            List<SubRoots> subRootAllShoes = new List<SubRoots>() { new SubRoots() { SubRoot = "Boots & Booties", Gender = "All", Root = "All", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Boat Shoes", Gender = "All", Root = "All", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Boots", Gender = "All", Root = "All", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Clogs & Mules", Gender = "All", Root = "All", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Casual Shoes", Gender = "All", Root = "All", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Dress Shoes", Gender = "All", Root = "All", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Espadrilles", Gender = "All", Root = "All", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Flats & Loafers", Gender = "All", Root = "All", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Heels", Gender = "All", Root = "All", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Loafers & Drivers", Gender = "All", Root = "All", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Narrow", Gender = "All", Root = "All", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Sandals", Gender = "All", Root = "All", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Sandals & Flip Flops", Gender = "All", Root = "All", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Single Shoes", Gender = "All", Root = "All", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Slippers", Gender = "All", Root = "All", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Wedges", Gender = "All", Root = "All", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Wide", Gender = "All", Root = "All", NodeTitle = "Shoes" } };
            List<SubRoots> subRootAllShorts = new List<SubRoots>() { new SubRoots() { SubRoot = "Active", Gender = "All", Root = "All", NodeTitle = "Shorts" }, new SubRoots() { SubRoot = "Athletic", Gender = "All", Root = "All", NodeTitle = "Shorts" }, new SubRoots() { SubRoot = "Bermudas", Gender = "All", Root = "All", NodeTitle = "Shorts" }, new SubRoots() { SubRoot = "Bikers", Gender = "All", Root = "All", NodeTitle = "Shorts" }, new SubRoots() { SubRoot = "Cargo", Gender = "All", Root = "All", NodeTitle = "Shorts" }, new SubRoots() { SubRoot = "Chino", Gender = "All", Root = "All", NodeTitle = "Shorts" }, new SubRoots() { SubRoot = "Denim", Gender = "All", Root = "All", NodeTitle = "Shorts" }, new SubRoots() { SubRoot = "High Waist", Gender = "All", Root = "All", NodeTitle = "Shorts" }, new SubRoots() { SubRoot = "Hybrids", Gender = "All", Root = "All", NodeTitle = "Shorts" }, new SubRoots() { SubRoot = "Short Shorts", Gender = "All", Root = "All", NodeTitle = "Shorts" }, new SubRoots() { SubRoot = "Skorts", Gender = "All", Root = "All", NodeTitle = "Shorts" } };
            List<SubRoots> subRootAllShirts = new List<SubRoots>() { new SubRoots() { SubRoot = "Casual Button Downs", Gender = "All", Root = "All", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Dress Shirts", Gender = "All", Root = "All", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Jerseys", Gender = "All", Root = "All", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Polos", Gender = "All", Root = "All", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Sweatshirts & Hoodies", Gender = "All", Root = "All", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Tank Tops", Gender = "All", Root = "All", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Tees: Long Sleeve", Gender = "All", Root = "All", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Tees: Short Sleeve", Gender = "All", Root = "All", NodeTitle = "Shirts" } };
            List<SubRoots> subRootAllSkirts = new List<SubRoots>() { new SubRoots() { SubRoot = "A Line", Gender = "All", Root = "All", NodeTitle = "Skirts" }, new SubRoots() { SubRoot = "Asymmetrical", Gender = "All", Root = "All", NodeTitle = "Skirts" }, new SubRoots() { SubRoot = "Denim", Gender = "All", Root = "All", NodeTitle = "Skirts" }, new SubRoots() { SubRoot = "High Low", Gender = "All", Root = "All", NodeTitle = "Skirts" }, new SubRoots() { SubRoot = "Maxi", Gender = "All", Root = "All", NodeTitle = "Skirts" }, new SubRoots() { SubRoot = "Midi", Gender = "All", Root = "All", NodeTitle = "Skirts" }, new SubRoots() { SubRoot = "Mini", Gender = "All", Root = "All", NodeTitle = "Skirts" }, new SubRoots() { SubRoot = "Pencil", Gender = "All", Root = "All", NodeTitle = "Skirts" }, new SubRoots() { SubRoot = "Pleated", Gender = "All", Root = "All", NodeTitle = "Skirts" } };
            //List<SubRoots> subRootAllSneakers = new List<SubRoots>() { new SubRoots() { SubRoot = "Basketball", Gender = "All", Root = "All", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Casual", Gender = "All", Root = "All", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Luxury", Gender = "All", Root = "All", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Running", Gender = "All", Root = "All", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Soccer", Gender = "All", Root = "All", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Skateboard", Gender = "All", Root = "All", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Slides", Gender = "All", Root = "All", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Tennis", Gender = "All", Root = "All", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Track & Field", Gender = "All", Root = "All", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Training", Gender = "All", Root = "All", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Walking", Gender = "All", Root = "All", NodeTitle = "Sneakers" } };
            List<SubRoots> subRootAllSuitsSeparates = new List<SubRoots>() { };
            List<SubRoots> subRootAllSuitsTuxedos = new List<SubRoots>() { };
            List<SubRoots> subRootAllSweaters = new List<SubRoots>() { new SubRoots() { SubRoot = "Cardigans", Gender = "All", Root = "All", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Cashmere", Gender = "All", Root = "All", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Cold Shoulder", Gender = "All", Root = "All", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Cowl & Turtle Neck", Gender = "All", Root = "All", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Crew & Scoop Necks", Gender = "All", Root = "All", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Cropped", Gender = "All", Root = "All", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Crewnecks", Gender = "All", Root = "All", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Hoodie", Gender = "All", Root = "All", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Ponchos", Gender = "All", Root = "All", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Shrugs", Gender = "All", Root = "All", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Tunic", Gender = "All", Root = "All", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Turtlenecks", Gender = "All", Root = "All", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Vest", Gender = "All", Root = "All", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "V-Neck", Gender = "All", Root = "All", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Wrap", Gender = "All", Root = "All", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Zip-Up", Gender = "All", Root = "All", NodeTitle = "Sweaters" } };
            List<SubRoots> subRootAllSwimsuits = new List<SubRoots>() { new SubRoots() { SubRoot = "Bikinis", Gender = "All", Root = "All", NodeTitle = "Swimsuits" }, new SubRoots() { SubRoot = "Cover-Ups", Gender = "All", Root = "All", NodeTitle = "Swimsuits" }, new SubRoots() { SubRoot = "One Pieces", Gender = "All", Root = "All", NodeTitle = "Swimsuits" }, new SubRoots() { SubRoot = "Tankinis", Gender = "All", Root = "All", NodeTitle = "Swimsuits" } };
            List<SubRoots> subRootAllSocksandUnderwear = new List<SubRoots>() { new SubRoots() { SubRoot = "Athletic Socks", Gender = "All", Root = "All", NodeTitle = "Socks & Underwear" }, new SubRoots() { SubRoot = "Boxer Briefs", Gender = "All", Root = "All", NodeTitle = "Socks & Underwear" }, new SubRoots() { SubRoot = "Boxers", Gender = "All", Root = "All", NodeTitle = "Socks & Underwear" }, new SubRoots() { SubRoot = "Briefs", Gender = "All", Root = "All", NodeTitle = "Socks & Underwear" }, new SubRoots() { SubRoot = "Casual Socks", Gender = "All", Root = "All", NodeTitle = "Socks & Underwear" }, new SubRoots() { SubRoot = "Dress Socks", Gender = "All", Root = "All", NodeTitle = "Socks & Underwear" }, new SubRoots() { SubRoot = "Undershirts", Gender = "All", Root = "All", NodeTitle = "Socks & Underwear" } };
            List<SubRoots> subRootAllSwim = new List<SubRoots>() { new SubRoots() { SubRoot = "Board Shorts", Gender = "All", Root = "All", NodeTitle = "Swim" }, new SubRoots() { SubRoot = "Hybrids", Gender = "All", Root = "All", NodeTitle = "Swim" }, new SubRoots() { SubRoot = "Rash Guard", Gender = "All", Root = "All", NodeTitle = "Swim" }, new SubRoots() { SubRoot = "Swim Trunks", Gender = "All", Root = "All", NodeTitle = "Swim" } };
            List<SubRoots> subRootAllTops = new List<SubRoots>() { new SubRoots() { SubRoot = "Blouses", Gender = "All", Root = "All", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Bodysuits", Gender = "All", Root = "All", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Button Down Shirts", Gender = "All", Root = "All", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Camisoles", Gender = "All", Root = "All", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Cardigans and Shrugs", Gender = "All", Root = "All", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Cowl & Turtleneck", Gender = "All", Root = "All", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Crop Tops", Gender = "All", Root = "All", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Jerseys", Gender = "All", Root = "All", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Muscle Tees", Gender = "All", Root = "All", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Sweatshirts & Hoodies", Gender = "All", Root = "All", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Tank Tops", Gender = "All", Root = "All", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Tees: Long Sleeve", Gender = "All", Root = "All", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Tees: Short Sleeve", Gender = "All", Root = "All", NodeTitle = "Tops" } };
            List<SubRoots> subRootAllBottoms = new List<SubRoots>() { new SubRoots() { SubRoot = "Bootcut", Gender = "All", Root = "All", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Crooped & Ankle", Gender = "All", Root = "All", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Flare & Wide Leg", Gender = "All", Root = "All", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "High Rise", Gender = "All", Root = "All", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Jeggings", Gender = "All", Root = "All", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Relaxed", Gender = "All", Root = "All", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Skinny", Gender = "All", Root = "All", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Straight", Gender = "All", Root = "All", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Tunics", Gender = "All", Root = "All", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Bootcut & Flare", Gender = "All", Root = "All", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Crops & Capris", Gender = "All", Root = "All", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "High Waisted", Gender = "All", Root = "All", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Overalls & Jumpsuits", Gender = "All", Root = "All", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Sweatpants & Joggers", Gender = "All", Root = "All", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Trousers & Wide Leg", Gender = "All", Root = "All", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "Active", Gender = "All", Root = "All", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Bermudas", Gender = "All", Root = "All", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Bikers", Gender = "All", Root = "All", NodeTitle = "Shorts" }, new SubRoots() { SubRoot = "Cargo", Gender = "All", Root = "All", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Chino", Gender = "All", Root = "All", NodeTitle = "Shorts" }, new SubRoots() { SubRoot = "Denim", Gender = "All", Root = "All", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Short Shorts", Gender = "All", Root = "All", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Skorts", Gender = "All", Root = "All", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "A Line", Gender = "All", Root = "All", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Asymmetrical", Gender = "All", Root = "All", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "High Low", Gender = "All", Root = "All", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Maxi", Gender = "All", Root = "All", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Midi", Gender = "All", Root = "All", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Mini", Gender = "All", Root = "All", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Pencil", Gender = "All", Root = "All", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Pleated", Gender = "All", Root = "All", NodeTitle = "Bottoms" } };
            List<SubRoots> subRootAllOtherClothing = new List<SubRoots>() { };

            nodeAll.Add(new Nodes() { NodeTitle = "Accessories", IsShowMore = true, Root = "All" }, subRootAllAccessories);
            nodeAll.Add(new Nodes() { NodeTitle = "Bags", IsShowMore = true, Root = "All" }, subRootAllBages);
            //nodeAll.Add(new Nodes() { NodeTitle = "Bottoms", IsShowMore = true, Root = "All" }, subRootAllBottoms);
            nodeAll.Add(new Nodes() { NodeTitle = "Dresses", IsShowMore = true, Root = "All" }, subRootAllDresses);
            nodeAll.Add(new Nodes() { NodeTitle = "Intimates & Sleepwear", IsShowMore = true, Root = "All" }, subRootAllIntimatesSleepwear);
            nodeAll.Add(new Nodes() { NodeTitle = "Jackets & Coats", IsShowMore = true, Root = "All" }, subRootAllJacketsCoats);
            nodeAll.Add(new Nodes() { NodeTitle = "Jeans", IsShowMore = true, Root = "All" }, subRootAllJeans);
            nodeAll.Add(new Nodes() { NodeTitle = "Jewelry", IsShowMore = true, Root = "All" }, subRootAllJewelry);
            nodeAll.Add(new Nodes() { NodeTitle = "Pants", IsShowMore = true, Root = "All" }, subRootAllPants);
            nodeAll.Add(new Nodes() { NodeTitle = "Shirts", IsShowMore = true, Root = "All" }, subRootAllShirts);
            if (Global.SearchedResultSelectedStore.ToLower() == Constant.ClothingStr.ToLower() || Global.SearchedResultSelectedStore.ToLower() == Constant.VintageStr.ToLower())
            {
                nodeAll.Add(new Nodes() { NodeTitle = "Shoes", IsShowMore = true, Root = "All" }, subRootAllShoes);
            }
            nodeAll.Add(new Nodes() { NodeTitle = "Shorts", IsShowMore = true, Root = "All" }, subRootAllShorts);
            nodeAll.Add(new Nodes() { NodeTitle = "Skirts", IsShowMore = true, Root = "All" }, subRootAllSkirts);
            //nodeAll.Add(new Nodes() { NodeTitle = "Sneakers", IsShowMore = true, Root = "All" }, subRootAllSneakers);
            if (Global.Storecategory == Constant.VintageStr)
            {
                //nodeMen.Add(new Nodes() { IsShowMore = false}, subRootMSocksandUnderwear);
            }
            else
            {
                nodeAll.Add(new Nodes() { NodeTitle = "Socks & Underwear", IsShowMore = true, Root = "All" }, subRootAllSocksandUnderwear);
            }
            nodeAll.Add(new Nodes() { NodeTitle = "Suits & Separates", IsShowMore = false, Root = "All" }, subRootAllSuitsSeparates);
            nodeAll.Add(new Nodes() { NodeTitle = "Suits & Tuxedos", IsShowMore = false, Root = "All" }, subRootAllSuitsTuxedos);
            nodeAll.Add(new Nodes() { NodeTitle = "Sweaters", IsShowMore = true, Root = "All" }, subRootAllSweaters);
            nodeAll.Add(new Nodes() { NodeTitle = "Swimsuits", IsShowMore = true, Root = "All" }, subRootAllSwimsuits);
            nodeAll.Add(new Nodes() { NodeTitle = "Swim", IsShowMore = true, Root = "All" }, subRootAllSwim);
            nodeAll.Add(new Nodes() { NodeTitle = "Tops", IsShowMore = true, Root = "All" }, subRootAllTops);
            nodeAll.Add(new Nodes() { NodeTitle = "Other", IsShowMore = false, Root = "All" }, subRootAllOtherClothing);
            #endregion

            //Category list for Women
            #region Category list for Women
            List<SubRoots> subRootWAccessories = new List<SubRoots>() { new SubRoots() { SubRoot = "Belts", Gender = "F", Root = "Women", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Electronics Cases", Gender = "F", Root = "Women", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Glasses", Gender = "F", Root = "Women", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Gloves & Mittens", Gender = "F", Root = "Women", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Hair Accessories", Gender = "F", Root = "Women", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Hats", Gender = "F", Root = "Women", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Hosiery", Gender = "F", Root = "Women", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Scarves & Wraps", Gender = "F", Root = "Women", NodeTitle = "Accessories" }, 
                //new SubRoots() { SubRoot = "Socks", Gender = "F", Root = "Women", NodeTitle = "Accessories" }, 
                new SubRoots() { SubRoot = "Sunglasses", Gender = "F", Root = "Women", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Umbrellas", Gender = "F", Root = "Women", NodeTitle = "Accessories" }, 
                //new SubRoots() { SubRoot = "Watches", Gender = "F", Root = "Women", NodeTitle = "Accessories" }
            };//, new SubRoots() { SubRoot = "Face Masks", Gender = "F", Root = "Women", NodeTitle = "Accessories" }
            List<SubRoots> subRootWBages = new List<SubRoots>() { new SubRoots() { SubRoot = "Baby Bags", Gender = "F", Root = "Women", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Backpacks", Gender = "F", Root = "Women", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Belt Bags", Gender = "F", Root = "Women", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Clutches & Wristlets", Gender = "F", Root = "Women", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Cosmetic Bags", Gender = "F", Root = "Women", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Crossbody Bags", Gender = "F", Root = "Women", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Hobos", Gender = "F", Root = "Women", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Laptop Bags", Gender = "F", Root = "Women", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Luggage & Travel Bags", Gender = "F", Root = "Women", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Satchels", Gender = "F", Root = "Women", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Shoulder Bags", Gender = "F", Root = "Women", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Totes", Gender = "F", Root = "Women", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Wallets", Gender = "F", Root = "Women", NodeTitle = "Bags" } };
            List<SubRoots> subRootWDresses = new List<SubRoots>() { new SubRoots() { SubRoot = "Asymmetrical", Gender = "F", Root = "Women", NodeTitle = "Dresses" }, new SubRoots() { SubRoot = "High Low", Gender = "F", Root = "Women", NodeTitle = "Dresses" }, new SubRoots() { SubRoot = "Long Sleeve", Gender = "F", Root = "Women", NodeTitle = "Dresses" }, new SubRoots() { SubRoot = "Maxi", Gender = "F", Root = "Women", NodeTitle = "Dresses" }, new SubRoots() { SubRoot = "Midi", Gender = "F", Root = "Women", NodeTitle = "Dresses" }, new SubRoots() { SubRoot = "Mini", Gender = "F", Root = "Women", NodeTitle = "Dresses" }, new SubRoots() { SubRoot = "Prom", Gender = "F", Root = "Women", NodeTitle = "Dresses" }, new SubRoots() { SubRoot = "Rompers", Gender = "F", Root = "Women", NodeTitle = "Dresses" }, new SubRoots() { SubRoot = "Strapless", Gender = "F", Root = "Women", NodeTitle = "Dresses" }, new SubRoots() { SubRoot = "Wedding", Gender = "F", Root = "Women", NodeTitle = "Dresses" } };
            List<SubRoots> subRootWIntimatesSleepwear = new List<SubRoots>() { new SubRoots() { SubRoot = "Bras", Gender = "F", Root = "Women", NodeTitle = "Intimates & Sleepwear" }, new SubRoots() { SubRoot = "Chemises & Slips", Gender = "F", Root = "Women", NodeTitle = "Intimates & Sleepwear" }, new SubRoots() { SubRoot = "Pajamas", Gender = "F", Root = "Women", NodeTitle = "Intimates & Sleepwear" }, new SubRoots() { SubRoot = "Panties", Gender = "F", Root = "Women", NodeTitle = "Intimates & Sleepwear" }, new SubRoots() { SubRoot = "Robes", Gender = "F", Root = "Women", NodeTitle = "Intimates & Sleepwear" }, new SubRoots() { SubRoot = "Shapewear", Gender = "F", Root = "Women", NodeTitle = "Intimates & Sleepwear" } };
            List<SubRoots> subRootWJacketsCoats = new List<SubRoots>() { new SubRoots() { SubRoot = "Active", Gender = "F", Root = "Women", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Blazers", Gender = "F", Root = "Women", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Bombers", Gender = "F", Root = "Women", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Capes & Ponchos", Gender = "F", Root = "Women", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Denim & Utility", Gender = "F", Root = "Women", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Down & Puffers", Gender = "F", Root = "Women", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Fleece", Gender = "F", Root = "Women", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Fur & Faux Fur", Gender = "F", Root = "Women", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Leather & Faux Leather", Gender = "F", Root = "Women", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Rain Coats", Gender = "F", Root = "Women", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Ski", Gender = "F", Root = "Women", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Teddy", Gender = "F", Root = "Women", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Trench Coats", Gender = "F", Root = "Women", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Varsity", Gender = "F", Root = "Women", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Vests", Gender = "F", Root = "Women", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Windbreakers", Gender = "F", Root = "Women", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Wool & Pea Coats", Gender = "F", Root = "Women", NodeTitle = "Jackets & Coats" } };
            if (Global.Storecategory == Constant.ClothingStr)
            {
                Global.CatBySubList = new List<SubRoots>() { new SubRoots() { SubRoot = "Bootcut", Gender = "F", Root = "Women", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Crooped & Ankle", Gender = "F", Root = "Women", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Flare & Wide Leg", Gender = "F", Root = "Women", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "High Rise", Gender = "F", Root = "Women", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Jeggings", Gender = "F", Root = "Women", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Relaxed", Gender = "F", Root = "Women", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Skinny", Gender = "F", Root = "Women", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Straight", Gender = "F", Root = "Women", NodeTitle = "Jeans" } };
            }
            else
            {
                Global.CatBySubList = new List<SubRoots>() { new SubRoots() { SubRoot = "Bootcut", Gender = "F", Root = "Women", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Crooped & Ankle", Gender = "F", Root = "Women", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Flare & Wide Leg", Gender = "F", Root = "Women", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "High Rise", Gender = "F", Root = "Women", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Jeggings", Gender = "F", Root = "Women", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Relaxed", Gender = "F", Root = "Women", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Skinny", Gender = "F", Root = "Women", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Straight", Gender = "F", Root = "Women", NodeTitle = "Jeans" } };
            }
            List<SubRoots> subRootWJeans = Global.CatBySubList;
            List<SubRoots> subRootWJewelry = new List<SubRoots>() { new SubRoots() { SubRoot = "Bracelets", Gender = "F", Root = "Women", NodeTitle = "Jewelry" }, new SubRoots() { SubRoot = "Brooches", Gender = "F", Root = "Women", NodeTitle = "Jewelry" }, new SubRoots() { SubRoot = "Earrings", Gender = "F", Root = "Women", NodeTitle = "Jewelry" }, new SubRoots() { SubRoot = "Necklaces", Gender = "F", Root = "Women", NodeTitle = "Jewelry" }, new SubRoots() { SubRoot = "Rings", Gender = "F", Root = "Women", NodeTitle = "Jewelry" }, new SubRoots() { SubRoot = "Watches", Gender = "F", Root = "Women", NodeTitle = "Jewelry" } };
            List<SubRoots> subRootWPants = new List<SubRoots>() { new SubRoots() { SubRoot = "Bootcut & Flare", Gender = "F", Root = "Women", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "Crops & Capris", Gender = "F", Root = "Women", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "High Waisted", Gender = "F", Root = "Women", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "Leggings", Gender = "F", Root = "Women", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "Overalls & Jumpsuits", Gender = "F", Root = "Women", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "Skinny", Gender = "F", Root = "Women", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "Straight", Gender = "F", Root = "Women", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "Sweatpants & Joggers", Gender = "F", Root = "Women", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "Trousers & Wide Leg", Gender = "F", Root = "Women", NodeTitle = "Pants" } };
            List<SubRoots> subRootWShoes = new List<SubRoots>() { new SubRoots() { SubRoot = "Boots & Booties", Gender = "F", Root = "Women", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Clogs & Mules", Gender = "F", Root = "Women", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Espadrilles", Gender = "F", Root = "Women", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Flats & Loafers", Gender = "F", Root = "Women", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Heels", Gender = "F", Root = "Women", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Narrow", Gender = "F", Root = "Women", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Sandals", Gender = "F", Root = "Women", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Single Shoes", Gender = "F", Root = "Women", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Slippers", Gender = "F", Root = "Women", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Wedges", Gender = "F", Root = "Women", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Wide", Gender = "F", Root = "Women", NodeTitle = "Shoes" } };
            List<SubRoots> subRootWShorts = new List<SubRoots>() { new SubRoots() { SubRoot = "Active", Gender = "F", Root = "Women", NodeTitle = "Shorts" }, new SubRoots() { SubRoot = "Bermudas", Gender = "F", Root = "Women", NodeTitle = "Shorts" }, new SubRoots() { SubRoot = "Bikers", Gender = "F", Root = "Women", NodeTitle = "Shorts" }, new SubRoots() { SubRoot = "Cargo", Gender = "F", Root = "Women", NodeTitle = "Shorts" }, new SubRoots() { SubRoot = "Chino", Gender = "F", Root = "Women", NodeTitle = "Shorts" }, new SubRoots() { SubRoot = "Denim", Gender = "F", Root = "Women", NodeTitle = "Shorts" }, new SubRoots() { SubRoot = "High Waist", Gender = "F", Root = "Women", NodeTitle = "Shorts" }, new SubRoots() { SubRoot = "Short Shorts", Gender = "F", Root = "Women", NodeTitle = "Shorts" }, new SubRoots() { SubRoot = "Skorts", Gender = "F", Root = "Women", NodeTitle = "Shorts" } };
            List<SubRoots> subRootWSkirts = new List<SubRoots>() { new SubRoots() { SubRoot = "A Line", Gender = "F", Root = "Women", NodeTitle = "Skirts" }, new SubRoots() { SubRoot = "Asymmetrical", Gender = "F", Root = "Women", NodeTitle = "Skirts" }, new SubRoots() { SubRoot = "Denim", Gender = "F", Root = "Women", NodeTitle = "Skirts" }, new SubRoots() { SubRoot = "High Low", Gender = "F", Root = "Women", NodeTitle = "Skirts" }, new SubRoots() { SubRoot = "Maxi", Gender = "F", Root = "Women", NodeTitle = "Skirts" }, new SubRoots() { SubRoot = "Midi", Gender = "F", Root = "Women", NodeTitle = "Skirts" }, new SubRoots() { SubRoot = "Mini", Gender = "F", Root = "Women", NodeTitle = "Skirts" }, new SubRoots() { SubRoot = "Pencil", Gender = "F", Root = "Women", NodeTitle = "Skirts" }, new SubRoots() { SubRoot = "Pleated", Gender = "F", Root = "Women", NodeTitle = "Skirts" } };
            //List<SubRoots> subRootWSneakers = new List<SubRoots>() { new SubRoots() { SubRoot = "Basketball", Gender = "F", Root = "Women", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Casual", Gender = "F", Root = "Women", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Luxury", Gender = "F", Root = "Women", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Running", Gender = "F", Root = "Women", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Soccer", Gender = "F", Root = "Women", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Skateboard", Gender = "F", Root = "Women", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Slides", Gender = "F", Root = "Women", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Tennis", Gender = "F", Root = "Women", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Track & Field", Gender = "F", Root = "Women", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Training", Gender = "F", Root = "Women", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Walking", Gender = "F", Root = "Women", NodeTitle = "Sneakers" } };
            List<SubRoots> subRootWSuitsSeparates = new List<SubRoots>() { };
            List<SubRoots> subRootWSweaters = new List<SubRoots>() { new SubRoots() { SubRoot = "Cardigans", Gender = "F", Root = "Women", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Cashmere", Gender = "F", Root = "Women", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Cold Shoulder", Gender = "F", Root = "Women", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Cowl & Turtle Neck", Gender = "F", Root = "Women", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Crew & Scoop Necks", Gender = "F", Root = "Women", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Cropped", Gender = "F", Root = "Women", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Hoodie", Gender = "F", Root = "Women", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Ponchos", Gender = "F", Root = "Women", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Shrugs", Gender = "F", Root = "Women", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Tunic", Gender = "F", Root = "Women", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Vest", Gender = "F", Root = "Women", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "V-Neck", Gender = "F", Root = "Women", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Wrap", Gender = "F", Root = "Women", NodeTitle = "Sweaters" } };
            List<SubRoots> subRootWSwimsuits = new List<SubRoots>() { new SubRoots() { SubRoot = "Bikinis", Gender = "F", Root = "Women", NodeTitle = "Swimsuits" }, new SubRoots() { SubRoot = "Cover-Ups", Gender = "F", Root = "Women", NodeTitle = "Swimsuits" }, new SubRoots() { SubRoot = "One Pieces", Gender = "F", Root = "Women", NodeTitle = "Swimsuits" }, new SubRoots() { SubRoot = "Tankinis", Gender = "F", Root = "Women", NodeTitle = "Swimsuits" } };
            //List<SubRoots> subRootWTops = new List<SubRoots>() { new SubRoots() { SubRoot = "Blouses", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Bodysuits", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Button Down Shirts", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Cowl & Turtleneck", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Crop Tops", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Sweatshirts & Hoodies", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Tank Tops", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Tees: Long Sleeve", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Tees: Short Sleeve", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Tunics", Gender = "F", Root = "Women", NodeTitle = "Tops" } };
            List<SubRoots> subRootWTops = new List<SubRoots>() { new SubRoots() { SubRoot = "Blouses", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Bodysuits", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Button Down Shirts", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Camisoles", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Cardigans and Shrugs", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Cowl & Turtleneck", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Crop Tops", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Jerseys", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Muscle Tees", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Sweatshirts & Hoodies", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Tank Tops", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Tees: Long Sleeve", Gender = "F", Root = "Women", NodeTitle = "Tops" }, new SubRoots() { SubRoot = "Tees: Short Sleeve", Gender = "F", Root = "Women", NodeTitle = "Tops" } };
            List<SubRoots> subRootWBottoms = new List<SubRoots>() { new SubRoots() { SubRoot = "Bootcut", Gender = "F", Root = "Women", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Crooped & Ankle", Gender = "F", Root = "Women", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Flare & Wide Leg", Gender = "F", Root = "Women", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "High Rise", Gender = "F", Root = "Women", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Jeggings", Gender = "F", Root = "Women", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Relaxed", Gender = "F", Root = "Women", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Skinny", Gender = "F", Root = "Women", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Straight", Gender = "F", Root = "Women", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Tunics", Gender = "F", Root = "Women", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Bootcut & Flare", Gender = "F", Root = "Women", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Crops & Capris", Gender = "F", Root = "Women", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "High Waisted", Gender = "F", Root = "Women", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Overalls & Jumpsuits", Gender = "F", Root = "Women", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Sweatpants & Joggers", Gender = "F", Root = "Women", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Trousers & Wide Leg", Gender = "F", Root = "Women", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "Active", Gender = "F", Root = "Women", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Bermudas", Gender = "F", Root = "Women", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Bikers", Gender = "F", Root = "Women", NodeTitle = "Shorts" }, new SubRoots() { SubRoot = "Cargo", Gender = "F", Root = "Women", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Chino", Gender = "F", Root = "Women", NodeTitle = "Shorts" }, new SubRoots() { SubRoot = "Denim", Gender = "F", Root = "Women", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Short Shorts", Gender = "F", Root = "Women", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Skorts", Gender = "F", Root = "Women", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "A Line", Gender = "F", Root = "Women", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Asymmetrical", Gender = "F", Root = "Women", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "High Low", Gender = "F", Root = "Women", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Maxi", Gender = "F", Root = "Women", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Midi", Gender = "F", Root = "Women", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Mini", Gender = "F", Root = "Women", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Pencil", Gender = "F", Root = "Women", NodeTitle = "Bottoms" }, new SubRoots() { SubRoot = "Pleated", Gender = "F", Root = "Women", NodeTitle = "Bottoms" } };
            List<SubRoots> subRootWOtherClothing = new List<SubRoots>() { };

            Dictionary<Nodes, List<SubRoots>> nodeW = new Dictionary<Nodes, List<SubRoots>>();

            nodeW.Add(new Nodes() { NodeTitle = "Accessories", IsShowMore = true, Root = "Women" }, subRootWAccessories);
            nodeW.Add(new Nodes() { NodeTitle = "Bags", IsShowMore = true, Root = "Women" }, subRootWBages);
            nodeW.Add(new Nodes() { NodeTitle = "Dresses", IsShowMore = true, Root = "Women" }, subRootWDresses);
            nodeW.Add(new Nodes() { NodeTitle = "Intimates & Sleepwear", IsShowMore = true, Root = "Women" }, subRootWIntimatesSleepwear);
            nodeW.Add(new Nodes() { NodeTitle = "Jackets & Coats", IsShowMore = true, Root = "Women" }, subRootWJacketsCoats);
            nodeW.Add(new Nodes() { NodeTitle = "Jeans", IsShowMore = true, Root = "Women" }, subRootWJeans);
            nodeW.Add(new Nodes() { NodeTitle = "Jewelry", IsShowMore = true, Root = "Women" }, subRootWJewelry);
            nodeW.Add(new Nodes() { NodeTitle = "Pants", IsShowMore = true, Root = "Women" }, subRootWPants);
            if (Global.SearchedResultSelectedStore.ToLower() == Constant.ClothingStr.ToLower() || Global.SearchedResultSelectedStore.ToLower() == Constant.VintageStr.ToLower())
            {
                nodeW.Add(new Nodes() { NodeTitle = "Shoes", IsShowMore = true, Root = "Women" }, subRootWShoes);
            }
            nodeW.Add(new Nodes() { NodeTitle = "Shorts", IsShowMore = true, Root = "Women" }, subRootWShorts);
            nodeW.Add(new Nodes() { NodeTitle = "Skirts", IsShowMore = true, Root = "Women" }, subRootWSkirts);
            //nodeW.Add(new Nodes() { NodeTitle = "Sneakers", IsShowMore = true, Root = "Women" }, subRootWSneakers);
            nodeW.Add(new Nodes() { NodeTitle = "Suits & Separates", IsShowMore = false, Root = "Women" }, subRootWSuitsSeparates);
            nodeW.Add(new Nodes() { NodeTitle = "Sweaters", IsShowMore = true, Root = "Women" }, subRootWSweaters);
            nodeW.Add(new Nodes() { NodeTitle = "Swimsuits", IsShowMore = true, Root = "Women" }, subRootWSwimsuits);
            nodeW.Add(new Nodes() { NodeTitle = "Tops", IsShowMore = true, Root = "Women" }, subRootWTops);
            //nodeW.Add(new Nodes() { NodeTitle = "Bottoms", IsShowMore = true, Root = "Women" }, subRootWBottoms);
            nodeW.Add(new Nodes() { NodeTitle = "Other", IsShowMore = false, Root = "Women" }, subRootWOtherClothing);
            #endregion

            //Category list for Men
            #region Category list for Men
            if (Global.Storecategory == Constant.StreetwearStr)
            {
                Global.CatBySubList = new List<SubRoots>() { new SubRoots() { SubRoot = "Belts", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Electronics Cases", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Glasses", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Gloves & Scarves", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Hats", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Jewelry & Watches", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Pajamas & Robes", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Sunglasses", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Ties & Pocket Squares", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Umbrellas", Gender = "M", Root = "Men", NodeTitle = "Accessories" } };//, new SubRoots() { SubRoot = "Face Masks", Gender = "M", Root = "Men", NodeTitle = "Accessories" }
            }
            else
            {
                Global.CatBySubList = new List<SubRoots>() { new SubRoots() { SubRoot = "Belts", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Electronics Cases", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Glasses", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Gloves & Scarves", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Hats", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Jewelry & Watches", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Pajamas & Robes", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Sunglasses", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Ties & Pocket Squares", Gender = "M", Root = "Men", NodeTitle = "Accessories" }, new SubRoots() { SubRoot = "Umbrellas", Gender = "M", Root = "Men", NodeTitle = "Accessories" } };//, new SubRoots() { SubRoot = "Face Masks", Gender = "M", Root = "Men", NodeTitle = "Accessories" }
            }
            List<SubRoots> subRootMAccessories = Global.CatBySubList;
            List<SubRoots> subRootMBages = new List<SubRoots>() { new SubRoots() { SubRoot = "Backpacks", Gender = "M", Root = "Men", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Belt Bags", Gender = "M", Root = "Men", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Briefcases", Gender = "M", Root = "Men", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Duffle Bags", Gender = "M", Root = "Men", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Laptop Bags", Gender = "M", Root = "Men", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Luggage & Travel Bags", Gender = "M", Root = "Men", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Messenger Bags", Gender = "M", Root = "Men", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Toiletry Bags", Gender = "M", Root = "Men", NodeTitle = "Bags" }, new SubRoots() { SubRoot = "Wallets", Gender = "M", Root = "Men", NodeTitle = "Bags" } };
            if (Global.Storecategory == Constant.StreetwearStr)
            {
                //Global.CatBySubList = new List<SubRoots>() { new SubRoots() { SubRoot = "Active", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Blazers", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Bombers", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Capes", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Denim & Utility", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Down & Puffers", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Fleece", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Fur & Faux Fur", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Leather & Faux Leather", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Rain Coats", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Ski", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Sport Coats", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Trench Coats", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Varsity", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Vests", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Wool & Pea Coats ", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" } };
                Global.CatBySubList = new List<SubRoots>() { new SubRoots() { SubRoot = "Active", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Blazers", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Bombers", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Capes", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Denim & Utility", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Down & Puffers", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Fleece", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Fur & Faux Fur", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Leather & Faux Leather", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Rain Coats", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Ski", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Trench Coats", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Varsity", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Vests", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Wool & Pea Coats ", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" } };
            }
            else
            {
                //Global.CatBySubList = new List<SubRoots>() { new SubRoots() { SubRoot = "Active", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Blazers", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Bombers", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Capes", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Denim & Utility", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Down & Puffers", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Fleece", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Fur & Faux Fur", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Leather & Faux Leather", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Rain Coats", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Ski", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Sport Coats", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Trench Coats", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Varsity", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Vests", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Windbreakers", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Wool & Pea Coats ", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" } };
                Global.CatBySubList = new List<SubRoots>() { new SubRoots() { SubRoot = "Active", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Blazers", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Bombers", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Capes", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Denim & Utility", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Down & Puffers", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Fleece", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Fur & Faux Fur", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Leather & Faux Leather", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Rain Coats", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Ski", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Trench Coats", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Varsity", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Vests", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Windbreakers", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" }, new SubRoots() { SubRoot = "Wool & Pea Coats ", Gender = "M", Root = "Men", NodeTitle = "Jackets & Coats" } };
            }
            List<SubRoots> subRootMJacketsCoats = Global.CatBySubList;
            List<SubRoots> subRootMJeans = new List<SubRoots>() { new SubRoots() { SubRoot = "Bootcut", Gender = "M", Root = "Men", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Relaxed", Gender = "M", Root = "Men", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Skinny", Gender = "M", Root = "Men", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Slim", Gender = "M", Root = "Men", NodeTitle = "Jeans" }, new SubRoots() { SubRoot = "Straight", Gender = "M", Root = "Men", NodeTitle = "Jeans" } };
            List<SubRoots> subRootMPants = new List<SubRoots>() { new SubRoots() { SubRoot = "Cargo", Gender = "M", Root = "Men", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "Chinos & Khakis", Gender = "M", Root = "Men", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "Corduroy", Gender = "M", Root = "Men", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "Dress", Gender = "M", Root = "Men", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "Leggings", Gender = "M", Root = "Men", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "Overalls & Jumpsuits", Gender = "M", Root = "Men", NodeTitle = "Pants" }, new SubRoots() { SubRoot = "Sweatpants & Joggers", Gender = "M", Root = "Men", NodeTitle = "Pants" } };
            if (Global.Storecategory == Constant.StreetwearStr)
            {
                Global.CatBySubList = new List<SubRoots>() { new SubRoots() { SubRoot = "Casual Button Downs", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Dress Shirts", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Jerseys", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Polos", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Sweatshirts & Hoodies", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Tank Tops", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Tees: Long Sleeve", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Tees: Short Sleeve", Gender = "M", Root = "Men", NodeTitle = "Shirts" } };
            }
            else if (Global.Storecategory != Constant.VintageStr)
            {

                Global.CatBySubList = new List<SubRoots>() { new SubRoots() { SubRoot = "Casual Button Downs", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Dress Shirts", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Jerseys", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Polos", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Sweatshirts & Hoodies", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Tank Tops", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Tees: Long Sleeve", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Tees: Short Sleeve", Gender = "M", Root = "Men", NodeTitle = "Shirts" } };
            }
            if (Global.Storecategory == Constant.VintageStr)
            {
                Global.CatBySubList = new List<SubRoots>() { new SubRoots() { SubRoot = "Casual Button Downs", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Dress Shirts", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Polos", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Sweatshirts & Hoodies", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Tank Tops", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Tees: Long Sleeve", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Tees: Short Sleeve", Gender = "M", Root = "Men", NodeTitle = "Shirts" } };
            }
            else if (Global.Storecategory != Constant.StreetwearStr)
            {

                Global.CatBySubList = new List<SubRoots>() { new SubRoots() { SubRoot = "Casual Button Downs", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Dress Shirts", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Jerseys", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Polos", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Sweatshirts & Hoodies", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Tank Tops", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Tees: Long Sleeve", Gender = "M", Root = "Men", NodeTitle = "Shirts" }, new SubRoots() { SubRoot = "Tees: Short Sleeve", Gender = "M", Root = "Men", NodeTitle = "Shirts" } };
            }
            List<SubRoots> subRootMShirts = Global.CatBySubList;
            List<SubRoots> subRootMShoes = new List<SubRoots>() { new SubRoots() { SubRoot = "Boat Shoes", Gender = "M", Root = "Men", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Boots", Gender = "M", Root = "Men", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Casual Shoes", Gender = "M", Root = "Men", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Dress Shoes", Gender = "M", Root = "Men", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Loafers & Drivers", Gender = "M", Root = "Men", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Sandals & Flip Flops", Gender = "M", Root = "Men", NodeTitle = "Shoes" }, new SubRoots() { SubRoot = "Slippers", Gender = "M", Root = "Men", NodeTitle = "Shoes" } };
            List<SubRoots> subRootMShorts = new List<SubRoots>() { new SubRoots() { SubRoot = "Athletic", Gender = "M", Root = "Men", NodeTitle = "Shorts" }, new SubRoots() { SubRoot = "Cargo", Gender = "M", Root = "Men", NodeTitle = "Shorts" }, new SubRoots() { SubRoot = "Chino", Gender = "M", Root = "Men", NodeTitle = "Shorts" }, new SubRoots() { SubRoot = "Denim", Gender = "M", Root = "Men", NodeTitle = "Shorts" }, new SubRoots() { SubRoot = "Hybrids", Gender = "M", Root = "Men", NodeTitle = "Shorts" } };
            //List<SubRoots> subRootMSneakers = new List<SubRoots>() { new SubRoots() { SubRoot = "Basketball", Gender = "M", Root = "Men", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Casual", Gender = "M", Root = "Men", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Luxury", Gender = "M", Root = "Men", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Running", Gender = "M", Root = "Men", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Soccer", Gender = "M", Root = "Men", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Skateboard", Gender = "M", Root = "Men", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Slides", Gender = "M", Root = "Men", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Tennis", Gender = "M", Root = "Men", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Track & Field", Gender = "M", Root = "Men", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Training", Gender = "M", Root = "Men", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Walking", Gender = "M", Root = "Men", NodeTitle = "Sneakers" } };
            List<SubRoots> subRootMSuitsTuxedos = new List<SubRoots>() { };
            if (Global.Storecategory == Constant.VintageStr)
            {
                Global.CatBySubList = new List<SubRoots>() { new SubRoots() { SubRoot = "Cardigans", Gender = "M", Root = "Men", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Cashmere", Gender = "M", Root = "Men", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Hoodie", Gender = "M", Root = "Men", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Turtlenecks", Gender = "M", Root = "Men", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Vest", Gender = "M", Root = "Men", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "V-Neck", Gender = "M", Root = "Men", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Zip-Up", Gender = "M", Root = "Men", NodeTitle = "Sweaters" } };
            }
            else
            {
                Global.CatBySubList = new List<SubRoots>() { new SubRoots() { SubRoot = "Cardigans", Gender = "M", Root = "Men", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Cashmere", Gender = "M", Root = "Men", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Crewnecks", Gender = "M", Root = "Men", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Hoodie", Gender = "M", Root = "Men", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Turtlenecks", Gender = "M", Root = "Men", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Vest", Gender = "M", Root = "Men", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "V-Neck", Gender = "M", Root = "Men", NodeTitle = "Sweaters" }, new SubRoots() { SubRoot = "Zip-Up", Gender = "M", Root = "Men", NodeTitle = "Sweaters" } };
            }
            List<SubRoots> subRootMSweaters = Global.CatBySubList;
            List<SubRoots> subRootMSocksandUnderwear = new List<SubRoots>() { new SubRoots() { SubRoot = "Athletic Socks", Gender = "M", Root = "Men", NodeTitle = "Socks & Underwear" }, new SubRoots() { SubRoot = "Boxer Briefs", Gender = "M", Root = "Men", NodeTitle = "Socks & Underwear" }, new SubRoots() { SubRoot = "Boxers", Gender = "M", Root = "Men", NodeTitle = "Socks & Underwear" }, new SubRoots() { SubRoot = "Briefs", Gender = "M", Root = "Men", NodeTitle = "Socks & Underwear" }, new SubRoots() { SubRoot = "Casual Socks", Gender = "M", Root = "Men", NodeTitle = "Socks & Underwear" }, new SubRoots() { SubRoot = "Dress Socks", Gender = "M", Root = "Men", NodeTitle = "Socks & Underwear" }, new SubRoots() { SubRoot = "Undershirts", Gender = "M", Root = "Men", NodeTitle = "Socks & Underwear" } };
            List<SubRoots> subRootMSwim = new List<SubRoots>() { new SubRoots() { SubRoot = "Board Shorts", Gender = "M", Root = "Men", NodeTitle = "Swim" }, new SubRoots() { SubRoot = "Hybrids", Gender = "M", Root = "Men", NodeTitle = "Swim" }, new SubRoots() { SubRoot = "Rash Guard", Gender = "M", Root = "Men", NodeTitle = "Swim" }, new SubRoots() { SubRoot = "Swim Trunks", Gender = "M", Root = "Men", NodeTitle = "Swim" } };
            List<SubRoots> subRootMOtherClothing = new List<SubRoots>() { };

            Dictionary<Nodes, List<SubRoots>> nodeMen = new Dictionary<Nodes, List<SubRoots>>();

            nodeMen.Add(new Nodes() { NodeTitle = "Accessories", IsShowMore = true, Root = "Men" }, subRootMAccessories);
            nodeMen.Add(new Nodes() { NodeTitle = "Bags", IsShowMore = true, Root = "Men" }, subRootMBages);
            nodeMen.Add(new Nodes() { NodeTitle = "Jackets & Coats", IsShowMore = true, Root = "Men" }, subRootMJacketsCoats);
            nodeMen.Add(new Nodes() { NodeTitle = "Jeans", IsShowMore = true, Root = "Men" }, subRootMJeans);
            nodeMen.Add(new Nodes() { NodeTitle = "Pants", IsShowMore = true, Root = "Men" }, subRootMPants);
            nodeMen.Add(new Nodes() { NodeTitle = "Shirts", IsShowMore = true, Root = "Men" }, subRootMShirts);
            if (Global.SearchedResultSelectedStore.ToLower() == Constant.ClothingStr.ToLower() || Global.SearchedResultSelectedStore.ToLower() == Constant.VintageStr.ToLower())
            {
                nodeMen.Add(new Nodes() { NodeTitle = "Shoes", IsShowMore = true, Root = "Men" }, subRootMShoes);
            }
            nodeMen.Add(new Nodes() { NodeTitle = "Shorts", IsShowMore = true, Root = "Men" }, subRootMShorts);
            //nodeMen.Add(new Nodes() { NodeTitle = "Sneakers", IsShowMore = true, Root = "Men" }, subRootMSneakers);
            if (Global.Storecategory == Constant.VintageStr)
            {
                //nodeMen.Add(new Nodes() { IsShowMore = false}, subRootMSocksandUnderwear);
            }
            else
            {
                nodeMen.Add(new Nodes() { NodeTitle = "Socks & Underwear", IsShowMore = true, Root = "Men" }, subRootMSocksandUnderwear);
            }
            nodeMen.Add(new Nodes() { NodeTitle = "Suits & Tuxedos", IsShowMore = false, Root = "Men" }, subRootMSuitsTuxedos);
            nodeMen.Add(new Nodes() { NodeTitle = "Sweaters", IsShowMore = true, Root = "Men" }, subRootMSweaters);
            nodeMen.Add(new Nodes() { NodeTitle = "Swim", IsShowMore = true, Root = "Men" }, subRootMSwim);
            nodeMen.Add(new Nodes() { NodeTitle = "Other", IsShowMore = false, Root = "Men" }, subRootMOtherClothing);
            #endregion

            roots = new List<Roots>()
            {
                new Roots(){ Root="All", Node=nodeAll},
                new Roots(){ Root="Women", Node=nodeW},
                new Roots(){ Root="Men", Node=nodeMen}
            };

            if (selectedsubcat != null && selectedcat != null)
            {
                if (selectedsubcat.ToLower().Contains("COATS".ToLower()))
                {
                    selectedsubcat = "JACKETS & COATS";
                }
                if (selectedsubcat.ToLower().Contains("SUITS".ToLower()))
                {
                    selectedsubcat = "SUITS & TUXEDOS";
                }
                if (selectedsubcat.ToLower().Contains("SOCKS".ToLower()))
                {
                    selectedsubcat = "socks & underwear";
                }
                if (selectedsubcat.ToLower().Contains("Swim".ToLower()) && selectedcat.Contains("Women"))
                {
                    selectedsubcat = "Swimsuits";
                }
                var selectedroot = roots.Where(x => x.Root.ToLower() == selectedcat.ToLower()).ToList();
                var subRootsList = selectedroot.Select(r => r.Node.Keys.Where(k => k.NodeTitle.ToLower() == selectedsubcat.ToLower()).FirstOrDefault()).FirstOrDefault();
                var res = selectedroot.Select(x => x.Node[subRootsList]).ToList().FirstOrDefault();

                List<SubRoots> subRoots = new List<SubRoots>();
                Dictionary<Nodes, List<SubRoots>> SubnodeDicn = new Dictionary<Nodes, List<SubRoots>>();
                foreach (var item in res)
                {
                    var node = new Nodes()
                    {
                        IsShowMore = item.IsShowMore,
                        NodeTitle = item.SubRoot,
                        Gender = item.Gender
                    };
                    SubnodeDicn.Add(node, subRoots);
                }

                List<Roots> rootsa = new List<Roots>
                {
                    new Roots()
                    {
                        Root = selectedsubcat,
                        Node = SubnodeDicn
                    }
                };
                return Global.RootsList = rootsa;
            }
            else if (selectedcat != null)
            {
                //return Global.RootsList = roots.Where(x => x.Root.ToLower() == selectedcat.ToLower()).ToList();
                return Global.RootsList = roots.ToList();
            }
            else
            {
                Global.RootsList = roots;
            }
            return roots;
        }
        #endregion

        #region Common Method for Sneakers to show all categories inside All stores (Clothing, Sneakers, Streetwear, Vintage)
        public List<Roots> GetCommonSneakersCateListForAllStores(string selectedcat, string selectedsubcat)
        {
            List<Roots> roots;

            //Categories list for All
            #region Category list for All
            Dictionary<Nodes, List<SubRoots>> nodeAll = new Dictionary<Nodes, List<SubRoots>>();
            Dictionary<Nodes, List<SubRoots>> nodeW = new Dictionary<Nodes, List<SubRoots>>();
            Dictionary<Nodes, List<SubRoots>> nodeMen = new Dictionary<Nodes, List<SubRoots>>();

            List<SubRoots> subRootAllBasketball = new List<SubRoots>() { };
            List<SubRoots> subRootAllCasual = new List<SubRoots>() { };
            List<SubRoots> subRootAllLuxury = new List<SubRoots>() { };
            List<SubRoots> subRootAllRunning = new List<SubRoots>() { };
            List<SubRoots> subRootAllSoccer = new List<SubRoots>() { };
            List<SubRoots> subRootAllSkateboard = new List<SubRoots>() { };
            List<SubRoots> subRootAllSlides = new List<SubRoots>() { };
            List<SubRoots> subRootAllTennis = new List<SubRoots>() { };
            List<SubRoots> subRootAllTrackField = new List<SubRoots>() { };
            List<SubRoots> subRootAllTraining = new List<SubRoots>() { };
            List<SubRoots> subRootAllWalking = new List<SubRoots>() { };

            nodeAll.Add(new Nodes() { NodeTitle = "Basketball", Gender = "All", IsShowMore = false, Root = "All" }, subRootAllBasketball);
            nodeAll.Add(new Nodes() { NodeTitle = "Casual", Gender = "All", IsShowMore = false, Root = "All" }, subRootAllCasual);
            nodeAll.Add(new Nodes() { NodeTitle = "Luxury", Gender = "All", IsShowMore = false, Root = "All" }, subRootAllLuxury);
            nodeAll.Add(new Nodes() { NodeTitle = "Running", Gender = "All", IsShowMore = false, Root = "All" }, subRootAllRunning);
            nodeAll.Add(new Nodes() { NodeTitle = "Soccer", Gender = "All", IsShowMore = false, Root = "All" }, subRootAllSoccer);
            nodeAll.Add(new Nodes() { NodeTitle = "Skateboard", Gender = "All", IsShowMore = false, Root = "All" }, subRootAllSkateboard);
            nodeAll.Add(new Nodes() { NodeTitle = "Slides", Gender = "All", IsShowMore = false, Root = "All" }, subRootAllSlides);
            nodeAll.Add(new Nodes() { NodeTitle = "Tennis", Gender = "All", IsShowMore = false, Root = "All" }, subRootAllTennis);
            nodeAll.Add(new Nodes() { NodeTitle = "Track & Field", Gender = "All", IsShowMore = false, Root = "All" }, subRootAllTrackField);
            nodeAll.Add(new Nodes() { NodeTitle = "Training", Gender = "All", IsShowMore = false, Root = "All" }, subRootAllTraining);
            nodeAll.Add(new Nodes() { NodeTitle = "Walking", Gender = "All", IsShowMore = false, Root = "All" }, subRootAllWalking);
            #endregion

            // Categories list for Women
            #region Category list for Women
            List<SubRoots> subRootWBasketball = new List<SubRoots>() { };
            List<SubRoots> subRootWCasual = new List<SubRoots>() { };
            List<SubRoots> subRootWLuxury = new List<SubRoots>() { };
            List<SubRoots> subRootWRunning = new List<SubRoots>() { };
            List<SubRoots> subRootWSoccer = new List<SubRoots>() { };
            List<SubRoots> subRootWSkateboard = new List<SubRoots>() { };
            List<SubRoots> subRootWSlides = new List<SubRoots>() { };
            List<SubRoots> subRootWTennis = new List<SubRoots>() { };
            List<SubRoots> subRootWTrackField = new List<SubRoots>() { };
            List<SubRoots> subRootWTraining = new List<SubRoots>() { };
            List<SubRoots> subRootWWalking = new List<SubRoots>() { };

            //List<SubRoots> subRootWSneakers = new List<SubRoots>() { new SubRoots() { SubRoot = "Basketball", Gender = "F", Root = "Women", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Casual", Gender = "F", Root = "Women", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Luxury", Gender = "F", Root = "Women", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Running", Gender = "F", Root = "Women", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Soccer", Gender = "F", Root = "Women", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Skateboard", Gender = "F", Root = "Women", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Slides", Gender = "F", Root = "Women", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Tennis", Gender = "F", Root = "Women", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Track & Field", Gender = "F", Root = "Women", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Training", Gender = "F", Root = "Women", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Walking", Gender = "F", Root = "Women", NodeTitle = "Sneakers" } };

            //nodeW.Add(new Nodes() { NodeTitle = "Sneakers", Gender = "F", IsShowMore = true, Root = "Women" }, subRootWSneakers);

            nodeW.Add(new Nodes() { NodeTitle = "Basketball", Gender = "F", IsShowMore = false, Root = "Women" }, subRootWBasketball);
            nodeW.Add(new Nodes() { NodeTitle = "Casual", Gender = "F", IsShowMore = false, Root = "Women" }, subRootWCasual);
            nodeW.Add(new Nodes() { NodeTitle = "Luxury", Gender = "F", IsShowMore = false, Root = "Women" }, subRootWLuxury);
            nodeW.Add(new Nodes() { NodeTitle = "Running", Gender = "F", IsShowMore = false, Root = "Women" }, subRootWRunning);
            nodeW.Add(new Nodes() { NodeTitle = "Soccer", Gender = "F", IsShowMore = false, Root = "Women" }, subRootWSoccer);
            nodeW.Add(new Nodes() { NodeTitle = "Skateboard", Gender = "F", IsShowMore = false, Root = "Women" }, subRootWSkateboard);
            nodeW.Add(new Nodes() { NodeTitle = "Slides", Gender = "F", IsShowMore = false, Root = "Women" }, subRootWSlides);
            nodeW.Add(new Nodes() { NodeTitle = "Tennis", Gender = "F", IsShowMore = false, Root = "Women" }, subRootWTennis);
            nodeW.Add(new Nodes() { NodeTitle = "Track & Field", Gender = "F", IsShowMore = false, Root = "Women" }, subRootWTrackField);
            nodeW.Add(new Nodes() { NodeTitle = "Training", Gender = "F", IsShowMore = false, Root = "Women" }, subRootWTraining);
            nodeW.Add(new Nodes() { NodeTitle = "Walking", Gender = "F", IsShowMore = false, Root = "Women" }, subRootWWalking);
            #endregion

            // Categories list for Men
            #region Category list for Men
            List<SubRoots> subRootMBasketball = new List<SubRoots>() { };
            List<SubRoots> subRootMCasual = new List<SubRoots>() { };
            List<SubRoots> subRootMLuxury = new List<SubRoots>() { };
            List<SubRoots> subRootMRunning = new List<SubRoots>() { };
            List<SubRoots> subRootMSoccer = new List<SubRoots>() { };
            List<SubRoots> subRootMSkateboard = new List<SubRoots>() { };
            List<SubRoots> subRootMSlides = new List<SubRoots>() { };
            List<SubRoots> subRootMTennis = new List<SubRoots>() { };
            List<SubRoots> subRootMTrackField = new List<SubRoots>() { };
            List<SubRoots> subRootMTraining = new List<SubRoots>() { };
            List<SubRoots> subRootMWalking = new List<SubRoots>() { };

            //List<SubRoots> subRootMSneakers = new List<SubRoots>() { new SubRoots() { SubRoot = "Basketball", Gender = "M", Root = "Men", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Casual", Gender = "M", Root = "Men", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Luxury", Gender = "M", Root = "Men", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Running", Gender = "M", Root = "Men", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Soccer", Gender = "M", Root = "Men", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Skateboard", Gender = "M", Root = "Men", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Slides", Gender = "M", Root = "Men", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Tennis", Gender = "M", Root = "Men", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Track & Field", Gender = "M", Root = "Men", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Training", Gender = "M", Root = "Men", NodeTitle = "Sneakers" }, new SubRoots() { SubRoot = "Walking", Gender = "M", Root = "Men", NodeTitle = "Sneakers" } };

            //nodeMen.Add(new Nodes() { NodeTitle = "Sneakers", Gender = "M", IsShowMore = true, Root = "Men" }, subRootMSneakers);

            nodeMen.Add(new Nodes() { NodeTitle = "Basketball", Gender = "M", IsShowMore = false, Root = "Men" }, subRootMBasketball);
            nodeMen.Add(new Nodes() { NodeTitle = "Casual", Gender = "M", IsShowMore = false, Root = "Men" }, subRootMCasual);
            nodeMen.Add(new Nodes() { NodeTitle = "Luxury", Gender = "M", IsShowMore = false, Root = "Men" }, subRootMLuxury);
            nodeMen.Add(new Nodes() { NodeTitle = "Running", Gender = "M", IsShowMore = false, Root = "Men" }, subRootMRunning);
            nodeMen.Add(new Nodes() { NodeTitle = "Soccer", Gender = "M", IsShowMore = false, Root = "Men" }, subRootMSoccer);
            nodeMen.Add(new Nodes() { NodeTitle = "Skateboard", Gender = "M", IsShowMore = false, Root = "Men" }, subRootMSkateboard);
            nodeMen.Add(new Nodes() { NodeTitle = "Slides", Gender = "M", IsShowMore = false, Root = "Men" }, subRootMSlides);
            nodeMen.Add(new Nodes() { NodeTitle = "Tennis", Gender = "M", IsShowMore = false, Root = "Men" }, subRootMTennis);
            nodeMen.Add(new Nodes() { NodeTitle = "Track & Field", Gender = "M", IsShowMore = false, Root = "Men" }, subRootMTrackField);
            nodeMen.Add(new Nodes() { NodeTitle = "Training", Gender = "M", Root = "Men", IsShowMore = false }, subRootMTraining);
            nodeMen.Add(new Nodes() { NodeTitle = "Walking", Gender = "M", Root = "Men", IsShowMore = false }, subRootMWalking);
            #endregion

            roots = new List<Roots>()
            {
                new Roots(){ Root="All", Node=nodeAll},
                new Roots(){ Root="Women", Node=nodeW},
                new Roots(){ Root="Men", Node=nodeMen}
            };

            if (selectedsubcat != null && selectedcat != null)
            {
                if (selectedsubcat.ToLower().Contains("COATS".ToLower()))
                {
                    selectedsubcat = "JACKETS & COATS";
                }
                if (selectedsubcat.ToLower().Contains("SUITS".ToLower()))
                {
                    selectedsubcat = "SUITS & TUXEDOS";
                }
                if (selectedsubcat.ToLower().Contains("SOCKS".ToLower()))
                {
                    selectedsubcat = "socks & underwear";
                }
                if (selectedsubcat.ToLower().Contains("Swim".ToLower()) && selectedcat.Contains("Women"))
                {
                    selectedsubcat = "Swimsuits";
                }
                var selectedroot = roots.Where(x => x.Root.ToLower() == selectedcat.ToLower()).ToList();
                var subRootsList = selectedroot.Select(r => r.Node.Keys.Where(k => k.NodeTitle.ToLower() == selectedsubcat.ToLower()).FirstOrDefault()).FirstOrDefault();
                var res = selectedroot.Select(x => x.Node[subRootsList]).ToList().FirstOrDefault();

                List<SubRoots> subRoots = new List<SubRoots>();
                Dictionary<Nodes, List<SubRoots>> SubnodeDicn = new Dictionary<Nodes, List<SubRoots>>();
                foreach (var item in res)
                {
                    var node = new Nodes()
                    {
                        IsShowMore = item.IsShowMore,
                        NodeTitle = item.SubRoot,
                        Gender = item.Gender
                    };
                    SubnodeDicn.Add(node, subRoots);
                }

                List<Roots> rootsa = new List<Roots>
                {
                    new Roots()
                    {
                        Root = selectedsubcat,
                        Node = SubnodeDicn
                    }
                };
                return Global.RootsList = rootsa;
            }
            else if (selectedcat != null)
            {
                //return Global.RootsList = roots.Where(x => x.Root.ToLower() == selectedcat.ToLower()).ToList();
                return Global.RootsList = roots.ToList();
            }
            else
            {
                Global.RootsList = roots;
            }
            return roots;
        }
        #endregion
    }
}
