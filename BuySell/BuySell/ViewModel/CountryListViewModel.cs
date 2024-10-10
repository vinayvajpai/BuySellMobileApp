using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Input;
using BuySell.Helper;
using BuySell.Model;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace BuySell.ViewModel
{
    class CountryListViewModel: BaseViewModel
    {
        #region Constructor
        public CountryListViewModel(INavigation _nav)
        {
            navigation = _nav;
            stateList = new ObservableCollection<string>(JsonConvert.DeserializeObject<List<string>>(Constant.stateJson));
        }
        #endregion

        #region Properties
        //private List<CountryModel> _countryList = new List<CountryModel>()
        //{
        //    _countryList.Add(new CountryModel(){ countryName = "Madhya Pradesh"})
        //};

        //public List<CountryModel> countryList
        //{
        //    get { return _countryList; }
        //    set { _countryList = value; OnPropertyChanged("countryList"); }
        //}

        private ObservableCollection<string> _stateList;
        public ObservableCollection<string> stateList
        {
            get { return _stateList; }
            set { _stateList = value; OnPropertyChanged("stateList"); }
        }

        #endregion

        #region Commands
        private ICommand _BackCommand;
        public ICommand BackCommand
        {
            get
            {
                if (_BackCommand == null)
                {
                    _BackCommand = new Command(() =>
                    {
                        try
                        {
                            if (IsTap)
                                return;
                            IsTap = true;
                            navigation.PopAsync();
                        }
                        catch (Exception ex)
                        {
                            IsTap = false;
                            Debug.WriteLine(ex.Message);
                        }
                    });
                }

                return _BackCommand;
            }
        }
        #endregion

        #region Methods
        #endregion

        #region Model
        public class CountryModel { 
        public string countryName { get; set; }
        }

        public class StateName
        {
            public string State { get; set; }
            
        }
        #endregion
    }
    
}
