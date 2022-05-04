﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Gamescore.Entities
{
    [Table("FriendRequests")]
    public class FriendRequest
    {
        public Guid SentById { get; set; }
        public virtual AppUser SentBy { get; set; } = null!;

        public Guid SentToId { get; set; }
        public virtual AppUser SentTo { get; set; } = null!;

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