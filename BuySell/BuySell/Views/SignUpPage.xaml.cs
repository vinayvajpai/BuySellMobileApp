using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using Acr.UserDialogs;
using BuySell.Helper;
using BuySell.Model.SignUpResponse;
using BuySell.ViewModel;
using Rg.Plugins.Popup.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BuySell.Views
{
    public partial class SignUpPage : ContentPage
    {
        SignUpViewModel m_model;
        public SignUpPage()
        {
            InitializeComponent();
            BindingContext = m_model = new SignUpViewModel(this.Navigation);
            //MessagingCenter.Subscribe<object, byte[]>("IsImgAdd", "IsImgAdd", (sender, arg) =>
            //{
            //    if (arg != null)
            //    {
            //        SelectedAddImgMethod(arg);
            //    }
            //});

            //MessagingCenter.Subscribe<object, byte[]>("ImageToBeDelete", "ImageToBeDelete", (sender, arg) =>
            //{
            //    if (arg != null)
            //    {
            //        SelectedDeletedImgMethod(arg);
            //    }
            //});
        }

        protected override void OnAppearing()
        {
            m_model.IsTap = false;
            base.OnAppearing();
        }
        //private void SelectedAddImgMethod(byte[] byt)
        //{
        //    try
        //    {
        //        UserDialogs.Instance.ShowLoading();
        //        Device.InvokeOnMainThreadAsync(() =>
        //        {
        //            var arg = ImageSource.FromStream(() => new MemoryStream(byt));
        //            if (m_model.IsUploadTagImage)
        //            {
        //                if(m_model.ImageToBeEdited == "EditingImage1")
        //                {
        //                    m_model.imageList.RemoveAt(0);
        //                    m_model.imageList.Insert(0, arg);
        //                    //imgAdd1.Source = arg;
        //                    m_model.ProfilePicture = arg;
        //                    Editimg1.IsVisible = true;
        //                    m_model.ImageToBeEdited = "null";
        //                }
        //                else
        //                {
        //                    m_model.imageList.Add(arg);
        //                    if (m_model.imageList.Count == 1)
        //                    {
        //                        Device.InvokeOnMainThreadAsync(() =>
        //                        {
        //                            //imgAdd1.Source = arg;
        //                            m_model.ProfilePicture = arg;
        //                            AddPic1Txt.IsVisible = false;
        //                            Editimg1.IsVisible = true;
        //                        });
        //                    }
        //                    m_model.ImgList = m_model.imageList;
        //                }
        //            }
        //            UserDialogs.Instance.HideLoading();
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex.Message);
        //        UserDialogs.Instance.HideLoading();
        //    }
        //}
        //private void SelectedDeletedImgMethod(byte[] DelImage)
        //{
        //    Device.InvokeOnMainThreadAsync(() =>
        //    {
        //        if (DelImage != null)
        //        {
        //            if (m_model.ImgEdit == "EditingImage1")
        //            {
        //                m_model.imageList.RemoveAt(0);
        //                //imgAdd1.Source = null;
        //                m_model.ProfilePicture = null;
        //                AddPic1Txt.IsVisible = true;
        //                Editimg1.IsVisible = false;
        //            }
        //            m_model.ImageToBeEdited = "null";
        //            if (PopupNavigation.PopupStack.Count > 0)
        //            {
        //                PopupNavigation.PopAsync();
        //            }
        //        }
        //    });
        //}
        void Back_Tapped(System.Object sender, System.EventArgs e)
        {
            MessagingCenter.Unsubscribe<object, byte[]>("IsImgAdd", "IsImgAdd");
            MessagingCenter.Unsubscribe<object, byte[]>("ImageToBeDelete", "ImageToBeDelete");
            Global.RemoveHomePageAndInsertLogin(this.Navigation);
        }
        void ShowPassword_Tapped(System.Object sender, System.EventArgs e)
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
        void ShowConfirmPassword_Tapped(System.Object sender, System.EventArgs e)
        {
            if (confirmpasswordtxt.IsPassword)
            {
                confirmpasswordtxt.IsPassword = false;
                lblShowConfirmPassword.Text = "Hide";
            }
            else
            {
                confirmpasswordtxt.IsPassword = true;
                lblShowConfirmPassword.Text = "Show";
            }
        }
        void Email_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            var email = emailEntry.Text;
            var emailRegex = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
            if (Regex.IsMatch(email, emailRegex))
            {
                emailEntry.TextColor = Color.Default;
            }
            else
            {
                emailEntry.TextColor = Color.Red;
            }
        }

        //void PhoneNumber_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        //{
        //    var phoneNumber = phoneNumberEntry.Text;
        //    var phoneNumberRegex = @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$";
        //    if (Regex.IsMatch(phoneNumber, phoneNumberRegex))
        //    {
        //        phoneNumberEntry.TextColor = Color.Default;
        //    }
        //    else
        //    {
        //        phoneNumberEntry.TextColor = Color.Red;
        //    }
        //}
    }
}
