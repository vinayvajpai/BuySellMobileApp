using BuySell.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

public class ShipOrderItem
{
    public int OrderId { get; set; }
    public int OrderItemId { get; set; }
    public int StatusId { get; set; }
    public decimal Amount { get; set; }
    public decimal Tax { get; set; }
    public bool IsPaid { get; set; }
    public BuyingSellingProduct Product { get; set; }
}

public class ShippinglabelRequestModel
{
    public int UserId { get; set; }
    public List<ShipOrderItem> OrderItems { get; set; }
}
