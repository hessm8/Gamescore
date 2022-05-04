using Gamescore.Entities;

namespace Gamescore.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext context;
        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
            Games = new Repository<Game>(context);
            Sessions = new Repository<Match>(context);
        }
        public IRepository<Game> Games { get; }
        public IRepository<Match> Sessions { get; }

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
