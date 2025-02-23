using Domain.Entities;
using Task = System.Threading.Tasks.Task;

namespace Application.Interfaces;

public interface ICommentRepository
{
    Task<Comment> GetByIdAsync(int id);
    Task<IEnumerable<Comment>> GetAllAsync();
    Task AddAsync(Comment entity);
    Task UpdateAsync(Comment entity);
    Task DeleteAsync(Comment entity);
}