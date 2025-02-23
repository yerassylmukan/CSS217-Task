using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Task = System.Threading.Tasks.Task;

namespace Persistence.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly ApplicationDbContext _dbContext;

    public CommentRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Comment> GetByIdAsync(int id)
    {
        return (await _dbContext.Comments.FirstOrDefaultAsync(c => c.Id == id))!;
    }

    public async Task<IEnumerable<Comment>> GetAllAsync()
    {
        return await _dbContext.Comments.ToListAsync();
    }

    public async Task AddAsync(Comment entity)
    {
        await _dbContext.Comments.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Comment entity)
    {
        _dbContext.Comments.Update(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Comment entity)
    {
        _dbContext.Comments.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }
}