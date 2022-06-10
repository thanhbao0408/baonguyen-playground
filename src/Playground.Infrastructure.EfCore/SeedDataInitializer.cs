
using Playground.Infrastructure.EfCore.DbContext;

namespace Playground.Infrastructure.EfCore;

public static class SeedDataInitializer
{
    public static void Initialize(PlaygroundDbContext context)
    {
        context.Database.EnsureCreated();

        // Initialize seed data

        context.SaveChanges();
    }
}