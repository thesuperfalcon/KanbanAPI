namespace KanbanAPI.Models;

public class Board
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string OwnerId { get; set; }
    public virtual User Owner { get; set; }
        
    public Board()
    {
        Id = Guid.NewGuid().ToString();
    }
}