using System;
using BuySell.Helper;
using BuySell.ViewModel;
using Xamarin.Forms;

namespace BuySell.Model
{
    //SearchProductModel
    public class SearchProductModel:BaseViewModel
    {
            public ImageSource ProductImage { get; set; }
            public string Description { get; set; }
            public string Price { get; set; }
            public string Size { get; set; }
            public ImageSource ProductRating { get; set; }
            private string _ThemeCol = Global.GetThemeColor(Global.setThemeColor);
            public string ThemeCol
            {
                get { return _ThemeCol; }
                set { _ThemeCol = value; OnPropertyChanged("ThemeCol"); }

            }
        }
    }
