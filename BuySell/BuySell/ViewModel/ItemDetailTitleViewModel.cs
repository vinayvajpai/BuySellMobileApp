using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace BuySell.ViewModel
{
    public class ItemDetailTitleViewModel : BaseViewModel
    {
        #region Constructor
        public ItemDetailTitleViewModel(INavigation _nav)
        {
            navigation = _nav;
        }
        #endregion
    }
}