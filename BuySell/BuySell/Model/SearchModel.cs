using System;
using BuySell.ViewModel;
using Xamarin.Forms;

namespace BuySell.Model
{
    //SearchModel
    public class SearchModel
    {
            public ImageSource StarImage { get; set; }
            public string Description { get; set; }
            public ImageSource NextImage { get; set; }
    }

    //SearchResultModel
    public class SearchResultModel
    {
        public ImageSource StarImage { get; set; }
        public string Description { get; set; }
        public ImageSource NextImage { get; set; }
        public Color TextColor { get; set; }
        public string StoreID { get; set; }

    }
}
