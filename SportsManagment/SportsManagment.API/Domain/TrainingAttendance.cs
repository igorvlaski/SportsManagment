namespace SportsManagment.API.Domain;

public class TrainingAttendance
{
    public Guid Id { get; set; }

    public DateOnly Date { get; set; }

    public Selection Selection { get; set; }

    public Guid PlayerId { get; set; }
}
