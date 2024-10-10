using Acr.UserDialogs;
using BuySell.Helper;
using BuySell.Model;
using FFImageLoading;
using Microsoft.AppCenter.Crashes;
using Stripe;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using static System.Net.WebRequestMethods;

namespace BuySell.ViewModel.Login_Flow
{
    public class OTPVerificationViewModel : BaseViewModel
    {
        #region Constructor

        public AddAddressModel Address { get; set; }

        public OTPVerificationViewModel(INavigation _nav, AddAddressModel addressModel)
        {
            navigation = _nav;
            if(addressModel != null )
            {
                Address = addressModel;
                MobileNumber = Address.PhoneNo;
            }

        }

        #endregion

        #region Properties

        private string _FullOTP = string.Empty;
        public string FullOTP
        {
            get
            {
                return _FullOTP;
            }
            set
            {
                _FullOTP = value;
                OnPropertyChanged(nameof(FullOTP));
            }
        }

        private string _OTP1;
        public string OTP1
        {
            get
            {
                return _OTP1;
            }
            set
            {
                _OTP1 = value;
                OnPropertyChanged(nameof(OTP1));
            }
        }
        private string _OTP2;
        public string OTP2
        {
            get
            {
                return _OTP2;
            }
            set
            {
                _OTP2 = value;
                OnPropertyChanged(nameof(OTP2));
            }
        }
        private string _OTP3;
        public string OTP3
        {
            get
            {
                return _OTP3;
            }
            set
            {
                _OTP3 = value;
                OnPropertyChanged(nameof(OTP3));
            }
        }
        private string _OTP4;
        public string OTP4
        {
            get
            {
                return _OTP4;
            }
            set
            {
                _OTP4 = value;
                OnPropertyChanged(nameof(OTP4));
            }
        }

