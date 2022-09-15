
using Playground.Infrastructure.DbContext;

namespace Playground.Infrastructure;

public static class SeedDataInitializer
{
    public static void Initialize(PlaygroundDbContext context)
    {
        context.Database.EnsureCreated();

        // Initialize seed data

        context.SaveChanges();
    }
}