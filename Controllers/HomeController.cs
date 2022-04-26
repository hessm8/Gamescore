using Gamescore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

using Gamescore.Entities;

namespace Gamescore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Games()
        {           
            return View(GenerateGames());
        }

        // Dummy games
        private IEnumerable<Game> GenerateGames()
        {
            var games = new List<Game>();

            for (int i = 0; i < 15; i++)
            {
                games.Add(new Game()
                {
                    Alias = "game" + i,
                    Name = "The Game of Chess and Something Else " + i,
                    NameLocalized = "Какая-то Игра и Что-то Еще " + i,
                    AgeMin = i,
                    ReleaseDate = DateTime.Now,
                    PlayersMin = 1,
                    PlayersMax = i,
                    DurationMin = 10,
                    DurationMax = 15 + i,

                });
            }

            return games;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}