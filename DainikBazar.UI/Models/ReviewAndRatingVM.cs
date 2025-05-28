
using System.ComponentModel.DataAnnotations;

namespace DainikBazar.UI.Models;

public class ReviewAndRatingVM
{
    public int Id { get; set; }
    public string? Review { get; set; }

    [Range(1, 5, ErrorMessage = "Please select a rating between 1 and 5.")]
    public  int Rating { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public int ProductId { get; set; }
    public string UserId { get; set; }

   
}
