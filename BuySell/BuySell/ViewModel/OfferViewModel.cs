using System;
using System.Collections.ObjectModel;
using BuySell.Model;
using Xamarin.Forms;

namespace BuySell.ViewModel
{
    public class OfferViewModel : BaseViewModel
    {
        #region Constructor
        public OfferViewModel(INavigation _nav)
        {
            GetOfferList();
            navigation = _nav;
        }
        #endregion

        #region Properties
        private ObservableCollection<OfferModel> _OfferList = new ObservableCollection<OfferModel>();
        public ObservableCollection<OfferModel> OfferList
        {
            get { return _OfferList; }
            set { _OfferList = value; OnPropertyChanged("OfferList"); }
        }
        #endregion

        #region Methods
        public void GetOfferList()
        {
            try
            {
                OfferList.Clear();
                OfferList = new ObservableCollection<OfferModel>()
                {
                    new OfferModel()
                    {
                         Image = "GreenDress",
                         Description = "Blue Cotton Trouser",
                         DollerValue = "150",
                         OfferValue = "12",
                         Size = "XXL",
                         Seller ="@talldh 22",
                         NextImage = "NextArrow"
                    },
                     new OfferModel()
                    {
                         Image = "GreenDress",
                         Description = "Blue Cotton Trouser",
                         DollerValue = "150",
                         OfferValue = "12",
                         Size = "XXL",
                         Seller ="@talldh 22",
                         NextImage = "NextArrow"
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

