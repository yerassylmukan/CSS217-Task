using Domain.DTOs;
using Domain.Entities;

namespace Application.Mappers;

public static class CommentMapper
{
    public static CommentDto MapToDto(this Comment comment)
    {
        return new CommentDto
        {
            Id = comment.Id,
            Content = comment.Content,
            UserId = comment.UserId,
            TaskId = comment.TaskId
        };
    }
}