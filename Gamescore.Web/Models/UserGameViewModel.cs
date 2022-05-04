using System.ComponentModel.DataAnnotations;

namespace Gamescore.Web.Models
{
    public class UserGameViewModel
    {
        [Required(ErrorMessage = "Enter the alias")]
        public string Alias { get; set; }

    }
}