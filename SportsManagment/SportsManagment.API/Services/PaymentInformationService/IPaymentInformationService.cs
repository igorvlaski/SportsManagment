using SportsManagment.API.DTOs;

namespace SportsManagment.API.Services.PaymentInformationService;

public interface IPaymentInformationService
{
    List<PaymentInformation> GetAll();
    Guid Create(PaymentInformation paymentInformation);
    bool Delete(Guid id);
    PaymentInformation GetById(Guid id);
    PaymentInformation Update(Guid id, PaymentInformation updatePaymentInformation);
    List<PaymentInformation> GetAllPaymentInformationsByPlayerId(Guid playerId, DateOnly? newerthen);
}