using System;
using System.Collections.Generic;
using BuySell.Helper;
using BuySell.Utility;
using BuySell.View;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace BuySell.CustomControl
{
    public partial class CustomTabPopup : PopupPage
    {
        public event EventHandler<int> SelectedEvent;
        public CustomTabPopup(string SelectedPopUpText)
        {
            InitializeComponent();
            SetSelectedtab(SelectedPopUpText);
        }

        private void SetSelectedtab(string SelectedPopUpText)
        {
            try
            {
                try
                {
                    if (SelectedPopUpText == Constant.ClothingStr)
                    {
                        Global.setThemeColor = ThemesColor.BlueColor;
                        imgClothingInActive.IsVisible = false;
                        imgClothingActive.IsVisible = true;
                        imgClothingActive.BackgroundColor = Color.FromHex("#1567A6");

                        imgVintageInActive.IsVisible = true;
                        imgVintageActive.IsVisible = false;

                        imgSneakersInActive.IsVisible = true;
                        imgSneakersActive.IsVisible = false;

                        imgStreetwearInActive.IsVisible = true;
                        imgStreetwearActive.IsVisible = false;
                        Global.StoreIndex = 1;
                    }
                    else if (SelectedPopUpText == Constant.VintageStr)
                    {
                        Global.setThemeColor = ThemesColor.GreenColor;
                        imgClothingInActive.IsVisible = true;
                        imgClothingActive.IsVisible = false;

                        imgVintageInActive.IsVisible = false;
                        imgVintageActive.IsVisible = true;
                        imgVintageActive.BackgroundColor = Color.FromHex("#467904");

                        imgSneakersInActive.IsVisible = true;
                        imgSneakersActive.IsVisible = false;

                        imgStreetwearInActive.IsVisible = true;
                        imgStreetwearActive.IsVisible = false;
                        Global.StoreIndex = 4;

                    }
                    else if (SelectedPopUpText == Constant.SneakersStr)
                    {
                        Global.setThemeColor = ThemesColor.RedColor;
                        imgClothingInActive.IsVisible = true;
                        imgClothingActive.IsVisible = false;

                        imgVintageInActive.IsVisible = true;
                        imgVintageActive.IsVisible = false;

                        imgSneakersInActive.IsVisible = false;
                        imgSneakersActive.IsVisible = true;
                        imgSneakersActive.BackgroundColor = Color.FromHex("#C52036");


                        imgStreetwearInActive.IsVisible = true;
                        imgStreetwearActive.IsVisible = false;
                        Global.StoreIndex = 2;

                    }
                    else if (SelectedPopUpText.ToLower() == Constant.StreetwearStr.ToLower())
                    {
                        Global.setThemeColor = ThemesColor.OrangeColor;
                        imgClothingInActive.IsVisible = true;
                        imgClothingActive.IsVisible = false;

                        imgVintageInActive.IsVisible = true;
                        imgVintageActive.IsVisible = false;

                        imgSneakersInActive.IsVisible = true;
                        imgSneakersActive.IsVisible = false;

                        imgStreetwearInActive.IsVisible = false;
                        imgStreetwearActive.IsVisible = true;
                        imgStreetwearActive.BackgroundColor = Color.FromHex("#D04107");
                        Global.StoreIndex = 3;
                    }
                }
                catch (Exception ex)
                {

                }
            }
            catch (Exception ex)
            {

            }
        }
        //private void SetSelectedtab(string SelectedPopUpText)
        //{
        //    TintImageEffect.SetTintColor(imgClothingInActive, Color.FromHex(Global.GetThemeColor(ThemesColor.BlueColor)));
        //    TintImageEffect.SetTintColor(imgSneakersInActive, Color.FromHex(Global.GetThemeColor(ThemesColor.RedColor)));
        //    TintImageEffect.SetTintColor(imgStreetwearInActive, Color.FromHex(Global.GetThemeColor(ThemesColor.OrangeColor)));
        //    TintImageEffect.SetTintColor(imgVintageInActive, Color.FromHex(Global.GetThemeColor(ThemesColor.GreenColor)));

        //    TintImageEffect.SetTintColor(imgClothInActive, Color.White);
        //    TintImageEffect.SetTintColor(imgSneakInActive, Color.White);
        //    TintImageEffect.SetTintColor(imgStreerInActive, Color.White);
        //    TintImageEffect.SetTintColor(imgVinActive, Color.White);

        //    if (SelectedPopUpText == "Clothing")
        //    {
        //        Global.setThemeColor = ThemesColor.BlueColor;
        //        imgClothingInActive.IsVisible = false;
        //        imgClothingActive.IsVisible = true;
        //        imgClothingActive.BackgroundColor = Color.FromHex("#1567A6");

        //        imgVintageInActive.IsVisible = true;
        //        imgVintageActive.IsVisible = false;

        //        imgSneakersInActive.IsVisible = true;
        //        imgSneakersActive.IsVisible = false;

        //        imgStreetwearInActive.IsVisible = true;
        //        imgStreetwearActive.IsVisible = false;
        //    }
        //    else if (SelectedPopUpText == "Vintage")
        //    {
        //        Global.setThemeColor = ThemesColor.GreenColor;
        //        imgClothingInActive.IsVisible = true;
        //        imgClothingActive.IsVisible = false;

        //        imgVintageInActive.IsVisible = false;
        //        imgVintageActive.IsVisible = true;
        //        imgVintageActive.BackgroundColor = Color.FromHex("#467904");

        //        imgSneakersInActive.IsVisible = true;
        //        imgSneakersActive.IsVisible = false;

        //        imgStreetwearInActive.IsVisible = true;
        //        imgStreetwearActive.IsVisible = false;

        //    }
        //    else if (SelectedPopUpText == "Sneakers")
        //    {
        //        Global.setThemeColor = ThemesColor.RedColor;
        //        imgClothingInActive.IsVisible = true;
        //        imgClothingActive.IsVisible = false;

        //        imgVintageInActive.IsVisible = true;
        //        imgVintageActive.IsVisible = false;

        //        imgSneakersInActive.IsVisible = false;
        //        imgSneakersActive.IsVisible = true;
        //        imgSneakersActive.BackgroundColor = Color.FromHex("#C52036");


        //        imgStreetwearInActive.IsVisible = true;
        //        imgStreetwearActive.IsVisible = false;

        //    }
        //    else if (SelectedPopUpText == "Streetwear")
        //    {
        //        Global.setThemeColor = ThemesColor.OrangeColor;
        //        imgClothingInActive.IsVisible = true;
        //        imgClothingActive.IsVisible = false;

        //        imgVintageInActive.IsVisible = true;
        //        imgVintageActive.IsVisible = false;

        //        imgSneakersInActive.IsVisible = true;
        //        imgSneakersActive.IsVisible = false;

        //        imgStreetwearInActive.IsVisible = false;
        //        imgStreetwearActive.IsVisible = true;
        //        imgStreetwearActive.BackgroundColor = Color.FromHex("#D04107");
        //    }
        //}

        private async void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }

        async void SelectedCat_tap(System.Object sender, System.EventArgs e)
        {
            var param = Convert.ToString(((TappedEventArgs)e).Parameter);
            SetSelectedtab(param);
            if (SelectedEvent != null)
            {
                SelectedEvent.Invoke(sender, Convert.ToInt16(Global.setThemeColor));
                await PopupNavigation.Instance.PopAsync();
                App.Current.MainPage = new NavigationPage(new DashBoardView(true));
            }
            //await PopupNavigation.Instance.PopAsync();
            //App.Current.MainPage = new NavigationPage(new DashBoardView());
        }
    }
}
