using SportsManagment.Shared.Domain;

namespace SportsManagment.API.Services.MeasurementInformationService;

public interface IMeasurementInformationService
{
    List<MeasurementInformation> GetAll();
    Guid Create(MeasurementInformation measurementInformation);
    MeasurementInformation GetById(Guid id);
    bool Delete(Guid id);
    MeasurementInformation Update(Guid id, MeasurementInformation updateMeasurementInformation);
}
