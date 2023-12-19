using Microsoft.AspNetCore.Mvc;
using SportsManagment.API.Services.PlayerService;
using SportsManagment.Shared.Domain;
using SportsManagment.Shared.DTOs;

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

    [HttpDelete("{id}", Name = "SoftDeleteAPlayer")]
    public ActionResult<Player> SoftDelete(Guid id)
    {
        var result = _playerService.SoftDelete(id);
        if (!result)
        {
            return NotFound("Player not found or already removed from the list.");
        }
        return NoContent();
    }

    [HttpDelete("{id}/HardDelete", Name = "DeleteAPlayer")]
    public ActionResult HardDelete(Guid id)
    {
        var player = _playerService.HardDelete(id);
        if (player == false)
        {
            return NotFound("This player is not on the roster.");
        }
        return NoContent();
    }

    [HttpPut("{id}/Restore", Name = "RestorePlayer")]
    public ActionResult Restore(Guid id)
    {
        var result = _playerService.Restore(id);
        if (!result)
        {
            return NotFound("Player not found or not temporary deleted.");
        }
        return NoContent();
    }

    [HttpGet("Deleted", Name = "GetDeletedPlayers")]
    public ActionResult<List<Player>> GetDeletedPlayers()
    {
        var deletedPlayers = _playerService.GetDeletedPlayers();
        if (deletedPlayers == null || !deletedPlayers.Any())
        {
            return NotFound("No temporary deleted players found.");
        }
        return Ok(deletedPlayers);
    }

    [HttpGet("{id}", Name = "GetPlayerById")]
    public ActionResult<Player> GetById(Guid id, DateOnly? newerthen, 
                                                 bool includePlayerMeasurements, 
                                                 bool includePaymentInformations, 
                                                 bool includeSelections,
                                                 bool includeTrainingAttendances)
    {
        var player = _playerService.GetById(id, newerthen, 
                                                includePlayerMeasurements, 
                                                includePaymentInformations, 
                                                includeSelections, 
                                                includeTrainingAttendances);
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