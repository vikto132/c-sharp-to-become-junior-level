using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;

namespace Core.Repository;

public interface IGeneralModelRepository
{
    IQueryable<T> GetQueryable<T>() where T : class;

    Task<T> Get<T>(long id, bool raiseException = true, params string[] includeNavigationPaths)
        where T : class, IHasId;

    Task<TEntity> GetAndCheckExisting<TEntity>(long id,
        params string[] includeNavigationPaths) where TEntity : class, IHasId;

    Task<T> GetByKeys<T>(params object[] keys) where T : class;

    Task<T> AddAsync<T>(T instance) where T : class;

    Task<IList<T>> AddRangeAsync<T>(IList<T> instances) where T : class;

    Task<T> Create<T>(T instance) where T : class;

    Task<T> Update<T>(T instance) where T : class;

    Task SaveChanges<T>() where T : class;

    public void RemoveRange<T>(IEnumerable<T> objects) where T : class, IHasId;
        
    Task Delete<T>(long id) where T : class, IHasId;

    Task Delete<T>(T instance) where T : class, IHasId;
    void Remove<T>(T instance) where T : class, IHasId;
}