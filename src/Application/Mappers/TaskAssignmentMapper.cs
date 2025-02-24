using Domain.DTOs;
using Domain.Entities;

namespace Application.Mappers;

public static class TaskAssignmentMapper
{
    public static TaskAssignmentDto MapToDto(this TaskAssignment taskAssignment)
    {
        return new TaskAssignmentDto
        {
            TaskId = taskAssignment.TaskId,
            UserId = taskAssignment.UserId
        };
    }
}