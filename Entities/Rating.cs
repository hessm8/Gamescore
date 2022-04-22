using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gamescore.Entities
{
    [Table("Ratings")]
    public class Rating : BaseEntity
    {
        [Required]
        public virtual Game Game { get; set; } = null!;
        [Required]
        public virtual AppUser User { get; set; } = null!;

        public float RatingGameplay { get; set; }
        public float RatingOriginality { get; set; }
        public float RatingImplementation { get; set; }

        [NotMapped]
        public float AvgRating 
        {
            get
            {
                var sum = RatingGameplay + RatingOriginality + RatingImplementation;
                return sum / 3;
            }
        }
    }
}
