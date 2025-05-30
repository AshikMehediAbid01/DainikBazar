using DainikBazar.Domain.Models;

namespace DainikBazar.Domain.Interfaces;

public interface IOrderRepository
{
    Task<List<Order>> GetOrdersByCustomerIdAsync( string userId );
    Task<List<Order>> GetOrdersBySellerIdAsync( string sellerId );
    Task<Cart> GetCartByUserAsync( string userId );
}
