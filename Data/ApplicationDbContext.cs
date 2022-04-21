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
        public DbSet<Match> Sessions => Set<Match>();
        public DbSet<User> Profiles => Set<User>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Friend requests

            builder.Entity<FriendRequest>()
                .HasKey(f => new { f.SentById, f.SentToId });

            builder.Entity<FriendRequest>()
                .HasOne(f => f.SentBy)
                .WithMany(user => user.SentFriendRequests)
                .HasForeignKey(f => f.SentById).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<FriendRequest>()
                .HasOne(f => f.SentTo)
                .WithMany(user => user.ReceievedFriendRequests)
                .HasForeignKey(f => f.SentToId);

            // Player

            //builder.Entity<Player>()
            //    .HasKey(f => new { f.SentById, f.SentToId });

            builder.Entity<Player>()
                .HasOne(player => player.Owner)
                .WithMany(user => user.PlayersCreated)
                .HasForeignKey(player => player.OwnerId);//.OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Player>()
                .HasOne(player => player.UserPlayer)
                .WithMany(user => user.MatchesPlayed)
                .HasForeignKey(player => player.UserPlayerId);

        }
    }
}