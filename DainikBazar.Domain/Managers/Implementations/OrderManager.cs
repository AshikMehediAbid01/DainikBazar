using DainikBazar.Domain.Interfaces;
using DainikBazar.Domain.Managers.Interfaces;
using DainikBazar.Domain.Models;
using Microsoft.Extensions.Logging;

namespace DainikBazar.Domain.Managers.Implementations;

public class OrderManager(
        IOrderRepository orderRepository,
        IGenericRepository repository,
        ILogger<OrderManager> logger
    ) : IOrderManager
{
    public async Task<List<Order>> GetAllOrdersAsync()
    {
        return await repository.GetAllAsync<Order>();
    }
    public async Task<List<Order>> GetOrdersByCustomerIdAsync( string userId )
    {
        return await orderRepository.GetOrdersByCustomerIdAsync(userId);
    }
    public async Task<List<Order>> GetOrdersBySellerIdAsync( string sellerId )
    {
        return await orderRepository.GetOrdersByCustomerIdAsync(sellerId);
    }
    public async Task ManageOrdersAsync( int orderId, string orderHistory )
    {
        Order order = await repository.GetByIdAsync<Order>( orderId);
        if( order == null)
        {
            throw new InvalidOperationException( "Order not found!" );
        }
        order.OrderHistory = orderHistory;
        await repository.SaveChangesAsync();
    }
    public async Task<Order> GetCartByUserAsync(string userId )
    {
        Cart cart = await orderRepository.GetCartByUserAsync( userId );
        if(cart == null)
        {
            throw new InvalidOperationException( "Cart not Found1" );
        }

        logger.LogInformation( $"CartId: {cart.Id}, SubtotalPrice: {cart.TotalPrice}, User: {cart.UserId}" );
        Order order = new Order
        {
            CartId = cart.Id,
            SubtotalPrice = cart.TotalPrice,
            DeliveryCharge = 80,
            UserId = cart.UserId,
        };

        logger.LogInformation($"DeliveryCharge: {order.DeliveryCharge}, TotalPrice: {order.TotalPrice}");
        return order;
    }
    public async Task UpdateCartStausAsync( int cartId, string cartStatus )
    {
        Cart cart = await repository.GetByIdAsync<Cart>( cartId );
        if(cart == null || cart.CartStatus == "InActive")
        {
            throw new InvalidOperationException( "Cart not available!" );
        }
        cart.CartStatus = cartStatus;
        await repository.SaveChangesAsync();
    }
    public async Task PlaceOrderAsync(Order order)
    {
        try
        {
            order.OrderHistory = "Pending";
            await repository.AddAsync<Order>( order );
            await repository.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException( ex.Message );
        }
    }

    public async Task<Order> BuyNowAsync(int productId, int quantity)
    {
        Product product = await repository.GetByIdAsync<Product>( productId );
        if(product == null)
        {
            throw new InvalidOperationException( "Product not found!" );
        }
        Order order = new Order
        {
            ProductId = productId,
            Quantity = quantity,
            UnitPrice = product.Price,
            SubtotalPrice = quantity * product.Price,
            DeliveryCharge = 80,
        };
        return order;
    }

}
