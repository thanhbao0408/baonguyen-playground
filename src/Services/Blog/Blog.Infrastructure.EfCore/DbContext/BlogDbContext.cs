using Blog.Core.Entities.Articles;
using BN.CleanArchitecture.Infrastructure.EfCore;
using MediatR;
using System.Data.Entity.Infrastructure.Annotations;

namespace Blog.Infrastructure.EfCore.DbContext
{
    public class BlogDbContext : DbContextBase
    {
        public DbSet<Article> Articles { get; set; } = default!;
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
        {
        }

        public BlogDbContext(DbContextOptions<BlogDbContext> options, IMediator? mediator)
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
                .HasAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_FirstNameLastName")));
            };
        }
    }
