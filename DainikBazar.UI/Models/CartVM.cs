
namespace DainikBazar.UI.Models;

public class CartVM
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public string CartStatus { get; set; }
    public ICollection<CartItemVM> CartItems { get; set; } = [];
    public decimal SubtotalPrice { get; set; }
    public decimal TotalPrice => SubtotalPrice;
}
