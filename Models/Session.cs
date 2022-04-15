using System.ComponentModel.DataAnnotations;

namespace Gamescore.Models
{
    public class Session
    {
        [Key]
        public virtual int Id { get; set; }
        [Required]
        public virtual Game Game { get; set; }

        public int Duration { get; set; }
        public DateTime? Date { get; set; }
        public string Place { get; set; }        

        public string Comment { get; set; }

        public virtual ICollection<Player> Players { get; set; } = new List<Player>();
    }
}
