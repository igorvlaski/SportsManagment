namespace SportsManagment.Shared.Domain;

public class TrainingAttendance
{
    public Guid Id { get; set; }
    public DateOnly Date { get; set; }
    public Guid SelectionId { get; set; }
    public Guid PlayerId { get; set; }
}