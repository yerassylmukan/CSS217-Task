using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Task = System.Threading.Tasks.Task;

namespace Persistence.Repositories;

public class TaskAssignmentRepository : ITaskAssignmentRepository
{
    private readonly ApplicationDbContext _dbContext;

    public TaskAssignmentRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TaskAssignment> GetByTaskIdAsync(int taskId)
    {
        return (await _dbContext.TaskAssignments.FirstOrDefaultAsync(ta => ta.TaskId == taskId))!;
    }

    public async Task<TaskAssignment> GetByUserIdAsync(string userId)
    {
        return (await _dbContext.TaskAssignments.FirstOrDefaultAsync(ta => ta.UserId == userId))!;
    }

    public async Task<IEnumerable<TaskAssignment>> GetAllAsync()
    {
        return await _dbContext.TaskAssignments.ToListAsync();
    }

    public async Task AddAsync(TaskAssignment entity)
    {
        await _dbContext.TaskAssignments.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(TaskAssignment entity)
    {
        _dbContext.TaskAssignments.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }
}