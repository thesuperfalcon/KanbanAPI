using KanbanAPI.Data;
using KanbanAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KanbanAPI.Controller;


[Route("api/[controller]")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly KanbanContext _context;

        public CommentController(KanbanContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comment>>> GetComments()
        {
            return await _context.Comments.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null) return NotFound();
            return comment;
        }

        [HttpPost]
        public async Task<ActionResult<Comment>> PostComment(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComment", new { id = comment.Id }, comment);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Comment>> EditComment(int id, Comment comment)
        {
            if (id != comment.Id) return BadRequest();

            var existingComment = await _context.Comments.FindAsync(id);
            if (existingComment == null) return NotFound();

            existingComment.Content = comment.Content; 
            existingComment.UserId = comment.UserId;   
            existingComment.TaskId = comment.TaskId;   

            await _context.SaveChangesAsync();
            return NoContent(); 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null) return NotFound();

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return NoContent(); 
        }
    }
