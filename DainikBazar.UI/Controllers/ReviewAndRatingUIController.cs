using System.Security.Claims;
using System.Threading.Tasks;
using DainikBazar.UI.ApiServices.Interfaces;
using DainikBazar.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DainikBazar.UI.Controllers;

public class ReviewAndRatingUIController(IReviewApiService apiService) : Controller
{
    [HttpGet]
    public IActionResult CreateReview(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        ViewBag.GuId = id;
        int ProductId = Convert.ToInt32(TempData["ProductId"]);
        var reviewEntity = new ReviewAndRatingVM
        {
            ProductId = ProductId,
            Rating = 2,
            UserId = "358a384a-ae96-44ab-8940-1567b66709d0"
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

        bool isSuccess = await apiService.CreateReviewAsync(review);

        if (isSuccess)
        {
            TempData["SuccessMessage"] = "New Product Created successfully";
            return RedirectToAction("DetailsProduct", "Product", new { id = TempData["GuId"] } );
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
