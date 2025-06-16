using DainikBazar.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
namespace DainikBazar.UI.Controllers;

public class OrderController : Controller
{
    private readonly HttpClient _httpClient;
    Uri baseAddress = new("https://localhost:7155/api/");
    public OrderController(  )
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = baseAddress;
    }

    [HttpGet]
    public IActionResult Index()
    {
        ICollection<OrderVM> orderVM = [];
        var response = _httpClient.GetAsync( _httpClient.BaseAddress + "order/get-all-order" ).Result;
        if(response.IsSuccessStatusCode)
        {
            var viewData = response.Content.ReadAsStringAsync().Result;
            orderVM = JsonConvert.DeserializeObject<ICollection<OrderVM>>( viewData );
        }
        else
        {
            ViewBag.ErrorMessage = response.StatusCode.ToString();
        }
        return View(orderVM);
    }

    [HttpGet]
    public IActionResult OrderHistory()
    {
        ICollection<OrderVM> orderVM = [];
        var userId = "226e5677-3d24-4518-aaf0-c6709fade9d5";
        var response = _httpClient.GetAsync(_httpClient.BaseAddress + $"order/get-customer-orders/{userId}").Result;
        if (response.IsSuccessStatusCode)
        {
            var viewData = response.Content.ReadAsStringAsync().Result;
            orderVM = JsonConvert.DeserializeObject<ICollection<OrderVM>>(viewData);
        }
        else
        {
            ViewBag.ErrorMessage = response.StatusCode.ToString();
        }
        return View( orderVM );
    }

    [HttpGet]
    public IActionResult GetOrderBySeller()
    {
        ICollection<OrderVM> orderVM = [];
        var sellerId = "";
        var response = _httpClient.GetAsync( _httpClient.BaseAddress + $"order/get-seller-orders/{sellerId}" ).Result;
        if (response.IsSuccessStatusCode)
        {
            var viewData = response.Content.ReadAsStringAsync().Result;
            orderVM = JsonConvert.DeserializeObject<ICollection<OrderVM>>( viewData );
        }
        else
        {
            ViewBag.ErrorMessage = response.StatusCode.ToString();
        }
        return View(orderVM);
    }

    [HttpGet]
    public IActionResult Checkout()
    {
        var userId = "226e5677-3d24-4518-aaf0-c6709fade9d5";
        var orderVM = new OrderVM();
        var response = _httpClient.GetAsync(_httpClient.BaseAddress + $"order/checkout/{userId}").Result;
        if (response.IsSuccessStatusCode)
        {
            var viewData = response.Content.ReadAsStringAsync().Result;
            orderVM = JsonConvert.DeserializeObject<OrderVM>( viewData );
        }
        else
        {
            var errorMessage = response.Content.ReadAsStringAsync().Result;
            ViewBag.ErrorMessage = response.StatusCode.ToString() + errorMessage;
        }
        return View( orderVM );
    }

    [HttpPost]
    public IActionResult BuyNow( int productId, int quantity = 1 )
    {
        var userId = "226e5677-3d24-4518-aaf0-c6709fade9d5";
        var orderVM = new OrderVM();
        var payload = new { productId, quantity, userId };
        var content = new StringContent( JsonConvert.SerializeObject( payload ), Encoding.UTF8, "application/json" );

        var response = _httpClient.PostAsync( _httpClient.BaseAddress + "order/buy-now", content ).Result;
        var viewData = response.Content.ReadAsStringAsync().Result;
        if (response.IsSuccessStatusCode)
        {
            orderVM = JsonConvert.DeserializeObject<OrderVM> ( viewData );
        }
        else
        {
            ViewBag.ErrorMessage = response.StatusCode.ToString() + " " + viewData;
        }
        return View( orderVM );
    }

    [HttpPost]
    public IActionResult ManageOrders( int orderId, string orderHistory )
    {
        var payload = new { orderId, orderHistory };
        var content = new StringContent( JsonConvert.SerializeObject( payload ), Encoding.UTF8, "application/json" );
        
        var response = _httpClient.PostAsync(_httpClient.BaseAddress + "order/manage-order", content).Result;
        if (response.IsSuccessStatusCode) 
        {
            var viewData = response.Content.ReadAsStringAsync().Result;
            //orderVM = JsonConvert.DeserializeObject<OrderVM>( viewData );
            ViewBag.Message = viewData;
        }
        else
        {
            ViewBag.ErrorMessage = response.StatusCode.ToString();
        }
        return RedirectToAction("Index", "Order");
    }

    [HttpPost]
    public IActionResult PlaceOrder( OrderVM orderVM ) 
    {
        //ModelState.Remove( "UserId" );
        ModelState.Remove( "OrderHistory" );
        if(!ModelState.IsValid)
        {
            return View( orderVM );
        }

        var content = new StringContent( JsonConvert.SerializeObject( orderVM ), Encoding.UTF8, "application/json" );
        var response = _httpClient.PostAsync( _httpClient.BaseAddress + "order/place-order", content ).Result;
        var viewData = response.Content.ReadAsStringAsync().Result;
        if (response.IsSuccessStatusCode) 
        {
            ViewBag.Message = viewData;
        }
        else
        {
            ViewBag.ErrorMessage = response.StatusCode.ToString() + " " + viewData;
        }
        return RedirectToAction( "Index", "Home" );
    }
}
