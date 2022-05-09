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
            Sessions = new Repository<Match>(context);
            Users = new Repository<AppUser>(context);
            FriendRequests = new Repository<FriendRequest>(context);
        }
        public IRepository<Game> Games { get; }
        public IRepository<Match> Sessions { get; }
        public IRepository<AppUser> Users { get; }
        public IRepository<FriendRequest> FriendRequests { get; }

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
