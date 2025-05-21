using System.Text;
using System.Text.Json.Serialization;
using DainikBazar.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DainikBazar.UI.Controllers;

public class ProductController : Controller
{
    Uri baseAddress = new Uri("https://localhost:7155/api/");
    private readonly HttpClient _httpClient;
    public ProductController()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = baseAddress;
    }

    [HttpGet]
    public IActionResult Index()
    {
        List<ProductVM> products = new List<ProductVM>();
        var response = _httpClient.GetAsync(_httpClient.BaseAddress + "Products/GetAllProducts").Result;

        if (response.IsSuccessStatusCode)
        {
            var data = response.Content.ReadAsStringAsync().Result;
            products = JsonConvert.DeserializeObject<List<ProductVM>>(data);
        }
        else
        {
            ViewBag.ErrorMessage = "Error while fetching data from API";
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
        try
        {
            string data = JsonConvert.SerializeObject(product);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            var response = _httpClient.PostAsync(_httpClient.BaseAddress + "Products/CreateProduct", content).Result;

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "New Product Created successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["ErrorMessage"] = "Something went wrong";
                return View();
            }
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return View();
        }

    }

    // Product Details
    [HttpGet]
    public IActionResult DetailsProduct(int id)
    {
        var response = _httpClient.GetAsync(_httpClient.BaseAddress + "Products/GetProduct/" + id).Result;

        if (response.IsSuccessStatusCode)
        {
            var data = response.Content.ReadAsStringAsync().Result;
            var product = JsonConvert.DeserializeObject<ProductVM>(data);

            return View(product);
        }
        else
        {
            TempData["ErrorMessage"] = "Something went wrong";
            return View();
        }
    }


    // Update Product
    [HttpGet]
    public IActionResult UpdateProduct(int id)
    {
        var response = _httpClient.GetAsync(_httpClient.BaseAddress + "Products/GetProduct/" + id).Result;

        if (response.IsSuccessStatusCode)
        {
            var data = response.Content.ReadAsStringAsync().Result;
           var product = JsonConvert.DeserializeObject<ProductVM>(data);

            return View(product);
        }
        else
        {
            TempData["ErrorMessage"] = "Something went wrong";
            return View();
        }
    }

    [HttpPost]
    public IActionResult UpdateProduct(ProductVM product)
    {
        string data = JsonConvert.SerializeObject(product);
        StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
        var response = _httpClient.PutAsync(_httpClient.BaseAddress + "Products/UpdateProduct", content).Result;

        if (response.IsSuccessStatusCode)
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
        var response = _httpClient.GetAsync(_httpClient.BaseAddress + "Products/GetProduct/" + id).Result;

        if (response.IsSuccessStatusCode)
        {
            var data = response.Content.ReadAsStringAsync().Result;
            var product = JsonConvert.DeserializeObject<ProductVM>(data);

            return View(product);
        }
        else
        {
            TempData["ErrorMessage"] = "Something went wrong";
            return View();
        }
    }

    [HttpPost,ActionName("DeleteProduct")]
    public IActionResult DeleteProductConfirmed(int id)
    {
        HttpResponseMessage response = _httpClient.DeleteAsync(_httpClient.BaseAddress + "Products/DeleteProduct/" + id).Result;

        if(response.IsSuccessStatusCode)
        {
            return RedirectToAction(nameof(Index));
        }
        return View();
    }

}
