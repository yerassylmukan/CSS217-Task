using Domain.DTOs;
using Task = System.Threading.Tasks.Task;

namespace Application.Interfaces;

public interface ITaskAssignmentService
{
    Task<TaskAssignmentDto> GetByTaskIdAsync(int taskId);
    Task<TaskAssignmentDto> GetByUserIdAsync(string userId);
    Task<IEnumerable<TaskAssignmentDto>> GetAllAsync();
    Task AssignTaskAsync(int taskId, string userId);
    Task DeleteByTaskIdAsync(int taskId);
    Task DeleteByUserIdAsync(string userId);
}