using System;
using System.Collections.Generic;

namespace BuySell.Model
{
	public class UpdatePaymentStatusRequest1
	{
        public int UserId { get; set; }
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
        public decimal TaxAmount { get; set; }
        public string PaymentMessage { get; set; }
        public string PaymentToken { get; set; }
    }

  

    public class PaymentStatusOrder
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int StatusId { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsPaid { get; set; }
        public List<PaymentStatusOrderItem> OrderItems { get; set; }
    }

    public class PaymentOrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int StatusId { get; set; }
        public int? Amount { get; set; }
        public int? Tax { get; set; }
        public bool IsPaid { get; set; }
        public PaymentProduct Product { get; set; }

    }

    public class PaymentStatusOrderItem
    {
        public int OrderId { get; set; }
        public int OrderItemId { get; set; }
        public int StatusId { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Tax { get; set; }
        public bool IsPaid { get; set; }
        public PaymentProduct Product { get; set; }
    }

    public class PaymentProduct
    {
        public int Id { get; set; }
        public string UserFirstName { get; set; }
        public int UserId { get; set; }
        public string UserProfile { get; set; }
        public string UserName { get; set; }
        public string UserLastName { get; set; }
        public string Source { get; set; }
        public string ProductName { get; set; }
        public string Size { get; set; }
        public string ProductRating { get; set; }
        public string ProductCondition { get; set; }
        public string ProductColor { get; set; }
        public string ProductCategory { get; set; }
        public string ParentCategory { get; set; }
        public string Brand { get; set; }
        public string StoreType { get; set; }
        public string Quantity { get; set; }
        public string Availability { get; set; }
        public string TagImage { get; set; }
        public string Description { get; set; }
        public bool IsLike { get; set; }
        public int? LikeCount { get; set; }
    }

    public class UpdatePaymentStatusRequest
    {
        public PaymentStatusOrder Order { get; set; }
        public int PaymentId { get; set; }
        public decimal ShippingAmount { get; set; }
        public int ShippingAddressId { get; set; }
        public string PaymentReceiptNumber { get; set; }
        public string PaymentResponse { get; set; }
    }

    

    public class DeleteCartRequest
    {
        public int OrderId { get; set; }
        public int OrderItemId { get; set; }
        public int StatusId { get; set; }
        public int? Amount { get; set; }
        public int? Tax { get; set; }
        public bool IsPaid { get; set; }
        public DeleteCartProduct Product { get; set; }
    }

    public class DeleteCartProduct
    {
        public int Id { get; set; }
        public string UserFirstName { get; set; }
        public int UserId { get; set; }
        public string UserProfile { get; set; }
        public string UserName { get; set; }
        public string UserLastName { get; set; }
        public string Source { get; set; }
        public string ProductName { get; set; }
        public string Size { get; set; }
        public string ProductRating { get; set; }
        public string ProductCondition { get; set; }
        public string ProductColor { get; set; }
        public string ProductCategory { get; set; }
        public string ParentCategory { get; set; }
        public string Brand { get; set; }
        public string StoreType { get; set; }
        public string Quantity { get; set; }
        public string Availability { get; set; }
        public string TagImage { get; set; }
        public string Description { get; set; }
        public bool IsLike { get; set; }
        public int? LikeCount { get; set; }
    }

}

