using System;
using System.Collections.Generic;

namespace BuySell.Model
{
	public class BuyingSellingModel
	{
        public int OrderId { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }

    //public class OrderItem
    //{
    //    public int Amount { get; set; }
    //    public int Tax { get; set; }
    //    public bool IsPaid { get; set; }
    //    public string ShipmentStatus { get; set; }
    //    public bool IsDispute { get; set; }
    //    public string BuyerDisputeReason { get; set; }
    //    public string SellerDisputeReason { get; set; }
    //    public DashBoardModel Product { get; set; }
    //    public string PayerToken { get; set; }
    //    public string PaymentAuth { get; set; }
    //    public int UserCardOnFileId { get; set; }
    //}

    //public class TagImage
    //{
    //    public string Image { get; set; }
    //    public string Extension { get; set; }
    //    public string ImagePath { get; set; }
    //    public bool IsDeleted { get; set; }
    //}
}

