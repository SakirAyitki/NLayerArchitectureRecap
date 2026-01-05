using System.Linq.Expressions;

namespace NLayer.Core.Repositories;

public interface IGenericRepository<T> where T : class
{
    Task AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entities);
    
    void Update(T entity);
    void Delete(T entity);
    void DeleteRange(IEnumerable<T> entities);
    
    IQueryable<T> GetAll();
    IQueryable<T> Where(Expression<Func<T, bool>> expression);

    Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
    
    Task<T> GetByIdAsync(int id);
    
}