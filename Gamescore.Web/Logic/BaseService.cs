using Gamescore.Web.Data.Repositories;

namespace Gamescore.Web.Logic
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
