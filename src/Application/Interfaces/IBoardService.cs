using Domain.DTOs;
using Task = System.Threading.Tasks.Task;

namespace Application.Interfaces;

public interface IBoardService
{
    Task<BoardDto> GetByIdAsync(int id);
    Task<IEnumerable<BoardDto>> GetAllAsync();
    Task AddAsync(string name, string ownerId);
    Task UpdateAsync(int id, string name, string ownerId);
    Task DeleteAsync(int id);
}