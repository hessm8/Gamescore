using Gamescore.Domain.Entities;

namespace Gamescore.Web.Data.Repositories
{
    public interface IUnitOfWork
    {
        public IRepository<Game> Games { get; }
        public IRepository<Match> Sessions { get; }
        public Task<int> Save();
    }
}
