namespace SportsManagment.API.Services.PlayerMeasurementService
{
    public class PlayerMeasurementService : IPlayerMeasurementService
    {
        private readonly SportsManagmentDbContext _dbContext;
        public PlayerMeasurementService(SportsManagmentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Guid Create(PlayerMeasurement playerMeasurement)
        {
            var player = _dbContext.Players.FirstOrDefault(x => x.Id == playerMeasurement.PlayerId);

            if (player == null)
            {
                throw new Exception($"Player with Id {playerMeasurement.PlayerId} does not exist!");
            }

            var measurementInformation = _dbContext.MeasurementInformations.FirstOrDefault(x => x.Id == playerMeasurement.MeasurementInformationId);

            if (measurementInformation == null)
            {
                throw new Exception($"Measurement Information with Id {playerMeasurement.MeasurementInformationId} does not exist!");
            }

            playerMeasurement.Id = Guid.NewGuid();
            _dbContext.PlayerMeasurements.Add(playerMeasurement);
            _dbContext.SaveChanges();
            return playerMeasurement.Id;
        }

        public PlayerMeasurement GetById(Guid id)
        {

            var playerMeasurement = _dbContext.PlayerMeasurements.FirstOrDefault(x => x.Id == id);

            if (playerMeasurement == null)
            {
                return null!;
            }

            return playerMeasurement;
        }

        public bool Delete(Guid id)
        {

            var playerMeasurement = _dbContext.PlayerMeasurements.FirstOrDefault(x => x.Id == id);

            if (playerMeasurement == null)
            {
                return false;
            }

            _dbContext.PlayerMeasurements.Remove(playerMeasurement);
            _dbContext.SaveChanges();

            return true;
        }

        public List<PlayerMeasurement> GetAll()
        {
            return _dbContext.PlayerMeasurements.ToList();
        }

        public PlayerMeasurement Update(Guid id, PlayerMeasurement updatePlayerMeasurement)
        {

            var playerMeasurement = _dbContext.PlayerMeasurements.FirstOrDefault(x => x.Id == id);

            if (playerMeasurement == null)
            {
                return null!;
            }

            playerMeasurement.AgilityTest505 = updatePlayerMeasurement.AgilityTest505;
            playerMeasurement.BeepTest = updatePlayerMeasurement.BeepTest;
            playerMeasurement.Sprint20m = updatePlayerMeasurement.Sprint20m;
            playerMeasurement.VerticalJump = updatePlayerMeasurement.VerticalJump;
            playerMeasurement.ArmSpan = updatePlayerMeasurement.ArmSpan;
            playerMeasurement.HandSpan = updatePlayerMeasurement.HandSpan;
            playerMeasurement.Skinfold = updatePlayerMeasurement.Skinfold;
            playerMeasurement.Height = updatePlayerMeasurement.Height;
            playerMeasurement.Weight = updatePlayerMeasurement.Weight;
            playerMeasurement.ShoeSize = updatePlayerMeasurement.ShoeSize;
            playerMeasurement.SitAndReach = updatePlayerMeasurement.SitAndReach;

            _dbContext.SaveChanges();

            return playerMeasurement;
        }

        public List<PlayerMeasurement> GetAllPlayerMeasurementsByPlayerId(Guid playerId)
        {
            var playerMeasurements = _dbContext.PlayerMeasurements.Where(x => x.PlayerId == playerId);

            if (playerMeasurements == null)
            {
                return null!;
            }

            return playerMeasurements.ToList();
        }
    }
}
