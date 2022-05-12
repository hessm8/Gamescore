using Gamescore.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

using Gamescore.Domain.Entities;
using Gamescore.DAL;
using Gamescore.BLL.Services;

namespace Gamescore.Web.Controllers
{
    public class GamesController : Controller
    {
        private readonly ILogger<GamesController> logger;
        private readonly IGameService service;

        public GamesController(ILogger<GamesController> logger, IGameService service)
        {
            this.logger = logger;
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {           
            return View(await service.GetAll());
        }

        [HttpGet]
        [Route("game/{name}")]
        public async Task<IActionResult> GameProfile(string name)
        {
            var game = await service.GetByName(name);
            var model = await GameProfileViewModel.Create(service, game, User);
            return game != null ? View(model) : NotFound();
        }

        [Route("game/{name}/rate")]
        public async Task<IActionResult> RateGame(string name, int rating)
        {
            var game = await service.GetByName(name);
            if (game == null) return NotFound();

            var authorized = await service.RateGame(User, game, rating);
            if (!authorized) return RedirectToAction("Login", "Account", new { area = "Identity" });

            return RedirectToAction("GameProfile", "games", new { name });
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Game game)
        {
            if (ModelState.IsValid)
            {
                var wasAdded = await service.AddGame(game);
                if (!wasAdded)
                {
                    ModelState.AddModelError("Alias", "The game under this alias or name already exists");
                    return View(game);
                }

                return RedirectToAction("Index");
            }

            return View(game);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}