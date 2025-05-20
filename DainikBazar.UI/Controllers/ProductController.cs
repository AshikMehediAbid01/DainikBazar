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
}
