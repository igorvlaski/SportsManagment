using SportsManagment.API.Services.BodyMeasurmentsService;

namespace SportsManagment.API.Services.BodyMeasurmentService
{
    public class BodyMeasurmentService : IBodyMeasurmentService
    {
        private readonly SportsManagmentDbContext _dbContext;
        public BodyMeasurmentService(SportsManagmentDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Guid Create(BodyMeasurement bodyMeasurement)
        {
            var player = _dbContext.Players.FirstOrDefault(x => x.Id == bodyMeasurement.PlayerId);

            if (player == null)
            {
                throw new Exception($"Player with Id {bodyMeasurement.PlayerId} does not exist!");
            }

            bodyMeasurement.Id = Guid.NewGuid();
            _dbContext.BodyMeasurements.Add(bodyMeasurement);
            _dbContext.SaveChanges();
            return bodyMeasurement.Id;
        }

        public bool Delete(Guid id)
        {

            var bodyMeasurement = _dbContext.BodyMeasurements.FirstOrDefault(x => x.Id == id);

            if (bodyMeasurement == null)
            {
                return false;
            }

            _dbContext.BodyMeasurements.Remove(bodyMeasurement);
            _dbContext.SaveChanges();
            return true;
        }

        public List<BodyMeasurement> GetAll()
        {
            return _dbContext.BodyMeasurements.ToList();
        }

        public BodyMeasurement GetById(Guid id)
        {

            var bodyMeasurement = _dbContext.BodyMeasurements.FirstOrDefault(x => x.Id == id);

            if (bodyMeasurement == null)
            {
                return null!;
            }

            return bodyMeasurement;
        }

        public BodyMeasurement Update(Guid id, BodyMeasurement updateBodyMeasurement)
        {

            var bodyMeasurement = _dbContext.BodyMeasurements.FirstOrDefault(x => x.Id == id);

            if (bodyMeasurement == null)
            {
                return null!;
            }

            bodyMeasurement.Weight = updateBodyMeasurement.Weight;
            bodyMeasurement.Height = updateBodyMeasurement.Height;
            bodyMeasurement.ShoeSize = updateBodyMeasurement.ShoeSize;
            bodyMeasurement.SitAndReach = updateBodyMeasurement.SitAndReach;
            bodyMeasurement.ArmSpan = updateBodyMeasurement.ArmSpan;
            bodyMeasurement.HandSpan = updateBodyMeasurement.HandSpan;
            bodyMeasurement.Skinfold = updateBodyMeasurement.Skinfold;

            _dbContext.SaveChanges();

            return bodyMeasurement;
        }
    }
}

