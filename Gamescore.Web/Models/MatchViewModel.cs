using Gamescore.BLL.Services;
using Gamescore.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Gamescore.Web.Models
{
    public class MatchViewModel
    {
        //public MatchViewModel(AppUser owner, Game? game)
        //{
        //    Owner = owner;

        //    // Need to get Match from the db

        //    Match = new Match()
        //    {
        //        Game = game
        //    };
        //}

        //public MatchViewModel() { }

        //gameAlias: '@Model.GameAlias',
        //date: new Date(),
        //place: '',
        //duration: 0,
        //players: []

        public string GameAlias { get; set; }
        public DateTime Date { get; set; }
        public string Place { get; set; }
        public int Duration { get; set; }
        public string Comment { get; set; }
        public List<PlayerViewModel> Players { get; set; }

        //public void AddPlayer()
        //{
        //    var player = new Player()
        //    {
        //        Alias = "Test " + PlayersOfUser.Count(),
        //        OwnerId = Owner.Id
        //    };
        //    PlayersOfUser.Add(player);

        //    Match.Players.Add(
        //        new MatchPlayer()
        //        {
        //            MatchId = Match.Id,
        //            PlayerId = player.Id,
        //            IsWinner = true,
        //            Points = 5,
        //            Team = "Blue"
        //        }    
        //    );
        //}
    }
}
