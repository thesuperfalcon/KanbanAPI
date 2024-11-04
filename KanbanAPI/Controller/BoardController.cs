using KanbanAPI.Data;
using KanbanAPI.Dto;
using KanbanAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KanbanAPI.Controller;

[Route("api/[controller]")]
[ApiController]
public class BoardController : ControllerBase
{
    private readonly KanbanContext _context;    
    
    public BoardController(KanbanContext context)
    {
        _context = context; 
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Board>>> GetBoards()
    {
       return await _context.Boards.ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<Board>> PostBoard(BoardDto boardDto)
    {
        var board = new Board()
        {
            Name = boardDto.Name,
            OwnerId = boardDto.OwnerId,
        };
        await _context.Boards.AddAsync(board);
        
        await _context.SaveChangesAsync();
                
        return CreatedAtAction("GetBoard", new { id = board.Id }, board);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Board>> GetBoard(string id)
    {
        var board = await _context.Boards.FindAsync(id);

        if (board == null)
        {
            return NotFound();
        }

        return board;
    }
}