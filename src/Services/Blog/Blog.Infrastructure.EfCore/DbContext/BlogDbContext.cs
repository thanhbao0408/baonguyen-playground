using BN.CleanArchitecture.Infrastructure.EfCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.EfCore.DbContext
{
    public class BlogDbContext: DbContextBase
    {
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
        }
    }
}
