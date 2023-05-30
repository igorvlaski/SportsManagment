using Microsoft.AspNetCore.Mvc;

namespace SportsManagment.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerService _playerService;

        public PlayerController(IPlayerService playerService)
        {
            _playerService = playerService;
        }


        [HttpGet(Name = "GetAllPlayers")]
        public ActionResult<List<Player>> GetAll()
        {
            return _playerService.GetAll();

        }

        [HttpPost(Name = "CreateAPlayer")]
        public ActionResult<Guid> Create(Player player)
        {
            var playerId = _playerService.Create(player);

            return CreatedAtAction(nameof(GetById), new { id = playerId }, playerId);
        }

        [HttpDelete("{id}", Name = "DeleteAPlayer")]
        public ActionResult Delete(Guid id)
        {
            var result = _playerService.Delete(id);
            if (result == false)
            {
                return NotFound("This player is not on the roster.");
            }
            return NoContent();
        }

        [HttpGet("{id}", Name = "GetPlayerBYId")]
        public ActionResult<Player> GetById(Guid id)
        {
            var result = _playerService.GetById(id);
            if (result == null)
            {
                return NotFound("This player is not on the roster.");
            }
            return Ok(result);
        }

        [HttpPut("{id}", Name = "UpdatePlayer")]
        public ActionResult<Player> Update(Guid id, Player updatePlayer)
        {
            var result = _playerService.Update(id, updatePlayer);
            if (result == null)
            {
                return NotFound("This player is not on the roster.");
            }
            return Ok(result);
        }


    }
}