using DainikBazar.Domain.Models;

namespace DainikBazar.Domain.Interfaces;

public interface IOrderRepository
{
    Task<List<Order>> GetOrdersByCustomerIdAsync( string userId );
    Task<List<Order>> GetOrdersBySellerIdAsync( string sellerId );
    Task<Cart> GetCartByUserAsync( string userId );
    Task<List<Order>> GetAllOrdersAsync();
    Task<Order> GetOrderByIdAsync(int orderId);
    Task<Product> GetProductByIdAsync(int productId);
    Task<Cart> GetCartByIdAsync(int cartId);
    Task AddOrderAsync(Order order);
    Task SaveChangesAsync();
}
