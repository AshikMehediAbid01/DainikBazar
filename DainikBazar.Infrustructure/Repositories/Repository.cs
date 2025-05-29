using DainikBazar.Application.Common.Repository_Interfaces;
using DainikBazar.Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace DainikBazar.Repository.Repositories;

public class Repository: IRepository
{
    private readonly AppDbContext _dbContext;
    public Repository( AppDbContext dbContext )
    {
        _dbContext = dbContext;
    }

    public async Task<T> GetByIdAsync<T>( int id ) where T : class
    {
        return await _dbContext.Set<T>().FindAsync( id );
    }
    public async Task<IEnumerable<T>> GetAllAsync<T>() where T : class
    {
        return await _dbContext.Set<T>().ToListAsync();
    }
    public async Task AddAsync<T>( T entity ) where T : class
    {
        await _dbContext.Set<T>().AddAsync( entity );
    }
    public async Task UpdateAsync<T>( T entity ) where T : class
    {
         _dbContext.Set<T>().Update(entity);
    }
    public async Task DeleteAsync<T>( T entity ) where T : class
    {
         _dbContext.Set<T>().Remove(entity);
    }
    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}
