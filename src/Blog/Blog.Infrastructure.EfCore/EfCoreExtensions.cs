using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Infrastructure.EfCore.DbContext;
using BN.CleanArchitecture.Infrastructure.EfCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Infrastructure.EfCore;

public static class EfCoreExtensions
{
    private const string DbName = "postgres";
    public static IServiceCollection AddEfCoreServices(this IServiceCollection services,
      IConfiguration config)
    {
        services.AddPostgresDbContext<BlogDbContext>(
            config.GetConnectionString(DbName),
            // dbOptionsBuilder => dbOptionsBuilder.UseModel(MainDbContextModel.Instance),
            null,
            svc => svc.AddRepository(typeof(Repository<,>))
        );

        return services;
    }
}
