using Microsoft.AspNetCore.Mvc;
using SportsManagment.API.Services.PerformanceMeasurementService;

namespace SportsManagment.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PerformanceMeasurementController : ControllerBase
    {
        private readonly IPerformanceMeasurementService _performanceMeasurementService;

        public PerformanceMeasurementController(IPerformanceMeasurementService performanceMeasurementService)
        {
            _performanceMeasurementService = performanceMeasurementService;
        }


        [HttpGet(Name = "GetAllPerformanceMeasurments")]
        public ActionResult<List<PerformanceMeasurement>> GetAll()
        {
            return _performanceMeasurementService.GetAll();

        }

        [HttpPost(Name = "CreateAPerformanceMeasurment")]
        public ActionResult<Guid> Create(PerformanceMeasurement performanceMeasurement)
        {
            var performanceMeasurmentId = _performanceMeasurementService.Create(performanceMeasurement);

            return CreatedAtAction(nameof(GetById), new { id = performanceMeasurmentId }, performanceMeasurmentId);
        }

        [HttpDelete("{id}", Name = "DeleteAPerformanceMeasurement")]
        public ActionResult Delete(Guid id)
        {
            var result = _performanceMeasurementService.Delete(id);
            if (result == false)
            {
                return NotFound("This Performance Measurement does not exist!");
            }
            return NoContent();
        }

        [HttpGet("{id}", Name = "GetPerformanceMeasurementById")]
        public ActionResult<PerformanceMeasurement> GetById(Guid id)
        {
            var result = _performanceMeasurementService.GetById(id);
            if (result == null)
            {
                return NotFound("This Performance Measurement does not exist!");
            }
            return Ok(result);
        }

        [HttpPut("{id}", Name = "UpdatePerformanceMeasurement")]
        public ActionResult<PerformanceMeasurement> Update(Guid id, PerformanceMeasurement updatePerformanceMeasurement)
        {
            var result = _performanceMeasurementService.Update(id, updatePerformanceMeasurement);
            if (result == null)
            {
                return NotFound("This Performance Measurement does not exist!");
            }
            return Ok(result);
        }


    }
}