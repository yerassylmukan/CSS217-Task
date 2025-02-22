namespace Domain.Entities;

public class Comment
{
    public int Id { get; set; }
    public string Content { get; set; }
    public string UserId { get; set; }
    public int TaskId { get; set; }
    public Task Task { get; set; }
}