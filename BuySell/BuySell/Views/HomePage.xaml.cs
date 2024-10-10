using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuySell.Helper;
using BuySell.ViewModel;
using BuySell.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BuySell.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        HomeViewModel vm;
        public HomePage()
        {
            InitializeComponent();
            BindingContext = vm=new HomeViewModel(this.Navigation);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            vm.IsTap = false;
            MessagingCenter.Unsubscribe<object>(Constant.UpdateThemeStr, Constant.UpdateThemeStr);
            MessagingCenter.Unsubscribe<object>(Constant.UpdateHeaderThemeStr, Constant.UpdateHeaderThemeStr);
            MessagingCenter.Unsubscribe<object, ThemesColor>(Constant.UpdateThemeForDetailStr, Constant.UpdateThemeForDetailStr);
            MessagingCenter.Unsubscribe<object,ThemesColor>(Constant.UpdateHeaderThemeForDetailStr, Constant.UpdateHeaderThemeForDetailStr);
            Global.ResetMessagingCenter(this);
        }

        #region Selected Category Tapped Method
        private void SelecteCategory_Tapped(object sender, EventArgs e)
        {
            try
            {
                if (vm.IsTap)
                    return;
                vm.IsTap = true;

                var parameter = Convert.ToInt32(Convert.ToString(((TappedEventArgs)e).Parameter));
                if (parameter == 1)
                {
                    Global.setThemeColor = ThemesColor.BlueColor;
                    this.Navigation.PushAsync(new DashBoardView(true));
                    return;
                }
                else if (parameter == 2)
                {
                    Global.setThemeColor = ThemesColor.GreenColor;
                    this.Navigation.PushAsync(new DashBoardView(true));
                    return;
                }
                else if (parameter == 3)
                {
                    Global.setThemeColor = ThemesColor.RedColor;
                    this.Navigation.PushAsync(new DashBoardView(true));
                    return;
                }
                else if (parameter == 4)
                {
                    Global.setThemeColor = ThemesColor.OrangeColor;
                    this.Navigation.PushAsync(new DashBoardView(true));
                    return;
                }

            }
            catch (Exception ex)
            {
                vm.IsTap = false;
                Debug.WriteLine(ex.Message);
            }
        }       
        #endregion


        void Skip_Tapped(System.Object sender, System.EventArgs e)
        {
            try
            {
                if (vm.IsTap)
                    return;
                vm.IsTap = true;

                Global.setThemeColor = ThemesColor.BlueColor;
                this.Navigation.PushAsync(new DashBoardView(true));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                vm.IsTap = true;
            }
            
        }

        void Login_Tapped(System.Object sender, System.EventArgs e)
        {
            Global.InsertLogin(this.Navigation);
            //this.Navigation.PushAsync(new LoginPage());
            //App.Current.MainPage = new NavigationPage(new LoginPage());
        }


    }
}