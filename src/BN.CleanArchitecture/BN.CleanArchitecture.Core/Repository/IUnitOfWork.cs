using Microsoft.EntityFrameworkCore;

namespace BN.CleanArchitecture.Core.Repository;

public interface IUnitOfWork<TDbContext> where TDbContext : DbContext
{
    Task CommitAsync();

    TDbContext Context { get; }
}