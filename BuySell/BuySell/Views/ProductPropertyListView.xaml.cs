using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BuySell.Helper;
using BuySell.Model;
using BuySell.Popup;
using BuySell.ViewModel;
using Rg.Plugins.Popup.Services;
using Stripe;
using Xamarin.Forms;

namespace BuySell.Views
{
    public partial class ProductPropertyListView : ContentPage
    {
        Label lblTitle;
        Frame bgFrame;
        bool _isFilter;
        bool IsTap = false;
        public ProductPropertyListView( string titleName)
        {
            InitializeComponent();
            lblTitleText.Text = titleName;
        }

        public ProductPropertyListView(string titleName, bool IsFilter)
        {
            InitializeComponent();
            lblTitleText.Text = titleName;
            _isFilter = IsFilter;
        }

        void SelectProperty_Tapped(System.Object sender, System.EventArgs e)
        {
            try
            {
                var param = Convert.ToString(((TappedEventArgs)e).Parameter);
                if (lblTitle != null)
                {
                    lblTitle.TextColor = Color.Black;
                    bgFrame.BorderColor = Color.WhiteSmoke;
                }
                var frame = (Frame)sender;
                var Allchild = frame.Children.AsEnumerable();
                var lbl = Allchild.ElementAt(0) as Label;
                frame.BorderColor = Color.FromHex(Global.GetThemeColor(Global.setThemeColor));
                lbl.TextColor = Color.FromHex(Global.GetThemeColor(Global.setThemeColor));
                bgFrame = frame;
                lblTitle = lbl;

                //case for checking if selected size is custom
                if (_isFilter == true && param.ToLower() == Constant.CustomSizeStr.ToLower())
                {
                    try
                    {
                        if (IsTap)
                            return;
                        IsTap = true;
                        var popupdefault = new CustomSizePopup();
                        popupdefault.eventOK += Popupdefault_eventOK;
                        popupdefault.eventCancel += Popupdefault_eventCancel;
                        PopupNavigation.Instance.PushAsync(popupdefault);
                        listProperties.ListView.SelectedItem = null;
                        return;
                    }
                    catch (Exception ex)
                    {
                        IsTap = false;
                        Debug.WriteLine(ex.Message);
                    }
                }
                else
                {
                    this.Navigation.PopAsync();
                    if(param == "NWT(New with price tag attached)")
                    {
                        param = "NWT";
                    }
                    else if(param == "NWOT(New with price tag removed)")
                    {
                        param = "NWOT";
                    }
                    else if(param == "Used(Preowned)")
                    {
                        param = "Used";
                    }
                    MessagingCenter.Send<object, string>("SelectPropertyValue", "SelectPropertyValue", param);
                    MessagingCenter.Send<object, bool>("IsTapChanged", "IsTapChanged", false);
                    MessagingCenter.Send<object, bool>("IsTapChangedFilter", "IsTapChangedFilter", false);
                }
            }
            catch (Exception ex)
            {
                IsTap = false;
                Debug.WriteLine(ex.Message);
            }
           
        }

        private void Popupdefault_eventCancel(object sender, EventArgs e)
        {
            IsTap = false;
            PopupNavigation.Instance.PopAsync();
        }

        private void Popupdefault_eventOK(object sender, string e)
        {
            try
            {
                this.Navigation.PopAsync();
                MessagingCenter.Send<object, string>("SelectPropertyValue", "SelectPropertyValue", e);
                MessagingCenter.Send<object, bool>("IsTapChanged", "IsTapChanged", false);
                MessagingCenter.Send<object, bool>("IsTapChangedFilter", "IsTapChangedFilter", false);
                IsTap = false;
            }
            catch (Exception)
            {
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            MessagingCenter.Send<object, bool>("IsTapChanged", "IsTapChanged", false);
            MessagingCenter.Send<object, bool>("IsTapChangedFilter", "IsTapChangedFilter", false);
            Navigation.PopAsync();
            
        }
    }
}
