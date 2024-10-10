using System;
using System.Collections.Generic;
using BuySell.ViewModel;
using Xamarin.Forms;

namespace BuySell.Views
{	
	public partial class ResetConfirmPasswordPage : ContentPage
	{
		ResetConfirmPasswordViewModel viewModel;
		public ResetConfirmPasswordPage ()
		{
			InitializeComponent ();
			BindingContext = viewModel = new ResetConfirmPasswordViewModel(this.Navigation);
		}

        void Back_Tapped(System.Object sender, System.EventArgs e)
        {
			this.Navigation.PopAsync();
        }

        void ShowPassword_Tapped(System.Object sender, System.EventArgs e)
        {
            if (passwordtxt.IsPassword)
            {
                passwordtxt.IsPassword = false;
                imgShowPassword.Source = "hide.png";
            }
            else
            {
                passwordtxt.IsPassword = true;
                imgShowPassword.Source = "show.png";
            }
        }

        void ShowConfirmPassword_Tapped(System.Object sender, System.EventArgs e)
        {
            if (confirmPasswordtxt.IsPassword)
            {
                confirmPasswordtxt.IsPassword = false;
                imgShowConfirmPassword.Source = "hide.png";
            }
            else
            {
                confirmPasswordtxt.IsPassword = true;
                imgShowConfirmPassword.Source = "show.png";
            }
        }
    }
}

