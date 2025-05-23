
namespace DainikBazar.Domain.Entities;

public class User
{
    public string Id { get; set; }
    public string Name { get; set; }
    public ICollection<Cart> Cart { get; set; } = new List<Cart>();
    public ICollection<Order> Order { get; set; } = new List<Order>();
}
