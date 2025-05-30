using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DainikBazar.Domain.Models;

public class User
{
    [Key]
    public string Id { get; set; }
    public required string Name { get; set; }
    public ICollection<Cart> Cart { get; set; } = new List<Cart>();
    public ICollection<Order> Order { get; set; } = new List<Order>();

}