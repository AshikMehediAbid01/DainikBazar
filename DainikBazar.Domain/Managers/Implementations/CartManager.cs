using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DainikBazar.Domain.Managers.Interfaces;
using DainikBazar.Domain.Models;
using DainikBazar.Domain.Repository_Interfaces;

namespace DainikBazar.Domain.Managers.Implementations;

public class CartManager : ICartManager
{
    private readonly IGenericRepository _repository;
    private readonly ICartRepository _cartRepository;
    public CartManager(IGenericRepository repository, ICartRepository cartRepository)
    {
        _repository = repository;
        _cartRepository = cartRepository;
    }

    public async Task<Cart> GetCartAsync(string userId)
    {
        return await _cartRepository.GetCartAsync(userId);
    }
    public async Task AddToCartAsync(int productId, string userId)
    {
        var product = await _repository.GetByIdAsync<Product>(productId);
        if (product == null)
        {
            throw new InvalidOperationException("Product Not Found!");
        }
        var cart = await _cartRepository.GetCartAsync(userId);

        var cartItem = new CartItem
        {
            ProductId = productId,
            UnitPrice = product.Price,
            Quantity = 1
        };

        if (cart == null)
        {
            cart = new Cart { UserId = userId, CartStatus = "Active" };
            //cart.CartItems.Add( cartItem );
            await _repository.AddAsync<Cart>(cart);
        }
        var existingItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
        if (existingItem == null)
        {
            cart.CartItems.Add(cartItem);
        }
        else
        {
            existingItem.Quantity += 1;
        }
        await _repository.SaveChangesAsync();
    }
    public async Task UpdateCartAsync(int cartItemId, int quantity)
    {
        var cartItem = await _repository.GetByIdAsync<CartItem>(cartItemId);
        if (cartItem == null)
        {
            throw new InvalidOperationException("Product Not Found!");
        }
        cartItem.Quantity = quantity;
        await _repository.SaveChangesAsync();
    }
    public async Task RemoveFromCartAsync(int cartItemId)
    {
        var cartItem = await _repository.GetByIdAsync<CartItem>(cartItemId);
        if (cartItem == null)
        {
            throw new InvalidOperationException("Product Not Found!");
        }
        await _repository.DeleteAsync(cartItem);
        await _repository.SaveChangesAsync();
    }
}
