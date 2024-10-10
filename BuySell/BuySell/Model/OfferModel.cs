using System;
using BuySell.ViewModel;
using Xamarin.Forms;

namespace BuySell.Model
{
    public class OfferModel : BaseViewModel
    {
        public ImageSource Image { get; set; }
        public string Description { get; set; }
        public string DollerValue { get; set; }
        public string OfferValue { get; set; }
        public string Size { get; set; }
        public string Seller { get; set; }
        public ImageSource NextImage { get; set; }
    }
}

