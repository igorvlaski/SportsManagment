using Microsoft.AspNetCore.Mvc;
using SportsManagment.API.Services.TrainingAttendanceService;

namespace SportsManagment.API.Controllers
{
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
            var result = _trainingAttendanceService.Delete(id);
            if (result == false)
            {
                return NotFound("This Training Attendance does not exist.");
            }
            return NoContent();
        }

        [HttpGet("{id}", Name = "GetTrainingAttendanceBYId")]
        public ActionResult<TrainingAttendance> GetById(Guid id)
        {
            var result = _trainingAttendanceService.GetById(id);
            if (result == null)
            {
                return NotFound("This Training Attendance does not exist.");
            }
            return Ok(result);
        }

        [HttpPut("{id}", Name = "UpdateTrainingAttendance")]
        public ActionResult<TrainingAttendance> Update(Guid id, TrainingAttendance updateTrainingAttendance)
        {
            var result = _trainingAttendanceService.Update(id, updateTrainingAttendance);
            if (result == null)
            {
                return NotFound("This Training Attendance does not exist.");
            }
            return Ok(result);
        }


    }
}
