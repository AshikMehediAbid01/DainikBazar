using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DainikBazar.Application.Common.Repository_Interfaces;
using DainikBazar.Domain.Entities;
using DainikBazar.Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace DainikBazar.Repository.Repositories;

public class ProductRepository(AppDbContext db) : IProductRepository
{
    public async Task CreateNewAsync(Product product)
    {
        await db.Products.AddAsync(product);
        await db.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var product = await GetByIdAsync(id);
        if(product != null)
        {
            db.Products.Remove(product);
            await db.SaveChangesAsync();
        }
       
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        var products = await db.Products.ToListAsync();
        return products;
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        var product = await db.Products
            .Include(r=>r.ReviewAndRatings)
            .FirstOrDefaultAsync(c => c.ProductId == id);
        return product;
    }

    public async Task UpdateAsync(Product product)
    {
        db.Products.Update(product);
        await db.SaveChangesAsync();
    }
}
