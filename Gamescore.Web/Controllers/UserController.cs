using Gamescore.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;

using Gamescore.Domain.Entities;
using Gamescore.Web.Data;

namespace Gamescore.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<GamesController> logger;
        private ApplicationDbContext context; // Will be replaced with services
        private UserManager<AppUser> userManager;

        public UserController(ILogger<GamesController> logger, UserManager<AppUser> userManager, ApplicationDbContext context)
        {
            this.logger = logger;
            this.userManager = userManager;
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await GetUser());
        }

        [HttpGet]
        [Route("user/{name}")]
        public async Task<IActionResult> Index(string name)
        {
            var user = await GetUser(name);
            return user != null ? View(user) : NotFound();
        }

        // Will move to a separate layer
        private async Task<AppUser?> GetUser(string? name = null)
        {
            AppUser? user;
            if (string.IsNullOrEmpty(name)) user = await userManager.GetUserAsync(User);
            else
            {
                user = await userManager.FindByNameAsync(name);
            }

            if (user != null) await context.Entry(user).Collection(x => x.GamesFavorited).LoadAsync();
            return user;
        }

        public async Task<IActionResult> AddGame(UserGameViewModel model)
        {
            logger.LogInformation("AddGame called");

            var game = context.Games.Where(x => x.Alias == model.Alias).FirstOrDefault();
            if (game == null) return NotFound();

            var user = await userManager.GetUserAsync(User);
            user.GamesFavorited.Add(game);
            await context.SaveChangesAsync();


            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}