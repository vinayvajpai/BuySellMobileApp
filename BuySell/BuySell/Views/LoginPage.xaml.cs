using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuySell.Helper;
using BuySell.ViewModel;
using BuySell.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BuySell.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        LoginViewModel vm;
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = vm = new LoginViewModel(this.Navigation);
        }

        protected override void OnAppearing()
        {
            vm.IsTap = false;
            base.OnAppearing();
        }

        void Skip_Tapped(System.Object sender, System.EventArgs e)
        {
            try
            {
                if (vm.IsTap)
                    return;
                vm.IsTap = true;

                Global.setThemeColor = ThemesColor.BlueColor;
                App.Current.MainPage = new NavigationPage(new DashBoardView(true,true));
            }
            catch (Exception ex)
            {
                vm.IsTap = false;
                Debug.WriteLine(ex.Message);
            }

        }

        void Back_Tapped(System.Object sender, System.EventArgs e)
        {
            try
            {
                if (vm.IsTap)
                    return;
                vm.IsTap = true;

                App.Current.MainPage = new NavigationPage(new HomePage());
            }
            catch (Exception ex)
            {
                vm.IsTap = false;
                Debug.WriteLine(ex.Message);
            }
        }

        void ResetPassword_Tapped(object sender, EventArgs e)
        {
            try
            {
                if (vm.IsTap)
                    return;
                vm.IsTap = true;

                this.Navigation.PushAsync(new ResetPasswordPage());
            }
            catch (Exception ex)
            {
                vm.IsTap = false;
                Debug.WriteLine(ex.Message);
            }
        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            try
            {
                if (vm.IsTap)
                    return;
                vm.IsTap = true;

                this.Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                vm.IsTap = false;
                Debug.WriteLine(ex.Message);
            }

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
    }
}