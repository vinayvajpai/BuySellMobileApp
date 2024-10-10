 using System;
using BuySell.Model.LoginResponse;
using BuySell.Model;
using System.Collections.ObjectModel;

namespace BuySell.Helper
{
    public class Constant
    {
        //All the string text which is used in entire project is written here

        //Without QA
        //public static readonly string BaseUrl = "https://buysellapi.azurewebsites.net";
        //With QA
        public static readonly string BaseUrl = "https://buysellapiqa.azurewebsites.net";
        //With UAT
        //public static readonly string BaseUrl = "https://buysellapiuat.azurewebsites.net";
        public static readonly string StripPublishAPIKey = "pk_test_51LfBl2BO6Q12PgkzMFep4okUWCWPH7on9Z5oJeQ8VUkrbuflQ38Juws9UIWIjJ2chRu9sTvsGk0sydQtrzgCaMdT001vvxCrqU";//"pk_test_51METrtSAPn7TJLsGObVO6IbhlSrZb8MBaVxTWnyNwTpGUyJSq47eqkbI0CbCYnDqwuL8lyCqilLqCqX1StmZjb7m00cduXDVlM";
        public static readonly string stripSecertAPIKey = "sk_test_51LfBl2BO6Q12PgkzBrw8ju9WspEO4BuPS2uKPXPxXk81SqltRWvZ1n18tZBMPR4AlnxCxu6eL8rIxEq25CjrC98p008onDDINL";//"sk_test_51METrtSAPn7TJLsGmBBRdgXuuX30DTe9SioFx0pjgzWCgXZDxhoI93oaXN3rcWMB8O5ydnOaWxq7JaJnb1jt17UA00qkWQ09ir";

        //Global constants which is used in entire project
        public static readonly string ClothingStr = "Clothing";
        public static readonly string SneakersStr = "Sneakers";
        public static readonly string StreetwearStr = "StreetWear";
        public static readonly string VintageStr = "Vintage"; 
        public static Data LoginUserData;
        public static readonly string ErrorMessage = "Unable to process your request, please try again after some time";//"Unable to process your request";
        public static readonly string CategoryText = "Select Category"; 
        public static readonly string SizeText = "Select Size";
        public static readonly string CustomSizeStr = "Custom";
        public static readonly string SizeMessageStr = "Please enter size";
        public static readonly string CheckCardDetailStr = "Please add card details";
        public static readonly string CheckAddressStr = "Please add shipping address";
        public static readonly string NotforsaleStr="NOT FOR SALE";
        public static readonly string SoldStr = "SOLD";
        public static readonly string TokenExpireMsgStr = "Audience validation failed";
        public static readonly string SessionTimeOutMsgStr = "Your login session has been expired, Please login again.";
        public static readonly string OKStr = "OK";
        public static readonly string CancelStr = "Cancel";
        public static readonly string logoutConfirmMsgStr = "Are you sure you want to logout?";
        public static readonly string deleteConfirmMsgStr = "Are you sure you want to delete?";
        public static CardListModel globalAddedCard = new CardListModel();
        public static AddAddressModel globalSelectedAddress = new AddAddressModel();
        public static AddAddressModel globalSelectedFromAddress = new AddAddressModel();
        public static decimal globalTax = 0;
        public static BankAccountModel globalBankAccount = new BankAccountModel();

        public static bool IsConnected = true;

        //constant created for custom category sub header 
        public static readonly string ClothingHanger = "ClothingHanger";
        public static readonly string ClothingImageWhiteBackground = "ClothingHangerW";
        public static readonly string SneakersImageWhiteBackground = "SneakersW";
        public static readonly string StreetwearImageWhiteBackground = "StreetwearW";
        public static readonly string VintageImageWhiteBackground = "VintageTopsW";

        //Constants created for custom filter view
        public static readonly string AllStr = "All";
        public static readonly string GenderStr = "GENDER";
        public static readonly string CategoryStr = "CATEGORY";
        public static readonly string SortStr = "SORT";
        public static readonly string SizeStr = "SIZE";
        public static readonly string BrandStr = "BRAND";
        public static readonly string PriceStr = "PRICE";
        public static readonly string ColorStr = "COLOR";
        public static readonly string ConditionStr = "CONDITION";
        public static readonly string AvailabilityStr = "AVAILABILITY";
        public static readonly string ShippingPriceStr = "SHIPPING PRICE";

