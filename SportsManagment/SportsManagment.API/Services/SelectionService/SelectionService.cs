using SportsManagment.API.Domain;

namespace SportsManagment.API.Services.SelectionService;

public class SelectionService : ISelectionService
{
    private readonly SportsManagmentDbContext _dbContext;
    public SelectionService(SportsManagmentDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Guid Create(Selection selection)
    {
        selection.Id = Guid.NewGuid();
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

        var selection = _dbContext.Selections.FirstOrDefault(x => x.Id == id);

        if (selection == null)
        {
            return null!;
        }

        return selection;
    }

    public Selection Update(Guid id, Selection updateSelection)
    {

        var selection = _dbContext.Selections.FirstOrDefault(x => x.Id == id);

        if (selection == null)
        {
            return null!;
        }

        selection.SelectionName = updateSelection.SelectionName;

        _dbContext.SaveChanges();

        return selection;
    }
}
