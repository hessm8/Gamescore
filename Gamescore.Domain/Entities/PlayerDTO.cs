using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gamescore.Domain.Entities
{
    // Simplified player model that combines player and match data for later transfer to Player and MatchPlayer entities
    public class PlayerDTO
    {
        public string UserName { get; set; }
        public bool Registered { get; set; }
        public string Team { get; set; }
        public int Points { get; set; }
        public bool IsWinner { get; set; }
    }
}
