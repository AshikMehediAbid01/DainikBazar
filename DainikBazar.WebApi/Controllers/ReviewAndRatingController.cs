using System.Threading.Tasks;
using DainikBazar.Application.Common.DTOs;
using DainikBazar.Application.Managers.Interfaces;
using DainikBazar.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DainikBazar.Service.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class ReviewAndRatingController(IReviewManager service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateReview([FromBody] ReviewDto reviewAndRating)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            var productReview = new ReviewDto
            {
                Review = reviewAndRating.Review,
                Rating = reviewAndRating.Rating,
                ProductId = reviewAndRating.ProductId,
                UserId = reviewAndRating.UserId // Assumed
            };
            await service.CreateNewAsync(productReview);
            return Ok(productReview);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }



    [HttpGet("{ProductId}")]
    public async Task<IActionResult> GetAllReviewByProductId(int? ProductId)
    {
        if (ProductId == null) return NotFound();
        try
        {
            var reviews = await service.GetAllByIdAsync(ProductId.Value);
            return Ok(reviews);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
