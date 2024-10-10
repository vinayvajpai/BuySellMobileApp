using System;
using UIKit;

namespace BuySell.iOS.Extentions
{
    public static class UIViewControllerExtensions
    {
        public static UIViewController GetViewControllerOnTopOfStack(this UIViewController rootViewController)
        {
            while (rootViewController.PresentedViewController != null)
            {
                rootViewController = rootViewController.PresentedViewController;
            }

            return rootViewController;
        }
    }
}
