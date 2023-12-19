using SportsManagment.Shared.Domain;

namespace SportsManagment.Blazor.Client.Shared;

public static partial class EnumExtension
{
    public static string ToUserFriendlyString(TypeOfPayment typeOfPayment)
    {
        return typeOfPayment switch
        {
            TypeOfPayment.MonthlyFee => "Mesečna vadnina",
            TypeOfPayment.YearlyFee => "Letna vadnina",
            TypeOfPayment.MembershipFee => "Članarina",
            TypeOfPayment.TechnicalFoul => "Tehnična napaka",
            TypeOfPayment.PreparationCamp => "Pripravljalni kamp",
            TypeOfPayment.SummerCamp => "Poletni kamp",
            TypeOfPayment.Merchandise => "Oprema",
            _ => typeOfPayment.ToString(),
        };
    }

}