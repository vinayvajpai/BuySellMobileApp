using System;
using System.Collections.Generic;
using BuySell.Helper;
using BuySell.ViewModel;
using Xamarin.Forms;

namespace BuySell.Views
{
    public partial class BankAccountPage : ContentPage
    {
        BankAccountViewModel vm;
        public BankAccountPage()
        {
            InitializeComponent();
            BindingContext = vm = new BankAccountViewModel(this.Navigation);
            vm.PropertyChanged += Vm_PropertyChanged;
        }

        private void Vm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (vm.BankAccounts.Count > 0)
            {
                //lblNoData.IsVisible = false;
                BankAccountsList.IsVisible = true;
            }
            else
            {
                //lblNoData.IsVisible = true;
                BankAccountsList.IsVisible = false;
            }
        }

        protected override void OnAppearing()
        {
            try
            {
                if(vm != null)
                {
                    vm.IsTap = false;
                    if(Global.BankAccountsList != null)
                    {
                        vm.GetAllBankAccounts();
                    }
                }
               
                base.OnAppearing();
            }
            catch (Exception ex)
            {

            }
          
        }
    }
}
