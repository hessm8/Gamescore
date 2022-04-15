using System.ComponentModel.DataAnnotations;

namespace Gamescore.Entities
{
    public class UserProfile : BaseEntity
    {
        //[Key]
        //public virtual int UserId { get; set; }
        // have property of User type?

        public virtual ICollection<Friend> Friends { get; set; } = new List<Friend>();
        public virtual ICollection<Game> Collection { get; set; } = new List<Game>();
    }

    public class Friend : BaseEntity
    {

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
