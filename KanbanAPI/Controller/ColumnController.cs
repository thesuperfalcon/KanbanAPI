using KanbanAPI.Data;
using Microsoft.AspNetCore.Mvc;
using KanbanAPI.Models;
using KanbanAPI.Dto;
using KanbanAPI.Helpers;
using Microsoft.EntityFrameworkCore;

namespace KanbanAPI.Controller;

[Route("api/[controller]")]
[ApiController]
public class ColumnController : ControllerBase
{
    private readonly KanbanContext _context;
    private readonly PositionHelper _positionHelper;

    public ColumnController(KanbanContext context, PositionHelper positionHelper)
    {
        _context = context; 
        _positionHelper = positionHelper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Column>>> GetColumns()
    {
        return await _context.Columns.ToListAsync();
    }

    [HttpGet("id")]
    public async Task<ActionResult<Column>> GetColumn(int id)
    {
        return await _context.Columns.FindAsync(id);
    }
    
    [HttpPost]
    public async Task<ActionResult<Column>> PostColumn(ColumnDto columnDto)
    {
        var column = new Column()
        {
            Name = columnDto.Name,
            BoardId = columnDto.BorderId,
            Position = await _positionHelper.GetColumnPosition(columnDto.BorderId)
        };
        
        await _context.Columns.AddAsync(column);
        
        await _context.SaveChangesAsync();
        
        return CreatedAtAction("GetColumn", new { id = column.Id }, column);
    }
}