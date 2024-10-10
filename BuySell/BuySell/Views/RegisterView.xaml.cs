using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuySell.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BuySell.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegisterView : ContentPage
	{
		RegisterViewModel vm;
        public RegisterView ()
		{
			InitializeComponent ();
			BindingContext = vm = new RegisterViewModel(this.Navigation);
		}
	}
}