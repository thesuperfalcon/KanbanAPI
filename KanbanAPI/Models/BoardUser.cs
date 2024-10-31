namespace KanbanAPI.Models;

public class BoardUser
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public string BoardId { get; set; }
    public virtual User User { get; set; }  
    public virtual Board Board { get; set; }
}