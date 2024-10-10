using BuySell.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BuySell.Model
{
    //public class BankAccountModel : BaseViewModel
    //{   
    //    public string AccHolderName { get; set; }
    //    public string BankRoutingNo { get; set; }
    //    public string CheckingAccNo { get; set; }
    //    public string ConfmCheckingAccNo { get; set; }
    //    public bool IsDefault { get; set; } = false;
    //}

    public class BankAccountModel : BaseViewModel
    {
        public int BankAccountId { get; set; }
        public string AccountHolderName { get; set; }
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        public string RoutingNumber { get; set; }
        public bool IsDefault { get; set; }
    }

}
