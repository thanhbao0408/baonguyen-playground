using System.Reflection;
using BN.CleanArchitecture.Core.Domain.Events;
using BN.CleanArchitecture.Core.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;

namespace BN.CleanArchitecture.Infrastructure.EfCore;

public static class InfrastructureEfCoreExtensions
{
    public static IServiceCollection AddPostgresDbContext<TDbContext>(this IServiceCollection services,
        string connString, Action<DbContextOptionsBuilder> doMoreDbContextOptionsConfigure = null,
        Action<IServiceCollection> doMoreActions = null)
        where TDbContext : DbContext
    {
        services.AddDbContext<TDbContext>(options =>
        {
            options.UseNpgsql(connString, sqlOptions =>
            {
                sqlOptions.MigrationsAssembly(typeof(TDbContext).Assembly.GetName().Name);
                sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
            }).UseSnakeCaseNamingConvention();

            doMoreDbContextOptionsConfigure?.Invoke(options);
        });

        // services.AddScoped<IDbFacadeResolver>(provider => provider.GetService<TDbContext>());
        // services.AddScoped<IDomainEventContext>(provider => provider.GetService<TDbContext>());

        // services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TxBehavior<,>));

        // services.AddHostedService<DbContextMigratorHostedService>();

        services.AddDatabaseDeveloperPageExceptionFilter();

        doMoreActions?.Invoke(services);

        return services;
    }

    public static IServiceCollection AddRepository(this IServiceCollection services, Type repoType)
    {
        services.Scan(scan => scan
            .FromAssembliesOf(repoType)
            .AddClasses(classes =>
                classes.AssignableTo(repoType)).As(typeof(IRepository<,>)).WithScopedLifetime()
            .AddClasses(classes =>
                classes.AssignableTo(repoType)).As(typeof(IGridRepository<,>)).WithScopedLifetime()
        );

        return services;
    }

    public static void MigrateDataFromScript(this MigrationBuilder migrationBuilder)
    {
        Assembly? assembly = Assembly.GetCallingAssembly();
        string[]? files = assembly.GetManifestResourceNames();
        string filePrefix = $"{assembly.GetName().Name}.Data.Scripts."; //IMPORTANT

        foreach (var file in files
                     .Where(f => f.StartsWith(filePrefix) && f.EndsWith(".sql"))
                     .Select(f => new {PhysicalFile = f, LogicalFile = f.Replace(filePrefix, string.Empty)})
                     .OrderBy(f => f.LogicalFile))
        {
            using Stream? stream = assembly.GetManifestResourceStream(file.PhysicalFile);
            using StreamReader reader = new(stream!);
            string command = reader.ReadToEnd();

            if (string.IsNullOrWhiteSpace(command))
            {
                continue;
            }

            migrationBuilder.Sql(command);
        }
    }
}