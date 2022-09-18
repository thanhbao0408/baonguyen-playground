using BN.CleanArchitecture.Infrastructure.EfCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Playground.Core.Entities.Blog.Articles;

namespace Playground.Infrastructure.Data.DbContext
{
    public class PlaygroundDbContext : DbContextBase
    {
        public DbSet<Article> Articles { get; set; } = default!;

        public PlaygroundDbContext(DbContextOptions<PlaygroundDbContext> options, IMediator? mediator)
            : base(options, mediator)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /* Include modules to your migration db context */

            builder.Entity<Article>(option =>
            {
                option.HasIndex(p => p.Slug).IsUnique();
            });

            builder.Entity<ArticleTag>(options =>
            {
                options.HasKey(table => new
                {
                    table.ArticleId,
                    table.TagId
                });
            });
        }
    }
}