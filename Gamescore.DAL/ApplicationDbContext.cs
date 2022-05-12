using Gamescore.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Gamescore.DAL
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<Game> Games => Set<Game>();
        public DbSet<Match> Sessions => Set<Match>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Game>()
                .HasIndex(game => game.Alias)
                .IsUnique();

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
                .HasForeignKey(player => player.OwnerId);

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
                .WithMany(player => player.Matches)
                .HasForeignKey(mp => mp.PlayerId);

            // Ratings

            builder.Entity<Rating>()
                .HasKey(mp => new { mp.GameId, mp.UserId });

            builder.Entity<Rating>()
                .HasOne(rating => rating.Game)
                .WithMany(game => game.RatedBy)
                .HasForeignKey(rating => rating.GameId);

            builder.Entity<Rating>()
                .HasOne(rating => rating.User)
                .WithMany(user => user.GamesRated)
                .HasForeignKey(rating => rating.UserId);
        }
    }
}