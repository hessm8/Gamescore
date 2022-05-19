using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gamescore.Domain.Entities
{
    [Table("Games")]
    public class Game : BaseEntity
    {
        [Required]
        public string Alias { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? NameLocalized { get; set; }        

        public int? ReleaseDate { get; set; }
        public string? Description { get; set; }

        public int AgeMin { get; set; }
        public int PlayersMin { get; set; }
        public int PlayersMax { get; set; }
        public int DurationMin { get; set; }
        public int DurationMax { get; set; }

        // User collection / rating with current game
        public virtual ICollection<AppUser> FavoritedBy { get; set; } = new List<AppUser>();
        public virtual ICollection<Rating> RatedBy { get; set; } = new List<Rating>();

        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile? ImageFile { get; set; } = null;

    }
}
