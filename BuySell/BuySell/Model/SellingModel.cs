using System;
using BuySell.ViewModel;
using Xamarin.Forms;

namespace BuySell.Model
{
   // SellingModel
    public class SellingModel : BaseViewModel
    {
        public ImageSource Image { get; set; }
        public string Description { get; set; }
        public string DollerValue { get; set; }
        public string Size { get; set; }
        public string Buyer { get; set; }
        public ImageSource NextImage { get; set; }
    }
}

