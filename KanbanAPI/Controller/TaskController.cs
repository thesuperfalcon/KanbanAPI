using KanbanAPI.Data;
using KanbanAPI.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task = KanbanAPI.Models.Task;

namespace KanbanAPI.Controller;


[Route("api/[controller]")]
[ApiController]
public class TaskController : ControllerBase
{
    private readonly KanbanContext _context;
    public TaskController(KanbanContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Task>>> GetTasks()
    {
        return await _context.Tasks.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Task>> GetTask(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null) return NotFound();
        return task;
    }

    [HttpPost]
    public async Task<ActionResult<Task>> PostTask(TaskDto taskDto)
    {
        var task = new Task
        {
            Title = taskDto.Title,
            Description = taskDto.Description,
            DueDate = taskDto.DueDate,
            ColumnId = taskDto.ColumnId
        };

        await _context.Tasks.AddAsync(task);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetTask", new { id = task.Id }, task);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Task>> EditTask(int id, TaskDto taskDto)
    {
        var existingTask = await _context.Tasks.FindAsync(id);
        if (existingTask == null) return NotFound();

        existingTask.Title = taskDto.Title;
        existingTask.Description = taskDto.Description;
        existingTask.DueDate = taskDto.DueDate;
        existingTask.ColumnId = taskDto.ColumnId;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null) return NotFound();

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}