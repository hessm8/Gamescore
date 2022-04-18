using Gamescore.Entities;

namespace Gamescore.Data.Repositories
{
    public interface IUnitOfWork
    {
        public IRepository<Game> Games { get; }
        public IRepository<Session> Sessions { get; }
        public IRepository<UserProfile> Profiles { get; }
        public Task<int> Save();
    }
}
