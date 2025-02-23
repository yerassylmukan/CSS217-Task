using Domain.Entities;
using Task = System.Threading.Tasks.Task;

namespace Application.Interfaces;

public interface ITaskAssignmentService
{
    Task<TaskAssignment> GetByTaskIdAsync(int taskId);
    Task<TaskAssignment> GetByUserIdAsync(string userId);
    Task<IEnumerable<TaskAssignment>> GetAllAsync();
    Task AssignTaskAsync(int taskId, string userId);
    Task DeleteByTaskIdAsync(int taskId);
    Task DeleteByUserIdAsync(string userId);
}