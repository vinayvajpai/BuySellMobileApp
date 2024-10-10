using System;
using System.Collections.Generic;
using BuySell.ViewModel;
using Xamarin.Forms;

namespace BuySell.Views
{
    public partial class EditCardPage : ContentPage
    {
        EditCardViewModel vm;
        public EditCardPage()
        {
            InitializeComponent();
            BindingContext = vm = new EditCardViewModel(this.Navigation);
        }

        protected override void OnAppearing()
        {
            vm.IsTap = false;
            base.OnAppearing();
        }
    }
}
