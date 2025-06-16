using DainikBazar.Domain.Models;

namespace DainikBazar.Domain.Interfaces;

public interface ICartRepository
{
    Task<Cart> GetCartAsync(string userId);
    Task MakeProcessingCartActiveAsync(string userId);
    Task<Product> GetProductByIdAsync(int productId);
    Task AddCartAsync(Cart cart);
    Task<CartItem> GetCartItemByIdAsync(int cartItemId);
    Task UpdateAsync(Cart cart);
    Task UpdateCartAsync(CartItem cartItem);
    Task DeleteAsync(int cartItemId);
    Task SaveChangesAsync();
}
