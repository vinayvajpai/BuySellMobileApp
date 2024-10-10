using BuySell.ViewModel.Login_Flow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BuySell.Views.Login_Flow
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserRegistrationPage : ContentPage
    {
        int Counter = 0;
        bool isSeller = false;

        UserRegistrationViewModel vm;
        public UserRegistrationPage(bool isSeller)
        {
            InitializeComponent();
            BindingContext  = vm = new UserRegistrationViewModel(this.Navigation);
            this.isSeller = isSeller;
        }

        protected override void OnAppearing()
        {
            if(vm != null)
            vm.IsTap = false;
            Counter = 0;

            if (isSeller)
                titleControl.Text = "Seller Registration";
            else
                titleControl.Text = "Buyer Registration";

            base.OnAppearing();
        }

        private void CityTxt_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ShippingAdd1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Counter != 0)
            {
                if (string.IsNullOrEmpty(ShippingAdd1.Text))
                {
                    ShipAddInvalid.IsVisible = true;
                    ShipAddInvalid.Text = "Invalid shipping address";
                    vm.IsTap = true;
                }
                else
                {
                    ShipAddInvalid.IsVisible = false;
                    vm.IsTap = false;
                }
            }

            Counter++;
        }

        private void CountryPickerText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(CountryPickerText.Text))
            {
                CountryInvalid.IsVisible = true;
            }
            else
            {
                CountryInvalid.IsVisible = false;
            }
        }

        private void ZipTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            var pattern = new Regex("^[0-9]{5,6}$");

            if (ZipTxt.Text != null)
            {
                if (ZipTxt.Text.Contains("."))
                {
                    ZipTxt.Text = ZipTxt.Text.Remove(ZipTxt.Text.Length - 1, 1);
                }
            }

            if (!string.IsNullOrWhiteSpace(ZipTxt.Text))
            {
                if (!pattern.IsMatch(ZipTxt.Text))
                {
                    ZipInvalid.IsVisible = true;
                    ZipInvalid.Text = "Should be 5 or 6 digits";
                    vm.IsTap = true;
                }
                else
                {
                    ZipInvalid.IsVisible = false;
                    vm.IsTap = false;
                }
            }
        }

        private void PhoneNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            var pattern = new Regex("^\\(?([0-9]{3})\\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$");


            if (!string.IsNullOrWhiteSpace(PhoneNumber.Text))
            {
                if (!pattern.IsMatch(PhoneNumber.Text))
                {
                    PhoneNumberInvalid.IsVisible = true;
                    PhoneNumberInvalid.Text = "Invalid phone number";
                    vm.IsTap = true;
                }
                else
                {
                    PhoneNumberInvalid.IsVisible = false;
                    vm.IsTap = false;
                }
            }
        }


        private void NameOnCard_TextChanged(object sender, TextChangedEventArgs e)
        {
            var pattern = new Regex("^[a-zA-Z\\s]+$");


            if (!string.IsNullOrWhiteSpace(NameOnCard.Text))
            {
                if (!pattern.IsMatch(NameOnCard.Text))
                {
                    NameOnCardInvalid.IsVisible = true;
                    NameOnCardInvalid.Text = "Should only contains charecters and spaces";
                    vm.IsTap = true;
                }
                else
                {
                    NameOnCardInvalid.IsVisible = false;
                    vm.IsTap = false;
                }
            }
        }


        private void NameOnCard_Completed(object sender, EventArgs e)
        {
            var pattern = new Regex("^[a-zA-Z\\s]+$");


            if (!string.IsNullOrWhiteSpace(NameOnCard.Text))
            {
                if (!pattern.IsMatch(NameOnCard.Text))
                {
                    NameOnCardInvalid.IsVisible = true;
                    NameOnCardInvalid.Text = "Should only contains charecters and spaces";
                    vm.IsTap = true;
                }
                else
                {
                    NameOnCardInvalid.IsVisible = false;
                    vm.IsTap = false;
                }
            }
        }


        private void CityTxt_Completed(object sender, EventArgs e)
        {

        }

        private void ShippingAdd1_Completed(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ShippingAdd1.Text))
            {
                ShipAddInvalid.IsVisible = true;
                ShipAddInvalid.Text = "Invalid shipping address";
                vm.IsTap = true;
            }
            else
            {
                ShipAddInvalid.IsVisible = false;
                vm.IsTap = false;
            }
        }


        private void ZipTxt_Completed(object sender, EventArgs e)
        {
            var pattern = new Regex("^[0-9]{5,6}$");

            if (!string.IsNullOrWhiteSpace(ZipTxt.Text))
            {
                if (!pattern.IsMatch(ZipTxt.Text))
                {
                    ZipInvalid.IsVisible = true;
                    ZipInvalid.Text = "Should be 5 or 6 digits";
                    vm.IsTap = true;
                }
                else
                {
                    ZipInvalid.IsVisible = false;
                    vm.IsTap = false;
                }
            }
        }
        private void PhoneNumber_Completed(object sender, EventArgs e)
        {
            var pattern = new Regex("^\\(?([0-9]{3})\\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$");


            if (!string.IsNullOrWhiteSpace(PhoneNumber.Text))
            {
                if (!pattern.IsMatch(PhoneNumber.Text))
                {
                    PhoneNumberInvalid.IsVisible = true;
                    PhoneNumberInvalid.Text = "Invalid phone number";
                    vm.IsTap = true;
                }
                else
                {
                    PhoneNumberInvalid.IsVisible = false;
                    vm.IsTap = false;
                }
            }
        }
    }
}