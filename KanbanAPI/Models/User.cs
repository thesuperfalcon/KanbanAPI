namespace KanbanAPI.Models;

public class User
{
    public string UserId { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public virtual ICollection<BoardUser> BoardUsers { get; set; }
    public virtual ICollection<TaskUser> TaskUsers { get; set; }
    public virtual ICollection<Comment> Comments { get; set; }
    public virtual ICollection<Board> Boards { get; set; }

    public User()
    {
        UserId = Guid.NewGuid().ToString();         
    }
}