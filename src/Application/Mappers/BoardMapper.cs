using Domain.DTOs;
using Domain.Entities;

namespace Application.Mappers;

public static class BoardMapper
{
    public static BoardDto MapToDto(this Board board)
    {
        return new BoardDto
        {
            Id = board.Id,
            Name = board.Name,
            OwnerId = board.OwnerId,
            Columns = board.Columns.Select(column => column.MapToDto()).ToList()
        };
    }
}