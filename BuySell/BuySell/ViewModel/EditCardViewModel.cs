using System;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;

namespace BuySell.ViewModel
{
    public class EditCardViewModel : BaseViewModel
    {
        #region Constructor
        public EditCardViewModel(INavigation _nav)
        {
            navigation = _nav;
        }
        #endregion

        #region Coomand
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
    }
}

