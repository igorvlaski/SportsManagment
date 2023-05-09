using Microsoft.AspNetCore.Mvc;
using SportsManagment.API.Domain;

namespace SportsManagment.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlayerController : ControllerBase
    {
        private static List<Player> _players = new List<Player>();
        
        [HttpGet(Name = "GetAllPlayers")]
        public ActionResult<List<Player>> GetAll()
        {
         return Ok(_players);
        }

        [HttpPost(Name = "CreateAPlayer")]
        public ActionResult<Guid> Create(Player player)
        {
            _players.Add(player);
            return CreatedAtAction(nameof(GetById), new { id = player.Id }, player.Id);
        }

        [HttpDelete("{id}", Name = "DeleteAPlayer")]
        public ActionResult Delete(Guid id) 
        {
            var player = _players.FirstOrDefault(x => x.Id == id);

            if (player == null)
            {
                return NotFound("This player is not on the roster.");
            }
            
            _players.Remove(player);   

            return NoContent();
        }

        [HttpGet("{id}",Name = "GetPlayerBYId")]
        public ActionResult<Player> GetById(Guid id) 
        {
            var player = _players.FirstOrDefault(x => x.Id == id);

            if (player == null) 
            {
                return NotFound("This player is not on the roster."); 
            }

            return Ok(player);
        }

        [HttpPut("{id}",Name = "UpdatePlayer")]
        public ActionResult<Player> Update(Guid id, Player updatePlayer)
        {
            var player = _players.FirstOrDefault(x=>x.Id == id);

            if(player == null)
            {
                return NotFound("This player is not on the roster.");
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