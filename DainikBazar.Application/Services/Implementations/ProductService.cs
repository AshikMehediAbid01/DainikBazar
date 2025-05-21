using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DainikBazar.Application.Common.Repository_Interfaces;
using DainikBazar.Application.Services.Interfaces;
using DainikBazar.Domain.Entities;

namespace DainikBazar.Application.Services.Implementations;

public class ProductService : IProductService
{
    private readonly IProductRepository _repo;

    public ProductService(IProductRepository repo)
    {
        _repo = repo;
    }



    public async Task CreateNewAsync(Product product)
    {
       await _repo.CreateNewAsync(product);
    }

    public async Task<List<Product>> GetAllAsync()
    {
        var products = await _repo.GetAllAsync();
        return products.ToList();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        var product = await _repo.GetByIdAsync(id);
        return product;
    }

    public async Task UpdateAsync(Product product)
    {
        await _repo.UpdateAsync(product);
    }

    public async Task DeleteAsync(int id)
    {
        await _repo.DeleteAsync(id);
    }
}
