using DainikBazar.UI.Models;

namespace DainikBazar.UI.ApiServices.Interfaces;

public interface IProductApiService
{
    Task<List<ProductVM>> GetAllProductsAsync();
    Task<ProductVM?> GetProductByIdAsync(int id);
    Task<bool> CreateProductAsync(ProductVM product);
    Task<bool> UpdateProductAsync(ProductVM product);
    Task<bool> DeleteProductAsync(int id);
}
