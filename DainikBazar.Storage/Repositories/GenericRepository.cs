using DainikBazar.Domain.Interfaces;
using DainikBazar.Storage.Data;
using Microsoft.EntityFrameworkCore;

namespace DainikBazar.Storage.Repositories;

public class GenericRepository(AppDbContext dbContext) : IGenericRepository
{

    public async Task<T> GetByIdAsync<T>( int id ) where T : class
    {
        return await dbContext.Set<T>().FindAsync( id );
    }
    public async Task<List<T>> GetAllAsync<T>() where T : class
    {
        return await dbContext.Set<T>().ToListAsync();
    }
    public async Task AddAsync<T>( T entity ) where T : class
    {
        await dbContext.Set<T>().AddAsync( entity );
    }
    public async Task UpdateAsync<T>( T entity ) where T : class
    {
         dbContext.Set<T>().Update(entity);
    }
    public async Task DeleteAsync<T>( T entity ) where T : class
    {
         dbContext.Set<T>().Remove(entity);
    }
    public async Task SaveChangesAsync()
    {
        await dbContext.SaveChangesAsync();
    }
}
