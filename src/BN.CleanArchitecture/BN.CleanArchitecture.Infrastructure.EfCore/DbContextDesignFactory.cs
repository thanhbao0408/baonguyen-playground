using BN.CleanArchitecture.Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace BN.CleanArchitecture.Infrastructure.EfCore;

public abstract class DbContextDesignFactoryBase<TDbContext> : IDesignTimeDbContextFactory<TDbContext>
    where TDbContext : DbContext
{
    public TDbContext CreateDbContext(string[] args)
    {
        string? connString = ConfigurationHelper.GetConfiguration(AppContext.BaseDirectory)
            ?.GetConnectionString("postgres");

        Console.WriteLine($"Connection String: {connString}");

        DbContextOptionsBuilder<TDbContext>? optionsBuilder = new DbContextOptionsBuilder<TDbContext>()
            .UseNpgsql(
                connString ?? throw new InvalidOperationException(),
                sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(GetType().Assembly.FullName);
                    sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(30), null);
                }
            ).UseSnakeCaseNamingConvention();

        Console.WriteLine(connString);
        return (TDbContext)Activator.CreateInstance(typeof(TDbContext), optionsBuilder.Options);
    }
}