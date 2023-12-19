namespace SportsManagment.Shared.Domain;

public class PaymentInformation
{
    public Guid Id { get; set; }
    public DateOnly DateOfPayment { get; set; }
    public string? Amount { get; set; }
    public string? Description { get; set; }
    public Guid PlayerId { get; set; }
    public TypeOfPayment typeOfPayment { get; set; }
}

public enum TypeOfPayment
{
    MonthlyFee = 0,
    YearlyFee = 1,
    MembershipFee = 2,
    TechnicalFoul = 3,
    Merchandise = 4,
    PreparationCamp = 5,
    SummerCamp = 6,
}