using System;
using System.Collections.Generic;
using System.Text;

namespace BuySell.Model
{

    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int StatusId { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsPaid { get; set; }
        public List<object> OrderShipping { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }

    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int OrderItemId { get; set; }
        public int ProductId { get; set; }
        public int StatusId { get; set; }
        public int? Amount { get; set; }
        public int? Tax { get; set; }
        public bool IsPaid { get; set; }
        public DashBoardModel Product { get; set; }
      
    }

    public class Product
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int StoreId { get; set; }
        public int CategoryId { get; set; }
        public string Source { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double ShippingPrice { get; set; }
        public double YourEarnings { get; set; }
        public double Discount { get; set; }
        public string Size { get; set; }
        public string Quantity { get; set; }
        public object Availability { get; set; }
        public object ProductRating { get; set; }
        public string ProductCondition { get; set; }
        public string ProductColor { get; set; }
        public string Brand { get; set; }
        public string ProductCoverImage { get; set; }
        public string TagImage { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public object LikeCount { get; set; }
        public object CustomSize { get; set; }
        public string ProductImages { get; set; }
    }

    public class UserCartModel
    {
        public int OrderId { get; set; }
        public int? UserId { get; set; }
        public int? StatusId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? IsPaid { get; set; }
        public List<object> OrderShipping { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
