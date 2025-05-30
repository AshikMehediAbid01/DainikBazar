using DainikBazar.Domain.Models;

namespace DainikBazar.Domain.Interfaces;

public interface ICartRepository
{
    Task<Cart> GetCartAsync(string userId);
}
