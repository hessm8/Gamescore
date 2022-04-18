using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gamescore.Entities
{
    [Table("Games")]
    public class Game : BaseEntity
    {
        
        [Required]
        public string Alias { get; set; }

        public string Name { get; set; }
        public string NameLocalized { get; set; }

        public byte[] Image { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public int AgeMin { get; set; }

        public int PlayersMin { get; set; }
        public int PlayersMax { get; set; }

        public int DurationMin { get; set; }
        public int DurationMax { get; set; }
    }

}
