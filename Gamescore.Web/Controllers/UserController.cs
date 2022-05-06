using Gamescore.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;

using Gamescore.Domain.Entities;
using Gamescore.DAL;
using Gamescore.BLL.Services;

namespace Gamescore.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<GamesController> logger;
        private readonly IUserService service;

        public UserController(ILogger<GamesController> logger, IUserService service)
        {
            this.logger = logger;
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            return View(await service.GetUser(User));
        }

        [HttpGet]
        [Route("user/{name}")]
        public async Task<IActionResult> Index(string name)
        {
            var user = await service.GetUser(User, name);
            return user != null ? View(user) : NotFound();
        }

        [Route("users")]
        public async Task<IActionResult> AllUsers()
        {
            return View(await service.GetAll());
        }

        //public IActionResult AddToCollection()
        //{
        //    return PartialView("AddToCollection", null);
        //}

        [HttpPost]
        public async Task<IActionResult> AddToCollection(UserGameViewModel model)
        {
            if (ModelState.IsValid)
            {
                var added = await service.AddGame(User, model.Alias);
                //return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}