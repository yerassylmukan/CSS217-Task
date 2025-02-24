using Application.Interfaces;
using Application.Mappers;
using Domain.DTOs;
using TaskEntity = Domain.Entities.Task;

namespace Application.Services;

public class TaskService : ITaskService
{
    private readonly IColumnRepository _columnRepository;
    private readonly ITaskRepository _taskRepository;

    public TaskService(ITaskRepository taskRepository, IColumnRepository columnRepository)
    {
        _taskRepository = taskRepository;
        _columnRepository = columnRepository;
    }

    public async Task<TaskDto> GetByIdAsync(int id)
    {
        var task = await _taskRepository.GetByIdAsync(id) ?? throw new Exception("Task not found");

        return task.MapToDto();
    }

    public async Task<IEnumerable<TaskDto>> GetAllAsync()
    {
        var tasks = await _taskRepository.GetAllAsync();

        var tasksDto = tasks.Select(task => task.MapToDto());

        return tasksDto;
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