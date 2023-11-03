using SportsManagment.Shared.Domain;
using SportsManagment.Shared.DTOs;

namespace SportsManagment.API.Services.PlayerService;

public interface IPlayerService
{
    List<Player> GetAll();
    Guid Create(CreatePlayerDTO player);
    bool Delete(Guid id);
    Player GetById(Guid id, DateOnly? newerthen, bool includePlayerMeasurements, bool includePaymentInformations);
    Player Update(Guid id, UpdatePlayerDTO updatePlayer);
    bool AddOrRemovePlayerFromSelection(Guid playerId, Guid selectionId);
}
