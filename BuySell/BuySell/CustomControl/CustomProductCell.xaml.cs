using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace BuySell.CustomControl
{	
	public partial class CustomProductCell : ContentView
	{
        public event EventHandler CallbackLikeDislike;
        public CustomProductCell ()
		{
			InitializeComponent ();
		}

        void imgLikeUnlike_Clicked(System.Object sender, System.EventArgs e)
        {
            //var objProduct = (Model.DashBoardModel)((TappedEventArgs)e).Parameter;
            if (CallbackLikeDislike != null)
            {
                CallbackLikeDislike.Invoke(sender, e);
            }
        }

        public static readonly BindableProperty CommandProperty =
        BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(CustomProductCell), null);

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public ICommand OnTapCommand
        {
            get
            {
                return new Command(() =>
                {
                    if (Command == null)
                        return;
                    if (Command.CanExecute(CommandParameter))
                        Command.Execute(CommandParameter);

                });
            }
        }

        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public static readonly BindableProperty CommandParameterProperty =
           BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(CustomProductCell), null);

        void SelectProduct_Tapped(System.Object sender, System.EventArgs e)
        {
            OnTapCommand.Execute(null);
        }

    }
}

