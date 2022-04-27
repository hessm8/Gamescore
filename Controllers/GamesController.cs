using Gamescore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

using Gamescore.Entities;
using Gamescore.Data;

namespace Gamescore.Controllers
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

        //public IActionResult Index()
        //{
        //    return View();
        //}

        public IActionResult Index()
        {           
            return View(context.Games.ToList());

            //return View(PlaceholderGames());
        }

        // Dummy games previously used to check games view
        private IEnumerable<Game> PlaceholderGames()
        {
            var games = new List<Game>();

            for (int i = 0; i < 10; i++)
            {
                games.Add(new Game()
                {
                    Alias = "game" + i,
                    Name = "The Game of Games and Something Else " + i,
                    NameLocalized = "Какая-то Игра и Что-то Еще " + i,
                    AgeMin = i,
                    ReleaseDate = 2015,
                    PlayersMin = 1,
                    PlayersMax = i,
                    DurationMin = 10,
                    DurationMax = 15 + i
                });
            }

            return games;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Game game)
        {
            if (ModelState.IsValid)
            {
                // Will replace EF context later
                context.Games.Add(game);
                await context.SaveChangesAsync();

                return RedirectToAction("Games");
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