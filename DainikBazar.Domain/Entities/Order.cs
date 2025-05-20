using System;
using System.Collections.Generic;
using System.Linq;

namespace DainikBazar.Domain.Entities;

public class Order
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public decimal SubtotalPrice => OrderItems.Sum(item => item.Quantity * item.UnitPrice); 
    public decimal DeliveryCharge {  get; set; }
    public decimal TotalPrice => SubtotalPrice + DeliveryCharge; 
    public string OrderStatus { get; set; }
    public DateTime OrderDate { get; set; }
    public string PaymentMethod { get; set; }

    public string ReceiverAddress { get; set; }
    public string ReceiverPhone { get; set; }
    public string ReceiverName { get; set; }
}
