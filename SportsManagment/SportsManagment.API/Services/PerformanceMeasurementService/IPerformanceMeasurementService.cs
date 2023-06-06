namespace SportsManagment.API.Services.PerformanceMeasurementService
{
    public interface IPerformanceMeasurementService
    {
        List<PerformanceMeasurement> GetAll();
        Guid Create(PerformanceMeasurement performanceMeasurement);
        bool Delete(Guid id);
        PerformanceMeasurement GetById(Guid id);
        PerformanceMeasurement Update(Guid id, PerformanceMeasurement updatePerformanceMeasurement);
    }
}
