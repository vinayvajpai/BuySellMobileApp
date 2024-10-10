using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using BuySell.Helper;
using BuySell.Popup;
using BuySell.Utility;
using BuySell.ViewModel;
using BuySell.Views;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace BuySell.CustomControl
{
    public partial class CustomHeaderControl : ContentView
    {
        public event EventHandler<int> SelectedCategoryEvent;
        CustomHeaderViewModel vm;
        bool IsLoad = false;
        public CustomHeaderControl()
        {
            IsLoad = true;
            InitializeComponent();
            BindingContext = vm = new CustomHeaderViewModel(this.Navigation);
            MessagingCenter.Subscribe<object>(Constant.UpdateHeaderThemeStr, Constant.UpdateHeaderThemeStr, (obj) =>
            {
                vm.ThemeColor = Global.GetThemeColor(Global.setThemeColor);
                lblToolItem.Text = vm.ProductCatName;
                imgToolLogo.Source = vm.ProductCatLogo;
                //TintImageEffect.SetTintColor(imgToolItem, Color.FromHex(Global.GetThemeColor(ThemesColor.BlueColor)));
                //if (imgToolItem.Effects.Count > 0)
                //{
                //    imgToolItem.Effects.RemoveAt(0);
                //}
                if (Global.setThemeColor == ThemesColor.GreenColor)
                {
                    imgToolItem.Source = ImageSource.FromFile(Global.GetProductCatIcon(Global.setThemeColor));
                }
                else
                {
                    imgToolItem.Source = ImageSource.FromFile(Global.GetProductCatIcon(Global.setThemeColor) + "W");
                }
                //TintImageEffect.SetTintColor(imgToolItem, Color.White);
            });
            MessagingCenter.Subscribe<object, ThemesColor>(Constant.UpdateHeaderThemeForDetailStr, Constant.UpdateHeaderThemeForDetailStr, (obj, theme) =>
            {
                vm.ThemeColor = Global.GetThemeColor(theme);
                lblToolItem.Text = Global.GetProductCatName(theme); ;
                imgToolLogo.Source = Global.GetProductCatLogo(theme);
                //TintImageEffect.SetTintColor(imgToolItem, Color.FromHex(Global.GetThemeColor(theme)));
                //if (imgToolItem.Effects.Count > 0)
                //{
                //    imgToolItem.Effects.RemoveAt(0);
                //}
                if (theme == ThemesColor.GreenColor)
                {
                    imgToolItem.Source = ImageSource.FromFile(Global.GetProductCatIcon(theme));
                }
                else
                {
                    imgToolItem.Source = ImageSource.FromFile(Global.GetProductCatIcon(theme) + "W");
                }
                //TintImageEffect.SetTintColor(imgToolItem, Color.White);
            });

            Device.StartTimer(TimeSpan.FromSeconds(3), () => {
                IsLoad = false;
                return false;
            });
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
        }

        private async void SearchTxt_Focused(object sender, FocusEventArgs e)
        {
            try
            {
                if (NavStack != null && NavStack.ModalStack != null)
                {
                    if (NavStack.ModalStack.Count > 0)
                    {
                        if (vm.IsTap)
                        {
                            return;
                        }
                    }
                }

                vm.IsTap = true;
                if (NavStack != null)
                {
                    await NavStack.PushModalAsync(new NavigationPage(new SearchPage()));
                    await Task.Run(async () =>
                    {
                        await Task.Delay(100);
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            searchTxt.Unfocus();
                        });
                    });
                }
            }
            catch (Exception ex)
            {
                vm.IsTap = false;
                Debug.WriteLine(ex.Message);
            }
        }

        public static BindableProperty BackCommandProperty =
            BindableProperty.Create(nameof(BackCommand), typeof(ICommand), typeof(CustomHeaderControl));
        public ICommand BackCommand
        {
            get => (ICommand)GetValue(BackCommandProperty);
            set => SetValue(BackCommandProperty, value);
        }

        public string TooItemText
        {
            get { return (string)GetValue(TooItemTextProperty); }
            set { SetValue(TooItemTextProperty, value); }

        }


        private static BindableProperty TooItemTextProperty = BindableProperty.Create(
                                                         propertyName: "TooItemText",
                                                         returnType: typeof(string),
                                                         declaringType: typeof(CustomHeaderControl),
                                                         defaultValue: "Cloths",
                                                         defaultBindingMode: BindingMode.TwoWay,
                                                         propertyChanged: TooItemTextPropertyChanged);


        private static void TooItemTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomHeaderControl)bindable;
        }

        public string TooItemIcon
        {
            get { return (string)GetValue(TooItemIconProperty); }
            set { SetValue(TooItemIconProperty, value); }

        }


        private static BindableProperty TooItemIconProperty = BindableProperty.Create(
                                                         propertyName: "TooItemIcon",
                                                         returnType: typeof(string),
                                                         declaringType: typeof(CustomHeaderControl),
                                                         defaultValue: "HeaderCloths",
                                                         defaultBindingMode: BindingMode.TwoWay,
                                                         propertyChanged: TooItemIconPropertyChanged);


        private static void TooItemIconPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            //var control = (CustomHeaderControl)bindable;
            //control.imgToolItem.Source = ImageSource.FromFile(newValue.ToString());
            //control.lblBuy.TextColor = Color.FromHex(control.vm.ThemeColor);
            //control.frmBg.BackgroundColor = Color.FromHex(control.vm.ThemeColor);
            //control.imgToolItem.Effects.RemoveAt(0);
            //TintImageEffect.SetTintColor(control.imgToolItem, Color.White);
        }

        public static BindableProperty NavStackProperty =
            BindableProperty.Create(nameof(NavStack), typeof(INavigation), typeof(CustomHeaderControl));
        public INavigation NavStack
        {
            get => (INavigation)GetValue(NavStackProperty);
            set => SetValue(NavStackProperty, value);
        }


        public static BindableProperty IsShowActionProperty =
           BindableProperty.Create(nameof(BackCommand), typeof(bool), typeof(CustomHeaderControl), defaultValue: true, propertyChanged: IsShowActionPropertyChanged);
        public bool IsShowAction
        {
            get => (bool)GetValue(IsShowActionProperty);
            set => SetValue(IsShowActionProperty, value);
        }

        private static void IsShowActionPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomHeaderControl)bindable;
            control.actionStk.IsVisible = Convert.ToBoolean(newValue);
        }

        public static BindableProperty IsShowSearchProperty =
           BindableProperty.Create(nameof(BackCommand), typeof(bool), typeof(CustomHeaderControl), defaultValue: true, propertyChanged: IsShowSearchPropertyChanged);
        public bool IsShowSearchIcon
        {
            get => (bool)GetValue(IsShowSearchProperty);
            set => SetValue(IsShowSearchProperty, value);
        }

        private static void IsShowSearchPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomHeaderControl)bindable;
            control.searchIcon.IsVisible = Convert.ToBoolean(newValue);
        }

        public static BindableProperty IsShowSearchFrmProperty =
           BindableProperty.Create(nameof(BackCommand), typeof(bool), typeof(CustomHeaderControl), defaultValue: true, propertyChanged: IsShowSearchFrmPropertyChanged);
        public bool IsShowSearchFrm
        {
            get => (bool)GetValue(IsShowSearchFrmProperty);
            set => SetValue(IsShowSearchFrmProperty, value);
        }

        private static void IsShowSearchFrmPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomHeaderControl)bindable;
            control.searchFrm.IsVisible = Convert.ToBoolean(newValue);
        }

        public static BindableProperty IsShowTooTipProperty =
          BindableProperty.Create(nameof(BackCommand), typeof(bool), typeof(CustomHeaderControl), defaultValue: false, propertyChanged: IsShowTooTipPropertyChanged);
        public bool IsShowTooTip
        {
            get => (bool)GetValue(IsShowTooTipProperty);
            set => SetValue(IsShowTooTipProperty, value);
        }

        private static void IsShowTooTipPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomHeaderControl)bindable;
            BuySell.Effects.TooltipEffect.SetIsShowTooltip(control.actionStk, (bool)newValue);
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
                if (vm.IsTap)
                {
                    await PopupNavigation.Instance.PopAsync();
                    vm.IsTap = false;
                    return;
                }
                else
                {
                    vm.IsTap = true;

                    var frame = (CustomFrame)sender;
                    var parent = frame.Parent as StackLayout;
                    var Allchild = parent.Children.AsEnumerable();
                    var SelectedPopUpText = Allchild.ElementAt(1) as Label;
                    if (!string.IsNullOrEmpty(SelectedPopUpText.Text))
                    {
                        var popuppage = new CustomTabPopup(SelectedPopUpText.Text);
                        popuppage.SelectedEvent += Popuppage_SelectedEvent;
                        await PopupNavigation.Instance.PushAsync(popuppage);
                    }
                }

            }
            catch (Exception ex)
            {
                vm.IsTap = false;
                Debug.WriteLine(ex.Message);
            }

        }

        private void Popuppage_SelectedEvent(object sender, int e)
        {
            if (SelectedCategoryEvent != null)
            {
                SelectedCategoryEvent.Invoke(sender, e);
            }
        }

        async void Search_Tapped(System.Object sender, System.EventArgs e)
        {
            //try
            //{
            //    if (IsLoad)
            //        return;

            //    if (NavStack != null && NavStack.ModalStack != null)
            //    {
            //        if (NavStack.ModalStack.Count > 0)
            //        {
            //            if (vm.IsTap)
            //            {
            //                return;
            //            }
            //        }
            //    }

            //    vm.IsTap = true;
            //    if (NavStack != null)
            //    {
            //        await NavStack.PushModalAsync(new NavigationPage(new SearchPage()));
            //        await Task.Run(async () =>
            //        {
            //            await Task.Delay(100);
            //            Device.BeginInvokeOnMainThread(async () =>
            //            {
            //                searchIcon.Focus();
            //            });
            //        });

            //    }
            //}
            //catch (Exception ex)
            //{
            //    vm.IsTap = false;
            //    Debug.WriteLine(ex.Message);
            //}
        }

        async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            try
            {
                if (IsLoad)
                    return;

                if (NavStack != null && NavStack.ModalStack != null)
                {
                    if (NavStack.ModalStack.Count > 0)
                    {
                        if (vm.IsTap)
                        {
                            return;
                        }
                    }
                }

                vm.IsTap = true;
                if (NavStack != null)
                {
                    await NavStack.PushModalAsync(new NavigationPage(new SearchPage()));
                    await Task.Run(async () =>
                    {
                        await Task.Delay(100);
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            searchIcon.Focus();
                        });
                    });

                }
            }
            catch (Exception ex)
            {
                vm.IsTap = false;
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
