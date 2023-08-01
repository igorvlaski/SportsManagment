namespace SportsManagment.API.Services.MeasurementInformationService;

public class MeasurementInformationService : IMeasurementInformationService
{
    private readonly SportsManagmentDbContext _dbContext;
    public MeasurementInformationService(SportsManagmentDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Guid Create(MeasurementInformation measurementInformation)
    {
        measurementInformation.Id = Guid.NewGuid();
        _dbContext.MeasurementInformations.Add(measurementInformation);
        _dbContext.SaveChanges();
        return measurementInformation.Id;
    }

    public List<MeasurementInformation> GetAll() 
    {
        return _dbContext.MeasurementInformations.ToList();
    }

    public bool Delete(Guid id)
    {
        var measurementInformation = _dbContext.MeasurementInformations.FirstOrDefault(x => x.Id == id);

        if (measurementInformation == null) 
        {
            return false;
        }
        _dbContext.MeasurementInformations.Remove(measurementInformation);
        _dbContext.SaveChanges();

        return true;   
    }

    public MeasurementInformation GetById(Guid id) 
    {
        var measurementInformation = _dbContext.MeasurementInformations.FirstOrDefault(x => x.Id == id);

        if(measurementInformation == null) 
        {
            return null!;
        }

        return measurementInformation;
    }

    public MeasurementInformation Update(Guid id, MeasurementInformation updateMeasurementInformation)
    {
        var measurementInformation = _dbContext.MeasurementInformations.FirstOrDefault(x => x.Id == id);

        if (measurementInformation == null) 
        {
            return null!;
        }

        measurementInformation.Name = updateMeasurementInformation.Name;
        measurementInformation.Description = updateMeasurementInformation.Description;
        measurementInformation.Location = updateMeasurementInformation.Location;
        measurementInformation.Date = updateMeasurementInformation.Date;
        _dbContext.SaveChanges();

        return measurementInformation;
    }
}
