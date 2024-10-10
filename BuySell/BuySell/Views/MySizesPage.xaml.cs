using System;
using System.Collections.Generic;
using BuySell.ViewModel;
using Xamarin.Forms;

namespace BuySell.Views
{
    public partial class MySizesPage : ContentPage
    {
        MySizesViewModel vm;
        int currentFilter = 1;
        int SelectedCommandperameter = 0;
        public MySizesPage()
        {
            InitializeComponent();
            BindingContext = vm = new MySizesViewModel(this.Navigation);
        }

        void SelectGenderFilter_Tapped(System.Object sender, System.EventArgs e)
        {
            var par = Convert.ToInt16(((TappedEventArgs)e).Parameter);
            sep1.BackgroundColor = Color.White;
            sep2.BackgroundColor = Color.White;
            currentFilter = par;
            SetFilterSelector();
        }

        void SetFilterSelector()
        {
            try
            {
                if (currentFilter == 1)
                {
                    sep1.BackgroundColor = Color.FromHex(vm.ThemeColor);
                }
                else if (currentFilter == 2)
                {
                    sep2.BackgroundColor = Color.FromHex(vm.ThemeColor);
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected override void OnAppearing()
        {
            vm.IsTap = false;
            base.OnAppearing();
        }

        void Save_Clicked(System.Object sender, System.EventArgs e)
        {
        }
        private async void SelectPropertiesValues_Tapped(object sender, EventArgs e)
        {
            var parameter = Convert.ToInt32(Convert.ToString(((TappedEventArgs)e).Parameter));
            if (parameter == 1)
            {
                SelectedCommandperameter = parameter;
                await vm.GetMySizesList(parameter);
                return;
            }
            else if (parameter == 2)
            {
                SelectedCommandperameter = parameter;
                await vm.GetMySizesList(parameter);
                return;
            }
            else if (parameter == 3)
            {
                SelectedCommandperameter = parameter;
                await vm.GetMySizesList(parameter);
                return;
            }
            else if (parameter == 4)
            {
                SelectedCommandperameter = parameter;
                await vm.GetMySizesList(parameter);
                return;
            }
            else if (parameter == 5)
            {
                SelectedCommandperameter = parameter;
                await vm.GetMySizesList(parameter);
                return;
            }
        }
    }
}
