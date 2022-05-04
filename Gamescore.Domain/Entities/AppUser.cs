using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Gamescore.Domain.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        // User collection / rating of current user
        public virtual ICollection<Game> GamesFavorited { get; set; } = new List<Game>();
        public virtual ICollection<Rating> GamesRated { get; set; } = new List<Rating>();

        [NotMapped]
        public virtual ICollection<FriendRequest> Friends
        {
            get
            {
                var friends = SentFriendRequests.Where(x => x.AreFriends).ToList();
                friends.AddRange(ReceievedFriendRequests.Where(x => x.AreFriends));
                return friends;
            }
        }
        public virtual ICollection<FriendRequest> SentFriendRequests { get; set; } = new List<FriendRequest>();
        public virtual ICollection<FriendRequest> ReceievedFriendRequests { get; set; } = new List<FriendRequest>();

        // Players where Owner is this user
        public virtual ICollection<Player> PlayersCreated { get; set; } = new List<Player>();
        // Players where UserPlayer is this user
        public virtual ICollection<Player> Players { get; set; } = new List<Player>();
    }

    public class AppRole : IdentityRole<Guid> { }

}
