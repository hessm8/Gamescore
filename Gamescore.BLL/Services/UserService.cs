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

        #region Receive

        public async Task<IEnumerable<AppUser>> GetAll()
        {
            return await GetLoadedUsers().GetAll();
        }

        public async Task<IEnumerable<string>> GetSearchUsers(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return await userManager.Users
                    .Select(u => u.UserName)
                    .Take(10)
                    .ToListAsync();
            }

            return await userManager.Users
                .Where(u => u.UserName.Contains(search))
                .Select(u => u.UserName)
                .Take(10)
                .ToListAsync();
        }

        public async Task<AppUser?> GetUser(ClaimsPrincipal claims)
        {
            if (!claims.Identity.IsAuthenticated) return null;

            var user = await userManager.GetUserAsync(claims);
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

            user = await GetLoadedUsers().GetFirst(u => u.Id == user.Id);

            return user;
        }

        private Repository<AppUser> GetLoadedUsers()
        {
            return uow.Users
                .Include("GamesFavorited")
                .Include("GamesRated")
                .Include("SentFriendRequests")
                .Include("ReceievedFriendRequests");
        }

        #endregion

        public async Task<IEnumerable<AppUser>> GetFriends(AppUser user)
        {
            var friends = new List<AppUser>();
            foreach (var request in user.Friends)
            {
                Guid friendId = request.FriendOf(user);
                var friendUser = await uow.Users.GetFirst(u => u.Id == friendId);
                friends.Add(friendUser!);
            }

            return friends;
        }

        public (FriendStatus status, bool received)? GetFriendStatus(AppUser me, AppUser friend)
        {
            if (me == null) return null;

            var sent = me.SentFriendRequests.FirstOrDefault(fr => fr.SentToId == friend.Id);
            var received = me.ReceievedFriendRequests.FirstOrDefault(fr => fr.SentById == friend.Id);

            if (sent != null) return (sent.Status, false);
            else if (received != null) return (received.Status, true);
            else return null;
        }

        public async Task<bool> AddToCollection(AppUser user, string alias)
        {
            var game = await uow.Games.GetFirst(x => x.Alias == alias);
            if (game == null) return false;

            if (!user.GamesFavorited.Contains(game)) user.GamesFavorited.Add(game); 
            else user.GamesFavorited.Remove(game);

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
                "decline" => await DeclineFriendRequest(myUser, friendUser),
                _ => false
            };
        }

        private async Task<bool> AddFriendRequest(AppUser myUser, AppUser friendUser)
        {
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

        private async Task<bool> DeclineFriendRequest(AppUser myUser, AppUser friendUser)
        {
            var friendRequest = myUser.ReceievedFriendRequests.FirstOrDefault(fr => fr.SentById == friendUser.Id);
            friendRequest.Status = FriendStatus.Rejected;
            await uow.Save();

            return true;
        }

        public async Task<IEnumerable<Match>> GetLoggedPlays(AppUser owner)
        {
            var matches = await uow.Matches.GetAll(
                m => m.Players.Any(
                    mp => mp.Player.UserPlayerId == owner.Id
                ),
                "Game", "Players", "Players.Player"
            );

            return matches;
        }
    }

    public interface IUserService
    {
        public Task<IEnumerable<AppUser>> GetAll();
        public Task<IEnumerable<string>> GetSearchUsers(string search);
        public Task<bool> AddToCollection(AppUser user, string alias);
        public Task<IEnumerable<Match>> GetLoggedPlays(AppUser owner);
        public Task<bool> ManageFriendRequest(ClaimsPrincipal User, string name, string action);
        Task<IEnumerable<AppUser>> GetFriends(AppUser user);
        Task<AppUser?> GetUser(string username);
        Task<AppUser?> GetUser(ClaimsPrincipal claims);
        (FriendStatus status, bool received)? GetFriendStatus(AppUser me, AppUser user);
    }
}
