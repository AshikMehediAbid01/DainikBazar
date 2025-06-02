using DainikBazar.Domain.Interfaces;
using Domains = DainikBazar.Domain.Models;
using DainikBazar.Storage.Models;
using DainikBazar.Storage.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace DainikBazar.Storage.Repositories;

public class OrderRepository( AppDbContext dbContext, IMapper mapper ) : IOrderRepository
{
    
    public async Task<List<Domains.Order>> GetOrdersByCustomerIdAsync( string userId )
    {
        var orderList = await dbContext.Orders
            .Include( o => o.Cart )
            .ThenInclude( c => c.CartItems )
            .ThenInclude( ci => ci.Product )
            .Where( o => o.UserId == userId )
            .OrderByDescending(o => o.OrderDate )
            .ToListAsync();
        var domainOrderList = mapper.Map<List<Domains.Order>>( orderList );
        return domainOrderList;
    }
    public async Task<List<Domains.Order>> GetOrdersBySellerIdAsync( string sellerId )
    {
        var orderList = await dbContext.Orders
            .Include( o => o.Cart )
            .ThenInclude( c => c.CartItems )
            .ThenInclude( ci => ci.Product )
            //.Where( p => p.SellerId == sellerId )
            .OrderByDescending (o => o.OrderDate )
            .ToListAsync();
        var domainOrderList = mapper.Map<List<Domains.Order>>( orderList );
        return domainOrderList;
    }
    public async Task<Domains.Cart> GetCartByUserAsync( string userId )
    {
        var cart = await dbContext.Carts
            .Include( c => c.CartItems )
            .ThenInclude( ci => ci.Product )
            .FirstOrDefaultAsync( c => c.UserId == userId && c.CartStatus == "Active" );
        var domainCart = mapper.Map<Domains.Cart>( cart );
        return domainCart;
    }
    public async Task<List<Domains.Order>> GetAllOrdersAsync()
    {
        var orderList = await dbContext.Orders.ToListAsync();
        return mapper.Map<List<Domains.Order>>( orderList );
    }
    public async Task<Domains.Order> GetOrderByIdAsync(int orderId)
    {
        var order = await dbContext.Orders.FirstOrDefaultAsync(o=> o.Id == orderId);
        return mapper.Map<Domains.Order>( order );
    }
    public async Task<Domains.Product> GetProductByIdAsync(int productId)
    {
        var product = await dbContext.Products.FirstOrDefaultAsync(p => p.ProductId == productId);
        return mapper.Map<Domains.Product>(product);
    }
    public async Task<Domains.Cart> GetCartByIdAsync(int cartId)
    {
        var cart = await dbContext.Carts.FirstOrDefaultAsync(c => c.Id == cartId);
        return mapper.Map<Domains.Cart>(cart);
    }
    public async Task AddOrderAsync(Domains.Order order)
    {
        var entityOrder = mapper.Map<Order>( order );
        await dbContext.Orders.AddAsync( entityOrder );
    }
    public async Task SaveChangesAsync()
    {
        await dbContext.SaveChangesAsync();
    }
}
