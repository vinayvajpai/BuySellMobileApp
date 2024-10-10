using System;

using Xamarin.Forms;

namespace BuySell.ViewModel
{
    public class CustomTitleViewModel : BaseViewModel
    {
        #region Constructor
        public CustomTitleViewModel(INavigation _nav)
        {
            navigation = _nav;
        }
        #endregion

        #region PagePopCommand
        private Command _PagePopCommand;
        public Command PagePopCommand
        {
            get
            {
                return _PagePopCommand ?? (_PagePopCommand = new Command(async () =>
                {
                    try
                    {
                         if (navigation.ModalStack.Count > 0)
                        {
                            await navigation.PopModalAsync(true);
                        }
                        else
                        { 
                            IsTap = false;
                            await navigation.PopAsync(true);
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
             ));
            }
        }
        #endregion
    }
}

