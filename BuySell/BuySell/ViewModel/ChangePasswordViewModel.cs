using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BuySell.ViewModel
{
    public class ChangePasswordViewModel : BaseViewModel
    {
        private readonly Services.IMessageService _messageService;
        #region Constructor
        public ChangePasswordViewModel(INavigation nav)
        {
            this._messageService = DependencyService.Get<Services.IMessageService>();
            navigation = nav;
        }
        #endregion

        //#region Command
        private Command _BackCommand;
        public Command BackCommand
        {
            get
            {
                return _BackCommand ?? (_BackCommand = new Command(async () => await navigation.PopAsync()));
            }
        }
        //private Command _ContinueCommand;
        //public Command ContinueCommand
        //{
        //    get
        //    {
        //        return _ContinueCommand ?? (_ContinueCommand = new Command(async () => await GetChangePasswordMethod()));
        //    }
        //}
        //#endregion 
    }
}
