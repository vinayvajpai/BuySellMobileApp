using System;
using BuySell.ViewModel;
using Xamarin.Forms;

namespace BuySell.Model
{
    //ViewOffersModel
    public class ViewOffersModel : BaseViewModel
    {
        public int OfferID { get; set; }
        public int ProductID { get; set; }
        public ImageSource Image { get; set; }
        public string Description { get; set; }
        public string DollerValue { get; set; }
        public string OfferValue { get; set; }
        public string Size { get; set; }
        public string Seller { get; set; }
        public string Buyer { get; set; }
        public string Brand { get; set; }
        public ImageSource NextImage { get; set; }
    }
}

