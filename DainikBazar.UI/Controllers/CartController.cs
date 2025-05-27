using DainikBazar.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Newtonsoft.Json;
using System.Text;

namespace DainikBazar.UI.Controllers;

public class CartController : Controller
{
    Uri baseAddress = new Uri( "https://localhost:7155/api/" );
    private readonly HttpClient _httpClient;
    public CartController()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = baseAddress;
    }
    //private readonly HttpClient _httpClient;

    //public CartController( HttpClient httpClient )
    //{
    //    _httpClient = httpClient; // BaseAddress is already set — don't touch it here
    //}

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var userId = "226e5677-3d24-4518-aaf0-c6709fade9d5";
        var response = _httpClient.GetAsync(_httpClient.BaseAddress + $"Cart/GetCart/userId?userId={userId}").Result;
        var cartVM = new CartVM();
        if (response.IsSuccessStatusCode)
        {
            var viewData = response.Content.ReadAsStringAsync().Result;
            cartVM = JsonConvert.DeserializeObject<CartVM>( viewData );
        }
        else 
        {
            ViewBag.ErrorMessage = response.StatusCode.ToString();//"Internal Server Error" ;
        }
        return View(cartVM);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateCart(int cartItemId, int quantity)
    {
        //var value = new Dictionary<string, string>
        //{
        //    {"cartItemId", cartItemId.ToString()},
        //    {"quantity", quantity.ToString()}
        //};
        //var content = new FormUrlEncodedContent( value );
        var payload = new { cartItemId, quantity };
        var content = new StringContent( JsonConvert.SerializeObject( payload ), Encoding.UTF8, "application/json" );
        var response = _httpClient.PostAsync( _httpClient.BaseAddress + "Cart/UpdateCart", content ).Result;
        if (response.IsSuccessStatusCode)
        {
            var viewData = response.Content.ReadAsStringAsync().Result;
            //string message = JsonConvert.DeserializeObject<string>( viewData );
            ViewBag.Message = viewData;
        }
        else
        {
            ViewBag.ErrorMessage = response?.StatusCode.ToString();
        }
        return RedirectToAction("Index", "Cart");
    }

    [HttpPost]
    public async Task<IActionResult> RemoveFromCart(int cartItemId)
    {
        var payload = new { cartItemId };
        var content = new StringContent(JsonConvert.SerializeObject( payload ), Encoding.UTF8, "application/json");
        var response = _httpClient.DeleteAsync( _httpClient.BaseAddress + $"Cart/RemoveCart/{cartItemId}" ).Result;
        if (response.IsSuccessStatusCode)
        {
            var viewData = response.Content.ReadAsStringAsync().Result;
            //string message = JsonConvert.DeserializeObject<string>( viewData );
            ViewBag.Message = viewData;
        }
        else
        {
            ViewBag.ErrorMessage = response?.StatusCode.ToString();
        }
        return RedirectToAction( "Index", "Cart" );
    }
}
