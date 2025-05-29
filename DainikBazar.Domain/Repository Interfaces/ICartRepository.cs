using DainikBazar.Domain.Entities;

namespace DainikBazar.Domain.Repository_Interfaces;

public interface ICartRepository
{
    Task<Cart> GetCartAsync(string userId);
}
