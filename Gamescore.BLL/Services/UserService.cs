using Gamescore.DAL;
using Gamescore.DAL.Repositories;
using Gamescore.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<AppUser>> GetAll()
        {
            return await userManager.Users.ToListAsync();
        }

        public async Task<AppUser?> GetUser(ClaimsPrincipal User)
        {
            var user = await userManager.GetUserAsync(User);
            return await GetUserWithData(user);
        }

        public async Task<AppUser?> GetUser(string username)
        {
            var user = await userManager.FindByNameAsync(username);
            return await GetUserWithData(user);
        }
        
        private async Task<AppUser?> GetUserWithData(AppUser? user)
        {
            if (user == null) return null;

            user = await uow.Users
                .Include("GamesFavorited")
                .Include("GamesRated")

                .Include("SentFriendRequests")
                //.Include("SentFriendRequests.SentBy")
                //.Include("SentFriendRequests.SentTo")

                .Include("ReceievedFriendRequests")
                //.Include("ReceievedFriendRequests.SentBy")
                //.Include("ReceievedFriendRequests.SentTo")

                .GetFirst(u => u.Id == user.Id);

            return user;
        }

        public async Task<IEnumerable<AppUser>> GetFriends(AppUser user)
        {
            // Implement later or most likely use user.Friends property instead
            return null;
        }

        public (FriendStatus status, bool received)? GetFriendStatus(AppUser me, AppUser friend)
        {
            var sent = me.SentFriendRequests.FirstOrDefault(fr => fr.SentToId == friend.Id);
            var received = me.ReceievedFriendRequests.FirstOrDefault(fr => fr.SentById == friend.Id);

            if (sent != null) return (sent.Status, false);
            else if (received != null) return (received.Status, true);
            else return null;
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

        public async Task<bool> ManageFriendRequest(ClaimsPrincipal claims, string username, string requestAction)
        {

            // Note: this probably needs some cleanup. Haven't tested this again since implementing the UserProfileViewModel and other service things...

            //var myUser = await userManager.GetUserAsync(claims);
            //var friendUser = await userManager.FindByNameAsync(username);

            var myUser = await GetUser(claims);
            var friendUser = await GetUser(username);

            if (friendUser == null || myUser.UserName == friendUser.UserName) return false;

            return requestAction switch
            {
                "add" => await AddFriendRequest(myUser, friendUser),
                "accept" => await AcceptFriendRequest(myUser, friendUser),
                _ => false
            };
        }

        private async Task<bool> AddFriendRequest(AppUser myUser, AppUser friendUser)
        {
            // REPLACED WITH INCLUDE --> await uow.Users.LoadCollection(myUser, "SentFriendRequests");
            //if (Fr) throw new Exception("friend request already sent");

            myUser.FriendWith(friendUser);
            await uow.Save();

            return true;
        }

        private async Task<bool> AcceptFriendRequest(AppUser myUser, AppUser friendUser)
        {

            var friendRequest = myUser.ReceievedFriendRequests.FirstOrDefault(fr => fr.SentById == friendUser.Id);
            friendRequest.Status = FriendStatus.Approved;
            await uow.Save();

            return true;
        }
    }

    public interface IUserService
    {
        public Task<IEnumerable<AppUser>> GetAll();
        public Task<bool> AddGame(ClaimsPrincipal User, string alias);
        //public Task<AppUser?> GetUser(ClaimsPrincipal User, string? name = null);
        public Task<bool> ManageFriendRequest(ClaimsPrincipal User, string name, string action);
        Task<IEnumerable<AppUser>> GetFriends(AppUser user);
        Task<AppUser> GetUser(string? username);
        Task<AppUser> GetUser(ClaimsPrincipal claims);
        (FriendStatus status, bool received)? GetFriendStatus(AppUser me, AppUser user);
    }
}
