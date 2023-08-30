namespace SportsManagment.API.Domain;

public class PaymentInformation
{
    public Guid Id { get; set; }
    public DateOnly DateOfPayment { get; set; }
    public string? Amount { get; set; }
    public string? Description { get; set;}
    public Guid PlayerId { get; set; }
}