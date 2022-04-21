using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gamescore.Entities
{
    [Table("Players")]
    public class Player : BaseEntity
    {
        [Key, Required]
        public Guid OwnerId { get; set; }
        public virtual User Owner { get; set; } = null!;
        public Guid? UserPlayerId { get; set; }
        public virtual User? UserPlayer { get; set; }
        public string? Alias { get; set; }

        // Matches this player participated in
        public virtual ICollection<MatchPlayer> Matches { get; set; } = new List<MatchPlayer>();
    }
}
