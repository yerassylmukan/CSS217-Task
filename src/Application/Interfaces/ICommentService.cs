using Domain.Entities;
using Task = System.Threading.Tasks.Task;

namespace Application.Interfaces;

public interface ICommentService
{
    Task<Comment> GetByIdAsync(int id);
    Task<IEnumerable<Comment>> GetAllAsync();
    Task AddAsync(string content, string userId, int taskId);
    Task UpdateAsync(int id, string content, string userId, int taskId);
    Task DeleteAsync(int id);
}