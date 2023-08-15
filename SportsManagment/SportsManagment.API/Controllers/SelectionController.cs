using Microsoft.AspNetCore.Mvc;
using SportsManagment.API.DTOs;
using SportsManagment.API.Services.SelectionService;

namespace SportsManagment.API.Controllers;

[ApiController]
[Route("[controller]")]
public class SelectionController : ControllerBase
{
    private readonly ISelectionService _selectionService;

    public SelectionController(ISelectionService selectionService)
    {
        _selectionService = selectionService;
    }

    [HttpGet(Name = "GetAllSelections")]
    public ActionResult<List<Selection>> GetAll()
    {
        return _selectionService.GetAll();
    }

    [HttpPost(Name = "CreateASelection")]
    public ActionResult<Guid> Create(CreateSelectionDTO selection)
    {
        var selectionId = _selectionService.Create(selection);

        return CreatedAtAction(nameof(GetById), new { id = selectionId }, selectionId);
    }

    [HttpDelete("{id}", Name = "DeleteSelection")]
    public ActionResult Delete(Guid id)
    {
        var selectionId = _selectionService.Delete(id);
        if (selectionId == false)
        {
            return NotFound("This Selection does not exist.");
        }
        return NoContent();
    }

    [HttpGet("{id}", Name = "GetSelectionById")]
    public ActionResult<Selection> GetById(Guid id)
    {
        var selectionId = _selectionService.GetById(id);
        if (selectionId == null)
        {
            return NotFound("This Selection does not exist.");
        }
        return Ok(selectionId);
    }

    [HttpPut("{id}", Name = "UpdateSelection")]
    public ActionResult<Selection> Update(Guid id, UpdateSelectionDTO updateSelection)
    {
        var selectionId = _selectionService.Update(id, updateSelection);
        if (selectionId == null)
        {
            return NotFound("This Selection does not exist.");
        }
        return Ok(selectionId);
    }
}