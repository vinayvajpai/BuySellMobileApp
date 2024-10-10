using System;
using System.Collections.Generic;
using System.Text;

namespace BuySell.Model
{
    public class MyEarningsResponseModel
    {
        public string UserId { get; set; }
        public string Balance { get; set; }
        public string CompletedPayments { get; set; }
        public string InWithdrawalStatusPayments { get; set; }
        public DateTime LastCompletedPayment { get; set; }
        public LastWithdrawals WithDrawalHistory { get;}
    }

    public class LastWithdrawals
    {
        public string CompletedPayments { get; set; }
        public DateTime LastCompletedPayment { get; set; }
    }

}
