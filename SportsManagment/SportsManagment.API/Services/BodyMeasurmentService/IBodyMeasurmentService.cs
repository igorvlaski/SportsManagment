namespace SportsManagment.API.Services.BodyMeasurmentsService
{
    public interface IBodyMeasurmentService
    {
        List<BodyMeasurement> GetAll();
        Guid Create(BodyMeasurement bodyMeasurement);
        bool Delete(Guid id);
        BodyMeasurement GetById(Guid id);
        BodyMeasurement Update(Guid id, BodyMeasurement updateBodyMeasurement);
    }
}

