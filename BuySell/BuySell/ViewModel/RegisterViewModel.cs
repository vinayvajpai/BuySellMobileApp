using System;
using System.Collections.Generic;
using System.Text;
using Acr.UserDialogs;
using BuySell.Helper;
using BuySell.Views;
using Xamarin.Forms;

namespace BuySell.ViewModel
{
    class RegisterViewModel:BaseViewModel
    {
        #region Constructor
        public RegisterViewModel(INavigation _nav) 
        {
            navigation = _nav;
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

        private Command _HelpNeedCommand;
        public Command HelpNeedCommand
        {
            get
            {
                return _HelpNeedCommand ?? (_HelpNeedCommand = new Command(async () =>
                {
                    try
                    {
                        if (IsTap)
                            return;

                        IsTap = true;
                        Global.AlertWithAction("This information helps us verify buyers so we can keep a safe and secure marketplace", () =>
                        {
                            //App.Current.MainPage = new NavigationPage(new ContactSupportPage());
                        }, actionTitle: "Contact Support",title: "Why do you need this info?");
                        //var alertConfig = new AlertConfig
                        //{
                        //    Title = "Why do you need this info?",
                        //    Message = "This information helps us verify buyers so we can keep a safe and secure marketplace,",
                        //    OkText = "Contact Support",
                        //    OnAction = () =>
                        //    {
                        //        //App.Current.MainPage = new NavigationPage(new ContactSupportPage());
                        //    }
                        //};
                        //Acr.UserDialogs.UserDialogs.Instance.Alert(alertConfig);
                    }
                    catch (Exception ex)
                    {
                        IsTap = false;
                    }
                }
             ));
            }
        }

        private Command _VerifyCommand;
        public Command VerifyCommand
        {
            get { return _VerifyCommand ?? (_VerifyCommand = new Command(async () => await navigation.PushAsync(new VerifyView()))); }
        }
        #endregion
    }
}
