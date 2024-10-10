using System;
using System.Diagnostics;
using System.Windows.Input;
using BuySell.Views;
using Xamarin.Forms;

namespace BuySell.ViewModel
{
    public class PaymentMethodsViewModel : BaseViewModel
    {
        #region Constructor
        public PaymentMethodsViewModel(INavigation _nav)
        {
            navigation = _nav;
        }
        #endregion

        #region Commands
        private ICommand _AddCardCommand;
        public ICommand AddCardCommand
        {
            get
            {
                if (_AddCardCommand == null)
                {
                    _AddCardCommand = new Command(() =>
                    {
                        try
                        {
                            if (IsTap)
                                return;
                            IsTap = true;
                            navigation.PushAsync(new AddCardPage());
                        }
                        catch (Exception ex)
                        {
                            IsTap = false;
                            Debug.WriteLine(ex.Message);
                        }
                    });
                }

                return _AddCardCommand;
            }
        }
        #endregion
    }
}

