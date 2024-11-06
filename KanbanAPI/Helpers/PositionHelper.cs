using KanbanAPI.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace KanbanAPI.Helpers;

public class PositionHelper
{
    private readonly KanbanContext _context;

    public PositionHelper(KanbanContext context)
    {
        _context = context;
    }

    public async Task<int> GetColumnPosition(string boardId)
    {
        var board = await _context.Boards.FindAsync(boardId);
        var position = board.Columns.Any() ? board.Columns.Max(x => x.Position) : 0;
        return position;
    }
}