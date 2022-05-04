using Gamescore.Web.Data.Repositories;
using Gamescore.Web.Entities;

namespace Gamescore.Web.Logic
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
