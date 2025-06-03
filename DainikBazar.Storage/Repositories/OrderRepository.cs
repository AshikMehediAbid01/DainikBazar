using DainikBazar.Domain.Interfaces;
using DainikBazar.Domain.Models;
using DainikBazar.Storage.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace DainikBazar.Storage.Repositories;

public class OrderRepository( AppDbContext dbContext, IMapper mapper ) : IOrderRepository
{
    
    public async Task<List<Order>> GetOrdersByCustomerIdAsync( string userId )
    {
        var orderList = await dbContext.Orders
            //.Include( o => o.Cart )
           // .ThenInclude( c => c.CartItems )
           // .ThenInclude( ci => ci.Product )
            .Where( o => o.UserId == userId )
            .OrderByDescending(o => o.OrderDate )
            .ToListAsync();
        var domainOrderList = mapper.Map<List<Order>>( orderList );
        return domainOrderList;
    }
    public async Task<List<Order>> GetOrdersBySellerIdAsync( string sellerId )
    {
        var orderList = await dbContext.Orders
           // .Include( o => o.Cart )
           // .ThenInclude( c => c.CartItems )
           // .ThenInclude( ci => ci.Product )
            //.Where( p => p.SellerId == sellerId )
            .OrderByDescending (o => o.OrderDate )
            .ToListAsync();
        var domainOrderList = mapper.Map<List<Order>>( orderList );
        return domainOrderList;
    }
    public async Task<Cart> GetCartByUserAsync( string userId )
    {
        var cart = await dbContext.Carts
            .Include( c => c.CartItems )
            .ThenInclude( ci => ci.Product )
            .FirstOrDefaultAsync( c => c.UserId == userId && c.CartStatus == "Active" );
        var domainCart = mapper.Map<Cart>( cart );
        return domainCart;
    }
}
