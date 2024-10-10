using System;
using System.Collections.Generic;
using System.Windows.Input;
using BuySell.Helper;
using BuySell.ViewModel;
using Xamarin.Forms;

namespace BuySell.CustomControl
{
    public partial class CustomTitleControl : ContentView
    {
        CustomTitleViewModel vm;
        #region Constructor
        public CustomTitleControl()
        {
            InitializeComponent();
            BindingContext = vm = new CustomTitleViewModel(this.Navigation);
            vm.navigation = this.Navigation;
            MessagingCenter.Subscribe<object>(Constant.UpdateHeaderThemeStr, Constant.UpdateHeaderThemeStr, (obj) =>
            {
                vm.ThemeColor = Global.GetThemeColor(Global.setThemeColor);
            });
        }
        #endregion

        #region Bindable Properties
        public static BindableProperty BackCommandProperty =
            BindableProperty.Create(nameof(BackCommand), typeof(ICommand), typeof(CustomTitleControl));
        public ICommand BackCommand
        {
            get => (ICommand)GetValue(BackCommandProperty);
            set => SetValue(BackCommandProperty, value);
        }
        public static BindableProperty IsShowBackProperty =
           BindableProperty.Create(nameof(BackCommand), typeof(bool), typeof(CustomTitleControl), defaultValue: true, propertyChanged: IsShowBackPropertyChanged);
        public bool IsShowBackIcon
        {
            get => (bool)GetValue(IsShowBackProperty);
            set => SetValue(IsShowBackProperty, value);
        }
        public string TitleText
        {
            get { return (string)GetValue(TitleTextProperty); }
            set { SetValue(TitleTextProperty, value); }

        }


        private static BindableProperty TitleTextProperty = BindableProperty.Create(
                                                         propertyName: "TitleText",
                                                         returnType: typeof(string),
                                                         declaringType: typeof(CustomTitleControl),
                                                         defaultValue: "",
                                                         defaultBindingMode: BindingMode.TwoWay,
                                                         propertyChanged: TitleTextPropertyChanged);

        #endregion

        #region Methods
        private static void IsShowBackPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomTitleControl)bindable;
            control.imgBack.IsVisible = Convert.ToBoolean(newValue);
        }


        private static void TitleTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomTitleControl)bindable;
            control.lblTitle.Text = newValue.ToString();
            control.lblTitle.TextColor = Color.Black;
        }
        #endregion
    }
}
