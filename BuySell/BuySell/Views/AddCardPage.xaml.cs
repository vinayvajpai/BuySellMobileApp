using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using BuySell.Model;
using BuySell.ViewModel;
using Stripe;
using Xamarin.Forms;

namespace BuySell.Views
{
    public partial class AddCardPage : ContentPage
    {
        AddCardViewModel vm;
        private bool v;

        public AddCardPage()
        {
            InitializeComponent();
            BindingContext = vm = new AddCardViewModel(this.Navigation);
        }
        public AddCardPage(CardListModel obj)
        {
            InitializeComponent();
            BindingContext = vm = new AddCardViewModel(this.Navigation);
        }
        public AddCardPage(Action<CardModel> action)
        {
            InitializeComponent();
            BindingContext = vm = new AddCardViewModel(this.Navigation, action);
        }
        public AddCardPage(Action<CardModel> action, CardModel addCardModel)
        {
            InitializeComponent();
            BindingContext = vm = new AddCardViewModel(this.Navigation, action);
            vm.addCardModel = addCardModel;
        }

        public AddCardPage(Action<CardModel> action, CardModel addCardModel, bool v)
        {
            InitializeComponent();
            BindingContext = vm = new AddCardViewModel(this.Navigation, action ,true);
            vm.addCardModel = addCardModel;
        }

        protected override void OnAppearing()
        {
            vm.IsTap = false;
            base.OnAppearing();
            
        }


        void txtExp_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            var pattern = new Regex("^[0-9]{2}/[0-9]{2}$");
            if (!string.IsNullOrWhiteSpace(txtExp.Text))
            {
                if (!pattern.IsMatch(txtExp.Text))
                {
                    txtExpInvalid.IsVisible = true;
                    txtExpInvalid.Text = "Invalid expiry date";
                    SaveButton.IsEnabled = false;
                    return;
                }
                else
                {
                    txtExpInvalid.IsVisible = false;
                    SaveButton.IsEnabled = true;
                }
            }

            if (string.IsNullOrWhiteSpace(txtExp.Text))
            {
                if (txtExp.Text.Length == 5)
                {
                    var expDate = txtExp.Text.Split('/');
                    var res = Helper.Global.IsValidExpiryDate(Convert.ToInt16(expDate[0]), Convert.ToInt16(expDate[1]));
                    if (!res)
                    {
                        txtExp.BorderColor = Color.Red;
                        SaveButton.IsEnabled = false;
                        Acr.UserDialogs.UserDialogs.Instance.Toast("Invalid date! Expiry date must be future date");
                    }
                    else
                    {
                        txtExp.BorderColor = Color.Gray;
                        SaveButton.IsEnabled = true;
                    }
                }
            }
        }

        private void CreditCardNo_TextChanged(object sender, TextChangedEventArgs e)
        {
            var pattern = new Regex(@"^(?:\d{4}\s{1}){3}\d{3,4}$");

            if (!string.IsNullOrWhiteSpace(CreditCardNo.Text))
            {

                if (!pattern.IsMatch(CreditCardNo.Text))
                {
                    CardInvalid.IsVisible = true;
                    CardInvalid.Text = "Should be minimum 15 digits";
                    SaveButton.IsEnabled = false;
                }
                else
                {
                    CardInvalid.IsVisible = false;
                    SaveButton.IsEnabled = true;
                }
            }
        }

        private void PhoneNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            var pattern = new Regex("^\\+?\\d{1,4}?[-.\\s]?\\(?\\d{1,3}?\\)?[-.\\s]?\\d{1,4}[-.\\s]?\\d{1,4}[-.\\s]?\\d{1,9}$");


