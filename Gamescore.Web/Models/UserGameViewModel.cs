using System.ComponentModel.DataAnnotations;

namespace Gamescore.Web.Models
{
    public class UserGameViewModel
    {
        [Required(ErrorMessage = "Enter the alias")]
        [MaxLength(15)]
        public string Alias { get; set; }

    }
}