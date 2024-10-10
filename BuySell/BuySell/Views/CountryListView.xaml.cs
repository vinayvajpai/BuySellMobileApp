using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuySell.Helper;
using BuySell.Model.LoginResponse;
using BuySell.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static BuySell.ViewModel.CountryListViewModel;

namespace BuySell.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CountryListView : ContentPage
	{
		CountryListViewModel vm;
        public CountryListView ()
		{
			InitializeComponent ();
			BindingContext = vm = new CountryListViewModel(this.Navigation);
		}
        void SelectProperty_Tapped(System.Object sender, System.EventArgs e)
        {
            try
            {
                var param = Convert.ToString(((TappedEventArgs)e).Parameter);
                var frame = (Frame)sender;
                var Allchild = frame.Children.AsEnumerable();
                var lbl = Allchild.ElementAt(0) as Label;
                frame.BorderColor = Color.FromHex(Global.GetThemeColor(Global.setThemeColor));
                lbl.TextColor = Color.FromHex(Global.GetThemeColor(Global.setThemeColor));

                MessagingCenter.Send<object, string>("SelectStateValue", "SelectStateValue", param);
                this.Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        void ListView_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var state = (string)e.SelectedItem;
            MessagingCenter.Send<object, string>("SelectStateValue", "SelectStateValue", state);
            this.Navigation.PopAsync();
        }
    }
}