using AutoMapper;
using DainikBazar.Service.Models;
using DainikBazar.Domain.Managers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DainikBazar.Service.Controllers;

[Route( "api/cart" )]
[ApiController]
public class CartController(ICartManager cartService, IMapper mapper) : ControllerBase
{
    [HttpGet]
    [Route("get-cart/{userId}")]
    public async Task<IActionResult> GetCart(string userId)
    {
        try
        {
            //string userId = ""; //var user = _httpContext.GetUserAsync( User );
            var cart = await cartService.GetCartAsync(userId);
            if (cart == null)
            {
                return Ok( "No Item in the Cart." );
            }
            var cartDto = mapper.Map<Cart>( cart );
            return Ok( cartDto );
        }
        catch (Exception ex)
        {
            return StatusCode( 500, ex.Message );
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="addCart"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("add-to-cart")]
    public async Task<IActionResult> AddToCart( [FromBody] AddCart addCart )//string userId, int productId
    {
        try
        {
            //var userId = ""; //var user = _httpContext.GetUserAsync( User );
            await cartService.AddToCartAsync( addCart.ProductId, addCart.UserId );
            return Ok( "Product Added to Cart" );
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

    [HttpPost]
    [Route("update-cart")]
    public async Task<IActionResult> UpdateCart([FromBody] UpdateCart updateProduct ) //int cartItemId, int quantity
    {
        try
        {
            await cartService.UpdateCartAsync( updateProduct.CartItemId, updateProduct.Quantity );
            return Ok( "Quantity Updated" );
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

    [HttpDelete( "remove-cart/{cartItemId}" )]
    public async Task<IActionResult> RemoveCart( int cartItemId )
    {
        try
        {
            await cartService.RemoveFromCartAsync( cartItemId );
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
