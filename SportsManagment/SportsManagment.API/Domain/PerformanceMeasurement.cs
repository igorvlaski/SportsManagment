namespace SportsManagment.API.Domain
{
    public class PerformanceMeasurement
    {
        public Guid Id { get; set; }
        public double Sprint20m { get; set; }
        public double VerticalJump { get; set; }
        public double BeepTest { get; set; }
        public double AgilityTest505 { get; set; }
        public Guid PlayerId { get; set; }
    }
}
