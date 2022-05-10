using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Gamescore.Domain.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        // Fields

        [PersonalData, DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "?")]
        public string? FullName { get; set; }

        [PersonalData, DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "?")]
        public string? Gender { get; set; }

        [PersonalData]
        public DateTime RegisterDate { get; set; } = DateTime.Now;
        public DateTime? LastLoginTime { get; set; }


        #region Relations

        // User collection / ratings of current user
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

                //return FriendRequests.Where(x => x.AreFriends)
            }
        }

        [NotMapped]
        public virtual ICollection<FriendRequest> FriendRequests
        {
            get
            {
                return SentFriendRequests.Union(ReceievedFriendRequests).ToList();
            }
        }
        public virtual ICollection<FriendRequest> SentFriendRequests { get; set; } = new List<FriendRequest>();
        public virtual ICollection<FriendRequest> ReceievedFriendRequests { get; set; } = new List<FriendRequest>();

        public void FriendWith(AppUser friendUser)
        {
            var friendRequest = new FriendRequest()
            {
                SentById = Id,
                SentToId = friendUser.Id,
                Status = FriendStatus.Pending
            };
            SentFriendRequests.Add(friendRequest);
        }
        public bool FriendIsSent(string username) => SentFriendRequests.Any(fr => fr.SentBy.UserName == username);

        // Players where Owner is this user
        public virtual ICollection<Player> PlayersCreated { get; set; } = new List<Player>();
        // Players where UserPlayer is this user
        public virtual ICollection<Player> Players { get; set; } = new List<Player>();

        #endregion
    }

    public class AppRole : IdentityRole<Guid> { }

}
