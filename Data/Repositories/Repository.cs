using Gamescore.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gamescore.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly ApplicationDbContext context;
        protected DbSet<TEntity> Entities => context.Set<TEntity>();

        public Repository(ApplicationDbContext appDbContext)
        {
            context = appDbContext;
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await Entities.ToListAsync();
        }

        public async Task<TEntity?> Get(Guid id)
        {
            return await Entities.FindAsync(id);
        }

        public async Task<TEntity> Create(TEntity entity)
        {
            var entry = await Entities.AddAsync(entity);
            return entry.Entity;
        }

        public void Update(TEntity entity)
        {
            Entities.Update(entity);
        }

        public void Delete(TEntity item)
        {
            Entities.Remove(item);
        }
    }
}
