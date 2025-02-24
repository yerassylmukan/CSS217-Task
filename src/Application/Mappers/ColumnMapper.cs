using Domain.DTOs;
using Domain.Entities;

namespace Application.Mappers;

public static class ColumnMapper
{
    public static ColumnDto MapToDto(this Column column)
    {
        return new ColumnDto
        {
            Id = column.Id,
            Name = column.Name,
            BoardId = column.BoardId,
            Tasks = column.Tasks.Select(task => task.MapToDto()).ToList()
        };
    }
}