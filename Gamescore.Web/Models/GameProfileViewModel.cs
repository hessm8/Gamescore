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

            return model;
        }

        public Game Game { get; set; } = null!;
        public Rating? Rating { get; set; }
    }
}
