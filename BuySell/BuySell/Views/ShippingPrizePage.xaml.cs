using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuySell.Helper;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BuySell.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShippingPrizePage : ContentPage
    {
        Label lblTitle;
        Frame bgFrame;
        public ShippingPrizePage(string titleName)
        {
            InitializeComponent();
            lblTitleText.Text = titleName;
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
                MessagingCenter.Send<object, bool>("IsTapChanged", "IsTapChanged", false);
                this.Navigation.PopAsync();


            }
            catch (Exception ex)
            {
                //IsTap = false;
                Debug.WriteLine(ex.Message);
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            MessagingCenter.Send<object, bool>("IsTapChanged", "IsTapChanged", false);
            Navigation.PopAsync();

        }
    }
}