using DainikBazar.Domain.Models;

namespace DainikBazar.UI.Models;

public class ProductVM
{
    public int ProductId { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required decimal Price { get; set; }
    public int Quantity { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public string? ImageUrl { get; set; }
    public ICollection<ReviewAndRatingVM>? ReviewAndRatings { get; set; } = new List<ReviewAndRatingVM>();

    public int? OrderId { get; set; }
    public Order? Order { get; set; }
}
