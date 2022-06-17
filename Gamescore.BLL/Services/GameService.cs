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

        public async Task<bool> AddGame(Game game)
        {
            var foundGame = await uow.Games.GetFirst(foundGame => 
                foundGame.Alias == game.Alias || foundGame.Name == game.Name);

            if (foundGame != null) return false;
            
            await uow.Games.Create(game);
            await uow.Save();

            return true;
        }

        public async Task<bool> EditGame(Game game)
        {
            var dbGame = await GetByName(game.Alias);
            if (dbGame == null) return false;

            dbGame.Alias = game.Alias;
            dbGame.Name = game.Name;            
            dbGame.NameLocalized = game.NameLocalized;
            dbGame.ReleaseDate = game.ReleaseDate;

            dbGame.PlayersMin = game.PlayersMin;
            dbGame.PlayersMax = game.PlayersMax;
            dbGame.DurationMin = game.DurationMin;
            dbGame.DurationMax = game.DurationMax;

            dbGame.Description = game.Description;

            await uow.Save();

            return true;
        }

        public async Task<bool> RateGame(Game game, AppUser user, int ratingPoints)
        {
            var userRating = await GetRating(game, user);

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

        public async Task<Rating?> GetRating(Game game, AppUser user)
        {
            if (user == null) return null;

            var userRating = await uow.Ratings.GetFirst(r => r.UserId == user.Id && r.GameId == game.Id);
            return userRating;
        }

        public async Task<float?> GetSiteRating(Game game)
        {            
            var ratings = await uow.Ratings.GetAll(r => r.GameId == game.Id);
            int count = ratings.Count();

            if (count == 0) return null;

            var siteRating = ratings.Sum(r => r.AvgRating) / count;
            return (float)Math.Round(siteRating, 2);
        }

        public async Task UploadGameImage(Game game, string basePath)
        {           
            // Create directory for game if needed
            string directory = $"{basePath}\\data\\games\\{game.Alias}";           
            Directory.CreateDirectory(directory);            

            // Set up default picture if none provided
            if (game.ImageFile == null)
            {
                File.Copy($"{basePath}\\images\\common\\unknown-game.png", $"{directory}\\image.png");
                return;
            }

            // Upload game picture
            string extension = Path.GetExtension(game.ImageFile.FileName);
            string filePath = $"{directory}\\image{extension}";
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await game.ImageFile.CopyToAsync(stream);
            }
        }

        private async Task<Player?> GetPlayer(AppUser owner, string userName, bool registered)
        {
            Player? player;
            if (!registered)
            {
                player = await uow.Players.GetFirst(p =>
                    p.OwnerId == owner.Id &&
                    p.Alias == userName
                );

                if (player != null) return player;

                return await CreatePlayer(owner, userName, registered);
            }

            var userPlayer = await uow.Users.GetFirst(u => u.UserName == userName);
            if (userPlayer == null) return null; // ?

            player = await uow.Players.GetFirst(p =>
                p.OwnerId == owner.Id &&
                p.UserPlayerId == userPlayer.Id
            );

            if (player != null) return player;

            return await CreatePlayer(owner, userName, registered, userPlayer.Id);
        }

        private async Task<Player> CreatePlayer(AppUser owner, string userName,
            bool registered, Guid? userPlayerId = null)
        {
            var player = new Player();
            player.OwnerId = owner.Id;

            if (!registered) player.Alias = userName;
            else player.UserPlayerId = userPlayerId;

            return await uow.Players.Create(player);
        }

        public async Task<bool> AddMatch(AppUser requestedFrom, Match match, List<PlayerDTO> playersReceived)
        {
            foreach (var playerToAdd in playersReceived)
            {
                var dbPlayer = await GetPlayer(requestedFrom, playerToAdd.UserName, playerToAdd.Registered);
                match.Players.Add(
                    new MatchPlayer()
                    {
                        PlayerId = dbPlayer.Id,
                        IsWinner = playerToAdd.IsWinner,
                        Points = playerToAdd.Points,
                        Team = playerToAdd.Team                        
                    }
                );
            }

            await uow.Matches.Create(match);
            await uow.Save();

            return true;
        }

        public async Task<IEnumerable<Match>> GetLoggedPlays(AppUser owner, Game game)
        {
            var matches = await uow.Matches.GetAll(
                m => m.Game.Id == game.Id && m.Players.Any(
                    mp => mp.Player.UserPlayerId == owner.Id
                ), 
                "Game", "Players", "Players.Player"                
            );

            return matches;
        }
    }

    public interface IGameService
    {
        public Task<IEnumerable<Game>> GetAll();
        public Task<bool> AddGame(Game game);
        public Task<bool> EditGame(Game game);
        public Task<Game?> GetByName(string alias);
        public Task<bool> RateGame(Game game, AppUser user, int rating);
        public Task<Rating?> GetRating(Game game, AppUser user);
        public Task<float?> GetSiteRating(Game game);
        public Task UploadGameImage(Game game, string basePath);
        public Task<bool> AddMatch(AppUser requestedFrom, Match match, List<PlayerDTO> players);
        public Task<IEnumerable<Match>> GetLoggedPlays(AppUser owner, Game game);
    }
}
