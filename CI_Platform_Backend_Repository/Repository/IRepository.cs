using System.Linq.Expressions;

namespace CI_Platform_Backend_Repository.Repository;

public interface IRepository<T> where T : class
{
    Task<bool> AddAsync(T entity);

    Task<bool> UpdateAsync(T entity);

    Task<T> GetAsync(Expression<Func<T, bool>> filter = null!);

    Task<List<T>> GetAsync();

    Task<bool> DeleteAsync(T entity);
}
