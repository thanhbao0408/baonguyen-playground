using BN.CleanArchitecture.Core.Domain.Entities;
using BN.CleanArchitecture.Core.Specification;

namespace BN.CleanArchitecture.Core.Repository;

public interface IGridRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
{
    ValueTask<long> CountAsync(IGridSpecification<TEntity> spec);
    Task<List<TEntity>> FindAsync(IGridSpecification<TEntity> spec);
}