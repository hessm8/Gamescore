using System.ComponentModel.DataAnnotations;

namespace Gamescore.Models
{
    public class Game
    {
        [Key]
        public virtual int Id { get; set; }
        [Required]
        public string Alias { get; set; }

        public string Name { get; set; }
        public string NameLocalized { get; set; }

        public byte[] Image { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public int MinAge { get; set; }

        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }

        public int MinDuration { get; set; }
        public int MaxDuration { get; set; }
    }

}
