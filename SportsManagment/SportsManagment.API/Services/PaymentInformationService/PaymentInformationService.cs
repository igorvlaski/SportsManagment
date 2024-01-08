using SportsManagment.API.Data;
using SportsManagment.Shared.Domain;


namespace SportsManagment.API.Services.PaymentInformationService;

public class PaymentInformationService : IPaymentInformationService
{
    private readonly SportsManagmentDbContext _dbContext;
    public PaymentInformationService(SportsManagmentDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Guid Create(PaymentInformation paymentInformation)
    {
        
            var player = _dbContext.Players.FirstOrDefault(x => x.Id == paymentInformation.PlayerId);

            if (player == null)
            {
                throw new Exception($"Player with Id {paymentInformation.PlayerId} does not exist!");
            }

        paymentInformation.Id = Guid.NewGuid();
        _dbContext.PaymentInformations.Add(paymentInformation);
        _dbContext.SaveChanges();
        return paymentInformation.Id;
    }

    public bool Delete(Guid id)
    {

        var paymentInformation = _dbContext.PaymentInformations.FirstOrDefault(x => x.Id == id);

        if (paymentInformation == null)
        {
            return false;
        }

        _dbContext.PaymentInformations.Remove(paymentInformation);
        _dbContext.SaveChanges();

        return true;
    }

    public List<PaymentInformation> GetAll() 
    {
        return _dbContext.PaymentInformations.ToList();
    }

    public PaymentInformation GetById(Guid id)
    {

        var paymentInformation = _dbContext.PaymentInformations.FirstOrDefault(x => x.Id == id);

        if (paymentInformation == null)
        {
            return null!;
        }

        return paymentInformation;
    }

    public PaymentInformation Update(Guid id, PaymentInformation updatePaymentInformation)
    {

        var paymentInformation = _dbContext.PaymentInformations.FirstOrDefault(x => x.Id == id);

        if (paymentInformation == null)
        {
            return null!;
        }

        paymentInformation.DateOfPayment = updatePaymentInformation.DateOfPayment;
        paymentInformation.Description = updatePaymentInformation.Description;
        paymentInformation.Amount = updatePaymentInformation.Amount;
        paymentInformation.typeOfPayment = updatePaymentInformation.typeOfPayment;

        _dbContext.SaveChanges();

        return paymentInformation;
    }

    public List<PaymentInformation> GetAllPaymentInformationsByPlayerId(Guid playerId, DateOnly? newerthen)
    {
        var paymentInformation = _dbContext.PaymentInformations.Where(x => x.PlayerId == playerId);
        if (newerthen.HasValue)
        {
            paymentInformation = paymentInformation.Where(x => x.DateOfPayment > newerthen);
        }

        if (paymentInformation == null)
        {
            return null!;
        }

        return paymentInformation.ToList();
    }
}