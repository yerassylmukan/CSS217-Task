using Domain.Entities;
using Task = System.Threading.Tasks.Task;

namespace Application.Interfaces;

public interface IBoardRepository
{
    Task<Board> GetByIdAsync(int id);
    Task<IEnumerable<Board>> GetAllAsync();
    Task AddAsync(Board entity);
    Task UpdateAsync(Board entity);
    Task DeleteAsync(Board entity);
}