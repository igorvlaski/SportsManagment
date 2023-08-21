namespace SportsManagment.API.DTOs;

public class CreatePlayerDTO
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
    public string? ParentName { get; set; }
    public string? ParentPhoneNumber { get; set; }
}