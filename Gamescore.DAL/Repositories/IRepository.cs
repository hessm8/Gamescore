using System.Linq.Expressions;

namespace Gamescore.DAL.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>>? filter = null, 
            params string[] includes);

        Task<TEntity?> GetById(Guid id);
        Task<TEntity?> GetFirst(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> Create(TEntity item);
        void Update(TEntity item);
        void Delete(TEntity item);
        
        // Might change to a proper method related to Include
        public Task LoadCollection(TEntity item, string property);
        public Repository<TEntity> Include(string property);
        //public Repository<TEntity> Include<TProperty>(Expression<Func<TEntity, TProperty>> property); 
    }
}
