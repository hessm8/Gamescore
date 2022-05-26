using Gamescore.BLL.Services;
using Gamescore.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Gamescore.Web.Models
{
    public class MatchViewModel
    {
        public MatchViewModel(AppUser owner, Game? game)
        {
            Owner = owner;

            // Need to get Match from the db

            Match = new Match()
            {
                Game = game
            };
        }

        public MatchViewModel() { }

        AppUser Owner { get; set; }

        public Match Match { get; set; }
        public List<Player> PlayersOfUser { get; set; }
        public Game Game => Match.Game;

        public void AddPlayer()
        {
            var player = new Player()
            {
                Alias = "Test " + PlayersOfUser.Count(),
                OwnerId = Owner.Id
            };
            PlayersOfUser.Add(player);

            Match.Players.Add(
                new MatchPlayer()
                {
                    MatchId = Match.Id,
                    PlayerId = player.Id,
                    IsWinner = true,
                    Points = 5,
                    Team = "Blue"
                }    
            );
        }
    }
}
