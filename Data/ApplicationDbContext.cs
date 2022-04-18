using Gamescore.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Gamescore.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Game> Games => Set<Game>();
        public DbSet<Session> Sessions => Set<Session>();
        public DbSet<UserProfile> Profiles => Set<UserProfile>();
    }
}