            if (!string.IsNullOrWhiteSpace(PhoneNumber.Text))
            {
                if (!pattern.IsMatch(PhoneNumber.Text))
                {
                    PhoneNumberInvalid.IsVisible = true;
                    PhoneNumberInvalid.Text = "Invalid phone number";
                    SaveButton.IsEnabled = false;
                }
                else
                {
                    PhoneNumberInvalid.IsVisible = false;
                    SaveButton.IsEnabled = true;
                }
            }

        }

        private void CountryPickerText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(CountryPickerText.Text))
            {
                CountryInvlaid.IsVisible = false;
            }
        }


        private void CVVTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            var pattern = new Regex("^[0-9]{3,4}$");

            if (!string.IsNullOrWhiteSpace(CVVTxt.Text))
            {
                if (!pattern.IsMatch(CVVTxt.Text))
                {
                    CvvInvalid.IsVisible = true;
                    CvvInvalid.Text = "Should be 3 or 4 digits";
                    SaveButton.IsEnabled = false;
                }
                else
                {
                    CvvInvalid.IsVisible = false;
                    SaveButton.IsEnabled = true;
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
                    SaveButton.IsEnabled = false;
                }
                else
                {
                    NameOnCardInvalid.IsVisible = false;
                    SaveButton.IsEnabled = true;
                }
            }
        }

        private void BillingAddress_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(BillingAddress.Text))
            { 
            StreetAddInvalid.IsVisible = true;
            StreetAddInvalid.Text = "please enter address";
            }
            else
            {
                StreetAddInvalid.IsVisible = false;
            }

        }

        private void AptSuiteTxt_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void CityTxt_TextChanged(object sender, TextChangedEventArgs e)
        {

            //var pattern = new Regex("^[ A-Za-z]+$");


            //if (!string.IsNullOrWhiteSpace(CityTxt.Text))
            //{
            //    if (!pattern.IsMatch(CityTxt.Text))
            //    {
            //        CityInvalid.IsVisible = true;
            //        CityInvalid.Text = "Should only contains charecters and spaces";
            //        SaveButton.IsEnabled = false;
            //    }
            //    else
            //    {
            //        CityInvalid.IsVisible = false;
            //        SaveButton.IsEnabled = true;
            //    }
            //}
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
                    SaveButton.IsEnabled = false;
                }
                else
                {
                    ZipInvalid.IsVisible = false;
                    SaveButton.IsEnabled = true;
                }
            }
        }

        private void CreditCardNo_Completed(object sender, EventArgs e)
        {
            var pattern = new Regex(@"^(?:\d{4}\s{1}){3}\d{3,4}$");

            if (!string.IsNullOrWhiteSpace(CreditCardNo.Text))
            {

                if (!pattern.IsMatch(CreditCardNo.Text))
                {
                    CardInvalid.IsVisible = true;
                    CardInvalid.Text = "Should be minimum 15 digits";
                    SaveButton.IsEnabled = false;
                }
                else
                {
                    CardInvalid.IsVisible = false;
                    SaveButton.IsEnabled = true;
                }
            }
        }

        private void PhoneNumber_Completed(object sender, EventArgs e)
        {
            var pattern = new Regex("^\\+?\\d{1,4}?[-.\\s]?\\(?\\d{1,3}?\\)?[-.\\s]?\\d{1,4}[-.\\s]?\\d{1,4}[-.\\s]?\\d{1,9}$");


            if (!string.IsNullOrWhiteSpace(PhoneNumber.Text))
            {
                if (!pattern.IsMatch(PhoneNumber.Text))
                {
                    PhoneNumberInvalid.IsVisible = true;
                    PhoneNumberInvalid.Text = "Invalid phone number";
                    SaveButton.IsEnabled = false;
                }
                else
                {
                    PhoneNumberInvalid.IsVisible = false;
                    SaveButton.IsEnabled = true;
                }
            }
        }

        private void txtExp_Completed(object sender, EventArgs e)
        {
            var pattern = new Regex("^[0-9]{2}/[0-9]{2}$");

            if (!string.IsNullOrWhiteSpace(txtExp.Text))
            {
                if (!pattern.IsMatch(txtExp.Text))
                {
                    txtExpInvalid.IsVisible = true;
                    txtExpInvalid.Text = "invalid expiry date";
                    SaveButton.IsEnabled = false;
                }
                else
                {
                    txtExpInvalid.IsVisible = false;
                    SaveButton.IsEnabled = true;
                }
            }
        }

        private void CVVTxt_Completed(object sender, EventArgs e)
        {
            var pattern = new Regex("^[0-9]{3,4}$");

            if (!string.IsNullOrWhiteSpace(CVVTxt.Text))
            {
                if (!pattern.IsMatch(CVVTxt.Text))
                {
                    CvvInvalid.IsVisible = true;
                    CvvInvalid.Text = "Should be 3 or 4 digits";
                    SaveButton.IsEnabled = false;
                }
                else
                {
                    CvvInvalid.IsVisible = false;
                    SaveButton.IsEnabled = true;
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
                    NameOnCardInvalid.Text = "Should only contains charecters and space";
                    SaveButton.IsEnabled = false;
                }
                else
                {
                    NameOnCardInvalid.IsVisible = false;
                    SaveButton.IsEnabled = true;
                }
            }
        }

        private void BillingAddress_Completed(object sender, EventArgs e)
        {
            
            //var pattern = new Regex("^(\\d{1,}) [a-zA-Z0-9\\s]+(\\,)? [a-zA-Z]+(\\,)? [A-Z]{2} [0-9]{5,6}$");


            //if (!string.IsNullOrWhiteSpace(BillingAddress.Text))
            //{
            //    if (!pattern.IsMatch(BillingAddress.Text))
            //    {
            //        StreetAddInvalid.IsVisible = true;
            //    }
            //    else
            //    {
            //        StreetAddInvalid.IsVisible = false;
            //    }
            //}
        }

        private void AptSuiteTxt_Completed(object sender, EventArgs e)
        {

            //var pattern = new Regex("^(\\d{1,}) [a-zA-Z0-9\\s]+(\\,)? [a-zA-Z]+(\\,)? [A-Z]{2} [0-9]{5,6}$");


            //if (!string.IsNullOrWhiteSpace(AptSuiteTxt.Text))
            //{
            //    if (!pattern.IsMatch(AptSuiteTxt.Text))
            //    {
            //        AptSuiteInvalid.IsVisible = true;
            //    }
            //    else
            //    {
            //        AptSuiteInvalid.IsVisible = false;
            //    }
            //}
        }

        private void CityTxt_Completed(object sender, EventArgs e)
        {

            //var pattern = new Regex("^[ A-Za-z]+$");


            //if (!string.IsNullOrWhiteSpace(CityTxt.Text))
            //{
            //    if (!pattern.IsMatch(CityTxt.Text))
            //    {
            //        CityInvalid.IsVisible = true;
            //        CityInvalid.Text = "Should only contains charecters and spaces";
            //        SaveButton.IsEnabled = false;
            //    }
            //    else
            //    {
            //        CityInvalid.IsVisible = false;
            //        SaveButton.IsEnabled = true;
            //    }
            //}
        }

        private void ZipTxt_Completed(object sender, EventArgs e)
        {
           // var pattern = new Regex("^[0-9]{5}(?:-[0-9]{4})?$");
            var pattern = new Regex("^[0-9]{5,6}$");

            if (!string.IsNullOrWhiteSpace(ZipTxt.Text))
            {
                if (!pattern.IsMatch(ZipTxt.Text))
                {
                    ZipInvalid.IsVisible = true;
                    ZipInvalid.Text = "Should be 5 or 6 digits";
                    SaveButton.IsEnabled = false;
                }
                else
                {
                    ZipInvalid.IsVisible = false;
                    SaveButton.IsEnabled = true;
                }
            }

        }

        private async void SaveButton_Tapped(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(vm.addCardModel.Number))
            {
                CardInvalid.IsVisible = true;
                CardInvalid.Text = "please enter phone number";
            }
            if (string.IsNullOrWhiteSpace(vm.addCardModel.Expire))
            {
                txtExpInvalid.IsVisible = true;
                txtExpInvalid.Text = "please enter expire date";
            }
            if (string.IsNullOrWhiteSpace(vm.addCardModel.Cvc))
            {
                CvvInvalid.IsVisible = true;
                CvvInvalid.Text = "please enter cvv";
            }
            if (string.IsNullOrWhiteSpace(vm.addCardModel.PhoneNo))
            {
                PhoneNumberInvalid.IsVisible = true;
                PhoneNumberInvalid.Text = "please enter phone number";
            }
            if (string.IsNullOrWhiteSpace(vm.addCardModel.Name))
            {
                NameOnCardInvalid.IsVisible = true;
                NameOnCardInvalid.Text = "please enter the name";
            }
            if (string.IsNullOrWhiteSpace(vm.addCardModel.AddressLine1))
            {
                StreetAddInvalid.IsVisible = true;
                StreetAddInvalid.Text = "please enter address";
            }
            //if (string.IsNullOrWhiteSpace(vm.addCardModel.City))
            //{
            //    CityInvalid.IsVisible = true;
            //    CityInvalid.Text = "please enter the city name";
            //}
            if (string.IsNullOrWhiteSpace(vm.addCardModel.State))
            {
                CountryInvlaid.IsVisible = true;
                CountryInvlaid.Text = "please select state";
            }
            if (string.IsNullOrWhiteSpace(vm.addCardModel.ZipCode))
            {
                ZipInvalid.IsVisible = true;
                ZipInvalid.Text = "please enter zip code";
            }
            vm.AddCardmethod();

        }

    }
}
