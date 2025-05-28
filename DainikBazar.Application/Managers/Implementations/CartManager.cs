

using DainikBazar.Application.Common.Repository_Interfaces;
using DainikBazar.Application.Managers.Interfaces;
using DainikBazar.Domain.Entities;

namespace DainikBazar.Application.Managers.Implementations;

public class CartManager: ICartManager
{
    private readonly IRepository _repository;
    private readonly ICartRepository _cartRepository;
    public CartManager( IRepository repository, ICartRepository cartRepository )
    {
        _repository = repository;
        _cartRepository = cartRepository;
    }

    public async Task<Cart> GetCartAsync(string userId)
    {
        return await _cartRepository.GetCartAsync( userId );
    }
    public async Task AddToCartAsync( int productId, string userId )
    {
        var product = await _repository.GetByIdAsync<Product>( productId );
        if ( product == null ) 
        {
            throw new InvalidOperationException( "Product Not Found!" );
        }
        var cart = await _cartRepository.GetCartAsync(userId);

        var cartItem = new CartItem
        {
            ProductId = productId,
            UnitPrice = product.Price,
            Quantity = 1
        };

        if (cart == null)
        {
            cart = new Cart { UserId = userId, CartStatus = "Active" };
            //cart.CartItems.Add( cartItem );
            await _repository.AddAsync<Cart>( cart );
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
        await _repository.SaveChangesAsync();
    }
    public async Task UpdateCartAsync( int cartItemId, int quantity )
    {
        var cartItem = await _repository.GetByIdAsync<CartItem>( cartItemId );
        if ( cartItem == null )
        {
            throw new InvalidOperationException( "Product Not Found!" );
        }
        cartItem.Quantity = quantity;
        await _repository.SaveChangesAsync();
    }
    public async Task RemoveFromCartAsync( int cartItemId )
    {
        var cartItem = await _repository.GetByIdAsync<CartItem>( cartItemId );
        if (cartItem == null)
        {
            throw new InvalidOperationException( "Product Not Found!" );
        }
        await _repository.DeleteAsync( cartItem );
        await _repository.SaveChangesAsync();
    }
}
