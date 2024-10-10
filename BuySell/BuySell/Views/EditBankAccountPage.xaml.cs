using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using BuySell.Model;
using BuySell.ViewModel;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace BuySell.Views
{
    public partial class EditBankAccountPage : ContentPage
    {
        EditBankAccountViewModel vm;
        bool isEditing = false;
        public EditBankAccountPage()
        {
            InitializeComponent();
            lbltitle.Text = "Add Account";
            BindingContext = vm = new EditBankAccountViewModel(this.Navigation);
        }

        public EditBankAccountPage(BankAccountModel bankAcc, bool IsEdit)
        {
            InitializeComponent();
            lbltitle.Text = "Edit Account";
            isEditing = IsEdit;
            BindingContext = vm = new EditBankAccountViewModel(this.Navigation , bankAcc);
            if(vm != null )
            {
                vm.IsEdit = IsEdit;

            }
        }


        protected override void OnAppearing()
        {
            vm.IsTap = false;
            if(isEditing)
            {
                ConfmCheckingAccNo.IsVisible = false;
                CheckingAccNo.IsReadOnly = true;
                CheckingAccNo.TextColor = Color.Gray;
                RoutingNumber.IsReadOnly = true;
                RoutingNumber.TextColor = Color.Gray;
            }
            else
            {
                RoutingNumber.IsReadOnly = false;
                ConfmCheckingAccNo.IsVisible = true;
                CheckingAccNo.IsReadOnly = false;
                RoutingNumber.TextColor = Color.Black;
                CheckingAccNo.TextColor = Color.Black;
            }
            base.OnAppearing();
        }

        private void CheckingAccNo_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (!isEditing)
                {
                    var regex = new Regex(@"^[0-9]{1,17}$");
                    if (regex.IsMatch(CheckingAccNo.Text))
                    {
                        AccNoInvalid.IsVisible = false;
                        vm.IsTap = false;
                    }
                    else
                    {
                        AccNoInvalid.IsVisible = true;
                        vm.IsTap = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
           
        }

        private void ConfmCheckingAccNo_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (!isEditing)
                {
                    var regex = new Regex(@"^[0-9]{1,17}$");
                    if (regex.IsMatch(CheckingAccNo.Text) && CheckingAccNo.Text == ConfmCheckingAccNo.Text)
                    {
                        CnfmAccNoInvalid.IsVisible = false;
                        vm.IsTap = false;
                    }
                    else
                    {
                        CnfmAccNoInvalid.IsVisible = true;
                        vm.IsTap = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Confirm Account number" + ex.Message);
            }
           
        }

        private void RoutingNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (!isEditing)
                {
                    var regex = new Regex(@"^\d{9}$");
                    if (regex.IsMatch(RoutingNumber.Text))
                    {
                        RoutAccNoInvalid.IsVisible = false;
                        vm.IsTap = false;
                    }
                    else
                    {
                        RoutAccNoInvalid.IsVisible = true;
                        vm.IsTap = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }
    }
}
