using Gamescore.Domain.Entities;

namespace Gamescore.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext context;
        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
            Games = new Repository<Game>(context);
            Matches = new Repository<Match>(context);
            Players = new Repository<Player>(context);
            Users = new Repository<AppUser>(context);
            FriendRequests = new Repository<FriendRequest>(context);
            Ratings = new Repository<Rating>(context);
        }
        public IRepository<Game> Games { get; }
        public IRepository<Match> Matches { get; }
        public IRepository<Player> Players { get; }
        public IRepository<AppUser> Users { get; }
        public IRepository<FriendRequest> FriendRequests { get; }
        public IRepository<Rating> Ratings { get; }

        public async Task<int> Save() => await context.SaveChangesAsync();

        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing) context.Dispose();
                disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
