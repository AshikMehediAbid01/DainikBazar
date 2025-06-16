namespace DainikBazar.Domain.Models;

public class Cart
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public string CartStatus { get; set; }
    public ICollection<CartItem> CartItems { get; set; } = [];
    public decimal ActualPrice { get; set; }
    public decimal TotalPrice => ActualPrice;
}