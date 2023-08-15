using SportsManagment.API.DTOs;

namespace SportsManagment.API.Services.SelectionService;

public interface ISelectionService
{
    List<Selection> GetAll();
    Guid Create(CreateSelectionDTO selection);
    bool Delete(Guid id);
    Selection GetById(Guid id);
    Selection Update(Guid id, UpdateSelectionDTO updateSelection);
}
