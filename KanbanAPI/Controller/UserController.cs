using KanbanAPI.Data;
using KanbanAPI.Dto;
using Microsoft.AspNetCore.Mvc;
using KanbanAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KanbanAPI.Controller;


[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly KanbanContext _context;

    public UserController(KanbanContext context)
    {
        _context = context; 
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        return await _context.Users.ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<User>> PostUser(UserDto userDto)
    {
        var user = new User()
        {
            Username = userDto.UserName,
            Password = userDto.Password,
        };
        
        await _context.Users.AddAsync(user);
        
        await _context.SaveChangesAsync();
        
        return CreatedAtAction("GetUser", new { id = user.Id }, user);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(string id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        return user;
    }
}