
namespace DainikBazar.Storage.Models;

public class Cart
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
    public string CartStatus { get; set; }
    public List<CartItem> CartItems { get; set; } = [];
    public Order Order { get; set; }
    public decimal ActualPrice { get; set; }//=> CartItems.Sum( item => item.Quantity * item.Product.Price); 
    public decimal TotalPrice => ActualPrice;  
}
