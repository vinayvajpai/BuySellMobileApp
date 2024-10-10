using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BuySell.ViewModel
{
    class MultiplePaymentMethodsViewModel : BaseViewModel
    {
        #region Constructor
        public MultiplePaymentMethodsViewModel(INavigation _nav)
        {
            navigation = _nav;
        }
        #endregion

        #region Properties
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
                        await navigation.PopAsync(true);
                    }
                    catch (Exception ex)
                    {
                        IsTap = false;
                    }
                }
             ));
            }
        }
        #endregion

        #region Methods
        #endregion

    }
}
