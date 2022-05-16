using Microsoft.EntityFrameworkCore.Infrastructure;

namespace BN.CleanArchitecture.Infrastructure.EfCore;

public interface IDbFacadeResolver
{
    DatabaseFacade Database { get; }
}