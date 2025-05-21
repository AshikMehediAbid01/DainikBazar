
namespace DainikBazar.Domain.Entities;

public class User
{
    public string Id { get; set; }
    public Cart Cart { get; set; }
    public ICollection<Order> Order { get; set; } = new List<Order>();
}
