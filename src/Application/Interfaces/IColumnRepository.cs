using Domain.Entities;
using Task = System.Threading.Tasks.Task;

namespace Application.Interfaces;

public interface IColumnRepository
{
    Task<Column> GetByIdAsync(int id);
    Task<IEnumerable<Column>> GetAllAsync();
    Task AddAsync(Column entity);
    Task UpdateAsync(Column entity);
    Task DeleteAsync(Column entity);
}