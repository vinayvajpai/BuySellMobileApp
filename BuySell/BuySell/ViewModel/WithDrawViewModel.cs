using BuySell.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace BuySell.ViewModel
{
    public class WithDrawViewModel : BaseViewModel
    {
        #region Constructor
        public WithDrawViewModel(INavigation _nav)
        {
            navigation = _nav;
        }
        #endregion

        #region Properties
        private int _withDrawAmount = 0;
        public int WithDrawAmount
        {
            get { return _withDrawAmount; }
            set { _withDrawAmount = value; OnPropertyChanged("WithDrawAmount"); }
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
                        await navigation.PushAsync(new WithDrawlSummaryView(WithDrawAmount));
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