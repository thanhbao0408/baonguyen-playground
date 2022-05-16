namespace Blog.Infrastructure.EfCore;

public static class SeedDataInitializer
{
    public static void Initialize(BlogDbContext context)
    {
        context.Database.EnsureCreated();

        // Initialize seed data

        context.SaveChanges();
    }
}