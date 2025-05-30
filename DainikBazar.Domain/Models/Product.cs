using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DainikBazar.Domain.Models;


public class Product
{
    public int ProductId { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required decimal Price { get; set; }
    public int Quantity { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public string? ImageUrl { get; set; }

    public ICollection<ReviewAndRating>? ReviewAndRatings { get; set; } = new List<ReviewAndRating>();
    public int? OrderId { get; set; }
    public Order Order { get; set; }

}