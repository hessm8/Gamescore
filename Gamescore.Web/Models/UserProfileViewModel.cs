using Gamescore.BLL.Services;
using Gamescore.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Gamescore.Web.Models
{
    public class UserProfileViewModel
    {
        //private readonly IUserService service;

        public static async Task<UserProfileViewModel?> Create(IUserService service,
            ClaimsPrincipal claims, string? username = null)
        {
            var model = new UserProfileViewModel();

            model.Me = await service.GetUser(claims);
            model.User = username != null ? await service.GetUser(username) : model.Me;
            
            if (model.User == null) return null;

            model.LoggedIn = claims.Identity.IsAuthenticated;
            model.IsMe = model.LoggedIn ? model.User.UserName == claims.Identity.Name : false;

            model.Friendship = service.GetFriendStatus(model.Me, model.User);

            model.Friends = await service.GetFriends(model.User);

            return model;
        }

        public AppUser User { get; set; } = null!;
        public AppUser Me { get; set; } = null!;
        public bool IsMe { get; set; } 
        public bool LoggedIn { get; set; }

        public IEnumerable<Game> Collection { get; set; } = new List<Game>();

        public IEnumerable<AppUser> Friends { get; set; } = new List<AppUser>();

        public (FriendStatus status, bool received)? Friendship { get; set; }
    }
}
