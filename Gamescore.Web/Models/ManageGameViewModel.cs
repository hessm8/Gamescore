using Gamescore.BLL.Services;
using Gamescore.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Gamescore.Web.Models
{
    public class ManageGameViewModel
    {
        public ManageGameViewModel(Game? game, bool editing)
        {
            Game = game;
            IsEditing = editing;
        }

        public ManageGameViewModel() { }

        public Game? Game { get; set; }
        public bool IsEditing { get; set; }
    }
}
