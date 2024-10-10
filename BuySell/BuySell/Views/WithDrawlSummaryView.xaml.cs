using BuySell.Helper;
using BuySell.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BuySell.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WithDrawlSummaryView : ContentPage
    {
        WithDrawlSummaryViewModel vm;

        #region Constructor

        public WithDrawlSummaryView(double WithdrawlAmount)
        {
            InitializeComponent();
            BindingContext = vm = new WithDrawlSummaryViewModel(this.Navigation, WithdrawlAmount);
        }
        #endregion

        protected override void OnAppearing()
        {
            vm.IsTap = false;
            base.OnAppearing();
            if(Constant.globalBankAccount != null)
            {
                if (Constant.globalBankAccount.AccountNumber != null)
                {
                    vm.CheckAccNo = Constant.globalBankAccount.AccountNumber;
                }
                else
                {
                    vm.GetAllBankAccounts();
                }
            }
            else
            {
                vm.GetAllBankAccounts();
            }
        }

        void AddAccount_Tapped(System.Object sender, System.EventArgs e)
        {
            this.Navigation.PushAsync(new BankAccountPage());
        }
    }
}