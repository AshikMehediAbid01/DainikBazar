using DainikBazar.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DainikBazar.UI.Controllers;

public class ReviewAndRatingUIController : Controller
{
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
            Rating = 0,
           // UserId = "123"
        };
        return View(reviewEntity);
    }
}
