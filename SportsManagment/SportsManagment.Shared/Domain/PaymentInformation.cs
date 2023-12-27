namespace SportsManagment.Shared.Domain;

public class PaymentInformation
{
    public Guid Id { get; set; }
    public DateOnly DateOfPayment { get; set; }
    public double Amount { get; set; }
    public string? Description { get; set; }
    public Guid PlayerId { get; set; }
    public TypeOfPayment typeOfPayment { get; set; }
}