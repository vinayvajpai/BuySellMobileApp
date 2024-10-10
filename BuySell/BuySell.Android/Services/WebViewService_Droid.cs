using System;
using BuySell.Services;
using System.IO;
using BuySell.iOS.Services;
using Android.Content.Res;
using Xamarin.Forms;
using BuySell.Droid;

[assembly: Dependency(typeof(WebViewService_Droid))]
namespace BuySell.iOS.Services
{
    public class WebViewService_Droid : IWebViewService
    {
        public string Get()
        {
            return "file:///android_asset/Content/";
        }

        public string ReadAssetFile(string filepath)
        {
            string text = string.Empty;
            string filename = filepath;
            AssetManager assets = MainActivity.mainActivity.Assets;
            using (StreamReader reader = new StreamReader(assets.Open(filename)))
            {
                text = reader.ReadToEnd();
            }
            return text;
        }
    }
}