        private string _MobileNumber;
        public string MobileNumber
        {
            get
            {
                return _MobileNumber;
            }
            set
            {
                _MobileNumber = value;
                OnPropertyChanged(nameof(MobileNumber));
            }
        }
         
        
        private string _ErrorMessage = string.Empty;
        public string ErrorMessage
        {
            get
            {
                return _ErrorMessage;
            }
            set
            {
                _ErrorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }
        
        private string _SendOTPBtnText = "Send OTP";
        public string SendOTPBtnText
        {
            get
            {
                return _SendOTPBtnText;
            }
            set
            {
                _SendOTPBtnText = value;
                OnPropertyChanged(nameof(SendOTPBtnText));
            }
        }

        private bool _IsAutoDetecting = false;
        public bool IsAutoDetecting
        {
            get
            {
                return _IsAutoDetecting;
            }
            set
            {
                _IsAutoDetecting = value;
                OnPropertyChanged(nameof(IsAutoDetecting));
            }
        }


        //private bool _IsShowReSendOTP = false;
        //public bool IsShowReSendOTP
        //{
        //    get
        //    {
        //        return _IsShowReSendOTP;
        //    }
        //    set
        //    {
        //        _IsShowReSendOTP = value;
        //        OnPropertyChanged(nameof(IsShowReSendOTP));
        //    }
        //}
        
        
        private bool _ShowErrorMessage = false;
        public bool ShowErrorMessage
        {
            get
            {
                return _ShowErrorMessage;
            }
            set
            {
                _ShowErrorMessage = value;
                OnPropertyChanged(nameof(ShowErrorMessage));
            }
        }

        private bool _IsSendOTPEnabled;
        public bool IsSendOTPEnabled
        {
            get { return _IsSendOTPEnabled; }
            set
            { 
                _IsSendOTPEnabled = value;
                OnPropertyChanged("IsSendOTPEnabled");
            }
        }

        private double _SendOTPOpacity=1.0;
        public double SendOTPOpacity
        {
            get { return _SendOTPOpacity; }
            set
            {
                _SendOTPOpacity = value;
                OnPropertyChanged("SendOTPOpacity");
            }
        }


        #endregion

        #region Command


        private ICommand _BackCommand;
        public ICommand BackCommand
        {
            get
            {
                return _BackCommand ?? (_BackCommand = new Command(async () =>
                {
                    try
                    {
                        if (IsTap)
                            return;

                        IsTap = true;
                        MessagingCenter.Unsubscribe<object, string>("SelectPropertyValue", "SelectPropertyValue");
                        await navigation.PopAsync();
                    }
                    catch (Exception ex)
                    {
                        IsTap = false;
                    }
                }
             ));
            }
        }


        private Command _SendOTPCommand;
        public Command SendOTPCommand
        {
            get
            {
                return _SendOTPCommand ?? (_SendOTPCommand = new Command(async () =>
                {
                    try
                    {
                        await SendOTPMethod();
                    }
                    catch (Exception ex)
                    {
                        IsTap = false;
                    }
                }
             ));
            }
        }

        private Command _SubmitOTPCommand;
        public Command SubmitOTPCommand
        {
            get
            {
                return _SubmitOTPCommand ?? (_SubmitOTPCommand = new Command(async () =>
                {
                    try
                    {
                       await SubmitOTPMethod();
                    }
                    catch (Exception ex)
                    {
                        IsTap = false;
                    }
                }
             ));
            }
        }       
        
        private Command _ChangePhoneNumberCmd;
        public Command ChangePhoneNumberCmd
        {
            get
            {
                return _ChangePhoneNumberCmd ?? (_ChangePhoneNumberCmd = new Command(async () =>
                {
                    try
                    {
                        if (IsTap)
                            return;

                        IsTap = true;
                        await navigation.PopAsync();
                    }
                    catch (Exception ex)
                    {
                        IsTap = false;
                    }
                }
             ));
            }
        }

        #endregion


        #region Method

        #region Send OTP method
        private async Task SendOTPMethod()
        {
            try
            {
                if (IsTap)
                    return;
                IsTap = true;

                Acr.UserDialogs.UserDialogs.Instance.Alert("OTP Sent");
                IsSendOTPEnabled = false;
                SendOTPOpacity = 0.7;


                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                Device.StartTimer(TimeSpan.FromSeconds(1), () =>
                {
                    TimeSpan elapsedTime = stopwatch.Elapsed;

                    // ShowErrorMessageMthod(string.Format("Try resend if OTP not received in 00:{0:00} seconds.",20 - elapsedTime.Seconds));

                    SendOTPBtnText = string.Format("Resend OTP ({0:00}) ", 20 - elapsedTime.Seconds);

                    if (elapsedTime.Seconds == 20)
                    {
                        stopwatch.Stop();
                        SendOTPBtnText = "Resend OTP";
                        ShowErrorMessage = false;
                        SendOTPOpacity = 1.0;
                        IsSendOTPEnabled = true;
                        return false;
                    }
                    return true;
                });

                IsTap = false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error in sending OTP" + ex.Message);
            }

        }

        #endregion


        #region  Submit OTP
        private async Task SubmitOTPMethod()
        {

            try
            {
                if (IsTap)
                    return;
                IsTap = true;

                if (!string.IsNullOrWhiteSpace(OTP1) && !string.IsNullOrWhiteSpace(OTP2) && !string.IsNullOrWhiteSpace(OTP3) && !string.IsNullOrWhiteSpace(OTP4) && !string.IsNullOrWhiteSpace(MobileNumber))
                {
                    FullOTP = OTP1 + OTP2 + OTP3 + OTP4;
                    // call API here  and submit fullOTP
                    await App.Current.MainPage.DisplayAlert("Information", "OTP Submitted", "Ok");
                    IsTap = false;
                }
                else if (string.IsNullOrWhiteSpace(MobileNumber))
                {
                    IsTap = false;
                   ShowErrorMessageMthod("Please enter mobile number");
                }
                else if (FullOTP.Length < 4)
                {
                    IsTap = false;
                    ShowErrorMessageMthod("Please enter a valid OTP");
                }
                else
                {
                    IsTap = false;
                    ShowErrorMessageMthod(Constant.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error in submitting OTP " + ex.Message);
            }
           
          
         }
        #endregion

        #region AutoDetectionMethod

        public async void AutoDetectionMethod()
        {
            // write code here to auto detect OTP.
            // it needs google hash key that's why it is pending.
        }

        #endregion



        #region Error Message

        public void ShowErrorMessageMthod(string message)
        {
            ErrorMessage = message;
            ShowErrorMessage = true; 
        }

        #endregion

        #endregion

    }
}
