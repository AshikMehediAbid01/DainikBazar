using DainikBazar.Domain.Interfaces;
using DainikBazar.Domain.Managers.Interfaces;
using DainikBazar.Domain.Models;
using Microsoft.Extensions.Logging;

namespace DainikBazar.Domain.Managers.Implementations;

public class OrderManager(
        IOrderRepository orderRepository,
        ILogger<OrderManager> logger
    ) : IOrderManager
{
    public async Task<List<Order>> GetAllOrdersAsync()
    {
        return await orderRepository.GetAllOrdersAsync();
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
        try
        {
            await orderRepository.ManageOrdersAsync(orderId, orderHistory);
            await orderRepository.SaveChangesAsync();
        }
        catch(Exception ex) 
        { 
            throw new InvalidOperationException("DB updated Failed" + ex.Message);
        }
    }
    public async Task<Order> GetCartByUserAsync(string userId )
    {
        Cart cart = await orderRepository.GetCartByUserAsync( userId );
        if(cart == null)
        {
            throw new InvalidOperationException( "Cart not Found1" );
        }

        logger.LogInformation( $"CartId: {cart.Id}, SubtotalPrice: {cart.TotalPrice}, User: {cart.UserId}" );
        Order order = new ()
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
        try
        {
            await orderRepository.UpdateCartStausAsync(cartId, cartStatus);
            await orderRepository.SaveChangesAsync();
        }
        catch(Exception ex)
        {
            throw new InvalidOperationException(ex.ToString());
        }
    }
    public async Task PlaceOrderAsync(Order order)
    {
        try
        {
            order.OrderHistory = "Pending";
            await orderRepository.AddOrderAsync( order );
            await orderRepository.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(ex.ToString());
        }
    }

    public async Task<Order> BuyNowAsync(int productId, int quantity, string userId)
    {
        Product product = await orderRepository.GetProductByIdAsync( productId );
        if(product == null)
        {
            throw new InvalidOperationException( "Product not found!" );
        }
        Order order = new ()
        {
            UserId = userId,
            ProductId = productId,
            Quantity = quantity,
            UnitPrice = product.Price,
            SubtotalPrice = quantity * product.Price,
            DeliveryCharge = 80,
        };
        return order;
    }

}
