namespace DainikBazar.Domain.Models;

public class Cart
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public string CartStatus { get; set; }
    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    public decimal SubtotalPrice {get; set; }
    public decimal TotalPrice => SubtotalPrice;
}
