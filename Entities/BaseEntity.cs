using System.ComponentModel.DataAnnotations;

namespace Gamescore.Entities
{
    public class BaseEntity
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
    }
}
