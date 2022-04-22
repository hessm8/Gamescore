﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gamescore.Entities
{
    [Table("Matches")]
    public class Match : BaseEntity
    {
        [Required]
        public virtual Game Game { get; set; } = null!;

        public int Duration { get; set; }
        public DateTime? Date { get; set; }
        public string? Place { get; set; } 
        public string? Comment { get; set; }

        // Players in current match
        public virtual ICollection<MatchPlayer> Players { get; set; } = new List<MatchPlayer>();
    }
}
