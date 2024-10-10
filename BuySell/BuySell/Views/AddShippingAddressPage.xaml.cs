using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using BuySell.Helper;
using BuySell.Model;
using BuySell.ViewModel;
using Xamarin.Forms;

namespace BuySell.Views
{	
	public partial class AddShippingAddressPage : ContentPage
	{
        int Counter = 0;
        AddShippingAddressViewModel vm {
			get
			{
				return (AddShippingAddressViewModel)this.BindingContext;
			}
		}

        public bool V { get; }

        public AddShippingAddressPage ()
		{
			InitializeComponent ();
			BindingContext = new AddShippingAddressViewModel(this.Navigation);
        }
        public AddShippingAddressPage(Action<AddAddressModel> action)
        {
            InitializeComponent();
            BindingContext  = new AddShippingAddressViewModel(this.Navigation, action);
        }
        public AddShippingAddressPage(Action<AddAddressModel> action,AddAddressModel addAddressModel)
        {
            InitializeComponent();
            BindingContext = new AddShippingAddressViewModel(this.Navigation, action);
            vm.AddAddModel = addAddressModel;
        }

        public AddShippingAddressPage(Action<AddAddressModel> action, AddAddressModel addAddressModel, bool IsEdit)
        {
            InitializeComponent();
            BindingContext = new AddShippingAddressViewModel(this.Navigation, action, IsEdit );
            vm.AddAddModel = addAddressModel;

        }

        protected override void OnAppearing()
        {
            vm.IsTap = false;
            Counter = 0;
            base.OnAppearing();
        }

        private void CityTxt_TextChanged(object sender, TextChangedEventArgs e)
        {

            //var pattern = new Regex("^[ A-Za-z]+$");


            //if (!string.IsNullOrWhiteSpace(CityTxt.Text))
            //{
            //    if (!pattern.IsMatch(CityTxt.Text))
            //    {
            //        CityInvalid.IsVisible = true;
            //        CityTxt.Text = "Should only contains charecters and spaces";
            //        vm.IsTap = true;
            //    }
            //    else
            //    {
            //        CityInvalid.IsVisible = false;
            //        vm.IsTap = false;
            //    }
            //}
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

            if(ZipTxt.Text != null)
            {
               if(ZipTxt.Text.Contains("."))
                {
                    ZipTxt.Text =  ZipTxt.Text.Remove(ZipTxt.Text.Length-1, 1);
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

            //var pattern = new Regex("^[ A-Za-z]+$");


            //if (!string.IsNullOrWhiteSpace(CityTxt.Text))
            //{
            //    if (!pattern.IsMatch(CityTxt.Text))
            //    {
            //        CityInvalid.IsVisible = true;
            //        CityInvalid.Text = "Should only contains charecters and spaces";
            //        vm.IsTap = true;
            //    }
            //    else
            //    {
            //        CityInvalid.IsVisible = false;
            //        vm.IsTap = false;
            //    }
            //}
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
            // var pattern = new Regex("^[0-9]{5}(?:-[0-9]{4})?$");
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

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameOnCard.Text))
            {
                NameOnCardInvalid.IsVisible = true;
                NameOnCardInvalid.Text = "Please enter name on card";
            }
            if (string.IsNullOrWhiteSpace(ShippingAdd1.Text))
            {
                ShipAddInvalid.IsVisible = true;
                ShipAddInvalid.Text = "Please enter shipping address";
            }
            //if (string.IsNullOrWhiteSpace(CityTxt.Text))
            //{
            //    CityInvalid.IsVisible = true;
            //    CityInvalid.Text = "Please enter city name";
            //}

            if (string.IsNullOrWhiteSpace(ZipTxt.Text))
            {
                ZipInvalid.IsVisible = true;
                ZipInvalid.Text = "Please enter zip code";
            }
            if (string.IsNullOrWhiteSpace(CountryPickerText.Text))
            {
                CountryInvalid.IsVisible = true;
                CountryInvalid.Text = "Please select state name";
            }
            if(!string.IsNullOrWhiteSpace(NameOnCard.Text) && !string.IsNullOrWhiteSpace(ShippingAdd1.Text) && !string.IsNullOrWhiteSpace(ZipTxt.Text) && !string.IsNullOrWhiteSpace(CountryPickerText.Text))
            {
                await vm.SaveMethod();
            }
           
        }
    }
}

