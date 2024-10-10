using BuySell.Helper;
using BuySell.Model;
using BuySell.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BuySell.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreditCardListView : ContentPage
    {
        CreditCardListViewModel vm;
        public CreditCardListView()
        {
            InitializeComponent();
            BindingContext = titleControl.BindingContext = vm = new CreditCardListViewModel(this.Navigation);
            vm.PropertyChanged += Vm_PropertyChanged;
        }
        public CreditCardListView(Action<CardListModel> action)
        {
            InitializeComponent();
            BindingContext = titleControl.BindingContext = vm = new CreditCardListViewModel(this.Navigation, action);
            vm.PropertyChanged += Vm_PropertyChanged;
        }
        private void Vm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (vm.CreditCardAddList.Count > 0)
            {
                //lblNoData.IsVisible = false;
                listCard.IsVisible = true;
            }
            else
            {
                ///lblNoData.IsVisible = true;
                listCard.IsVisible = false;
            }
        }
        protected override async void OnAppearing()
        {
            try
            {
                if(vm != null)
                {
                  await  vm.GetallcardList();
                    vm.IsTap = false;
                }
               
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            base.OnAppearing();
        }
    }
}