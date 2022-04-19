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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<FriendRequest>()
                .HasKey(f => new { f.SentById, f.SentToId });

            builder.Entity<FriendRequest>()
                .HasOne(f => f.SentBy)
                .WithMany(b => b.SentFriendRequests)
                .HasForeignKey(f => f.SentById).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<FriendRequest>()
                .HasOne(f => f.SentTo)
                .WithMany(b => b.ReceievedFriendRequests)
                .HasForeignKey(f => f.SentToId);

        }
    }
}