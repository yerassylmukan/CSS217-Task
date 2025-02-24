namespace Domain.DTOs;

public class CommentDto
{
    public int Id { get; set; }
    public string Content { get; set; }
    public string UserId { get; set; }
    public int TaskId { get; set; }
}