
using DainikBazar.Domain.Models;

namespace DainikBazar.Domain.Managers.Interfaces;

public interface IOrderManager
{
    Task<List<Order>> GetAllOrdersAsync();
    Task<List<Order>> GetOrdersByCustomerIdAsync(string userId);
    Task<List<Order>> GetOrdersBySellerIdAsync(string sellerId);
    Task ManageOrdersAsync( int orderId, string orderStatus );
    Task UpdateCartStausAsync( int cartId, string cartStatus );
    Task PlaceOrderAsync( Order order);
    Task<Order> BuyNowAsync( int productId, int quantity );
    Task<Order> GetCartByUserAsync( string userId );
}
