using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Playground.Infrastructure.Identity;

namespace Playground.Infrastructure.Data.DbContext
{
    public class PlaygroundIdentityDbContext : IdentityDbContext<PlaygroundUser>
    {
        public PlaygroundIdentityDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
