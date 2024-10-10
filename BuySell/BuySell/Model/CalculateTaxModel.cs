using System;
using System.Collections.Generic;
using System.Text;

namespace BuySell.Model
{
    class CalculateTaxModel
    {
        public int UserId { get; set; }
        public long OrderId { get; set; }
        public double Amount { get; set; }
        public double TaxAmount { get; set; }
        public string ShippingZipCode { get; set; }
    }

    public class ComputeTax
    {
        public int UserId { get; set; }
        public int OrderId { get; set; }
        public int OrderItemId { get; set; }
        public int SellerId { get; set; }
        public decimal Amount { get; set; }
        public decimal TaxAmount { get; set; }
        public string ShippingZipCode { get; set; }
        public int ShippingCost { get; set; }
        public string ShippingState2LetterCode { get; set; }
    }

}
