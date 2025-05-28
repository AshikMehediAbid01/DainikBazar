using DainikBazar.Domain.Entities;

namespace DainikBazar.Application.Managers.Interfaces;

public interface ICartManager
{
    Task<Cart> GetCartAsync( string userId );
    Task AddToCartAsync( int productId, string userId );
    Task UpdateCartAsync( int cartItemId, int quantity );
    Task RemoveFromCartAsync( int cartItemId );

}
