using Gamescore.BLL.Services;
using Gamescore.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Gamescore.Web.Models
{
    public class MatchViewModel
    {
        public string GameAlias { get; set; }
        public DateTime Date { get; set; }
        public string Place { get; set; }
        public int Duration { get; set; }
        public string Comment { get; set; }
        public List<PlayerDTO> Players { get; set; }
    }
}
