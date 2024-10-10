using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BuySell.Helper;
using Xamarin.Forms;

namespace BuySell.Views
{
    public partial class MySizesPropertyListPage : ContentPage
    {
        Label lblTitle;
        Frame bgFrame;
        public bool IsTap = false;
        public MySizesPropertyListPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            IsTap = false;
            base.OnAppearing();
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
                MessagingCenter.Send<object, string>("SelectPropertyValue", "SelectPropertyValue", param);
                this.Navigation.PopAsync();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                IsTap = false;
            }
           
        }
    }
}
