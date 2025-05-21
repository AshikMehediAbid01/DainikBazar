using DainikBazar.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DainikBazar.WebApi.Controllers;

[Route( "api/[controller]/[action]" )]
[ApiController]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;
    public CartController( ICartService cartService )
    {
        _cartService = cartService;
    }

    [HttpGet]
    public async Task<IActionResult> GetCart()
    {
        try
        {
            var userId = ""; //var user = _httpContext.GetUserAsync( User );
            var cart = await _cartService.GetCartAsync( userId );
            return Ok( cart );
        }
        catch (Exception ex) 
        { 
            return BadRequest( ex.Message );
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddToCart(int productId)
    {
        try
        {
            var userId = ""; //var user = _httpContext.GetUserAsync( User );
            await _cartService.AddToCartAsync( productId, userId );
            return Ok( "Product Added to Cart" );
        }
        catch (InvalidOperationException ex) 
        { 
            return NotFound( ex.Message );
        }
        catch (Exception ex) {
            return StatusCode(500, $"Internal Server Error: {ex.Message}" );
        }
    }

    [HttpPost]
    public async Task<IActionResult> UpdateCart(int cartItemId, int quantity)
    {
        try
        {
            await _cartService.UpdateCartAsync( cartItemId, quantity );
            return Ok( "Quantity Updated" );
        }
        catch ( InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex) 
        {
            return StatusCode( 500, $"Internal Server Error: {ex.Message}" );
        }
    }

    [HttpPost]
    public async Task<IActionResult> RemoveCart( int cartItemId )
    {
        try
        {
            await _cartService.RemoveFromCartAsync( cartItemId );
            return Ok( "Cart Item Removed" );
        }
        catch (InvalidOperationException ex)
        {
            return NotFound( ex.Message );
        }
        catch (Exception ex)
        {
            return StatusCode( 500, $"Internal Server Error: {ex.Message}" );
        }
    }
}
