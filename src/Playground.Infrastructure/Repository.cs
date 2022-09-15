using BN.CleanArchitecture.Core.Domain.Entities;
using BN.CleanArchitecture.Infrastructure.EfCore;
using Playground.Infrastructure.DbContext;

namespace Playground.Infrastructure;

public class Repository<TEntity, TKey> : RepositoryBase<PlaygroundDbContext, TEntity, TKey>
    where TEntity : class, IEntity<TKey>
{
    public Repository(PlaygroundDbContext dbContext) : base(dbContext)
    {
    }
}