using System;
using BuySell.Helper;
using BuySell.View;
using BuySell.ViewModel;
using Xamarin.Forms;

namespace BuySell.Model
{
    //TopTrendingModel
    public class TopTrendingModel : BaseViewModel
    { 
        public ImageSource ProductImage { get; set; }
        public string ProductName { get; set; }
        public string ProductCost { get; set; }
        public string ProductSize { get; set; }
        public ImageSource ProductRating { get; set; }
        private string _ThemeCol = Global.GetThemeColor(Global.setThemeColor);
        public string ThemeCol
        {
            get { return _ThemeCol; }
            set { _ThemeCol = value; OnPropertyChanged("ThemeCol"); }

        }
    }
}
