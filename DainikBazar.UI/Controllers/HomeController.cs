using System.Diagnostics;
using DainikBazar.UI.ApiServices.Interfaces;
using DainikBazar.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DainikBazar.UI.Controllers;

public class HomeController(IProductApiService apiService, ILogger<HomeController> _logger) : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        var products = apiService.GetAllProductsAsync().Result;
        if (products == null)
        {
            TempData["ErrorMessage"] = "No products found";
            return View(new List<ProductVM>());
        }
        return View(products);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
