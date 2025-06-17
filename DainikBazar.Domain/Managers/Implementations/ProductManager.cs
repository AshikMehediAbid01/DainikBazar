using DainikBazar.Domain.Managers.Interfaces;
using DainikBazar.Domain.Models;
using DainikBazar.Domain.Interfaces;

namespace DainikBazar.Domain.Managers.Implementations;

public class ProductManager(IProductRepository productRepo) : IProductManager
{
    public async Task CreateNewAsync(Product product)
    {
        product.ProductGuid = Guid.NewGuid().ToString();

        await productRepo.CreateNewAsync(product);
    }

    public async Task<List<Product>> GetAllAsync()
    {
        var products = await productRepo.GetAllAsync();
        return products.ToList();
    }

    public async Task<Product?> GetByIdAsync(string id)
    {
        var product = await productRepo.GetByIdAsync(id);
        return product;
    }

    public async Task UpdateAsync(Product product)
    {
        await productRepo.UpdateAsync(product);
    }

    public async Task DeleteAsync(int id)
    {
        await productRepo.DeleteAsync(id);
    }
}
