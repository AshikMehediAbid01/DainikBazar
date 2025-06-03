namespace DainikBazar.UI.Models;

public class OrderVM
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public decimal SubtotalPrice { get; set; }
    public decimal DeliveryCharge { get; set; }
    public decimal TotalPrice => SubtotalPrice + DeliveryCharge;
    public int? ProductId { get; set; }
    public ICollection<CartItemVM>? CartItems { get; set; }
 //   public int? CartId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public string OrderStatus { get; set; }
    public DateTime OrderDate { get; set; }
    public string PaymentMethod { get; set; }
    public string OrderHistory { get; set; }
    public string ReceiverAddress { get; set; }
    public string ReceiverPhone { get; set; }
    public string ReceiverName { get; set; }
}

