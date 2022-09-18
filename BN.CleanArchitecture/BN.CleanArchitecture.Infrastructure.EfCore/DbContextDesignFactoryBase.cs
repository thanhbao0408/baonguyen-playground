using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using BN.CleanArchitecture.Infrastructure.Helpers;

namespace BN.CleanArchitecture.Infrastructure.EfCore
{
    public abstract class DbContextDesignFactoryBase<TDbContext> : IDesignTimeDbContextFactory<TDbContext>
       where TDbContext : DbContext
    {
        public TDbContext CreateDbContext(string[] args)
        {
            var connString = ConfigurationHelper.GetConfiguration(AppContext.BaseDirectory)
                ?.GetConnectionString("mssql");

            Console.WriteLine($"Connection String: {connString}");

            var optionsBuilder = new DbContextOptionsBuilder<TDbContext>()
                .UseSqlServer(
                    connString ?? throw new InvalidOperationException(),
                    sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(GetType().Assembly.FullName);
                        sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(30), null);
                    }
                ).UseSnakeCaseNamingConvention();

            Console.WriteLine(connString);
            return (TDbContext)Activator.CreateInstance(typeof(TDbContext), optionsBuilder.Options, null);
        }
    }
}
