using System.Linq.Expressions;
using CI_Platform_Backend_DBEntity.Context;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform_Backend_Repository.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly ApplicationDbContext _dbContext;
    internal DbSet<T> _dbSet;

    public Repository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<T>();
    }

    public async Task<bool> AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        return await _dbContext.SaveChangesAsync() == 1;
    }

    public async Task<bool> UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        return await _dbContext.SaveChangesAsync() == 1;
    }

    // public async Task<List<string>> GetAsync(List<long> skillIDs)
    // {
    //     return await _dbContext.Skills.Where(c => skillIDs.Contains(c.SkillId) && c.Status).Select(x=>x.Skills).ToListAsync();
    // }

    public async Task<T> GetAsync(Expression<Func<T, bool>> filter = null!)
    {
        return await _dbSet.Where(filter).FirstOrDefaultAsync();
    }

    public async Task<List<T>> GetAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<bool> DeleteAsync(T entity)
    {
        _dbSet.Remove(entity);
        return await _dbContext.SaveChangesAsync() == 1;
    }
}
