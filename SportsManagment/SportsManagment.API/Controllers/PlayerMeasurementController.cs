using Microsoft.AspNetCore.Mvc;
using SportsManagment.API.Services.PlayerMeasurementService;

namespace SportsManagment.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlayerMeasurementController : ControllerBase
    {
        private readonly IPlayerMeasurementService _playerMeasurementService;

        public PlayerMeasurementController(IPlayerMeasurementService playerMeasurementService)
        {
            _playerMeasurementService = playerMeasurementService;
        }


        [HttpGet(Name = "GetAllPlayerMeasurements")]
        public ActionResult<List<PlayerMeasurement>> GetAll()
        {
            return _playerMeasurementService.GetAll();

        }

        [HttpPost(Name = "CreateAPlayerMeasurement")]
        public ActionResult<Guid> Create(PlayerMeasurement playerMeasurement)
        {
            var playerMeasurementId = _playerMeasurementService.Create(playerMeasurement);

            return CreatedAtAction(nameof(GetById), new { id = playerMeasurementId }, playerMeasurementId);
        }

        [HttpGet("{id}", Name = "GetPlayerMeasurementById")]
        public ActionResult<PlayerMeasurement> GetById(Guid id)
        {
            var result = _playerMeasurementService.GetById(id);
            if (result == null)
            {
                return NotFound("This Player Measurement does not exist.");
            }
            return Ok(result);
        }

        [HttpDelete("{id}", Name = "DeletePlayerMeasurement")]
        public ActionResult Delete(Guid id)
        {
            var result = _playerMeasurementService.Delete(id);
            if (result == false)
            {
                return NotFound("This Player Measurement does not exist.");
            }
            return NoContent();
        }

        [HttpPut("{id}", Name = "UpdatePlayerMeasurement")]
        public ActionResult<PlayerMeasurement> Update(Guid id, PlayerMeasurement updatePlayerMeasurement)
        {
            var result = _playerMeasurementService.Update(id, updatePlayerMeasurement);
            if (result == null)
            {
                return NotFound("This Player Measurement does not exist.");
            }
            return Ok(result);
        }

        [HttpGet("player/{playerId}", Name = "GetAllPlayerMeasurementByPlayerId")]
        public ActionResult<List<PlayerMeasurement>> GetAllPlayerMeasurementByPlayerId(Guid playerId)
        {
            var result = _playerMeasurementService.GetAllPlayerMeasurementsByPlayerId(playerId);

            if (result == null)
            {
                return NotFound("No player measurement found for the specified player.");
            }

            return Ok(result);
        }
    }
}
