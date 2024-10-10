using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using BuySell.Model;
using BuySell.Views;
using Xamarin.Forms;

namespace BuySell.ViewModel
{
    public class MySizesViewModel : BaseViewModel
    {
        #region Constructor
        public MySizesViewModel(INavigation _nav)
        {
            navigation = _nav;
        }
        #endregion

        #region Properties
        private ObservableCollection<string> _SelectMySizeList = new ObservableCollection<string>();
        public ObservableCollection<string> SelectMySizeList
        {
            get
            {
                return _SelectMySizeList;
            }
            set
            {
                _SelectMySizeList = value;
                OnPropertyChanged("SelectMySizeList");
            }
        }
        private int _SelectedPropertyIndex = 0;
        public int SelectedPropertyIndex
        {
            get { return _SelectedPropertyIndex; }
            set { _SelectedPropertyIndex = value; OnPropertyChanged("SelectedPropertyIndex"); }
        }
        private MySizesModel _mySizesModel = new MySizesModel();
        public MySizesModel mySizesModel
        {
            get { return _mySizesModel; }
            set { _mySizesModel = value; OnPropertyChanged("mySizesModel"); }
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
        public async Task GetMySizesList(int index)
        {
            try
            {
                if (IsTap)
                    return;
                IsTap = true;

                SelectedPropertyIndex = index;
                if (index == 1)
                {
                    SelectMySizeList = new ObservableCollection<string>() { "XXS", "XS", "S", "M", "L", "XL", "XXL" };
                }
                else if (index == 2)
                {
                    SelectMySizeList = new ObservableCollection<string>() { "XXS", "XS", "S", "M", "L", "XL", "XXL" };
                }
                else if (index == 3)
                {
                    SelectMySizeList = new ObservableCollection<string>() { "XXS", "XS", "S", "M", "L", "XL", "XXL" };
                }
                else if (index == 4)
                {
                    SelectMySizeList = new ObservableCollection<string>() { "XXS", "XS", "S", "M", "L", "XL", "XXL" };
                }
                else if (index == 5)
                {
                    SelectMySizeList = new ObservableCollection<string>() { "XXS", "XS", "S", "M", "L", "XL", "XXL" };
                }
                MessagingCenter.Subscribe<object, string>(this, "SelectPropertyValue", (sender, arg) =>
                {
                    if (arg != null)
                    {
                        SetSelectedValues(arg, SelectedPropertyIndex);
                    }
                });
                var mySizesPropertyListPage = new MySizesPropertyListPage();
                mySizesPropertyListPage.BindingContext = this;

                await navigation.PushAsync(mySizesPropertyListPage);
            }
            catch (Exception ex)
            {
                IsTap = false;
                Debug.WriteLine(ex.Message);
            }
        }

        public void SetSelectedValues(string value, int index)
        {
            try
            {
                if (index == 1)
                {
                    mySizesModel.Dresses = value;
                }
                else if (index == 2)
                {
                    mySizesModel.Tops = value;
                }
                else if (index == 3)
                {
                    mySizesModel.Bottoms = value;
                }
                else if (index == 4)
                {
                    mySizesModel.Jeans = value;
                }
                else if (index == 5)
                {
                    mySizesModel.Shoes = value;
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
    }
}
