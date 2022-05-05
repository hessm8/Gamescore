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
        public async Task<IActionResult> GamePage(string name)
        {
            var user = await service.GetByName(name);
            return user != null ? View(user) : NotFound();
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
                await service.AddGame(game);
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