using KanbanAPI.Data;
using KanbanAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KanbanAPI.Controller;

[Route("api/[controller]")]
[ApiController]
public class TaskUserController : ControllerBase
{
    private readonly KanbanContext _context;

    public TaskUserController(KanbanContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskUser>>> GetTaskUsers()
    {
        return await _context.TaskUsers.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TaskUser>> GetTaskUser(int id)
    {
        var taskUser = await _context.TaskUsers.FindAsync(id);
        if (taskUser == null) return NotFound();
        return taskUser;
    }

    [HttpPost]
    public async Task<ActionResult<TaskUser>> PostTaskUser(TaskUser taskUser)
    {
        await _context.TaskUsers.AddAsync(taskUser);
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetTaskUser", new { id = taskUser.Id }, taskUser);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTaskUser(int id)
    {
        var taskUser = await _context.TaskUsers.FindAsync(id);
        if (taskUser == null) return NotFound();

        _context.TaskUsers.Remove(taskUser);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}