using System;
using System.Diagnostics;
using BuySell.Views;
using Xamarin.Forms;

namespace BuySell.ViewModel
{
    public class HomeViewModel : BaseViewModel
    {
        #region Constructor
        public HomeViewModel(INavigation nav)
        {
            navigation = nav;
        }
        #endregion

        #region Commands
        private Command _SignUpCommand;
        public Command SignUpCommand
        {
            get
            {
                return _SignUpCommand ?? (_SignUpCommand = new Command(async () =>
                {
                    try
                    {
                        if (IsTap)
                            return;
                        IsTap = false;
                        await navigation.PushAsync(new SignUpPage());
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
