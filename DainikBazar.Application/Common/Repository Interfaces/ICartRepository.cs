using DainikBazar.Domain.Entities;

namespace DainikBazar.Application.Common.Repository_Interfaces;

public interface ICartRepository
{
    Task<Cart> GetCartAsync( string userId );
}
