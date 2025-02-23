using Application.Interfaces;
using TaskEntity = Domain.Entities.Task;

namespace Application.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;
    private readonly IColumnRepository _columnRepository;

    public TaskService(ITaskRepository taskRepository, IColumnRepository columnRepository)
    {
        _taskRepository = taskRepository;
        _columnRepository = columnRepository;
    }

    public async Task<TaskEntity> GetByIdAsync(int id)
    {
        var task = await _taskRepository.GetByIdAsync(id) ?? throw new Exception("Task not found");

        return task;
    }

    public async Task<IEnumerable<TaskEntity>> GetAllAsync()
    {
        var tasks = await _taskRepository.GetAllAsync();

        return tasks;
    }

    public async Task AddAsync(string title, string description, int columnId)
    {
        var column = await _columnRepository.GetByIdAsync(columnId) ?? throw new Exception("Column not found");
        
        var task = new TaskEntity
        {
            Title = title,
            Description = description,
            ColumnId = columnId
        };

        await _taskRepository.AddAsync(task);
    }

    public async Task UpdateAsync(int id, string title, string description, int columnId)
    {
        var task = await _taskRepository.GetByIdAsync(id) ?? throw new Exception("Task not found");
        
        var column = await _columnRepository.GetByIdAsync(columnId) ?? throw new Exception("Column not found");

        task.Title = title;
        task.Description = description;

        await _taskRepository.UpdateAsync(task);
    }

    public async Task DeleteAsync(int id)
    {
        var task = await _taskRepository.GetByIdAsync(id) ?? throw new Exception("Task not found");

        await _taskRepository.DeleteAsync(task);
    }
}