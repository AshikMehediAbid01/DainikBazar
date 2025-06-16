namespace DainikBazar.Storage.Models;

public class User
{
    public string Id { get; set; }
    public string Email { get; set; }
    public required string Name { get; set; }
    public ICollection<Cart> Cart { get; set; } = [];
    public ICollection<Order> Order { get; set; } = [];
}