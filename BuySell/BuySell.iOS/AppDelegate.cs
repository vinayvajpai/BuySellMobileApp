using System;
using System.Collections.Generic;
using System.Linq;
using Acr.UserDialogs;
using BuySell.Helper;
using ColorPicker.iOS;
using FFImageLoading.Forms.Platform;
using Firebase.CloudMessaging;
using Foundation;
//using Plugin.FirebasePushNotification;
using UIKit;
using UserNotifications;

namespace BuySell.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate,IUNUserNotificationCenterDelegate, IMessagingDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Xamarin.Forms.Forms.Init();
            Rg.Plugins.Popup.Popup.Init();
            CachedImageRenderer.Init();
            ColorPickerEffects.Init();
            CachedImageRenderer.InitImageSourceHandler();
            Firebase.Core.App.Configure();
           
            //FirebasePushNotificationManager.Initialize(options, new NotificationUserCategory[]
            //{
            //    new NotificationUserCategory("message",new List<NotificationUserAction> {
            //        new NotificationUserAction("Reply","Reply",NotificationActionType.Foreground)
            //    }),
            //    new NotificationUserCategory("request",new List<NotificationUserAction> {
            //        new NotificationUserAction("Accept","Accept"),
            //        new NotificationUserAction("Reject","Reject",NotificationActionType.Destructive)
            //    }),

            //},true);

            if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
            {
                // For iOS 10 display notification (sent via APNS)
                UNUserNotificationCenter.Current.Delegate = this;
                var authOptions = UNAuthorizationOptions.Alert | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Sound;
                UNUserNotificationCenter.Current.RequestAuthorization(authOptions, (granted, error) =>
                {
                    Console.WriteLine("Push Setup successfully - " + granted);
                });
            }
            else
            {
                // iOS 9 or before
                var allNotificationTypes = UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound;
                var settings = UIUserNotificationSettings.GetSettingsForTypes(allNotificationTypes, null);
                UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
            }

            UIApplication.SharedApplication.RegisterForRemoteNotifications();
            Messaging.SharedInstance.Delegate = this;
            if (UNUserNotificationCenter.Current != null)
            {
                UNUserNotificationCenter.Current.Delegate = new UserNotificationCenterDelegate();
            }
            LoadApplication(new App());
            return base.FinishedLaunching(app, options);
        }

        [Export("messaging:didReceiveRegistrationToken:")]
        public void DidReceiveRegistrationToken(Messaging messaging, string fcmToken)
        {
            // Monitor token generation: To be notified whenever the token is updated.
            if(Xamarin.Forms.Application.Current.Properties.ContainsKey("Fcmtoken"))
            {
                Xamarin.Forms.Application.Current.Properties.Remove("Fcmtoken");
                Xamarin.Forms.Application.Current.SavePropertiesAsync();
            }
            Xamarin.Forms.Application.Current.Properties["Fcmtoken"] = fcmToken ?? "";
            Xamarin.Forms.Application.Current.SavePropertiesAsync();
            //Constant.DeviceToken = fcmToken;
            // TODO: If necessary send token to application server.
            // Note: This callback is fired at each app startup and whenever a new token is generated.
        }

        //Notification tapping when the app is in background mode.
        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
        {
            NSDictionary aps = userInfo.ObjectForKey(new NSString("aps")) as NSDictionary;
            string alert = string.Empty;
            string title = string.Empty;

        }

        //public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        //{
        //    FirebasePushNotificationManager.DidRegisterRemoteNotifications(deviceToken);
        //}

        //public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
        //{
        //    FirebasePushNotificationManager.RemoteNotificationRegistrationFailed(error);
        //}
        //// To receive notifications in foregroung on iOS 9 and below.
        //// To receive notifications in background in any iOS version
        //public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
        //{
        //    // If you are receiving a notification message while your app is in the background,
        //    // this callback will not be fired 'till the user taps on the notification launching the application.

        //    // If you disable method swizzling, you'll need to call this method. 
        //    // This lets FCM track message delivery and analytics, which is performed
        //    // automatically with method swizzling enabled.
        //    FirebasePushNotificationManager.DidReceiveMessage(userInfo);
        //    // Do your magic to handle the notification data
        //    System.Console.WriteLine(userInfo);

        //    completionHandler(UIBackgroundFetchResult.NewData);
        //}
    }
}
