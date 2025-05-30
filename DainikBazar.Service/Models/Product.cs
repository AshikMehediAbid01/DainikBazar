namespace DainikBazar.Service.Models;

public class Product
{
    public int ProductId { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required decimal Price { get; set; }
    public int Quantity { get; set; }
    public ICollection<ReviewAndRating>? ReviewAndRatings { get; set; } = new List<ReviewAndRating>();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string? ImageUrl { get; set; }
}