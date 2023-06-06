namespace SportsManagment.API.Domain;

public class BodyMeasurement
{
    public Guid Id { get; set; }   
    public double Weight { get; set; }
    public double Height { get; set; }
    public double HandSpan { get; set; }
    public double ArmSpan { get; set; }
    public double Skinfold { get; set; }
    public double SitAndReach { get; set; }
    public double ShoeSize { get; set; }
    public Guid PlayerId { get; set; }

}
