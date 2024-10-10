using BuySell.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace BuySell.Model
{

    public class ProductFilterModel : BaseViewModel
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 50;
        public string Search { get; set; }
        public int? StoreId { get; set; }
        public int? LoggedUserId { get; set; }
        public int? UserId { get; set; }
        public List<string> Categories { get; set; }
        public List<int> RootCategories { get; set; }
        public List<string> Color { get; set; }
        public List<string> Brand { get; set; }
        public List<string> Size { get; set; }
        public List<string> Condition { get; set; }
        public List<string> Availability { get; set; }
        public PriceFilter Price { get; set; }
        public PriceFilter ShippingPrice { get; set; }
        public SortingFilter Sort { get; set; }
    }
    public class PriceFilter
    {
        public bool IsApply { get; set; } = false;
        public decimal? FromPrice { get; set; }
        public decimal? ToPrice { get; set; }
        public PriceCompareFilter PriceCompareOperator { get; set; } = PriceCompareFilter.GreaterThan;
    }
    public enum PriceCompareFilter
    {
        GreaterThan = 1,
        GreaterThanOrEqual = 2,
        LessThan = 3,
        LessThanOrEqual = 4,
        Between = 5
    }
    public class SortingFilter
    {
        public bool IsApply { get; set; } = false;
        public SortCompareFilter Sort { get; set; } = SortCompareFilter.PriceHighToLow;
    }
    public enum SortCompareFilter
    {
        PriceLowToHigh = 1,
        PriceHighToLow = 2,
        NewList = 3,
    }
}