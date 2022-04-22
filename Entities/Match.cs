using System.ComponentModel.DataAnnotations;
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

    public class MatchPlayer
    {
        public Guid MatchId { get; set; }
        public virtual Match Match { get; set; } = null!;
        public Guid PlayerId { get; set; }
        public virtual Player Player { get; set; } = null!;

        public string? Team { get; set; }
        public int Points { get; set; }        
        public bool IsWinner { get; set; }
    }
}
