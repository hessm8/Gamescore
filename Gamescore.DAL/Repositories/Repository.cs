using Gamescore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Gamescore.DAL.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly ApplicationDbContext context;
        protected DbSet<TEntity> Entities => context.Set<TEntity>();

        public Repository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>>? filter = null)
        {
            IQueryable<TEntity> query = Entities;
            if (filter != null) query = query.Where(filter);            

            return await query.ToListAsync();
        }

        public async Task<TEntity?> GetById(Guid id)
        {
            return await Entities.FindAsync(id);
        }

        public async Task<TEntity?> GetFirst(Expression<Func<TEntity, bool>> expression)
        {
            return await Entities.FirstOrDefaultAsync(expression);
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

        public async Task LoadCollection(TEntity item, string property)
        {
            await context.Entry(item).Collection(property).LoadAsync();
        }
    }
}
