using Microsoft.AspNetCore.Mvc;

namespace SportsManagment.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlayerController : ControllerBase
    {
        private static List<Player> _players = new List<Player>();
        
        [HttpGet(Name = "GetAllPlayers")]
        public List<Player> GetAll()
        {
         return _players;
        }

        [HttpPost(Name = "CreateAPlayer")]
        public Guid Create(Player player)
        {
            _players.Add(player);
            return player.Id;
        }
    }
}