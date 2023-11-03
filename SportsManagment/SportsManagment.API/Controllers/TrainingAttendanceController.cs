using Microsoft.AspNetCore.Mvc;
using SportsManagment.API.Services.TrainingAttendanceService;
using SportsManagment.Shared.Domain;

namespace SportsManagment.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TrainingAttendanceController : ControllerBase
{
    private readonly ITrainingAttendanceService _trainingAttendanceService;

    public TrainingAttendanceController(ITrainingAttendanceService trainingAttendanceService)
    {
        _trainingAttendanceService = trainingAttendanceService;
    }


    [HttpGet(Name = "GetAllTrainingAttendances")]
    public ActionResult<List<TrainingAttendance>> GetAll()
    {
        return _trainingAttendanceService.GetAll();

    }

    [HttpPost(Name = "CreateATrainingAttendance")]
    public ActionResult<Guid> Create(TrainingAttendance trainingAttendance)
    {
        var trainingAttendanceId = _trainingAttendanceService.Create(trainingAttendance);

        return CreatedAtAction(nameof(GetById), new { id = trainingAttendanceId }, trainingAttendanceId);
    }

    [HttpDelete("{id}", Name = "DeleteTrainingAttendance")]
    public ActionResult Delete(Guid id)
    {
        var isDeleted = _trainingAttendanceService.Delete(id);
        if (isDeleted == false)
        {
            return NotFound("This Training Attendance does not exist.");
        }
        return NoContent();
    }

    [HttpGet("{id}", Name = "GetTrainingAttendanceById")]
    public ActionResult<TrainingAttendance> GetById(Guid id)
    {
        var trainingAttendance = _trainingAttendanceService.GetById(id);
        if (trainingAttendance == null)
        {
            return NotFound("This Training Attendance does not exist.");
        }
        return Ok(trainingAttendance);
    }

    [HttpPut("{id}", Name = "UpdateTrainingAttendance")]
    public ActionResult<TrainingAttendance> Update(Guid id, TrainingAttendance updateTrainingAttendance)
    {
        var trainingAttendance = _trainingAttendanceService.Update(id, updateTrainingAttendance);
        if (trainingAttendance == null)
        {
            return NotFound("This Training Attendance does not exist.");
        }
        return Ok(trainingAttendance);
    }

    [HttpGet("player/{playerId}", Name = "GetAllTrainingAttendancesByPlayerId")]
    public ActionResult<List<TrainingAttendance>> GetAllTrainingAttendancesByPlayerId(Guid playerId, DateOnly? newerthen)
    {
        var trainingAttendances = _trainingAttendanceService.GetAllTrainingAttendancesByPlayerId(playerId, newerthen);
        if (trainingAttendances == null)
        {
            return NotFound("No training attendances found for the specified player.");
        }

        return Ok(trainingAttendances);
    }
}
