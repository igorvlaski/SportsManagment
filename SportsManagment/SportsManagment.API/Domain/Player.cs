namespace SportsManagment.API.Domain;

public class Player
{
    public Guid Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public bool IsMonthlyFeePaid { get; set; }
    public bool IsYearlyFeePaid { get; set; }
    public List<TrainingAttendance> TrainingAttendances { get; set; } = new List<TrainingAttendance>();
    public List<PlayerMeasurement> PlayerMeasurements { get; set; } = new List<PlayerMeasurement>();
    public List<Selection> Selections { get; set; } = new List<Selection>();
}