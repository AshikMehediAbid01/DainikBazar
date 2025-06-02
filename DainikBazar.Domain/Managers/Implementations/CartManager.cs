using DainikBazar.Domain.Interfaces;
using DainikBazar.Domain.Managers.Interfaces;
using DainikBazar.Domain.Models;

namespace DainikBazar.Domain.Managers.Implementations;

public class CartManager(IGenericRepository repository, ICartRepository cartRepository) : ICartManager
{

    public async Task<Cart> GetCartAsync(string userId)
    {
        return await cartRepository.GetCartAsync( userId );
    }
    public async Task AddToCartAsync( int productId, string userId )
    {
        var product = await repository.GetByIdAsync<Product>( productId );
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

        if (cart == null)
        {
            cart = new Cart { UserId = userId, CartStatus = "Active" };

            await repository.AddAsync<Cart>( cart );
        }
        var existingItem = cart.CartItems.FirstOrDefault( ci => ci.ProductId == productId );
        if (existingItem == null)
        {
            cart.CartItems.Add( cartItem );
        }
        else
        {
            existingItem.Quantity += 1;
        }

        await repository.SaveChangesAsync();
    }
    public async Task UpdateCartAsync( int cartItemId, int quantity )
    {
        var cartItem = await repository.GetByIdAsync<CartItem>( cartItemId );
        if ( cartItem == null )
        {
            throw new InvalidOperationException( "Product Not Found!" );
        }
        cartItem.Quantity = quantity;
        await repository.SaveChangesAsync();
    }
    public async Task RemoveFromCartAsync( int cartItemId )
    {
        var cartItem = await repository.GetByIdAsync<CartItem>( cartItemId );
        if (cartItem == null)
        {
            throw new InvalidOperationException( "Product Not Found!" );
        }
        await repository.DeleteAsync( cartItem );
        await repository.SaveChangesAsync();
    }

    public async Task MakeCartStatusActive(string userId)
    {
        Cart cart = await cartRepository.GetProcessingCartAsync( userId );
        cart.CartStatus = "Active";
        await repository.SaveChangesAsync();
    }
}
