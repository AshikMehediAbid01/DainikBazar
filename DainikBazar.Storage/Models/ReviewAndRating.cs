using System.ComponentModel.DataAnnotations;

namespace DainikBazar.Storage.Models;

public class ReviewAndRating
{
    public int Id { get; set; }
    public string? Review { get; set; }

    [Range(1, 5, ErrorMessage = "Please select a rating between 1 and 5.")]
    public required int Rating { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public int ProductId { get; set; }
    public Product? Product { get; set; }

    public string UserId { get; set; }
    public User? User { get; set; }
}
