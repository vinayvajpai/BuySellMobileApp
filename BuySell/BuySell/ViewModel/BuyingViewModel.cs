using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using BuySell.Model;
using Xamarin.Forms;

namespace BuySell.ViewModel
{
    public class BuyingViewModel : BaseViewModel
    {
        #region Constructor
        public BuyingViewModel(INavigation _nav)
        {
            GetBuyingList();
            navigation = _nav;
        }
        #endregion

        #region Properties
        private ObservableCollection<BuyingModel> _BuyingList = new ObservableCollection<BuyingModel>();
        public ObservableCollection<BuyingModel> BuyingList
        {
            get { return _BuyingList; }
            set { _BuyingList = value; OnPropertyChanged("BuyingList"); }
        }
        #endregion

        #region GetSellingList method
        public void GetBuyingList()
        {
            try
            {
                BuyingList.Clear();
                BuyingList = new ObservableCollection<BuyingModel>()
                {
                    new BuyingModel()
                    {
                         Image = "GreenDress",
                         Description = "Green Dress",
                         DollerValue = "15",
                         Size = "XXL",
                         Seller ="@talldh22",
                         NextImage = "NextArrow"
                    },
                     new BuyingModel()
                    {
                         Image = "GreenDress",
                         Description = "Green Dress",
                         DollerValue = "20",
                         Size = "S",
                         Seller ="@shortdp21",
                         NextImage = "NextArrow"
                     },
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

