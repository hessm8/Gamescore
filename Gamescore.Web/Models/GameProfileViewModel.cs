using Gamescore.BLL.Services;
using Gamescore.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Gamescore.Web.Models
{
    public class GameProfileViewModel
    {
        //private readonly IUserService service;

        public static async Task<GameProfileViewModel?> Create(IGameService service, Game game, ClaimsPrincipal claims)
        {
            var model = new GameProfileViewModel();

            model.Game = game;
            model.Rating = await service.GetRating(game, claims);

            return model;
        }

        public Game Game { get; set; }
        public Rating Rating { get; set; }
    }
}
