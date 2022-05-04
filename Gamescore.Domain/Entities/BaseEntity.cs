using System.ComponentModel.DataAnnotations;

namespace Gamescore.Domain.Entities
{
    public class BaseEntity
    {
        [Key, Required]
        public virtual Guid Id { get; set; }
    }
}
