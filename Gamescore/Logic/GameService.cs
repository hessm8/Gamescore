using Gamescore.Data.Repositories;
using Gamescore.Entities;

namespace Gamescore.Logic
{
    public class GameService : BaseService
    {
        public GameService(IUnitOfWork uow) : base(uow) { }

        public async Task<IEnumerable<Game>> GetAll()
        {
            return await uow.Games.GetAll();
        }
    }
}
