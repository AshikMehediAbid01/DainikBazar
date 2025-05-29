using DainikBazar.Domain.Models;

namespace DainikBazar.Domain.Repository_Interfaces;

public interface ICartRepository
{
    Task<Cart> GetCartAsync(string userId);
}
