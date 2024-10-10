using System;
using System.Diagnostics;
using BuySell.Helper;
using BuySell.Views;
using Xamarin.Forms;

namespace BuySell.ViewModel
{
    public class OurStoresViewModel : BaseViewModel
    {
        #region Constructor
        public OurStoresViewModel(INavigation _nav)
        {
            navigation = _nav;
        }
        #endregion

        #region Properties
        private string _ClothingDescription = Constant.ClothingDescriptionStr;
        public string ClothingDescription
        {
            get { return _ClothingDescription; }
            set { _ClothingDescription = value; OnPropertyChanged("ClothingDescription"); }
        }

        private string _SneakersDescription = Constant.SneakersDescriptionStr;
        public string SneakersDescription
        {
            get { return _SneakersDescription; }
            set { _SneakersDescription = value; OnPropertyChanged("SneakersDescription"); }
        }

        private string _StreetwearDescription = Constant.StreetwearDescriptionStr;
        public string StreetwearDescription
        {
            get { return _StreetwearDescription; }
            set { _StreetwearDescription = value; OnPropertyChanged("StreetwearDescription"); }
        }

        private string _VintageDescription = Constant.VintageDescriptionStr;
        public string VintageDescription
        {
            get { return _VintageDescription; }
            set { _VintageDescription = value; OnPropertyChanged("VintageDescription"); }
        }
        #endregion

        #region Commands
        private Command _BackCommand;
        public Command BackCommand
        {
            get
            {
                return _BackCommand ?? (_BackCommand = new Command(async () =>
                {
                    try
                    {
                        if (IsTap)
                            return;
                        IsTap = true;
                        await navigation.PopAsync();
                    }
                    catch (Exception ex)
                    {
                        IsTap = false;
                        Debug.WriteLine(ex.Message);
                    }

                }));

            }
        }
        #endregion
    }
}
