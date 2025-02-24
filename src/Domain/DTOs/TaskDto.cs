namespace Domain.DTOs;

public class TaskDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int ColumnId { get; set; }
    public ICollection<TaskAssignmentDto> Assignments { get; set; }
    public ICollection<CommentDto> Comments { get; set; }
}