using System.Text;
using System.Text.Json.Serialization;
using DainikBazar.UI.ApiServices.Interfaces;
using DainikBazar.UI.Models;

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DainikBazar.UI.Controllers;

public class ProductController : Controller
{
    private readonly IProductApiService _apiService;

    public ProductController(IProductApiService apiService)
    {
        _apiService = apiService;
    }



    [HttpGet]
    public IActionResult Index()
    {
        var products = _apiService.GetAllProductsAsync().Result;

        if (products == null)
        {
            TempData["ErrorMessage"] = "No products found";
            return View(new List<ProductVM>());
        }

        return View(products);
    }





    [HttpGet]
    public IActionResult CreateProduct()
    {
        return View();
    }
    [HttpPost]
    public IActionResult CreateProduct(ProductVM product)
    {
        if (!ModelState.IsValid) return View(product);

        bool isSuccess = _apiService.CreateProductAsync(product).Result;


        if (isSuccess)
        {
            TempData["SuccessMessage"] = "New Product Created successfully";
            return RedirectToAction(nameof(Index));
        }
        else
        {
            TempData["ErrorMessage"] = "Something went wrong";
            return View(product);
        }


    }





    // Product Details
    [HttpGet]
    public IActionResult DetailsProduct(int id)
    {
        var product = _apiService.GetProductByIdAsync(id).Result;

        if (product == null)
        {
            TempData["ErrorMessage"] = "Product not found";
            return RedirectToAction(nameof(Index));
        }

        return View(product);
    }




    // Update Product
    [HttpGet]
    public IActionResult UpdateProduct(int id)
    {
        var product = _apiService.GetProductByIdAsync(id).Result;

        if (product == null)
        {
            TempData["ErrorMessage"] = "Product not found";
            return RedirectToAction(nameof(Index));
        }

        return View(product);
    }

    [HttpPost]
    public IActionResult UpdateProduct(ProductVM product)
    {
        if (!ModelState.IsValid) return View(product);

        bool isSuccess = _apiService.CreateProductAsync(product).Result;

        if (isSuccess)
        {
            TempData["SuccessMessage"] = "Product Updated successfully";
            return RedirectToAction(nameof(Index));
        }
        else
        {
            TempData["ErrorMessage"] = "Something went wrong";
            return View(product);
        }

    }


    // Delete Product
    [HttpGet]
    public IActionResult DeleteProduct(int id)
    {
        var product = _apiService.GetProductByIdAsync(id).Result;

        if (product == null)
        {
            TempData["ErrorMessage"] = "Product not found";
            return RedirectToAction(nameof(Index));
        }

        return View(product);
    }

    [HttpPost, ActionName("DeleteProduct")]
    public IActionResult DeleteProductConfirmed(int id)
    {
        bool isSuccess = _apiService.DeleteProductAsync(id).Result;

        if (isSuccess)
        {
            return RedirectToAction(nameof(Index));
        }

        return View();
    }

}
