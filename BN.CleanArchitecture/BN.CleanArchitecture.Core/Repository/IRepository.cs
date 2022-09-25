using BN.CleanArchitecture.Core.Domain.Entities;
using BN.CleanArchitecture.Core.Specification;
using System.Linq.Expressions;

namespace BN.CleanArchitecture.Core.Repository;

public interface IRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
{
    Task<TEntity> FindByIdAsync(TKey id);
    Task<TEntity> FindOneAsync(ISpecification<TEntity> spec);

    Task<List<TEntity>> FindAsync(ISpecification<TEntity> spec);
    Task<TEntity> AddAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);

    Task RemoveAsync(TEntity entity);

    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
}