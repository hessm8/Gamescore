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
            var user = await UserProfileViewModel.Create(service, User, null);
            return View(user);
        }

        [HttpGet]
        [Route("user/{username}")]
        public async Task<IActionResult> Index(string username)
        {
            //var user = await service.GetUser(User, username);
            var user = await UserProfileViewModel.Create(service, User, username);
            return user != null ? View("Index", user) : NotFound();
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

        public ActionResult AddToCollection()
        {
            var _objuserloginmodel = new UserGameViewModel();
            return View("AddToCollection", _objuserloginmodel);
        }

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

        [Route("user/{username}/friend")]
        public async Task<IActionResult> AddFriend(string username)
        {
            if (ModelState.IsValid)
            {
                var added = await service.AddFriend(User, username);
            }

            return RedirectToAction("Index", "user", new { username });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}