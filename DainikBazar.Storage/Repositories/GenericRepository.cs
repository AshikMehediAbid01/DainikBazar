//using AutoMapper;
//using DainikBazar.Domain.Interfaces;
//using DainikBazar.Storage.Data;
//using Microsoft.EntityFrameworkCore;

//namespace DainikBazar.Storage.Repositories;

//public class GenericRepository<TDomain, TEntity>(AppDbContext dbContext, IMapper mapper) : IGenericRepository<TDomain>
//    where TDomain : class
//    where TEntity : class
//{

//    public async Task<TDomain> GetByIdAsync(int id) //where T : class
//    {
//        var entities = await dbContext.Set<TEntity>().FindAsync(id);
//        return mapper.Map<TDomain>(entities);
//    }
//    public async Task<List<TDomain>> GetAllAsync() //where T : class
//    {
//        var entities = await dbContext.Set<TEntity>().ToListAsync();
//        return mapper.Map<List<TDomain>>(entities);
//    }
//    public async Task AddAsync<T>(T entity) where T : class
//    {
//        await dbContext.Set<T>().AddAsync(entity);
//    }
//    public async Task UpdateAsync<T>(T entity) where T : class
//    {
//        dbContext.Set<T>().Update(entity);
//    }
//    public async Task DeleteAsync<T>(T entity) where T : class
//    {
//        dbContext.Set<T>().Remove(entity);
//    }
//    public async Task SaveChangesAsync()
//    {
//        await dbContext.SaveChangesAsync();
//    }
//}
