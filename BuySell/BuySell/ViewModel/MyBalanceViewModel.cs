using System;
using System.Collections.ObjectModel;
using BuySell.Model;
using Xamarin.Forms;

namespace BuySell.ViewModel
{
    public class MyBalanceViewModel : BaseViewModel
    {
        #region Constructor
        public MyBalanceViewModel(INavigation _nav)
        {
            GetMyBalanceList();
            navigation = _nav;
        }
        #endregion

        #region Properties
        private ObservableCollection<MyBalanceModel> _MyBalanceList = new ObservableCollection<MyBalanceModel>();
        public ObservableCollection<MyBalanceModel> MyBalanceList
        {
            get { return _MyBalanceList; }
            set { _MyBalanceList = value; OnPropertyChanged("MyBalanceList"); }
        }
        #endregion

        #region Methods
        public void GetMyBalanceList()
        {
            try
            {
                MyBalanceList.Clear();
                MyBalanceList = new ObservableCollection<MyBalanceModel>()
                {
                    new MyBalanceModel()
                    {
                         Date = "30-Aug-2020",
                         Description = "Direct Bank Deposit",
                         CardNumber = "********1234",
                         Process = "Completed"
                    },
                     new MyBalanceModel()
                    {
                       Date = "30-Aug-2020",
                       Description = "Direct Bank Deposit",
                       CardNumber = "********1234",
                       Process = "Completed"
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
