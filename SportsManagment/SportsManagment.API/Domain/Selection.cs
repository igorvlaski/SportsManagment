namespace SportsManagment.API.Domain;

public class Selection
{
    public Guid Id { get; set; }
    public string? SelectionName { get; set; }
    public string? Description { get; set; }
    public List<TrainingAttendance> TrainingAttendances { get; set; } = new List<TrainingAttendance>();
    public List<Player> Players { get; set; } = new List<Player>();
}