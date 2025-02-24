using Domain.DTOs;

namespace Application.Interfaces;

public interface ITaskService
{
    Task<TaskDto> GetByIdAsync(int id);
    Task<IEnumerable<TaskDto>> GetAllAsync();
    Task AddAsync(string title, string description, int columnId);
    Task UpdateAsync(int id, string title, string description, int columnId);
    Task DeleteAsync(int id);
}