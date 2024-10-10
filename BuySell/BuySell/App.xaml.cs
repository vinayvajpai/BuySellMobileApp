using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using BuySell.Helper;
using BuySell.Model.LoginResponse;
using BuySell.Services;
using BuySell.View;
using BuySell.Views;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Stripe;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Application = Xamarin.Forms.Application;
using Device = Xamarin.Forms.Device;

[assembly: ExportFont("arial-black.ttf", Alias = "ArialBlackFont")]
[assembly: ExportFont("Blackout-Midnight.ttf", Alias = "BlackoutMidnightFont")]
[assembly: ExportFont("OpenSans-BoldItalic.ttf", Alias = "OpenSansBoldItalicFont")]
namespace BuySell
{
    public partial class App : Application
    {
        public const string NotificationHubName = "BuySellNotificationHub";
        public App()
        {
            StripeConfiguration.ApiKey = Constant.stripSecertAPIKey;
            Constant.IsConnected = Connectivity.NetworkAccess == NetworkAccess.Internet;
            Connectivity.ConnectivityChanged -= Connectivity_ConnectivityChanged;
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
            Global.database = Global.GetConnection();
            RegiserServices();
            InitializeComponent();
            if (Application.Current.Properties.ContainsKey("LoginUserData"))
            {
                Constant.LoginUserData = JsonConvert.DeserializeObject<Data>(Convert.ToString(Application.Current.Properties["LoginUserData"]));
                Global.Username = Convert.ToString(Application.Current.Properties["LoginUserName"]);
                Global.Password = Convert.ToString(Application.Current.Properties["LoginUserPass"]);
                Global.globalNav = new NavigationPage(new DashBoardView(false));
                //Global.globalNav = new NavigationPage(new MyEarningView());
                MainPage = Global.globalNav;
            }
            else
            {
                Global.SetFirstTimeLoad();
                //Global.globalNav = new NavigationPage(new HomePage());
                Global.globalNav = new NavigationPage(new LoginPage());
                MainPage = Global.globalNav;
            }
            try
            {
                AppCenter.Start("ios=a84e5c0b-d08d-453d-ba8f-c943a37cd317;android=4e738768-f46c-4c17-9fbd-4a00d82a04ed", typeof(Analytics), typeof(Crashes));
            }
            catch (Exception ex)
            {}
            
        }
        protected override void OnStart()
        {
            //SetUpPushNotification();
            //Constant.IsConnected = CrossConnectivity.Current.IsConnected;
            //CrossConnectivity.Current.ConnectivityChanged += (sender, args) =>
            //{
            //    Constant.IsConnected = args.IsConnected;
            //    if(!Constant.IsConnected)
            //    UserDialogs.Instance.Toast("Internet not available");
            //};
            //Constant.IsConnected = Connectivity.NetworkAccess == NetworkAccess.Internet;
            ///Connectivity.ConnectivityChanged -= Connectivity_ConnectivityChanged;
           // Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }
        protected override void OnSleep()
        {
        }
        protected override void OnResume()
        {
            //Constant.IsConnected = CrossConnectivity.Current.IsConnected;
            //CrossConnectivity.Current.ConnectivityChanged += (sender, args) =>
            //{
            //    Constant.IsConnected = args.IsConnected;
            //    if (!Constant.IsConnected)
            //        UserDialogs.Instance.Toast("Internet not available");
            //};
            //Constant.IsConnected = Connectivity.NetworkAccess == NetworkAccess.Internet;
            //Connectivity.ConnectivityChanged -= Connectivity_ConnectivityChanged;
            //Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }
        private void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            Constant.IsConnected = e.NetworkAccess == NetworkAccess.Internet;
        }

        void RegiserServices()
        {
            DependencyService.Register<IMessageService, MessageService>();
            DependencyService.Register<IStripePaymentService, StripePaymentService>();
            Task.Run(() => {
                Xamarin.Forms.Device.StartTimer(TimeSpan.FromSeconds(2), () => {
                    Constant.IsConnected = Connectivity.NetworkAccess == NetworkAccess.Internet;
                    return true;
                });
            });
        }

        void SetUpPushNotification()
        {
            // Handle when your app starts
            //CrossFirebasePushNotification.Current.RegisterForPushNotifications();
            //CrossFirebasePushNotification.Current.Subscribe("general");
            //CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>
            //{
            //    System.Diagnostics.Debug.WriteLine($"TOKEN REC: {p.Token}");
            //};
            //System.Diagnostics.Debug.WriteLine($"TOKEN: {CrossFirebasePushNotification.Current.Token}");

            //CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            //{
            //    try
            //    {
            //        System.Diagnostics.Debug.WriteLine("Received");
            //        if (p.Data.ContainsKey("body"))
            //        {
            //            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
            //            {
            //                //mPage.Message = $"{p.Data["body"]}";
            //            });

            //        }
            //    }
            //    catch (Exception ex)
            //    {

            //    }

            //};

            //CrossFirebasePushNotification.Current.OnNotificationOpened += (s, p) =>
            //{
            //    //System.Diagnostics.Debug.WriteLine(p.Identifier);

            //    System.Diagnostics.Debug.WriteLine("Opened");
            //    foreach (var data in p.Data)
            //    {
            //        System.Diagnostics.Debug.WriteLine($"{data.Key} : {data.Value}");
            //    }

            //    if (!string.IsNullOrEmpty(p.Identifier))
            //    {
            //        //Device.BeginInvokeOnMainThread(() =>
            //        //{
            //        //    mPage.Message = p.Identifier;
            //        //});
            //    }
            //    else if (p.Data.ContainsKey("color"))
            //    {
            //        Device.BeginInvokeOnMainThread(() =>
            //        {
            //            //mPage.Navigation.PushAsync(new ContentPage()
            //            //{
            //            //    BackgroundColor = Color.FromHex($"{p.Data["color"]}")

            //            //});
            //        });

            //    }
            //    else if (p.Data.ContainsKey("aps.alert.title"))
            //    {
            //        //Device.BeginInvokeOnMainThread(() =>
            //        //{
            //        //    mPage.Message = $"{p.Data["aps.alert.title"]}";
            //        //});

            //    }
            //};

            //CrossFirebasePushNotification.Current.OnNotificationAction += (s, p) =>
            //{
            //    System.Diagnostics.Debug.WriteLine("Action");

            //    if (!string.IsNullOrEmpty(p.Identifier))
            //    {
            //        System.Diagnostics.Debug.WriteLine($"ActionId: {p.Identifier}");
            //        foreach (var data in p.Data)
            //        {
            //            System.Diagnostics.Debug.WriteLine($"{data.Key} : {data.Value}");
            //        }

            //    }

            //};

            //CrossFirebasePushNotification.Current.OnNotificationDeleted += (s, p) =>
            //{
            //    System.Diagnostics.Debug.WriteLine("Dismissed");
            //};
        }
    }
}
