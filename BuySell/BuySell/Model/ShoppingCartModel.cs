using System;
using System.Collections.Generic;
using System.Linq;
using BuySell.Helper;
using BuySell.ViewModel;
using Xamarin.Forms;

namespace BuySell.Model
{
    //ShoppingCartModel
    public class ShoppingCartModel : BaseViewModel
    {
        public ImageSource ProductImage { get; set; }
        public string Description { get; set; }
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public string Brand { get; set; }
        public string Price { get; set; }
        public string Size { get; set; }
        public string SizeValue { get; set; }
        public string ProductRating { get; set; }
        public DashBoardModel product { get; set; }
        public string ThemeCol
        {
            get { return Global.GetThemeColor(Global.setThemeColor); }

        }
    }
    //CartModel
    public class CartModel : BaseViewModel
    {
        public string PromoCode { get; set; }
        public long OrderId { get; set; } = 0;

        private string _SellerNameWithCount;
        public string SellerNameWithCount
        {
            get { return _SellerNameWithCount; }
            set { _SellerNameWithCount = value; OnPropertyChanged("SellerNameWithCount"); }

        }

        private string _SellerName;
        public string SellerName
        {
            get { return _SellerName; }
            set { _SellerName = value; OnPropertyChanged("SellerName"); }

        }
        public DashBoardModel product { get; set; }
        public int CountSeller { get {
                if (listShoppingCart != null)
                {
                    return listShoppingCart.Count;
                }
                return 0;
            }
        }
        public string EstimatedTotal
        {
            get
            {
                if (listShoppingCart != null)
                {
                    if (listShoppingCart.Count > 1)
                        return string.Format("Estimated Total ({0} items ) ${1:F2}", listShoppingCart.Where(p=>p.product.IsNotSaleOrSold==false).ToList().Count.ToString(), listShoppingCart.Where(p => p.product.IsNotSaleOrSold == false).ToList().Sum(i => Convert.ToDouble(i.Price.Replace("$", ""))));
                    else if (listShoppingCart.Count <= 1)
                        return string.Format("Estimated Total ({0} item ) ${1:F2}", listShoppingCart.Where(p => p.product.IsNotSaleOrSold == false).ToList().Count.ToString(), listShoppingCart.Where(p => p.product.IsNotSaleOrSold == false).ToList().Sum(i => Convert.ToDouble(i.Price.Replace("$", ""))));
                    else
                        return string.Format("Estimated Total ({0} item ) ${1:F2}", listShoppingCart.Where(p => p.product.IsNotSaleOrSold == false).ToList().Count.ToString(), listShoppingCart.Where(p => p.product.IsNotSaleOrSold == false).ToList().Sum(i => Convert.ToDouble(i.Price.Replace("$", ""))));
                }
                return "Estimated Total (0 items) $0";
            }
        }
        
        public string ThemeCol
        {
            get { return Global.GetThemeColor(Global.setThemeColor); }

        }
        public List<ShoppingCartModel> listShoppingCart { get; set; }
    }
    //OrderSummaryListModel
    public class OrderSummaryListModel : BaseViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public ImageSource Image { get; set; }
        private string _ThemeCol = Global.GetThemeColor(Global.setThemeColor);
        public string ThemeCol
        {
            get { return _ThemeCol; }
            set { _ThemeCol = value; OnPropertyChanged("ThemeCol"); }

        }
    }
    //SummaryCartModel
    public class SummaryCartModel : BaseViewModel
    {
        public List<OrderSummaryListModel> listOrderSummary { get; set; }
    }

    // API Model for requesting order id for the cart.


    public class RequestCartOrderID
    {
        public int UserId { get; set; }
        public long OrderId { get; set; }
        public long ProductId { get; set; }
    }

    public class ResponseCartOrderID
    {
        public long OrderId { get; set; }
        public long ProductId { get; set; }
    }

}


