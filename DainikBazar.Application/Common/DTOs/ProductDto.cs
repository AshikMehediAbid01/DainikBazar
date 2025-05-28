using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DainikBazar.Domain.Entities;

namespace DainikBazar.Application.Common.DTOs;

public class ProductDto
{
    public int ProductId { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required decimal Price { get; set; }
    public int Quantity { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public string? ImageUrl { get; set; }
    public ICollection<ReviewDto>? ReviewAndRatings { get; set; } = new List<ReviewDto>();
}
