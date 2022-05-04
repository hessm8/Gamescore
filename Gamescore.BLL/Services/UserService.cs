using Gamescore.DAL;
using Gamescore.DAL.Repositories;
using Gamescore.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Gamescore.BLL.Services
{
    public class UserService : BaseService, IUserService
    {
        private UserManager<AppUser> userManager;
        public UserService(IUnitOfWork uow, UserManager<AppUser> userManager) 
            : base(uow) {
            this.userManager = userManager;
        }

        public async Task<IEnumerable<Game>> GetAll()
        {
            return await uow.Games.GetAll();
        }

        public async Task AddGame(Game game)
        {
            await uow.Games.Create(game);
            await uow.Save();
        }

        //ApplicationDbContext context;
        public async Task<AppUser?> GetUser(ClaimsPrincipal User, string? name = null)
        {
            AppUser? user;
            if (string.IsNullOrEmpty(name)) user = await userManager.GetUserAsync(User);
            else
            {
                user = await userManager.FindByNameAsync(name);
            }

            if (user != null) await uow.Users.LoadCollection(user, "GamesFavorited");

            return user;
        }

        public async Task<bool> AddGame(ClaimsPrincipal User, string alias)
        {
            // filtering not in query
            var game = uow.Games.GetAll().Result.Where(x => x.Alias == alias).FirstOrDefault();

            if (game == null) return false;

            var user = await userManager.GetUserAsync(User);
            user.GamesFavorited.Add(game);
            await uow.Save();

            return true;
        }
    }

    public interface IUserService
    {
        public Task<IEnumerable<Game>> GetAll();
        public Task<bool> AddGame(ClaimsPrincipal User, string alias);
        public Task<AppUser?> GetUser(ClaimsPrincipal User, string? name = null);
    }
}
