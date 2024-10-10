using System;
using System.Collections.Generic;
using System.Windows.Input;
using BuySell.Helper;
using BuySell.Utility;
using BuySell.ViewModel;
using Xamarin.Forms;

namespace BuySell.CustomControl
{
    public partial class CustomCategorySubHeader : ContentView
    {
        #region Constructor
        public CustomCategorySubHeader()
        {
            InitializeComponent();
            imgHanger.Source = ImageSource.FromFile(Global.GetProductCatIcon(Global.setThemeColor));
            //TintImageEffect.SetTintColor(imgHanger, Color.White);
            imgHanger.Source = Constant.ClothingHanger;
        }
        #endregion

        #region Bindable Properties
        public int SelectedTapIndex
        {
            get { return (int)GetValue(SelectedTapIndexProperty); }
            set { SetValue(SelectedTapIndexProperty, value); }

        }

        private static BindableProperty SelectedTapIndexProperty = BindableProperty.Create(
                                                         propertyName: "SelectedTapIndex",
                                                         returnType: typeof(int),
                                                         declaringType: typeof(CustomCategorySubHeader),
                                                         defaultValue: -1,
                                                         defaultBindingMode: BindingMode.TwoWay,
                                                         propertyChanged: SelectedTapIndexPropertyChanged);
        public int SelectedTapIndexAddListing
        {
            get { return (int)GetValue(SelectedTapIndexPropertyAddListing); }
            set { SetValue(SelectedTapIndexPropertyAddListing, value); }

        }

        private static BindableProperty SelectedTapIndexPropertyAddListing = BindableProperty.Create(
                                                         propertyName: "SelectedTapIndexAddListing",
                                                         returnType: typeof(int),
                                                         declaringType: typeof(CustomCategorySubHeader),
                                                         defaultValue: 0,
                                                         defaultBindingMode: BindingMode.TwoWay,
                                                         propertyChanged: SelectedTapIndexPropertyChanged);

        public static BindableProperty SelectCommandProperty =
        BindableProperty.Create(nameof(SelectCommand), typeof(ICommand), typeof(CustomCategorySubHeader));
        public ICommand SelectCommand
        {
            get => (ICommand)GetValue(SelectCommandProperty);
            set => SetValue(SelectCommandProperty, value);
        }


        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(CustomCategorySubHeader), null);
        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }
        #endregion

        #region Methods
        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            var paranmeter = Convert.ToInt16(((TappedEventArgs)e).Parameter);
            SetSelectedTab(this, paranmeter);
        }
        
        private static void SelectedTapIndexPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomCategorySubHeader)bindable;
            SetSelectedTab(control, Convert.ToInt16(newValue.ToString()));
        }
        //Method created to set selected and unselected tab and image color as per store selected
        public static void SetSelectedTab(CustomCategorySubHeader control, int index)
        {
            try
            {
                control.imgHanger.Source = Constant.ClothingHanger;
                control.imgSneaker.Source = "Sneakers";
                control.imgStreet.Source = "Streetwear";
                control.imgTops.Source = "VintageTops";

                control.frmHanger.BackgroundColor = Color.White;
                control.frmSneaker.BackgroundColor = Color.White;
                control.frmStreet.BackgroundColor = Color.White;
                control.frmTops.BackgroundColor = Color.White;

                //TintImageEffect.SetTintColor(control.imgHanger,Color.FromHex(Global.GetThemeColor(ThemesColor.BlueColor)));
                //TintImageEffect.SetTintColor(control.imgSneaker, Color.FromHex(Global.GetThemeColor(ThemesColor.RedColor)));
                //TintImageEffect.SetTintColor(control.imgStreet, Color.FromHex(Global.GetThemeColor(ThemesColor.OrangeColor)));
                //TintImageEffect.SetTintColor(control.imgTops, Color.FromHex(Global.GetThemeColor(ThemesColor.GreenColor)));

                switch (index)
                {
                    case 0:
                        {

                            //control.imgHanger.Effects.RemoveAt(0);
                            //control.frmHanger.BackgroundColor = Color.FromHex(Global.GetThemeColor(ThemesColor.BlueColor));
                            //TintImageEffect.SetTintColor(control.imgHanger, Color.White);
                            Global.Storecategory = string.Empty;
                            //To set the store inside searchedResultSelected store global variable
                            Global.SearchedResultSelectedStore = null;
                            control.imgHanger.Source = Constant.ClothingHanger;
                            break;
                        }
                    case 1:
                        {

                            //control.imgHanger.Effects.RemoveAt(0);
                            control.frmHanger.BackgroundColor = Color.FromHex(Global.GetThemeColor(ThemesColor.BlueColor));
                            //TintImageEffect.SetTintColor(control.imgHanger, Color.White);
                            Global.Storecategory = Constant.ClothingStr;

                            //To set the store inside searchedResultSelected store global variable
                            Global.SearchedResultSelectedStore = Constant.ClothingStr;
                            control.imgHanger.Source = Constant.ClothingImageWhiteBackground;
                            break;
                        }
                    case 2:
                        {
                            //control.imgTops.Effects.RemoveAt(0);
                            //control.frmTops.BackgroundColor = Color.FromHex(Global.GetThemeColor(ThemesColor.GreenColor));
                            //TintImageEffect.SetTintColor(control.imgTops, Color.White);
                            //break;
                            //control.imgSneaker.Effects.RemoveAt(0);
                            control.frmSneaker.BackgroundColor = Color.FromHex(Global.GetThemeColor(ThemesColor.RedColor));
                            //TintImageEffect.SetTintColor(control.imgSneaker, Color.White);
                            Global.Storecategory = Constant.SneakersStr;

                            //To set the store inside searchedResultSelected store global variable
                            Global.SearchedResultSelectedStore = Constant.SneakersStr;
                            control.imgSneaker.Source = Constant.SneakersImageWhiteBackground;
                            break;
                        }
                    case 3:
                        {
                            //control.imgSneaker.Effects.RemoveAt(0);
                            //control.frmSneaker.BackgroundColor = Color.FromHex(Global.GetThemeColor(ThemesColor.RedColor));
                            //TintImageEffect.SetTintColor(control.imgSneaker, Color.White);
                            //break;
                            //control.imgStreet.Effects.RemoveAt(0);
                            control.frmStreet.BackgroundColor = Color.FromHex(Global.GetThemeColor(ThemesColor.OrangeColor));
                            //TintImageEffect.SetTintColor(control.imgStreet, Color.White);
                            Global.Storecategory = Constant.StreetwearStr;

                            //To set the store inside searchedResultSelected store global variable
                            Global.SearchedResultSelectedStore = Constant.StreetwearStr;
                            control.imgStreet.Source = Constant.StreetwearImageWhiteBackground;
                            break;
                        }
                    case 4:
                        {
                            //control.imgStreet.Effects.RemoveAt(0);
                            //control.frmStreet.BackgroundColor = Color.FromHex(Global.GetThemeColor(ThemesColor.OrangeColor));
                            //TintImageEffect.SetTintColor(control.imgStreet, Color.White);
                            //break;
                            //control.imgTops.Effects.RemoveAt(0);
                            control.frmTops.BackgroundColor = Color.FromHex(Global.GetThemeColor(ThemesColor.GreenColor));
                            //TintImageEffect.SetTintColor(control.imgTops, Color.White);
                            Global.Storecategory = Constant.VintageStr;

                            //To set the store inside searchedResultSelected store global variable
                            Global.SearchedResultSelectedStore = Constant.VintageStr;
                            control.imgTops.Source = Constant.VintageImageWhiteBackground;
                            break;
                        }

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        


    }
}
