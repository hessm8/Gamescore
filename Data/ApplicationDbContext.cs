using Gamescore.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Gamescore.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DbSet<Game> Games => Set<Game>();
        public DbSet<Match> Sessions => Set<Match>();

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

            builder.Entity<Player>()
                .HasOne(player => player.Owner)
                .WithMany(user => user.PlayersCreated)
                .HasForeignKey(player => player.OwnerId);//.OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Player>()
                .HasOne(player => player.UserPlayer)
                .WithMany(user => user.Players)
                .HasForeignKey(player => player.UserPlayerId);

            // Match-Player

            builder.Entity<MatchPlayer>()
                .HasKey(mp => new { mp.MatchId, mp.PlayerId });

            builder.Entity<MatchPlayer>()
                .HasOne(mp => mp.Match)
                .WithMany(match => match.Players)
                .HasForeignKey(mp => mp.MatchId);

            builder.Entity<MatchPlayer>()
                .HasOne(mp => mp.Player)
                .WithMany(match => match.Matches)
                .HasForeignKey(mp => mp.PlayerId);
        }
    }
}