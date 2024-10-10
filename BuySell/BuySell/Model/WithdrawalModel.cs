using System;
using System.Collections.Generic;
using System.Text;

namespace BuySell.Model
{
    public class WithdrawalRequestModel
    {
        public int UserId { get; set; }
        public long BankAccountId { get; set; }
        public double AmountRequested { get; set; }
    }
}
