using Gamescore.BLL.Services;
using Gamescore.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Gamescore.Web.Models
{
    public class GameProfileViewModel
    {
        public static async Task<GameProfileViewModel?> Create(IGameService service, Game game, AppUser user)
        {
            var model = new GameProfileViewModel();

            model.Game = game;
            model.Rating = await service.GetRating(game, user);
            model.SiteRating = await service.GetSiteRating(game);

            if (user != null)
            {
                model.InCollection = user.GamesFavorited.Contains(game);
                model.LoggedPlays = await service.GetLoggedPlays(user, game);
            }

            return model;
        }

        public Game Game { get; set; } = null!;
        public Rating? Rating { get; set; }
        public float? SiteRating { get; set; }

        public IEnumerable<Match>? LoggedPlays { get; set; }
        
        public bool InCollection { get; set; }
    }
}
