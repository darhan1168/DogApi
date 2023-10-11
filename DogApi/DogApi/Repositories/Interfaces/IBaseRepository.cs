using System.Linq.Expressions;

namespace DogApi.Repositories.Interfaces;

public interface IBaseRepository<T> where T : class
{
    Task<List<T>> GetAsync(
        Expression<Func<T, bool>> filter = null,
        Expression<Func<IQueryable<T>, IOrderedQueryable<T>>> orderBy = null);
}