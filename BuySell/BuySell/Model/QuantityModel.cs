using System;
using BuySell.ViewModel;

namespace BuySell.Model
{
    public class QuantityModel : BaseViewModel
    {
        public int QIndex { get; set; }
        
        private string _QuantityType;
        public string QuantityType
        {
            get { return _QuantityType; }
            set { _QuantityType = value; OnPropertyChanged("QuantityType"); }
        }

        private string _Quantity;
        public string Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; OnPropertyChanged("Quantity"); }
        }

        private string _Size;
        public string Size
        {
            get { return _Size; }
            set { _Size = value; OnPropertyChanged("Size"); }
        }
    }
}
