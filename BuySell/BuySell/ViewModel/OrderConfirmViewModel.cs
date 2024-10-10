using System;
using System.Diagnostics;
using System.Linq;
using BuySell.Helper;
using BuySell.View;
using Xamarin.Forms;

namespace BuySell.ViewModel
{
    public class OrderConfirmViewModel : BaseViewModel
    {
        #region Constructor
        public OrderConfirmViewModel(INavigation _nav)
        {
            navigation = _nav;
        }
        #endregion

        #region Commands
        private Command _DoneCommand;
        public Command DoneCommand
        {
            get
            {
                return _DoneCommand ?? (_DoneCommand = new Command(async () =>
                {
                    try
                    {
                        if (IsTap)
                            return;
                        IsTap = true;
                        App.Current.MainPage = new NavigationPage(new DashBoardView(true));
                    }
                    catch (Exception ex)
                    {
                        IsTap = false;
                        Debug.WriteLine(ex.Message);
                    }
                    
                }
          ));
            }
        }
        #endregion
    }
}
