using SportsManagment.API.Data;
using SportsManagment.Shared.Domain;
using SportsManagment.Shared.DTOs;

namespace SportsManagment.API.Services.SelectionService;

public class SelectionService : ISelectionService
{
    private readonly SportsManagmentDbContext _dbContext;
    public SelectionService(SportsManagmentDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Guid Create(CreateSelectionDTO selectionToCreate)
    {
        var selection = new Selection
        {
            Id = Guid.NewGuid(),
            SelectionName = selectionToCreate.SelectionName,
            Description = selectionToCreate.Description,
        };
        _dbContext.Selections.Add(selection);
        _dbContext.SaveChanges();
        return selection.Id;
    }

    public bool Delete(Guid id)
    {

        var selection = _dbContext.Selections.FirstOrDefault(x => x.Id == id);

        if (selection == null)
        {
            return false;
        }

        _dbContext.Selections.Remove(selection);
        _dbContext.SaveChanges();

        return true;
    }

    public List<Selection> GetAll() 
    {
        return _dbContext.Selections.ToList();
    }

    public Selection GetById(Guid id)
    {

        var selection = _dbContext.Selections.Include(x=>x.Players.Where(player => !player.IsDeleted)).FirstOrDefault(x => x.Id == id);

        if (selection == null)
        {
            return null!;
        }

        return selection;
    }

    public Selection Update(Guid id, UpdateSelectionDTO updateSelection)
    {

        var selection = _dbContext.Selections.FirstOrDefault(x => x.Id == id);

        if (selection == null)
        {
            return null!;
        }

        selection.SelectionName = updateSelection.SelectionName;
        selection.Description = updateSelection.Description;

        _dbContext.SaveChanges();

        return selection;
    }
}
