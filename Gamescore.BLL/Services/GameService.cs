using Gamescore.DAL.Repositories;
using Gamescore.Domain.Entities;

namespace Gamescore.BLL.Services
{
    public class GameService : BaseService, IGameService
    {
        public GameService(IUnitOfWork uow) : base(uow) { }

        public async Task<IEnumerable<Game>> GetAll()
        {
            return await uow.Games.GetAll();
        }

        public async Task AddGame(Game game)
        {
            await uow.Games.Create(game);
            await uow.Save();
        }
    }

    public interface IGameService
    {
        public Task<IEnumerable<Game>> GetAll();
        public Task AddGame(Game game);
    }
}
