using Gamescore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;

using Gamescore.Entities;
using Gamescore.Data;

namespace Gamescore.Controllers
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
            return View(await userManager.GetUserAsync(User));
        }

        [HttpGet]
        [Route("user/{name}")]
        public async Task<IActionResult> Index(string name)
        {
            var providedUser = await userManager.FindByNameAsync(name);
            if (providedUser == null) return NotFound();

            return View(providedUser);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}