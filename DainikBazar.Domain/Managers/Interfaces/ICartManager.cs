using DainikBazar.Domain.Models;

namespace DainikBazar.Domain.Managers.Interfaces;

public interface ICartManager
{
    Task<Cart> GetCartAsync( string userId );
    Task AddToCartAsync( int productId, string userId );
    Task UpdateCartAsync( int cartItemId, int quantity );
    Task RemoveFromCartAsync( int cartItemId );
    Task MakeCartStatusActive( string userId );
}
