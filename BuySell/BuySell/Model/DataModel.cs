using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using BuySell.Helper;
using BuySell.Model.LoginResponse;
using BuySell.ViewModel;
using FFImageLoading.Cache;
using FFImageLoading.Forms;
using Newtonsoft.Json;
using SQLite;
using Xamarin.Forms;
using static BuySell.Model.CategoryModel;

namespace BuySell.Model
{
    //DataModel
    public class DashBoardModel : BaseViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string ProductCoverImage { get; set; }
        public string UserName { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserProfile { get; set; }

        [JsonProperty(ItemConverterType = typeof(BuySell.Converter.ImageSourceConverter))]
        public ImageSource ProductImage { get; set; }
        //public ImageSource _ProductImage;
        //public ImageSource ProductImage
        //{
        //    get
        //    {
        //        return _ProductImage;
        //    }
        //    set
        //    {
        //        _ProductImage = value;
        //        OnPropertyChanged("ProductImage");
        //        cleanCache();
        //    }
        //}
        public string TagImage { get; set; }
        public string Quantity { get; set; }
        public string Availability { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string ShippingPrice { get; set; }
        // public string Size { get; set; }
        private string _Size;
        public string Size
        {
            get
            {
                //return _Size;
                if (_Size != null)
                    return _Size.ToLower().Contains("one size") ? "OS" : _Size;
                else
                    return _Size;

            }
            set { _Size = value; OnPropertyChanged("Size"); }
        }
        public string Source { get; set; }
        public string SizeValue { get; set; }
        public string StoreCategory { get; set; }
        public string OfferPrice { get; set; }
        public List<string> ProductImages { get; set; }

        [JsonProperty(ItemConverterType = typeof(BuySell.Converter.ImageSourceConverter))]
        private List<ImageSource> _otherImages { get; set; }

        [JsonProperty(ItemConverterType = typeof(BuySell.Converter.ImageSourceConverter))]
        public List<ImageSource> otherImages
        {
            get
            {
                return _otherImages;
            }
            set
            {
                _otherImages = value;
            }
        }
        private string _ThemeCol = Global.GetThemeColor(Global.setThemeColor);
        internal ObservableCollection<DashBoardModel> templist;
        public string ProductName { get; set; }
        public string _ProductCategory;

        #region Product Category
        public string ProductCategory
        {
            get
            {
                if (CategoryName != null)
                {
                    return CategoryName;
                }
                else
                {
                    return _ProductCategory;
                }
            }
            set
            {
                _ProductCategory = value;
            }
        }
        #endregion
        public string ProductColor { get; set; }
        public string ProductCondition { get; set; }
        public string Brand { get; set; }
        public string StoreType { get; set; }
        public string Earning { get; set; }
        public string SingleQty { get; set; }

        #region Theme Color property
        public string ThemeCol
        {
            get
            {
                if (StoreType.ToLower() == Constant.ClothingStr.ToLower())
                {
                    return Global.GetThemeColor(ThemesColor.BlueColor);
                }
                else if (StoreType.ToLower() == Constant.SneakersStr.ToLower())
                {
                    return Global.GetThemeColor(ThemesColor.RedColor);
                }
                else if (StoreType.ToLower() == Constant.StreetwearStr.ToLower())
                {
                    return Global.GetThemeColor(ThemesColor.OrangeColor);
                }
                else if (StoreType.ToLower() == Constant.VintageStr.ToLower())
                {
                    return Global.GetThemeColor(ThemesColor.GreenColor);
                }
                return _ThemeCol;
            }
            set
            {
                _ThemeCol = value; OnPropertyChanged("ThemeCol");
            }

        }
        #endregion

        public List<QuantityModel> quantityModels { get; set; }
        public string Gender { get; set; }

        #region Gender Full Name 
        public string GenderFullName
        {
            get
            {
                if (Gender.ToLower().Equals("m") || Gender.ToLower().Equals("men"))
                {
                    return "Men";
                }
                else
                {
                    return "Women";
                }
            }
        }
        #endregion 
        public string CategoryName { get; set; }
        public string ParentCategory { get; set; }
        public SubRoots CategorySubRoot { get; set; }

        #region Edit Category
        public SubRoots EditCategory
        {
            get
            {
                return new SubRoots()
                {
                    Node = null,
                    Root = GenderFullName,
                    //NodeTitle = ParentCategory,
                    NodeTitle = string.IsNullOrEmpty(StoreType) ? ParentCategory : StoreType.ToLower().Equals(Constant.SneakersStr.ToLower()) ? ProductCategory : ParentCategory,
                    //string.IsNullOrEmpty(StoreType) ? ParentCategory : StoreType.ToLower().Equals(Constant.SneakersStr.ToLower()) ? ProductCategory : (ParentCategory.ToLower().Equals("m") || ParentCategory.ToLower().Equals("f")) ? ProductCategory : ParentCategory,
                    SubRoot = ProductCategory,
                    Gender = Gender
                };
            }
        }
        #endregion
        public DateTime RecentViewItemDate { get; set; }
        public double Row1 { get { return Global.TileWidth; } }
        public int Row2 { get { return 30; } }
        public int Row3 { get { return 30; } }

        private bool _IsLike;
        public bool IsLike
        {
            get { return _IsLike; }
            set
            {
                _IsLike = value;
                OnPropertyChanged("IsLike");
                OnPropertyChanged("ProductRating");
            }
        }
        private int? _LikeCount;
        public int? LikeCount
        {
            get {
                return _LikeCount;
            }
            set
            {
                _LikeCount = value;
                OnPropertyChanged("LikeCount");
            }
        }
     
        public string ProductRating
        {
            get
            {
                return (IsLike == true) ? "FillHeart" : "UnfillHeart";
            }
        }

        public bool IsNotSaleOrSold
        {
            get
            {
                if (string.IsNullOrEmpty(Availability))
                    return false;
                else if(Availability.Trim().ToLower().Equals(Constant.NotforsaleStr.Trim().ToLower()) || Availability.Trim().ToLower().Equals(Constant.SoldStr.Trim().ToLower()) )
                {
                    if (Availability.Trim().ToLower().Equals(Constant.NotforsaleStr.Trim().ToLower()))
                    {
                        Availability = Constant.NotforsaleStr;
                    }
                    else
                    {
                        Availability = Constant.SoldStr;
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private async void cleanCache()
        {
            await CachedImage.InvalidateCache(ProductImage, CacheType.All, true);
        }
    }

    



    public class DashboardProductModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int CategoryID { get; set; }
        public int StoreID { get; set; }
        public string ProductDetail { get; set; }
    }
    public class BannerProductModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string BannerDetail { get; set; }
    }
    public class RecentProductModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public string ProductJson { get; set; }
        public DateTime RecentViewItemDate { get; set; }
    }
    public class CachedImageDatabaseModel
    {
        [SQLite.PrimaryKey]
        public Guid Id { get; set; }
        public string ImageName { get; set; }
        public string Url { get; set; }
        public byte[] ImageByte { get; set; }
        public override string ToString()
        {
            return string.Format("[CachedImageDatabaseModel: Id={0} ImageName={1},Url={2},ImageByte={4}", Id, ImageName, Url, ImageByte);
        }
    }
    public class ProductListResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public List<DashBoardModel> Data { get; set; }
    }
    public class ProductPagingListData: BaseViewModel
    {
        public int TotalRowCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public bool HasNextPage { get; set; }
        public int NextPageNumber { get; set; }
        public bool HasPreviousPage { get; set; }
        public int PreviousPageNumber { get; set; }
        public List<DashBoardModel> Data { get; set; }
    }
    public class ProductPagingListResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public ProductPagingListData Data { get; set; }
    }
    //CategoryModel
    public class ShopByCategoryModel : BaseViewModel
    {
        public ImageSource CategoryImage1 { get; set; }
        public ImageSource CategoryImage2 { get; set; }
        public ImageSource CategoryImage3 { get; set; }
        public string CategoryName1 { get; set; }
        public string CategoryName2 { get; set; }
        public string CategoryName3 { get; set; }
        private string _ThemeCol = Global.GetThemeColor(Global.setThemeColor);
        public string ThemeCol
        {
            get { return _ThemeCol; }
            set { _ThemeCol = value; OnPropertyChanged("ThemeCol"); }

        }

    }
    //BannerModel
    public class BannerModel : BaseViewModel
    {
        public string BannerImage { get; set; }
        public int Sequence { get; set; }
        public string Store { get; set; }
        public string GenderType { get; set; }
    }
    public class BannerResponseModel
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public List<BannerModel> Data { get; set; }
    }
    public class AddItemResponseModel
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public Data Data { get; set; }
    }
    public class ProductCoverImage
    {
        public string Image { get; set; }
        public string Extension { get; set; }
        public string ImagePath { get; set; }
        public bool IsDeleted { get; set; }
    }
    public class ProductImage
    {
        public string Image { get; set; }
        public string Extension { get; set; }
        public string ImagePath { get; set; }
        public bool IsPrimary { get; set; }
        public int SequenceNo { get; set; }
        public bool IsDeleted { get; set; }
    }
    public class AddItemRequestModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int StoreId { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string RootCategoryName { get; set; }
        public string ParentCategoryName { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double ShippingPrice { get; set; }
        public string Size { get; set; }
        //public string SizeValue { get; set; }
        public string ProductRating { get; set; }
        public string ProductCondition { get; set; }
        public string ProductColor { get; set; }
        public string Brand { get; set; }
        public string Quantity { get; set; }
        public string Availability { get; set; }
        public string Source { get; set; }
        //public ProductCoverImage ProductCoverImage { get; set; }
        public List<ProductImage> ProductImages { get; set; }
        public TagImage TagImage { get; set; }

        public string Earning { get; set; }
    }
    public class TagImage
    {
        public string Image { get; set; }
        public string Extension { get; set; }
        public string ImagePath { get; set; }
        public ImageSource TagImgsource { get; set; }
        public byte[] imageBytes { get; set; }
        public byte[] OrgimageBytes { get; set; }
        public bool IsDeleted { get; set; }
        public string OrignalImagePath { get; set; }
    }

     public class ExtraParam
    {
        public string CategoryId { get; set; }
        public string StoreId { get; set; }
        public string UserId { get; set; }
        public string ViaCategoryName { get; set; }
    }
    public class ProductRequestModel
    {
        public string Search { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public ExtraParam ExtraParam { get; set; }
    }
    public class Banner
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        public string CategoryName { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public List<ImageInfo> ImageInfos { get; set; }
    }
    public class Category
    {
        public int CategoryId { get; set; }
        public int ParentCategoryId { get; set; }
        public int RootCategoryId { get; set; }
        public int StoreId { get; set; }
        public string CategoryName { get; set; }
        public string ParentCategoryName { get; set; }
        public string Title { get; set; }
        public string RelationKey { get; set; }
        public object Sizes { get; set; }
    }
    public class ProductDataModel
    {
        public List<Banner> Banners { get; set; }
        public List<object> Products { get; set; }
        public List<Category> Categories { get; set; }
    }
    public class ImageInfo
    {
        public int SequenceNo { get; set; }
        public string Path { get; set; }
    }
    public class ProductResponseModel
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public ProductDataModel Data { get; set; }
    }
    public class CategoryListResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public List<Category> Data { get; set; }
    }
    public class SearchResponse
    {
        public int StoreId { get; set; }
        public string StoreType { get; set; }
        public int Counter { get; set; }
        public ProductPagingListData Data { get; set; }
    }
    public class SearchResponseModel
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public List<SearchResponse> Data { get; set; }
    }
    public class ProductSelectImage : BaseViewModel
    {
        public string path { get; set; }

        private ImageSource _image;
        public ImageSource image {
            get { return _image; }
            set { SetProperty(ref _image, value); }
        }

        private byte[] _OrgimageBytes;
        public byte[] OrgimageBytes
        {
            get { return _OrgimageBytes; }
            set { SetProperty(ref _OrgimageBytes, value); }
        }

        private byte[] _imageBytes;
        public byte[] imageBytes {
            get { return _imageBytes; }
            set { SetProperty(ref _imageBytes, value); }
        }
        private bool _isBeingDragged;
        public bool IsBeingDragged
        {
            get { return _isBeingDragged; }
            set { SetProperty(ref _isBeingDragged, value); }
        }

        private bool _isBeingDraggedOver;
        public bool IsBeingDraggedOver
        {
            get { return _isBeingDraggedOver; }
            set { SetProperty(ref _isBeingDraggedOver, value); }
        }
        public bool IsDeleted { get; set; }
    }
}
