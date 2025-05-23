using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DainikBazar.Application.Common.Repository_Interfaces;
using DainikBazar.Domain.Entities;
using DainikBazar.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DainikBazar.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _db;

    public ProductRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task CreateNewAsync(Product product)
    {
        await _db.AddAsync(product);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var product = await GetByIdAsync(id);
        if(product != null)
        {
            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
        }
       
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        var products = await _db.Products.ToListAsync();
        return products;
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        var product = await _db.Products.FirstOrDefaultAsync(c => c.Id == id);
        return product;
    }

    public async Task UpdateAsync(Product product)
    {
        _db.Products.Update(product);
        await _db.SaveChangesAsync();
    }
}
