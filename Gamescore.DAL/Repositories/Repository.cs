using Gamescore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Gamescore.DAL.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly ApplicationDbContext context;
        protected DbSet<TEntity> Entities;
        protected IQueryable<TEntity> Query;

        public Repository(ApplicationDbContext context)
        {
            this.context = context;
            Entities = context.Set<TEntity>();
            Query = Entities;
        }

        public async Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>>? filter = null, params string[] includes)
        {
            //IQueryable<TEntity> Query = Entities;

            foreach (var include in includes)
            {
                Query = Query.Include(include);
            }

            if (filter != null) Query = Query.Where(filter); 

            return await Query.ToListAsync();
        }

        public async Task<TEntity?> GetById(Guid id)
        {
            return await Entities.FindAsync(id);
        }

        public async Task<TEntity?> GetFirst(Expression<Func<TEntity, bool>> expression)
        {
            return await Query.FirstOrDefaultAsync(expression);
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

        public Repository<TEntity> Include(string property)
        {
            //Entities = (DbSet<TEntity>)Entities.Include(property);
            Query = Query.Include(property);

            return this;
        }

        //public Repository<TEntity> Include<TProperty>(string include)
        //{
        //    Query = Query.Include(include);
        //    return this;
        //}
    }
}
