using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BuySell.Helper;
using BuySell.Model;
using BuySell.View;
using Xamarin.Forms;

namespace BuySell.ViewModel
{
    public class RecentAllProductViewModel : BaseViewModel
    {
        public int PageNumber = 1;
        public ProductFilterModel productFilterModel;
        int count = 0;

        #region Constructor
        public RecentAllProductViewModel(INavigation _nav,List<DashBoardModel> _listProduct)
        {
            navigation = _nav;
            ProductList = new ObservableCollection<DashBoardModel>(_listProduct);
            ConstructorMethod();
        }
        #endregion

        #region Properties
        private ObservableCollection<DashBoardModel> _ProductList = new ObservableCollection<DashBoardModel>();
        public ObservableCollection<DashBoardModel> ProductList
        {
            get { return _ProductList; }
            set { _ProductList = value; OnPropertyChanged("ProductList"); }
        }

        private ObservableCollection<DashBoardModel> _ProductListDuplicate = new ObservableCollection<DashBoardModel>();
        public ObservableCollection<DashBoardModel> ProductListDuplicate
        {
            get { return _ProductListDuplicate; }
            set { _ProductListDuplicate = value; OnPropertyChanged("ProductListDuplicate"); }
        }

        public bool _IsFilterChanged = true;
        public bool IsFilterChanged
        {
            get
            {
                return _IsFilterChanged;
            }
            set
            {
                _IsFilterChanged = value;
                OnPropertyChanged("IsFilterChanged");
            }
        }
        public int _PageSize = 20;
        public int PageSize
        {
            get
            {
                return _PageSize;
            }
            set
            {
                _PageSize = value;
                OnPropertyChanged("PageSize");
            }
        }

        private bool _IsLoadMore;
        public bool IsLoadMore
        {
            get => _IsLoadMore;
            set
            {
                _IsLoadMore = value;
                OnPropertyChanged("IsLoadMore");
            }
        }

        private int _itemTreshold;
        public int ItemTreshold
        {
            get { return _itemTreshold; }
            set { SetProperty(ref _itemTreshold, value); }
        }
        #endregion

        #region Commands
        public Command ItemTresholdReachedCommand { get; set; }
        private Command _TopTrandingItemsCommand;
        public Command TopTrandingItemsCommand
        {
            get
            {
                return _TopTrandingItemsCommand ?? (_TopTrandingItemsCommand = new Command(async (obj) => await ExicuteTopTrandingCommond(obj)));
            }
        }
        #endregion

        #region Methods
        private async Task ExicuteTopTrandingCommond(object obj)
        {
            try
            {
                if (IsTap)
                    return;
                IsTap = true;
                Global.SetRecentViewList((DashBoardModel)obj);
                await navigation.PushAsync(new ItemDetailsPage((DashBoardModel)obj, false));
            }
            catch (Exception ex)
            {
                IsTap = false;
                Debug.WriteLine(ex.Message);
            }
          
        }

        public async Task FilterData(List<FilterModel> arg)
        {
            try
            {
                count++;
                if (count == 1) ProductListDuplicate = ProductList;
                ProductList.Clear();
                //switch (Global.SelectedFilter)
                //{
                //    case "COLOR":
                //        var filteredData = ProductListDuplicate.Where(x => x.ProductColor == arg.Colo).ToList();
                //}
            }
            catch (Exception ex)
            {
                IsTap = false;
                Debug.WriteLine(ex.Message);
            }

        }

        public async Task ConstructorMethod( )
        {
            try
            {
                //ProductList = new ObservableCollection<DashBoardModel>( _listProduct);
                if (ProductList.Count > 0)
                {
                    TotalCount = "Showing 1 to " + ProductList.Count + " of " + ProductList.Count + " Items";
                    IsShowFilter = true;
                    IsNoData = false;
                }
                else
                {
                    IsShowFilter = false;
                    IsNoData = true;
                }

            }
            catch (Exception ex)
            {
                IsTap = false;
                Debug.WriteLine(ex.Message);
            }

        }
        #endregion
    }
}
