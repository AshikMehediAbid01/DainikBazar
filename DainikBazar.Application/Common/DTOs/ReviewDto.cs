using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DainikBazar.Application.Common.DTOs;

public class ReviewDto
{
    public string? Review { get; set; }

    [Range(1, 5, ErrorMessage = "Please select a rating between 1 and 5.")]
    public required int Rating { get; set; }

    public int ProductId{get;set;}
    public string UserId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
