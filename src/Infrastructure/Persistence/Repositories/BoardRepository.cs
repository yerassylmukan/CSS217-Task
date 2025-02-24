using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Task = System.Threading.Tasks.Task;

namespace Persistence.Repositories;

public class BoardRepository : IBoardRepository
{
    private readonly ApplicationDbContext _dbContext;

    public BoardRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Board> GetByIdAsync(int id)
    {
        return (await _dbContext.Boards
            .Include(b => b.Columns)
            .ThenInclude(c => c.Tasks)
            .ThenInclude(t => t.Assignments)
            .Include(b => b.Columns)
            .ThenInclude(c => c.Tasks)
            .ThenInclude(t => t.Comments)
            .FirstOrDefaultAsync(b => b.Id == id))!;
    }

    public async Task<IEnumerable<Board>> GetAllAsync()
    {
        return await _dbContext.Boards
            .Include(b => b.Columns)
            .ThenInclude(c => c.Tasks)
            .ThenInclude(t => t.Assignments)
            .Include(b => b.Columns)
            .ThenInclude(c => c.Tasks)
            .ThenInclude(t => t.Comments)
            .ToListAsync();
    }

    public async Task AddAsync(Board entity)
    {
        await _dbContext.Boards.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Board entity)
    {
        _dbContext.Boards.Update(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Board entity)
    {
        _dbContext.Boards.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }
}