namespace KanbanAPI.Models;

public class Board
{
    public string Id { get; set; } = Guid.NewGuid().ToString(); // Initialize Id
    public string Name { get; set; }
    public string OwnerId { get; set; }
        
    public virtual User Owner { get; set; }
    public virtual ICollection<Column> Columns { get; set; } = new List<Column>();
    public virtual ICollection<BoardUser> BoardUsers { get; set; } = new List<BoardUser>();
}

wjfewofjewfjiewofj
smdklwsmdwd