using DainikBazar.Domain.Interfaces;
using DainikBazar.Domain.Managers.Interfaces;
using DainikBazar.Domain.Models;

namespace DainikBazar.Domain.Managers.Implementations;

public class CartManager(ICartRepository cartRepository) : ICartManager
{

    public async Task<Cart> GetCartAsync(string userId)
    {
        return await cartRepository.GetCartAsync( userId );
    }
    public async Task AddToCartAsync( int productId, string userId )
    {
        var product = await cartRepository.GetProductByIdAsync( productId );
        if ( product == null ) 
        {
            throw new InvalidOperationException( "Product Not Found!" );
        }
        var cart = await cartRepository.GetCartAsync(userId);

        var cartItem = new CartItem
        {
            ProductId = productId,
            UnitPrice = product.Price,
            Quantity = 1
        };

        if(cart == null)
        {
            cart = new Cart { UserId = userId, CartStatus = "Active", CartItems = [] };
            cart.CartItems.Add(cartItem);
            cart.ActualPrice = cart.CartItems.Sum(item => item.Quantity * item.UnitPrice);
            await cartRepository.AddCartAsync(cart);
            await cartRepository.SaveChangesAsync();
        }
        else
        {
            var existingItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
            if(existingItem == null)
            {
                cart.CartItems.Add(cartItem);
            }
            else
            {
                existingItem.Quantity += 1;
            }
            cart.ActualPrice = cart.CartItems.Sum(item => item.Quantity * item.UnitPrice);
            await cartRepository.UpdateAsync(cart);
            await cartRepository.SaveChangesAsync();
        }
    }
    public async Task UpdateCartAsync( int cartItemId, int quantity )
    {
        var cartItem = new CartItem { Id = cartItemId, Quantity = quantity};
        try
        {
            await cartRepository.UpdateCartAsync(cartItem);
            await cartRepository.SaveChangesAsync();
        }
        catch(Exception ex)
        {
            throw new InvalidOperationException($"DB Update failed: {ex.InnerException?.Message}", ex);
        }
    }
    public async Task RemoveFromCartAsync( int cartItemId )
    {
        await cartRepository.DeleteAsync( cartItemId );
        await cartRepository.SaveChangesAsync();
    }

    public async Task MakeCartStatusActive(string userId)
    {
        await cartRepository.MakeProcessingCartActiveAsync( userId );
        await cartRepository.SaveChangesAsync();
    }
}
