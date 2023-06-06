using Microsoft.AspNetCore.Mvc;
using SportsManagment.API.Services.BodyMeasurmentsService;

namespace SportsManagment.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BodyMeasurementController : ControllerBase
    {
        private readonly IBodyMeasurmentService _bodyMeasurmentService;

        public BodyMeasurementController(IBodyMeasurmentService bodyMeasurmentService)
        {
            _bodyMeasurmentService = bodyMeasurmentService;
        }


        [HttpGet(Name = "GetAllBodyMeasurments")]
        public ActionResult<List<BodyMeasurement>> GetAll()
        {
            return _bodyMeasurmentService.GetAll();

        }

        [HttpPost(Name = "CreateABodyMeasurment")]
        public ActionResult<Guid> Create(BodyMeasurement bodyMeasurement)
        {
            var bodyMeasurmentId = _bodyMeasurmentService.Create(bodyMeasurement);

            return CreatedAtAction(nameof(GetById), new { id = bodyMeasurmentId }, bodyMeasurmentId);
        }

        [HttpDelete("{id}", Name = "DeleteABodyMeasurement")]
        public ActionResult Delete(Guid id)
        {
            var result = _bodyMeasurmentService.Delete(id);
            if (result == false)
            {
                return NotFound("This Body Measurment does not exist!");
            }
            return NoContent();
        }

        [HttpGet("{id}", Name = "GetBodyMeasurementById")]
        public ActionResult<BodyMeasurement> GetById(Guid id)
        {
            var result = _bodyMeasurmentService.GetById(id);
            if (result == null)
            {
                return NotFound("This Body Measurment does not exist!");
            }
            return Ok(result);
        }

        [HttpPut("{id}", Name = "UpdateBodyMeasurement")]
        public ActionResult<BodyMeasurement> Update(Guid id, BodyMeasurement updateBodyMeasurement)
        {
            var result = _bodyMeasurmentService.Update(id, updateBodyMeasurement);
            if (result == null)
            {
                return NotFound("This Body Measurment does not exist!");
            }
            return Ok(result);
        }


    }
}