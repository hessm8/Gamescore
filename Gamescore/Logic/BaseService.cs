using Gamescore.Data.Repositories;

namespace Gamescore.Logic
{
    public class BaseService
    {
        protected readonly IUnitOfWork uow;
        public BaseService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

    }
}
