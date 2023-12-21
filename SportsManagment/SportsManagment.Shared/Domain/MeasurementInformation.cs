namespace SportsManagment.Shared.Domain;

public class MeasurementInformation
{
    public Guid Id { get; set; }
    public DateTime? Date { get; set; }
    public string? Description { get; set; }
    public string? Name { get; set; }
    public string? Location { get; set; }
    public List<PlayerMeasurement> PlayerMeasurements { get; set; } = new List<PlayerMeasurement>();
}