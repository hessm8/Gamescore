using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gamescore.Entities
{
    [Table("Sessions")]
    public class Session : BaseEntity
    {
        [Required]
        public virtual Game Game { get; set; }

        public int Duration { get; set; }
        public DateTime? Date { get; set; }
        public string Place { get; set; }        

        public string Comment { get; set; }

        public virtual ICollection<Player> Players { get; set; } = new List<Player>();
    }
}
