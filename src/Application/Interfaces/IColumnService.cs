using Domain.Entities;
using Task = System.Threading.Tasks.Task;

namespace Application.Interfaces;

public interface IColumnService
{
    Task<Column> GetByIdAsync(int id);
    Task<IEnumerable<Column>> GetAllAsync();
    Task AddAsync(string name, int boardId);
    Task UpdateAsync(int id, string name, int boardId);
    Task DeleteAsync(int id);
}