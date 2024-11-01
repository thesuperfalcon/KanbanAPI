namespace KanbanAPI.Models;
public class Task
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime? DueDate { get; set; }
    public int ColumnId { get; set; }
        
    public virtual Column Column { get; set; }
    public virtual ICollection<TaskUser> TaskUsers { get; set; } = new List<TaskUser>();
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
}