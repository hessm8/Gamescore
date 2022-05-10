﻿using Gamescore.DAL.Repositories;
using Gamescore.Domain.Entities;
using System.Security.Claims;

namespace Gamescore.BLL.Services
{
    public class GameService : BaseService, IGameService
    {
        public GameService(IUnitOfWork uow) : base(uow) { }

        public async Task<IEnumerable<Game>> GetAll()
        {
            return await uow.Games.GetAll();
        }

        public async Task<Game?> GetByName(string alias)
        {
            var game = await uow.Games.GetFirst(game => game.Alias == alias);
            return game;
        }

        public async Task AddGame(Game game)
        {
            await uow.Games.Create(game);
            await uow.Save();
        }

        public async Task RateGame(ClaimsPrincipal claimsUser, Game game, int rating)
        {
            var user = await uow.Users.GetFirst(u => u.UserName == claimsUser.Identity.Name);

            var userRating = new Rating()
            {
                GameId = game.Id,
                UserId = user.Id,
                RatingGameplay = rating,
                RatingImplementation = rating,
                RatingOriginality = rating
            };

            game.RatedBy.Add(userRating);
            await uow.Save();
        }
    }

    public interface IGameService
    {
        public Task<IEnumerable<Game>> GetAll();
        public Task AddGame(Game game);
        public Task<Game?> GetByName(string alias);
        public Task RateGame(ClaimsPrincipal user, Game game, int rating);
    }
}
