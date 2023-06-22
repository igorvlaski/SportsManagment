using SportsManagment.API.Domain;

namespace SportsManagment.API.Services.PlayerService
{
    public class PlayerService : IPlayerService
    {
        private readonly SportsManagmentDbContext _dbContext;
        public PlayerService(SportsManagmentDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Guid Create(Player player)
        {
            player.Id = Guid.NewGuid();
            _dbContext.Players.Add(player);
            _dbContext.SaveChanges();
            return player.Id;
        }

        public bool Delete(Guid id)
        {

            var player = _dbContext.Players.FirstOrDefault(x => x.Id == id);

            if (player == null)
            {
                return false;
            }

            _dbContext.Players.Remove(player);
            _dbContext.SaveChanges();

            return true;
        }

        public List<Player> GetAll()
        {
            return _dbContext.Players.ToList();
        }

        public Player GetById(Guid id, DateOnly? newerthen)
        {
            var player = _dbContext.Players.Include(x => x.TrainingAttendances).Include(x => x.PlayerMeasurements).FirstOrDefault(x => x.Id == id);

            if (player == null)
            {
                return null!;
            }

            if (newerthen.HasValue)
            {
                player.TrainingAttendances = player.TrainingAttendances.Where(x => x.Date > newerthen).ToList();
            }

            return player;
        }

        public Player Update(Guid id, Player updatePlayer)
        {

            var player = _dbContext.Players.FirstOrDefault(x => x.Id == id);

            if (player == null)
            {
                return null!;
            }

            player.FirstName = updatePlayer.FirstName;
            player.LastName = updatePlayer.LastName;
            player.DateOfBirth = updatePlayer.DateOfBirth;
            player.IsMonthlyFeePaid = updatePlayer.IsMonthlyFeePaid;
            player.IsYearlyFeePaid = updatePlayer.IsYearlyFeePaid;
            _dbContext.SaveChanges();

            return player;
        }
    }
}
