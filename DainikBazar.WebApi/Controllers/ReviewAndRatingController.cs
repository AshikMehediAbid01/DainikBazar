using System.Threading.Tasks;
using DainikBazar.Application.Common.DTOs;
using DainikBazar.Application.Services.Interfaces;
using DainikBazar.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DainikBazar.WebApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class ReviewAndRatingController : ControllerBase
{
    private readonly IReviewService _service;

    public ReviewAndRatingController(IReviewService service)
    {
        _service = service;
    }



    [HttpPost]
    public async Task<IActionResult> CreateReview(int productId,int userId,[FromBody] ReviewDto reviewAndRating)
    {
        if (!ModelState.IsValid)return BadRequest(ModelState);

        try
        {
            var productReview = new ReviewAndRating
            {
                Review = reviewAndRating.Review,
                Rating = reviewAndRating.Rating,
                CreatedAt = DateTime.Now,
                ProductId = productId,
                UserId = userId, // Assumed
            };
            await _service.CreateNewAsync(productReview);
            return Ok(productReview);
        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);
        }
    }


    [HttpGet]
    public async Task<IActionResult> GetAllReview()
    {
        try
        {
            var reviews = await _service.GetAllAsync();
            return Ok(reviews);
        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message);
        }
    }
}
