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

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        var products = await _db.Products.ToListAsync();
        return products;
    }
}
