using System.Linq.Expressions;

namespace NLayer.Core.Services;

public interface IService<T> where T:class
{
    Task<T> AddAsync(T entity);
    Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
    
    Task Update(T entity);
    Task Delete(T entity);
    Task DeleteRange(IEnumerable<T> entities);
    
    IQueryable<T> GetAll();
    IQueryable<T> Where(Expression<Func<T, bool>> expression);

    Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
    
    Task<T> GetByIdAsync(int id);
}