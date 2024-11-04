namespace KanbanAPI.Models;

public class User
{
    public string Id { get; set; } = Guid.NewGuid().ToString(); // Initialize UserId
    public string Username { get; set; }
    public string Password { get; set; }
        
    public virtual ICollection<BoardUser> BoardUsers { get; set; } = new List<BoardUser>();
    public virtual ICollection<TaskUser> TaskUsers { get; set; } = new List<TaskUser>();
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public virtual ICollection<Board> Boards { get; set; } = new List<Board>();
}
