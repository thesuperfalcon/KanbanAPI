﻿namespace KanbanAPI.Models;

public class Task
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime? DueDate { get; set; }
    public int ColumnId { get; set; }
    public virtual Column Column { get; set; }
}