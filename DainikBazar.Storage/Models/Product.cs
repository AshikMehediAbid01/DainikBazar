namespace DainikBazar.Storage.Models;

public class Product
{
    public int ProductId { get; set; }
    public string? ProductGuid { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public string? ImageUrl { get; set; }
    public ICollection<ReviewAndRating>? ReviewAndRatings { get; set; } = [];

    // public int? OrderId { get; set; }
    public ICollection<Order>? Orders { get; set; }

}
