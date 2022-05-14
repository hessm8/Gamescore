using Gamescore.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Web;

using Gamescore.Domain.Entities;
using Gamescore.DAL;
using Gamescore.BLL.Services;

namespace Gamescore.Web.Controllers
{
    public class GamesController : Controller
    {
        private readonly ILogger<GamesController> logger;
        private readonly IGameService gameService;
        private readonly IUserService userService;
        private readonly IWebHostEnvironment webHostEnvironment;

        public GamesController(ILogger<GamesController> logger, 
            IGameService gameService, IUserService userService,
            IWebHostEnvironment webHostEnvironment)
        {
            this.logger = logger;
            this.gameService = gameService;
            this.userService = userService;
            this.webHostEnvironment = webHostEnvironment;
        }

        // List of all games accessed through nav menu
        public async Task<IActionResult> Index()
        {
            ViewBag.LoggedIn = User.Identity.IsAuthenticated;
            return View(await gameService.GetAll());
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Game game)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }

            if (ModelState.IsValid)
            {               
                var wasAdded = await gameService.AddGame(game);
                if (!wasAdded)
                {
                    ModelState.AddModelError("Alias", "The game under this alias or name already exists");
                    return View(game);
                }

                await gameService.UploadGameImage(game, webHostEnvironment.WebRootPath);
                return RedirectToAction("Index");
            }

            return View(game);
        }

        #region Game profile

        [HttpGet]
        [Route("game/{alias}")]
        public async Task<IActionResult> GameProfile(string alias)
        {
            var game = await gameService.GetByName(alias);
            if (game == null) return NotFound();

            var user = await userService.GetUser(User);

            var model = await GameProfileViewModel.Create(gameService, game, user);
            return View(model);
        }

        [Route("game/{alias}/rate")]
        public async Task<IActionResult> RateGame(string alias, int rating)
        {
            var game = await gameService.GetByName(alias);
            if (game == null) return NotFound();

            var user = await userService.GetUser(User);
            if (user == null) return RedirectToAction("Login", "Account", new { area = "Identity" });

            await gameService.RateGame(game, user, rating);

            return RedirectToAction("GameProfile", "games", new { alias });
        }

        public async Task<IActionResult> AddToCollection(string alias)
        {
            var user = await userService.GetUser(User);
            if (user == null) return RedirectToAction("Login", "Account", new { area = "Identity" });

            await userService.AddToCollection(user, alias);

            return RedirectToAction("GameProfile", "games", new { alias });
        }

        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}