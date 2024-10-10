using System;
using System.Collections.Generic;
using BuySell.ViewModel;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BuySell.Popup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImageDeleteEditPopup : PopupPage
    {
        public event EventHandler eventDelete;
        public event EventHandler eventEdit;
        public ImageDeleteEditPopup()
        {
            InitializeComponent();
            BindingContext = new ImageDelEditViewModel(this.Navigation);
        }
        private void DeleteCommand(object sender, EventArgs e)
        {
            if (eventDelete != null)
            {
                eventDelete.Invoke(sender, e);
            }
        }

        private void EditCommand(object sender, EventArgs e)
        {
            if (eventEdit != null)
            {
                eventEdit.Invoke(sender, e);
            }
        }

        async void PopupCancelCommand(System.Object sender, System.EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }
    }
}

