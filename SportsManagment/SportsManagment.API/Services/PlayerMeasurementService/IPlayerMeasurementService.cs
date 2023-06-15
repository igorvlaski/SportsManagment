namespace SportsManagment.API.Services.PlayerMeasurementService
{
    public interface IPlayerMeasurementService
    {
        List<PlayerMeasurement> GetAll();
        Guid Create(PlayerMeasurement playerMeasurement);
        PlayerMeasurement GetById(Guid id);
        bool Delete(Guid id);
        PlayerMeasurement Update(Guid id, PlayerMeasurement updatePlayerMeasurement);
        List<PlayerMeasurement> GetAllPlayerMeasurementByPlayerId(Guid playerId);
    }
}
