using System;
using System.Collections.Generic;
using BuySell.Helper;
using BuySell.ViewModel;
using Xamarin.Forms;

namespace BuySell.CustomControl
{
    public partial class CustomBottomNavigation : ContentView
    {
        private static CustomBottomNavigationViewModel m_viewModel;
        //Custom bottom navigation constructor
        public CustomBottomNavigation()
        {
            InitializeComponent();
            m_viewModel = BindingContext as CustomBottomNavigationViewModel;
            MessagingCenter.Subscribe<object>("Updatetheme", "Updatetheme", (obj) => {
                m_viewModel.ThemeColor = Global.GetThemeColor(Global.setThemeColor);
                frm.BackgroundColor = Color.FromHex(m_viewModel.ThemeColor);
            });

            MessagingCenter.Subscribe<object,ThemesColor>("UpdatethemeForDetail", "UpdatethemeForDetail", (obj,theme) => {
                m_viewModel.ThemeColor = Global.GetThemeColor(theme);
                frm.BackgroundColor = Color.FromHex(m_viewModel.ThemeColor);
            });
        }

        //Custom bottom navigation Bindable properties
        public static BindableProperty SelectedTabIndexProperty =
            BindableProperty.CreateAttached("SelectedTabIndex",
                                            typeof(int),
                                            typeof(CustomBottomNavigation),
                                            -1,
                                            propertyChanged: OnTintColorPropertyPropertyChanged);

        public static int GetSelectedTabIndex(BindableObject element)
        {
            return (int)element.GetValue(SelectedTabIndexProperty);
        }

        public static void SetSelectedTabIndex(BindableObject element, int value)
        {
            element.SetValue(SelectedTabIndexProperty, value);
        }


        static void OnTintColorPropertyPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            m_viewModel.SetTabsHighlight((int)newValue);
        }
    }
}
