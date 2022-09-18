using BN.CleanArchitecture.Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Playground.Infrastructure.Data.DbContext
{
    public class PlaygroundIdentityDbContextDatabaseDesign : IDesignTimeDbContextFactory<PlaygroundIdentityDbContext>
    {
        public PlaygroundIdentityDbContext CreateDbContext(string[] args)
        {
            var connString = ConfigurationHelper.GetConfiguration(AppContext.BaseDirectory)
                ?.GetConnectionString("identitydb");

            Console.WriteLine($"Connection String: {connString}");

            var optionsBuilder = new DbContextOptionsBuilder<PlaygroundIdentityDbContext>()
                .UseSqlServer(
                    connString ?? throw new InvalidOperationException(),
                    sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(GetType().Assembly.FullName);
                        sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(30), null);
                    }
                ).UseSnakeCaseNamingConvention();

            Console.WriteLine(connString);
            return (PlaygroundIdentityDbContext)Activator.CreateInstance(typeof(PlaygroundIdentityDbContext), optionsBuilder.Options);
        }
    }
}
