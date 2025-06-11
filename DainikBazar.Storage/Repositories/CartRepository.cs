using AutoMapper;
using DainikBazar.Domain.Interfaces;
using Domains = DainikBazar.Domain.Models;
using DainikBazar.Storage.Models;
using DainikBazar.Storage.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Threading.Tasks;

namespace DainikBazar.Storage.Repositories;

public class CartRepository( AppDbContext dbContext , IMapper mapper) : ICartRepository
{
    
    public async Task<Domains.Cart> GetCartAsync( string userId )
    {
        var cart = await dbContext.Carts
            .Include( c => c.CartItems )
            .ThenInclude( ci => ci.Product )
            .FirstOrDefaultAsync( c => c.UserId == userId && c.CartStatus == "Active" );
        var domainCart = mapper.Map<Domains.Cart>( cart );
        return domainCart;
    }
    public async Task MakeProcessingCartActiveAsync( string userId )
    {
        var cart = await dbContext.Carts
            .FirstOrDefaultAsync( c => c.UserId == userId && c.CartStatus == "Processing" );
        if(cart == null)
        {
            throw new InvalidOperationException("Cart Not Found");
        }
        cart.CartStatus = "Active";
        //var domainCart = mapper.Map<Domains.Cart>( cart );
        //return domainCart;
    }
    public async Task< Domains.Product> GetProductByIdAsync(int productId)
    {
        var product = await dbContext.Products
            .FirstOrDefaultAsync ( p => p.ProductId == productId );
        return mapper.Map<Domains.Product>( product );
    }
    public async Task AddCartAsync(Domains.Cart cart)
    {
        var cartEntity = mapper.Map<Cart>( cart );
        await dbContext.Carts.AddAsync( cartEntity );
    }
    public async Task<Domains.CartItem> GetCartItemByIdAsync(int cartItemId)
    {
        var cartItem = await dbContext.CartItems
            .FirstOrDefaultAsync(ci => ci.Id == cartItemId);
        return mapper.Map<Domains.CartItem>(cartItem);
    }
    public async Task DeleteAsync(int cartItemId)
    {
        var cartItem = await dbContext.CartItems
            .FirstOrDefaultAsync(ci => ci.Id == cartItemId);
        if(cartItem == null)
        {
            throw new InvalidOperationException("Product Not Found!");
        }
        dbContext.CartItems.Remove(cartItem);
    }
    public async Task UpdateAsync(Domains.Cart cart)
    {
        var entityCart = await dbContext.Carts
            .Include(c => c.CartItems)
            .ThenInclude(ci => ci.Product)
            .FirstOrDefaultAsync(c => c.Id == cart.Id);
        if(cart == null)
        {
            throw new InvalidOperationException("Cart not available!");
        }
        mapper.Map(cart, entityCart);
    }
    public async Task UpdateCartAsync(Domains.CartItem cartItem)
    {
        var entityCartItem = await dbContext.CartItems
            .FirstOrDefaultAsync(c => c.Id == cartItem.Id);
        if(entityCartItem == null)
        {
            throw new InvalidOperationException("Invalid CartItem!");
        }
        entityCartItem.Quantity = cartItem.Quantity;
    }
    public async Task SaveChangesAsync()
    {
        await dbContext.SaveChangesAsync();
    }
}
