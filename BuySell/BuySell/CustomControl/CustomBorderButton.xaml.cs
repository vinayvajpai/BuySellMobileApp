using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BuySell.CustomControl
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomBorderButton : ContentView
    {
        public CustomBorderButton()
        {
            InitializeComponent();
            Content.BindingContext = this;
        }
        public string TitleText
        {
            get { return (string)GetValue(TitleTextProperty); }
            set { SetValue(TitleTextProperty, value); }

        }


        public static BindableProperty TitleTextProperty = BindableProperty.Create(
                                                         propertyName: "TitleText",
                                                         returnType: typeof(string),
                                                         declaringType: typeof(CustomBorderButton),
                                                         defaultValue: "Text",
                                                         defaultBindingMode: BindingMode.TwoWay,
                                                         propertyChanged: titleTextPropertyChanged);


        private static void titleTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomBorderButton)bindable;
            control.btn.Text = newValue.ToString();
        }

        public Color BorderColor
        {
            get { return (Color)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }

        }

        public static BindableProperty BorderColorProperty = BindableProperty.Create(
                                                         propertyName: "BorderColor",
                                                         returnType: typeof(Color),
                                                         declaringType: typeof(CustomBorderButton),
                                                         defaultValue: Color.LightGray,
                                                         defaultBindingMode: BindingMode.TwoWay,
                                                         propertyChanged: BordernColorPropertyChanged);


        private static void BordernColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomBorderButton)bindable;
            control.frm.BackgroundColor = (Color)newValue;
        }

        public Color TextColor
        {
            get { return (Color)GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }

        }

        public static BindableProperty TextColorProperty = BindableProperty.Create(
                                                         propertyName: "TextColor",
                                                         returnType: typeof(Color),
                                                         declaringType: typeof(CustomBorderButton),
                                                         defaultValue: Color.LightGray,
                                                         defaultBindingMode: BindingMode.TwoWay,
                                                         propertyChanged: TextColorPropertyChanged);


        private static void TextColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomBorderButton)bindable;
            control.btn.TextColor = (Color)newValue;
        }

        public int fontsize
        {
            get { return (int)GetValue(fontsizeProperty); }
            set { SetValue(fontsizeProperty, value); }

        }

        public static BindableProperty fontsizeProperty = BindableProperty.Create(
                                                         propertyName: "TextColor",
                                                         returnType: typeof(int),
                                                         declaringType: typeof(CustomBorderButton),
                                                         defaultValue: 15,
                                                         defaultBindingMode: BindingMode.TwoWay,
                                                         propertyChanged: fontsizePropertyChanged);


        private static void fontsizePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomBorderButton)bindable;
            control.btn.FontSize = (int)newValue;
        }

        public int height
        {
            get { return (int)GetValue(heightProperty); }
            set { SetValue(heightProperty, value); }

        }

        private static BindableProperty heightProperty = BindableProperty.Create(
                                                         propertyName: "height",
                                                         returnType: typeof(int),
                                                         declaringType: typeof(CustomBorderButton),
                                                         defaultValue: 50,
                                                         defaultBindingMode: BindingMode.TwoWay,
                                                         propertyChanged: heightPropertyChanged);


        private static BindableProperty CommandParameterProperty = BindableProperty.Create(
                                                         propertyName: "CommandParameter",
                                                         returnType: typeof(object),
                                                         declaringType: typeof(CustomBorderButton),
                                                         defaultValue: null,
                                                         defaultBindingMode: BindingMode.TwoWay, propertyChanged: CommandParameterUpdated);

        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }


        private static void heightPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomBorderButton)bindable;
            control.frm.HeightRequest = (int)newValue;
        }


        public Color BgColor
        {
            get { return (Color)GetValue(BgColorProperty); }
            set { SetValue(BgColorProperty, value); }

        }

        public static BindableProperty BgColorProperty = BindableProperty.Create(
                                                         propertyName: "BgColor",
                                                         returnType: typeof(Color),
                                                         declaringType: typeof(CustomBorderButton),
                                                         defaultValue: Color.White,
                                                         defaultBindingMode: BindingMode.TwoWay,
                                                         propertyChanged: BgColorPropertyChanged);


        private static void BgColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomBorderButton)bindable;
            control.frmInside.BackgroundColor = (Color)newValue;
        }

        public int CornerRadius
        {
            get { return (int)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }

        }

        public static BindableProperty CornerRadiusProperty = BindableProperty.Create(
                                                         propertyName: "CornerRadius",
                                                         returnType: typeof(int),
                                                         declaringType: typeof(CustomBorderButton),
                                                         defaultValue: 5,
                                                         defaultBindingMode: BindingMode.TwoWay,
                                                         propertyChanged: CornerRadiusPropertyChanged);


        private static void CornerRadiusPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomBorderButton)bindable;
            control.frm.CornerRadius = (int)newValue;
            control.frmInside.CornerRadius = (int)newValue;
        }



        public static BindableProperty ClickCommandProperty =
            BindableProperty.Create(nameof(ClickCommand), typeof(ICommand), typeof(CustomHeaderControl));
        public ICommand ClickCommand
        {
            get => (ICommand)GetValue(ClickCommandProperty);
            set => SetValue(ClickCommandProperty, value);
        }

        private static void CommandParameterUpdated(object sender, object oldValue, object newValue)
        {
            if (sender is CustomBorderButton btnAction && newValue != null)
            {
                btnAction.btn.CommandParameter = newValue;
            }
        }

        public ICommand ClickParamCommand
        {
            get
            {
                return new Command(() =>
                {
                        if (ClickCommand == null)
                            return;
                        if(ClickCommand.CanExecute(CommandParameter))
                        ClickCommand.Execute(CommandParameter);
                    
                });
            }
        }
    }
}