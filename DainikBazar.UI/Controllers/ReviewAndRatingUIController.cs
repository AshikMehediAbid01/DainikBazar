using System.Security.Claims;
using System.Threading.Tasks;
using DainikBazar.UI.ApiServices.Interfaces;
using DainikBazar.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DainikBazar.UI.Controllers;

public class ReviewAndRatingUIController : Controller
{
    private readonly IReviewApiService _apiService;

    public ReviewAndRatingUIController(IReviewApiService apiService)
    {
        _apiService = apiService;
    }


    [HttpGet]
    public IActionResult CreateReview(int? Id)
    {
        if (Id == null)
        {
            return NotFound();
        }
        var reviewEntity = new ReviewAndRatingVM
        {
            ProductId = Id.Value,
            Rating = 2,
            UserId = "f15061b4-d79c-4596-b8e5-8ec21f6f69a2"
        };
        return View(reviewEntity);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
     
    public async Task<IActionResult> CreateReview(int ProductId, string UserId, int Rating, String? Review)
    {
        var review = new ReviewAndRatingVM
        {
            ProductId = ProductId,
            UserId = UserId,
            CreatedAt = DateTime.Now,
            Rating = Rating,
            Review = Review
        };

        if (!ModelState.IsValid) return View(review);

        bool isSuccess = await _apiService.CreateReviewAsync(review);

        if (isSuccess)
        {
            TempData["SuccessMessage"] = "New Product Created successfully";
            return RedirectToAction("DetailsProduct", "Product", new { id = ProductId } );
        }
        else
        {
            TempData["ErrorMessage"] = "Something went wrong";
            return View(review);
        }

    }

/*    public async Task<IActionResult> CreateReview(ReviewAndRatingVM review)
    {
        if (!ModelState.IsValid) return View(review);


        return View(review);

        *//*        bool isSuccess = await _apiService.CreateReviewAsync(review);


                if (isSuccess)
                {
                    TempData["SuccessMessage"] = "New Product Created successfully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong";
                    return View(review);
                }*//*
    }*/






}
