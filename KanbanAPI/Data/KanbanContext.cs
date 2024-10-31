using KanbanAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KanbanAPI.Data;

public class KanbanContext : DbContext
{
    public KanbanContext(DbContextOptions<KanbanContext> options) {}
    
    
    public DbSet<User> Users { get; set; }
    public DbSet<Board> Boards { get; set; }
    
}