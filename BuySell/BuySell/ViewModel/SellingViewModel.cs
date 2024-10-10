using System;
using System.Collections.ObjectModel;
using BuySell.Model;
using Xamarin.Forms;

namespace BuySell.ViewModel
{
    public class SellingViewModel : BaseViewModel
    {
        #region Constructor
        public SellingViewModel(INavigation _nav)
        {
            GetSellingList();
            navigation = _nav;
        }
        #endregion

        #region Properties
        private ObservableCollection<SellingModel> _SellingList = new ObservableCollection<SellingModel>();
        public ObservableCollection<SellingModel> SellingList
        {
            get { return _SellingList; }
            set { _SellingList = value; OnPropertyChanged("SellingList"); }
        }
        #endregion

        #region Methods
        public void GetSellingList()
        {
            try
            {
                SellingList.Clear();
                SellingList = new ObservableCollection<SellingModel>()
                {
                    new SellingModel()
                    {
                         Image = "GreenDress",
                         Description = "Blue Cotton Trouser",
                         DollerValue = "20",
                         Size = "XXL",
                         Buyer ="@talldh 22",
                         NextImage = "NextArrow"
                    },
                     new SellingModel()
                    {
                         Image = "GreenDress",
                         Description = "Blue Cotton Trouser",
                         DollerValue = "20",
                         Size = "XXL",
                         Buyer ="@talldh 22",
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
