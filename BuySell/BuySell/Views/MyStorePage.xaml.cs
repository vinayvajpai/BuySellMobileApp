using System;
using System.Collections.Generic;
using BuySell.ViewModel;
using Xamarin.Forms;

namespace BuySell.Views
{
    public partial class MyStorePage : ContentPage
    {
        MyStoreViewModel vm;
        public MyStorePage()
        {
            InitializeComponent();
            BindingContext = vm = new MyStoreViewModel(this.Navigation);
        }

        protected override void OnAppearing()
        {
            vm.IsTap = false;
            base.OnAppearing();
        }
    }
}
