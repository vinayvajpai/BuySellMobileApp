using System;
using BuySell.ViewModel;

namespace BuySell.Model
{
    //MyBalanceModel
    public class MyBalanceModel : BaseViewModel
    {
       public string Date { get; set; }
       public string Description { get; set; }
       public string CardNumber { get; set; }
       public string Process { get; set; }
    }
}
