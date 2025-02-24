using Domain.DTOs;
using Task = System.Threading.Tasks.Task;

namespace Application.Interfaces;

public interface ICommentService
{
    Task<CommentDto> GetByIdAsync(int id);
    Task<IEnumerable<CommentDto>> GetAllAsync();
    Task AddAsync(string content, string userId, int taskId);
    Task UpdateAsync(int id, string content, string userId, int taskId);
    Task DeleteAsync(int id);
}