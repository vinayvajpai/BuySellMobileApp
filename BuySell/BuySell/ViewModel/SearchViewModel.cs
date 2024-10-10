using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using BuySell.Helper;
using BuySell.Model;
using Xamarin.Forms;

namespace BuySell.ViewModel
{
    public class SearchViewModel : BaseViewModel
    {
        public int PageNumber = 1;
        public int PageSize = 20;
        #region Constructor
        public SearchViewModel(INavigation _nav)
        {
            GetSearchList(SearchItem);
            SearchCommand = new Command(() =>
            {
                SearchClick();
            });
            navigation = _nav;
        }
        #endregion

        #region Properties
        private ObservableCollection<SearchModel> _SearchList = new ObservableCollection<SearchModel>();
        public ObservableCollection<SearchModel> SearchList
        {
            get { return _SearchList; }
            set { _SearchList = value; OnPropertyChanged("SearchList"); }
        }

        private ObservableCollection<SearchResultModel> _SearchResultList = new ObservableCollection<SearchResultModel>();
        public ObservableCollection<SearchResultModel> SearchResultList
        {
            get { return _SearchResultList; }
            set { _SearchResultList = value; OnPropertyChanged("SearchResultList"); }
        }


        private ObservableCollection<DashBoardModel> _ClothingProductList = new ObservableCollection<DashBoardModel>();
        public ObservableCollection<DashBoardModel> ClothingProductList
        {
            get { return _ClothingProductList; }
            set { _ClothingProductList = value; OnPropertyChanged("ClothingProductList"); }
        }

        private ObservableCollection<DashBoardModel> _SneakersProductList = new ObservableCollection<DashBoardModel>();
        public ObservableCollection<DashBoardModel> SneakersProductList
        {
            get { return _SneakersProductList; }
            set { _SneakersProductList = value; OnPropertyChanged("SneakersProductList"); }
        }


        private ObservableCollection<DashBoardModel> _StreetwearProductList = new ObservableCollection<DashBoardModel>();
        public ObservableCollection<DashBoardModel> StreetwearProductList
        {
            get { return _StreetwearProductList; }
            set { _StreetwearProductList = value; OnPropertyChanged("StreetwearProductList"); }
        }

        private ObservableCollection<DashBoardModel> _VintageProductList = new ObservableCollection<DashBoardModel>();
        public ObservableCollection<DashBoardModel> VintageProductList
        {
            get { return _VintageProductList; }
            set { _VintageProductList = value; OnPropertyChanged("VintageProductList"); }
        }

        private string _SearchText { get; set; }
        public string SearchText
        {
            get { return _SearchText; }
            set
            {
                if (_SearchText != value)
                {
                    _SearchText = value;
                }
                OnPropertyChanged("SearchText");
            }
        }

        private string _SearchItem { get; set; }
        public string SearchItem
        {
            get { return _SearchItem; }
            set
            {
                if (_SearchItem != value)
                {
                    _SearchItem = value;
                }
                OnPropertyChanged("SearchItem");
            }
        }
        public Command SearchCommand { get; set; }
        public void SearchClick()
        {
            try
            {
                if (!string.IsNullOrEmpty(SearchText))
                {
                    GetSearchResultList();
                }
            }

            catch (Exception ex)
            {

            }
        }
        #endregion

        #region Methods
        public void GetSearchList(string SearchItem)
        {
            try
            {
                SearchList.Clear();
                SearchList = new ObservableCollection<SearchModel>()
                {
                    new SearchModel()
                    {
                         StarImage= Constant.StartIconStr,
                         Description="Sweater vest",
                         NextImage= Constant.NextImageStr
                    },
                     new SearchModel()
                    {
                         StarImage=Constant.StartIconStr,
                         Description="Turtleneck",
                         NextImage=Constant.NextImageStr
                     },

                      new SearchModel()
                    {
                         StarImage=Constant.StartIconStr,
                         Description="HoodieJacket",
                         NextImage=Constant.NextImageStr
                     },
            };
            }
            catch (Exception ex)
            {

            }
        }

        public void GetSearchResultList()
        {
            try
            {
                SearchResultList.Clear();
                SearchResultList = new ObservableCollection<SearchResultModel>()
                {
                    new SearchResultModel()
                    {
                         StarImage=Constant.StartIconStr,
                         Description="Clothing (72)",
                         NextImage=Constant.NextImageStr,
                         TextColor = Color.FromHex("#1567A6")
                    },
                     new SearchResultModel()
                    {
                         StarImage=Constant.StartIconStr,
                         Description=Constant.SneakersZeroStr,
                         NextImage=Constant.NextImageStr,
                         TextColor = Color.FromHex("#C52036")
                     },

                     new SearchResultModel()
                    {
                         StarImage=Constant.StartIconStr,
                         Description=Constant.StreetwearZeroStr,
                         NextImage=Constant.NextImageStr,
                         TextColor = Color.FromHex("#D04107")
                     },
                      new SearchResultModel()
                    {
                         StarImage=Constant.StartIconStr,
                         Description=Constant.VintageZeroStr,
                         NextImage=Constant.NextImageStr,
                         TextColor = Color.FromHex("#467904")
                     },
            };
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
    }
}
