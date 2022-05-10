using Gamescore.Domain.Entities;

namespace Gamescore.DAL.Repositories
{
    public interface IUnitOfWork
    {
        public IRepository<Game> Games { get; }
        public IRepository<Match> Sessions { get; }
        public IRepository<AppUser> Users { get; }
        public IRepository<FriendRequest> FriendRequests { get; }
        public IRepository<Rating> Ratings { get; }
        public Task<int> Save();
    }
}
