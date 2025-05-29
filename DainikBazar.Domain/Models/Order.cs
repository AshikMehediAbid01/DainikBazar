using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DainikBazar.Domain.Models;


public class Order
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
    public decimal SubtotalPrice { get; set; }
    public decimal DeliveryCharge { get; set; }
    public decimal TotalPrice => SubtotalPrice + DeliveryCharge;
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public int Quantity { get; set; }
    public int UnitPrice { get; set; }
    public string OrderStatus { get; set; }
    public DateTime OrderDate { get; set; }
    public string PaymentMethod { get; set; }
    public string OrderHistory { get; set; }
    public string ReceiverAddress { get; set; }
    public string ReceiverPhone { get; set; }
    public string ReceiverName { get; set; }
}
