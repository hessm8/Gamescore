using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gamescore.Entities
{
    [Table("Profiles")]
    public class UserProfile : BaseEntity
    {
        //[Key]
        //public virtual int UserId { get; set; }
        // have property of User type?

        //[ForeignKey("SentBy_Id")]
        //public virtual ICollection<Friend> Friends { get; set; } = new List<Friend>();

        public virtual ICollection<Game> Collection { get; set; } = new List<Game>();
    }

    //public class Friend : BaseEntity
    //{
    //    //[ForeignKey("SentBy")]
    //    //public virtual int SentBy_Id { get; set; }
    //    public virtual UserProfile SentBy { get; set; } = null!;
    //    public virtual UserProfile SentTo { get; set; } = null!;

    //    public FriendStatus Status { get; set; }
    //}

    public enum FriendStatus
    {
        Pending,
        Approved,
        Rejected
    };

}
