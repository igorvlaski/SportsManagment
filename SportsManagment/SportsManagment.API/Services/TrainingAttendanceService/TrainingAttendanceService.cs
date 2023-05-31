namespace SportsManagment.API.Services.TrainingAttendanceService
{
    public class TrainingAttendanceService : ITrainingAttendanceService
    {
        private readonly SportsManagmentDbContext _dbContext;
        public TrainingAttendanceService(SportsManagmentDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Guid Create(TrainingAttendance trainingAttendance)
        {
            var player = _dbContext.Players.FirstOrDefault(x => x.Id == trainingAttendance.PlayerId);

            if (player == null)
            {
                throw new Exception($"Player with Id {trainingAttendance.PlayerId} does not exist!");
            }

            trainingAttendance.Id = Guid.NewGuid();
            _dbContext.TrainingAttendances.Add(trainingAttendance);
            _dbContext.SaveChanges();
            return trainingAttendance.Id;
        }

        public bool Delete(Guid id)
        {

            var trainingAttendance = _dbContext.TrainingAttendances.FirstOrDefault(x => x.Id == id);

            if (trainingAttendance == null)
            {
                return false;
            }

            _dbContext.TrainingAttendances.Remove(trainingAttendance);
            _dbContext.SaveChanges();

            return true;
        }

        public List<TrainingAttendance> GetAll()
        {
            return _dbContext.TrainingAttendances.ToList();
        }

        public TrainingAttendance GetById(Guid id)
        {

            var trainingAttendance = _dbContext.TrainingAttendances.FirstOrDefault(x => x.Id == id);

            if (trainingAttendance == null)
            {
                return null!;
            }

            return trainingAttendance;
        }

        public TrainingAttendance Update(Guid id, TrainingAttendance updateTrainingAttendance)
        {

            var trainingAttendance = _dbContext.TrainingAttendances.FirstOrDefault(x => x.Id == id);

            if (trainingAttendance == null)
            {
                return null!;
            }

            trainingAttendance.Selection = updateTrainingAttendance.Selection;
            trainingAttendance.Date = updateTrainingAttendance.Date;

            _dbContext.SaveChanges();

            return trainingAttendance;
        }
    }
}

