using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DainikBazar.Domain.Entities;

public class Product
{
    public int ProductId { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required decimal Price { get; set; }
    public int Quantity { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string? ImageUrl { get; set; }

    public int ReviewAndRatingId { get; set; }
    public virtual ReviewAndRating? ReviewAndRating { get; set; }

    public virtual ICollection<ReviewAndRating>? ReviewAndRatings { get; set; } = new List<ReviewAndRating>();
}
