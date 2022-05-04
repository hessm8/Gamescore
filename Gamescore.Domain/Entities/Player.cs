﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gamescore.Domain.Entities
{
    [Table("Players")]
    public class Player : BaseEntity
    {
        [Required]
        public Guid OwnerId { get; set; }
        public virtual AppUser Owner { get; set; } = null!;
        
        public Guid? UserPlayerId { get; set; }
        public virtual AppUser? UserPlayer { get; set; }
        
        public string? Alias { get; set; }

        // Matches this player participated in
        public virtual ICollection<MatchPlayer> Matches { get; set; } = new List<MatchPlayer>();
    }
}