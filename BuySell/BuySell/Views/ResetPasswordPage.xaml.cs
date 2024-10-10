using BuySell.Helper;
using BuySell.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BuySell.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResetPasswordPage : ContentPage
    {
        ResetPasswordViewModel vm;
        #region Constructor
        public ResetPasswordPage()
        {
            InitializeComponent();
            BindingContext = vm = new ResetPasswordViewModel(this.Navigation);
        }
        #endregion
    }
}