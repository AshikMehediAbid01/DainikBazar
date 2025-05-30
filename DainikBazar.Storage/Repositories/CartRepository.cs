using AutoMapper;
using DainikBazar.Domain.Models;
using DainikBazar.Domain.Interfaces;
using DainikBazar.Storage.Data;
using Microsoft.EntityFrameworkCore;

namespace DainikBazar.Storage.Repositories;

public class CartRepository(AppDbContext dbContext, IMapper _mapper) : ICartRepository
{
    public async Task<Cart> GetCartAsync( string userId )
    {
        
        var storageModel =  await dbContext.Carts
            .Include( c => c.CartItems )
            .ThenInclude( ci => ci.Product )
            .FirstOrDefaultAsync( c => c.UserId == userId && c.CartStatus == "Active" );

        var domainModel = _mapper.Map<Cart>(storageModel);
        return domainModel;

    }
}
