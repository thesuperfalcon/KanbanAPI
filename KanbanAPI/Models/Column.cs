namespace KanbanAPI.Models;

public class Column
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Position { get; set; }
    public string BoardId { get; set; }
    public virtual Board Board { get; set; }
}