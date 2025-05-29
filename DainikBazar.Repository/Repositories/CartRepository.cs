
using DainikBazar.Domain.Entities;
using DainikBazar.Domain.Repository_Interfaces;
using DainikBazar.Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace DainikBazar.Repository.Repositories;

public class CartRepository: ICartRepository
{
    private readonly AppDbContext _dbContext;
    public CartRepository(AppDbContext dbContext )
    {
        _dbContext = dbContext;
    }
    public async Task<Cart> GetCartAsync( string userId )
    {
        return await _dbContext.Carts
            .Include( c => c.CartItems )
            .ThenInclude( ci => ci.Product )
            .FirstOrDefaultAsync( c => c.UserId == userId && c.CartStatus == "Active" );

    }
}
