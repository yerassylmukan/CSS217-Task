using Domain.DTOs;
using TaskEntity = Domain.Entities.Task;

namespace Application.Mappers;

public static class TaskMapper
{
    public static TaskDto MapToDto(this TaskEntity task)
    {
        return new TaskDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            ColumnId = task.ColumnId,
            Assignments = task.Assignments.Select(assignment => assignment.MapToDto()).ToList(),
            Comments = task.Comments.Select(comment => comment.MapToDto()).ToList()
        };
    }
}