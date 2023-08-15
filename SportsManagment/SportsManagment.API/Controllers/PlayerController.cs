using Microsoft.AspNetCore.Mvc;
using SportsManagment.API.DTOs;

namespace SportsManagment.API.Controllers;

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
    public ActionResult<Guid> Create(CreatePlayerDTO player)
    {
        var playerId = _playerService.Create(player);

        return CreatedAtAction(nameof(GetById), new { id = playerId }, playerId);
    }

    [HttpDelete("{id}", Name = "DeleteAPlayer")]
    public ActionResult Delete(Guid id)
    {
        var player = _playerService.Delete(id);
        if (player == false)
        {
            return NotFound("This player is not on the roster.");
        }
        return NoContent();
    }

    [HttpGet("{id}", Name = "GetPlayerById")]
    public ActionResult<Player> GetById(Guid id, DateOnly? newerthen, bool includePlayerMeasurements)
    {
        var player = _playerService.GetById(id, newerthen, includePlayerMeasurements);
        if (player == null)
        {
            return NotFound("This player is not on the roster.");
        }
        return Ok(player);
    }

    [HttpPut("{id}", Name = "UpdatePlayer")]
    public ActionResult<Player> Update(Guid id, UpdatePlayerDTO updatePlayer)
    {
        var player = _playerService.Update(id, updatePlayer);
        if (player == null)
        {
            return NotFound("This player is not on the roster.");
        }
        return Ok(player);
    }

    [HttpPatch("{playerId}/selection/{selectionId}", Name = "AddOrRemovePlayerFromSelection")]
    public ActionResult<bool> AddOrRemovePlayerFromSelection(Guid playerId, Guid selectionId)
    {
        var player = _playerService.AddOrRemovePlayerFromSelection(playerId, selectionId);
        if (!player)
        {
            return BadRequest("This player or selection does not exist.");
        }

        return Ok(true);
    }
}