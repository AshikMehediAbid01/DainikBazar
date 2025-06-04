namespace DainikBazar.Service.Models;

public class Product
{
    public int ProductId { get; set; }
    public string? ProductGuid { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required decimal Price { get; set; }
    public int Quantity { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public string? ImageUrl { get; set; }
    public ICollection<ReviewAndRating>? ReviewAndRatings { get; set; } = [];
    public Order? Order { get; set; }


}