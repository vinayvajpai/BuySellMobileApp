using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using BuySell.ViewModel;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;

namespace BuySell.Views
{
    public partial class SearchPage : ContentPage
    {
        SearchViewModel ViewModel;
        public SearchPage()
        {
            InitializeComponent();
            BindingContext = ViewModel = new SearchViewModel(this.Navigation);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.IsTap = false;
            await Task.Run(async () =>
            {
                await Task.Delay(100);
                Device.BeginInvokeOnMainThread(async () =>
                {
                    searchtext.Focus();
                });
            });
            //searchtext.Focus();
        }

        protected override void OnDisappearing()
        {
            ViewModel.IsTap = false;
            base.OnDisappearing();
        }

        public string GlobalNavigation { get; private set; }

        async void Back_Tapped(System.Object sender, System.EventArgs e)
        {
            try
            {
                if (Navigation.ModalStack.Count > 0)
                {
                    await Navigation.PopModalAsync(true);
                }
                else
                {
                    await Navigation.PopAsync(true);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

       async void SerachResult_Tapped(System.Object sender, System.EventArgs e)
        {
            try
            {
                if (ViewModel.IsTap)
                    return;
                ViewModel.IsTap = true;

                if (string.IsNullOrWhiteSpace(searchtext.Text))
                {
                    ViewModel.IsTap = false;
                    Acr.UserDialogs.UserDialogs.Instance.Alert("Please enter search text.");
                    return;
                }

                await Navigation.PushAsync(new SearchResultPage(searchtext.Text));
            }
            catch (Exception ex)
            {
                ViewModel.IsTap = false;
                Debug.WriteLine(ex.Message);
            }
           
        }
    }
}
