namespace SportsManagment.API.Services.PlayerService
{
    public interface IPlayerService
    {
        List<Player> GetAll();
        Guid Create(Player player);
        bool Delete(Guid id);
        Player GetById(Guid id, DateOnly? newerthen, bool includePlayerMeasurements);
        Player Update(Guid id, Player updatePlayer);
    }
}
