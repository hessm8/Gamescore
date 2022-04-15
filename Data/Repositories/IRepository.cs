namespace Gamescore.Data.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity?> Get(int id);
        Task<TEntity> Create(TEntity item);
        void Update(TEntity item);
        void Delete(TEntity item);
    }
}
