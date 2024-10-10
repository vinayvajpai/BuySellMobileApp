using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace BuySell.Popup
{	
	public partial class CustomToolTipPopup : PopupPage
	{	
		public CustomToolTipPopup ()
		{
			InitializeComponent ();
			this.BindingContext = new ViewModel.BaseViewModel();
            this.BackgroundColor = new Color(0, 0, 0, 0.8);
        }

        void ClosePopup_Tapped(System.Object sender, System.EventArgs e)
        {
            PopupNavigation.Instance.PopAsync();
        }
    }
}

