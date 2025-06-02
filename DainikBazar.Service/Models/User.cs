using System.ComponentModel.DataAnnotations;

namespace DainikBazar.Service.Models;

public class User
{
    [Key]
    public string Id { get; set; }
    public required string Name { get; set; }
    public ICollection<Cart> Cart { get; set; } = [];
    public ICollection<Order> Order { get; set; } = [];

}