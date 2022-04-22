using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Gamescore.Entities
{
    // Note: Will be changed to IdentityUser
    public class UserProfile : BaseEntity // : IdentityUser
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

    [Table("FriendRequests")]
    public class FriendRequest
    {
        public Guid SentById { get; set; }
        public virtual UserProfile SentBy { get; set; } = null!;

        public Guid SentToId { get; set; }
        public virtual UserProfile SentTo { get; set; } = null!;

        public FriendStatus Status { get; set; }

        [NotMapped]
        public bool AreFriends => Status == FriendStatus.Approved;
    }

    public enum FriendStatus
    {
        Pending,
        Approved,
        Rejected
    };

}
