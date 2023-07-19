namespace SportsManagment.API.Services.SelectionService
{
    public interface ISelectionService
    {
        List<Selection> GetAll();
        Guid Create(Selection selection);
        bool Delete(Guid id);
        Selection GetById(Guid id);
        Selection Update(Guid id, Selection updateSelection);
    }
}
