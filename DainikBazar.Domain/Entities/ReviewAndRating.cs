using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DainikBazar.Domain.Entities;

public class ReviewAndRating
{
    public int ReviewAndRatingId { get; set; }
    public int ProductId { get; set; }
    public int UserId { get; set; }
    public required string Review { get; set; }
    public required int Rating { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public virtual Product? Product { get; set; }

}
