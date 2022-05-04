using Gamescore.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

using Gamescore.Web.Entities;
using Gamescore.Web.Data;

namespace Gamescore.Web.Controllers
{
    public class GamesController : Controller
    {
        private readonly ILogger<GamesController> logger;
        private ApplicationDbContext context; // Will be replaced with services

        public GamesController(ILogger<GamesController> logger, ApplicationDbContext context)
        {
            this.logger = logger;
            this.context = context;
        }

        public IActionResult Index()
        {           
            return View(context.Games.ToList());
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
                // Will replace EF context later
                context.Games.Add(game);
                await context.SaveChangesAsync();

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