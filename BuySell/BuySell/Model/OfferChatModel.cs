using System;
using BuySell.Helper;
using Xamarin.Forms;

namespace BuySell.Model
{
    //OfferChatModel
    public class OfferChatModel
    {
        public string Name { get; set; }
        public bool Sender { get; set; }
        public string PaymentMsg { get; set; }
        public string SellerProfile { get; set; }
        public string BuyerProfile { get; set; }
        public DateTime msgTime { get; set; }
        public string thmcolor
        {
            get
            {
                return Global.GetThemeColor(Global.setThemeColor);
            }
        }
    }
}
