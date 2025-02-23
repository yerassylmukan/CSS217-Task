using Application.Interfaces;
using Domain.Entities;
using Task = System.Threading.Tasks.Task;

namespace Application.Services;

public class ColumnService : IColumnService
{
    private readonly IBoardRepository _boardRepository;
    private readonly IColumnRepository _columnRepository;

    public ColumnService(IColumnRepository columnRepository, IBoardRepository boardRepository)
    {
        _columnRepository = columnRepository;
        _boardRepository = boardRepository;
    }

    public async Task<Column> GetByIdAsync(int id)
    {
        var column = await _columnRepository.GetByIdAsync(id) ?? throw new ArgumentException("Column not found");

        return column;
    }

    public async Task<IEnumerable<Column>> GetAllAsync()
    {
        var columns = await _columnRepository.GetAllAsync();

        return columns;
    }

    public async Task AddAsync(string name, int boardId)
    {
        var board = await _boardRepository.GetByIdAsync(boardId) ?? throw new ArgumentException("Board not found");

        var column = new Column
        {
            Name = name,
            BoardId = boardId
        };

        await _columnRepository.AddAsync(column);
    }

    public async Task UpdateAsync(int id, string name, int boardId)
    {
        var column = await _columnRepository.GetByIdAsync(id) ?? throw new ArgumentException("Column not found");

        var board = await _boardRepository.GetByIdAsync(boardId) ?? throw new ArgumentException("Board not found");

        column.Name = name;

        await _columnRepository.UpdateAsync(column);
    }

    public async Task DeleteAsync(int id)
    {
        var column = await _columnRepository.GetByIdAsync(id) ?? throw new ArgumentException("Column not found");

        await _columnRepository.DeleteAsync(column);
    }
}