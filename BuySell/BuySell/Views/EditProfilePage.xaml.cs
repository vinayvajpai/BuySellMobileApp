using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Acr.UserDialogs;
using BuySell.Helper;
using BuySell.Model.LoginResponse;
using BuySell.ViewModel;
using Rg.Plugins.Popup.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace BuySell.Views
{
    public partial class EditProfilePage : ContentPage
    {
        EditProfileViewModel m_model;

        #region Constructor
        public EditProfilePage()
        {
            InitializeComponent();
            BindingContext = m_model = new EditProfileViewModel(this.Navigation);
            nameLbl.Text = (Constant.LoginUserData.FirstName).Replace("'", "");
            nameEntry.Text = nameLbl.Text;
            lastnameLbl.Text = (Constant.LoginUserData.LastName).Replace("'", "");
            lastnameEntry.Text = lastnameLbl.Text;
            usernameLbl.Text = Constant.LoginUserData.UserId;
            usernameEntry.Text = usernameLbl.Text;
            emailLbl.Text = Constant.LoginUserData.Email;
            emailEntry.Text = emailLbl.Text;
            passwordLbl.Text = Global.Password;
            passwordEntry.Text = passwordLbl.Text;

            if (passwordLbl.IsVisible == true)
            {
                foreach (var r in passwordLbl.Text)
                {
                    passwordLbl.Text = passwordLbl.Text.Replace(r.ToString(), "*");
                }
            }
            else
            {
                passwordLbl.Text = Global.Password;
            }
            passwordEntry.IsPassword = true;
            //Task.Run(() =>
            //{

            //});
            imgShowPassword.IsVisible = false;
        }
        #endregion

        async void MySizes_Tapped(System.Object sender, System.EventArgs e)
        {
            try
            {
                if (m_model.IsTap)
                    return;
                m_model.IsTap = true;
                await Navigation.PushAsync(new MySizesPage());
            }
            catch (Exception)
            {
                m_model.IsTap = false;
                throw;
            }

        }
        //Methods created to manage all fields at edit profile page.
        private void usernameButton_Tapped(object sender, EventArgs e)
        {

            usernameEntry.IsVisible = true;
            saveButton.IsVisible = true;
            saveImage.IsVisible = false;
            usernameEntry.Text = usernameLbl.Text;
            usernameLbl.IsVisible = false;
        }
        private void saveUsername_Tapped(object sender, EventArgs e)
        {
            usernameLbl.Text = usernameEntry.Text;
            usernameLbl.IsVisible = true;
            usernameEntry.IsVisible = false;
            usernameEntry.IsVisible = false;
            saveButton.IsVisible = false;
            saveImage.IsVisible = true;
        }
        private void FirstNameButton_Tapped(object sender, EventArgs e)
        {
            nameEntry.IsVisible = true;
            saveName.IsVisible = true;
            nameArrow.IsVisible = false;
            nameEntry.Text = nameLbl.Text;
            nameLbl.IsVisible = false;
        }
        private void saveFirstName_Tapped(object sender, EventArgs e)
        {
            nameEntry.IsVisible = false;
            saveName.IsVisible = false;
            nameArrow.IsVisible = true;
            nameLbl.Text = nameEntry.Text;
            nameLbl.IsVisible = true;
            nameEntry.IsVisible = false;
        }
        private void EmailButton_Tapped(object sender, EventArgs e)
        {

            emailEntry.IsVisible = true;
            saveEmail.IsVisible = true;
            emailArrow.IsVisible = false;
            emailEntry.Text = emailLbl.Text;
            emailLbl.IsVisible = false;
        }
        private void saveEmail_Tapped(object sender, EventArgs e)
        {
            emailEntry.IsVisible = false;
            saveEmail.IsVisible = false;
            emailArrow.IsVisible = true;
            emailLbl.Text = emailEntry.Text;
            emailLbl.IsVisible = true;
            emailEntry.IsVisible = false;
        }
        private void passImage_Tapped(object sender, EventArgs e)
        {
            passwordLbl.Text = null;
            passwordLbl.Text = passwordEntry.Text;
            //imgShowPassword.IsVisible = true;
            if (Global.Password != null)
            {
                if (Global.Password == m_model.Password)
                {
                    imgShowPassword.IsVisible = false;
                }
                else
                {
                    imgShowPassword.IsVisible = true;
                }
            }
            passwordEntry.IsVisible = true;
            savePass.IsVisible = true;
            passArrow.IsVisible = false;
            passwordLbl.IsVisible = false;
        }
        private void savePassword_Tapped(object sender, EventArgs e)
        {
            passwordLbl.Text = passwordEntry.Text;
            if (passwordLbl.IsVisible == false)
            {
                foreach (var r in passwordLbl.Text)
                {
                    passwordLbl.Text = passwordLbl.Text.Replace(r.ToString(), "*");
                }
            }
            else
            {
                passwordLbl.Text = Global.Password;
            }
            passwordEntry.IsPassword = true;
            imgShowPassword.IsVisible = false;
            passwordEntry.IsVisible = false;
            savePass.IsVisible = false;
            passArrow.IsVisible = true;
            passwordLbl.IsVisible = true;
            passwordEntry.IsVisible = false;
        }
        void ShowPassword_Tapped(System.Object sender, System.EventArgs e)
        {
            if (passwordEntry.IsPassword)
            {
                passwordEntry.IsPassword = false;
                imgShowPassword.Source = "hide";
            }
            else
            {
                passwordEntry.IsPassword = true;
                imgShowPassword.Source = "show";
            }
        }
        void LastNameButton_Tapped(System.Object sender, System.EventArgs e)
        {
            lastnameEntry.IsVisible = true;
            savelastName.IsVisible = true;
            lastnameArrow.IsVisible = false;
            lastnameEntry.Text = lastnameLbl.Text;
            lastnameLbl.IsVisible = false;
        }
        void saveLastName_Tapped(System.Object sender, System.EventArgs e)
        {
            lastnameEntry.IsVisible = false;
            savelastName.IsVisible = false;
            lastnameArrow.IsVisible = true;
            lastnameLbl.Text = lastnameEntry.Text;
            lastnameLbl.IsVisible = true;
            lastnameEntry.IsVisible = false;
        }
        void savePhone_Clicked(System.Object sender, System.EventArgs e)
        {
            PhoneEntry.IsVisible = false;
            savePhone.IsVisible = false;
            phoneArrow.IsVisible = true;
            lblPhone.IsVisible = true;
            //lblPhone.Text = PhoneEntry.Text;
            //m_model.ConvertPhoneNumber = PhoneEntry.Text;
            if (PhoneEntry.Text.Contains("(") && PhoneEntry.Text.Contains(")"))
            {
                m_model.ConvertPhoneNumber = PhoneEntry.Text;
            }
            else
            {
                m_model.ConvertPhoneNumber = string.Format("({0}) {1}-{2}", PhoneEntry.Text.Substring(0, 3), PhoneEntry.Text.Substring(3, 3), PhoneEntry.Text.Substring(6));
            }
            PhoneEntry.IsVisible = false;
        }
        void PhoneButton_Tapped(System.Object sender, System.EventArgs e)
        {
            PhoneEntry.IsVisible = true;
            savePhone.IsVisible = true;
            phoneArrow.IsVisible = false;
            if (!string.IsNullOrEmpty(lblPhone.Text))
            {
                //PhoneEntry.Text = Constant.LoginUserData.PhoneNumber;
                // PhoneEntry.Text = string.Format("({0}) {1}-{2}", Constant.LoginUserData.PhoneNumber.Substring(0, 3), Constant.LoginUserData.PhoneNumber.Substring(3, 3), Constant.LoginUserData.PhoneNumber.Substring(6));
            }
            lblPhone.IsVisible = false;
        }
        async protected override void OnAppearing()
        {
            m_model.IsTap = false;
            if (m_model != null)
            {
                //if (!string.IsNullOrEmpty(Convert.ToString(Constant.LoginUserData.ProfilePath)))
                //{
                // var data = m_model.DownLoadAndStoreImage(Constant.LoginUserData.ProfilePath.ToString());
                //    imgProfile.Source = ImageSource.FromStream(() =>
                //            new MemoryStream(data)
                //            );
                //    //imgProfile.Source = ImageSource.FromUri(new Uri(Convert.ToString(Constant.LoginUserData.ProfilePath)));
                //}
                if (Constant.LoginUserData.ProfilePath != null)
                {
                    editImg.IsEnabled = false;
                }
                await Device.InvokeOnMainThreadAsync(async () =>
                {
                    try
                    {
                        if (Constant.LoginUserData.ProfilePath != null)
                        {
                            using (WebClient client = new WebClient())
                            {
                                actInd.IsVisible = true;
                                actInd.IsRunning = true;
                                byte[] data = await client.DownloadDataTaskAsync(Convert.ToString(Constant.LoginUserData.ProfilePath));
                                imgProfile.Source = ImageSource.FromStream(() =>
                                        new MemoryStream(data)
                                        );
                                m_model.ProfilePicture = imgProfile.Source;
                                m_model.OrginalProfilePicture = imgProfile.Source;
                                actInd.IsVisible = false;
                                actInd.IsRunning = false;

                                editImg.IsEnabled = true;
                            }
                        }
                        else
                        {
                            editImg.IsEnabled = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        editImg.IsEnabled = true;
                        UserDialogs.Instance.Toast("Failed to load profile image: "+ex.Message);
                    }

                    if (m_model.ProfilePicture == null)
                    {
                        addProPicTxt.Text = "Add Photo";
                        m_model.AddProPictxt = true;
                    }
                    else
                    {
                        m_model.AddProPictxt = false;
                    }
                    editImg.IsEnabled = true;
                });

                if (Constant.globalSelectedFromAddress == null)
                {
                    m_model.GetAllShippingFromAddress();
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(Constant.globalSelectedFromAddress.AddressLine1))
                    {
                        m_model.GetAllShippingFromAddress();
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(Constant.globalSelectedFromAddress.AddressLine1))
                        {
                            m_model.ShipFromAdd = Constant.globalSelectedFromAddress.AddressLine1;
                        }
                        if (!string.IsNullOrWhiteSpace(Constant.globalSelectedFromAddress.AddressLine2))
                        {
                            m_model.ShipFromAdd = m_model.ShipFromAdd + ", " + Constant.globalSelectedFromAddress.AddressLine2;
                        }
                        if (!string.IsNullOrWhiteSpace(Constant.globalSelectedFromAddress.Country))
                        {
                            m_model.ShipFromAdd = m_model.ShipFromAdd + ", " + Constant.globalSelectedFromAddress.Country;
                        }
                        if (!string.IsNullOrWhiteSpace(Constant.globalSelectedFromAddress.ZipCode))
                        {
                            m_model.ShipFromAdd = m_model.ShipFromAdd + ", " + Constant.globalSelectedFromAddress.ZipCode;
                        }
                    }
                }
            }

            base.OnAppearing();
        }
        protected override bool OnBackButtonPressed()
        {
            if (PopupNavigation.PopupStack.Count > 0)
            {
                PopupNavigation.PopAsync();
            }
            return false;
        }
        private void EditProf_Tapped(object sender, EventArgs e)
        {
            nameLbl.Text = nameEntry.Text;
            nameEntry.IsVisible = false;
            nameLbl.IsVisible = true;
            saveName.IsVisible = false;
            nameArrow.IsVisible = true;

            emailLbl.Text = emailEntry.Text;
            emailLbl.IsVisible = true;
            emailEntry.IsVisible = false;
            saveEmail.IsVisible = false;
            emailArrow.IsVisible = true;

            passwordLbl.Text = passwordEntry.Text;
            if (passwordLbl.IsVisible == true || passwordLbl.IsVisible == false)
            {
                foreach (var r in passwordLbl.Text)
                {
                    passwordLbl.Text = passwordLbl.Text.Replace(r.ToString(), "*");
                }
            }
            passwordEntry.IsPassword = true;
            imgShowPassword.IsVisible = false;
            passwordEntry.IsVisible = false;
            savePass.IsVisible = false;
            passArrow.IsVisible = true;
            passwordLbl.IsVisible = true;
            passwordEntry.IsVisible = false;

            lastnameLbl.Text = lastnameEntry.Text;
            lastnameEntry.IsVisible = false;
            lastnameLbl.IsVisible = true;
            savelastName.IsVisible = false;
            lastnameArrow.IsVisible = true;

            PhoneEntry.IsVisible = false;
            savePhone.IsVisible = false;
            phoneArrow.IsVisible = true;
            lblPhone.IsVisible = true;
            if (!string.IsNullOrWhiteSpace(PhoneEntry.Text))
            {
                if (PhoneEntry.Text.Contains("(") && PhoneEntry.Text.Contains(")"))
                {
                    m_model.ConvertPhoneNumber = PhoneEntry.Text;
                }
                else
                {
                    m_model.ConvertPhoneNumber = string.Format("({0}) {1}-{2}", PhoneEntry.Text.Substring(0, 3), PhoneEntry.Text.Substring(3, 3), PhoneEntry.Text.Substring(6));
                }
            }
            PhoneEntry.IsVisible = false;
        }
        private void passwordEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.NewTextValue))
            {
                imgShowPassword.IsVisible = true;
            }
            else
            {
                imgShowPassword.IsVisible = false;
            }
        }

        private async void ShipFromAdd_Tapped(object sender, EventArgs e)
        {
            try
            {
                if (m_model.IsTap)
                    return;
                m_model.IsTap = true;

                await Navigation.PushAsync(new ShippingFromAddressPage());
            }
            catch (Exception ex)
            {
                m_model.IsTap = false;
                Debug.WriteLine(ex.Message);
            }
        }

        //private void PhoneEntry_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if(!string.IsNullOrEmpty(e.NewTextValue))
        //    {
        //        PhoneEntry.Text = string.Format("({0}) {1}-{2}", m_model.PhoneNumber.Substring(0, 3), m_model.PhoneNumber.Substring(3, 3), m_model.PhoneNumber.Substring(6));
        //    }
        //    else
        //    {
        //        PhoneEntry.Text = Constant.LoginUserData.PhoneNumber;
        //    }
        //}
    }
}
