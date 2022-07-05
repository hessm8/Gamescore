using Gamescore.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Web;

using Gamescore.Domain.Entities;
using Gamescore.DAL;
using Gamescore.BLL.Services;
using Microsoft.AspNetCore.Authorization;

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

        #region Manage

        [HttpGet, Authorize]
        public async Task<IActionResult> Manage(string? alias = null)
        {
            if (alias == null)
            {
                return View(new ManageGameViewModel(new Game(), false));
            }

            var game = await gameService.GetByName(alias);
            if (game == null) return NotFound();

            var model = new ManageGameViewModel(game, true);

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Manage(ManageGameViewModel model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }

            if (ModelState.IsValid)
            {
                logger.LogInformation(model.IsEditing.ToString());
                if (model.IsEditing)
                {
                    var wasEdited = await gameService.EditGame(model.Game);
                    if (model.Game.ImageFile != null) await gameService.UploadGameImage(model.Game, webHostEnvironment.WebRootPath);

                    //await gameService.UploadGameImage(game, webHostEnvironment.WebRootPath);
                    return RedirectToAction("GameProfile", "games", new { alias = model.Game.Alias });
                }

                var wasAdded = await gameService.AddGame(model.Game);
                if (!wasAdded)
                {
                    ModelState.AddModelError("Alias", "The game under this alias or name already exists");

                    return View(model);
                }

                await gameService.UploadGameImage(model.Game, webHostEnvironment.WebRootPath);
                return RedirectToAction("Index");
            }

            return View(model);
        }
        #endregion

        #region Game profile

        [HttpGet, Route("game/{alias}")]
        public async Task<IActionResult> GameProfile(string alias)
        {
            var game = await gameService.GetByName(alias);
            if (game == null) return NotFound();

            var user = await userService.GetUser(User);

            var model = await GameProfileViewModel.Create(gameService, game, user);
            return View(model);
        }

        [Route("game/{alias}/rate")]
        [Authorize]
        public async Task<IActionResult> RateGame(string alias, int rating)
        {
            var game = await gameService.GetByName(alias);
            if (game == null) return NotFound();

            var user = await userService.GetUser(User);

            await gameService.RateGame(game, user!, rating);

            return RedirectToAction("GameProfile", "games", new { alias });
        }

        [Authorize]
        public async Task<IActionResult> AddToCollection(string alias)
        {
            var user = await userService.GetUser(User);
            await userService.AddToCollection(user!, alias);

            return RedirectToAction("GameProfile", "games", new { alias });
        }

        #endregion

        #region Matches

        [HttpGet, Route("game/{alias}/match")]
        [Authorize]
        public async Task<IActionResult> Match(string? alias = null)
        {
            if (alias == null)
            {
                return RedirectToAction("Index", "games");
            }

            var game = await gameService.GetByName(alias);
            if (game == null) return NotFound();

            var model = new MatchViewModel()
            {
                GameAlias = alias
            };

            return View(model);
        }

        public async Task<IActionResult> GetSearchUsers(string search)
        {
            var users = await userService.GetSearchUsers(search);
            return Ok(users);
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> SubmitMatch([FromBody] MatchViewModel viewModel)
        {
            var requestedFrom = await userService.GetUser(User);

            var game = await gameService.GetByName(viewModel.GameAlias);
            if (game == null) return BadRequest();

            var match = new Match()
            {
                Game = game,
                Comment = viewModel.Comment,
                Date = viewModel.Date,
                Duration = viewModel.Duration,
                Place = viewModel.Place
            };

            await gameService.AddMatch(requestedFrom!, match, viewModel.Players);

            return Ok(viewModel);
        }

        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}