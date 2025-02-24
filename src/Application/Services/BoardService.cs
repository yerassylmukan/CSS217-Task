using Application.Interfaces;
using Application.Mappers;
using Domain.DTOs;
using Domain.Entities;
using Task = System.Threading.Tasks.Task;

namespace Application.Services;

public class BoardService : IBoardService
{
    private readonly IBoardRepository _boardRepository;
    private readonly IProfileService _profileService;

    public BoardService(IBoardRepository boardRepository, IProfileService profileService)
    {
        _boardRepository = boardRepository;
        _profileService = profileService;
    }

    public async Task<BoardDto> GetByIdAsync(int id)
    {
        var board = await _boardRepository.GetByIdAsync(id) ?? throw new ArgumentException("Board not found");

        return board.MapToDto();
    }

    public async Task<IEnumerable<BoardDto>> GetAllAsync()
    {
        var boards = await _boardRepository.GetAllAsync();

        var boardsDto = boards.Select(board => board.MapToDto()).ToList();

        return boardsDto;
    }

    public async Task AddAsync(string name, string ownerId)
    {
        var checkProfile = await _profileService.CheckProfileByUserIdAsync(ownerId);

        if (checkProfile) throw new ArgumentException("Profile not found");

        var board = new Board
        {
            Name = name,
            OwnerId = ownerId
        };

        await _boardRepository.AddAsync(board);
    }

    public async Task UpdateAsync(int id, string name, string ownerId)
    {
        var board = await _boardRepository.GetByIdAsync(id) ?? throw new ArgumentException("Board not found");

        var checkProfile = await _profileService.CheckProfileByUserIdAsync(ownerId);

        if (checkProfile) throw new ArgumentException("Profile not found");

        board.Name = name;

        await _boardRepository.UpdateAsync(board);
    }

    public async Task DeleteAsync(int id)
    {
        var board = await _boardRepository.GetByIdAsync(id) ?? throw new ArgumentException("Board not found");

        await _boardRepository.DeleteAsync(board);
    }
}