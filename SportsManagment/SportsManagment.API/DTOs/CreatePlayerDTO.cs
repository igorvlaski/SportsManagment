namespace SportsManagment.API.DTOs;

public class CreatePlayerDTO
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public bool IsMonthlyFeePaid { get; set; }
    public bool IsYearlyFeePaid { get; set; }
}
