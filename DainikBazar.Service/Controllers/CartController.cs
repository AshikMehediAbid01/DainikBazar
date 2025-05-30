using AutoMapper;
using DainikBazar.Domain.Managers.Interfaces;
using DainikBazar.Service.Models;
using Microsoft.AspNetCore.Mvc;

namespace DainikBazar.Service.Controllers;

[Route( "api/[controller]/[action]" )]
[ApiController]
public class CartController : ControllerBase
{
    private readonly ICartManager _cartService;
    private readonly IMapper _mapper;
    public CartController( ICartManager cartService, IMapper mapper )
    {
        _cartService = cartService;
        _mapper = mapper;
    }

    [HttpGet("userId")]
    public async Task<IActionResult> GetCart( string userId )
    {
        try
        {
            //string userId = ""; //var user = _httpContext.GetUserAsync( User );
            var cart = await _cartService.GetCartAsync( userId );
            if (cart == null)
            {
                return Ok( "No Item in the Cart." );
            }
            var cartDto = _mapper.Map<Cart>(cart);
            return Ok( cartDto );
        }
        catch (Exception ex) 
        { 
            return BadRequest( ex.Message );
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddToCart(string userId, int productId)
    {
        try
        {
            //var userId = ""; //var user = _httpContext.GetUserAsync( User );
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
    public async Task<IActionResult> UpdateCart( [FromBody] UpdateProductDto updateProduct ) //int cartItemId, int quantity
    {
        try
        {
            await _cartService.UpdateCartAsync( updateProduct.cartItemId, updateProduct.quantity );
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

    [HttpDelete("{cartItemId}")]
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
