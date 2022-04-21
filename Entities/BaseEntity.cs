using System.ComponentModel.DataAnnotations;

namespace Gamescore.Entities
{
    public class BaseEntity
    {
        [Key, Required]
        public virtual Guid Id { get; set; }
    }
}
