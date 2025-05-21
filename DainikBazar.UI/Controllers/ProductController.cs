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
}
