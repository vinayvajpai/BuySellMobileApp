using Acr.UserDialogs;
using BuySell.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace BuySell.ViewModel
{
    public class TaxCenterViewModel : BaseViewModel
    {
        #region Constructor
        public TaxCenterViewModel(INavigation _nav)
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

        private Command _DeleteCrossCommand;
        public Command DeleteCrossCommand
        {
            get
            {
                return _DeleteCrossCommand ?? (_DeleteCrossCommand = new Command(async () =>
                {
                    try
                    {
                        if (IsTap)
                            return;

                        IsTap = true;
                        await navigation.PopAsync();
                    }
                    catch (Exception ex)
                    {
                        IsTap = false;
                    }
                }
             ));
            }
        }

        private Command _QuestionCommand;
        public Command QuestionCommand
        {
            get
            {
                return _QuestionCommand ?? (_QuestionCommand = new Command(async () =>
                {
                    try
                    {
                        if (IsTap)
                            return;

                        IsTap = true;
                        App.Current.MainPage = new NavigationPage(new FAQView());
                    }
                    catch (Exception ex)
                    {
                        IsTap = false;
                    }
                }
             ));
            }
        }

        private Command _SubmitCommand;
        public Command SubmitCommand
        {
            get
            {
                return _SubmitCommand ?? (_SubmitCommand = new Command(async () =>
                {
                    try
                    {
                        if (IsTap)
                            return;

                        IsTap = true;
                        App.Current.MainPage = new NavigationPage(new WithDrawlSummaryView(0));
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
    }
}