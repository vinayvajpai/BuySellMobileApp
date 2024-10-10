using BuySell.Model;
using BuySell.ViewModel.Login_Flow;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static System.Net.WebRequestMethods;

namespace BuySell.Views.Login_Flow
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OTPVerificationPage : ContentPage
    {
        string tempOTP = string.Empty;
        OTPVerificationViewModel vm;
        public OTPVerificationPage(AddAddressModel  addressModel)
        {
            InitializeComponent();
            BindingContext = vm = new OTPVerificationViewModel(this.Navigation, addressModel);
        }

        protected override void OnAppearing()
        {
            if (vm != null)
                vm.IsTap = false;

            base.OnAppearing();
        }

        private void OnOtpEntry1TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(e.OldTextValue))
                {
                    tempOTP += otpEntry1.Text;
                }
                else
                {
                    tempOTP = tempOTP.Remove(tempOTP.Length - 1, 1);
                }
                if (!string.IsNullOrWhiteSpace(e.NewTextValue))
                {
                    if (!intVerifier(e.NewTextValue))
                    {
                        vm.ShowErrorMessage = true;
                        vm.ErrorMessage = "Please enter valid value";
                    }
                    else
                    {
                        vm.ShowErrorMessage = false;
                    }

                    if (e.NewTextValue.Length > 0)
                    {
                        otpEntry2.Focus();
                    }
                }

                if (!string.IsNullOrWhiteSpace(tempOTP) && tempOTP?.Length == 4)
                {
                    ContinueBtn.IsEnabled = true;
                    ContinueBtn.Opacity = 1.0;
                }
                else
                {
                    ContinueBtn.IsEnabled = false;
                    ContinueBtn.Opacity = 0.7;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }
        private void OnOtpEntry2TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(e.OldTextValue) && !string.IsNullOrWhiteSpace(e.NewTextValue))
                {
                    tempOTP += otpEntry2.Text;
                }
                else
                {
                    tempOTP = tempOTP.Remove(tempOTP.Length - 1, 1);
                }

                if (!string.IsNullOrWhiteSpace(e.NewTextValue))
                {
                    if (!intVerifier(e.NewTextValue))
                    {
                        vm.ShowErrorMessage = true;
                        vm.ErrorMessage = "Please enter valid value";
                    }
                    else
                    {
                        vm.ShowErrorMessage = false;
                    }

                    if (e.NewTextValue.Length > 0)
                    {
                        otpEntry3.Focus();
                    }
                }
                if (e.OldTextValue != null)
                {
                    if (string.IsNullOrWhiteSpace(e.NewTextValue) || e.NewTextValue?.Length == 0)
                    {
                        otpEntry1.Focus();
                    }
                }

                if (!string.IsNullOrWhiteSpace(tempOTP) && tempOTP?.Length == 4)
                {
                    ContinueBtn.IsEnabled = true;
                    ContinueBtn.Opacity = 1.0;
                }
                else
                {
                    ContinueBtn.IsEnabled = false;
                    ContinueBtn.Opacity = 0.7;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        private void OnOtpEntry3TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(e.OldTextValue))
                {
                    tempOTP += otpEntry3.Text;
                }
                else
                {
                    tempOTP = tempOTP.Remove(tempOTP.Length - 1, 1);
                }

                if (!string.IsNullOrWhiteSpace(e.NewTextValue))
                {
                    if (!intVerifier(e.NewTextValue))
                    {
                        vm.ShowErrorMessage = true;
                        vm.ErrorMessage = "Please enter valid value";
                    }
                    else
                    {
                        vm.ShowErrorMessage = false;
                    }

                    if (e.NewTextValue.Length > 0)
                    {
                        otpEntry4.Focus();
                    }
                }
                if (e.OldTextValue != null)
                {
                    if (string.IsNullOrWhiteSpace(e.NewTextValue) || e.NewTextValue?.Length == 0)
                    {
                        otpEntry2.Focus();
                    }
                }


                if (!string.IsNullOrWhiteSpace(tempOTP) && tempOTP?.Length == 4)
                {
                    ContinueBtn.IsEnabled = true;
                    ContinueBtn.Opacity = 1.0;
                }
                else
                {
                    ContinueBtn.IsEnabled = false;
                    ContinueBtn.Opacity = 0.7;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
           
        }        
        private void OnOtpEntry4TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(e.OldTextValue))
                {
                    tempOTP += otpEntry4.Text;
                }
                else
                {
                    tempOTP = tempOTP.Remove(tempOTP.Length-1, 1);
                }

                if (!string.IsNullOrWhiteSpace(e.NewTextValue))
                {
                    if (!intVerifier(e.NewTextValue))
                    {
                        vm.ShowErrorMessage = true;
                        vm.ErrorMessage = "Please enter valid value";
                    }
                    else
                    {
                        vm.ShowErrorMessage = false;
                    }
                }

                if (e.OldTextValue != null)
                {
                    if (string.IsNullOrWhiteSpace(e.NewTextValue)|| e.NewTextValue?.Length == 0)
                    {
                        otpEntry3.Focus();
                    }
                }

                if (!string.IsNullOrWhiteSpace(tempOTP) && tempOTP?.Length == 4)
                {
                    ContinueBtn.IsEnabled = true;
                    ContinueBtn.Opacity = 1.0;
                }
                else
                {
                    ContinueBtn.IsEnabled = false;
                    ContinueBtn.Opacity = 0.7;
                }
            }
            catch (Exception ex) 
            { 
                Debug.WriteLine(ex.Message); 
            }
           
        }

       public bool intVerifier(string val)
        {
           Regex regex = new Regex(@"^\d+$");
            if(regex.IsMatch(val))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}