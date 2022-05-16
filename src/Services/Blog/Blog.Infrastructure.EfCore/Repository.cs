using Blog.Infrastructure.EfCore.DbContext;
using BN.CleanArchitecture.Core.Domain.Entities;
using BN.CleanArchitecture.Infrastructure.EfCore;

namespace Blog.Infrastructure.EfCore;

public class Repository<TEntity, TKey> : RepositoryBase<BlogDbContext, TEntity, TKey>
    where TEntity : class, IEntity<TKey>
{
    public Repository(BlogDbContext dbContext) : base(dbContext)
    {
    }
}