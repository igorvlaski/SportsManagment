namespace SportsManagment.API.Domain;

public class PlayerMeasurement
{
    public Guid Id { get; set; }
    public decimal Sprint20m { get; set; }
    public decimal VerticalJump { get; set; }
    public int BeepTest { get; set; }
    public decimal AgilityTest505 { get; set; }
    public decimal Weight { get; set; }
    public decimal Height { get; set; }
    public decimal HandSpan { get; set; }
    public decimal ArmSpan { get; set; }
    public decimal Skinfold { get; set; }
    public decimal SitAndReach { get; set; }
    public decimal ShoeSize { get; set; }
    public Guid PlayerId { get; set; }
    public Guid MeasurementInformationId { get; set; }
}
