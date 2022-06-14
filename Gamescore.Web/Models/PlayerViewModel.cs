using Gamescore.BLL.Services;
using Gamescore.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Gamescore.Web.Models
{
    public class PlayerViewModel
    {
        public string UserName { get; set; }
        public bool Registered { get; set; }
        public string Team { get; set; }
        public int Points { get; set; }
        public bool IsWinner { get; set; }
    }
}
