using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Task = System.Threading.Tasks.Task;

namespace Persistence.Repositories;

public class ColumnRepository : IColumnRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ColumnRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Column> GetByIdAsync(int id)
    {
        return (await _dbContext.Columns.FirstOrDefaultAsync(c => c.Id == id))!;
    }

    public async Task<IEnumerable<Column>> GetAllAsync()
    {
        return await _dbContext.Columns.ToListAsync();
    }

    public async Task AddAsync(Column entity)
    {
        await _dbContext.Columns.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Column entity)
    {
        _dbContext.Columns.Update(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Column entity)
    {
        _dbContext.Columns.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }
}