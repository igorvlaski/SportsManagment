using SportsManagment.API.Domain;
using SportsManagment.API.DTOs;

namespace SportsManagment.API.Services.PlayerService
{
    public class PlayerService : IPlayerService
    {
        private readonly SportsManagmentDbContext _dbContext;
        public PlayerService(SportsManagmentDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Guid Create(CreatePlayerDTO playerToCreate)
        {
            var player = new Player
            {
                Id = Guid.NewGuid(),
                FirstName = playerToCreate.FirstName,
                LastName = playerToCreate.LastName,
                DateOfBirth = playerToCreate.DateOfBirth,
                IsMonthlyFeePaid = playerToCreate.IsMonthlyFeePaid,
                IsYearlyFeePaid = playerToCreate.IsYearlyFeePaid
            };
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

        public Player GetById(Guid id, DateOnly? newerthen, bool includePlayerMeasurements)
        {
            IQueryable<Player> players = _dbContext.Players;

            if (includePlayerMeasurements)
            {
                players = players.Include(x => x.PlayerMeasurements);
            }

            if (newerthen.HasValue)
            {
                players = players.Include(x => x.TrainingAttendances.Where(x => x.Date > newerthen));
            }

            var player = players.FirstOrDefault(x => x.Id == id);

            if (player == null)
            {
                return null!;
            }

            return player;
        }

        public Player Update(Guid id, UpdatePlayerDTO updatePlayer)
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

        public bool AddOrRemovePlayerFromSelection(Guid playerId, Guid selectionId)
        {
            var player = _dbContext.Players.FirstOrDefault(x => x.Id == playerId);
            var selection = _dbContext.Selections.Include(s => s.Players).FirstOrDefault(x => x.Id == selectionId);

            if (player == null || selection == null)
            {
                return false;
            }

            // Check if the player is already associated with the selection
            bool isPlayerInSelection = selection.Players.Any(p => p.Id == playerId);

            if (isPlayerInSelection)
            {
                // Player is already in the selection, so remove the association
                selection.Players.Remove(player);
            }
            else
            {
                // Player is not in the selection, so add the association
                selection.Players.Add(player);
            }

            _dbContext.SaveChanges();

            return true;
        }
    }
}