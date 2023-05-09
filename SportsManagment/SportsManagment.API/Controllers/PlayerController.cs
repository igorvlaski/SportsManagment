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

        [HttpDelete(Name = "DeleteAPlayer")]
        public IActionResult Delete(Guid id) 
        {
            var player = _players.FirstOrDefault(x => x.Id == id);

            if (player == null)
            {
                return NotFound();
            }
            
            _players.Remove(player);   

            return NoContent();
        }

        [HttpGet(Name = "GetPlayerBYId")]
        public IActionResult GetById(Guid id) 
        {
            var player = _players.FirstOrDefault(x => x.Id == id);

            if (player == null) 
            {
                return NotFound(); 
            }

            return Ok(player);
        }

        [HttpPut(Name = "UpdatePlayer")]
        public IActionResult Update(Guid id, Player updatePlayer)
        {
            var player = _players.FirstOrDefault(x=>x.Id == id);

            if(player == null)
            {
                return NotFound();
            }

            player.Id = updatePlayer.Id;
            player.FirstName = updatePlayer.FirstName;
            player.LastName = updatePlayer.LastName;
            player.DateOfBirth = updatePlayer.DateOfBirth;
            player.IsMonthlyFeePaid = updatePlayer.IsMonthlyFeePaid;
            player.IsYearlyFeePaid = updatePlayer.IsYearlyFeePaid;

            return Ok(player);
        }


    }
}