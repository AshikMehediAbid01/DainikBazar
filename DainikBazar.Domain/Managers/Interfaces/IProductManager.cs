using DainikBazar.Domain.Models;

namespace DainikBazar.Domain.Managers.Interfaces;

public interface IProductManager
{
    Task<List<Product>> GetAllAsync();
    Task CreateNewAsync(Product product);
    Task<Product?> GetByIdAsync(string id);
    Task UpdateAsync(Product product);
    Task DeleteAsync(int id);
}
