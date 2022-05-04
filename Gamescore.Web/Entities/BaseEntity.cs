using System.ComponentModel.DataAnnotations;

namespace Gamescore.Web.Entities
{
    public class BaseEntity
    {
        [Key, Required]
        public virtual Guid Id { get; set; }
    }
}
