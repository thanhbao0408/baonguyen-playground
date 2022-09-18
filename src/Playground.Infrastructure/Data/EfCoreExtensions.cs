using BN.CleanArchitecture.Infrastructure.EfCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Playground.Infrastructure.Data.DbContext;

namespace Playground.Infrastructure.Data;

public static class EfCoreExtensions
{
    private const string DefaultDbName = "mssql";
    public static IServiceCollection AddEfCoreServices(this IServiceCollection services,
      IConfiguration config)
    {
        var dbProvider = config.GetValue<string>("DbProvider");
        if (string.IsNullOrEmpty(dbProvider))
        {
            dbProvider = DefaultDbName;
        }

        if (config.GetValue<string>("DbProvider") == "Postgresql")
        {
            services.AddPostgresDbContext<PlaygroundDbContext>(
                config.GetConnectionString(dbProvider),
                // dbOptionsBuilder => dbOptionsBuilder.UseModel(MainDbContextModel.Instance),
                null,
                svc => svc.AddRepository(typeof(Repository<,>))
            );
        }
        else
        {
            services.AddSQLDbContext<PlaygroundDbContext>(
                config.GetConnectionString(dbProvider),
                null,
                svc => svc.AddRepository(typeof(Repository<,>))
            );
        }

        return services;
    }
}
