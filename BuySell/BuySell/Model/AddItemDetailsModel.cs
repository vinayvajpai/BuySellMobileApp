using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Xml.Linq;
using Acr.UserDialogs;
using BuySell.Helper;
using BuySell.View;
using BuySell.ViewModel;
using Xamarin.Forms;
using static BuySell.Model.CategoryModel;

namespace BuySell.Model
{
    public class AddItemDetailsModel : BaseViewModel
    {
        #region Properties
        private string _HeadLine;
        public string HeadLine
        {
            get { return _HeadLine; }
            set { _HeadLine = value; OnPropertyChanged("HeadLine"); }
        }

        private string _StoreType;
        public string StoreType
        {
            get { return _StoreType; }
            set { _StoreType = value; OnPropertyChanged("StoreType"); }
        }

        private string _Description;
        public string Description
        {
            get
            {
                return _Description;
            }
            set { _Description = value; OnPropertyChanged("Description"); }
        }

        private string _Category = Constant.CategoryText;
        public string Category
        {
            get { return _Category; }
            set { _Category = value; OnPropertyChanged("Category"); }
        }

        private string _Quantity = "One";
        public string Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; OnPropertyChanged("Quantity"); }
        }

        private string _Size = Constant.SizeText;
        public string Size
        {
            get
            {
                //return _Size;
                if (_Size != null)
                    return _Size.ToLower().Contains("select") ? _Size : _Size.ToLower().Contains("one size") ? "One Size" : _Size.ToLower().Contains("custom") ? "" : _Size;
                else
                    return _Size;

            }
            set { _Size = value; OnPropertyChanged("Size"); }
        }

        private string _brand;
        public string Brand
        {
            get { return _brand; }
            set { _brand = value; OnPropertyChanged("Brand"); }
        }

        private string _Condition = "Used";
        public string Condition
        {
            get { return _Condition; }
            set { _Condition = value; OnPropertyChanged("Condition"); }
        }

        private string _ProdColor = "Red";
        public string ProdColor
        {
            get
            {
                return _ProdColor;
            }
            set
            {
                _ProdColor = value;
                OnPropertyChanged("ProdColor");
                OnPropertyChanged("ProdColorValue");
            }
        }


        public string ProdColorValue
        {
            get
            {
                return ProdColor.ToLower().Equals("cream") ? "#FFFDD0" : ProdColor.ToLower().Equals("brown") ? "#704214" : ProdColor;

            }

        }

        private string _Availability = "For Sale";
        public string Availability
        {
            get { return _Availability; }
            set { _Availability = value; OnPropertyChanged("Availability"); }
        }

        private string _Price = "$";
        public string Price
        {
            get
            {
                if (_Price != null)
                {
                    return _Price.Contains("$") ? _Price : "$" + _Price;
                }
                else
                    return _Price;
            }
            set
            {
                try
                {
                    //Condition added to restrict the user to do not enter values in decimals
                    if (value.Contains("."))
                    {
                        value = value.Replace(".", "");

                        Helper.Global.AlertWithAction("Only integers are allowed",null);

                        //var alertConfig = new AlertConfig
                        //{
                        //    Message = "Only integers are allowed",
                        //    OkText = "OK",

                        //};
                        //Acr.UserDialogs.UserDialogs.Instance.Alert(alertConfig);
                        _Price = value;
                    }
                    else
                    {
                        _Price = value;
                        if (!string.IsNullOrWhiteSpace(_Price))
                        {
                            var a = _Price.ToCharArray();
                            if (a.Length > 1)
                            {
                                if (a[1] == '.')
                                {
                                    _Price = _Price.Replace(".", "");
                                }
                            }
                        }
                        //var newPrice = _Price.Replace("$", "");

                        ////if (newPrice.Length >= 2) {
                        //    if (Convert.ToDecimal(newPrice) < 10)
                        //    {
                        //        Acr.UserDialogs.UserDialogs.Instance.Toast("Minimum Listing Price is $10");
                        //    }
                        //Condition added for "User's earning calculation" inside edit listing and add listing
                        if (!string.IsNullOrWhiteSpace(_Price) && !string.IsNullOrEmpty(_Price) && !value.Equals("$"))
                        {
                            _Price = _Price.Replace("$", "");
                            decimal twentyPercent;
                            decimal earning = 0;
                            var shippingCost = "$8";
                            string shippingText = "(Shipping)";
                            if (!string.IsNullOrWhiteSpace(_Price))
                            {
                                var price = Convert.ToDecimal(_Price);
                                twentyPercent = price * 20 / 100;
                                if (ShipPrice == "$8")
                                {
                                    //earning = price - (twentyPercent + 8);
                                    earning = price - (twentyPercent);
                                    shippingCost = "$8";
                                }
                                else if (ShipPrice == "$11")
                                {
                                    //earning = price - (twentyPercent + 11);
                                    earning = price - (twentyPercent);
                                    shippingCost = "$11";
                                }
                                else if (ShipPrice == "$14")
                                {
                                    //earning = price - (twentyPercent + 14);
                                    earning = price - (twentyPercent);
                                    shippingCost = "$14";
                                }
                                //Earning = "$" + price + " - " + "20%" + " (" + "$" + Convert.ToString(twentyPercent) + ") " + "- " + shippingCost + shippingText + " = " + "$" + Convert.ToString(earning);
                                Earning = "$" + Convert.ToString(earning);
                            }
                        }  //Condition added for "User's earning calculation" inside edit listing and add listing
                        if (!string.IsNullOrWhiteSpace(_Price) && !string.IsNullOrEmpty(_Price) && !value.Equals("$"))
                        {
                            _Price = _Price.Replace("$", "");
                            decimal twentyPercent;
                            decimal earning = 0;
                            var shippingCost = "$8";
                            string shippingText = "(Shipping)";
                            if (!string.IsNullOrWhiteSpace(_Price))
                            {
                                var price = Convert.ToDecimal(_Price);
                                twentyPercent = price * 20 / 100;
                                if (ShipPrice == "$8")
                                {
                                    //earning = price - (twentyPercent + 8);
                                    earning = price - (twentyPercent);
                                    shippingCost = "$8";
                                }
                                else if (ShipPrice == "$11")
                                {
                                    //earning = price - (twentyPercent + 11);
                                    earning = price - (twentyPercent);
                                    shippingCost = "$11";
                                }
                                else if (ShipPrice == "$14")
                                {
                                    //earning = price - (twentyPercent + 14);
                                    earning = price - (twentyPercent);
                                    shippingCost = "$14";
                                }
                                //Earning = "$" + price + " - " + "20%" + " (" + "$" + Convert.ToString(twentyPercent) + ") " + "- " + shippingCost + shippingText + " = " + "$" + Convert.ToString(earning);
                                Earning = "$" + Convert.ToString(earning);
                            }
                        }
                        //}

                    }
                    OnPropertyChanged("Price");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

        }

        private string _ShipPrice = "$8";
        public string ShipPrice
        {
            get { return _ShipPrice; }
            set { _ShipPrice = value; OnPropertyChanged("ShipPrice"); }
        }

        private string _Earning = "$0";
        internal object Global;

        public string Earning
        {
            get { return _Earning; }
            set { _Earning = value; OnPropertyChanged("Earning"); }
        }

        public List<QuantityModel> quantityModels { get; set; }
        public string Gender { get; set; }
        public SubRoots CategorySubRoot { get; set; }
        public string ParentCategory { get; set; }
        #endregion

    }
}
