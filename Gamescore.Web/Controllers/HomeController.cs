using Gamescore.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

using Gamescore.Domain.Entities;
using Gamescore.DAL;
using Gamescore.BLL.Services;

namespace Gamescore.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IGameService gameService;

        public HomeController(ILogger<HomeController> logger, IGameService gameService)
        {
            this.logger = logger;
            this.gameService = gameService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await gameService.GetAll();
            return View(result.Take(10));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}