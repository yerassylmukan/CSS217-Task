using Domain.Entities;
using Task = System.Threading.Tasks.Task;

namespace Application.Interfaces;

public interface IBoardService
{
    Task<Board> GetByIdAsync(int id);
    Task<IEnumerable<Board>> GetAllAsync();
    Task AddAsync(string name, string ownerId);
    Task UpdateAsync(int id, string name, string ownerId);
    Task DeleteAsync(int id);
}