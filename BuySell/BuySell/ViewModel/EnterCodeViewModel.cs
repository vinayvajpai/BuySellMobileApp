using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BuySell.ViewModel
{
    class EnterCodeViewModel:BaseViewModel
    {
        public EnterCodeViewModel(INavigation _nav) 
        {
            navigation = _nav;
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
