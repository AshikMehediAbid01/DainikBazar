using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DainikBazar.Domain.Entities;

namespace DainikBazar.Domain.Managers.Interfaces;

public interface ICartManager
{
    Task<Cart> GetCartAsync(string userId);
    Task AddToCartAsync(int productId, string userId);
    Task UpdateCartAsync(int cartItemId, int quantity);
    Task RemoveFromCartAsync(int cartItemId);

}
