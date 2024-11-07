using KanbanAPI.Data;
using KanbanAPI.Dto;
using KanbanAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KanbanAPI.Controller;

[Route("api/[controller]")]
[ApiController]
public class BoardUserController : ControllerBase
{
    private readonly KanbanContext _context;

    public BoardUserController(KanbanContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BoardUser>>> GetBoardUsers()
    {
        return await _context.BoardUsers.ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<BoardUser>> PostBoardUser(BoardUserDto boardUserDto)
    {
        var boardUser = new BoardUser()
        {
            UserId = boardUserDto.UserId,
            BoardId = boardUserDto.BoardId,
        };
        
        await _context.BoardUsers.AddAsync(boardUser);
        
        await _context.SaveChangesAsync();
     
        return Ok(boardUser);
    }

    [HttpDelete]
    public async Task<ActionResult<BoardUser>> DeleteBoardUser(BoardUserDto boardUserDto)
    {
        var boardUser = await _context.BoardUsers
            .Where(x => x.BoardId == boardUserDto.BoardId)
            .Where(x => x.UserId == boardUserDto.UserId)
            .FirstOrDefaultAsync();
        
        if(boardUser == null) return NotFound();
        
        _context.BoardUsers.Remove(boardUser);
        
        await _context.SaveChangesAsync();
        
        return Ok(boardUser);
    }
}