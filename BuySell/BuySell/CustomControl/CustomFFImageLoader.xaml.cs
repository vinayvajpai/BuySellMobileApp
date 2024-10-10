using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using BuySell.Helper;
using BuySell.Model;
using BuySell.Persistence;
using Xamarin.Forms;

namespace BuySell.CustomControl
{
    public partial class CustomFFImageLoader : ContentView
    {
        public CustomFFImageLoader()
        {
            InitializeComponent();
        }

        public string ProductImageURL
        {
            get { return (string)GetValue(ProductImageURLProperty); }
            set { SetValue(ProductImageURLProperty, value); }

        }



        public static BindableProperty ProductImageURLProperty = BindableProperty.Create(
                                                         propertyName: "ProductImageURL",
                                                         returnType: typeof(string),
                                                         declaringType: typeof(CustomFFImageLoader),
                                                         defaultValue: string.Empty,
                                                         defaultBindingMode: BindingMode.TwoWay,
                                                         propertyChanged: imageUrlPropertyChanged);

        public ImageSource ProductImage
        {
            get { return (ImageSource)GetValue(ProductImageProperty); }
            set { SetValue(ProductImageProperty, value); }

        }



        public static BindableProperty ProductImageProperty = BindableProperty.Create(
                                                         propertyName: "ProductImage",
                                                         returnType: typeof(ImageSource),
                                                         declaringType: typeof(CustomFFImageLoader),
                                                         defaultValue: null,
                                                         defaultBindingMode: BindingMode.TwoWay);


        private static void imageUrlPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomFFImageLoader)bindable;
            var imgBytes = control.LoadImage(Convert.ToString(newValue.ToString()));
        }

        public byte[] LoadImage(string url)
        {
           
            byte[] data = null;
            try
            {
                var db = Global.GetConnection();
                if (db != null && !string.IsNullOrEmpty(url))
                {
                    var img = url.Split('/').LastOrDefault();
                    if (img != null)
                    {
                        if (db._sqlconnection.Table<CachedImageDatabaseModel>().Any(x => x.ImageName == img))
                        {
                            Debug.WriteLine("DB IMage " + img);
                            var OutputDataModel = db._sqlconnection.Table<CachedImageDatabaseModel>().Where(x => x.ImageName == img).LastOrDefault();
                            data = OutputDataModel.ImageByte;
                            Debug.WriteLine("Get from db");
                        }
                        else
                        {
                            String[] path = url.Split(':');
                            ProductImage = path[1] + ":" + path[2];
                            data = DownLoadAndStoreImage(path[1]+":"+ path[2], img, db);
                            Debug.WriteLine("Insert in db");
                        }
                        ProductImage = ImageSource.FromStream(() =>
                        new MemoryStream(data)
                        ); ;
                    }
                }
            }
            catch (Exception ex)
            {
                //Debug.WriteLine(ex.Message);
                //var properties = new Dictionary<string, string> { { "LoadImage for error : url" + Url + " and hash " + HashSha1, "ImageInfoModel" } };
                //Crashes.TrackError(ex.InnerException, properties);
            }
            finally
            {
            }
            return data;
        }
        private static byte[] DownLoadAndStoreImage(string url, string imgName, SqlProductDB db)
        {
            //  string OnlineImageUrl =url;
            //_image = await WebService.GetData(OnlineImageUrl, true);
            byte[] imageBytes = null;
            using (var webClient = new WebClient())
            {
                imageBytes = webClient.DownloadData(url);
                if (imageBytes != null)
                {
                    Debug.WriteLine("DOwnload IMage " + imgName);

                    // Image = imageBytes;
                    CachedImageDatabaseModel cachedImageDatabaseModel = new CachedImageDatabaseModel();
                    cachedImageDatabaseModel.ImageName = imgName;
                    cachedImageDatabaseModel.ImageByte = imageBytes;
                    cachedImageDatabaseModel.Url = url;
                    cachedImageDatabaseModel.Id = Guid.NewGuid();
                    //   InitDatabaseTable db = new InitDatabaseTable();
                    //   Image = ResizedImageByteArray;
                    db._sqlconnection.Insert(cachedImageDatabaseModel);
                }
            }
            return imageBytes;
        }
    }
}
