using BN.CleanArchitecture.Infrastructure.EfCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Playground.Infrastructure.EfCore.DbContext;

namespace Playground.Infrastructure.EfCore;

public static class EfCoreExtensions
{
    private const string DbName = "postgres";
    public static IServiceCollection AddEfCoreServices(this IServiceCollection services,
      IConfiguration config)
    {
        services.AddPostgresDbContext<PlaygroundDbContext>(
            config.GetConnectionString(DbName),
            // dbOptionsBuilder => dbOptionsBuilder.UseModel(MainDbContextModel.Instance),
            null,
            svc => svc.AddRepository(typeof(Repository<,>))
        );

        return services;
    }
}
