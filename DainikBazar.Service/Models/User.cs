using System.ComponentModel.DataAnnotations;

namespace DainikBazar.Service.Models;

public class User
{
    [Key]
    public string Id { get; set; }
    public required string Name { get; set; }
    public ICollection<Cart> Cart { get; set; } = new List<Cart>();
    public ICollection<Order> Order { get; set; } = new List<Order>();

}