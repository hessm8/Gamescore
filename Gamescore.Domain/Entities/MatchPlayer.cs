namespace Gamescore.Domain.Entities
{
    // Entity between Match and Player tables
    // Stores additional match data for player  
    public class MatchPlayer
    {
        public Guid MatchId { get; set; }
        public virtual Match Match { get; set; } = null!;
        public Guid PlayerId { get; set; }
        public virtual Player Player { get; set; } = null!;

        public string? Team { get; set; }
        public int Points { get; set; }
        public bool IsWinner { get; set; }
    }
}
