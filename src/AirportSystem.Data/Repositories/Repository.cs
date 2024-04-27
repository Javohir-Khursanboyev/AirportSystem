using AirportSystem.Data.DbContexts;
using AirportSystem.Domain.Commons;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AirportSystem.Data.Repositories;

public class Repository<T> : IRepository<T> where T : Auditable
{
    private readonly AppDbContext _context;
    private readonly DbSet<T> set;
    public Repository(AppDbContext context)
    {
        this._context = context;
        this.set = _context.Set<T>();
    }
    public async ValueTask<T> InsertAsync(T entity)
    {
       return (await set.AddAsync(entity)).Entity;
    }

    public async ValueTask<T> DeleteAsync(T entity)
    {
        entity.IsDeleted = true;
        entity.DeletedAt = DateTime.UtcNow;
        set.Update(entity);
        return await Task.FromResult(entity);
    }

    public async ValueTask<T> DropAsync(T entity)
    {
        return await Task.FromResult(set.Remove(entity).Entity);   
    }

    public async ValueTask<T> UpdateAsync(T entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        set.Update(entity);
        return await Task.FromResult(entity);
    }

    public async ValueTask<T> SelectAsync(Expression<Func<T, bool>> expression, string[] includes = null)
    {
        var query = set.Where(expression);

        if(includes is not null)
            foreach (var include in includes)
                query = query.Include(include);

        return await query.FirstOrDefaultAsync();
    }

    public async ValueTask<IEnumerable<T>> SelectAsEnumerable(
        Expression<Func<T, bool>> expression = null,
        string[] includes = null,
        bool isTracked = true)
    {
        var query = expression is null ? set : set.Where(expression);

        if (includes is not null)
            foreach (var include in includes)
                query = query.Include(include);

        if (!isTracked)
            query.AsNoTracking();

        return await query.ToListAsync();        
    }

    public IQueryable<T> SelectAsQueryable(
        Expression<Func<T, bool>> expression = null,
        string[] includes = null,
        bool isTracked = true)
    {
        var query = expression is null ? set : set.Where(expression);

        if (includes is not null)
            foreach (var include in includes)
                query = query.Include(include);

        if (!isTracked)
            query.AsNoTracking();

        return query;
    }
}
