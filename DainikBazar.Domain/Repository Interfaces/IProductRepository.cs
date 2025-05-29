using DainikBazar.Domain.Models;

namespace DainikBazar.Domain.Repository_Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
     Task CreateNewAsync(Product product);
    Task<Product?> GetByIdAsync(int id);
    Task UpdateAsync(Product product);
    Task DeleteAsync(int id);
}
