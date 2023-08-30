using Microsoft.AspNetCore.Mvc;
using SportsManagment.API.DTOs;
using SportsManagment.API.Services.PaymentInformationService;

namespace SportsManagment.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PaymentInformationController : ControllerBase
{
    private readonly IPaymentInformationService _paymentInformationService;

    public PaymentInformationController(IPaymentInformationService paymentInformation)
    {
        _paymentInformationService = paymentInformation;
    }


    [HttpGet(Name = "GetAllPaymentInformation")]
    public ActionResult<List<PaymentInformation>> GetAll()
    {
        return _paymentInformationService.GetAll();

    }

    [HttpPost(Name = "CreateAPaymentInformation")]
    public ActionResult<Guid> Create(PaymentInformation paymentInformation)
    {
        var paymentInformationId = _paymentInformationService.Create(paymentInformation);

        return CreatedAtAction(nameof(GetById), new { id = paymentInformationId }, paymentInformationId);
    }

    [HttpDelete("{id}", Name = "DeleteAPaymentInformation")]
    public ActionResult Delete(Guid id)
    {
        var paymentInformation = _paymentInformationService.Delete(id);
        if (paymentInformation == false)
        {
            return NotFound("This Payment does not exist.");
        }
        return NoContent();
    }

    [HttpGet("{id}", Name = "GetPaymentInformationById")]
    public ActionResult<Player> GetById(Guid id)
    {
        var paymentInformation = _paymentInformationService.GetById(id);
        if (paymentInformation == null)
        {
            return NotFound("This Payment does not exist.");
        }
        return Ok(paymentInformation);
    }

    [HttpPut("{id}", Name = "UpdatePaymentInformation")]
    public ActionResult<PaymentInformation> Update(Guid id, PaymentInformation updatePaymentInformation)
    {
        var paymentInformation = _paymentInformationService.Update(id, updatePaymentInformation);
        if (paymentInformation == null)
        {
            return NotFound("This Payment does not exist.");
        }
        return Ok(paymentInformation);
    }

    [HttpGet("player/{playerId}", Name = "GetAllPaymentInformationsByPlayerId")]
    public ActionResult<List<PaymentInformation>> GetAllPaymentInformationsByPlayerId(Guid playerId, DateOnly? newerthen)
    {
        var paymentInformation = _paymentInformationService.GetAllPaymentInformationsByPlayerId(playerId, newerthen);
        if (paymentInformation == null)
        {
            return NotFound("No Payment Information found for the specified player.");
        }

        return Ok(paymentInformation);
    }
}