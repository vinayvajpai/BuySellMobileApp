using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using BuySell.Helper;
using BuySell.Model;
using Xamarin.Forms;

namespace BuySell.ViewModel
{
    public class MyStoreViewModel : BaseViewModel
    {
        #region Constractor
        public MyStoreViewModel(INavigation _nav)
        {
            GetSearchProductList();
            navigation = _nav;
        }
        #endregion

        #region Properties
        private ObservableCollection<SearchProductModel> _SearchProductList = new ObservableCollection<SearchProductModel>();
        public ObservableCollection<SearchProductModel> SearchProductList
        {
            get { return _SearchProductList; }
            set { _SearchProductList = value; OnPropertyChanged("SearchProductList"); }
        }
        #endregion

        #region Methods
        public void GetSearchProductList()
        {
            try
            {
                SearchProductList.Clear();
                SearchProductList = new ObservableCollection<SearchProductModel>()
                {
                    new SearchProductModel()
                    {
                         ProductImage="Tshirt3",
                         Description="1992 Grateful De..",
                         Price="$350",
                         Size="SIZE XL",
                         ProductRating="FillHeart",
                         ThemeCol = Global.GetThemeColor(Global.setThemeColor)
                    },
                     new SearchProductModel()
                    {
                         ProductImage="NewTshirt",
                         Description="Jordan Vintage company",
                         Price="$150",
                         Size="SIZE XL",
                         ProductRating="UnfillHeart",
                         ThemeCol = Global.GetThemeColor(Global.setThemeColor)
                     },
                     new SearchProductModel()
                     {
                         ProductImage="Tshirt1",
                         Description="Police Concern 3",
                         Price="$50",
                         Size="SIZE L",
                         ProductRating="UnfillHeart",
                         ThemeCol = Global.GetThemeColor(Global.setThemeColor)
                     },
                     new SearchProductModel()
                    {
                        ProductImage="Tshirt3",
                        Description="1992 Grateful De..",
                        Price="$350",
                        Size="SIZE XL",
                        ProductRating="FillHeart",
                        ThemeCol = Global.GetThemeColor(Global.setThemeColor)
                    },
                     new SearchProductModel()
                    {
                         ProductImage="NewTshirt",
                         Description="Jordan Vintage company",
                         Price="$150",
                         Size="SIZE XL",
                         ProductRating="UnfillHeart",
                         ThemeCol = Global.GetThemeColor(Global.setThemeColor)

                    },
                    new SearchProductModel()
                    {
                         ProductImage="Tshirt1",
                         Description="Police Concern 3",
                         Price="$50",
                         Size="SIZE L",
                         ProductRating="UnfillHeart",
                         ThemeCol = Global.GetThemeColor(Global.setThemeColor)
                     }

                };
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        #endregion
    }
}

