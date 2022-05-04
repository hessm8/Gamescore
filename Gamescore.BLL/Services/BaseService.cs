using Gamescore.DAL.Repositories;

namespace Gamescore.BLL.Services
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
