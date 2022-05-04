using Gamescore.DAL.Repositories;
using Gamescore.Domain.Entities;

namespace Gamescore.BLL.Services
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
