using TaskEntity = Domain.Entities.Task;

namespace Application.Interfaces;

public interface ITaskRepository
{
    Task<TaskEntity> GetByIdAsync(int id);
    Task<IEnumerable<TaskEntity>> GetAllAsync();
    Task AddAsync(TaskEntity entity);
    Task UpdateAsync(TaskEntity entity);
    Task DeleteAsync(TaskEntity entity);
}