using System;
using System.Collections.Generic;
using BuySell.ViewModel;
using Xamarin.Forms;

namespace BuySell.Views
{
    public partial class NotificationsPage : ContentPage
    {
        NotificationsViewModel vm;
        public NotificationsPage()
        {
            InitializeComponent();
            BindingContext = vm = new NotificationsViewModel(this.Navigation);
        }

        protected override void OnAppearing()
        {
            vm.IsTap = false;
            base.OnAppearing();
        }

    }
}
