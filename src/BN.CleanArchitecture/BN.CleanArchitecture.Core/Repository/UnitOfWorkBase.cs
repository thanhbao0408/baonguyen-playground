using BN.CleanArchitecture.Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace BN.CleanArchitecture.Infrastructure.EfCore;

public class UnitOfWorkBase<TDbContext> : IUnitOfWork<TDbContext>
    where TDbContext: DbContext
{  
    public TDbContext Context { get; private set; }

    public UnitOfWorkBase(TDbContext dbContext)
    {
        Context = dbContext;
    }

    public Task CommitAsync()  
    {  
        return Context.SaveChangesAsync();  
    }
}