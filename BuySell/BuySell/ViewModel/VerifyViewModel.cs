using System;
using System.Collections.Generic;
using System.Text;
using BuySell.Views;
using Xamarin.Forms;

namespace BuySell.ViewModel
{
    class VerifyViewModel:BaseViewModel
    {
        public VerifyViewModel(INavigation _nav) 
        {
            navigation = _nav;
        }

        private Command _EnterCodeCommand;
        public Command EnterCodeCommand
        {
            get { return _EnterCodeCommand ?? (_EnterCodeCommand = new Command(async () => await navigation.PushAsync(new EnterCodeView()))); }
        }

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
    }
}
