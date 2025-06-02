namespace DainikBazar.Service.Models;

public class Cart
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public string CartStatus { get; set; }
    public ICollection<CartItem> CartItems { get; set; } = [];
    public decimal SubtotalPrice {get; set; }
    public decimal TotalPrice => SubtotalPrice;
}
