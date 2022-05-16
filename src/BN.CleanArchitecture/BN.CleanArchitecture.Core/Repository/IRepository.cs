using BN.CleanArchitecture.Core.Domain.Entities;
using BN.CleanArchitecture.Core.Specification;

namespace BN.CleanArchitecture.Core.Repository;

public interface IRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
{
    TEntity FindById(TKey id);
    Task<TEntity> FindOneAsync(ISpecification<TEntity> spec);
    Task<List<TEntity>> FindAsync(ISpecification<TEntity> spec);
    Task<TEntity> AddAsync(TEntity entity);
    Task RemoveAsync(TEntity entity);
}