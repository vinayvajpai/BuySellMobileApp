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
    public partial class ChangePasswordPage : ContentPage
    {
        ChangePasswordViewModel vm;
        public ChangePasswordPage()
        {
            InitializeComponent();
            BindingContext = vm = new ChangePasswordViewModel(this.Navigation);
        }

        private void ShowPassword_Tapped(object sender, EventArgs e)
        {
            if (passwordtxt.IsPassword)
            {
                passwordtxt.IsPassword = false;
                lblShowPassword.Text = "Hide";
            }
            else
            {
                passwordtxt.IsPassword = true;
                lblShowPassword.Text = "Show";
            }
        }

        private void ShowConfirmPassword_Tapped(object sender, EventArgs e)
        {
            if (confirmpasstxt.IsPassword)
            {
                confirmpasstxt.IsPassword = false;
                lblShowConfirmPass.Text = "Hide";
            }
            else
            {
                confirmpasstxt.IsPassword = true;
                lblShowConfirmPass.Text = "Show";
            }
        }
    }
}