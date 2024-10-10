using BuySell.Helper;
using BuySell.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace BuySell.Model
{
    //ItemDetailSizeModel
    public class ItemDetailSizeModel : BaseViewModel
    {
        public string Size { get; set; }
        private string _ThemeCol = Global.GetThemeColor(Global.setThemeColor);
        public string ThemeCol
        {
            get { return _ThemeCol; }
            set { _ThemeCol = value; OnPropertyChanged("ThemeCol"); }

        }

        private Color _SelectionColor = Color.WhiteSmoke;
        public Color SelectionColor
        {
            get { return _SelectionColor; }
            set {
                _SelectionColor = value;
                OnPropertyChanged("SelectionColor");
            }

        }
    }
}