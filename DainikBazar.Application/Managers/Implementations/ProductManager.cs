using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DainikBazar.Application.Common.Repository_Interfaces;
using DainikBazar.Application.Managers.Interfaces;
using DainikBazar.Domain.Entities;

namespace DainikBazar.Application.Managers.Implementations;

public class ProductManager(IProductRepository repo) : IProductManager
{
    public async Task CreateNewAsync(Product product)
    {
       await repo.CreateNewAsync(product);
    }

    public async Task<List<Product>> GetAllAsync()
    {
        var products = await repo.GetAllAsync();
        return products.ToList();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        var product = await repo.GetByIdAsync(id);
        return product;
    }

    public async Task UpdateAsync(Product product)
    {
        await repo.UpdateAsync(product);
    }

    public async Task DeleteAsync(int id)
    {
        await repo.DeleteAsync(id);
    }
}
