﻿namespace KanbanAPI.Dto;

public class TaskDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public int ColumnId { get; set; }
}