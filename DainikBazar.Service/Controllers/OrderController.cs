using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using DainikBazar.Domain.Managers.Interfaces;
using Domains = DainikBazar.Domain.Models;
using DainikBazar.Service.Models;
using System.Data.Common;

namespace DainikBazar.Service.Controllers;

[Route( "api/order" )]
[ApiController]
public class OrderController( IOrderManager orderService, IMapper mapper ) : ControllerBase
{
    [HttpGet]
    //Getting All Orders For Management By Admin 
    [Route("get-all-order")]
    public async Task<IActionResult> GetAllOrders()
    {
        try
        {
            var ordersList = await orderService.GetAllOrdersAsync();
            if (ordersList != null)
            {
                var orderDtoList = mapper.Map<List<Order>>( ordersList );
                return Ok( orderDtoList );
            }
            return Ok( "No Order Yet!" );
        }
        catch (DbException ex)
        {
            return StatusCode( 500, ex.Message );
        }
        catch (Exception ex)
        {
            return StatusCode(404, ex.Message);
        }
    }

    [HttpGet]
    [Route("get-customer-orders/{userId}")]
    public async Task<IActionResult> GetOrderByCustomerId(string userId)
    {
        try 
        {
            var ordersList = await orderService.GetOrdersByCustomerIdAsync(userId);
            if (ordersList != null)
            {
                var orderDtoList = mapper.Map<List<Order>>( ordersList );
                return Ok( orderDtoList );
            }
            return Ok( "You Have No Order Placed Yet" );
        }
        catch (DbException ex)
        {
            return StatusCode( 500, ex.Message );
        }
        catch (Exception ex)
        {
            return StatusCode(404, ex.Message);
        }
    }

    [HttpGet]
    [Route("get-seller-orders/{sellerId}")]
    public async Task<IActionResult> GetAllOrderBySeller(string sellerId)
    {
        try
        { 
            var ordersList = await orderService.GetOrdersBySellerIdAsync( sellerId );
            if (ordersList != null)
            {
                var orderDtoList = mapper.Map<List<Order>>( ordersList );
                return Ok( orderDtoList );
            }
            
            return Ok( "No Order Yet!" );
        }
        catch (DbException ex)
        {
            return StatusCode( 500, ex.Message );
        }
        catch (Exception ex)
        {
            return StatusCode(404, ex.Message);
        }
    }

    [HttpGet("checkout/{userId}")]
    public async Task<IActionResult> Checkout(string userId)
    {
        try 
        {
            var order = await orderService.GetCartByUserAsync( userId );
            if (order.CartId == 0)
            {
                order.CartId = null;
            }
            if (order.ProductId == 0)
            {
                order.ProductId = null;
            }
            if (order.CartId.HasValue)
            {
                await orderService.UpdateCartStausAsync( order.CartId.Value, "Processing" );
            }
            var orderDto = mapper.Map<Order>( order );
            return Ok( orderDto );
        }
        catch (DbException ex)
        {
            return StatusCode( 500, ex.Message );
        }
        catch (Exception ex)
        {
            return StatusCode(404, ex.Message);
        }
    }

    [HttpPost]
    [Route("buy-now")]
    public async Task<IActionResult> BuyNow( [FromBody] BuyNow buyNow) //int productId, int quantity
    {
        try 
        {
            var order = await orderService.BuyNowAsync(buyNow.ProductId, buyNow.Quantity, buyNow.UserId);
            if (order.CartId == 0)
            {
                order.CartId = null;
            }
            if (order.ProductId == 0)
            {
                order.ProductId = null;
            }
            var orderDto = mapper.Map<Order>(order);
            return Ok( orderDto );
        }
        catch (DbException ex)
        {
            return StatusCode( 500, ex.Message );
        }
        catch (Exception ex)
        {
            return StatusCode(404, ex.Message);
        }
    }

    [HttpPost]
    [Route("manage-order")]
    public async Task<IActionResult> ManageOrders( [FromBody] ManageOrders manageOrder) //int orderId, string orderStatus
    {
        try 
        {
            await orderService.ManageOrdersAsync( manageOrder.OrderId, manageOrder.OrderHistory );
            return Ok("Status Updated.");
        }
        catch (DbException ex)
        {
            return StatusCode( 500, ex.Message );
        }
        catch (Exception ex)
        {
            return StatusCode(404, ex.Message);
        }
    }

    [HttpPost]
    [Route("place-order")]
    public async Task<IActionResult> PlaceOrder([FromBody] Order orderDto)
    {
        try
        { 
            var order = mapper.Map<Domains.Order>( orderDto );
            if (orderDto.CartId == 0)
            {
                order.CartId = null;
            }
            if (orderDto.ProductId == 0)
            {
                order.ProductId = null;
            }

            await orderService.PlaceOrderAsync( order );
            if (order.CartId.HasValue)
            {
                await orderService.UpdateCartStausAsync( order.CartId.Value, "InActive" );
            }
            return Ok("Your Order Is Placed Successfully");
        }
        catch (DbException ex)
        {
            return StatusCode( 500, ex.InnerException?.Message ?? ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode( 404, ex.InnerException?.Message ?? ex.Message);
        }
    }
}
