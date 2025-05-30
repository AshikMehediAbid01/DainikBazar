using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DainikBazar.Domain.Managers.Interfaces;
using DainikBazar.Domain.Models;
using DainikBazar.Domain.Interfaces;

namespace DainikBazar.Domain.Managers.Implementations;

public class ProductManager(IProductRepository productRepo, IGenericRepository genericRepo) : IProductManager
{
    public async Task CreateNewAsync(Product product)
    {
        // await genericRepo.AddAsync<Product>(product);
        await productRepo.CreateNewAsync(product);
    }

    public async Task<List<Product>> GetAllAsync()
    {
        var products = await productRepo.GetAllAsync();
        return products.ToList();
    }

    public async Task<Product?> GetByIdAsync(int id)
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
