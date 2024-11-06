using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class GenericRepository<T>(StoreContext storeContext) : IGenericRepository<T> where T : BaseEntity
{
    public async Task<T?> GetByIdAsync(int id)
    {
        return await storeContext.Set<T>().FindAsync(id);
    }

    public async Task<IReadOnlyList<T>> ListAllAsync()
    {
        return await storeContext.Set<T>().ToListAsync();
    }

    public void Add(T entity)
    {
        storeContext.Set<T>().Add(entity);
    }

    public void Update(T entity)
    {
        storeContext.Set<T>().Attach(entity);
        storeContext.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(T entity)
    {
        storeContext.Set<T>().Remove(entity);
    }

    public async Task<bool> SaveAllAsync()
    {
        return await storeContext.SaveChangesAsync() > 0;
    }

    public bool Exists(int id)
    {
        return storeContext.Set<T>().Any(e => e.Id == id);
    }
}
