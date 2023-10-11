using System.Linq.Expressions;
using DogApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using AppContext = DogApi.DataBase.AppContext;

namespace DogApi.Repositories.Implementations;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    private readonly AppContext _context;
    private readonly DbSet<T> _dbSet;

    public BaseRepository(AppContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }
    
    public virtual async Task<List<T>> GetAsync(
        Expression<Func<T, bool>> filter = null,
        Expression<Func<IQueryable<T>, IOrderedQueryable<T>>> orderBy = null)
    {
        IQueryable<T> query = _dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (orderBy != null)
        {
            return await orderBy.Compile()(query).ToListAsync();
        }
        else
        {
            return await query.ToListAsync();
        }
    }
}