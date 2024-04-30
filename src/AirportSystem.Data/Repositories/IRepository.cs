using AirportSystem.Domain.Commons;
using System.Linq.Expressions;

namespace AirportSystem.Data.Repositories;

public interface IRepository<T> where T : Auditable
{
    ValueTask<T> InsertAsync(T entity);
    ValueTask<T> UpdateAsync(T entity);
    ValueTask<T> DeleteAsync(T entity);
    ValueTask<T> DropAsync(T entity);
    ValueTask<T> SelectAsync(
        Expression<Func<T, bool>> expression,
        string[] includes = null,
        bool isTracked = true);
    ValueTask<IEnumerable<T>> SelectAsEnumerable(
        Expression<Func<T, bool>> expression = null,
        string[] includes = null,
        bool isTracked = true);
   IQueryable<T> SelectAsQueryable(
        Expression<Func<T, bool>> expression = null,
        string[] includes = null,
        bool isTracked = true);
}
