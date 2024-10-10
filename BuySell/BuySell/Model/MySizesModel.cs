using System;
using BuySell.ViewModel;

namespace BuySell.Model
{
    public class MySizesModel : BaseViewModel
    {
            private string _Dresses = "- Select -";

            public string Dresses
            {
                get { return _Dresses; }
                set { _Dresses = value; OnPropertyChanged("Dresses"); }
            }

            private string _Tops = "- Select -";

            public string Tops
            {
                get { return _Tops; }
                set { _Tops = value; OnPropertyChanged("Tops"); }
            }

            private string _Bottoms = "- Select -";
            public string Bottoms
            {
                get { return _Bottoms; }
                set { _Bottoms = value; OnPropertyChanged("Bottoms"); }
            }

            private string _Jeans = "- Select -";
            public string Jeans
            {
                get { return _Jeans; }
                set { _Jeans = value; OnPropertyChanged("Jeans"); }
            }

            private string _Shoes = "- Select -";
            public string Shoes
            {
            get { return _Shoes; }
            set { _Shoes = value; OnPropertyChanged("Shoes"); }
            }
    }
}
