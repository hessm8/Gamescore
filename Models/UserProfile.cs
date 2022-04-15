using System.ComponentModel.DataAnnotations;

namespace Gamescore.Models
{
    public class UserProfile
    {
        [Key]
        public virtual int UserId { get; set; }
        // have property of User type?

        public virtual ICollection<Friend> Friends { get; set; } = new List<Friend>();
        public virtual ICollection<Game> Collection { get; set; } = new List<Game>();
    }

    public class Friend
    {
        public virtual int Id { get; set; }

        public virtual UserProfile RequestedBy { get; set; } = null!;
        public virtual UserProfile RequestedTo { get; set; } = null!;

        public FriendStatus Status { get; set; }
    }

    public enum FriendStatus
    {
        Pending,
        Approved,
        Rejected
    };

}
