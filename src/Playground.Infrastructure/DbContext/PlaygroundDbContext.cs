using BN.CleanArchitecture.Infrastructure.EfCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Playground.Core.Entities.Blog.Articles;
using System.Data.Entity.Infrastructure.Annotations;

namespace Playground.Infrastructure.DbContext
{
    public class PlaygroundDbContext : DbContextBase
    {
        public DbSet<Article> Articles { get; set; } = default!;
        public PlaygroundDbContext(DbContextOptions<PlaygroundDbContext> options) : base(options)
        {
        }

        public PlaygroundDbContext(DbContextOptions<PlaygroundDbContext> options, IMediator? mediator)
            : base(options, mediator)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /* Include modules to your migration db context */

            //builder.Entity<ApplicationUser>(option => { 
            //    option.ToTable("")
            //});

            builder.Entity<Article>(option =>
            {
                option.Property(p => p.Slug)
                .HasAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new System.ComponentModel.DataAnnotations.Schema.IndexAttribute("IX_FirstNameLastName")));
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