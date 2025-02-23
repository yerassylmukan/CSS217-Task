using Domain.Entities;
using Task = System.Threading.Tasks.Task;

namespace Application.Interfaces;

public interface ITaskAssignmentRepository
{
    Task<TaskAssignment> GetByTaskIdAsync(int taskId);
    Task<TaskAssignment> GetByUserIdAsync(string userId);
    Task<IEnumerable<TaskAssignment>> GetAllAsync();
    Task AddAsync(TaskAssignment entity);
    Task DeleteAsync(TaskAssignment entity);
}