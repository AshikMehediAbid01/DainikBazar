using AutoMapper;
using DainikBazar.Domain.Interfaces;
using DainikBazar.Domain.Models;
using DainikBazar.Storage.Data;
using Microsoft.EntityFrameworkCore;

namespace DainikBazar.Storage.Repositories;

public class CartRepository( AppDbContext dbContext , IMapper mapper) : ICartRepository
{
    
    public async Task<Cart> GetCartAsync( string userId )
    {
        var cart = await dbContext.Carts
            .Include( c => c.CartItems )
            .ThenInclude( ci => ci.Product )
            .FirstOrDefaultAsync( c => c.UserId == userId && c.CartStatus == "Active" );
        var domainCart = mapper.Map<Cart>( cart );
        return domainCart;
    }
    public async Task<Cart> GetProcessingCartAsync( string userId )
    {
        var cart = await dbContext.Carts
            .FirstOrDefaultAsync( c => c.UserId == userId && c.CartStatus == "Processing" );
        var domainCart = mapper.Map<Cart>( cart );
        return domainCart;
    }
}
