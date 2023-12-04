using SportsManagment.Shared.Domain;
using SportsManagment.Shared.DTOs;

namespace SportsManagment.API.Services.PlayerService;

public interface IPlayerService
{
    List<Player> GetAll();
    List<Player> GetDeletedPlayers();
    Guid Create(CreatePlayerDTO player);
    bool HardDelete(Guid id);
    bool SoftDelete(Guid Id);
    bool Restore(Guid Id);
    Player GetById(Guid id, DateOnly? newerthen, 
                            bool includePlayerMeasurements, 
                            bool includePaymentInformations, 
                            bool includeSelections,
                            bool includeTrainingAttendances);
    Player Update(Guid id, UpdatePlayerDTO updatePlayer);
    bool AddOrRemovePlayerFromSelection(Guid playerId, Guid selectionId);
}
