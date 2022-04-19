using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gamescore.Entities
{
    [Table("Profiles")]
    // Пока что я сделал это отдельной таблицей которая использовала бы ID юзера
    // Но возможно стоит это просто перенести в самого юзера... "AspNetUsers"
    public class UserProfile : BaseEntity
    {
        //[Key]
        //public virtual int UserId { get; set; }
        // have property of User type?

        // Favorite games of a user (Иметь возможность указать что игра есть в коллекции пользователя), many to many
        public virtual ICollection<Game> Games { get; set; } = new List<Game>();


        public virtual ICollection<FriendRequest> SentFriendRequests { get; set; } = new List<FriendRequest>();
        public virtual ICollection<FriendRequest> ReceievedFriendRequests { get; set; } = new List<FriendRequest>();        

        [NotMapped]
        public virtual ICollection<FriendRequest> Friends
        {
            get
            {
                var friends = SentFriendRequests.Where(x => x.Status == FriendStatus.Approved).ToList();
                friends.AddRange(ReceievedFriendRequests.Where(x => x.Status == FriendStatus.Approved));
                return friends;
            }
        }

    }

    public class FriendRequest
    {
        public Guid SentById { get; set; }
        public virtual UserProfile SentBy { get; set; } = null!;

        public Guid SentToId { get; set; }
        public virtual UserProfile SentTo { get; set; } = null!;

        public FriendStatus Status { get; set; }
    }

    public enum FriendStatus
    {
        Pending,
        Approved,
        Rejected
    };

}
