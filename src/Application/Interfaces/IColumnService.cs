using Domain.DTOs;
using Task = System.Threading.Tasks.Task;

namespace Application.Interfaces;

public interface IColumnService
{
    Task<ColumnDto> GetByIdAsync(int id);
    Task<IEnumerable<ColumnDto>> GetAllAsync();
    Task AddAsync(string name, int boardId);
    Task UpdateAsync(int id, string name, int boardId);
    Task DeleteAsync(int id);
}