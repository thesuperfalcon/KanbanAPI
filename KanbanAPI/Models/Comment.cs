﻿namespace KanbanAPI.Models;

public class Comment
{
    public int Id { get; set; }
    public string Content { get; set; }
    public string UserId { get; set; }
    public int TaskId { get; set; }
    public virtual User User { get; set; }
    public virtual Task Task { get; set; }
}