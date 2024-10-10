using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using BuySell.Helper;
using BuySell.View;
using Xamarin.Forms;

namespace BuySell.ViewModel
{
    public class CustomHeaderViewModel : BaseViewModel
    {
        #region Constructor
        public CustomHeaderViewModel(INavigation _nav)
        {
            navigation = _nav;
        }
        #endregion

        #region Command
        private ICommand _BackHomeCommand;
        public ICommand BackHomeCommand
        {
            get
            {
                if (_BackHomeCommand == null)
                {
                    _BackHomeCommand = new Command(async () =>
                    {
                        try
                        {
                            if (IsTap)
                                return;

                            IsTap = true;

                            //if (App.Current.MainPage.GetType() == typeof(NavigationPage))
                            //{
                            //    var dashBoardView = ((NavigationPage)App.Current.MainPage).Navigation.NavigationStack.FirstOrDefault(p => p is DashBoardView);
                            //    if (dashBoardView != null)
                            //    {
                            //        int pageIndex = ((NavigationPage)App.Current.MainPage).Navigation.NavigationStack.ToList().IndexOf(dashBoardView);
                            //        navigation.RemovePage(((NavigationPage)App.Current.MainPage).Navigation.NavigationStack[pageIndex]);
                            //        await navigation.PopAsync();
                            //    }
                            //    else
                            //    {
                            //        Global.IsUploadedProduct = true;
                            //        App.Current.MainPage = new NavigationPage(new DashBoardView());
                            //    }
                            //}
                            //else
                            //{
                            //    Global.IsUploadedProduct = true;
                            //    App.Current.MainPage = new NavigationPage(new DashBoardView());
                            //}

                            Global.IsUploadedProduct = true;
                            App.Current.MainPage = new NavigationPage(new DashBoardView(true));
                            await Task.Delay(1000);
                            IsTap = false;

                        }
                        catch (Exception ex)
                        {
                            IsTap = false;
                            Debug.WriteLine(ex.Message);
                        }

                    });
                }

                return _BackHomeCommand;
            }
        }
        #endregion
    }
}
