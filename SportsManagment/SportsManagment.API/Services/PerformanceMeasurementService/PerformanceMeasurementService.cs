using SportsManagment.API.Domain;

namespace SportsManagment.API.Services.PerformanceMeasurementService
{
    public class PerformanceMeasurementService : IPerformanceMeasurementService
    {
        private readonly SportsManagmentDbContext _dbContext;
        public PerformanceMeasurementService(SportsManagmentDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Guid Create(PerformanceMeasurement performanceMeasurement)
        {
            var player = _dbContext.Players.FirstOrDefault(x => x.Id == performanceMeasurement.PlayerId);

            if (player == null)
            {
                throw new Exception($"Player with Id {performanceMeasurement.PlayerId} does not exist!");
            }

            performanceMeasurement.Id = Guid.NewGuid();
            _dbContext.PerformanceMeasurements.Add(performanceMeasurement);
            _dbContext.SaveChanges();
            return performanceMeasurement.Id;
        }

        public bool Delete(Guid id)
        {

            var performanceMeasurement = _dbContext.PerformanceMeasurements.FirstOrDefault(x => x.Id == id);

            if (performanceMeasurement == null)
            {
                return false;
            }

            _dbContext.PerformanceMeasurements.Remove(performanceMeasurement);
            _dbContext.SaveChanges();
            return true;
        }

        public List<PerformanceMeasurement> GetAll()
        {
            return _dbContext.PerformanceMeasurements.ToList();
        }

        public PerformanceMeasurement GetById(Guid id)
        {

            var performanceMeasurement = _dbContext.PerformanceMeasurements.FirstOrDefault(x => x.Id == id);

            if (performanceMeasurement == null)
            {
                return null!;
            }

            return performanceMeasurement;
        }

        public PerformanceMeasurement Update(Guid id, PerformanceMeasurement updatePerformanceMeasurement)
        {

            var performanceMeasurement = _dbContext.PerformanceMeasurements.FirstOrDefault(x => x.Id == id);

            if (performanceMeasurement == null)
            {
                return null!;
            }

            performanceMeasurement.VerticalJump = updatePerformanceMeasurement.VerticalJump;
            performanceMeasurement.BeepTest = updatePerformanceMeasurement.BeepTest;
            performanceMeasurement.Sprint20m = updatePerformanceMeasurement.Sprint20m;
            performanceMeasurement.AgilityTest505 = updatePerformanceMeasurement.AgilityTest505;

            _dbContext.SaveChanges();

            return performanceMeasurement;
        }
    }
}


