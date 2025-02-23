using TaskEntity = Domain.Entities.Task;

namespace Application.Interfaces;

public interface ITaskService
{
    Task<TaskEntity> GetByIdAsync(int id);
    Task<IEnumerable<TaskEntity>> GetAllAsync();
    Task AddAsync(string title, string description, int columnId);
    Task UpdateAsync(int id, string title, string description, int columnId);
    Task DeleteAsync(int id);
}