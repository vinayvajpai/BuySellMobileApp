using System;
using BuySell.Services;
using Foundation;
using System.IO;
using BuySell.iOS.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(WebViewService_iOS))]
namespace BuySell.iOS.Services
{
    public class WebViewService_iOS : IWebViewService
    {
        public string Get()
        {
            return Path.Combine(NSBundle.MainBundle.BundlePath, "Content/");
        }

        public string ReadAssetFile(string filepath)
        {
            using (StreamReader reader = new StreamReader(NSBundle.MainBundle.BundlePath+"/"+filepath))
            {
                string filecontent = reader.ReadToEnd();
                return filecontent;
            }
            return string.Empty;
        }
    }
}

