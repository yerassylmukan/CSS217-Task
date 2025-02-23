using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using TaskEntity = Domain.Entities.Task;

namespace Persistence.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly ApplicationDbContext _dbContext;

    public TaskRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<TaskEntity> GetByIdAsync(int id)
    {
        return (await _dbContext.Tasks.FirstOrDefaultAsync(t => t.Id == id))!;
    }

    public async Task<IEnumerable<TaskEntity>> GetAllAsync()
    {
        return await _dbContext.Tasks.ToListAsync();
    }

    public async Task AddAsync(TaskEntity entity)
    {
        await _dbContext.Tasks.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(TaskEntity entity)
    {
        _dbContext.Tasks.Update(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(TaskEntity entity)
    {
        _dbContext.Tasks.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }
}
