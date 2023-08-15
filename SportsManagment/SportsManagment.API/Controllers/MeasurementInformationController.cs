using Microsoft.AspNetCore.Mvc;
using SportsManagment.API.Services.MeasurementInformationService;

namespace SportsManagment.API.Controllers;

[ApiController]
[Route("[controller]")]
public class MeasurementInformationController : ControllerBase
{
    private readonly IMeasurementInformationService _measurementInformationService;

    public MeasurementInformationController(IMeasurementInformationService measurementInformationService) 
    {
        _measurementInformationService = measurementInformationService;
    }

    [HttpPost(Name = "CreateAMeasurementInformation")]
    public ActionResult<Guid> Create(MeasurementInformation measurementInformation)
    {
        var measurementInformatioId = _measurementInformationService.Create(measurementInformation);

        return CreatedAtAction(nameof(GetById), new { id = measurementInformatioId }, measurementInformatioId);
    }

    [HttpGet(Name = "GetAllMeasurementInformation")]
    public ActionResult<List<MeasurementInformation>> GetAll()
    {
        return _measurementInformationService.GetAll();
    }

    [HttpGet("{id}", Name = "GetMeasurementInformationById")]
    public ActionResult GetById(Guid Id) 
    {
        var result = _measurementInformationService.GetById(Id);
        if (result == null)
        {
            return NotFound("This Measurement Information does not exist!");
        }
        return Ok(result);
    }

    [HttpDelete("{id}", Name = "DeleteMeasurementById")]
    public ActionResult Delete(Guid Id) 
    {
        var result = _measurementInformationService.Delete(Id);
        if (result == false)
        {
            return NotFound("This Measurement Information does not exist!");
        }
        return NoContent();
    }

    [HttpPut("{id}", Name = "UpdateMeasurementInformationMeasurement")]
    public ActionResult<MeasurementInformation> Update(Guid id, MeasurementInformation updateMeasurementInformation) 
    {
        var result = _measurementInformationService.Update(id, updateMeasurementInformation);
        if (result == null)
        {
            return NotFound("This Measurement Information does not exist!");
        }
        return Ok(result);
    }
}