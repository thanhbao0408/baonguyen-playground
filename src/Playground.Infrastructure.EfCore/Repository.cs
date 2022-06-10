using BN.CleanArchitecture.Core.Domain.Entities;
using BN.CleanArchitecture.Infrastructure.EfCore;
using Playground.Infrastructure.EfCore.DbContext;

namespace Playground.Infrastructure.EfCore;

public class Repository<TEntity, TKey> : RepositoryBase<PlaygroundDbContext, TEntity, TKey>
    where TEntity : class, IEntity<TKey>
{
    public Repository(PlaygroundDbContext dbContext) : base(dbContext)
    {
    }
}