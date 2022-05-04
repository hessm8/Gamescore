using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gamescore.Web.Entities
{
    [Table("Ratings")]
    public class Rating
    {
        public Guid GameId { get; set; }
        public virtual Game Game { get; set; } = null!;
        public Guid UserId { get; set; }
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
