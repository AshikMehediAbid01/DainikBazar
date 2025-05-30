using AutoMapper;
using DomainModels = DainikBazar.Domain.Models;
using DainikBazar.Storage.Models;
using DainikBazar.Domain.Interfaces;
using DainikBazar.Storage.Data;
using Microsoft.EntityFrameworkCore;

namespace DainikBazar.Storage.Repositories;

public class ProductRepository(AppDbContext db, IMapper mapper) : IProductRepository
{

    public async Task CreateNewAsync(DomainModels.Product domainModel)
    {
        var storageModel = mapper.Map<Product>(domainModel);

        await db.Products.AddAsync(storageModel);
        await db.SaveChangesAsync();
    }

public async Task DeleteAsync(int id)
    {
        var domainModel = await GetByIdAsync(id);
        if (domainModel == null) return;
        var StorageModel = mapper.Map<Product>(domainModel);

        db.Products.Remove(StorageModel);
        await db.SaveChangesAsync();
    }

    public async Task<IEnumerable<DomainModels.Product>> GetAllAsync()
    {
        var storageMOdel = await db.Products.ToListAsync();
        var domainModels = mapper.Map<IEnumerable<DomainModels.Product>>(storageMOdel);
        return domainModels;
    }
       
    public async Task<DomainModels.Product?> GetByIdAsync(int id)
    {
        var storageModel = await db.Products
            .Include(r => r.ReviewAndRatings)
            .FirstOrDefaultAsync(c => c.ProductId == id);

        if (storageModel == null) return null;

        var domainModel = mapper.Map<DomainModels.Product>(storageModel);
        return domainModel;
    }
     
    public async Task UpdateAsync(DomainModels.Product domainModel)
    {
        var storageModel = mapper.Map<Product>(domainModel);
        db.Products.Update(storageModel);
        await db.SaveChangesAsync();
    }
}
