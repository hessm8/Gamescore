using Gamescore.DAL.Repositories;
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

        public async Task<bool> RateGame(ClaimsPrincipal claimsUser, Game game, int ratingPoints)
        {
            if (!claimsUser.Identity.IsAuthenticated) return false;

            var user = await uow.Users.GetFirst(u => u.UserName == claimsUser.Identity.Name);

            var userRating = await uow.Ratings.GetFirst(r => r.UserId == user.Id && r.GameId == game.Id);

            if (userRating != null)
            {
                userRating.RatingGameplay = ratingPoints;
                userRating.RatingImplementation = ratingPoints;
                userRating.RatingOriginality = ratingPoints;
            } else
            {
                userRating = new Rating()
                {
                    GameId = game.Id,
                    UserId = user.Id,
                    RatingGameplay = ratingPoints,
                    RatingImplementation = ratingPoints,
                    RatingOriginality = ratingPoints
                };
                game.RatedBy.Add(userRating);
            }          

            await uow.Save();
            return true;
        }

        public async Task<Rating?> GetRating(Game game, ClaimsPrincipal claims)
        {
            if (!claims.Identity.IsAuthenticated) return null;

            var user = await uow.Users.GetFirst(u => u.UserName == claims.Identity.Name);

            var rating = await uow.Ratings.GetFirst(r => r.UserId == user.Id && r.GameId == game.Id);
            return rating;
        }
    }

    public interface IGameService
    {
        public Task<IEnumerable<Game>> GetAll();
        public Task AddGame(Game game);
        public Task<Game?> GetByName(string alias);
        public Task<bool> RateGame(ClaimsPrincipal user, Game game, int rating);
        public Task<Rating?> GetRating(Game game, ClaimsPrincipal claims);
    }
}