        //Constant created for update theme messaging center
        public static readonly string UpdateHeaderThemeStr = "UpdateHeadertheme";
        public static readonly string UpdateHeaderThemeForDetailStr = "UpdateHeaderthemeForDetail";
        public static readonly string UpdateThemeForDetailStr = "UpdatethemeForDetail";
        public static readonly string UpdateThemeStr = "Updatetheme";

        //Constant created for filter API
        public static readonly string LowToHighStr = "Low to High";
        public static readonly string HighToLowStr = "High to Low";
        public static readonly string NewListingStr = "New Listing";
        public static readonly string UnderStr = "Under";
        public static readonly string OverStr = "Over";

        //Constant created for Search related views
        public static readonly string StartIconStr = "Star_Icon";
        public static readonly string NextImageStr = "Next_Img";
        public static readonly string ClothingZeroStr = "Clothing (0)";
        public static readonly string StreetwearZeroStr = "Sneakers (0)";
        public static readonly string SneakersZeroStr = "Streetwear (0)";
        public static readonly string VintageZeroStr = "Vintage (0)";

        //Constant created for Description of all the stores
        //public static readonly string ClothingDescriptionStr = "The Clothing store encompasses everything from casual athleisure to business suiting to swanky formalwear. The Clothing store is where you'll find a large variety of brands from your everyday favorites to the high-end fashion treasures you desire most.";
        //public static readonly string SneakersDescriptionStr = "The Sneakers store is entirely dedicated to althetic shoes generally constructed of cloth or leather with laces and a rubber sole. Worn for comfort, sports, casual dress, and fashion statements. You'll find the most popular brands from your everyday favorites to special limited editions.";
        //public static readonly string StreetwearDescriptionStr = "The Streetwear store is all about emerging and iconic brands that are synonymous with trending fashion and luxury. Featuring apparel like t-shirts, hats, hoodies, watches, and a wide array of accessories. Streetwear captures a lifestyle worn by the icons of popular culture.";
        //public static readonly string VintageDescriptionStr = "The Vintage store you’ll find one-of-a kind items manufactured in a previous era. To be considered Vintage an item must be manufactured 20+ years ago. Increasingly in demand, Vintage fashions have attracted many copycats. We are here to help you get your hands on the real thing.";

        public static readonly string ClothingDescriptionStr = "The clothing store encompasses everything from high end designer to fast fashion. Most general items that do not fall under vintage, streetwear, and sneakers can fall into the cloth-ing store.";
        public static readonly string SneakersDescriptionStr = " The sneakers store consists of all brands that manufacture athletic footwear. Primarily worn for active lifestyle, comfort, casual and sports. ";
        public static readonly string StreetwearDescriptionStr = "The Streetwear store is all about emerging brands that capture a metropolitan lifestyle. Originally defined by the skate and surf scene, Streetwear has evolved to also include certain luxury brands. ";
        public static readonly string VintageDescriptionStr = "The Vintage store is where you'll find unique items manufactured in a previous era, normally 20+ years ago. Size tags, stitching, and cut can often help identify the real thing from retro copies. ";
        public static readonly string stateJson = "['Alabama','Alaska','Arizona','Arkansas','California','Colorado','Connecticut','Delaware','Florida','Georgia','Hawaii','Idaho','Illinois','Indiana','Iowa','Kansas','Kentucky','Louisiana','Maine','Maryland','Massachusetts','Michigan','Minnesota','Mississippi','Missouri','Montana','Nebraska','Nevada','New Hampshire','New Jersey','New Mexico','New York','North Carolina','North Dakota','Ohio','Oklahoma','Oregon','Pennsylvania','Rhode Island','South Carolina','South Dakota','Tennessee','Texas','Utah','Vermont','Virginia','Washington','West Virginia','Wisconsin','Wyoming']";
    }
}
