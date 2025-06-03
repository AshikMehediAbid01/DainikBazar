using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using DainikBazar.UI.ApiServices.Interfaces;
using DainikBazar.UI.Models;

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;

namespace DainikBazar.UI.Controllers;

public class ProductController(IProductApiService apiService, IImageService _imageService) : Controller
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





    [HttpGet]
    public IActionResult CreateProduct()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> CreateProduct(ProductVM product, IFormFile? ImageUrl)
    {
        if (!ModelState.IsValid) return View(product);

        // Handle image upload
        if (ImageUrl != null && ImageUrl.Length > 0)
        {
            try
            {
                product.ImageUrl = await _imageService.ImageMappingAsync(ImageUrl);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("ImageUpload", $"Image upload failed: {ex.Message}");
                return View(product);
            }
        }
        else
        {
            product.ImageUrl = "Images/NoImageFound.jpg";
        }


        bool isSuccess = await apiService.CreateProductAsync(product);


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
        var productDto = apiService.GetProductByIdAsync(id).Result;

        if (productDto == null)
        {
            TempData["ErrorMessage"] = "Product not found";
            return RedirectToAction(nameof(Index));
        }

        return View(productDto);
    }




    // Update Product
    [HttpGet]
    public async Task<IActionResult> UpdateProduct(int id)
    {
        var product = await apiService.GetProductByIdAsync(id);

        if (product == null)
        {
            TempData["ErrorMessage"] = "Product not found";
            return RedirectToAction(nameof(Index));
        }

        return View(product);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateProduct(ProductVM product, IFormFile? ImageUrl)
    {
        if (!ModelState.IsValid) return View(product);

        try
        {
            product.ImageUrl = await _imageService.ImageMappingAsync(ImageUrl);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("ImageUpload", $"Image upload failed: {ex.Message}");
            return View(product);
        }

        bool isSuccess = apiService.UpdateProductAsync(product).Result;

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
        var product = apiService.GetProductByIdAsync(id).Result;

        if (product == null)
        {
            TempData["ErrorMessage"] = "Product not found";
            return RedirectToAction(nameof(Index));
        }

        return View(product);
    }

    [HttpPost, ActionName("DeleteProduct")]
    public async Task<IActionResult> DeleteProductConfirmed(int id)
    {
        bool isSuccess = await apiService.DeleteProductAsync(id);

        if (isSuccess)
        {
            return RedirectToAction(nameof(Index));
        }

        return View();
    }

}